Imports System.Data.SqlClient

Public Class DetalheBaseInstalada
    Inherits BaseWebUI

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                DirectCast(Me.Master.Controls(0).Controls(3).Controls(7).FindControl("menuOsOnline"), Menu).Visible = False
                NomeBotoes()
                PreencherTecnicos()
                PreencherStatus()
                If Request("Serie") Is Nothing Then
                    HabilitarModo("I")
                    Incluir()
                Else
                    HabilitarModo("A")
                    Selecionar(Request("Serie").Trim)
                End If
                HabilitarLeitura(True)
            End If
        Catch ex As Exception
            Dim oRet As New ctlRetornoGenerico
            ctlUtil.EscreverLogErro("DetalheBaseInstalada - Page_load: " & ex.Message())
            oRet.Mensagem = ctlUtil.sMsgErroPadrao

            oRet.Sucesso = False
            oMensagem.SetMessage(oRet)
        End Try
    End Sub

    Private Sub Incluir()
        Dim dt As DataTable = ObterEstrutura()
        Preencher(dt)
        pnlCabecalho.Enabled = True
    End Sub

    Private Function ObterEstrutura(Optional ByVal bAdicionarLinha As Boolean = True) As DataTable
        Dim dt As New DataTable
        dt.Columns.Add("Filial")
        dt.Columns.Add("SerieEquipamento")
        dt.Columns.Add("NomeEquipamento")
        dt.Columns.Add("CodigoEquipamento")
        dt.Columns.Add("LoteEquipamento")
        dt.Columns.Add("CodigoStatus")
        dt.Columns.Add("CodigoCliente")
        dt.Columns.Add("NomeCliente")
        dt.Columns.Add("CnpjCliente")
        dt.Columns.Add("LojaCliente")
        dt.Columns.Add("CodigoTecnico")
        dt.Columns.Add("NotaFiscal")
        dt.Columns.Add("SerieNotaFiscal")
        dt.Columns.Add("Pedido")
        dt.Columns.Add("ItemPedido")
        dt.Columns.Add("DataInstalacao")
        dt.Columns.Add("DataVenda")
        dt.Columns.Add("DataGarantia")
        dt.Columns.Add("LocalInstalacao")
        dt.Columns.Add("Endereco")
        dt.Columns.Add("Telefone")
        dt.Columns.Add("Contato")
        dt.Columns.Add("Acessorio")
        dt.Columns.Add("Serie")
        dt.Columns.Add("Lote")
        dt.Columns.Add("CodigoTecnico")
        If bAdicionarLinha Then
            AdicionarItem(dt)
        End If
        Return dt
    End Function

    Private Sub Selecionar(ByVal sNumero As String)
        Dim cn As New ctlBaseInstalada
        Dim reader As SqlDataReader = cn.Listar(CType(0, ctlBaseInstalada.TipoPesquisa), sNumero)
        If Not reader.HasRows() Then
            Throw New Exception("A Ordem de Serviço '" + sNumero + "' não foi localizada.")
        Else
            Dim dt As New DataTable
            If reader IsNot Nothing Then
                dt.Load(reader)
                reader.Close()
            End If
            Preencher(dt)
        End If
    End Sub

    Private Sub Preencher(Optional ByVal dt As DataTable = Nothing)
        grdItens.DataSource = dt
        grdItens.DataBind()
        txtSerie.Text = dt.Rows(0).Item("SerieEquipamento").ToString
        lblProduto.text = dt.Rows(0).Item("CodigoEquipamento").ToString & " - " & dt.Rows(0).Item("NomeEquipamento").ToString
        txtLote.Text = dt.Rows(0).Item("LoteEquipamento").ToString
        If Not String.IsNullOrEmpty(dt.Rows(0).Item("CodigoStatus").ToString) Then
            drpStatus.SelectedValue = dt.Rows(0).Item("CodigoStatus").ToString
        End If
        txtEndereco.Text = dt.Rows(0).Item("Endereco").ToString
        txtCidade.Text = dt.Rows(0).Item("Cidade").ToString
        lblEstado.Text = dt.Rows(0).Item("Estado").ToString & " - " & dt.Rows(0).Item("SiglaEstado").ToString
        txtCliente.Codigo = dt.Rows(0).Item("CodigoCliente").ToString
        txtCliente.Loja = dt.Rows(0).Item("LojaCliente").ToString
        txtCliente.Nome = dt.Rows(0).Item("NomeCliente").ToString
        txtCliente.CPF_CNPJ = dt.Rows(0).Item("CnpjCliente").ToString
        If Not String.IsNullOrEmpty(dt.Rows(0).Item("CodigoTecnico").ToString) Then
            drpTecnico.SelectedValue = dt.Rows(0).Item("CodigoTecnico").ToString
        End If
        txtNotaFiscal.Text = dt.Rows(0).Item("NotaFiscal").ToString
        txtSerieNota.Text = dt.Rows(0).Item("SerieNotaFiscal").ToString
        txtPedido.Text = dt.Rows(0).Item("Pedido").ToString
        txtItemPedido.Text = dt.Rows(0).Item("ItemPedido").ToString
        If Not String.IsNullOrEmpty(dt.Rows(0).Item("DataInstalacao").ToString) Then
            If CDate("1/1/1900") <> CDate(dt.Rows(0).Item("DataInstalacao")) Then
                oDataInstalacao.Text = CDate(dt.Rows(0).Item("DataInstalacao")).ToShortDateString
            End If
        End If
        If Not String.IsNullOrEmpty(dt.Rows(0).Item("DataVenda").ToString) Then
            oDataVenda.Text = CDate(dt.Rows(0).Item("DataVenda")).ToShortDateString
        End If
        If Not String.IsNullOrEmpty(dt.Rows(0).Item("DataGarantia").ToString) Then
            oDataGarantia.Text = CDate(dt.Rows(0).Item("DataGarantia")).ToShortDateString
        End If
        txtLocalInstalacao.Text = dt.Rows(0).Item("LocalInstalacao").ToString
        txtContato.Text = dt.Rows(0).Item("Contato").ToString
        txtTelefone.Text = dt.Rows(0).Item("Telefone").ToString
        If Not String.IsNullOrEmpty(dt.Rows(0).Item("CodigoRevisaoFabrica").ToString) Then
            drpRevisaoFabrica.SelectedValue = dt.Rows(0).Item("CodigoRevisaoFabrica").ToString
        End If
        ViewState.Add("BaseInstalada", dt)
    End Sub

    Private Sub Recarregar(ByVal sNumero As String)
        Selecionar(sNumero)
    End Sub

    Private Sub AdicionarItem(ByRef dt As DataTable, Optional ByVal nIndice As Integer = 0)
        Dim nItem As Integer = 0
        Dim dr As DataRow = dt.NewRow
        dr("Filial") = ""
        dr("SerieEquipamento") = ""
        dr("NomeEquipamento") = ""
        dr("CodigoEquipamento") = ""
        dr("LoteEquipamento") = ""
        dr("CodigoStatus") = ""
        dr("Endereco") = ""
        dr("CodigoCliente") = ""
        dr("NomeCliente") = ""
        dr("CnpjCliente") = ""
        dr("LojaCliente") = ""
        dr("CodigoTecnico") = ""
        dr("NotaFiscal") = ""
        dr("SerieNotaFiscal") = ""
        dr("Pedido") = ""
        dr("ItemPedido") = ""
        dr("DataInstalacao") = Now.Date
        dr("DataVenda") = Now.Date
        dr("DataGarantia") = Now.Date
        dr("LocalInstalacao") = ""
        dr("Endereco") = ""
        dr("Telefone") = ""
        dr("Contato") = ""
        dr("Acessorio") = ""
        dr("Serie") = ""
        dr("Lote") = ""
        dr("CodigoRevisaoFabrica") = ""
        If nIndice = 0 Then
            dt.Rows.Add(dr)
        Else
            dt.Rows.InsertAt(dr, nIndice)
        End If
    End Sub

    Private Sub grdItens_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdItens.RowCommand
        Dim gv As GridView = DirectCast(sender, GridView)
        Dim dt As DataTable = DirectCast(ViewState("BaseInstalada"), DataTable)
        If e.CommandName = "Busca" Or e.CommandName = "" Then
            '
        Else
            Dim nIndice As Integer = Integer.Parse(e.CommandArgument.ToString)
            Select Case e.CommandName
                Case "New"
                    nIndice = nIndice + 1
                    AdicionarItem(dt, nIndice)
                    grdItens.EditIndex = nIndice
                    grdItens.SelectedIndex = nIndice
                Case "Edit"
                    grdItens.EditIndex = nIndice
                    grdItens.SelectedIndex = nIndice
                Case "Update"
                    AtualizarItem(grdItens.Rows(nIndice), nIndice)
                    grdItens.EditIndex = -1
                Case "Cancel"
                    'se for alteracao, basta cacelar. se for inclusao, eh necessário remover a linha
                    grdItens.EditIndex = -1
                    'If dt.Rows(0)("Item").ToString = "" Then
                    'dt.Rows.RemoveAt(nIndice)
                    'End If
                Case "Delete"
                    dt.Rows.RemoveAt(nIndice)
                    If dt.Rows.Count = 0 Then
                        AdicionarItem(dt)
                    End If
            End Select
            ViewState.Add("BaseInstalada", dt)
            grdItens.DataSource = dt
            grdItens.DataBind()
        End If
    End Sub

    Private Sub AtualizarItem(ByVal gr As GridViewRow, ByVal nIndice As Integer)
        Dim sCodigoProduto As String = ""
        Dim sNomeProduto As String = ""
        Dim oAcessorio = DirectCast(gr.FindControl("oAcessorio"), AcessorioView)
        Dim txtSerie = DirectCast(gr.FindControl("txtSerie"), TextBox)
        Dim txtLote = DirectCast(gr.FindControl("txtLote"), TextBox)
        Dim dt As DataTable
        dt = DirectCast(ViewState("BaseInstalada"), DataTable)
        Dim dr As DataRow = dt.NewRow()
        dr.ItemArray = dt.Rows(nIndice).ItemArray
        'dr = dt.Rows(nIndice)
        'dr.BeginEdit()
        dr("Acessorio") = oAcessorio.Codigo
        dr("Serie") = txtSerie.Text
        dr("Lote") = txtLote.Text
        dt.Rows.RemoveAt(nIndice)
        dt.Rows.InsertAt(dr, nIndice)
        dt.AcceptChanges()
    End Sub

    Private Sub gridItens_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles grdItens.RowDeleting
    End Sub

    Private Sub gridItens_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles grdItens.RowEditing
    End Sub

    Private Sub gridItens_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles grdItens.RowUpdating
    End Sub

    Private Sub gridItens_RowCancelingEdit(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCancelEditEventArgs) Handles grdItens.RowCancelingEdit
    End Sub

    Protected Sub oProduto_SelecionarOnClick(ByVal obj As Object)

    End Sub

    Private Sub btnIncluir_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnIncluir.Click
        Dim oRet As New ctlRetornoGenerico
        Try
            Dim ct As New ctlBaseInstalada
            Dim sUsuario As String = HttpContext.Current.User.Identity.Name
            oRet = Validar()
            If oRet.Sucesso Then
                Dim dt As DataTable = DirectCast(ViewState("BaseInstalada"), DataTable)
                Dim oBaseInstalada As New ctlBaseInstalada()
                Dim oCliente As New ctlCliente(txtCliente.Codigo, txtCliente.Loja, txtCliente.Nome, txtCliente.CPF_CNPJ)
                oBaseInstalada.Cliente = oCliente
                oBaseInstalada.SerieEquipamento = txtSerie.Text.Trim
                'oBaseInstalada.CodigoEquipamento = oProduto.Codigo.Trim
                oBaseInstalada.LoteEquipamento = txtLote.Text.Trim
                oBaseInstalada.CodigoStatus = drpStatus.SelectedValue
                oBaseInstalada.Endereco = txtEndereco.Text.Trim
                oBaseInstalada.CodigoTecnico = drpTecnico.SelectedValue
                oBaseInstalada.NotaFiscal = txtNotaFiscal.Text.Trim
                oBaseInstalada.SerieNota = txtSerieNota.Text.Trim
                oBaseInstalada.Pedido = txtPedido.Text.Trim
                oBaseInstalada.ItemPedido = txtItemPedido.Text.Trim
                oBaseInstalada.DataVenda = CDate(oDataVenda.Text.Trim)
                oBaseInstalada.DataGarantia = CDate(oDataGarantia.Text.Trim)
                oBaseInstalada.LocalInstalacao = txtLocalInstalacao.Text.Trim
                oBaseInstalada.Contato = txtContato.Text.Trim
                oBaseInstalada.Telefone = txtTelefone.Text.Trim
                oBaseInstalada.Email = txtEmail.Text.Trim
                oBaseInstalada.Mensagem = txtMensagem.Text.Trim
                oBaseInstalada.PutItens(dt)
                'oRet = ct.Incluir(oBaseInstalada)
                If oRet.Sucesso Then
                    Recarregar(oRet.Chave.Substring(2))
                End If
            End If
        Catch ex As Exception
            ctlUtil.EscreverLogErro("DetalheBaseInstalada - btnIncluir_click: " & ex.Message())
            oRet.Mensagem = ctlUtil.sMsgErroPadrao

            oRet.Sucesso = False

        End Try
        oMensagem.SetMessage(oRet)
    End Sub

    Private Function Validar() As ctlRetornoGenerico
        Dim oRet As New ctlRetornoGenerico
        Return oRet
    End Function

    Private Function ValidarItens(ByVal oRet As ctlRetornoGenerico) As Boolean
        Dim bRet As Boolean = False
        Return bRet
    End Function

    Private Sub TituloPagina(ByVal sTexto As String)
        DirectCast(Me.Master.Controls(0).Controls(3).Controls(7).FindControl("oBarraUsuario"), BarraUsuario).PaginaAtual = sTexto
    End Sub

    Private Sub NomeBotoes()
        btnIncluir.NomePainelMensagem = oMensagem.ClientID
        btnIncluir.NomePainelBotoes = pnlBotoes.ClientID
        btnAlterar.NomePainelMensagem = oMensagem.ClientID
        btnAlterar.NomePainelBotoes = pnlBotoes.ClientID
    End Sub

    Private Sub __gridItens_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        Dim gv As GridView = DirectCast(sender, GridView)
        Dim dt As DataTable = DirectCast(ViewState("BaseInstalada"), DataTable)
        'If e.CommandName = "Busca" Or e.CommandName = "" Then
        '    '
        'Else
        '    Dim nIndice As Integer = Integer.Parse(e.CommandArgument.ToString)
        '    Select Case e.CommandName
        '        Case "New"
        '            nIndice = nIndice + 1
        '            AdicionarItem(dt, nIndice)
        '            gridItens.EditIndex = nIndice
        '            gridItens.SelectedIndex = nIndice
        '        Case "Edit"
        '            gridItens.EditIndex = nIndice
        '            gridItens.SelectedIndex = nIndice
        '        Case "Update"
        '            AtualizarItem(gridItens.Rows(nIndice), nIndice)
        '            gridItens.EditIndex = -1
        '        Case "Cancel"
        '            'se for alteracao, basta cacelar. se for inclusao, eh necessário remover a linha
        '            gridItens.EditIndex = -1
        '            'If dt.Rows(0)("Item").ToString = "" Then
        '            'dt.Rows.RemoveAt(nIndice)
        '            'End If
        '        Case "Delete"
        '            dt.Rows.RemoveAt(nIndice)
        '            If dt.Rows.Count = 0 Then
        '                AdicionarItem(dt)
        '            End If
        '    End Select
        ViewState.Add("BaseInstalada", dt)
        grdItens.DataSource = dt
        grdItens.DataBind()
        'End If
    End Sub

    Private Sub PreencherTecnicos()
        Dim reader As SqlDataReader
        Dim ct As New ctlAtendimento
        reader = ct.ListarTecnicos
        drpTecnico.DataSource = reader
        drpTecnico.DataBind()
        drpTecnico.Items.Insert(0, "Nenhum")
        reader.Close()
    End Sub

    Private Sub PreencherStatus()
        Dim reader As SqlDataReader
        Dim ct As New ctlBaseInstalada
        reader = ct.ListarStatus
        drpStatus.DataSource = reader
        drpStatus.DataBind()
        drpStatus.Items.Insert(0, "Nenhum")
        reader.Close()
    End Sub

    Private Sub btnVoltar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnVoltar.Click
        Response.Redirect("ConsultaBaseInstalada.aspx")
    End Sub

    Private Sub HabilitarLeitura(ByVal valor As Boolean)
        txtSerie.ReadOnly = valor
        'oProduto.SomenteLeitura = valor
        txtLote.ReadOnly = valor
        drpStatus.Enabled = Not valor
        txtEndereco.ReadOnly = valor
        txtCliente.SomenteLeitura = valor
        drpTecnico.Enabled = Not valor
        txtNotaFiscal.ReadOnly = valor
        txtSerieNota.ReadOnly = valor
        txtPedido.ReadOnly = valor
        txtItemPedido.ReadOnly = valor
        oDataInstalacao.ReadOnly = valor
        oDataVenda.ReadOnly = valor
        oDataGarantia.ReadOnly = valor
        txtLocalInstalacao.ReadOnly = valor
        txtContato.ReadOnly = valor
        txtTelefone.ReadOnly = valor
        drpRevisaoFabrica.Enabled = Not valor
    End Sub

    Private Sub HabilitarModo(ByVal codigo As String)
        If codigo = "I" Then
            TituloPagina("Base Instalada - Incluir")
            'Botões
            btnIncluir.Visible = False
            btnAlterar.Visible = False
        ElseIf codigo = "A" Then
            TituloPagina("Base Instalada - Visualizar")
            'Botões
            btnIncluir.Visible = False
            btnAlterar.Visible = False
        End If
    End Sub



















End Class