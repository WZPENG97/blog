using MySql.Data.MySqlClient;
using PersonalDiary.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PersonalDiary.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            var blogs = new List<Blog>();

            if (Session["User"] != null)
            {
                var id = ((User)Session["User"]).Id;
                string connStr = ConfigurationManager.ConnectionStrings["MySqlConnection"].ToString();
                MySqlConnection conn = new MySqlConnection(connStr);
                conn.Open();
                string sqlstr = String.Format("select * from blog where UserId = '{0}';", id);
                MySqlCommand comm = new MySqlCommand(sqlstr, conn);
                MySqlDataReader reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    Blog blog = new Blog();
                    blog.Id = Convert.ToInt32(reader["Id"]);
                    blog.Title = reader["Title"].ToString();
                    blog.Content = reader["Content"].ToString();
                    blog.PubDate = Convert.ToDateTime(reader["PubDate"]);
                    blog.UserId = Convert.ToInt32(reader["UserId"]);
                    blog.UserName = Convert.ToString(reader["UserName"]);
                    blogs.Add(blog);
                }
            }
            return View(blogs);
        }

        public ActionResult Remove(int id)
        {
            var Id = id;
            string connStr = ConfigurationManager.ConnectionStrings["MySqlConnection"].ToString();
            MySqlConnection conn = new MySqlConnection(connStr);
            conn.Open();
            string sqlstr = String.Format("delete from blog where Id = '{0}';", Id);
            MySqlCommand comm = new MySqlCommand(sqlstr, conn);
            comm.ExecuteNonQuery();
            return RedirectToAction("Index", "User");
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Blog blog)
        {
            blog.PubDate = DateTime.Now;
            blog.UserName = ((User)Session["User"]).UserName;
            blog.UserId = ((User)Session["User"]).Id;

            string connStr = ConfigurationManager.ConnectionStrings["MySqlConnection"].ToString();
            MySqlConnection conn = new MySqlConnection(connStr);
            conn.Open();
            string sqlstr = String.Format("insert into blog (Title,Content,PubDate,UserId,UserName) values ('{0}','{1}','{2}','{3}','{4}')", blog.Title, blog.Content, blog.PubDate, blog.UserId, blog.UserName);
            MySqlCommand comm = new MySqlCommand(sqlstr, conn);
            if (comm.ExecuteNonQuery() != 0)
            {
                Response.Write("<script>alert('添加成功');window.location.href='Index'</script>");
            }
            return View();
        }

        public ActionResult Info(int id)
        {
            var userid = id;
            string connStr = ConfigurationManager.ConnectionStrings["MySqlConnection"].ToString();
            MySqlConnection conn = new MySqlConnection(connStr);
            conn.Open();
            string sqlstr = String.Format("select * from user where Id = '{0}';", userid);
            MySqlCommand comm = new MySqlCommand(sqlstr, conn);
            MySqlDataReader reader = comm.ExecuteReader();
            while (reader.Read())
            {
                User user = new User();
                user.UserName = Convert.ToString(reader["UserName"]);
                user.Major = reader["Major"].ToString();
                user.Introduce = reader["Introduce"].ToString();
                return View(user);
            }
            return View();
        }
    }
}