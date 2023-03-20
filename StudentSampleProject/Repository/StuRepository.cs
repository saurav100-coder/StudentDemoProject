
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Configuration;
using StudentSampleProject.Models;

namespace StudentSampleProject.Repository
{
    public class StuRepository
    {

        private string conString = "Server=.;database=StudentDB;Trusted_Connection=True;TrustServerCertificate=True";
        public List<StudentModel> GetAllStudents()
        {
            //connection();
            List<StudentModel> stuList = new List<StudentModel>();

            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand sqlCommand = new SqlCommand("GetStudentDetails", con);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
                DataTable dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow row in dt.Rows)
                {
                    stuList.Add(
                        new StudentModel
                        {
                            StudId = Convert.ToInt32(row["StudId"]),
                            Name = Convert.ToString(row["Name"]),
                            City = Convert.ToString(row["City"]),
                            Department = Convert.ToString(row["Department"]),
                            Gender = Convert.ToString(row["Gender"])
                        });
                }
            }
            
            return stuList;


        }
        public bool AddStudent (StudentModel student)
        {
            int i = 0;
            using(SqlConnection connection= new SqlConnection(conString))
            {
                SqlCommand sqlCommand = new SqlCommand("AddNewStuDetails", connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@Name", student.Name);
                sqlCommand.Parameters.AddWithValue("@City", student.City);
                sqlCommand.Parameters.AddWithValue("@Department", student.Department);
                sqlCommand.Parameters.AddWithValue("@Gender", student.Gender);
                connection.Open();
                i = sqlCommand.ExecuteNonQuery();
            }
            if (i >= 1 ) { return true; }
            else { return false; }
        }
        public bool UpdateStudentDetails(StudentModel student)
        {
            int i = 0;
            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand sqlCommand = new SqlCommand("UpdateStudentDetails", connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@StudId", student.StudId);
                sqlCommand.Parameters.AddWithValue("@Name", student.Name);
                sqlCommand.Parameters.AddWithValue("@City", student.City);
                sqlCommand.Parameters.AddWithValue("@Department", student.Department);
                sqlCommand.Parameters.AddWithValue("@Gender", student.Gender);
                connection.Open();
                i = sqlCommand.ExecuteNonQuery();
            }
            if (i >= 1) { return true; }
            else { return false; }
        }
        public bool DeleteStudentDetails(int? id)
        {
            int i = 0;
            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand sqlCommand = new SqlCommand("DeleteStudentDetails", connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@StudId", id);
                connection.Open();
                i = sqlCommand.ExecuteNonQuery();
            }
            if (i >= 1) { return true; }
            else { return false; }
        }
        
    }
}
