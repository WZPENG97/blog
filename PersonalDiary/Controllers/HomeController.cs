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
           var diaries = new List<Diary>();
            
            if (Session["User"] != null)
            {
                var userName = ((User)Session["User"]).UserName;
                string connStr = ConfigurationManager.ConnectionStrings["MySqlConnection"].ToString();
                MySqlConnection conn = new MySqlConnection(connStr);
               
                    conn.Open();
                    string sqlstr = String.Format("select * from diary where User = '{0}';", userName);
                    MySqlCommand comm = new MySqlCommand(sqlstr, conn);
                    MySqlDataReader reader = comm.ExecuteReader();
                    while (reader.Read()) {
                    Diary diary = new Diary();
                    diary.Id = Convert.ToInt32(reader["Id"]);
                        diary.Title = reader["Title"].ToString();
                        diary.Content = reader["Content"].ToString();
                        diary.PubDate = Convert.ToDateTime(reader["PubDate"]);
                        diary.UserId = Convert.ToInt32(reader["UserId"]);
                        diary.User = Convert.ToString(reader["User"]);
                        diaries.Add(diary);
                    }
            }
            return View(diaries);
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

        public ActionResult Remove(int id)
        {
            var Id = id;
            string connStr = ConfigurationManager.ConnectionStrings["MySqlConnection"].ToString();
            MySqlConnection conn = new MySqlConnection(connStr);
            conn.Open();
            string sqlstr = String.Format("delete from diary where Id = '{0}';", Id);
            MySqlCommand comm = new MySqlCommand(sqlstr, conn);
            if (comm.ExecuteNonQuery() != 0) {
                Response.Write("<script>alert('删除成功')</script>");
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Create()
        {
            return View();
        }
    }
}