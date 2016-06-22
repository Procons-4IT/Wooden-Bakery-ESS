<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Login.aspx.vb" Inherits="UIWoodenBakery.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Login</title>
    <link rel="stylesheet" href="Styles/Login.css" type="text/css" />
    <script src='http://cdnjs.cloudflare.com/ajax/libs/jquery/2.1.3/jquery.min.js'></script>
      <link rel="shortcut icon" href="Images/favicon.ico" type="image/x-icon"/>
<link rel="icon" href="Images/favicon.ico" type="image/x-icon"/>
    <%-- <script type="text/javascript" language="javascript" >
        $("#BtnSubmit").click(function (event) {
            event.preventDefault();

            $('form').fadeOut(500);
            $('.wrapper').addClass('form-success');
        });
    </script>--%>
  <%--  <script type="text/javascript" language="javascript">
        function SetButtonStatus() {
            var txt = document.getElementById('TxtUid');
            var txt2 = document.getElementById('TxtPwd');
            if (txt.value.length >= 1 && txt2.value.length >= 1)
                document.getElementById('BtnSubmit').disabled = false;
            else
                document.getElementById('BtnSubmit').disabled = true;
        }
    </script>--%>
</head>
<body>

    <div class="wrapper">
        <div class="container" style="border: 1px solid white; border-radius: 10px 10px;">
            <h1>Welcome to Wooden Bakery</h1>

            <form id="Form1" class="form" runat="server">
                <asp:TextBox ID="TxtUid" runat="server" TabIndex="1" PlaceHolder="Username"  />
                <asp:TextBox ID="txtpwd" runat="server" TabIndex="2" TextMode="Password" PlaceHolder="Password"  />
                <asp:Button ID="BtnSubmit" runat="server" Enabled="true" Text="Login"
                    Font-Bold="True" TabIndex="3"  />

            </form>
        </div>

        <ul class="bg-bubbles">
            <li></li>
            <li></li>
            <li></li>
            <li></li>
            <li></li>
            <li></li>
            <li></li>
            <li></li>
            <li></li>
            <li></li>
        </ul>
    </div>



</body>
</html>
