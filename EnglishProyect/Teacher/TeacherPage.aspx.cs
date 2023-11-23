using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EnglishProyect.Teacher
{
    public partial class TeacherPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Obtén el User ID del contexto de la sesión
                string userId = GetUserIdSession();

                // Asegúrate de que el User ID no sea nulo o vacío antes de cargar la información
                if (!string.IsNullOrEmpty(userId))
                {
                    LoadSubjectsTaught(userId);
                }
            }
        }

        private void LoadSubjectsTaught(string userId)
        {
            // Lógica para cargar las asignaturas que enseña el profesor y los estudiantes inscritos
            DataTable subjectsTable = GetSubjectsTaughtFromDatabase(userId);

            rptSubjects.DataSource = subjectsTable;
            rptSubjects.DataBind();
        }

        private DataTable GetSubjectsTaughtFromDatabase(string userId)
        {
            // Lógica para obtener las asignaturas que enseña el profesor y los estudiantes inscritos
            // Utiliza userId para filtrar las asignaturas del profesor y las tablas relacionadas para obtener información de los estudiantes
            // ...

            // Retorna un DataTable con los datos de las asignaturas y estudiantes
            return new DataTable();
        }

        private string GetUserIdSession()
        {
            if (Session["UserId"] != null)
            {
                return Session["UserId"].ToString();
            }
            else
            {
                // Puedes manejar aquí la situación en la que el User ID no está en la sesión
                // Puedes redirigir a la página de login u otro manejo de error
                Response.Redirect("PaginaError.aspx");
                return null;
            }
        }
    }
}