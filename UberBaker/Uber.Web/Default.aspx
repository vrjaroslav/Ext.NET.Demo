<%@ Page Language="C#" %>

<%@ Import Namespace="Uber.Core" %>
<%@ Import Namespace="Uber.Data" %>

<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest)
        {
            var data = new UberContext();

            var store = this.GridPanel1.GetStore();

            store.Data = data.Products;
            store.DataBind();
        }
    }

    protected void Button1_Click(object sender, DirectEventArgs e)
    {
        using (var data = new UberContext())
        {
            var product = new Product { Name = this.txtName.Text };

            data.Products.Add(product);

            data.SaveChanges();
        }
        
        this.GridPanel1.GetStore().Reload();
    }
</script>

<!DOCTYPE html>

<html>
<head runat="server">
    <title>Ext.NET Demo</title>
</head>
<body>
<form runat="server">
    <ext:ResourceManager runat="server" />

    <ext:TextField ID="txtName" runat="server" FieldLabel="Name" />

    <ext:Button runat="server" Text="Submit" OnDirectClick="Button1_Click" />

    <ext:DisplayField ID="txtResults" runat="server" />

    <ext:GridPanel 
        ID="GridPanel1"
        runat="server" 
        Title="Productss" 
        Width="600" 
        Height="350">
        <Store>
            <ext:Store runat="server">
                <Model>
                    <ext:Model runat="server">
                        <Fields>
                            <ext:ModelField Name="Id" />
                            <ext:ModelField Name="Name" />
                            <ext:ModelField Name="DateCreated" Type="Date" />
                            <ext:ModelField Name="DateUpdated" Type="Date" />
                        </Fields>
                    </ext:Model>
                </Model>
            </ext:Store>
        </Store>
        <ColumnModel>
            <Columns>
                <ext:Column runat="server" Text="Name" DataIndex="Name" Flex="1" />
                <ext:DateColumn runat="server" Text="Date Created" DataIndex="DateCreated" Format="HH:mm:ss" />
                <ext:DateColumn runat="server" Text="Date Updated" DataIndex="DateUpdated" Format="HH:mm:ss" />
            </Columns>
        </ColumnModel>
        <SelectionModel>
            <ext:RowSelectionModel runat="server" />
        </SelectionModel>
    </ext:GridPanel>
</form>
</body>
</html>
