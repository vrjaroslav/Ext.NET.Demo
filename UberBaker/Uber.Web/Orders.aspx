<%@ Page Language="C#" %>

<%@ Import Namespace="Uber.Core" %>
<%@ Import Namespace="Uber.Data" %>
<%@ Import Namespace="System.Data.Entity.Infrastructure" %>
<%@ Import Namespace="System.Data" %>

<script runat="server">
    
    UberContext data = new UberContext();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest)
        {
            this.BindOrders();
            this.BindProducts();
        }        
    }

    private void BindOrders()
    {
        var store = this.OrdersStore;

        store.DataSource = data.Orders.ToList();
        store.DataBind();
    }

    private void BindProducts()
    {
        var store = this.ProductsStore;

        store.DataSource = data.Products.ToList();
        store.DataBind();
    }

    protected void HandleChanges(object sender, BeforeStoreChangedEventArgs e)
    {
        e.ResponseRecords.UseModel = true;
        
        List<Order> orders = e.DataHandler.ObjectData<Order>();

        if (e.Action == StoreAction.Create)
        {
            foreach (Order created in orders)
            {
                created.OrderDate = DateTime.Now;
                created.GrossTotal = 0;
                data.Orders.Add(created);
            }

            data.SaveChanges();
            e.ResponseRecords.AddRange(orders);
        }

        if (e.Action == StoreAction.Destroy)
        {
            foreach (Order deleted in orders)
            {
                data.Orders.Remove(data.Orders.Find(deleted.Id));
            }

            data.SaveChanges();
        }

        e.Cancel = true;
    }

    [DirectMethod]
    public object SaveOrderItems(string action, Dictionary<string, object> extraParams, IEnumerable<OrderItem> orderItems)
    {
        int orderId = Convert.ToInt32(extraParams["orderId"]);
        Order order = data.Orders.Find(orderId);
        
        if (action == "create")
        {
            foreach (OrderItem created in orderItems)
            {
                created.Product = data.Products.Find(created.Product.Id);
                data.Entry(created).State = EntityState.Added;
                order.OrderItems.Add(created);
                //data.OrderItems.Add(created);
            }

            data.SaveChanges();            
        }

        if (action == "destroy")
        {
            foreach (OrderItem deleted in orderItems)
            {
                data.Entry(deleted).State = EntityState.Deleted;
                order.OrderItems.Remove(deleted);
            }

            data.SaveChanges();
        }

        if (action == "update")
        {
            foreach (OrderItem updated in orderItems)
            {
                OrderItem item = data.OrderItems.Find(updated.Id);                
                data.Entry(item).CurrentValues.SetValues(updated);
                item.Product = data.Products.Find(updated.Product.Id);

                data.Entry(item).State = EntityState.Modified;
            }

            orderItems = data.ChangeTracker.Entries<OrderItem>()
                .Where(c => c.State == EntityState.Modified)
                .Select(de => de.Entity).ToList();

            data.SaveChanges();
        }
        
        return JRawValue.From(ModelSerializer.Serialize(orderItems, Model.Get("OrderItem")));
    }

    [DirectMethod]
    public object GetOrderItems(string action, Dictionary<string, object> extraParams)
    {
        int orderId = -1;

        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        DataFilter[] filter = prms.Filter;
        if (filter.Length > 0 && filter[0].Property == "OrderId")
        {
            orderId = Convert.ToInt32(filter[0].Value);
        }

        if(orderId >= 0) 
        {

            var items = data.Orders.Include("OrderItems").Include("OrderItems.Product").First(o => o.Id == orderId).OrderItems;
            return JRawValue.From(ModelSerializer.Serialize(items, Model.Get("OrderItem")));
        }
        
        return null;
    }
    
</script>

<!DOCTYPE html>

