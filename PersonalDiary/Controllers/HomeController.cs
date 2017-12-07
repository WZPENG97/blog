using MySql.Data.MySqlClient;
using PersonalDiary.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace PersonalDiary.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index() {
            var blogs = new List<Blog>();
            string connStr = ConfigurationManager.ConnectionStrings["MySqlConnection"].ToString();
            MySqlConnection conn = new MySqlConnection(connStr);
            conn.Open();
            string sqlstr = "select * from blog";
            MySqlCommand comm = new MySqlCommand(sqlstr, conn);
            MySqlDataReader reader = comm.ExecuteReader();
            while (reader.Read()) {
                Blog blog = new Blog();
                blog.Id = Convert.ToInt32(reader["Id"]);
                blog.Title = reader["Title"].ToString();
                blog.Content = reader["Content"].ToString();
                blog.PubDate = Convert.ToDateTime(reader["PubDate"]);
                blog.UserId = Convert.ToInt32(reader["UserId"]);
                blog.UserName = Convert.ToString(reader["UserName"]);
                blogs.Add(blog);
            }
            return View(blogs);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        

        public ActionResult Show(int id)
        {
            var Id = id;
            string connStr = ConfigurationManager.ConnectionStrings["MySqlConnection"].ToString();
            MySqlConnection conn = new MySqlConnection(connStr);
            conn.Open();
            string sqlstr = String.Format("select * from blog where Id = '{0}';", Id);
            MySqlCommand comm = new MySqlCommand(sqlstr, conn);
            MySqlDataReader reader = comm.ExecuteReader();
            var blog = new Blog();
            while (reader.Read())
            {
                blog.Id = Convert.ToInt32(reader["Id"]);
                blog.Title = reader["Title"].ToString();
                blog.Content = reader["Content"].ToString(); ;
                blog.PubDate = Convert.ToDateTime(reader["PubDate"]);
                blog.UserId = Convert.ToInt32(reader["UserId"]);
                blog.UserName = Convert.ToString(reader["UserName"]);
            }
            return View(blog);
        }

        
    }
}