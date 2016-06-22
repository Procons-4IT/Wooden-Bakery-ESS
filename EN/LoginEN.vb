Public Class LoginEN
    Private _UserCode As String
    Private _Password As String
    Private _CustomerCode As String
    Private _CustomerName As String
    Private _DBName As String
    Public Property DBName() As String
        Get
            Return _DBName
        End Get
        Set(ByVal value As String)
            _DBName = value
        End Set
    End Property
    Public Property UserCode() As String
        Get
            Return _UserCode
        End Get
        Set(ByVal value As String)
            _UserCode = value
        End Set
    End Property
    Public Property Password() As String
        Get
            Return _Password
        End Get
        Set(ByVal value As String)
            _Password = value
        End Set
    End Property
    Public Property CustomerCode() As String
        Get
            Return _CustomerCode
        End Get
        Set(ByVal value As String)
            _CustomerCode = value
        End Set
    End Property
    Public Property CustomerName() As String
        Get
            Return _CustomerName
        End Get
        Set(ByVal value As String)
            _CustomerName = value
        End Set
    End Property
End Class