<html>
<head runat="server">
    <title>Ext.NET Demo</title>

    <script>
        var productRenderer = function (value) {
            var r = App.ProductsStore.getById(value);

            if (Ext.isEmpty(r)) {
                return "";
            }

            return r.get("Name");
        };

        var storeExceptionHandler = function (proxy, response, operation) {
            var error = operation.getError(),
                message = Ext.isString(error) ? error : ('(' + error.status + ')' + error.statusText);

            Ext.net.Notification.show({
                iconCls    : 'icon-exclamation', 
                html       : message, 
                title      : 'EXCEPTION', 
                autoScroll : true, 
                hideDelay  : 5000, 
                width      : 300, 
                height     : 200
            });

            this.rejectChanges();
        };

        var bindOrderItems = function (orderRecord) {
            if(!orderRecord) {
                App.GridPanel2.unbindStore();
                App.OrderItemsToolbar.disable();
            }
            else if(orderRecord.phantom) {
                orderRecord.on("idchanged", bindOrderItems, this, {single:true});
            }
            else {
                App.GridPanel2.bindStore(getOrderItemsStore(orderRecord));
                App.OrderItemsToolbar.enable();
            }
        };

        var getOrderItemsStore = function (orderRecord) {
            var store = orderRecord.orderItems();

            if (!store.autoSync) {
                store.autoSync = true;
                store.showWarningOnFailure = false;
                store.remoteFilter = true;
                store.on("exception", storeExceptionHandler, store);

                store.getProxy().extraParams = {orderId: orderRecord.getId()};
            }           
            
            return store;
        };
    </script>
