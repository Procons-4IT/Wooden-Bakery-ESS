Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.Odbc
Public Class CommonVariables
    Public SqlCon As OdbcConnection 'OdbcConnection
    Public SqlCmd As OdbcCommand ' OdbcCommand
    Public SqlDS As New DataSet()
    Public SqlDS1 As New DataSet()
    Public SqlDS2 As New DataSet()
    Public SqlDS3 As New DataSet()
    Public SqlDA As OdbcDataAdapter '  OdbcDataAdapter
    Public sqlreader As OdbcDataReader ' OdbcDataReader
    Public StrQuery As String
    Public ReturnValue As String
    Public StrMsg As String
End Class
