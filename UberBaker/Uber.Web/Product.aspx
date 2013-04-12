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
            this.BindProductTypes();
            this.BindProducts();
        }        
    }

    private void BindProducts()
    {
        var store = this.Store1;

        store.DataSource = data.Products.ToList();
        store.DataBind();
    }

    private void BindProductTypes()
    {
        var store = this.ProductTypeStore;

        store.DataSource = data.ProductTypes.ToList();
        store.DataBind();
    }

    protected void CheckProductName(object sender, RemoteValidationEventArgs e)
    {
        if (e.Value == null || string.IsNullOrWhiteSpace(e.Value.ToString()))
        {
            e.Success = false;
            e.ErrorMessage = "Product name cannot be empty";
            
            return;
        }

        string name = e.Value.ToString();
        bool found = data.Products.Any(pt => pt.Name == name);

        e.Success = !found;
        
        if (found)
        {
            e.ErrorMessage = string.Format("Product with '{0}' name is already exists", name);
        }
    }

    protected void HandleChanges(object sender, BeforeStoreChangedEventArgs e)
    {
        var converters = new List<Newtonsoft.Json.JsonConverter> { new ProductTypeJsonConverter() };
        List<Product> products = e.DataHandler.ObjectData<Product>(converters);
        e.ResponseRecords.Converters = converters;
        
        if (e.Action == StoreAction.Create)
        {
            foreach (Product created in products)
            {
                if (data.Products.Any(pt => pt.Name == created.Name))
                {
                    throw new Exception(string.Format("Product with '{0}' name is already exists", created.Name));
                }
                created.Type = data.ProductTypes.Find(created.Type.Id);
                data.Products.Add(created);                
            }

            data.SaveChanges();
            e.ResponseRecords.AddRange(products);
        }

        if (e.Action == StoreAction.Destroy)
        {
            foreach (Product deleted in products)
            {
                data.Products.Remove(data.Products.Find(deleted.Id));
            }
            
            data.SaveChanges();
        }

        if (e.Action == StoreAction.Update)
        {
            foreach (Product updated in products)
            {
                Product product = data.Products.Find(updated.Id);
                data.Entry(product).CurrentValues.SetValues(updated);
                product.Type = data.ProductTypes.Find(updated.Type.Id);
                
                data.Entry(product).State = EntityState.Modified;                
            }

            var modified = data.ChangeTracker.Entries<Product>().Where(c => c.State == EntityState.Modified);
            e.ResponseRecords.AddRange(modified.Select(de=>de.Entity));
            
            data.SaveChanges();
        }
       
        e.Cancel = true;
    }
</script>

<!DOCTYPE html>

<html>
<head runat="server">
    <title>Ext.NET Demo</title>

    <script>
        var productTypeRenderer = function (value) {
            var r = App.ProductTypeStore.getById(value);

            if (Ext.isEmpty(r)) {
                return "";
            }

            return r.get('Name');
        };

        var updateProduct = function (form) {
            if (!form.getForm().getBoundRecord()) {
                Ext.net.Notification.show({
                    iconCls  : "icon-exclamation",
                    html     : "There is no bound record",
                    title    : "Error"
                });

                return false;
            }
            
            if (!form.getForm().isValid()) {
                Ext.net.Notification.show({
                    iconCls  : "icon-exclamation",
                    html     : "Product is invalid",
                    title    : "Error"
                });

                return false;
            }
            
            form.getForm().updateRecord();
        };

        var addProduct = function (form, store) {
            if (!form.getForm().isValid()) {
                Ext.net.Notification.show({
                    iconCls  : "icon-exclamation",
                    html     : "Product is invalid",
                    title    : "Error"
                });
            
                return false;
            }
            
            store.insert(0, new Product(form.getForm().getValues()));
            form.getForm().reset();
        };

        var insertRecord = function (grid, form) {
            var store = grid.store;

            store.insert(0, new Product());
            grid.getSelectionModel().select(0);
            form.getComponent(0).focus();
        };
    </script>
