Public Class SalesEN
    Private _DocEntry As String
    Private _CustCode As String
    Private _ItemCode As String
    Private _ItemName As String
    Private _Quantity As Double
    Private _SessionId As String
    Private _SAPCompany As SAPbobsCOM.Company
    Private _oChoice As String
    Private _UomCode As String
    Private _UomName As String
    Private _ShpDate As Date
    Private _PostDate As Date
    Private _DBName As String
    Private _frgName As String
    Private _SaleUom As String
    Private _DelDate As Date
    Private _LineDays As String
    Private _HeadDays As String
    Private _Price As Double
    Private _DocType As String
    Public Property DocType() As String
        Get
            Return _DocType
        End Get
        Set(ByVal value As String)
            _DocType = value
        End Set
    End Property
    Public Property Price() As Double
        Get
            Return _Price
        End Get
        Set(ByVal value As Double)
            _Price = value
        End Set
    End Property
    Public Property DelDate() As Date
        Get
            Return _DelDate
        End Get
        Set(ByVal value As Date)
            _DelDate = value
        End Set
    End Property
    Public Property LineDays() As String
        Get
            Return _LineDays
        End Get
        Set(ByVal value As String)
            _LineDays = value
        End Set
    End Property
    Public Property HeadDays() As String
        Get
            Return _HeadDays
        End Get
        Set(ByVal value As String)
            _HeadDays = value
        End Set
    End Property
    Public Property SaleUom() As String
        Get
            Return _SaleUom
        End Get
        Set(ByVal value As String)
            _SaleUom = value
        End Set
    End Property
    Public Property FrgName() As String
        Get
            Return _frgName
        End Get
        Set(ByVal value As String)
            _frgName = value
        End Set
    End Property
    Public Property DBName() As String
        Get
            Return _DBName
        End Get
        Set(ByVal value As String)
            _DBName = value
        End Set
    End Property
    Public Property PostDate() As Date
        Get
            Return _PostDate
        End Get
        Set(ByVal value As Date)
            _PostDate = value
        End Set
    End Property
    Public Property ShpDate() As Date
        Get
            Return _ShpDate
        End Get
        Set(ByVal value As Date)
            _ShpDate = value
        End Set
    End Property
    Public Property UomCode() As String
        Get
            Return _UomCode
        End Get
        Set(ByVal value As String)
            _UomCode = value
        End Set
    End Property
    Public Property UomName() As String
        Get
            Return _UomName
        End Get
        Set(ByVal value As String)
            _UomName = value
        End Set
    End Property
    Public Property oChoice() As String
        Get
            Return _oChoice
        End Get
        Set(ByVal value As String)
            _oChoice = value
        End Set
    End Property
    Public Property SAPCompany() As SAPbobsCOM.Company
        Get
            Return _SAPCompany
        End Get
        Set(ByVal value As SAPbobsCOM.Company)
            _SAPCompany = value
        End Set
    End Property
    Public Property DocEntry() As String
        Get
            Return _DocEntry
        End Get
        Set(ByVal value As String)
            _DocEntry = value
        End Set
    End Property
    Public Property CustCode() As String
        Get
            Return _CustCode
        End Get
        Set(ByVal value As String)
            _CustCode = value
        End Set
    End Property
    Public Property ItemCode() As String
        Get
            Return _ItemCode
        End Get
        Set(ByVal value As String)
            _ItemCode = value
        End Set
    End Property
    Public Property ItemName() As String
        Get
            Return _ItemName
        End Get
        Set(ByVal value As String)
            _ItemName = value
        End Set
    End Property
    Public Property Quantity() As Double
        Get
            Return _Quantity
        End Get
        Set(ByVal value As Double)
            _Quantity = value
        End Set
    End Property
    Public Property SessionId() As String
        Get
            Return _SessionId
        End Get
        Set(ByVal value As String)
            _SessionId = value
        End Set
    End Property
End Class

