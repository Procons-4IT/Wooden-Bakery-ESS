Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports EN
Imports DAL
Public Class SalesBL
    Dim ObjDA As SalesDAL = New SalesDAL()
    Public Function InsertTemp(ByVal objen As SalesEN) As String
        Try
            Return ObjDA.InsertTemp(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function getNodays(ByVal PostDate As Date, ByVal DelDate As Date) As String
        Try
            Return ObjDA.getNodays(PostDate, DelDate)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function MainGVBind(ByVal objen As SalesEN) As DataSet
        Try
            Return ObjDA.MainGVBind(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function MainGVBind1(ByVal objen As SalesEN) As DataSet
        Try
            Return ObjDA.MainGVBind1(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function PopulateDraftLine(ByVal objen As SalesEN) As DataSet
        Try
            Return ObjDA.PopulateDraftLine(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function PopulateSalesLine(ByVal objen As SalesEN) As DataSet
        Try
            Return ObjDA.PopulateSalesLine(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetRetUomName(ByVal objen As SalesEN) As String
        Try
            Return ObjDA.GetRetUomName(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetFrgName(ByVal objen As SalesEN) As String
        Try
            Return ObjDA.GetFrgName(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetUomName(ByVal objen As SalesEN) As String
        Try
            Return ObjDA.GetUomName(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetUomEntry(ByVal objen As SalesEN) As String
        Try
            Return ObjDA.GetUomEntry(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function ImportCatelogItems(ByVal objen As SalesEN) As DataSet
        Try
            Return ObjDA.ImportCatelogItems(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function ImportDuplicateSales(ByVal DocEntry As String) As DataSet
        Try
            Return ObjDA.ImportDuplicateSales(DocEntry)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetPrice(ByVal UserCode As String, ByVal itemCode As String, ByVal UomEntry As String) As Double
        Try
            Return ObjDA.GetPrice(UserCode, itemCode, UomEntry)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#Region "Credit Memo"
    Public Function InsertCreditTemp(ByVal objen As SalesEN) As String
        Try
            Return ObjDA.InsertCreditTemp(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function CMMainGVBind(ByVal objen As SalesEN) As DataSet
        Try
            Return ObjDA.CMMainGVBind(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function CMMainGVBind1(ByVal objen As SalesEN) As DataSet
        Try
            Return ObjDA.CMMainGVBind1(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function CMPopulateDraftLine(ByVal objen As SalesEN) As DataSet
        Try
            Return ObjDA.CMPopulateDraftLine(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function CMPopulateSalesLine(ByVal objen As SalesEN) As DataSet
        Try
            Return ObjDA.CMPopulateSalesLine(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region
    Public Function GetAccBalancedate(ByVal objen As SalesEN) As DataSet
        Try
            Return ObjDA.GetAccBalancedate(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function getOpeningBalance(ByVal objen As SalesEN) As Double
        Try
            Return ObjDA.getOpeningBalance(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function getEndingBalance(ByVal objen As SalesEN) As Double
        Try
            Return ObjDA.getEndingBalance(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class

