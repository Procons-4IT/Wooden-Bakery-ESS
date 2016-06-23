Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports EN
Imports DAL
Public Class LoginBL
    Dim ObjDA As LoginDA = New LoginDA()
    Public Function UserAuthentication(ByVal objen As LoginEN) As String
        Try
            Return ObjDA.UserAuthentication(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetCustName(ByVal objen As LoginEN) As String
        Try
            Return ObjDA.GetCustName(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetCustPriceList(ByVal objen As LoginEN) As String
        Try
            Return ObjDA.GetCustPriceList(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function SessionDetails(ByVal CustCode As String) As Integer
        Try
            Return objDA.SessionDetails(CustCode)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetCreditMemo(ByVal objen As LoginEN) As String
        Try
            Return ObjDA.GetCreditMemo(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetStofAccount(ByVal objen As LoginEN) As String
        Try
            Return ObjDA.GetStofAccount(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
