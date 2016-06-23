Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.Odbc
Imports EN
Imports System.Web.UI.WebControls
Public Class SalesDAL
    Dim ObjDA As DBConnectionDA = New DBConnectionDA()
    Dim ComVar As CommonVariables = New CommonVariables()
    Dim sCode As String
    Public Sub New()
        ComVar.SqlCon = New OdbcConnection(ObjDA.GetConnection)
    End Sub
    Public Function Getmaxcode(ByVal sTable As String, ByVal sColumn As String) As String
        Dim MaxCode As Integer

        ComVar.SqlCon.Open()
        ComVar.StrQuery = "SELECT IFNULL(max(IFNULL(CAST(IFNULL(""" & sColumn & """,'0') AS Numeric),0)),0) FROM " & ObjDA.DBName & ".""" & sTable & """"
        If sTable = "@Z_OWRE" Then
            ComVar.SqlCmd = New OdbcCommand(ComVar.StrQuery, ComVar.SqlCon)
        Else
            ComVar.SqlCmd = New OdbcCommand(ComVar.StrQuery.ToUpper(), ComVar.SqlCon)
        End If

        ComVar.SqlCmd.CommandType = CommandType.Text
        MaxCode = Convert.ToString(ComVar.SqlCmd.ExecuteScalar())
        If MaxCode >= 0 Then
            MaxCode = MaxCode + 1
        Else
            MaxCode = 1
        End If
        sCode = Format(MaxCode, "00000000")
        ComVar.SqlCon.Close()
        Return sCode
    End Function
    Public Function GetFrgName(ByVal objen As SalesEN) As String
        Try
            ComVar.SqlCon.Open()
            ComVar.StrQuery = "select IFNULL(""FrgnName"",'') from " & ObjDA.DBName & ".OITM where ""ItemCode""='" & objen.ItemCode & "'"
            ComVar.SqlCmd = New OdbcCommand(ComVar.StrQuery, ComVar.SqlCon)
            ComVar.SqlCmd.CommandType = CommandType.Text
            ComVar.ReturnValue = ComVar.SqlCmd.ExecuteScalar()
            ComVar.SqlCon.Close()
            Return ComVar.ReturnValue
        Catch ex As Exception
            DBConnectionDA.WriteError(ex.Message)
            Throw ex
        End Try
    End Function
    Public Function GetUomName(ByVal objen As SalesEN) As String
        Try
            ComVar.SqlCon.Open()
            ComVar.StrQuery = "select IFNULL(""SalUnitMsr"",'') from " & ObjDA.DBName & ".OITM where ""ItemCode""='" & objen.ItemCode & "'"
            ComVar.SqlCmd = New OdbcCommand(ComVar.StrQuery, ComVar.SqlCon)
            ComVar.SqlCmd.CommandType = CommandType.Text
            ComVar.ReturnValue = ComVar.SqlCmd.ExecuteScalar()
            ComVar.SqlCon.Close()
            Return ComVar.ReturnValue
        Catch ex As Exception
            DBConnectionDA.WriteError(ex.Message)
            Throw ex
        End Try
    End Function
    Public Function GetRetUomName(ByVal objen As SalesEN) As String
        Try
            ComVar.SqlCon.Open()
            ComVar.StrQuery = "select IFNULL(""U_UoM"",'') from " & ObjDA.DBName & ".OSCN where ""ItemCode""='" & objen.ItemCode & "'"
            ComVar.SqlCmd = New OdbcCommand(ComVar.StrQuery, ComVar.SqlCon)
            ComVar.SqlCmd.CommandType = CommandType.Text
            ComVar.ReturnValue = ComVar.SqlCmd.ExecuteScalar()
            ComVar.SqlCon.Close()
            Return ComVar.ReturnValue
        Catch ex As Exception
            DBConnectionDA.WriteError(ex.Message)
            Throw ex
        End Try
    End Function
    Public Function GetUomEntry(ByVal objen As SalesEN) As String
        Try
            ComVar.SqlCon.Open()
            ComVar.StrQuery = "select IFNULL(""UomEntry"",'-1') from " & ObjDA.DBName & ".OUOM where ""UomCode""='" & objen.UomName & "'"
            ComVar.SqlCmd = New OdbcCommand(ComVar.StrQuery, ComVar.SqlCon)
            ComVar.SqlCmd.CommandType = CommandType.Text
            ComVar.ReturnValue = ComVar.SqlCmd.ExecuteScalar()
            ComVar.SqlCon.Close()
            Return ComVar.ReturnValue
        Catch ex As Exception
            DBConnectionDA.WriteError(ex.Message)
            Throw ex
        End Try
    End Function
    Public Function GetWebReference() As String
        Try
            sCode = Getmaxcode("@Z_OWRE", "Code")
            ComVar.StrQuery = "Insert Into " & ObjDA.DBName & ".""@Z_OWRE"" (""Code"",""Name"") Values ('" & sCode.Trim() & "','" & sCode.Trim() & "')"
            ComVar.SqlCmd = New OdbcCommand(ComVar.StrQuery, ComVar.SqlCon)
            ComVar.SqlCmd.Connection.Open()
            ComVar.SqlCmd.ExecuteNonQuery()
            ComVar.SqlCmd.Connection.Close()
            Return sCode
        Catch ex As Exception
            DBConnectionDA.WriteError(ex.Message)
            Throw ex
        End Try
    End Function
    Public Function MainGVBind(ByVal objen As SalesEN) As DataSet
        ComVar.SqlDS.Clear()
        Try
            ComVar.StrQuery = "Select ""DocEntry"",""CardCode"",""CardName"",Cast(""DocDate"" as varchar(11)) as ""DocDate"",Cast(""DocDueDate"" as varchar(11)) as ""DocDueDate"",Cast(""TaxDate"" as varchar(11)) as ""TaxDate"","
            ComVar.StrQuery += " Case ""DocStatus"" when 'O' then 'Open' when 'C' then 'Closed' end as ""DocStatus""  from " & ObjDA.DBName & ".ODRF where ""ObjType""='17' and ""CardCode""='" & objen.CustCode & "' order by ""DocEntry"" desc"
            ComVar.SqlDA = New OdbcDataAdapter(ComVar.StrQuery, ComVar.SqlCon)
            ComVar.SqlDA.Fill(ComVar.SqlDS)
            Return ComVar.SqlDS
        Catch ex As Exception
            DBConnectionDA.WriteError(ex.Message)
            Throw ex
        End Try
    End Function
    Public Function MainGVBind1(ByVal objen As SalesEN) As DataSet
        ComVar.SqlDS.Clear()
        Try
            ComVar.StrQuery = "Select ""DocEntry"",""CardCode"",""CardName"",Cast(""DocDate"" as varchar(11)) as ""DocDate"",Cast(""DocDueDate"" as varchar(11)) as ""DocDueDate"",Cast(""TaxDate"" as varchar(11)) as ""TaxDate"","
            ComVar.StrQuery += " Case ""DocStatus"" when 'O' then 'Open' when 'C' then 'Closed' end as ""DocStatus""  from " & ObjDA.DBName & ".ORDR where ""CardCode""='" & objen.CustCode & "' order by ""DocEntry"" desc"
            ComVar.SqlDA = New OdbcDataAdapter(ComVar.StrQuery, ComVar.SqlCon)
            ComVar.SqlDA.Fill(ComVar.SqlDS)
            Return ComVar.SqlDS
        Catch ex As Exception
            DBConnectionDA.WriteError(ex.Message)
            Throw ex
        End Try
    End Function
    Public Function PopulateDraftLine(ByVal objen As SalesEN) As DataSet
        Try
            ComVar.StrQuery = "Select ""DocEntry"",""ItemCode"",""Dscription"",CAST(""Quantity"" AS int) AS ""Quantity"",CAST(""Price"" AS Decimal(10,2)) AS ""Price"",CAST(""LineTotal"" AS Decimal(10,2)) AS ""LineTotal"",""UomCode"",""U_Z_Noofdays"","
            ComVar.StrQuery += " Case ""LineStatus"" when 'O' then 'Open' when 'C' then 'Closed' end as ""LineStatus""  from " & ObjDA.DBName & ".DRF1 where ""ObjType""='17' and ""DocEntry""='" & objen.DocEntry & "'"
            ComVar.SqlDA = New OdbcDataAdapter(ComVar.StrQuery, ComVar.SqlCon)
            ComVar.SqlDA.Fill(ComVar.SqlDS1)
            Return ComVar.SqlDS1
        Catch ex As Exception
            DBConnectionDA.WriteError(ex.Message)
            Throw ex
        End Try
    End Function
    Public Function PopulateSalesLine(ByVal objen As SalesEN) As DataSet
        Try
            ComVar.StrQuery = "Select ""DocEntry"",""ItemCode"",""Dscription"",CAST(""Quantity"" AS int) AS ""Quantity"",CAST(""Price"" AS Decimal(10,2)) AS ""Price"",CAST(""LineTotal"" AS Decimal(10,2)) AS ""LineTotal"",""UomCode"","
            ComVar.StrQuery += " Case ""LineStatus"" when 'O' then 'Open' when 'C' then 'Closed' end as ""LineStatus"",""U_Z_Noofdays""  from " & ObjDA.DBName & ".RDR1 where ""DocEntry""='" & objen.DocEntry & "'"
            ComVar.SqlDA = New OdbcDataAdapter(ComVar.StrQuery, ComVar.SqlCon)
            ComVar.SqlDA.Fill(ComVar.SqlDS1)
            Return ComVar.SqlDS1
        Catch ex As Exception
            DBConnectionDA.WriteError(ex.Message)
            Throw ex
        End Try
    End Function
    Public Sub GridviewBind(ByVal query As String, ByVal Gv As GridView)
        Try
            ComVar.SqlDA = New OdbcDataAdapter(query, ComVar.SqlCon)
            Dim gds As DataSet = New DataSet()
            ComVar.SqlDA.Fill(gds)
            Gv.DataSource = gds
            Gv.DataBind()
        Catch ex As Exception
            DBConnectionDA.WriteError(ex.Message)
            Throw ex
        End Try
    End Sub
    Public Sub BindDropdown(ByVal query As String, ByVal valcode As String, ByVal valname As String, ByVal ddl As DropDownList)
        Try
            ComVar.SqlDA = New OdbcDataAdapter(query, ComVar.SqlCon)
            Dim Dds As DataSet = New DataSet()
            ComVar.SqlDA.Fill(Dds)
            ddl.DataTextField = valname
            ddl.DataValueField = valcode
            ddl.DataSource = Dds
            ddl.DataBind()
        Catch ex As Exception
            DBConnectionDA.WriteError(ex.Message)
            Throw ex
        End Try
    End Sub
    Public Function getNodays(ByVal PostDate As Date, ByVal DelDate As Date) As String
        Try
            ComVar.SqlDS.Clear()
            ComVar.StrQuery = "SELECT DAYS_BETWEEN (TO_DATE ('" & PostDate.ToString("yyyy-MM-dd") & "'), TO_DATE('" & DelDate.ToString("yyyy-MM-dd") & "')) FROM DUMMY"
            ' ComVar.StrQuery = "select datediff(D,'" & PostDate.ToString("yyyy/MM/dd") & "','" & DelDate.ToString("yyyy/MM/dd") & "')"
            ComVar.SqlDA = New OdbcDataAdapter(ComVar.StrQuery, ComVar.SqlCon)
            ComVar.SqlDA.Fill(ComVar.SqlDS)
            If ComVar.SqlDS.Tables(0).Rows.Count > 0 Then
                ComVar.StrMsg = CDbl(ComVar.SqlDS.Tables(0).Rows(0)(0).ToString()) + 1
            End If
            Return ComVar.StrMsg
        Catch ex As Exception
            DBConnectionDA.WriteError(ex.Message)
            Throw ex
        End Try
    End Function
    Public Function GetPrice(ByVal PriceList As String, ByVal itemCode As String, ByVal UomEntry As String) As Double
        ComVar.SqlDS3.Clear()
        Dim dblPrice As Double = 0
        Try
            ComVar.StrQuery = "SELECT T1.""PriceList"", T1.""Price"" FROM " & ObjDA.DBName & ".ITM9  T1  JOIN " & ObjDA.DBName & ".OUOM T3 ON T3.""UomEntry"" = T1.""UomEntry""" ' JOIN  " & ObjDA.DBName & ".OCRD T2 on T2.""ListNum"" =T1.""PriceList"""
            ComVar.StrQuery += "  WHERE T1.""ItemCode"" ='" & itemCode.Trim() & "' and"
            ComVar.StrQuery += " T1.""PriceList""=" & PriceList.Trim() & " and T3.""UomEntry""='" & UomEntry.Trim() & "' "
            ComVar.SqlDA = New OdbcDataAdapter(ComVar.StrQuery, ComVar.SqlCon)
            ComVar.SqlDA.Fill(ComVar.SqlDS3)
            If ComVar.SqlDS3.Tables(0).Rows.Count > 0 Then
                dblPrice = ComVar.SqlDS3.Tables(0).Rows(0)(1).ToString()
                Return dblPrice
            End If
        Catch ex As Exception
            DBConnectionDA.WriteError(ex.Message)
            Throw ex
        End Try
    End Function
    Public Function InsertTemp(ByVal objen As SalesEN) As String
        Try
            sCode = Getmaxcode("U_ORDR", "U_DocEntry")
            ComVar.StrQuery = "Insert Into " & ObjDA.DBName & ".""U_ORDR"" (""U_DocEntry"",""U_CardCode"",""U_ItemCode"",""U_ItemName"",""U_Quantity"",""U_SessionId"",""U_ShpDate"",""U_UoMName"",""U_UomCode"",""U_frgName"",""U_SalUom"",""U_Headnodays"",""U_LineNodays"",""U_Price"")"
            ComVar.StrQuery += " Values ('" & sCode & "','" & objen.CustCode & "','" & objen.ItemCode & "','" & objen.ItemName & "','" & objen.Quantity & "','" & objen.SessionId & "','" & objen.ShpDate.ToString("yyyy-MM-dd") & "','" & objen.UomName & "','" & objen.UomCode & "','" & objen.FrgName & "','" & objen.SaleUom & "','" & objen.HeadDays & "','" & objen.LineDays & "','" & objen.Price & "')"
            ComVar.SqlCmd = New OdbcCommand(ComVar.StrQuery.ToUpper(), ComVar.SqlCon)
            ComVar.SqlCmd.Connection.Open()
            ComVar.SqlCmd.ExecuteNonQuery()
            ComVar.SqlCmd.Connection.Close()
            Return "Success"
        Catch ex As Exception
            DBConnectionDA.WriteError(ex.Message)
            Throw ex
            Return ex.Message
        End Try
    End Function
    Public Sub DeleteTemp(ByVal objen As SalesEN)
        Try
            If objen.DocType = "Credit" Then
                ComVar.StrQuery = "delete from " & ObjDA.DBName & ".""U_ORIN"" where ""U_CardCode""='" & objen.CustCode & "'"
            Else
                ComVar.StrQuery = "delete from " & ObjDA.DBName & ".""U_ORDR"" where ""U_CardCode""='" & objen.CustCode & "'"
            End If
            ComVar.SqlCmd = New OdbcCommand(ComVar.StrQuery.ToUpper(), ComVar.SqlCon)
            ComVar.SqlCmd.Connection.Open()
            ComVar.SqlCmd.ExecuteNonQuery()
            ComVar.SqlCmd.Connection.Close()
        Catch ex As Exception
            DBConnectionDA.WriteError(ex.Message)
            Throw ex
        End Try
    End Sub
    Public Function ImportCatelogItems(ByVal objen As SalesEN) As DataSet
        Try
            If objen.DocType = "Credit" Then
                ComVar.StrQuery = "select T0.""ItemCode"",T1.""ItemName"",T1.""FrgnName"",IFNULL(T1.""SalUnitMsr"",'') AS ""SalUnitMsr"",T1.""UgpEntry"" from " & ObjDA.DBName & ".OSCN T0 inner Join " & ObjDA.DBName & ".OITM T1 ON T0.""ItemCode""=T1.""ItemCode"" where T0.""CardCode""='" & objen.CustCode & "' and ""U_IsReturn""='Y' "
            Else
                ComVar.StrQuery = "select T0.""ItemCode"",T1.""ItemName"",T1.""FrgnName"",IFNULL(T1.""SalUnitMsr"",'') AS ""SalUnitMsr"",T1.""UgpEntry"" from " & ObjDA.DBName & ".OSCN T0 inner Join " & ObjDA.DBName & ".OITM T1 ON T0.""ItemCode""=T1.""ItemCode"" where T0.""CardCode""='" & objen.CustCode & "' "
            End If
            ComVar.SqlDA = New OdbcDataAdapter(ComVar.StrQuery, ComVar.SqlCon)
            ComVar.SqlDA.Fill(ComVar.SqlDS2)
            Return ComVar.SqlDS2
        Catch ex As Exception
            DBConnectionDA.WriteError(ex.Message)
            Throw ex
        End Try
    End Function
    Public Function ImportDuplicateSales(ByVal DocEntry As String) As DataSet
        Try
            ComVar.StrQuery = "select T0.""ItemCode"",T1.""ItemName"",T1.""FrgnName"",T0.""Quantity"",T0.""UomCode"",T0.""UomEntry"",T0.""Price""  from " & ObjDA.DBName & ".RDR1 T0 inner join " & ObjDA.DBName & ".OITM T1 on T0.""ItemCode""=T1.""ItemCode"" where T0.""DocEntry""='" & DocEntry & "' "
            ComVar.SqlDA = New OdbcDataAdapter(ComVar.StrQuery, ComVar.SqlCon)
            ComVar.SqlDA.Fill(ComVar.SqlDS2)
            Return ComVar.SqlDS2
        Catch ex As Exception
            DBConnectionDA.WriteError(ex.Message)
            Throw ex
        End Try
    End Function
