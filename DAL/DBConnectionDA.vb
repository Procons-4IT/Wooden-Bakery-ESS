Imports Microsoft.VisualBasic
Imports System
Imports System.Web
Imports System.Xml
Imports System.Configuration
Imports System.Globalization
Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports EN
Imports System.Data.Odbc
Imports System.Net.Mail
Imports System.Collections.Specialized
Imports System.Security.Cryptography
Imports System.Text
Imports System.Management
Imports System.Web.UI.WebControls
Public Class DBConnectionDA
    Dim DBConnection, strError As String
    Public oCompany, objMainCompany As SAPbobsCOM.Company
    Dim objDA As CommonVariables = New CommonVariables()
    Public DBName As String = ConfigurationManager.AppSettings("CompanyDB").ToString() ' & ".dbo"
    Public Sub New()
        ' DBConnection = "data source=" & ConfigurationManager.AppSettings("SAPServer") & ";Integrated Security=SSPI;database=" & ConfigurationManager.AppSettings("CompanyDB") & ";User id=" & ConfigurationManager.AppSettings("DbUserName") & "; password=" & ConfigurationManager.AppSettings("DbPassword")
        DBConnection = ConfigurationManager.ConnectionStrings("ODBCConnection").ToString()
    End Sub
    Public Function GetConnection() As String
        Return DBConnection
    End Function
    Public Function Connection() As String
        Try
            oCompany = New SAPbobsCOM.Company
            objMainCompany = New SAPbobsCOM.Company
            oCompany.Server = ConfigurationManager.AppSettings("SAPServer") ' 
            Select Case ConfigurationManager.AppSettings("DbServerType")
                Case "2005"
                    oCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2005
                Case "2008"
                    oCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2008
                Case "2012"
                    oCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2012
                Case "2014"
                    oCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2014
                Case "SAPHANA"
                    oCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_HANADB
            End Select
            oCompany.DbUserName = ConfigurationManager.AppSettings("DbUserName") '
            oCompany.DbPassword = ConfigurationManager.AppSettings("DbPassword") ' 
            oCompany.CompanyDB = ConfigurationManager.AppSettings("CompanyDB") ' 
            oCompany.UserName = ConfigurationManager.AppSettings("SAPuserName") ' 
            oCompany.Password = ConfigurationManager.AppSettings("SAPpassword") '
            oCompany.LicenseServer = ConfigurationManager.AppSettings("SAPlicense") ' 
            oCompany.UseTrusted = ConfigurationManager.AppSettings("SAPtursted") ' 
            If oCompany.Connect <> 0 Then
                strError = oCompany.GetLastErrorDescription()
                Return strError
            Else
                objMainCompany = oCompany
                Return "Success"
            End If
        Catch ex As Exception
            DBConnectionDA.WriteError(ex.Message)
            Throw ex
        End Try
    End Function
    Public Shared Sub WriteError(ByVal errorMessage As String)
        Dim err As String
        Try

            Dim path As String = "~/Error/" & DateTime.Today.ToString("dd-MM-yy") & ".txt"

            If (Not File.Exists(System.Web.HttpContext.Current.Server.MapPath(path))) Then

                File.Create(System.Web.HttpContext.Current.Server.MapPath(path)).Close()

            End If

            Using w As StreamWriter = File.AppendText(System.Web.HttpContext.Current.Server.MapPath(path))

                w.WriteLine(Constants.vbCrLf & "Log Entry : ")

                w.WriteLine("{0}", DateTime.Now.ToString(CultureInfo.InvariantCulture))
                Try
                    err = "Error in: " & System.Web.HttpContext.Current.Request.Url.ToString() & ". Error Message:" & errorMessage
                Catch ex As Exception
                    err = " Error Message:" & errorMessage
                End Try


                w.WriteLine(err)

                w.WriteLine("__________________________")

                w.Flush()

                w.Close()

            End Using

        Catch ex As Exception

            WriteError(ex.Message)

        End Try

    End Sub
    Public Function checktable() As String
        Try
            Dim exists As Integer = 0
            objDA.SqlCon = New OdbcConnection(GetConnection)
            objDA.StrQuery = "SELECT COUNT(*) FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[U_ORDR]') AND type in (N'U')"
            objDA.SqlCmd = New OdbcCommand(objDA.StrQuery, objDA.SqlCon)
            objDA.SqlCon.Open()
            objDA.sqlreader = objDA.SqlCmd.ExecuteReader()
            If objDA.sqlreader.HasRows Then
                Do
                    While objDA.sqlreader.Read
                        exists = objDA.sqlreader(0)
                    End While
                Loop While objDA.sqlreader.NextResult()
            End If
            objDA.SqlCon.Close()
            If exists = 0 Then
                objDA.StrQuery = "CREATE TABLE U_ORDR(U_DocEntry int NOT NULL,U_CardCode nvarchar(50) NOT NULL,U_UoMName nvarchar(50) NULL,U_UomCode nvarchar(50) NULL,U_SalUom nvarchar(50) NULL,U_Headnodays int NULL,U_LineNodays int NULL,"
                objDA.StrQuery += " U_ItemCode nvarchar(50) NULL,U_ItemName nvarchar(100) NULL,U_frgName nvarchar(100) NULL,U_Quantity Decimal(18,2) NULL,U_Price Decimal(18,2) NULL,U_SessionId nvarchar(50) NOT NULL,U_ShpDate datetime NULL)"
                objDA.SqlCmd = New OdbcCommand(objDA.StrQuery, objDA.SqlCon)
                objDA.SqlCon.Open()
                objDA.SqlCmd.ExecuteNonQuery()
                objDA.SqlCon.Close()
            End If
            exists = 0
            objDA.StrQuery = "SELECT COUNT(*) FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[U_WSESSION]') AND type in (N'U')"

            objDA.SqlCmd = New OdbcCommand(objDA.StrQuery, objDA.SqlCon)
            objDA.SqlCmd.Connection.Open()
            objDA.sqlreader = objDA.SqlCmd.ExecuteReader()
            If objDA.sqlreader.HasRows Then
                Do
                    While objDA.sqlreader.Read
                        exists = objDA.sqlreader(0)
                    End While
                Loop While objDA.sqlreader.NextResult()
            End If
            objDA.SqlCmd.Connection.Close()
            If exists = 0 Then
                objDA.StrQuery = "CREATE TABLE [dbo].[U_WSESSION]([U_SESSIONID] int NOT NULL,[U_CUSTCODE] [nvarchar](max) NOT NULL, [U_LOGIN_DATE] DATETIME, [U_LOGOUT_DATE] DATETIME)"
                objDA.SqlCmd = New OdbcCommand(objDA.StrQuery, objDA.SqlCon)
                objDA.SqlCmd.Connection.Open()
                objDA.SqlCmd.ExecuteNonQuery()
                objDA.SqlCmd.Connection.Close()
            End If
            exists = 0
            objDA.StrQuery = "SELECT COUNT(*) FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[U_ORIN]') AND type in (N'U')"

            objDA.SqlCmd = New OdbcCommand(objDA.StrQuery, objDA.SqlCon)
            objDA.SqlCmd.Connection.Open()
            objDA.sqlreader = objDA.SqlCmd.ExecuteReader()
            If objDA.sqlreader.HasRows Then
                Do
                    While objDA.sqlreader.Read
                        exists = objDA.sqlreader(0)
                    End While
                Loop While objDA.sqlreader.NextResult()
            End If
            objDA.SqlCmd.Connection.Close()
            If exists = 0 Then
                objDA.StrQuery = "CREATE TABLE U_ORIN(U_DocEntry int NOT NULL,U_CardCode nvarchar(50) NOT NULL,U_UoMName nvarchar(50) NULL,U_UomCode nvarchar(50) NULL,U_SalUom nvarchar(50) NULL,"
                objDA.StrQuery += " U_ItemCode nvarchar(50) NULL,U_ItemName nvarchar(100) NULL,U_frgName nvarchar(100) NULL,U_Quantity Decimal(18,2) NULL,U_Price Decimal(18,2) NULL,U_SessionId nvarchar(50) NOT NULL)"
                objDA.SqlCmd = New OdbcCommand(objDA.StrQuery, objDA.SqlCon)
                objDA.SqlCmd.Connection.Open()
                objDA.SqlCmd.ExecuteNonQuery()
                objDA.SqlCmd.Connection.Close()
            End If
            Return ""
        Catch ex As Exception
            DBConnectionDA.WriteError(ex.Message)
            Throw ex
        End Try
    End Function
    Public Function Checkpassword(ByVal objen As ChangePwdEN) As Boolean
        objDA.SqlCon = New OdbcConnection(GetConnection)
        objDA.StrQuery = "select * from " & DBName & ".OCRD where ""CardCode""='" & objen.CustCode & "' and ""Password""='" & objen.OldPwd & "'"
        objDA.SqlDA = New OdbcDataAdapter(objDA.StrQuery, objDA.SqlCon)
        objDA.SqlDA.Fill(objDA.SqlDS)
        If objDA.SqlDS.Tables(0).Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Sub UpdatePassword(ByVal objen As ChangePwdEN)
        objDA.SqlCon = New OdbcConnection(GetConnection)
        objDA.StrQuery = "Update " & DBName & ".OCRD set ""Password""='" & objen.NewPwd & "' where ""CardCode""='" & objen.CustCode & "'"
        objDA.SqlCmd = New OdbcCommand(objDA.StrQuery, objDA.SqlCon)
        objDA.SqlCon.Open()
        objDA.SqlCmd.ExecuteNonQuery()
        objDA.SqlCon.Close()
    End Sub
   
End Class

