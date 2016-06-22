Imports System
Imports DAL
Imports BL
Imports EN
Public Class Login
    Inherits System.Web.UI.Page
    Dim ComVar As CommonVariables = New CommonVariables()
    Dim objDA As DBConnectionDA = New DBConnectionDA()
    Dim ObjEN As LoginEN = New LoginEN()
    Dim ObjBL As LoginBL = New LoginBL()
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack Then
                If Request.QueryString("SessionExpired") <> Nothing Or Request.QueryString("SessionExpired") = "ture" Then
                    Session.Clear()
                    Dim strmsg As String = "Session expired. You will be redirected to Login page"
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "js", "<script>alert('" & strmsg & "')</script>")
                End If
            End If
        Catch ex As Exception
            ErrorMess(ex.Message)
        End Try
    End Sub
    Private Sub ErrorMess(ByVal strmsg As String)
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "js", "<script>alert('" & strmsg & "')</script>")
    End Sub

    Private Sub BtnSubmit_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnSubmit.Click
        Try
            Session.Clear()
            'objDA.checktable()
            DBName = ConfigurationManager.AppSettings("CompanyDB").ToString() '& ".dbo"
            ObjEN.UserCode = TxtUid.Text.Trim()
            ObjEN.Password = txtpwd.Text.Trim()
            If ObjEN.UserCode = "" Then
                ErrorMess("Enter the UserName")
            ElseIf ObjEN.Password = "" Then
                ErrorMess("Enter the Password")
            Else
                ObjEN.CustomerCode = ObjBL.UserAuthentication(ObjEN)
                If ObjEN.CustomerCode <> "" Then
                    Session("UserCode") = ObjEN.CustomerCode
                    Session("UserName") = ObjBL.GetCustName(ObjEN)
                    Session("SessionId") = ObjBL.SessionDetails(Session("UserCode").ToString())
                    Session("SAPCompany") = Application("DBName")
                    Response.Redirect("Home.aspx", False)
                Else
                    ErrorMess("login failed. UserName and Password does not matching or you are not valid customer")
                End If
            End If
        Catch ex As Exception
            ErrorMess(ex.Message)
        End Try
    End Sub
End Class