#Region "Credit Memo"
    Public Function InsertCreditTemp(ByVal objen As SalesEN) As String
        Try
            sCode = Getmaxcode("U_ORIN", "U_DocEntry")
            ComVar.StrQuery = "Insert Into " & ObjDA.DBName & ".""U_ORIN"" (""U_DocEntry"",""U_CardCode"",""U_ItemCode"",""U_ItemName"",""U_Quantity"",""U_SessionId"",""U_UoMName"",""U_UomCode"",""U_frgName"",""U_Price"")"
            ComVar.StrQuery += " Values ('" & sCode & "','" & objen.CustCode & "','" & objen.ItemCode & "','" & objen.ItemName & "','" & objen.Quantity & "','" & objen.SessionId & "','" & objen.UomName & "','" & objen.UomCode & "','" & objen.FrgName & "','" & objen.Price & "')"
            ComVar.SqlCmd = New OdbcCommand(ComVar.StrQuery.ToUpper(), ComVar.SqlCon)
            ComVar.SqlCmd.Connection.Open()
            ComVar.SqlCmd.ExecuteNonQuery()
            ComVar.SqlCmd.Connection.Close()
            Return "Success"
        Catch ex As Exception
            DBConnectionDA.WriteError(ex.Message)
            Throw ex
            Return ex.Message
        End Try
    End Function
    Public Sub DeleteCreditTemp(ByVal objen As SalesEN)
        Try
            ComVar.StrQuery = "delete from " & ObjDA.DBName & ".""U_ORIN"" where ""U_CardCode""='" & objen.CustCode & "'"
            ComVar.SqlCmd = New OdbcCommand(ComVar.StrQuery.ToUpper(), ComVar.SqlCon)
            ComVar.SqlCmd.Connection.Open()
            ComVar.SqlCmd.ExecuteNonQuery()
            ComVar.SqlCmd.Connection.Close()
        Catch ex As Exception
            DBConnectionDA.WriteError(ex.Message)
            Throw ex
        End Try
    End Sub
    Public Function CMMainGVBind(ByVal objen As SalesEN) As DataSet
        ComVar.SqlDS.Clear()
        Try
            ComVar.StrQuery = "Select ""DocEntry"",""CardCode"",""CardName"",Cast(""DocDate"" as varchar(11)) as ""DocDate"",Cast(""DocDueDate"" as varchar(11)) as ""DocDueDate"",Cast(""TaxDate"" as varchar(11)) as ""TaxDate"","
            ComVar.StrQuery += " Case ""DocStatus"" when 'O' then 'Open' when 'C' then 'Closed' end as ""DocStatus""  from " & ObjDA.DBName & ".ODRF where ""ObjType""='14' and ""CardCode""='" & objen.CustCode & "' order by ""DocEntry"" desc"
           ComVar.SqlDA = New OdbcDataAdapter(ComVar.StrQuery, ComVar.SqlCon)
            ComVar.SqlDA.Fill(ComVar.SqlDS)
            Return ComVar.SqlDS
        Catch ex As Exception
            DBConnectionDA.WriteError(ex.Message)
            Throw ex
        End Try
    End Function
    Public Function CMMainGVBind1(ByVal objen As SalesEN) As DataSet
        ComVar.SqlDS.Clear()
        Try
            ComVar.StrQuery = "Select ""DocEntry"",""CardCode"",""CardName"",Cast(""DocDate"" as varchar(11)) as ""DocDate"",Cast(""DocDueDate"" as varchar(11)) as ""DocDueDate"",Cast(""TaxDate"" as varchar(11)) as ""TaxDate"","
            ComVar.StrQuery += " Case ""DocStatus"" when 'O' then 'Open' when 'C' then 'Closed' end as ""DocStatus""  from " & ObjDA.DBName & ".ORIN where ""CardCode""='" & objen.CustCode & "' order by ""DocEntry"" desc"
            ComVar.SqlDA = New OdbcDataAdapter(ComVar.StrQuery, ComVar.SqlCon)
            ComVar.SqlDA.Fill(ComVar.SqlDS)
            Return ComVar.SqlDS
        Catch ex As Exception
            DBConnectionDA.WriteError(ex.Message)
            Throw ex
        End Try
    End Function
    Public Function CMPopulateDraftLine(ByVal objen As SalesEN) As DataSet
        Try
            ComVar.StrQuery = "Select ""DocEntry"",""ItemCode"",""Dscription"",CAST(""Quantity"" AS int) AS ""Quantity"",CAST(""Price"" AS Decimal(10,2)) AS ""Price"",CAST(""LineTotal"" AS Decimal(10,2)) AS ""LineTotal"",""UomCode"","
            ComVar.StrQuery += " Case ""LineStatus"" when 'O' then 'Open' when 'C' then 'Closed' end as ""LineStatus""  from " & ObjDA.DBName & ".DRF1 where ""ObjType""='14' and ""DocEntry""='" & objen.DocEntry & "'"
            ComVar.SqlDA = New OdbcDataAdapter(ComVar.StrQuery, ComVar.SqlCon)
            ComVar.SqlDA.Fill(ComVar.SqlDS1)
            Return ComVar.SqlDS1
        Catch ex As Exception
            DBConnectionDA.WriteError(ex.Message)
            Throw ex
        End Try
    End Function
    Public Function CMPopulateSalesLine(ByVal objen As SalesEN) As DataSet
        Try
            ComVar.StrQuery = "Select ""DocEntry"",""ItemCode"",""Dscription"",CAST(""Quantity"" AS int) AS ""Quantity"",CAST(""Price"" AS Decimal(10,2)) AS ""Price"",CAST(""LineTotal"" AS Decimal(10,2)) AS ""LineTotal"",""UomCode"","
            ComVar.StrQuery += " Case ""LineStatus"" when 'O' then 'Open' when 'C' then 'Closed' end as ""LineStatus""  from " & ObjDA.DBName & ".RIN1 where ""DocEntry""='" & objen.DocEntry & "'"
            ComVar.SqlDA = New OdbcDataAdapter(ComVar.StrQuery, ComVar.SqlCon)
            ComVar.SqlDA.Fill(ComVar.SqlDS1)
            Return ComVar.SqlDS1
        Catch ex As Exception
            DBConnectionDA.WriteError(ex.Message)
            Throw ex
        End Try
    End Function
    Public Sub WithdrawTemp(ByVal objen As SalesEN)
        Try
            If objen.oChoice = "Sales" Then
                ComVar.StrQuery = "delete from " & ObjDA.DBName & ".""U_ORDR"" where ""U_DocEntry""='" & objen.DocEntry & "'"
            ElseIf objen.oChoice = "Credit" Then
                ComVar.StrQuery = "delete from " & ObjDA.DBName & ".""U_ORIN"" where ""U_DocEntry""='" & objen.DocEntry & "'"
            End If
            ComVar.SqlCmd = New OdbcCommand(ComVar.StrQuery.ToUpper(), ComVar.SqlCon)
            ComVar.SqlCmd.Connection.Open()
            ComVar.SqlCmd.ExecuteNonQuery()
            ComVar.SqlCmd.Connection.Close()
        Catch ex As Exception
            DBConnectionDA.WriteError(ex.Message)
            Throw ex
        End Try
    End Sub
    Public Function GetDate(ByVal strDate As String) As DateTime
        Try
            Dim stdate As String = strDate.Trim().Replace("-", "/").Replace(".", "/")
            stdate = stdate.Trim().Replace("/", "")
            Dim stda As String = stdate.Substring(0, 2)
            Dim stmon As String = stdate.Substring(2, 2)
            Dim styear As String = stdate.Substring(4, 4)
            Dim dttime As New DateTime(CInt(styear), CInt(stmon), CInt(stda))
            Return dttime.Date
        Catch ex As Exception
            DBConnectionDA.WriteError(ex.Message & strDate)
        End Try
    End Function
    Public Function GetDeliveryDate(ByVal objen As SalesEN) As Date 'aCardCode As String, aItemCode As String, aPostingDate As Date
        Dim oRec, oRec1 As SAPbobsCOM.Recordset
        Dim intWeekFrom, intWeekTo As Integer
        oRec = objen.SAPCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
        oRec1 = objen.SAPCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
        oRec.DoQuery("Select * from " & ObjDA.DBName & ".OCRD where ""CardCode""='" & objen.CustCode & "'")
        If oRec.Fields.Item("U_Z_WeekEnd").Value <> "" Then
            oRec1.DoQuery("Select * from " & ObjDA.DBName & ".""@Z_OWEM"" where ""U_Z_Code""='" & oRec.Fields.Item("U_Z_WeekEnd").Value & "'")
        Else
            oRec1.DoQuery("Select * from " & ObjDA.DBName & ".""@Z_OWEM"" where ""U_Z_Default""='Y'")
        End If
        If oRec1.RecordCount > 0 Then
            intWeekFrom = oRec1.Fields.Item("U_Z_From").Value
            intWeekTo = oRec1.Fields.Item("U_Z_End").Value
        Else
            intWeekFrom = 7
            intWeekTo = 1
        End If
        oRec1.DoQuery("Select * from " & ObjDA.DBName & ".OITM where ""ItemCode""='" & objen.ItemCode & "'")
        If oRec1.Fields.Item("U_Z_DelDays").Value > 0 Then
            objen.PostDate = DateAdd("D", oRec1.Fields.Item("U_Z_DelDays").Value, objen.PostDate)
        End If
        Dim intDay As Integer = objen.PostDate.DayOfWeek
        If intDay = intWeekFrom Then
            objen.PostDate = DateAdd("D", 1, objen.PostDate)
        End If
        intDay = objen.PostDate.DayOfWeek

        If intDay = intWeekTo Then
            objen.PostDate = DateAdd("D", 1, objen.PostDate)
        End If
        Return objen.PostDate
    End Function
