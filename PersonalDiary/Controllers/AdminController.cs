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
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            if (Session["Admin"] != null)
            {
                var users = new List<User>();
                string connStr = ConfigurationManager.ConnectionStrings["MySqlConnection"].ToString();
                MySqlConnection conn = new MySqlConnection(connStr);
                conn.Open();
                string sqlstr = "select * from user";
                MySqlCommand comm = new MySqlCommand(sqlstr, conn);
                MySqlDataReader reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    User user = new User();
                    user.Id = Convert.ToInt32(reader["Id"]);
                    user.UserName = Convert.ToString(reader["UserName"]);
                    user.PassWord = Convert.ToString(reader["PassWord"]);
                    users.Add(user);
                }
                return View(users);
            }
            return RedirectToAction("Login", "Admin");
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Admin user)
        {
            string username = user.UserName;
            string password = user.PassWord;
            string connStr = ConfigurationManager.ConnectionStrings["MySqlConnection"].ToString();
            MySqlConnection conn = new MySqlConnection(connStr);
            conn.Open();
            string sqlstr = String.Format("select * from admin where UserName = '{0}' and PassWord = '{1}';", username, password);
            MySqlDataAdapter mda = new MySqlDataAdapter(sqlstr, conn);
            DataSet ds = new DataSet();
            mda.Fill(ds);
            if (ds.Tables[0].Rows.Count == 0)
            {
                Response.Write("<script>alert('帐号密码错误')</script>");
                return View();
            }
            else
            {
                user.Id = Convert.ToInt32(ds.Tables[0].Rows[0]["Id"].ToString());
                Session["Admin"] = user;
                return RedirectToAction("Index", "Admin");
            }
        }

        public ActionResult Remove(int id)
        {
            var Id = id;
            string connStr = ConfigurationManager.ConnectionStrings["MySqlConnection"].ToString();
            MySqlConnection conn = new MySqlConnection(connStr);
            conn.Open();
            string sqlstr = String.Format("delete from user where Id = '{0}';", Id);
            MySqlCommand comm = new MySqlCommand(sqlstr, conn);
            comm.ExecuteNonQuery();
            return RedirectToAction("Index", "Admin");
        }
    }
}