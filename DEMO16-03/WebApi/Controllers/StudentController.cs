using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.SqlClient;
using System.Data;
using WebApi.Models;
using System.Collections;

namespace WebApi.Controllers
{
    public class StudentController : ApiController
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-A4571O2;Initial Catalog=demo;Integrated Security=True");

        //Function to get all Student from database
        [Route("api/Student")]
        [HttpGet]
        [ActionName("GetAllStudent")]
        public ArrayList GetAllStudent()
        {
            SqlDataReader rd = null;
            SqlCommand cmd = new SqlCommand("getAll", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            rd = cmd.ExecuteReader();
            ArrayList arrStu = new ArrayList();
            while (rd.Read())
            {
                Student std = new Student();
                std.id = Convert.ToInt32(rd.GetValue(0));
                std.name = rd.GetValue(1).ToString();
                std.email = rd.GetValue(2).ToString();
                arrStu.Add(std);
            }

            con.Close();
            return arrStu;
        }

        //Function get student by id 
        [Route("api/Student/{id}")]
        [HttpGet]
        public Student GetStudentById(int id)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("getStudentById", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id",id);
            SqlDataReader rd = null;
            rd = cmd.ExecuteReader();
            Student std = new Student();

            while (rd.Read())
            {
                std.id = Convert.ToInt32(rd.GetValue(0));
                std.name = rd.GetValue(1).ToString();
                std.email = rd.GetValue(2).ToString();  
            }
            return std;
        }

        //function add 1 record
        [Route("api/Student")]
        [HttpPost]
        public IHttpActionResult AddStudent (string name, string email)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("insertStudent",con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.Add(new SqlParameter("id", SqlDbType.Int, 50) {
                Direction = ParameterDirection.Output
            });
            cmd.ExecuteNonQuery();
            con.Close();
            int id;
            id = Convert.ToInt32(cmd.Parameters["id"].Value);
            return Ok(id);
        }

        //edit 1 record
        [Route("api/Student/{id}")]
        [HttpPost]
        public IHttpActionResult UpdateStudent (int id, string name, string email)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("updateStudent", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.ExecuteNonQuery();
            con.Close();
            return Ok();
        }

        //delete 1 record 
        [Route("api/Student/{id}")]
        [HttpDelete]
        public IHttpActionResult DeleteStudent(int id)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("deleteStudent", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
            con.Close();

            return Ok();
        }

        //Em dang mac o cho nay
        [Route("api/Student/{name}")]
        [HttpGet]
        public ArrayList GetByName(string name)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("searchName", con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@name", name);
            SqlDataReader rd = null;
            rd = cmd.ExecuteReader();
            ArrayList arrStu = new ArrayList();

            while (rd.Read())
            {
                Student std = new Student();
                std.id = Convert.ToInt32(rd.GetValue(0));
                std.name = rd.GetValue(1).ToString();
                std.email = rd.GetValue(2).ToString();
                arrStu.Add(std);
            }
            con.Close();
            return arrStu;

        }
    }
}
