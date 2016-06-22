Public Class ChangePwdEN
    Private _CustCode As String
    Private _OldPwd As String
    Private _NewPwd As String
    Private _ConfirmPwd As String
    Private _DBName As String
    Public Property DBName() As String
        Get
            Return _DBName
        End Get
        Set(ByVal value As String)
            _DBName = value
        End Set
    End Property
    Public Property ConfirmPwd() As String
        Get
            Return _ConfirmPwd
        End Get
        Set(ByVal value As String)
            _ConfirmPwd = value
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
    Public Property OldPwd() As String
        Get
            Return _OldPwd
        End Get
        Set(ByVal value As String)
            _OldPwd = value
        End Set
    End Property
    Public Property NewPwd() As String
        Get
            Return _NewPwd
        End Get
        Set(ByVal value As String)
            _NewPwd = value
        End Set
    End Property
End Class

