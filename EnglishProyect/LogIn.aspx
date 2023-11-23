<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LogIn.aspx.cs" Inherits="EnglishProyect.LogIn" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
        <form id="form1" runat="server">
            <div>
                 <asp:Label ID="Namelbl" runat="server" Text="NameID"></asp:Label>
                 <p>
                     <asp:TextBox ID="namebox" runat="server"></asp:TextBox>
                 </p>
                 <p>
                     <asp:Label ID="Passwordlbl" runat="server" Text="Password"></asp:Label>
                 </p>
                 <p>
                     <asp:TextBox ID="passwbox" runat="server"></asp:TextBox>
                 </p>
                 <asp:Button runat="server" id="btnLogin" Text="OK" OnClick="btnLogin_Click" />
            </div>
        </form>
</body>
</html>
