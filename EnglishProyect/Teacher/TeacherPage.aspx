<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TeacherPage.aspx.cs" Inherits="EnglishProyect.Teacher.TeacherPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Teacher Page</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>Teacher Information</h1>
            <label>Select Subject:</label>
            <asp:ListBox ID="ListBoxSubjects" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ListBoxSubjects_SelectedIndexChanged">
            </asp:ListBox>

            <br />

            <asp:GridView ID="GridStudents" runat="server" AutoGenerateColumns="True"></asp:GridView>
        </div>
    </form>
</body>
</html>
