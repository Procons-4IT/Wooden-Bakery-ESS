Imports System
Imports DAL
Imports BL
Imports EN
Public Class UIWB
    Inherits System.Web.UI.MasterPage
    Dim objDA As DBConnectionDA = New DBConnectionDA()
    Dim ObjEN As LoginEN = New LoginEN()
    Dim ObjBL As LoginBL = New LoginBL()
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If Session("UserCode") Is Nothing Then
                Response.Redirect("Login.aspx?sessionExpired=true", True)
            Else
                lbluser.Text = Session("UserName").ToString()
                ObjEN.UserCode = Session("UserCode").ToString()
                Dim strCode As String = ObjBL.GetCreditMemo(ObjEN)
                If strCode = "N" Then
                    MnuCredit.Visible = False
                Else
                    MnuCredit.Visible = True
                End If
                Dim strCode1 As String = ObjBL.GetStofAccount(ObjEN)
                If strCode1 = "N" Then
                    mnuStAcc.Visible = False
                Else
                    mnuStAcc.Visible = True
                End If
            End If
        End If
    End Sub

End Class