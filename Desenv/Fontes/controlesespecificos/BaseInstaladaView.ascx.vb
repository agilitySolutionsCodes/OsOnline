Public Class BaseInstaladaView
    Inherits System.Web.UI.UserControl

    Public Event SelecionarOnClick(ByVal oSelecao As Object)
    Public Event PaginarOnClick()
    Public Event CancelarOnClick()
    Public Event FecharOnClick()
    Public Event PesquisarOnClick()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Public Property Serie() As String
        Get
            Return txtSerie.Text
        End Get
        Set(ByVal value As String)
            txtSerie.Text = value
            lblSerie.Text = value
        End Set
    End Property

    Public Property Codigo() As String
        Get
            Return lblCodigo.Text
        End Get
        Set(ByVal value As String)
            lblCodigo.Text = value
        End Set
    End Property


    Public Property Nome() As String
        Get
            Return lblNome2.Text
        End Get
        Set(ByVal value As String)
            lblNome2.Text = value
            lblNome.Text = value
        End Set
    End Property

    Public Property Garantia() As String
        Get
            Return lblGarantia.Text
        End Get
        Set(ByVal value As String)
            lblGarantia.Text = value
            lblGarantia2.Text = value
        End Set
    End Property


    Public Property HabilitarEdicao() As Boolean
        Get
            Return pnlEdicao.Visible
        End Get
        Set(ByVal value As Boolean)
            pnlEdicao.Visible = value
            pnlVisualizacao.Visible = Not pnlEdicao.Visible
        End Set
    End Property

    Public Property CssClass() As String
        Get
            Return txtSerie.CssClass
        End Get
        Set(ByVal value As String)
            txtSerie.CssClass = value
            lblNome2.CssClass = value
            lblCodigo.CssClass = value
            lblNome.CssClass = value
            lblSerie.CssClass = value
            lblGarantia.CssClass = value
            lblGarantia2.CssClass = value
        End Set
    End Property

    Public ReadOnly Property Text() As String
        Get
            Return txtSerie.Text
        End Get
    End Property

    Public Sub LimparCampos()
        lblNome.Text = ""
        lblNome2.Text = ""
        lblSerie.Text = ""
        lblCodigo.Text = ""
        lblGarantia.Text = ""
        lblGarantia2.Text = ""
    End Sub

    Public Property Enabled() As Boolean
        Get
            Return txtSerie.Enabled
        End Get
        Set(ByVal value As Boolean)
            txtSerie.Enabled = value
            lblNome2.Enabled = value
            lblCodigo.Enabled = value
            lblSerie.Enabled = value
            lblNome.Enabled = value
            lblGarantia.Enabled = value
            lblGarantia2.Enabled = value
            btnBusca.Visible = value
        End Set
    End Property

    Protected Sub btnBusca_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnBusca.Click
        oDetalhe.Exibir()
    End Sub

    Private Sub oConsulta_SelecionarOnClick(ByVal oSelecao As Object) Handles oDetalhe.SelecionarOnClick
        Dim dr As DataRow
        dr = DirectCast(oSelecao, DataRow)
        txtSerie.Text = Replace(dr("Serie").ToString, "&#186;", "º")
        lblSerie.Text = Replace(dr("Serie").ToString, "&#186;", "º")
        lblCodigo.Text = dr("Codigo").ToString
        lblNome2.Text = dr("Produto").ToString
        lblNome.Text = dr("Produto").ToString
        lblGarantia.Text = dr("DataGarantia").ToString
        lblGarantia2.Text = lblGarantia.Text
        GravaCliente(dr)
        updCabecalho.Update()
        oDetalhe.FecharJanela()
        lblFound.Text = (True).ToString
        imgNotFound.Visible = Not Boolean.Parse(lblFound.Text)
        RaiseEvent SelecionarOnClick(oSelecao)
    End Sub

    Private Sub GravaCliente(ByVal dr As DataRow)
        If HttpContext.Current.Session("CodigoCliente") Is Nothing Or String.IsNullOrEmpty(HttpContext.Current.Session("CodigoCliente").ToString) Then
            If HttpContext.Current.Session("NovoItem") IsNot Nothing And HttpContext.Current.Session("NovoItem").ToString = "S" Then
                Dim oCliente As New ctlCliente
                oCliente.Codigo = dr("CodigoCliente").ToString
                oCliente.CNPJ = dr("CNPJ").ToString
                oCliente.Loja = dr("Loja").ToString
                oCliente.Nome = dr("Cliente").ToString
                Session.Add("oCliente", oCliente)
                Session.Remove("NovoItem")
            End If
        End If
    End Sub

    Protected Sub txtSerie_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtSerie.TextChanged
        Dim sSerie As String = DirectCast(sender, TextBox).Text.Trim
        If sSerie.Length > 0 Then
            Dim cn As New ctlBaseInstalada
            Dim dt As DataTable
            dt = cn.Pesquisar(CType(0, ctlBaseInstalada.TipoPesquisa), sSerie)
            lblFound.Text = (False).ToString
            Dim dr As DataRow
            LimparCampos()
            If dt.Rows.Count > 0 Then
                dr = dt.Rows(0)
                lblFound.Text = (True).ToString
                lblSerie.Text = dr("Serie").ToString
                lblCodigo.Text = dr("Codigo").ToString
                lblNome2.Text = dr("Produto").ToString
                lblNome.Text = dr("Produto").ToString
                If dr("DataGarantia").ToString.Contains("01/01/1900") Then
                    lblGarantia.Text = "(" & dr("DescrGarantia").ToString & ")"
                    lblGarantia2.Text = lblGarantia.Text
                Else
                    lblGarantia.Text = "(" & Left(dr("DataGarantia").ToString, 10) & dr("DescrGarantia").ToString & ")"
                    lblGarantia2.Text = lblGarantia.Text
                End If

                GravaCliente(dr)
            Else
                dr = dt.NewRow()
            End If
            imgNotFound.Visible = Not Boolean.Parse(lblFound.Text)
            RaiseEvent SelecionarOnClick(dr)
        End If
    End Sub

    Public ReadOnly Property IsValid() As Boolean
        Get
            If txtSerie.Text.Trim.Length > 0 And lblNome.Text.Trim.Length > 0 Then
                Return True
            Else
                Return False
            End If
        End Get
    End Property

End Class