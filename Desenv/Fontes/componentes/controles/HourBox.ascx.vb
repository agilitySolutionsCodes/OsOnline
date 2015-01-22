Imports System.Globalization
<ValidationProperty("Text")> Partial Public Class HourBox
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Public Property Text() As String
        Get
            Return txtHour.Text
        End Get
        Set(ByVal value As String)
            txtHour.Text = value
        End Set
    End Property

    Public Property Width() As WebControls.Unit
        Get
            Return txtHour.Width
        End Get
        Set(ByVal value As WebControls.Unit)
            txtHour.Width = value
        End Set
    End Property

    Public Property CssClass() As String
        Get
            Return txtHour.CssClass
        End Get
        Set(ByVal value As String)
            txtHour.CssClass = value
        End Set
    End Property

    Public Sub Clear()
        txtHour.Text = ""
    End Sub

    Public Property Enabled() As Boolean
        Get
            Return txtHour.Enabled
        End Get
        Set(ByVal value As Boolean)
            txtHour.Enabled = value
        End Set
    End Property

    Public Property [ReadOnly]() As Boolean
        Get
            Return txtHour.ReadOnly
        End Get
        Set(ByVal value As Boolean)
            txtHour.ReadOnly = value
        End Set
    End Property

    Public ReadOnly Property IsValid() As Boolean
        Get
            If txtHour.Text.Trim.Length = 5 Then
                Return (CInt(txtHour.Text.Trim.Substring(0, 2)) <= 23) AndAlso (CInt(txtHour.Text.Trim.Substring(3, 2)) <= 59)
            Else
                Return False
            End If
        End Get
    End Property

    Public ReadOnly Property IsEmpty() As Boolean
        Get
            Return (txtHour.Text.Trim.Length = 0)
        End Get
    End Property

End Class