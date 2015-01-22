Imports System.Globalization
<ValidationProperty("Text")> Partial Public Class PhoneBox
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Public Property Text() As String
        Get
            Return txtPhone.Text
        End Get
        Set(ByVal value As String)
            txtPhone.Text = value
        End Set
    End Property

    Public Property Width() As WebControls.Unit
        Get
            Return txtPhone.Width
        End Get
        Set(ByVal value As WebControls.Unit)
            txtPhone.Width = value
        End Set
    End Property

    Public Property CssClass() As String
        Get
            Return txtPhone.CssClass
        End Get
        Set(ByVal value As String)
            txtPhone.CssClass = value
        End Set
    End Property

    Public Sub Clear()
        txtPhone.Text = ""
    End Sub

    Public Property Enabled() As Boolean
        Get
            Return txtPhone.Enabled
        End Get
        Set(ByVal value As Boolean)
            txtPhone.Enabled = value
        End Set
    End Property

    Public Property [ReadOnly]() As Boolean
        Get
            Return txtPhone.ReadOnly
        End Get
        Set(ByVal value As Boolean)
            txtPhone.ReadOnly = value
        End Set
    End Property

    Public ReadOnly Property IsValid() As Boolean
        Get
            Return (txtPhone.Text.Trim.Length >= 10)
        End Get
    End Property

    Public ReadOnly Property IsEmpty() As Boolean
        Get
            Return (txtPhone.Text.Trim.Length = 0)
        End Get
    End Property

End Class