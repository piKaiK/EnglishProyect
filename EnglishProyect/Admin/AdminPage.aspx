<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminPage.aspx.cs" Inherits="EnglishProyect.Admin.AdminPage" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8" />
    <title>Admin Page</title>
    <!-- Agrega aquí tus enlaces a hojas de estilo (CSS) si es necesario -->
</head>
<body>
    <form runat="server">
        <!-- Tabla para el primer semestre -->
        <div>
            <h3>First Semester</h3>
            <asp:GridView ID="GridSemesterA" runat="server" AutoGenerateColumns="True"></asp:GridView>
        </div>

        <!-- Tabla para el segundo semestre -->
        <div>
            <h3>Second Semester</h3>
            <asp:GridView ID="GridSemesterB" runat="server" AutoGenerateColumns="True"></asp:GridView>
        </div>

        <!-- Puedes agregar más secciones según tus necesidades -->

    </form>

    <!-- Agrega aquí tus enlaces a scripts de JavaScript si es necesario -->
</body>
</html>
