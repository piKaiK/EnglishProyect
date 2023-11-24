using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.IO;

namespace EnglishProyect.Teacher
{
    public partial class TeacherPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Se guarda el ID del profesor
                string idDelProfesor = GetUserIdSession();

                string pathDB = Server.MapPath("~/bbddEnglish.db");
                using (SQLiteConnection connection = new SQLiteConnection("Data Source=" + pathDB + ";Version=3"))
                {
                    connection.Open();

                    string query = @"SELECT DISTINCT S.name, S.semester, S.credits
                                 FROM Subjects S
                                 JOIN Teachers_s T ON S.subject_id = T.subject_id
                                 WHERE T.number_id = @IdDelProfesor";

                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        // Utilizar el parámetro específico para SQLite
                        command.Parameters.AddWithValue("@IdDelProfesor", idDelProfesor);

                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            ListBoxSubjects.DataSource = reader;
                            ListBoxSubjects.DataTextField = "name";
                            ListBoxSubjects.DataValueField = "name";
                            ListBoxSubjects.DataBind();
                        }
                    }
                }
            }
        }

        protected void ListBoxSubjects_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Obtener el ID del profesor
            string idDelProfesor = GetUserIdSession();

            // Obtener la asignatura seleccionada
            string selectedSubject = ListBoxSubjects.SelectedValue;

            // Realizar la consulta para obtener estudiantes de la asignatura seleccionada
            string query = @"SELECT U.name, U.surname, U.number_id
                         FROM Users U
                         JOIN Students_s SS ON U.number_id = SS.number_id
                         JOIN Subjects S ON SS.subject_id = S.subject_id
                         JOIN Teachers_s T ON S.subject_id = T.subject_id
                         WHERE T.number_id = @IdDelProfesor AND S.name = @SelectedSubject"
            ;

            string pathDB = Server.MapPath("~/bbddEnglish.db");
            using (SQLiteConnection connection = new SQLiteConnection("Data Source=" + pathDB + ";Version=3"))
            {
                connection.Open();

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdDelProfesor", idDelProfesor);
                    command.Parameters.AddWithValue("@SelectedSubject", selectedSubject);

                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        DataTable dataTable = new DataTable();
                        dataTable.Load(reader);

                        // Actualizar la tabla de estudiantes con los resultados de la consulta
                        GridStudents.DataSource = dataTable;
                        GridStudents.DataBind();
                    }
                }
            }
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
                Response.Redirect("PaginaError.aspx");
                return null;
            }
        }
    }

}