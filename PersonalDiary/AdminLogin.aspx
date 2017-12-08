<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminLogin.aspx.cs" Inherits="PersonalDiary.AdminLogin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="Content/bootstrap.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div style="width:400px;margin:100px auto">
            <h3>管理员登录</h3>
            <asp:TextBox ID="TextBox1" runat="server" class="form-control" placeholder="用户名"></asp:TextBox>
            <br />
            <asp:TextBox ID="TextBox2" runat="server" class="form-control" placeholder="密码"></asp:TextBox>
            <br />
            <asp:Button ID="Button1" runat="server" Text="登录"  class="btn btn-primary" OnClick="Button1_Click1"/>
        </div>
    </form>
</body>
</html>