#End Region


#Region "Statement of Accounts"
    Public Function GetAccBalancedate(ByVal objen As SalesEN) As DataSet
        ComVar.StrQuery = "SELECT Cast(T0.""TaxDate"" as varchar(11)) as ""Date"", case T0.""TransType"" when 13 then 'Invoice'  when 14 then 'CreditNotes' "
        ComVar.StrQuery += " when 15 then 'Delivery' when 16 then 'Returns' when 17 then 'Order' when 203 then 'AR DownPayment' when 20 then 'GRPO' when 18 then 'Purchase Invoce' "
        ComVar.StrQuery += " when 19 then 'AP Credit Notes' when 21 then 'Purchase Return' when 24 then 'Incoming Payment' when 30 then 'Journal Entry'"
        ComVar.StrQuery += "  when 46 then 'Outgoing Payment' else T0.""TransType""  end as ""Transaction"", T0.""BaseRef"" as ""Ref"", T0.""Memo"", IFNULL(T1.""FCCurrency"",'' ) as ""FCCurrency"", "
        'ComVar.StrQuery += "  when 46 then 'Outgoing Payment' else T0.""TransType""  end as ""Transaction"", T0.""BaseRef"" as ""Ref"", T0.""Memo"", cast(T1.""FCCredit"" as decimal(38,2)) as ""FCCredit"", cast(T1.""FCDebit"" as decimal(38,2)) as ""FCDebit"",IFNULL(T1.""FCCurrency"",'' ) as ""FCCurrency"", "
        ComVar.StrQuery += " cast(T1.""Debit"" as decimal(38,2)) as ""Debit"", cast(T1.""Credit"" as decimal(38,2)) as ""Credit"" FROM " & ObjDA.DBName & ".OJDT T0  "
        ComVar.StrQuery += " INNER JOIN " & ObjDA.DBName & ".JDT1 T1 ON T0.""TransId"" = T1.""TransId"" INNER JOIN " & ObjDA.DBName & ".OACT T2 ON T1.""Account"" = T2.""AcctCode"" WHERE T1.""ShortName""='" & objen.CustCode & "' "
        ComVar.StrQuery += " and (IFNULL(T1.""ExtrMatch"",0)=0) and T0.""TaxDate"" between '" & objen.PostDate.ToString("yyyy-MM-dd") & "' and '" & objen.ShpDate.ToString("yyyy-MM-dd") & "' "
        ComVar.SqlDA = New OdbcDataAdapter(ComVar.StrQuery, ComVar.SqlCon)
        ComVar.SqlDA.Fill(ComVar.SqlDS)
        Return ComVar.SqlDS
    End Function
    Public Function getOpeningBalance(ByVal objen As SalesEN) As Double
        Dim dblBalance As Double
        ComVar.SqlCon.Open()
        ComVar.StrQuery = "SELECT IFNULL(sum(cast(T1.""Debit"" as decimal(38,2))- cast(T1.""Credit"" as decimal(38,2)) ),0) AS ""OB"" FROM " & ObjDA.DBName & ".OJDT T0 "
        ComVar.StrQuery += "  INNER JOIN " & ObjDA.DBName & ".JDT1 T1 ON T0.""TransId"" = T1.""TransId"" INNER JOIN " & ObjDA.DBName & ".OACT T2 ON T1.""Account"" = T2.""AcctCode"" WHERE T1.""ShortName""='" & objen.CustCode & "'"
        ComVar.StrQuery += " and T0.""TaxDate"" < '" & objen.PostDate.ToString("yyyy-MM-dd") & "'"
        ComVar.SqlCmd = New OdbcCommand(ComVar.StrQuery, ComVar.SqlCon)
        ComVar.sqlreader = ComVar.SqlCmd.ExecuteReader
        If ComVar.sqlreader.HasRows Then
            Do
                While ComVar.sqlreader.Read
                    dblBalance = ComVar.sqlreader(0)
                End While
            Loop While ComVar.sqlreader.NextResult()
        End If
        ComVar.SqlCon.Close()
        Return dblBalance
    End Function
    Public Function getEndingBalance(ByVal objen As SalesEN) As Double
        Dim dblBalance As Double
        ComVar.SqlCon.Open()
        ComVar.StrQuery = "SELECT IFNULL(sum(cast(T1.""Debit"" as decimal(38,2))- cast(T1.""Credit"" as decimal(38,2)) ),0) AS ""OB"" FROM " & ObjDA.DBName & ".OJDT T0 "
        ComVar.StrQuery += "   INNER JOIN " & ObjDA.DBName & ".JDT1 T1 ON T0.""TransId"" = T1.""TransId"" INNER JOIN " & ObjDA.DBName & ".OACT T2 ON T1.""Account"" = T2.""AcctCode"" WHERE T1.""ShortName""='" & objen.CustCode & "'"
        ComVar.StrQuery += " and T0.""TaxDate"" <= '" & objen.ShpDate.ToString("yyyy-MM-dd") & "'"
        ComVar.SqlCmd = New OdbcCommand(ComVar.StrQuery, ComVar.SqlCon)
        ComVar.sqlreader = ComVar.SqlCmd.ExecuteReader
        If ComVar.sqlreader.HasRows Then
            Do
                While ComVar.sqlreader.Read
                    dblBalance = ComVar.sqlreader(0)
                End While
            Loop While ComVar.sqlreader.NextResult()
        End If
        ComVar.SqlCon.Close()
        Return dblBalance
    End Function
#End Region
End Class
