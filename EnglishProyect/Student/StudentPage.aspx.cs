﻿using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EnglishProyect.Student
{
    public partial class StudentPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            editSection.Visible = false;
            infoSection.Visible = true;

            if (!IsPostBack)
            {
                // Obtén el User ID del contexto de la sesión
                string userId = GetUserIdSession();

                // Asegúrate de que el User ID no sea nulo o vacío antes de cargar la información
                if (!string.IsNullOrEmpty(userId))
                {
                    LoadStudentInformation(userId);
                    LoadEnrolledSubjects(userId);
                }
            }
        }

        private void LoadStudentInformation(string userId)
        {
            // Lógica para cargar la información del estudiante desde la base de datos
            DataTable dt = GetStudentInformation(userId);

            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];

                // Mostrar la información en las etiquetas
                lblUserId.Text = row["number_id"].ToString();
                lblName.Text = row["name"].ToString();
                lblSurname.Text = row["surname"].ToString();
                lblDob.Text = row["dob"].ToString();
                lblNationality.Text = row["nationality"].ToString();
                lblAddress.Text = row["address"].ToString();

                // También puedes asignar estos valores a los TextBoxes en modo de edición si es necesario
                txtUserid.Text = row["number_id"].ToString();
                txtName.Text = row["name"].ToString();
                txtSurname.Text = row["surname"].ToString();
                txtDob.Text = row["dob"].ToString();
                txtNationality.Text = row["nationality"].ToString();
                txtAddress.Text = row["address"].ToString();
            }
        }
        private void LoadEnrolledSubjects(string userId)
        {
            // Lógica para obtener las asignaturas inscritas por el estudiante desde la base de datos
            DataTable dt = GetEnrolledSubjects(userId);

            // Vincular datos al GridView
            gridSubjects.DataSource = dt;
            gridSubjects.DataBind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            // Obtén el User ID del contexto de la sesión
            string userId = GetUserIdSession();

            // Lógica para guardar los cambios en la base de datos
            SaveChangesInDatabase(userId);

            // Oculta el botón de guardar y muestra el botón de edición
            editSection.Visible = false;
            infoSection.Visible = true;

            // Recarga la información del estudiante después de guardar cambios
            LoadStudentInformation(userId);
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {

            // Oculta el botón de edición y muestra el botón de guardar
            infoSection.Visible = false;
            editSection.Visible = true;
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
        private DataTable GetStudentInformation(string userId)
        {
            DataTable dt = new DataTable();

            // Query para obtener la información del estudiante
            string query = "SELECT number_id, name, surname, dob, nationality, address FROM Users WHERE number_id = @number_id";

            // Conexión a la base de datos
            string pathDB = Server.MapPath("~/bbddEnglish.db");
            using (SQLiteConnection connection = new SQLiteConnection("Data Source=" + pathDB + ";Version=3"))
            {
                connection.Open();

                using (SQLiteCommand cmd = new SQLiteCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@number_id", userId);

                    using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
                    }
                }
            }

            return dt;
        }

        private void SaveChangesInDatabase(string userId)
        {
            // Obtén los nuevos valores de los TextBoxes
            string newName = txtName.Text;
            string newSurname = txtSurname.Text;
            string newDob = txtDob.Text;
            string newNationality = txtNationality.Text;
            string newAddress = txtAddress.Text;

            // Lógica para actualizar la información del estudiante en la base de datos
            string query = "UPDATE Users SET name = @Name, surname = @Surname, dob = @Dob, nationality = @Nationality, address = @Address WHERE number_id = @UserId";

            string pathDB = Server.MapPath("~/bbddEnglish.db");
            using (SQLiteConnection connection = new SQLiteConnection("Data Source=" + pathDB + ";Version=3"))
            {
                connection.Open();

                using (SQLiteCommand cmd = new SQLiteCommand(query, connection))
                {
                    // Asigna los nuevos valores como parámetros
                    cmd.Parameters.AddWithValue("@Name", newName);
                    cmd.Parameters.AddWithValue("@Surname", newSurname);
                    cmd.Parameters.AddWithValue("@Dob", newDob);
                    cmd.Parameters.AddWithValue("@Nationality", newNationality);
                    cmd.Parameters.AddWithValue("@Address", newAddress);
                    cmd.Parameters.AddWithValue("@UserId", userId);

                    // Ejecuta la actualización
                    cmd.ExecuteNonQuery();
                }
            }
        }
        private DataTable GetEnrolledSubjects(string userId)
        {
            DataTable dt = new DataTable();

            // Query para obtener las asignaturas en las que está inscrito el estudiante
            string query = @"
        SELECT
            Subjects.subject_id AS SubjectID,
            Subjects.name AS Name,
            Subjects.semester AS Semester,
            Subjects.credits AS Credits,
            Teachers.name AS TeacherName
        FROM
            Subjects
        JOIN
            Students_s ON Subjects.subject_id = Students_s.subject_id
        JOIN
            Teachers_s ON Subjects.subject_id = Teachers_s.subject_id
        JOIN
            Users AS Teachers ON Teachers_s.number_id = Teachers.number_id
        WHERE
            Students_s.number_id = @UserId;
    ";

            // Conexión a la base de datos
            string pathDB = Server.MapPath("~/bbddEnglish.db");
            using (SQLiteConnection connection = new SQLiteConnection("Data Source=" + pathDB + ";Version=3"))
            {
                connection.Open();

                using (SQLiteCommand cmd = new SQLiteCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);

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