<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TeacherPage.aspx.cs" Inherits="EnglishProyect.Teacher.TeacherPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Teacher Page</title>
</head>
<body>
    <form runat="server">
        <h2>Teacher Page</h2>

        <div>
            <h3>Subjects Taught</h3>
            <asp:Repeater ID="rptSubjects" runat="server">
                <ItemTemplate>
                    <div>
                        <p>Subject Name: <%# Eval("Name") %></p>
                        <!-- Add more details as needed -->
                        <asp:Repeater ID="rptStudents" runat="server">
                            <ItemTemplate>
                                <p>Student: <%# Eval("StudentName") %></p>
                                <!-- Add more student details as needed -->
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </form>
</body>
</html>