</head>
<body>
    <ext:ResourceManager runat="server" ScriptMode="Development" />

    <ext:Store ID="ProductsStore" runat="server">
        <Model>
            <ext:Model runat="server" Name="Product" IDProperty="Id">
                <Fields>
                    <ext:ModelField Name="Id" Type="Int" />
                    <ext:ModelField Name="Name" />
                    <ext:ModelField Name="UnitPrice" Type="Float" />
                </Fields>
            </ext:Model>
        </Model>
    </ext:Store>

    <ext:Model runat="server" IDProperty="Id" Name="OrderItem">
        <Fields>
            <ext:ModelField Name="Id" Type="Int" UseNull="true" />
            <ext:ModelField Name="Product" ServerMapping="Product.Id">
                <Serialize Handler="return Ext.isEmpty(value) ? null : {Id: value};" />
            </ext:ModelField>
            <ext:ModelField Name="UnitPrice" Type="Float" />
            <ext:ModelField Name="Quantity" Type="Int" />
        </Fields>           
        <Validations>
            <ext:PresenceValidation Field="Product" />
            <ext:PresenceValidation Field="UnitPrice" />
            <ext:PresenceValidation Field="Quantity" />
        </Validations>   
        <Proxy>
            <ext:PageProxy>
                <API Read="App.direct.GetOrderItems" Sync="App.direct.SaveOrderItems" />
            </ext:PageProxy>
        </Proxy>        
    </ext:Model>

    <ext:Container runat="server">
        <Items>
            <ext:GridPanel 
                ID="GridPanel1"
                runat="server"
                Title="Orders"
                MultiSelect="false"
                Frame="true"
                Width="400"
                Height="300">
                <Store>
                    <ext:Store 
                        ID="OrdersStore" 
                        runat="server"                        
                        AutoSync="true"
                        ShowWarningOnFailure="false"
                        OnBeforeStoreChanged="HandleChanges">
                        <Model>
                            <ext:Model runat="server" IDProperty="Id" Name="Order">
                                <Fields>
                                    <ext:ModelField Name="Id" Type="Int" UseNull="true" />
                                    <ext:ModelField Name="OrderDate" Type="Date" />
                                    <ext:ModelField Name="GrossTotal" Type="Float" />
                                </Fields>                                
                                <Associations>
                                    <ext:HasManyAssociation Model="OrderItem" Name="orderItems" ForeignKey="OrderId" PrimaryKey="Id" AutoLoad="true" />
                                </Associations>
                            </ext:Model>
                        </Model>
                        <Listeners>
                            <Exception Fn="storeExceptionHandler" />
                        </Listeners>
                    </ext:Store>
                </Store>
                <ColumnModel>
                    <Columns>
                        <ext:Column 
                            runat="server" 
                            DataIndex="Id" 
                            Text="Id" 
                            Width="50" 
                            />
                        <ext:DateColumn 
                            runat="server" 
                            DataIndex="OrderDate" 
                            Text="OrderDate"
                            Format="dd MMM yyyy">
                        </ext:DateColumn>
                        <ext:NumberColumn 
                            runat="server" 
                            DataIndex="GrossTotal" 
                            Text="Gross Total">
                            <Renderer Format="UsMoney" />
                        </ext:NumberColumn>                        
                    </Columns>
                </ColumnModel>
                <Listeners>
                    <SelectionChange Handler="bindOrderItems(selected[0]);" />
                </Listeners>
                <TopBar>
                    <ext:Toolbar runat="server">
                        <Items>
                            <ext:Button runat="server" Text="Add" Icon="Add">
                                <Listeners>
                                    <Click Handler="#{OrdersStore}.add(new Order());" />
                                </Listeners>
                            </ext:Button>
                        
                            <ext:Button runat="server" Text="Delete" Icon="Exclamation">
                                <Listeners>
                                    <Click Handler="this.up('grid').deleteSelected();" />
                                </Listeners>
                            </ext:Button>                            
                        </Items>
                    </ext:Toolbar>
                </TopBar>                           
            </ext:GridPanel>    

            <ext:GridPanel 
                ID="GridPanel2"
                runat="server"
                Title="Order Items"                
                Frame="true"
                Width="400"
                Height="300">                
                <ColumnModel>
                    <Columns>                        
                        <ext:Column 
                            runat="server" 
                            DataIndex="Product" 
                            Text="Product"
                            Flex="1">
                            <Editor>
                                <ext:ComboBox
                                    runat="server"
                                    AllowBlank="false"
                                    Editable="false"
                                    StoreID="ProductsStore"
                                    DisplayField="Name"
                                    ValueField="Id">                                    
                                </ext:ComboBox>
                            </Editor>
                            <Renderer Fn="productRenderer" />
                        </ext:Column>

                        <ext:NumberColumn 
                            runat="server" 
                            DataIndex="UnitPrice" 
                            Text="Unit Price">
                            <Renderer Format="UsMoney" />
                            <Editor>
                                <ext:NumberField runat="server" AllowBlank="false" />
                            </Editor>
                        </ext:NumberColumn>

                        <ext:NumberColumn 
                            runat="server" 
                            DataIndex="Quantity" 
                            Text="Quantity">
                            <Editor>
                                <ext:NumberField runat="server" AllowDecimals="false" AllowBlank="false" />
                            </Editor>
                        </ext:NumberColumn>
                    </Columns>
                </ColumnModel>
                <Plugins>
                    <ext:RowEditing runat="server" />
                </Plugins>
                <TopBar>
                    <ext:Toolbar ID="OrderItemsToolbar" runat="server" Disabled="true">
                        <Items>
                            <ext:Button runat="server" Text="Add" Icon="Add">
                                <Listeners>
                                    <Click Handler="var grid = this.up('grid'); grid.store.insert(0, new OrderItem()); grid.editingPlugin.startEdit(0, 0);" />
                                </Listeners>
                            </ext:Button>
                        
                            <ext:Button runat="server" Text="Delete" Icon="Exclamation">
                                <Listeners>
                                    <Click Handler="this.up('grid').deleteSelected();" />
                                </Listeners>
                            </ext:Button>                            
                        </Items>
                    </ext:Toolbar>
                </TopBar>                           
            </ext:GridPanel>
        </Items>
    </ext:Container>
</body>
</html>
