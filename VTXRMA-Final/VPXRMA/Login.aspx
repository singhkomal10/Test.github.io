<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="VPXRMA.Login" %>




<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  	<link rel="stylesheet" href="css/style.css"/>
  	<meta charset="utf-8"/>
    <title>ََV2SK</title>
    <link rel="stylesheet" href="style.css"/>
		<link href="loginpage/style.css" rel="stylesheet" />
 <%--   <link href="style.css" rel="stylesheet" />
    <link href="style.css" rel="stylesheet" />--%>
  </head>
<body>
    <form id="form1" class="box" runat="server">
     <h1>VTXRMA</h1>
  <%--<input type="text" name="" placeholder="Username"/>
  <input type="password" name="" placeholder="Password"/>
  <input type="submit" name="" value="Login">--%>
        <label id="lblmsg" runat="server"></label>
        <asp:TextBox ID="txt_email" runat="server" placeholder="Username"></asp:TextBox>
        <asp:TextBox ID="txt_password" runat="server" TextMode="Password" placeholder="Password"></asp:TextBox>
        <asp:Button ID="btn_signin" runat="server" Text="Login" OnClick="btn_signin_Click" />
    </form>
</body>
</html>


