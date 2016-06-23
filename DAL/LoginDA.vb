Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.Odbc
Imports EN
Public Class LoginDA
    Dim ObjDA As DBConnectionDA = New DBConnectionDA()
    Dim ComVar As CommonVariables = New CommonVariables()
    Dim sCode As String
    Public Sub New()
        ComVar.SqlCon = New OdbcConnection(ObjDA.GetConnection)
    End Sub
    Public Function UserAuthentication(ByVal objen As LoginEN) As String
        Try
            ComVar.SqlCon.Open()
            ComVar.StrQuery = "select IFNULL(""CardCode"",'') from " & ObjDA.DBName & ".OCRD where ""AddID""='" & objen.UserCode & "' and ""Password""='" & objen.Password & "' and  NOW() between ""validFrom"" and ""validTo"" "
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
    Public Function GetCustName(ByVal objen As LoginEN) As String
        Try
            ComVar.SqlCon.Open()
            ComVar.StrQuery = "select IFNULL(""CardName"",'') from " & ObjDA.DBName & ".OCRD where ""CardCode""='" & objen.CustomerCode & "'"
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
    Public Function GetCustPriceList(ByVal objen As LoginEN) As String
        Try
            ComVar.SqlCon.Open()
            ComVar.StrQuery = "select IFNULL(""ListNum"",0) from " & ObjDA.DBName & ".OCRD where ""CardCode""='" & objen.CustomerCode & "'"
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
    Public Function SessionDetails(ByVal CustCode As String) As Integer
        Try
            Dim exists As Integer = 0
            sCode = Getmaxcode("U_WSESSION", "U_SESSIONID")
            ComVar.StrQuery = "INSERT INTO " & ObjDA.DBName & ".""U_WSESSION""(""U_SESSIONID"",""U_CUSTCODE"",""U_LOGIN_DATE"") VALUES('" & sCode & "','" & CustCode & "',NOW())"
            ComVar.SqlCmd = New OdbcCommand(ComVar.StrQuery, ComVar.SqlCon)
            ComVar.SqlCmd.Connection.Open()
            ComVar.SqlCmd.ExecuteNonQuery()
            ComVar.SqlCmd.Connection.Close()
            ComVar.SqlCon.Open()
            ComVar.SqlCmd = New OdbcCommand("SELECT MAX(""U_SESSIONID"") FROM " & ObjDA.DBName & ".""U_WSESSION""", ComVar.SqlCon)
            ComVar.SqlCmd.CommandType = CommandType.Text
            exists = ComVar.SqlCmd.ExecuteScalar()
            ComVar.SqlCon.Close()
            Return exists
        Catch ex As Exception
            DBConnectionDA.WriteError(ex.Message)
            Throw ex
        End Try
    End Function
    Public Function Getmaxcode(ByVal sTable As String, ByVal sColumn As String) As String
        Dim MaxCode As Integer

        ComVar.SqlCon.Open()
        ComVar.StrQuery = "SELECT IFNULL(max(IFNULL(CAST(IFNULL(""" & sColumn & """,'0') AS Numeric),0)),0) FROM " & ObjDA.DBName & ".""" & sTable & """"
        ComVar.SqlCmd = New OdbcCommand(ComVar.StrQuery.ToUpper(), ComVar.SqlCon)
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
    Public Function GetCreditMemo(ByVal objen As LoginEN) As String
        Try
            ComVar.SqlCon.Open()
            ComVar.StrQuery = "select IFNULL(""U_Z_AllReturn"",'N') from " & ObjDA.DBName & ".OCRD where ""CardCode""='" & objen.UserCode & "'"
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
    Public Function GetStofAccount(ByVal objen As LoginEN) As String
        Try
            ComVar.SqlCon.Open()
            ComVar.StrQuery = "select IFNULL(""U_Z_AllStAcc"",'N') from " & ObjDA.DBName & ".OCRD where ""CardCode""='" & objen.UserCode & "'"
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
End Class


