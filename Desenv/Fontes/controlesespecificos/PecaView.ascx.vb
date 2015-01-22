Public Class PecaView
    Inherits System.Web.UI.UserControl

    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            imgNotFound.Visible = False
        End If
    End Sub

    Public Property Codigo() As String
        Get
            Return lblCodigo.Text
        End Get
        Set(ByVal value As String)
            lblCodigo.Text = value.Trim
        End Set
    End Property

    Public Property Serie() As String
        Get
            Return txtSerie.Text
        End Get
        Set(ByVal value As String)
            txtSerie.Text = value.Trim
        End Set
    End Property

    Public Property Descricao() As String
        Get
            Return lblDescricao.Text
        End Get
        Set(ByVal value As String)
            lblDescricao.Text = value.Trim
        End Set
    End Property

    Public Property Enabled() As Boolean
        Get
            Return pnlProdutoSerie.Enabled
        End Get
        Set(ByVal value As Boolean)
            pnlProdutoSerie.Enabled = value
        End Set
    End Property

    Public Property HabilitarDescricao() As Boolean
        Get
            Return lblDescricao.Visible
        End Get
        Set(ByVal value As Boolean)
            lblDescricao.Visible = value
        End Set
    End Property

    Public Property isValid() As Boolean
        Get
            Return CBool(lblFound.Text)
        End Get
        Set(ByVal value As Boolean)
            lblFound.Text = value.ToString
            imgNotFound.Visible = Not value
        End Set
    End Property

    Private Sub PreencherSerie()
        Dim cn As New ctlProduto
        Dim dt As DataTable
        dt = cn.SelecionarNumeroSerie(Serie)
        If dt.Rows.Count > 0 Then
            Descricao = dt.Rows(0)("DescricaoProduto").ToString.Trim
            Codigo = dt.Rows(0)("CodigoProduto").ToString.Trim
        Else
            LimparCampos()
            If Not String.IsNullOrEmpty(Serie) AndAlso String.IsNullOrEmpty(Codigo) Then
                isValid = False
            Else
                isValid = True
            End If
        End If
    End Sub

    Private Sub LimparCampos()
        lblDescricao.Text = ""
        lblCodigo.Text = ""
    End Sub

    Private Sub btnBusca_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBusca.Click
        PreencherSerie()
    End Sub

End Class