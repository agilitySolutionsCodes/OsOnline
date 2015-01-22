Public Class AcessorioView
    Inherits System.Web.UI.UserControl

    Public Property Codigo() As String
        Get
            Return txtCodigo.Text
        End Get
        Set(ByVal value As String)
            txtCodigo.Text = value
            PreencherCodigo()
        End Set
    End Property

    Public Property Descricao() As String
        Get
            Return lblDescricao.Text
        End Get
        Set(ByVal value As String)
            lblDescricao.Text = value
        End Set
    End Property

    Public Property Enabled() As Boolean
        Get
            Return pnlAcessorio.Enabled
        End Get
        Set(ByVal value As Boolean)
            pnlAcessorio.Enabled = value
        End Set
    End Property

    Public Property SomenteLeitura() As Boolean
        Get
            Return txtCodigo.ReadOnly
        End Get
        Set(ByVal value As Boolean)
            txtCodigo.ReadOnly = value
            btnBusca.Enabled = Not value
        End Set
    End Property

    Private Sub PreencherCodigo()
        Dim cn As New ctlProduto
        Dim dt As DataTable
        dt = cn.SelecionarCodigo(Codigo)
        If dt.Rows.Count > 0 Then
            Descricao = dt.Rows(0)("Nome").ToString
            txtCodigo.Text = dt.Rows(0)("Codigo").ToString
        Else
            LimparCampos()
        End If
    End Sub

    Private Sub LimparCampos()
        lblDescricao.Text = ""
        txtCodigo.Text = ""
    End Sub

    Private Sub btnBusca_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBusca.Click
        PreencherCodigo()
    End Sub

End Class