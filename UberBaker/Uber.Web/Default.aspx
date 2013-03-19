<%@ Page Language="C#" %>

<%@ Import Namespace="Uber.Core" %>
<%@ Import Namespace="Uber.Data" %>

<script runat="server">
    protected void Button1_Click(object sender, DirectEventArgs e)
    {
        using (var data = new UberContext())
        {
            var product = new Product { Name = this.txtName.Text };

            data.Products.Add(product);

            data.SaveChanges();
        
            var buffer = new StringBuilder();
        
            foreach (Product item in data.Products)
            {
                buffer.AppendFormat("{0} : {1} : {2}<br />", item.Name, item.DateCreated.ToString("HH:mm:ss"), item.DateUpdated.ToString("HH:mm:ss"));
            }

            this.txtResults.Html = buffer.ToString();
        }
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
</form>
</body>
</html>
