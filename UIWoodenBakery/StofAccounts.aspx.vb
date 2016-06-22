Imports System
Imports DAL
Imports BL
Imports EN
Imports System.Globalization
Imports System.Data
Imports System.IO
Imports System.Web.UI
Imports Microsoft.Office.Interop
Imports Microsoft.Office.Interop.Excel

Public Class StofAccounts
    Inherits System.Web.UI.Page
    Dim ComVar As CommonVariables = New CommonVariables()
    Dim ObjEN As SalesEN = New SalesEN()
    Dim ObjBL As SalesBL = New SalesBL()
    Dim ObjDA As SalesDAL = New SalesDAL()
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack Then
                If Session("UserCode") Is Nothing Then
                    Response.Redirect("Login.aspx?sessionExpired=true", True)
                Else
                    lblcardCode.Text = Session("UserCode").ToString()
                    lblCardName.Text = Session("UserName").ToString()
                    txtpostDate.Text = Now.Date.ToString("dd/MM/yyyy")
                    txtdelDate.Text = Now.Date.ToString("dd/MM/yyyy")
                End If
            End If
        Catch ex As Exception
            DBConnectionDA.WriteError(ex.Message)
            ComVar.StrMsg = "alert('" & ex.Message & "')"
            ErrorMess(ComVar.StrMsg)
        End Try
    End Sub
    Private Sub ErrorMess(ByVal strmsg As String)
        ScriptManager.RegisterStartupScript(Update, Update.[GetType](), "strmsg", strmsg, True)
    End Sub

    Private Sub btngo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btngo.Click
        If Session("UserCode") Is Nothing Then
            Response.Redirect("Login.aspx?sessionExpired=true", True)
        Else
            AccountSummary()
        End If
    End Sub
    Private Sub AccountSummary()
        Try
            ObjEN.FrgName = txtpostDate.Text.Trim()
            ObjEN.oChoice = txtdelDate.Text.Trim()
            If ObjEN.FrgName = "" Then
                ComVar.StrMsg = "alert('Select From date...')"
                ErrorMess(ComVar.StrMsg)
            Else
                ObjEN.PostDate = ObjDA.GetDate(txtpostDate.Text.Trim()) ' Date.ParseExact(txtpostDate.Text.Trim().Replace("-", "/").Replace(".", "/"), "dd/MM/yyyy", CultureInfo.InvariantCulture)
            End If
            If ObjEN.oChoice = "" Then
                ComVar.StrMsg = "alert('Select To date...')"
                ErrorMess(ComVar.StrMsg)
            Else
                ObjEN.ShpDate = ObjDA.GetDate(txtdelDate.Text.Trim()) ' Date.ParseExact(txtdelDate.Text.Trim().Replace("-", "/").Replace(".", "/"), "dd/MM/yyyy", CultureInfo.InvariantCulture)
            End If
            If ObjEN.PostDate > ObjEN.ShpDate Then
                ComVar.StrMsg = "alert('Start date should be less than end date...')"
                ErrorMess(ComVar.StrMsg)
            Else
                ObjEN.CustCode = Session("UserCode").ToString()
                ComVar.SqlDS = ObjBL.GetAccBalancedate(ObjEN)
                If 1 = 1 Then
                    grdSalesItem.DataSource = ComVar.SqlDS
                    grdSalesItem.DataBind()
                    btnexpexcel.Visible = False
                Else
                    grdSalesItem.DataBind()
                    btnexpexcel.Visible = False
                End If
                TextBox3.Text = ObjBL.getOpeningBalance(ObjEN)
                TextBox4.Text = ObjBL.getEndingBalance(ObjEN)
            End If
        Catch ex As Exception
            DBConnectionDA.WriteError(ex.Message)
            ComVar.StrMsg = "alert('" & ex.Message & "')"
            ErrorMess(ComVar.StrMsg)
        End Try
    End Sub

    Private Sub grdSalesItem_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdSalesItem.PageIndexChanging
        grdSalesItem.PageIndex = e.NewPageIndex
        AccountSummary()
    End Sub
    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        ' Verifies that the control is rendered

    End Sub

    Private Sub btnexpexcel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnexpexcel.Click
        Try
            HttpContext.Current.Response.ClearContent()
            HttpContext.Current.Response.Buffer = True
            HttpContext.Current.Response.AddHeader("content-disposition", String.Format("attachment; filename={0}", "Customers.xls"))
            HttpContext.Current.Response.ContentType = "application/ms-excel"
            Dim sw As New StringWriter()
            Dim htw As New HtmlTextWriter(sw)
            grdSalesItem.AllowPaging = False
            AccountSummary()
            'Change the Header Row back to white color
            grdSalesItem.HeaderRow.Style.Add("background-color", "#FFFFFF")
            'Applying stlye to gridview header cells
            For i As Integer = 0 To grdSalesItem.HeaderRow.Cells.Count - 1
                grdSalesItem.HeaderRow.Cells(i).Style.Add("background-color", "#df5015")
            Next
            grdSalesItem.RenderControl(htw)
            HttpContext.Current.Response.Write(sw.ToString())
            HttpContext.Current.Response.[End]()
        Catch ex As Threading.ThreadAbortException
            DBConnectionDA.WriteError(ex.Message)
            ComVar.StrMsg = "alert('" & ex.Message & "')"
            ErrorMess(ComVar.StrMsg)
        End Try
        'Try
        '    Dim strTime, strFileName1 As String
        '    Dim filename As String
        '    Dim Excel As New Microsoft.Office.Interop.Excel.Application
        '    AccountSummary()
        '    With Excel
        '        .SheetsInNewWorkbook = 1
        '        .Workbooks.Add()
        '        .Worksheets(1).Select()

        '        Dim i As Integer = 1
        '        For col = 0 To ComVar.SqlDS.Tables(0).Columns.Count - 1
        '            .Cells(1, i).value = ComVar.SqlDS.Tables(0).Columns(col).ColumnName
        '            .Cells(1, i).EntireRow.Font.Bold = True
        '            i += 1
        '        Next

        '        i = 2

        '        Dim k As Integer = 1
        '        For col = 0 To ComVar.SqlDS.Tables(0).Columns.Count - 1
        '            i = 2
        '            For row = 0 To ComVar.SqlDS.Tables(0).Rows.Count - 1
        '                .Cells(i, k).Value = ComVar.SqlDS.Tables(0).Rows(row).ItemArray(col)
        '                i += 1
        '            Next
        '            k += 1
        '        Next
        '        strTime = Now.ToShortTimeString.Replace(":", "").Replace("AM", "").Replace("PM", "")
        '        strFileName1 = Now.Date.ToString("ddMMyyyy")
        '        strFileName1 = strFileName1 '& strTime

        '        filename = ConfigurationManager.AppSettings("ExcelPath") & Session("UserCode").ToString() & "_" & strFileName1 & ".xls"
        '        .ActiveCell.Worksheet.SaveAs(filename)
        '    End With
        '    ComVar.StrMsg = "alert('Successfully exported excel. File Location " & filename & "')"
        '    ErrorMess(ComVar.StrMsg)
        'Catch ex As Exception
        '    DBConnectionDA.WriteError(ex.Message)
        '    ComVar.StrMsg = "alert('" & ex.Message & "')"
        '    ErrorMess(ComVar.StrMsg)
        'End Try
    End Sub
  
End Class