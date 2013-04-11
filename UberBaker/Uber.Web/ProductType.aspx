<%@ Page Language="C#" %>

<%@ Import Namespace="Uber.Core" %>
<%@ Import Namespace="Uber.Data" %>
<%@ Import Namespace="System.Data.Entity.Infrastructure" %>

<script runat="server">
    
    UberContext dbContext = new UberContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest)
        {
            this.BindProductTypes();
        }        
    }

    private void BindProductTypes()
    {
        var store = this.Store1;

        store.DataSource = dbContext.ProductTypes.ToList();
        store.DataBind();
    }

    protected void CheckProductType(object sender, RemoteValidationEventArgs e)
    {
        if (e.Value == null || string.IsNullOrWhiteSpace(e.Value.ToString()))
        {
            e.Success = false;
            e.ErrorMessage = "Product type cannot be empty";
            return;
        }

        string type = e.Value.ToString();
        bool found = dbContext.ProductTypes.Any(pt => pt.Name == type);

        e.Success = !found;
        
        if (found)
        {
            e.ErrorMessage = string.Format("Product type with '{0}' name is already exists", type);
        }
    }

    protected void HandleChanges(object sender, BeforeStoreChangedEventArgs e)
    {
        List<ProductType> productTypes = e.DataHandler.ObjectData<ProductType>();

        if (e.Action == StoreAction.Create)
        {
            foreach (ProductType created in productTypes)
            {
                if (dbContext.ProductTypes.Any(pt => pt.Name == created.Name))
                {
                    throw new Exception(string.Format("Product type with '{0}' name is already exists", created.Name));
                }
                
                dbContext.ProductTypes.Add(created);                
            }

            dbContext.SaveChanges();
            e.ResponseRecords.AddRange(productTypes);
        }

        if (e.Action == StoreAction.Destroy)
        {
            foreach (ProductType deleted in productTypes)
            {

                //dbContext.ProductTypes.Remove(dbContext.ProductTypes.Find(deleted.Id));
                dbContext.ProductTypes.Remove(deleted);
            }
            
            dbContext.SaveChanges();
        }

        if (e.Action == StoreAction.Update)
        {
            List<ProductType> updatedEntities = new List<ProductType>(productTypes.Count);
            
            foreach (ProductType updated in productTypes)
            {
                ProductType pType = dbContext.ProductTypes.Find(updated.Id);
                dbContext.Entry(pType).CurrentValues.SetValues(updated);
                updatedEntities.Add(pType);
            }

            dbContext.SaveChanges();
            
            e.ResponseRecords.AddRange(updatedEntities);
        }
        
        e.Cancel = true;
    }
</script>

<!DOCTYPE html>

<html>
<head runat="server">
    <title>Ext.NET Demo</title>

    <script>
        var updateProductType = function (form) {
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
                    html     : "Product type is invalid",
                    title    : "Error"
                });

                return false;
            }
            
            form.getForm().updateRecord();
        };

        var addProductType = function (form, store) {
            if (!form.getForm().isValid()) {
                Ext.net.Notification.show({
                    iconCls  : "icon-exclamation",
                    html     : "Product type is invalid",
                    title    : "Error"
                });
            
                return false;
            }
            
            store.insert(0, new ProductType(form.getForm().getValues()));
            form.getForm().reset();
        };

        var insertRecord = function (grid, form) {
            var store = grid.store;

            store.insert(0, new ProductType());
            grid.getSelectionModel().select(0);
            form.getComponent(0).focus();
        };
    </script>
</head>
<body>
    <ext:ResourceManager runat="server" />

    <ext:Container runat="server">
        <Items>
            <ext:FormPanel 
                runat="server"
                Width="400"
                Height="90"
                Frame="true"
                Layout="FormLayout"
                MarginSpec="0 0 10 0">
                <Items>
                    <ext:TextField 
                        ID="ProductTypeName" 
                        runat="server"                        
                        Name="Name"
                        AllowBlank="false"
                        MsgTarget="Under"
                        FieldLabel="Product Type"
                        IsRemoteValidation="true">
                        <RemoteValidation 
                            ShowBusy="true" 
                            OnValidation="CheckProductType" 
                            InitValueValidation="Invalid" 
                            />                   
                    </ext:TextField>
                </Items>
                <Buttons>
                    <ext:Button 
                            runat="server"
                            FormBind="true"
                            Text="Update"
                            Icon="Disk">
                            <Listeners>
                                <Click Handler="updateProductType(this.up('form'));" />
                            </Listeners>
                        </ext:Button>
                
                        <ext:Button 
                            runat="server"
                            FormBind="true"
                            Text="Create"
                            Icon="Add">
                            <Listeners>
                                <Click Handler="addProductType(this.up('form'), this.up('form').next().store);" />
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
                Title="Product Types"
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
                            <ext:Model Name="ProductType" runat="server" IDProperty="Id">
                                <Fields>
                                    <ext:ModelField Name="Id" Type="Int" UseNull="true" />
                                    <ext:ModelField Name="Name" />
                                </Fields>
                                <Validations>
                                    <ext:PresenceValidation Field="Name" />                                    
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

                                this.rejectChanges({ destroy : true });" />
                        </Listeners>
                    </ext:Store>
                </Store>
                <ColumnModel>
                    <Columns>
                        <ext:Column runat="server" DataIndex="Name" Text="Name" Flex="1" />
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
