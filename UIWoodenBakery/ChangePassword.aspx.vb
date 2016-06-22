Imports System
Imports BL
Imports DAL
Imports EN
Public Class ChangePassword
    Inherits System.Web.UI.Page
    Dim objEN As ChangePwdEN = New ChangePwdEN()
    Dim objDA As DBConnectionDA = New DBConnectionDA()
    Dim strmsg As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If Session("UserCode") Is Nothing Then
                Response.Redirect("Login.aspx?sessionExpired=true", True)
            End If
        End If
    End Sub
    Private Sub mess(ByVal str As String)
        ScriptManager.RegisterStartupScript(Update, Update.[GetType](), "strmsg", strmsg, True)
    End Sub
    Protected Sub btnsave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsave.Click
        If Session("UserCode") Is Nothing Then
            Response.Redirect("Login.aspx?sessionExpired=true", True)
        Else
            objEN.CustCode = Session("UserCode").ToString()
            objen.OldPwd = txtoldpwd.Text.Trim()
            objen.NewPwd = txtnewpwd.Text.Trim()
            objen.ConfirmPwd = txtconfirmpwd.Text.Trim()

            If objen.OldPwd = "" Then
                strmsg = "alert('Enter the old Password..')"
                mess(strmsg)
            End If
            If objen.NewPwd = "" Then
                strmsg = "alert('Enter the New Password..')"
                mess(strmsg)
            End If
            If objen.ConfirmPwd = "" Then
                strmsg = "alert('Enter the Confirm Password..')"
                mess(strmsg)
            End If
            If objDA.Checkpassword(objEN) = False Then
                strmsg = "alert('Old password does not match..')"
                mess(strmsg)
            Else
                objDA.UpdatePassword(objEN)
                strmsg = "alert('Password changed Successfully ..')"
                mess(strmsg)
            End If
        End If
    End Sub

    Private Sub btnclose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Response.Redirect("Home.aspx", False)
    End Sub
End Class