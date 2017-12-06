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
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Logoff()
        {
            Session.Clear();
            return RedirectToAction("Login", "Account");
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User user)
        {
            string username = user.UserName;
            string password = user.PassWord;
            string connStr = ConfigurationManager.ConnectionStrings["MySqlConnection"].ToString();
            MySqlConnection conn = new MySqlConnection(connStr);
            conn.Open();
            string sqlstr = String.Format("select * from user where UserName = '{0}' and PassWord = '{1}';", username, password);
            MySqlDataAdapter mda = new MySqlDataAdapter(sqlstr, conn);
            DataSet ds = new DataSet();
            mda.Fill(ds);
            if (ds.Tables[0].Rows.Count == 0)
            {
                Response.Write("<script>alert('帐号密码错误')</script>");
                return View();
            }else {
                user.Id = Convert.ToInt32(ds.Tables[0].Rows[0]["Id"].ToString());
                Session["User"] = user;
                return RedirectToAction("Index", "Home");
            }
        }


        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(User user) {
            string username = user.UserName;
            string password = user.PassWord;
            string connStr = ConfigurationManager.ConnectionStrings["MySqlConnection"].ToString();
            //string connStr = "Integrated Security=False;server=localhost;user id=root;password=root;database=diary; pooling=true;";
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                conn.Open();
                string sqlstr = String.Format("select * from user where UserName = '{0}';", username);
                MySqlDataAdapter mda = new MySqlDataAdapter(sqlstr, conn);
                DataSet ds = new DataSet();
                mda.Fill(ds);
                if (ds.Tables[0].Rows.Count != 0)
                {
                    Response.Write("<script>alert('该用户已存在')</script>");
                }
                else
                {
                    string insertStr = String.Format("insert into user (UserName,Password) values ('{0}','{1}');", username,password);
                    MySqlCommand comm = new MySqlCommand(insertStr, conn);
                    if (comm.ExecuteNonQuery() != 0)
                    {
                        Response.Write("<script>alert('注册成功')</script>");
                        return RedirectToAction("Login", "Account");
                    }
                    else {
                        Response.Write("<script>alert('注册失败，请重试')</script>");
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')</script>");
            }
            finally
            {
                conn.Close();
            }
            return View();
        }
    }
}