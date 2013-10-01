<%@ Page Language="C#" %>

<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="System.Linq" %>
<%@ Import Namespace="Ext.Net.Utilities" %>
<%@ Import Namespace="Newtonsoft.Json" %>
<%@ Import Namespace="System.ComponentModel" %>
<%@ Import Namespace="Uber.Data" %>
<%@ Import Namespace="Uber.Core" %>
<%@ Import Namespace="System.Data.Entity" %>

<%@ Register assembly="Ext.Net" namespace="Ext.Net" tagprefix="ext" %>

<script runat="server">
    UberContext data = new UberContext();
    
    protected void Button1_Click(object sender, DirectEventArgs e)
    {
        var log = new StringBuilder();

        var orders = data.Orders
            .Take(3);

        foreach (Order order in orders)
        {
            log.AppendFormat("{0}<br />", order.Id);
            
            //var orderItems = order.OrderItems.ToList();
            
            //foreach (OrderItem item in orderItems)
            //{
            //    log.AppendFormat("{0}<br />", item.Product.Type.Name);
            //}
        }

        this.Label1.Html = log.ToString();
    }
</script>

<!DOCTYPE html>

<html>
<head runat="server">
    <title>Testing</title>
    <link href="//netdna.bootstrapcdn.com/twitter-bootstrap/2.3.1/css/bootstrap-combined.min.css" rel="stylesheet">
</head>
<body>
<form runat="server" style="margin:30px;">
    <ext:ResourceManager runat="server" />
    <ext:Button 
        runat="server" 
        Text="Submit" 
        Icon="Accept">
        <DirectEvents>
            <Click OnEvent="Button1_Click" Timeout="300000" />
        </DirectEvents>
    </ext:Button>
<pre>
<ext:Label ID="Label1" runat="server" />
</pre>
</form>
</body>
</html>