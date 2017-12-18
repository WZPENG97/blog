using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Data;
using System.Configuration;

namespace PersonalDiary
{
    public partial class EditInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)    //一定要确保是首次加载
            {
                string id = Request.QueryString["id"];
                string connStr = ConfigurationManager.ConnectionStrings["MySqlConnection"].ToString();
                MySqlConnection conn = new MySqlConnection(connStr);
                conn.Open();
                string str = String.Format("select Id,UserName as 用户名,PassWord as '密码',Major as '擅长领域',Introduce as '个人简介' from  user where Id = {0}",id);
                MySqlDataAdapter sda = new MySqlDataAdapter(str, conn);
                sda = new MySqlDataAdapter(str, conn);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                GridView1.DataSource = dt;
                GridView1.DataBind();
                conn.Close();
            }

        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            string id = Request.QueryString["id"];
            GridView1.EditIndex = e.NewEditIndex;
            string connStr = ConfigurationManager.ConnectionStrings["MySqlConnection"].ToString();
            MySqlConnection conn = new MySqlConnection(connStr);
            conn.Open();
            string str = String.Format("select Id,UserName as '用户名',PassWord as '密码',Major as '擅长领域',Introduce as '个人简介' from  user where Id = {0}", id);
            MySqlDataAdapter sda = new MySqlDataAdapter(str, conn);
            sda = new MySqlDataAdapter(str, conn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            GridView1.DataSource = dt;
            GridView1.DataBind();
            conn.Close();

        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            string connStr = ConfigurationManager.ConnectionStrings["MySqlConnection"].ToString();
            MySqlConnection conn = new MySqlConnection(connStr);
            conn.Open();
            string id = Request.QueryString["id"];
            string str = String.Format("select Id,UserName as '用户名', PassWord as '密码', Major as '擅长领域', Introduce as '个人简介' from user where Id = {0}", id);
            MySqlDataAdapter sda = new MySqlDataAdapter(str, conn);
            sda = new MySqlDataAdapter(str, conn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            GridView1.DataSource = dt;
            GridView1.DataBind();
            conn.Close();


        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            this.GridView1.EditIndex = e.RowIndex;
            string id = GridView1.DataKeys[e.RowIndex].Value.ToString();
            string username = ((TextBox)(GridView1.Rows[e.RowIndex].Cells[2].Controls[0])).Text;
            string password = ((TextBox)(GridView1.Rows[e.RowIndex].Cells[3].Controls[0])).Text;
            string major = ((TextBox)(GridView1.Rows[e.RowIndex].Cells[4].Controls[0])).Text;
            string introduce = ((TextBox)(GridView1.Rows[e.RowIndex].Cells[5].Controls[0])).Text;
            string str = string.Format("update user set UserName='{0}',PassWord='{1}',Major='{2}',Introduce='{3}' where Id='{4}'", username, password, major, introduce,id);
            string connStr = ConfigurationManager.ConnectionStrings["MySqlConnection"].ToString();
            MySqlConnection conn = new MySqlConnection(connStr);
            conn.Open();
            MySqlCommand cmd = new MySqlCommand(str,conn);
            cmd.ExecuteNonQuery();
            GridView1.EditIndex = -1;

            string sqlstr = String.Format("select Id,UserName as '用户名',PassWord as '密码',Major as '擅长领域',Introduce as '个人简介' from  user where Id = {0}", id);
            MySqlDataAdapter sda = new MySqlDataAdapter(sqlstr, conn);
            sda = new MySqlDataAdapter(sqlstr, conn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            GridView1.DataSource = dt;
            GridView1.DataBind();
            conn.Close();
            Response.Write(@"<script language='javascript'>alert('更新成功');</script>");

        }
    }
}