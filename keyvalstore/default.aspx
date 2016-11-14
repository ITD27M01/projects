<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="keyvalstore.Default" %>

<%
    ip.Text = Request.UserHostAddress;
    hostname.Text = System.Net.Dns.GetHostEntry(ip.Text).HostName;
%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label runat="server" id="ip"></asp:Label>
        <br/>
        <asp:Label runat="server" id="hostname"></asp:Label>
    </div>
    </form>
</body>
</html>