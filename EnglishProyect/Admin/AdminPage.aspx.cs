using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EnglishProyect.Admin
{
    public partial class AdminPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Verifica si es una postback para evitar recargar datos en cada postback
            if (!IsPostBack)
            {
                // Carga asignaturas para el primer y segundo semestre
                LoadSubjectsBySemester();
            }
        }

        private void LoadSubjectsBySemester()
        {
            // Lógica para cargar asignaturas del primer semestre
            DataTable dtSemester1 = GetSubjectsBySemester(1);
            GridSemesterA.DataSource = dtSemester1;
            GridSemesterA.DataBind();

            // Lógica para cargar asignaturas del segundo semestre
            DataTable dtSemester2 = GetSubjectsBySemester(2);
            GridSemesterB.DataSource = dtSemester2;
            GridSemesterB.DataBind();
        }

        private DataTable GetSubjectsBySemester(int semester)
        {
            // Lógica para recuperar asignaturas de la base de datos según el semestre
            DataTable dt = new DataTable();
            string query = "SELECT * FROM Subjects WHERE Semester = @Semester";

            string pathDB = Server.MapPath("~/bbddEnglish.db");
            using (SQLiteConnection connection = new SQLiteConnection("Data Source=" + pathDB + ";Version=3"))
            {
                connection.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Semester", semester);

                    using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
                    }
                }
            }

            return dt;
        }

    }
}