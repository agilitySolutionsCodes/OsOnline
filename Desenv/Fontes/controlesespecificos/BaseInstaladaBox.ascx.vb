Public Class BaseInstaladaBox
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Public Property Serie() As String
        Get
            Return lblSerie.Text.Trim
        End Get
        Set(ByVal value As String)
            lblSerie.Text = value.Trim
        End Set
    End Property

    Public Property Nome() As String
        Get
            Return lblNome.Text.Trim
        End Get
        Set(ByVal value As String)
            lblNome.Text = value.Trim
        End Set
    End Property

    Public Property Garantia() As String
        Get
            Return lblGarantia.Text.Trim
        End Get
        Set(ByVal value As String)
            lblGarantia.Text = value.Trim
        End Set
    End Property

    Public Property Codigo() As String
        Get
            Return lblCodigo.Text.Trim
        End Get
        Set(ByVal value As String)
            lblCodigo.Text = value.Trim
        End Set
    End Property


    Public Property CssClass() As String
        Get
            Return lblSerie.CssClass
        End Get
        Set(ByVal value As String)
            lblSerie.CssClass = value
            lblNome.CssClass = value
            lblGarantia.CssClass = value
        End Set
    End Property

    Public ReadOnly Property Text() As String
        Get
            Return lblSerie.Text.Trim + " " + lblNome.Text.Trim + " " + lblGarantia.Text.Trim
        End Get
    End Property

    Public Shadows ReadOnly Property ToString() As String
        Get
            Return lblSerie.Text.Trim + " " + lblNome.Text.Trim + " " + lblGarantia.Text.Trim
        End Get
    End Property

End Class