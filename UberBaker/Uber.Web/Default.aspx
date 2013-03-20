<%@ Page Language="C#" %>

<%@ Import Namespace="Uber.Core" %>
<%@ Import Namespace="Uber.Data" %>

<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest)
        {
            this.BindProducts();
            this.BindProductTypes();
        }
    }

    protected void GetProducts(object sender, StoreReadDataEventArgs e)
    {
        this.BindProducts();
    }

    protected void Button1_Click(object sender, DirectEventArgs e)
    {
        using (var data = new UberContext())
        {
            var product = new Product { Name = this.txtProductName.Text };

            data.Products.Add(product);

            data.SaveChanges();
        }

        this.BindProducts();
    }

    protected void Button2_Click(object sender, DirectEventArgs e)
    {
        using (var data = new UberContext())
        {
            var productType = new ProductType { Name = this.txtProductTypeName.Text };

            data.ProductTypes.Add(productType);

            data.SaveChanges();
        }

        this.BindProductTypes();
    }
    
    public void BindProducts()
    {
        var data = new UberContext();

        var store = this.GridPanel1.GetStore();

        store.Data = data.Products;
        store.DataBind();
    }

    public void BindProductTypes()
    {
        var data = new UberContext();

        var store = this.storeProductTypes;

        store.Data = data.ProductTypes;
        store.DataBind();
    }

    [DirectMethod]
    public void UpdateProduct(int id, string name, int type)
    {
        var data = new UberContext();

        var product = data.Products.Find(id);
        var productType = data.ProductTypes.Find(type);

        if (product != null && productType != null)
        {
            product.Type = productType;

            data.SaveChanges();
            this.BindProducts();

            X.Msg.Notify("Update", productType.Name).Show();
        }
    }
</script>

<!DOCTYPE html>

<html>
<head runat="server">
    <title>Ext.NET Demo</title>

    <script>
        var productTypeRenderer = function (record) {
            if (!Ext.isEmpty(record)) {
                return record.name;
            }
        };
    </script>
</head>
<body>
    <ext:ResourceManager runat="server" />

    <ext:Viewport runat="server" Layout="HBoxLayout" Margin="12">
        <Items>
            <ext:Container runat="server" Flex="1" MarginSpec="0 12 0 0">
                <Items>
                    <ext:FormPanel 
                        runat="server" 
                        Title="Product" 
                        BodyPadding="5"
                        DefaultAnchor="100%"
                        Flex="1">
                        <Items>
                            <ext:TextField ID="txtProductName" runat="server" FieldLabel="Name" />
                        </Items>
                        <Buttons>
                            <ext:Button runat="server" Text="Save" Icon="Disk" OnDirectClick="Button1_Click" />
                        </Buttons>
                    </ext:FormPanel>
                    <ext:FormPanel 
                        runat="server" 
                        Title="Product Type" 
                        BodyPadding="5"
                        DefaultAnchor="100%"
                        Flex="1"
                        MarginSpec="12 0 0 0">
                        <Items>
                            <ext:TextField ID="txtProductTypeName" runat="server" FieldLabel="Name" />
                        </Items>
                        <Buttons>
                            <ext:Button runat="server" Text="Save" Icon="Disk" OnDirectClick="Button2_Click" />
                        </Buttons>
                    </ext:FormPanel>
                </Items>
            </ext:Container>
            <ext:GridPanel 
                ID="GridPanel1"
                runat="server" 
                Title="Uber Products" 
                Height="350"
                Flex="1">
                <Bin>
                    <ext:Store ID="storeProductTypes" runat="server">
                        <Model>
                            <ext:Model runat="server">
                                <Fields>
                                    <ext:ModelField Name="id" Type="Int" />
                                    <ext:ModelField Name="name" />
                                </Fields>
                            </ext:Model>
                        </Model>
                    </ext:Store>
                </Bin>
                <Store>
                    <ext:Store runat="server" OnReadData="GetProducts" PageSize="10">
                        <Model>
                            <ext:Model runat="server">
                                <Fields>
                                    <ext:ModelField Name="id" Type="Int" />
                                    <ext:ModelField Name="name" />
                                    <ext:ModelField Name="type" IsComplex="true" />
                                    <ext:ModelField Name="dateCreated" Type="Date" />
                                    <ext:ModelField Name="dateUpdated" Type="Date" />
                                </Fields>
                            </ext:Model>
                        </Model>
                    </ext:Store>
                </Store>
                <ColumnModel>
                    <Columns>
                        <ext:Column runat="server" Text="Id" DataIndex="id" />
                        <ext:Column runat="server" Text="Name" DataIndex="name" Flex="1">
                            <Editor>
                                <ext:TextField runat="server" />
                            </Editor>
                        </ext:Column>
                        <ext:Column runat="server" Text="Type" DataIndex="type">
                            <Renderer Fn="productTypeRenderer" />
                            <Editor>
                                <ext:ComboBox 
                                    runat="server"
                                    Editable="false"
                                    StoreID="storeProductTypes"
                                    DisplayField="name"
                                    ValueField="id"
                                    />
                            </Editor>
                        </ext:Column>
                        <ext:DateColumn runat="server" Text="Date Created" DataIndex="dateCreated" Format="HH:mm:ss" />
                        <ext:DateColumn runat="server" Text="Date Updated" DataIndex="dateUpdated" Format="HH:mm:ss" />
                    </Columns>
                </ColumnModel>
                <Plugins>
                    <ext:RowEditing runat="server" SaveHandler="var data = this.editingPlugin.context.record.data; this.editingPlugin.completeEdit(); App.direct.UpdateProduct(data.id, data.name, data.type.id);" />
                </Plugins>
                <DockedItems>
                    <ext:PagingToolbar runat="server" Dock="Bottom" />
                </DockedItems>
            </ext:GridPanel>
        </Items>
    </ext:Viewport>
</body>
</html>
