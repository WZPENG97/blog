using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PersonalDiary
{
    public partial class Manage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string connStr = ConfigurationManager.ConnectionStrings["MySqlConnection"].ToString();
            MySqlConnection conn = new MySqlConnection(connStr);
            conn.Open();
            string userstr = "select * from user";
            MySqlCommand cmd = new MySqlCommand(userstr,conn);
            MySqlDataAdapter ada = new MySqlDataAdapter();
            ada.SelectCommand = cmd;
            DataTable dt = new DataTable();
            ada.Fill(dt);
            GridView1.DataBind();
        }

    }
}