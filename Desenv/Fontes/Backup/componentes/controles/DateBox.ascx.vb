Imports System.Globalization
<ValidationProperty("Text")> Partial Public Class DateBox
    Inherits System.Web.UI.UserControl

    Public Property Text() As String
        Get
            Return txtDateBox.Text
        End Get
        Set(ByVal value As String)
            txtDateBox.Text = value
        End Set
    End Property

    Public Property Width() As WebControls.Unit
        Get
            Return txtDateBox.Width
        End Get
        Set(ByVal value As WebControls.Unit)
            txtDateBox.Width = value
        End Set
    End Property

    Public Property CssClass() As String
        Get
            Return txtDateBox.CssClass
        End Get
        Set(ByVal value As String)
            txtDateBox.CssClass = value
        End Set
    End Property

    Public Function GetDate() As DateTime
        Dim dView As DateTime
        DateTime.TryParseExact(Text.ToString(), "dd/MM/yyyy", New CultureInfo("pt-BR"), DateTimeStyles.None, dView)
        Return dView
    End Function

    Public Sub Clear()
        txtDateBox.Text = ""
    End Sub

    Public Property Enabled() As Boolean
        Get
            Return txtDateBox.Enabled
        End Get
        Set(ByVal value As Boolean)
            txtDateBox.Enabled = value
        End Set
    End Property

    Public Property [ReadOnly]() As Boolean
        Get
            Return txtDateBox.ReadOnly
        End Get
        Set(ByVal value As Boolean)
            txtDateBox.ReadOnly = value
        End Set
    End Property

    Public ReadOnly Property IsValid() As Boolean
        Get
            Return IsDate(txtDateBox.Text)
        End Get
    End Property

    Public ReadOnly Property IsEmpty() As Boolean
        Get
            Return (txtDateBox.Text.Trim.Length = 0)
        End Get
    End Property

End Class