</head>
<body>
    <ext:ResourceManager runat="server" />

    <ext:Store 
        ID="ProductTypeStore" 
        runat="server">
        <Model>
            <ext:Model runat="server" IDProperty="Id">
                <Fields>
                    <ext:ModelField Name="Id" Type="Int" />
                    <ext:ModelField Name="Name" />
                </Fields>
            </ext:Model>
        </Model>
    </ext:Store>

    <ext:Container runat="server">
        <Items>
            <ext:FormPanel 
                runat="server"
                Width="400"                
                Frame="true"
                Layout="FormLayout"
                MarginSpec="0 0 10 0">
                <Items>
                    <ext:TextField 
                        ID="ProductName" 
                        runat="server"                        
                        Name="Name"
                        AllowBlank="false"
                        MsgTarget="Under"
                        FieldLabel="Name">
                    </ext:TextField>

                    <ext:TextArea runat="server"
                        Name="Description"
                        FieldLabel="Description"
                        Height="100" />

                    <ext:NumberField runat="server" 
                        Name="UnitPrice"
                        FieldLabel="Unit Price"
                        AllowBlank="false"/>

                    <ext:ComboBox runat="server"
                        Name="Type"
                        FieldLabel="Type"
                        AllowBlank="false"
                        Editable="false"
                        StoreID="ProductTypeStore"
                        DisplayField="Name"
                        ValueField="Id">                        
                    </ext:ComboBox>
                </Items>
                <Buttons>
                    <ext:Button 
                            runat="server"
                            FormBind="true"
                            Text="Update"
                            Icon="Disk">
                            <Listeners>
                                <Click Handler="updateProduct(this.up('form'));" />
                            </Listeners>
                        </ext:Button>
                
                        <ext:Button 
                            runat="server"
                            FormBind="true"
                            Text="Create"
                            Icon="Add">
                            <Listeners>
                                <Click Handler="addProduct(this.up('form'), this.up('form').next().store);" />
                            </Listeners>
                        </ext:Button>
                
                        <ext:Button 
                            runat="server"
                            Text="Reset">
                            <Listeners>
                                <Click Handler="this.up('form').getForm().reset();" />
                            </Listeners>
                        </ext:Button>
                </Buttons>
            </ext:FormPanel>

            <ext:GridPanel 
                ID="GridPanel1"
                runat="server"
                Title="Products"
                MultiSelect="false"
                Frame="true"
                Width="400"
                Height="300">
                <Store>
                    <ext:Store 
                        ID="Store1" 
                        runat="server" 
                        AutoSync="true"
                        ShowWarningOnFailure="false"
                        OnBeforeStoreChanged="HandleChanges">
                        <Model>
                            <ext:Model Name="Product" runat="server" IDProperty="Id">
                                <Fields>
                                    <ext:ModelField Name="Id" Type="Int" UseNull="true" />
                                    <ext:ModelField Name="Name" />
                                    <ext:ModelField Name="Description" />
                                    <ext:ModelField Name="UnitPrice" Type="Float" />
                                    <ext:ModelField Name="Type" ServerMapping="Type.Id" Type="Int" UseNull="true" />
                                </Fields>
                                <Validations>
                                    <ext:PresenceValidation Field="Name" />
                                    <ext:PresenceValidation Field="UnitPrice" />
                                    <ext:PresenceValidation Field="Type" />
                                </Validations>
                            </ext:Model>
                        </Model>
                        <Listeners>
                            <Exception Handler="
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

                                this.rejectChanges();" />
                        </Listeners>
                    </ext:Store>
                </Store>
                <ColumnModel>
                    <Columns>
                        <ext:Column runat="server" DataIndex="Name" Text="Name" Flex="1" />
                        <ext:NumberColumn runat="server" DataIndex="UnitPrice" Text="Unit Price">
                            <Renderer Format="UsMoney" />
                        </ext:NumberColumn>
                        <ext:Column runat="server" 
                            DataIndex="Type" 
                            Text="Type">
                            <Renderer Fn="productTypeRenderer" />
                        </ext:Column>
                    </Columns>
                </ColumnModel>
                <Listeners>
                    <Select Handler="this.prev().getForm().loadRecord(record);" />
                </Listeners>

                <TopBar>
                    <ext:Toolbar runat="server">
                        <Items>
                            <ext:Button runat="server" Text="Add" Icon="Add">
                                <Listeners>
                                    <Click Handler="insertRecord(this.up('grid'), this.up('grid').prev());" />
                                </Listeners>
                            </ext:Button>
                        
                            <ext:Button runat="server" Text="Delete" Icon="Exclamation">
                                <Listeners>
                                    <Click Handler="this.up('grid').deleteSelected(); this.up('grid').prev().getForm().reset();" />
                                </Listeners>
                            </ext:Button>

                            <ext:Button runat="server" Text="Sync" Icon="Disk" Hidden="true">
                                <Listeners>
                                    <Click Handler="this.up('grid').store.sync();" />
                                </Listeners>
                            </ext:Button>
                        
                            <ext:ToolbarSeparator />
                        
                            <ext:Button 
                                runat="server" 
                                Text="Auto Sync"
                                EnableToggle="true"
                                Pressed="true"
                                ToolTip="When enabled, Store will execute Ajax requests as soon as a Record becomes dirty.">
                                <Listeners>
                                    <Toggle Handler="this.up('grid').store.autoSync = pressed; this.prev('button').setVisible(!pressed)" />
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
