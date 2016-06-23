Imports System
Imports DAL
Imports BL
Imports EN
Imports System.Globalization
Imports System.Drawing

Public Class SalesOrder
    Inherits System.Web.UI.Page
    Dim ComVar As CommonVariables = New CommonVariables()
    Dim ObjEN As SalesEN = New SalesEN()
    Dim ObjBL As SalesBL = New SalesBL()
    Dim ObjDA As SalesDAL = New SalesDAL()
    Dim blnFlag, StrCode As String
    Dim dtPost, dtDel, dtDoc As Date
    Dim Postdate, Deldate As String
    Dim grdTotal As Decimal = 0
    Dim grdTotal1 As Decimal = 0
    Dim grdLocTotal As Decimal = 0
    Dim grdLocTotal1 As Decimal = 0
    Dim grdTotal2 As Decimal = 0
    Dim grdLocTotal2 As Decimal = 0
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack Then
                If Session("UserCode") Is Nothing Then
                    Response.Redirect("Login.aspx?sessionExpired=true", True)
                Else
                    panelview.Visible = True
                    PanelNewRequest.Visible = False
                    lblcardCode.Text = Session("UserCode").ToString()
                    lblCardName.Text = Session("UserName").ToString()
                    ObjDA.GridviewBind("SELECT T1.""ItemCode"", T1.""ItemName"" FROM " & DBName & ".OSCN T0  INNER JOIN " & DBName & ".OITM T1 ON T0.""ItemCode"" = T1.""ItemCode"" WHERE T0.""CardCode"" ='" & Session("UserCode").ToString() & "' and T1.""SellItem""='Y'", grdItems)
                    ObjDA.BindDropdown("select ""CntctCode"",""Name""  from " & DBName & ".OCPR where ""CardCode""='" & Session("UserCode").ToString() & "' order by ""CntctCode""", "CntctCode", "Name", ddlconPerson)
                    ObjDA.GridviewBind("SELECT ""DocEntry"",CASE ""DocStatus"" when 'C' then 'Closed' else 'Open' end as ""DocStatus"",Cast(""DocDueDate"" as varchar(11)) as ""DocDueDate"" FROM " & DBName & ".ORDR  WHERE ""CardCode"" ='" & Session("UserCode").ToString() & "' order by ""DocEntry"" desc", GridView1)
                    ObjDA.BindDropdown("select T1.""FldValue"",T1.""Descr""  from " & DBName & ".CUFD T0 inner join " & DBName & ".UFD1 T1 on T1.""FieldID""=T0.""FieldID"" and T1.""TableID""=T0.""TableID""   where T0.""TableID""='ORDR' and T0.""AliasID""='Item_Cat'", "FldValue", "Descr", ddlItemCat)
                    ObjEN.CustCode = Session("UserCode").ToString()
                    ObjDA.DeleteTemp(ObjEN)
                    MainGVBind(ObjEN)
                    '   Me.Form.DefaultButton = btnSubmit.UniqueID
                End If
            End If

        Catch ex As Exception
            DBConnectionDA.WriteError(ex.Message)
            ComVar.StrMsg = "alert('" & ex.Message & "')"
            ErrorMess(ComVar.StrMsg)
        End Try
    End Sub
    Private Sub MainGVBind(ByVal objen As SalesEN)
        Try
            ComVar.SqlDS = ObjBL.MainGVBind(objen)
            If ComVar.SqlDS.Tables(0).Rows.Count > 0 Then
                grdSalesDraft.DataSource = ComVar.SqlDS.Tables(0)
                grdSalesDraft.DataBind()
            Else
                grdSalesDraft.DataBind()
            End If
            ComVar.SqlDS = ObjBL.MainGVBind1(objen)
            If ComVar.SqlDS.Tables(0).Rows.Count > 0 Then
                grdSales.DataSource = ComVar.SqlDS.Tables(0)
                grdSales.DataBind()
            Else
                grdSales.DataBind()
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
    Protected Sub Btncallpop_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Btncallpop.ServerClick
        Dim str1, str2, str3, strDocEntry As String
        str1 = txtpopunique.Text.Trim()
        str2 = txtpoptno.Text.Trim()
        str3 = txttname.Text.Trim()
        If txthidoption.Text = "Items" Then
            If txtpoptno.Text.Trim() <> "" Then
                txtItemCode.Text = txtpopunique.Text.Trim()
                txtItemName.Text = txtpoptno.Text.Trim()
                ObjDA.BindDropdown("SELECT  T2.""UomEntry"" as ""UomEntry"", T3.""UomCode"" as ""UomCode"" FROM " & DBName & ".OITM T0 INNER JOIN " & DBName & ".OUGP T1 ON T0.""UgpEntry"" = T1.""UgpEntry"" INNER JOIN " & DBName & ".UGP1 T2 ON T1.""UgpEntry"" = T2.""UgpEntry"" INNER JOIN " & DBName & ".OUOM T3 ON T3.""UomEntry"" = T2.""UomEntry"" WHERE T0.""ItemCode""='" & txtItemCode.Text.Trim().Replace("'", "") & "'", "UomEntry", "UomCode", ddlUoM)
                ObjEN.ItemCode = txtItemCode.Text.Trim().Replace("'", "")
                lblSalesUom.Text = ObjBL.GetUomName(ObjEN)
                If ddlUoM.SelectedItem.Text <> "Manual" And lblSalesUom.Text <> "" Then
                    ' ddlUoM.SelectedItem.Text = lblSalesUom.Text.Trim()
                    ddlUoM.SelectedValue = ddlUoM.Items.FindByText(lblSalesUom.Text.Trim()).Value
                End If
                txtPrice.Text = ObjBL.GetPrice(Session("PriceList").ToString(), txtItemCode.Text.Trim(), ddlUoM.SelectedItem.Value)
                txtQty.Focus()
                ' Me.Form.DefaultButton = btnSubmit.UniqueID
            End If
        ElseIf txthidoption.Text = "Sales" Then
            If txtpoptno.Text.Trim() <> "" Then
                ObjEN.CustCode = Session("UserCode").ToString()
                ObjDA.DeleteTemp(ObjEN)
                strDocEntry = txtpopunique.Text.Trim()
                DuplicateSalesOrder(strDocEntry)
            End If
        End If
        ObjDA.GridviewBind("SELECT T1.""ItemCode"", T1.""ItemName"" FROM " & DBName & ".OSCN T0  INNER JOIN " & DBName & ".OITM T1 ON T0.""ItemCode"" = T1.""ItemCode"" WHERE T0.""CardCode"" ='" & Session("UserCode").ToString() & "' and T1.""SellItem""='Y'", grdItems)
    End Sub
    Private Sub btnnew_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs) Handles btnnew.Click
        btnImport.Visible = True
        panelview.Visible = False
        PanelNewRequest.Visible = True
        btnSubmit.Visible = True
        btnDuplicate.Visible = True
        btnImport.Visible = True
        txtpostDate.Text = Now.Date.ToString("dd/MM/yyyy")
        txtdocdate.Text = Now.Date.ToString("dd/MM/yyyy")
        txtdelDate.Text = Now.Date.ToString("dd/MM/yyyy")
        Clear()
        ObjEN.CustCode = Session("UserCode").ToString()
        ObjDA.DeleteTemp(ObjEN)
        grdSalesItem.DataBind()
        Postdate = txtpostDate.Text.Trim().Replace("-", "/").Replace(".", "/")
        Deldate = txtdelDate.Text.Trim().Replace("-", "/").Replace(".", "/")
        If Postdate <> "" And Deldate <> "" Then
            ObjEN.PostDate = ObjDA.GetDate(Postdate)
            ObjEN.DelDate = ObjDA.GetDate(Deldate)
            lblNodays.Text = ObjBL.getNodays(ObjEN.PostDate, ObjEN.DelDate)
        End If
    End Sub

    Private Sub grdItems_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles grdItems.RowDataBound
        txtpoptno.Text = ""
        txtpopunique.Text = ""
        txthidoption.Text = ""
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onclick", "popupdisplay('Items','" + (DataBinder.Eval(e.Row.DataItem, "ItemCode")).ToString().Trim() + "','" + (DataBinder.Eval(e.Row.DataItem, "ItemName")).ToString().Trim() + "');")
        End If
    End Sub

    Private Sub btnSubmit_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSubmit.Click
        Try
            If txtItemCode.Text = "" Then
                ComVar.StrMsg = "alert('Enter the itemcode...')"
                ErrorMess(ComVar.StrMsg)
            ElseIf txtQty.Text = "" Then
                ComVar.StrMsg = "alert('Enter the quantity...')"
                ErrorMess(ComVar.StrMsg)
            ElseIf txtpostDate.Text = "" Then
                ComVar.StrMsg = "alert('Enter the Posting date...')"
                ErrorMess(ComVar.StrMsg)
            ElseIf txtdelDate.Text = "" Then
                ComVar.StrMsg = "alert('Enter the Delivery date...')"
                ErrorMess(ComVar.StrMsg)
            Else
                ObjEN.PostDate = ObjDA.GetDate(txtpostDate.Text.Trim()) ' Date.ParseExact(txtpostDate.Text.Trim().Replace("-", "/").Replace(".", "/"), "dd/MM/yyyy", CultureInfo.InvariantCulture)
                ObjEN.DelDate = ObjDA.GetDate(txtdelDate.Text.Trim()) ' Date.ParseExact(txtdelDate.Text.Trim().Replace("-", "/").Replace(".", "/"), "dd/MM/yyyy", CultureInfo.InvariantCulture)
                ObjEN.HeadDays = lblNodays.Text.Trim() 'ObjBL.getNodays(ObjEN.PostDate, ObjEN.DelDate)
                ObjEN.SAPCompany = Session("SAPCompany")
                ObjEN.CustCode = Session("UserCode").ToString()
                ObjEN.ItemCode = txtItemCode.Text.Trim().Replace("'", "")
                ObjEN.ItemName = txtItemName.Text.Trim().Replace("'", "")
                ObjEN.Quantity = CDbl(txtQty.Text.Trim())
                ObjEN.SessionId = Session("SessionId").ToString()
                ObjEN.ShpDate = ObjDA.GetDeliveryDate(ObjEN)
                ObjEN.LineDays = ObjBL.getNodays(ObjEN.PostDate, ObjEN.ShpDate)
                ObjEN.FrgName = ObjBL.GetFrgName(ObjEN)
                ObjEN.UomName = ddlUoM.SelectedItem.Text
                ObjEN.Price = CDbl(txtPrice.Text.Trim())
                If ObjEN.UomName <> "Manual" Then
                    ObjEN.UomCode = ObjBL.GetUomEntry(ObjEN)
                Else
                    ObjEN.UomCode = ddlUoM.SelectedItem.Value
                End If
                blnFlag = ObjBL.InsertTemp(ObjEN)
                If blnFlag = "Success" Then
                    ComVar.StrQuery = "Select ""U_DocEntry"",""U_ItemCode"",""U_ItemName"",CAST(""U_Quantity"" AS int) AS ""U_Quantity"",""U_frgName"",""U_UoMName"",""U_Price"",""U_LineNodays"",Cast(""U_ShpDate"" as varchar(11)) as ""U_ShpDate"" from " & DBName & ".""U_ORDR"" where ""U_SessionId""='" & Session("SessionId").ToString() & "' order by ""U_DocEntry"""
                    ObjDA.GridviewBind(ComVar.StrQuery.ToUpper(), grdSalesItem)
                Else
                    ComVar.StrMsg = "alert('" & blnFlag & "')"
                    ErrorMess(ComVar.StrMsg)
                End If
                Clear()

            End If

        Catch ex As Exception
            DBConnectionDA.WriteError(ex.Message)
            ComVar.StrMsg = "alert('" & ex.Message & "')"
            ErrorMess(ComVar.StrMsg)
        End Try
    End Sub
    Private Sub Clear()
        txtItemCode.Text = ""
        txtItemName.Text = ""
        txtQty.Text = ""
    End Sub

    Private Sub btnAdd_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAdd.Click
        Try
            If Session("UserCode") Is Nothing Or Session("SAPCompany") Is Nothing Then
                Response.Redirect("Login.aspx?sessionExpired=true", True)
            Else
                If txtpostDate.Text = "" Then
                    ComVar.StrMsg = "alert('Enter the posting date...')"
                    ErrorMess(ComVar.StrMsg)
                ElseIf txtdelDate.Text = "" Then
                    ComVar.StrMsg = "alert('Enter the delivery date...')"
                    ErrorMess(ComVar.StrMsg)
                ElseIf txtdocdate.Text = "" Then
                    ComVar.StrMsg = "alert('Enter the Document Date...')"
                    ErrorMess(ComVar.StrMsg)
                ElseIf Session("SAPCompany") Is Nothing Then
                    ComVar.StrMsg = "alert('SAP Company not connected...')"
                    ErrorMess(ComVar.StrMsg)
                Else
                    blnFlag = SalesDraft()
                    If blnFlag = "Success" Then
                        ObjEN.CustCode = Session("UserCode").ToString()
                        MainGVBind(ObjEN)
                        panelview.Visible = True
                        PanelNewRequest.Visible = False
                    Else
                        panelview.Visible = False
                        PanelNewRequest.Visible = True
                    End If
                End If
            End If
        Catch ex As Exception
            DBConnectionDA.WriteError(ex.Message)
            ComVar.StrMsg = "alert('" & ex.Message & "')"
            ErrorMess(ComVar.StrMsg)
        End Try
    End Sub
    Private Function SalesDraft() As String
        Dim strDocNum As String = ""
        Dim Driver As String = ""
        Dim BPRoute As String = ""
        Dim BPSubCat As String = ""
        Dim strqty, strcode, StUom As String
        Dim oRec, oTemp, ORecSet As SAPbobsCOM.Recordset
        Dim blnRecordAdded As Boolean = False
        Dim row As GridViewRow
        Try
            dtPost = ObjDA.GetDate(txtpostDate.Text.Trim()) ' Date.ParseExact(txtpostDate.Text.Trim().Replace("-", "/").Replace(".", "/"), "dd/MM/yyyy", CultureInfo.InvariantCulture)
            dtDel = ObjDA.GetDate(txtdelDate.Text.Trim()) ' Date.ParseExact(txtdelDate.Text.Trim().Replace("-", "/").Replace(".", "/"), "dd/MM/yyyy", CultureInfo.InvariantCulture)
            dtDoc = ObjDA.GetDate(txtdocdate.Text.Trim()) ' Date.ParseExact(txtdocdate.Text.Trim().Replace("-", "/").Replace(".", "/"), "dd/MM/yyyy", CultureInfo.InvariantCulture)
            ObjEN.SAPCompany = Session("SAPCompany")
            Dim oDrfDoc As SAPbobsCOM.Documents
            oRec = ObjEN.SAPCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
            oTemp = ObjEN.SAPCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
            ORecSet = ObjEN.SAPCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
            oDrfDoc = ObjEN.SAPCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oDrafts)

            For Each row In grdSalesItem.Rows
                Dim ddluom As DropDownList = DirectCast(row.FindControl("ddlgUoM"), DropDownList)
                strqty = CType(row.FindControl("lblQty"), TextBox).Text
                If strqty = "" Then
                    strqty = 0
                End If
                strcode = CType(row.FindControl("lblDocNo"), Label).Text
                ComVar.StrQuery = "Update " & DBName & ".""U_ORDR"" set ""U_Quantity""='" & strqty & "',""U_UomCode""='" & ddluom.SelectedValue & "',""U_UoMName""='" & ddluom.SelectedItem.Text & "' where ""U_CardCode""='" & lblcardCode.Text.Trim() & "' and ""U_SessionId""='" & Session("SessionId").ToString() & "' and U_DocEntry='" & strcode & "' "
                oRec.DoQuery(ComVar.StrQuery.ToUpper())
            Next

            oDrfDoc.CardCode = lblcardCode.Text.Trim()
            oDrfDoc.CardName = lblCardName.Text.Trim().Replace("'", "")
            oDrfDoc.DocObjectCode = SAPbobsCOM.BoObjectTypes.oOrders
            oDrfDoc.DocDate = dtPost
            oDrfDoc.DocDueDate = dtDel
            oDrfDoc.TaxDate = dtDoc
            oDrfDoc.UserFields.Fields.Item("U_Z_Source").Value = "W"
            oDrfDoc.UserFields.Fields.Item("U_Z_Noofdays").Value = ObjDA.getNodays(dtPost, dtDel) ' lblNodays.Text.Trim()
            Try
                oDrfDoc.UserFields.Fields.Item("U_Item_Cat").Value = ddlItemCat.SelectedValue
            Catch ex As Exception
            End Try
            ComVar.StrQuery = "SELECT * FROM " & DBName & ".OCRD T0 WHERE T0.""CardCode""='" & lblcardCode.Text.Trim() & "'"
            ORecSet.DoQuery(ComVar.StrQuery)
            If ORecSet.RecordCount > 0 Then
                Try
                    Driver = ORecSet.Fields.Item("U_Driver").Value
                Catch ex As Exception
                End Try
                Try
                    BPSubCat = ORecSet.Fields.Item("U_SubSubCategory").Value
                Catch ex As Exception
                End Try
                Try
                    BPRoute = ORecSet.Fields.Item("U_Cust_Route").Value
                Catch ex As Exception
                End Try
            End If

            ComVar.StrQuery = "Select ""U_ItemCode"",""U_Quantity"",""U_UoMName"",""U_ShpDate"",IFNULL(""U_UomCode"",''),""U_Price"",""U_LineNodays"" from " & DBName & ".""U_ORDR"" where ""U_CardCode""='" & lblcardCode.Text.Trim() & "' and ""U_SessionId""='" & Session("SessionId").ToString() & "' and ""U_Quantity"" > 0"
            oRec.DoQuery(ComVar.StrQuery.ToUpper())
            If oRec.RecordCount > 0 Then
                For introw As Integer = 0 To oRec.RecordCount - 1
                    If introw > 0 Then
                        oDrfDoc.Lines.Add()
                        oDrfDoc.Lines.SetCurrentLine(introw)
                    End If
                    oDrfDoc.Lines.ItemCode = oRec.Fields.Item(0).Value
                    oDrfDoc.Lines.ShipDate = oRec.Fields.Item(3).Value
                    oDrfDoc.Lines.Quantity = oRec.Fields.Item(1).Value
                    oDrfDoc.Lines.UnitPrice = oRec.Fields.Item(5).Value
                    oDrfDoc.Lines.UserFields.Fields.Item("U_Z_Noofdays").Value = oRec.Fields.Item(6).Value
                    If oRec.Fields.Item(4).Value <> "" Then
                        oDrfDoc.Lines.UoMEntry = oRec.Fields.Item(4).Value
                    End If

                    Try
                        oDrfDoc.Lines.UserFields.Fields.Item("U_Cust_Driver").Value = Driver
                    Catch ex As Exception
                    End Try
                    Try
                        oDrfDoc.Lines.UserFields.Fields.Item("U_Cust_Route").Value = BPRoute
                    Catch ex As Exception
                    End Try
                    Try
                        oDrfDoc.Lines.CostingCode2 = BPSubCat
                    Catch ex As Exception
                    End Try
                    Try
                        oDrfDoc.Lines.UserFields.Fields.Item("U_Item_Cat").Value = ddlItemCat.SelectedValue
                    Catch ex As Exception
                    End Try

                    ComVar.StrQuery = "SELECT * FROM " & DBName & ".OITM T0 WHERE T0.""ItemCode""='" & oRec.Fields.Item(0).Value & "'"
                    oTemp.DoQuery(ComVar.StrQuery)
                    If oTemp.RecordCount > 0 Then
                        Try
                            oDrfDoc.Lines.UserFields.Fields.Item("U_Item_SubGrp1").Value = oTemp.Fields.Item("U_Item_SubGrp1").Value
                        Catch ex As Exception
                        End Try
                        Try
                            oDrfDoc.Lines.UserFields.Fields.Item("U_Item_SubGrp2").Value = oTemp.Fields.Item("U_Item_SubGrp2").Value
                        Catch ex As Exception
                        End Try
                        Try
                            oDrfDoc.Lines.UserFields.Fields.Item("U_Item_SubGrp3").Value = oTemp.Fields.Item("U_Item_SubGrp3").Value
                        Catch ex As Exception
                        End Try
                        Try
                            oDrfDoc.Lines.UserFields.Fields.Item("U_Item_SubGrp4").Value = oTemp.Fields.Item("U_Item_SubGrp4").Value
                        Catch ex As Exception
                        End Try
                        Try
                            oDrfDoc.Lines.UserFields.Fields.Item("U_Item_SubGrp5").Value = oTemp.Fields.Item("U_Item_SubGrp5").Value
                        Catch ex As Exception
                        End Try
                        Try
                            oDrfDoc.Lines.UserFields.Fields.Item("U_Shelf").Value = oTemp.Fields.Item("U_Shelf").Value
                        Catch ex As Exception
                        End Try
                        Try
                            oDrfDoc.Lines.CostingCode = oTemp.Fields.Item("U_CostCenter").Value
                        Catch ex As Exception
                        End Try
                        Try
                            oDrfDoc.Lines.UserFields.Fields.Item("U_Item_Grp").Value = oTemp.Fields.Item("U_Item_Grp").Value
                        Catch ex As Exception
                        End Try
                    End If
                    blnRecordAdded = True
                    oRec.MoveNext()
                Next
            Else
                ComVar.StrMsg = "alert('Item Details missing...')"
                ErrorMess(ComVar.StrMsg)
                Return ComVar.StrMsg
            End If

            If blnRecordAdded = True Then
                If oDrfDoc.Add() <> 0 Then
                    DBConnectionDA.WriteError(ObjEN.SAPCompany.GetLastErrorDescription)
                    ComVar.StrMsg = "alert('" & ObjEN.SAPCompany.GetLastErrorDescription & "')"
                    ErrorMess(ComVar.StrMsg)
                    Return ObjEN.SAPCompany.GetLastErrorDescription
                Else
                    strcode = ObjDA.GetWebReference()
                    ObjEN.SAPCompany.GetNewObjectCode(strDocNum)
                    If oDrfDoc.GetByKey(strDocNum) Then
                        oRec.DoQuery("Update " & DBName & ".ODRF set ""NumAtCard""='" & strcode & "' where ""DocEntry""='" & strDocNum & "'")
                    End If
                    Return "Success"
                End If
            End If
        Catch ex As Exception
            DBConnectionDA.WriteError(ex.Message)
            ComVar.StrMsg = "alert('" & ex.Message & "')"
            ErrorMess(ComVar.StrMsg)
        End Try
        ' Return "Success"
    End Function
    Private Sub btncancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btncancel.Click
        panelview.Visible = True
        PanelNewRequest.Visible = False
        ObjEN.CustCode = Session("UserCode").ToString()
        ObjDA.DeleteTemp(ObjEN)
    End Sub

    Private Sub grdSales_PageIndexChanging(ByVal sender As Object, ByVal e As GridViewPageEventArgs) Handles grdSales.PageIndexChanging
        grdSales.PageIndex = e.NewPageIndex
        ObjEN.CustCode = Session("UserCode").ToString()
        MainGVBind(ObjEN)
    End Sub

    Private Sub grdSalesDraft_PageIndexChanging(ByVal sender As Object, ByVal e As GridViewPageEventArgs) Handles grdSalesDraft.PageIndexChanging
        grdSalesDraft.PageIndex = e.NewPageIndex
        ObjEN.CustCode = Session("UserCode").ToString()
        MainGVBind(ObjEN)
    End Sub
    Protected Sub lbtndocnum_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try
            If Session("UserCode") Is Nothing Or Session("SAPCompany") Is Nothing Then
                ComVar.StrMsg = "alert('Your session is Expired...')"
                ErrorMess(ComVar.StrMsg)
                Response.Redirect("Login.aspx?sessionExpired=true", True)
            Else
                Dim link As LinkButton = CType(sender, LinkButton)
                Dim gv As GridViewRow = CType((link.Parent.Parent), GridViewRow)
                Dim DocNo As LinkButton = CType(gv.FindControl("lbtDdocnum"), LinkButton)
                lblSdocnumpg.Text = DocNo.Text.Trim()
                ObjEN.CustCode = Session("UserCode").ToString()
                ObjEN.DocEntry = DocNo.Text.Trim()
                populateSalesDraft(ObjEN)
            End If
        Catch ex As Exception
            DBConnectionDA.WriteError(ex.Message)
            ComVar.StrMsg = "alert('" & ex.Message & "')"
            ErrorMess(ComVar.StrMsg)
        End Try
    End Sub
    Private Sub populateSalesDraft(ByVal objen As SalesEN)
        Try
            ComVar.SqlDS = ObjBL.PopulateDraftLine(objen)
            If ComVar.SqlDS.Tables(0).Rows.Count > 0 Then
                grdDrfLines.DataSource = ComVar.SqlDS.Tables(0)
                grdDrfLines.DataBind()
            Else
                grdDrfLines.DataBind()
            End If
        Catch ex As Exception
            DBConnectionDA.WriteError(ex.Message)
            ComVar.StrMsg = "alert('" & ex.Message & "')"
            ErrorMess(ComVar.StrMsg)
        End Try
    End Sub
    Protected Sub lbtnSdocnum_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try
            If Session("UserCode") Is Nothing Or Session("SAPCompany") Is Nothing Then
                ComVar.StrMsg = "alert('Your session is Expired...')"
                ErrorMess(ComVar.StrMsg)
                Response.Redirect("Login.aspx?sessionExpired=true", True)
            Else
                Dim link As LinkButton = CType(sender, LinkButton)
                Dim gv As GridViewRow = CType((link.Parent.Parent), GridViewRow)
                Dim DocNo As LinkButton = CType(gv.FindControl("lbtnSdocnum"), LinkButton)
                lblDocnumpg.Text = DocNo.Text.Trim()
                ObjEN.CustCode = Session("UserCode").ToString()
                ObjEN.DocEntry = DocNo.Text.Trim()
                populateSales(ObjEN)
            End If
        Catch ex As Exception
            DBConnectionDA.WriteError(ex.Message)
            ComVar.StrMsg = "alert('" & ex.Message & "')"
            ErrorMess(ComVar.StrMsg)
        End Try
    End Sub
    Private Sub populateSales(ByVal objen As SalesEN)
        Try
            ComVar.SqlDS = ObjBL.PopulateSalesLine(objen)
            If ComVar.SqlDS.Tables(0).Rows.Count > 0 Then
                grdSalesLine.DataSource = ComVar.SqlDS.Tables(0)
                grdSalesLine.DataBind()
            Else
                grdSalesLine.DataBind()
            End If
        Catch ex As Exception
            DBConnectionDA.WriteError(ex.Message)
            ComVar.StrMsg = "alert('" & ex.Message & "')"
            ErrorMess(ComVar.StrMsg)
        End Try
    End Sub

    Private Sub grdSalesItem_PageIndexChanging(ByVal sender As Object, ByVal e As GridViewPageEventArgs) Handles grdSalesItem.PageIndexChanging
        grdSalesItem.PageIndex = e.NewPageIndex
        ComVar.StrQuery = "Select ""U_DocEntry"",""U_ItemCode"",""U_ItemName"",CAST(""U_Quantity"" AS int) AS ""U_Quantity"",""U_frgName"",""U_UoMName"",Cast(""U_ShpDate"" as varchar(11)) as ""U_ShpDate"",""U_Price"",""U_LineNodays"" from " & DBName & ".""U_ORDR"" where ""U_SessionId""='" & Session("SessionId").ToString() & "' order by ""U_DocEntry"""
        ObjDA.GridviewBind(ComVar.StrQuery.ToUpper(), grdSalesItem)
    End Sub

    Private Sub grdSalesItem_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdSalesItem.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim cell As TableCell = e.Row.Cells(4)
                Dim lblqty As TextBox = DirectCast(e.Row.FindControl("lblQty"), TextBox)
                Dim lbltranscur As Label = DirectCast(e.Row.FindControl("lblPrice"), Label)
                Dim lblItemCode As Label = DirectCast(e.Row.FindControl("lblItCode"), Label)
                Dim ddlGUom As DropDownList = DirectCast(e.Row.FindControl("ddlgUoM"), DropDownList)
                Dim lbluom As Label = DirectCast(e.Row.FindControl("lblUom"), Label)
                Dim dblqty As Double = CDbl(lblqty.Text.Trim())
                If dblqty = 0 Then
                    lblqty.BackColor = Color.White
                End If

                ObjDA.BindDropdown("SELECT  T2.""UomEntry"" as ""UomEntry"", T3.""UomCode"" as ""UomCode"" FROM " & DBName & ".OITM T0 INNER JOIN " & DBName & ".OUGP T1 ON T0.""UgpEntry"" = T1.""UgpEntry"" INNER JOIN " & DBName & ".UGP1 T2 ON T1.""UgpEntry"" = T2.""UgpEntry"" INNER JOIN " & DBName & ".OUOM T3 ON T3.""UomEntry"" = T2.""UomEntry"" WHERE T0.""ItemCode""='" & lblItemCode.Text.Trim().Replace("'", "") & "'", "UomEntry", "UomCode", ddlGUom)
                'ObjEN.ItemCode = lblItemCode.Text.Trim().Replace("'", "")
                'lblSalesUom.Text = ObjBL.GetUomName(ObjEN)
                'If ddlGUom.SelectedItem.Text <> "Manual" And lblSalesUom.Text <> "" Then
                Try
                    ddlGUom.SelectedValue = ddlGUom.Items.FindByText(lbluom.Text.Trim()).Value
                Catch ex As Exception
                End Try

                'End If

                Dim rowtotal As Decimal = Convert.ToDecimal(lbltranscur.Text.Trim())
                grdTotal1 = grdTotal1 + rowtotal
            End If
            If e.Row.RowType = DataControlRowType.Footer Then
                Dim lbl As Label = CType(e.Row.FindControl("lblRCurTotal"), Label)
                lbl.Text = Math.Round(grdTotal1, 2)
            End If
        Catch ex As Exception
            DBConnectionDA.WriteError(ex.Message)
            ComVar.StrMsg = "alert('" & ex.Message & "')"
            ErrorMess(ComVar.StrMsg)
        End Try
    End Sub
    Protected Sub ddlgUoM_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oRec As SAPbobsCOM.Recordset
        Try
            ObjEN.SAPCompany = Session("SAPCompany")
            oRec = ObjEN.SAPCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
            For Each row1 As GridViewRow In grdSalesItem.Rows
                Dim lblprice As Label = DirectCast(grdSalesItem.Rows(row1.RowIndex).FindControl("lblPrice"), Label)
                Dim lblitemcode As Label = DirectCast(grdSalesItem.Rows(row1.RowIndex).FindControl("lblItCode"), Label)
                Dim ddluom As DropDownList = DirectCast(grdSalesItem.Rows(row1.RowIndex).FindControl("ddlgUoM"), DropDownList)
                Dim txtQty As TextBox = DirectCast(grdSalesItem.Rows(row1.RowIndex).FindControl("lblQty"), TextBox)

                Dim lblcode As Label = DirectCast(grdSalesItem.Rows(row1.RowIndex).FindControl("lblDocNo"), Label) ' CType(row.FindControl("lblDocNo"), Label).Text
                ComVar.StrQuery = "Update " & DBName & ".""U_ORDR"" set ""U_UoMName""='" & ddluom.SelectedItem.Text() & "',""U_UomCode""='" & ddluom.SelectedValue & "' where ""U_CardCode""='" & lblcardCode.Text.Trim() & "' and ""U_SessionId""='" & Session("SessionId").ToString() & "' and U_DocEntry='" & lblcode.Text.Trim() & "' "
                oRec.DoQuery(ComVar.StrQuery.ToUpper())
                lblprice.Text = ObjBL.GetPrice(Session("PriceList").ToString(), txtItemCode.Text.Trim().Replace("'", ""), ddluom.SelectedItem.Value)
                txtQty.Focus()
            Next
            Me.Form.DefaultButton = btnAdd.UniqueID
        Catch ex As Exception
            DBConnectionDA.WriteError(ex.Message)
            ComVar.StrMsg = "alert('" & ex.Message & "')"
            ErrorMess(ComVar.StrMsg)
        End Try
        'Try
        '    txtPrice.Text = ObjBL.GetPrice(Session("UserCode").ToString(), txtItemCode.Text.Trim().Replace("'", ""), ddlUoM.SelectedItem.Value)
        '    txtQty.Focus()
        'Catch ex As Exception
        '    DBConnectionDA.WriteError(ex.Message)
        '    ComVar.StrMsg = "alert('" & ex.Message & "')"
        '    ErrorMess(ComVar.StrMsg)
        'End Try
    End Sub
    Protected Sub lblQty_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oRec As SAPbobsCOM.Recordset

        Try
            ObjEN.SAPCompany = Session("SAPCompany")
            oRec = ObjEN.SAPCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
            For Each row1 As GridViewRow In grdSalesItem.Rows
                Dim txtCode As TextBox = DirectCast(grdSalesItem.Rows(row1.RowIndex).FindControl("lblQty"), TextBox)
                Dim dblqty As Double = CDbl(txtCode.Text.Trim())
                If dblqty = 0 Then
                    txtCode.BackColor = Color.White
                Else
                    txtCode.BackColor = Color.White
                End If

              
                Dim lblcode As Label = DirectCast(grdSalesItem.Rows(row1.RowIndex).FindControl("lblDocNo"), Label) ' CType(row.FindControl("lblDocNo"), Label).Text
                ComVar.StrQuery = "Update " & DBName & ".""U_ORDR"" set ""U_Quantity""='" & dblqty & "' where ""U_CardCode""='" & lblcardCode.Text.Trim() & "' and ""U_SessionId""='" & Session("SessionId").ToString() & "' and U_DocEntry='" & lblcode.Text.Trim() & "' "
                oRec.DoQuery(ComVar.StrQuery.ToUpper())
            Next
            Me.Form.DefaultButton = btnAdd.UniqueID
        Catch ex As Exception
            DBConnectionDA.WriteError(ex.Message)
            ComVar.StrMsg = "alert('" & ex.Message & "')"
            ErrorMess(ComVar.StrMsg)
        End Try

    End Sub

    Private Sub grdSalesItem_RowDeleting(ByVal sender As Object, ByVal e As GridViewDeleteEventArgs) Handles grdSalesItem.RowDeleting
        Try
            ObjEN.DocEntry = DirectCast(grdSalesItem.Rows(e.RowIndex).FindControl("lblDocNo"), Label).Text
            ObjEN.oChoice = "Sales"
            ObjDA.WithdrawTemp(ObjEN)
            ComVar.StrQuery = "Select ""U_DocEntry"",""U_ItemCode"",""U_ItemName"",CAST(""U_Quantity"" AS int) AS ""U_Quantity"",""U_UoMName"",""U_frgName"",Cast(""U_ShpDate"" as varchar(11)) as ""U_ShpDate"",""U_Price"",""U_LineNodays"" from " & DBName & ".""U_ORDR"" where ""U_SessionId""='" & Session("SessionId").ToString() & "' order by ""U_DocEntry"""
            ObjDA.GridviewBind(ComVar.StrQuery.ToUpper(), grdSalesItem)
        Catch ex As Exception
            DBConnectionDA.WriteError(ex.Message)
            ComVar.StrMsg = "alert('" & ex.Message & "')"
            ErrorMess(ComVar.StrMsg)
        End Try
    End Sub
    Protected Sub btngoItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btngoItem.Click
        If txtsearchItem.Text.Trim() <> "" Then
            ObjDA.GridviewBind("SELECT T1.""ItemCode"", T1.""ItemName"" FROM " & DBName & ".OSCN T0  INNER JOIN " & DBName & ".OITM T1 ON T0.""ItemCode"" = T1.""ItemCode"" WHERE T0.""CardCode"" ='" & Session("UserCode").ToString() & "' and T1.""SellItem""='Y' and Upper(T1.""ItemCode"")  like '%" + txtsearchItem.Text.Trim().Replace("'", "''").ToUpper() + "%' order by ""DocEntry"" desc", grdItems)
        ElseIf txtsearchItemNa.Text.Trim() <> "" Then
            ObjDA.GridviewBind("SELECT T1.""ItemCode"", T1.""ItemName"" FROM " & DBName & ".OSCN T0  INNER JOIN " & DBName & ".OITM T1 ON T0.""ItemCode"" = T1.""ItemCode"" WHERE T0.""CardCode"" ='" & Session("UserCode").ToString() & "' and T1.""SellItem""='Y' and Upper(T1.""ItemName"")  like '%" + txtsearchItemNa.Text.Trim().Replace("'", "''").ToUpper() + "%' order by ""DocEntry"" desc", grdItems)
        Else
            ObjDA.GridviewBind("SELECT T1.""ItemCode"", T1.""ItemName"" FROM " & DBName & ".OSCN T0  INNER JOIN " & DBName & ".OITM T1 ON T0.""ItemCode"" = T1.""ItemCode"" WHERE T0.""CardCode"" ='" & Session("UserCode").ToString() & "' and T1.""SellItem""='Y'", grdItems)
        End If
        popemployee.Show()
    End Sub
    Protected Sub lbtpopnview_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbtpopnview.Click
        ObjDA.GridviewBind("SELECT T1.""ItemCode"", T1.""ItemName"" FROM " & DBName & ".OSCN T0  INNER JOIN " & DBName & ".OITM T1 ON T0.""ItemCode"" = T1.""ItemCode"" WHERE T0.""CardCode"" ='" & Session("UserCode").ToString() & "' and T1.""SellItem""='Y'", grdItems)
        popemployee.Show()
    End Sub

    Private Sub btnImport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnImport.Click
        ObjEN.CustCode = Session("UserCode").ToString()
        ObjDA.DeleteTemp(ObjEN)
        btnSubmit.Visible = False
        btnDuplicate.Visible = False
        Dim uomentry As String
        Try
            If txtpostDate.Text = "" Then
                ComVar.StrMsg = "alert('Enter the Posting date...')"
                ErrorMess(ComVar.StrMsg)
            ElseIf txtdelDate.Text = "" Then
                ComVar.StrMsg = "alert('Enter the Delivery date...')"
                ErrorMess(ComVar.StrMsg)
            Else
                ObjEN.PostDate = ObjDA.GetDate(txtpostDate.Text.Trim()) ' Date.ParseExact(txtpostDate.Text.Trim().Replace("-", "/").Replace(".", "/"), "dd/MM/yyyy", CultureInfo.InvariantCulture)
                ObjEN.DelDate = ObjDA.GetDate(txtdelDate.Text.Trim()) ' Date.ParseExact(txtdelDate.Text.Trim().Replace("-", "/").Replace(".", "/"), "dd/MM/yyyy", CultureInfo.InvariantCulture)
                ObjEN.HeadDays = ObjBL.getNodays(ObjEN.PostDate, ObjEN.DelDate) ' lblNodays.Text.Trim()

                ObjEN.SAPCompany = Session("SAPCompany")
                ObjEN.CustCode = Session("UserCode").ToString()
                ObjEN.SessionId = Session("SessionId").ToString()
                ObjEN.DocType = "Sales"
                ObjEN.ShpDate = ObjDA.GetDeliveryDate(ObjEN)
                ObjEN.LineDays = ObjBL.getNodays(ObjEN.PostDate, ObjEN.ShpDate)
                ComVar.SqlDS2 = ObjBL.ImportCatelogItems(ObjEN)
                If ComVar.SqlDS2.Tables(0).Rows.Count > 0 Then
                    For intRow As Integer = 0 To ComVar.SqlDS2.Tables(0).Rows.Count - 1
                        ObjEN.ItemCode = ComVar.SqlDS2.Tables(0).Rows(intRow)("ItemCode").ToString().Trim().Replace("'", "")
                        ObjEN.ItemName = ComVar.SqlDS2.Tables(0).Rows(intRow)("ItemName").ToString().Trim().Replace("'", "")
                        ObjEN.Quantity = 0
                        ObjEN.Price = ObjBL.GetPrice(Session("PriceList").ToString(), ObjEN.ItemCode, ComVar.SqlDS2.Tables(0).Rows(intRow)("UgpEntry").ToString().Trim().Replace("'", ""))
                        ObjEN.FrgName = ComVar.SqlDS2.Tables(0).Rows(intRow)("FrgnName").ToString().Trim().Replace("'", "")
                        ObjEN.UomName = ComVar.SqlDS2.Tables(0).Rows(intRow)("SalUnitMsr").ToString().Trim().Replace("'", "")
                        uomentry = ComVar.SqlDS2.Tables(0).Rows(intRow)("UgpEntry").ToString().Trim().Replace("'", "")
                        If ObjEN.UomName <> "" And uomentry <> "-1" Then
                            ObjEN.UomCode = ObjBL.GetUomEntry(ObjEN)
                        Else
                            ObjEN.UomName = "Manual"
                            ObjEN.UomCode = "-1"
                        End If
                        blnFlag = ObjBL.InsertTemp(ObjEN)
                    Next
                End If

                If blnFlag = "Success" Then
                    ComVar.StrQuery = "Select ""U_DocEntry"",""U_ItemCode"",""U_ItemName"",CAST(""U_Quantity"" AS int) AS ""U_Quantity"",""U_frgName"",""U_UoMName"",Cast(""U_ShpDate"" as varchar(11)) as ""U_ShpDate"",""U_Price"",""U_LineNodays"" from " & DBName & ".""U_ORDR"" where ""U_SessionId""='" & Session("SessionId").ToString() & "' order by ""U_DocEntry"""
                    ObjDA.GridviewBind(ComVar.StrQuery.ToUpper(), grdSalesItem)
                Else
                    btnImport.Visible = True
                    ComVar.StrMsg = "alert('" & blnFlag & "')"
                    ErrorMess(ComVar.StrMsg)
                End If
            End If
            Clear()
            btnImport.Visible = False
        Catch ex As Exception
            btnImport.Visible = True
            DBConnectionDA.WriteError(ex.Message)
            ComVar.StrMsg = "alert('" & ex.Message & "')"
            ErrorMess(ComVar.StrMsg)
        End Try
    End Sub

    Private Sub btnDuplicate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDuplicate.Click
        ObjEN.CustCode = Session("UserCode").ToString()
        ObjDA.DeleteTemp(ObjEN)
        btnSubmit.Visible = True
        btnDuplicate.Visible = True
        btnImport.Visible = False
    End Sub
    Private Sub DuplicateSalesOrder(ByVal DocEntry As String)
        Dim uomentry As String
        Try
            If txtpostDate.Text = "" Then
                ComVar.StrMsg = "alert('Enter the Posting date...')"
                ErrorMess(ComVar.StrMsg)
            ElseIf txtdelDate.Text = "" Then
                ComVar.StrMsg = "alert('Enter the Delivery date...')"
                ErrorMess(ComVar.StrMsg)
            Else
                ObjEN.PostDate = ObjDA.GetDate(txtpostDate.Text.Trim()) ' Date.ParseExact(txtpostDate.Text.Trim().Replace("-", "/").Replace(".", "/"), "dd/MM/yyyy", CultureInfo.InvariantCulture)
                ObjEN.DelDate = ObjDA.GetDate(txtdelDate.Text.Trim()) ' Date.ParseExact(txtdelDate.Text.Trim().Replace("-", "/").Replace(".", "/"), "dd/MM/yyyy", CultureInfo.InvariantCulture)
                ObjEN.HeadDays = ObjBL.getNodays(ObjEN.PostDate, ObjEN.DelDate) ' lblNodays.Text.Trim()
                ObjEN.SAPCompany = Session("SAPCompany")
                ObjEN.CustCode = Session("UserCode").ToString()
                ObjEN.SessionId = Session("SessionId").ToString()
                ObjEN.ShpDate = ObjDA.GetDeliveryDate(ObjEN)
                ObjEN.LineDays = ObjBL.getNodays(ObjEN.PostDate, ObjEN.ShpDate)
                ComVar.SqlDS2 = ObjBL.ImportDuplicateSales(DocEntry)
                If ComVar.SqlDS2.Tables(0).Rows.Count > 0 Then
                    For intRow As Integer = 0 To ComVar.SqlDS2.Tables(0).Rows.Count - 1
                        ObjEN.ItemCode = ComVar.SqlDS2.Tables(0).Rows(intRow)("ItemCode").ToString().Trim().Replace("'", "")
                        ObjEN.ItemName = ComVar.SqlDS2.Tables(0).Rows(intRow)("ItemName").ToString().Trim().Replace("'", "")
                        ObjEN.Quantity = ComVar.SqlDS2.Tables(0).Rows(intRow)("Quantity").ToString()
                        ObjEN.Price = ComVar.SqlDS2.Tables(0).Rows(intRow)("Price").ToString()
                        ObjEN.FrgName = ComVar.SqlDS2.Tables(0).Rows(intRow)("FrgnName").ToString().Trim().Replace("'", "")
                        ObjEN.UomName = ComVar.SqlDS2.Tables(0).Rows(intRow)("UomCode").ToString().Trim().Replace("'", "")
                        uomentry = ComVar.SqlDS2.Tables(0).Rows(intRow)("UomEntry").ToString().Trim()
                        If ObjEN.UomName <> "Manual" And uomentry <> "-1" Then
                            ObjEN.UomCode = ObjBL.GetUomEntry(ObjEN)
                        Else
                            ObjEN.UomName = "Manual"
                            ObjEN.UomCode = "-1"
                        End If
                        blnFlag = ObjBL.InsertTemp(ObjEN)
                    Next
                End If

                If blnFlag = "Success" Then
                    ComVar.StrQuery = "Select ""U_DocEntry"",""U_ItemCode"",""U_ItemName"",CAST(""U_Quantity"" AS int) AS ""U_Quantity"",""U_frgName"",""U_UoMName"",Cast(""U_ShpDate"" as varchar(11)) as ""U_ShpDate"",""U_Price"",""U_LineNodays"" from " & DBName & ".""U_ORDR"" where ""U_SessionId""='" & Session("SessionId").ToString() & "' order by ""U_DocEntry"""
                    ObjDA.GridviewBind(ComVar.StrQuery.ToUpper(), grdSalesItem)
                Else
                    btnImport.Visible = True
                    ComVar.StrMsg = "alert('" & blnFlag & "')"
                    ErrorMess(ComVar.StrMsg)
                End If
            End If
            Clear()
            btnImport.Visible = False
        Catch ex As Exception
            btnImport.Visible = True
            DBConnectionDA.WriteError(ex.Message)
            ComVar.StrMsg = "alert('" & ex.Message & "')"
            ErrorMess(ComVar.StrMsg)
        End Try
    End Sub

    Private Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        txtpoptno.Text = ""
        txtpopunique.Text = ""
        txthidoption.Text = ""
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onclick", "popupdisplay('Sales','" + (DataBinder.Eval(e.Row.DataItem, "DocEntry")).ToString().Trim() + "','" + (DataBinder.Eval(e.Row.DataItem, "DocStatus")).ToString().Trim() + "');")
        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        If txtDocEntry.Text.Trim() <> "" Then
            ObjDA.GridviewBind("SELECT ""DocEntry"",CASE ""DocStatus"" when 'C' then 'Closed' else 'Open' end as ""DocStatus"",Cast(""DocDueDate"" as varchar(11)) as ""DocDueDate"" FROM " & DBName & ".ORDR  WHERE ""DocEntry"" ='" & txtDocEntry.Text.Trim() & "' and ""CardCode"" ='" & Session("UserCode").ToString() & "' order by ""DocEntry"" desc", GridView1)
        ElseIf ddldocStatus.SelectedIndex <> 0 Then
            ObjDA.GridviewBind("SELECT ""DocEntry"",CASE ""DocStatus"" when 'C' then 'Closed' else 'Open' end as ""DocStatus"",Cast(""DocDueDate"" as varchar(11)) as ""DocDueDate"" FROM " & DBName & ".ORDR  WHERE ""DocStatus"" ='" & ddldocStatus.SelectedValue & "' and ""CardCode"" ='" & Session("UserCode").ToString() & "' order by ""DocEntry"" desc", GridView1)
        Else
            ObjDA.GridviewBind("SELECT ""DocEntry"",CASE ""DocStatus"" when 'C' then 'Closed' else 'Open' end as ""DocStatus"",Cast(""DocDueDate"" as varchar(11)) as ""DocDueDate"" FROM " & DBName & ".ORDR  WHERE ""CardCode"" ='" & Session("UserCode").ToString() & "' order by ""DocEntry"" desc", GridView1)
        End If
        ModalPopupExtender1.Show()
    End Sub

    Private Sub LinkButton1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton1.Click
        ObjDA.GridviewBind("SELECT ""DocEntry"",CASE ""DocStatus"" when 'C' then 'Closed' else 'Open' end as ""DocStatus"",Cast(""DocDueDate"" as varchar(11)) as ""DocDueDate"" FROM " & DBName & ".ORDR  WHERE ""CardCode"" ='" & Session("UserCode").ToString() & "' order by ""DocEntry"" desc", GridView1)
        ModalPopupExtender1.Show()
    End Sub

    Private Sub grdDrfLines_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdDrfLines.PageIndexChanging
        grdDrfLines.PageIndex = e.NewPageIndex
        ObjEN.CustCode = Session("UserCode").ToString()
        ObjEN.DocEntry = lblSdocnumpg.Text
        populateSalesDraft(ObjEN)
    End Sub

    Private Sub grdSalesLine_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdSalesLine.PageIndexChanging
        grdSalesLine.PageIndex = e.NewPageIndex
        ObjEN.CustCode = Session("UserCode").ToString()
        ObjEN.DocEntry = lblDocnumpg.Text.Trim()
        populateSales(ObjEN)
    End Sub

    Private Sub txtdelDate_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtdelDate.TextChanged
        Try
            If Session("UserCode") Is Nothing Or Session("SAPCompany") Is Nothing Then
                Response.Redirect("Login.aspx?sessionExpired=true", True)
            Else
                ObjEN.DBName = DBName
                ObjEN.CustCode = Session("UserCode").ToString()
                ObjEN.SAPCompany = Session("SAPCompany")
                Postdate = txtpostDate.Text.Trim().Replace("-", "/").Replace(".", "/")
                Deldate = txtdelDate.Text.Trim().Replace("-", "/").Replace(".", "/")
                If Postdate <> "" And Deldate <> "" Then
                    ObjEN.PostDate = ObjDA.GetDate(Postdate)
                    ObjEN.DelDate = ObjDA.GetDate(Deldate)
                    lblNodays.Text = ObjBL.getNodays(ObjEN.PostDate, ObjEN.DelDate)
                End If
            End If
        Catch ex As Exception
            DBConnectionDA.WriteError(ex.Message)
            ComVar.StrMsg = "alert('" & ex.Message & "')"
            ErrorMess(ComVar.StrMsg)
        End Try
    End Sub

    Private Sub txtpostDate_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtpostDate.TextChanged

        Try
            If Session("UserCode") Is Nothing Or Session("SAPCompany") Is Nothing Then
                Response.Redirect("Login.aspx?sessionExpired=true", True)
            Else
                ObjEN.DBName = DBName
                ObjEN.CustCode = Session("UserCode").ToString()
                ObjEN.SAPCompany = Session("SAPCompany")
                Postdate = txtpostDate.Text.Trim().Replace("-", "/").Replace(".", "/")
                Deldate = txtdelDate.Text.Trim().Replace("-", "/").Replace(".", "/")
                If Postdate <> "" And Deldate <> "" Then
                    ObjEN.PostDate = ObjDA.GetDate(Postdate)
                    ObjEN.DelDate = ObjDA.GetDate(Deldate)
                    lblNodays.Text = ObjBL.getNodays(ObjEN.PostDate, ObjEN.DelDate)
                End If
            End If
        Catch ex As Exception
            DBConnectionDA.WriteError(ex.Message)
            ComVar.StrMsg = "alert('" & ex.Message & "')"
            ErrorMess(ComVar.StrMsg)
        End Try
    End Sub

    Private Sub grdDrfLines_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdDrfLines.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim lbltranscur As Label = DirectCast(e.Row.FindControl("lblDLTotal"), Label)
                Dim rowtotal As Decimal = Convert.ToDecimal(lbltranscur.Text.Trim())
                grdTotal = grdTotal + rowtotal
            End If
            If e.Row.RowType = DataControlRowType.Footer Then
                Dim lbl As Label = CType(e.Row.FindControl("lblDCurTotal"), Label)
                lbl.Text = Math.Round(grdTotal, 2)
            End If
        Catch ex As Exception
            DBConnectionDA.WriteError(ex.Message)
            ComVar.StrMsg = "alert('" & ex.Message & "')"
            ErrorMess(ComVar.StrMsg)
        End Try
    End Sub

    Private Sub grdSalesLine_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdSalesLine.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim lbltranscur As Label = DirectCast(e.Row.FindControl("lblLTotal"), Label)
                Dim rowtotal As Decimal = Convert.ToDecimal(lbltranscur.Text.Trim())
                grdTotal2 = grdTotal2 + rowtotal
            End If
            If e.Row.RowType = DataControlRowType.Footer Then
                Dim lbl As Label = CType(e.Row.FindControl("lblSCurTotal"), Label)
                lbl.Text = Math.Round(grdTotal2, 2)
            End If
        Catch ex As Exception
            DBConnectionDA.WriteError(ex.Message)
            ComVar.StrMsg = "alert('" & ex.Message & "')"
            ErrorMess(ComVar.StrMsg)
        End Try
    End Sub
   
    Private Sub ddlUoM_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlUoM.SelectedIndexChanged
        Try
            txtPrice.Text = ObjBL.GetPrice(Session("PriceList").ToString(), txtItemCode.Text.Trim().Replace("'", ""), ddlUoM.SelectedItem.Value)
            txtQty.Focus()
        Catch ex As Exception
            DBConnectionDA.WriteError(ex.Message)
            ComVar.StrMsg = "alert('" & ex.Message & "')"
            ErrorMess(ComVar.StrMsg)
        End Try
    End Sub

    Private Sub btnhome_Click(sender As Object, e As ImageClickEventArgs) Handles btnhome.Click
        Response.Redirect("Home.aspx", False)
    End Sub
End Class