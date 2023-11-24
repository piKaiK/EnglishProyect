<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StudentPage.aspx.cs" Inherits="EnglishProyect.Student.StudentPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form runat="server">
        <h2>Student Page</h2>

        <div id="infoSection" runat="server">
            <h3>Student Information</h3>
            <label>User ID:</label>
            <asp:Label ID="lblUserId" runat="server"></asp:Label><br />
            
            <label>Name:</label>
            <asp:Label ID="lblName" runat="server"></asp:Label><br />

            <label>Surname:</label>
            <asp:Label ID="lblSurname" runat="server"></asp:Label><br />

            <label>Date of Birth:</label>
            <asp:Label ID="lblDob" runat="server"></asp:Label><br />

            <label>Nationality:</label>
            <asp:Label ID="lblNationality" runat="server"></asp:Label><br />

            <label>Address:</label>
            <asp:Label ID="lblAddress" runat="server"></asp:Label><br />

            <asp:Button ID="btnEdit" runat="server" Text="Edit Information" OnClick="btnEdit_Click" />
        </div>

        <!-- Add your textboxes for editing here -->
        <div id="editSection" runat="server">
            <!-- Textboxes for editing -->

            <label>User ID:</label>
            <asp:TextBox ID="txtUserid" runat="server"></asp:TextBox><br />

            <label>Name:</label>
            <asp:TextBox ID="txtName" runat="server"></asp:TextBox><br />

            <label>Surname:</label>
            <asp:TextBox ID="txtSurname" runat="server"></asp:TextBox><br />

            <label>Date of Birth:</label>
            <asp:TextBox ID="txtDob" runat="server"></asp:TextBox><br />

            <label>Nationality:</label>
            <asp:TextBox ID="txtNationality" runat="server"></asp:TextBox><br />

            <label>Address:</label>
            <asp:TextBox ID="txtAddress" runat="server"></asp:TextBox><br />

            <asp:Button ID="btnSave" runat="server" Text="Save Changes" OnClick="btnSave_Click" />
        </div>

        <asp:GridView ID="gridSubjects" runat="server" AutoGenerateColumns="True"></asp:GridView>

    </form>
</body>
</html>
