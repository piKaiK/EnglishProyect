using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EnglishProyect
{
    public partial class LogIn : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        //--------------------------------------------------------------------
        //
        // Función para autenticar los usuarios en la bbdd
        //
        //--------------------------------------------------------------------

        // Función para autenticar los usuarios en la base de datos y obtener el rol y User ID.
        private Tuple<string, string> AutenticarUsuario(string username, string password)
        {
            // Query para consultar a la base de datos los datos
            string query = "SELECT user_rol, number_id FROM Users WHERE number_id = @number_id AND password = @password";

            // Conexión a la base de datos
            string pathDB = Server.MapPath("~/bbddEnglish.db");
            using (SQLiteConnection connection = new SQLiteConnection("Data Source=" + pathDB + ";Version=3"))
            {
                connection.Open();

                using (SQLiteCommand cmd = new SQLiteCommand(query, connection))
                {
                    // Parametriza las consultas para prevenir inyección SQL.
                    cmd.Parameters.AddWithValue("@number_id", username);
                    cmd.Parameters.AddWithValue("@password", password);

                    // Ejecuta la consulta y obtén el resultado.
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // El usuario existe y las credenciales son correctas.
                            // Retorna el rol y el User ID del usuario.
                            string userRole = reader["user_rol"].ToString();
                            string userId = reader["number_id"].ToString();
                            return Tuple.Create(userRole, userId);
                        }
                        else
                        {
                            // El usuario no existe o las credenciales son incorrectas.
                            return null;
                        }
                    }
                }
            }
        }

        //--------------------------------------------------------------------
        //
        // Proceso al clickar el botón OK
        //
        //--------------------------------------------------------------------

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string username = namebox.Text; // Obtiene el nombre de usuario desde el campo de texto.
            string password = passwbox.Text; // Obtiene la contraseña desde el campo de texto.

            // Llamada a la autenticación del usuario, devolviendo el rol y el User ID.
            Tuple<string, string> userData = AutenticarUsuario(username, password);

            // Si no es null
            if (userData != null)
            {
                string userRole = userData.Item1;
                string userId = userData.Item2;

                // Almacena el User ID en la sesión
                Session["UserId"] = userId;

                // Redirige al usuario según su rol.
                if (userRole.Equals("teacher", StringComparison.OrdinalIgnoreCase))
                {
                    Response.Redirect("~/Teacher/TeacherPage.aspx");
                }
                else if (userRole.Equals("student", StringComparison.OrdinalIgnoreCase))
                {
                    Response.Redirect("~/Student/StudentPage.aspx");
                }
                else if (userRole.Equals("admin", StringComparison.OrdinalIgnoreCase))
                {
                    Response.Redirect("~/Admin/AdminPage.aspx");
                }
                else
                {
                    // Si pertenece a otro rol, enviar a una página de error.
                  //  Response.Redirect("PaginaError.aspx");
                }
            }
            else
            {
                // La autenticación falló, puedes mostrar un mensaje de error o redirigir a una página de error.
               // Response.Redirect("PaginaError.aspx");
            }
        }
    }
}