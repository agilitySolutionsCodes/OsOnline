Imports System.Data.SqlClient
Imports System.IO
Imports System.Xml

Public Class DetalheAtendimento
    Inherits BaseWebUI

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                Dim oAtendimento As New ctlAtendimento
                Dim oRet As New ctlRetornoGenerico

                DirectCast(Me.Master.Controls(0).Controls(3).Controls(7).FindControl("menuOsOnline"), Menu).Visible = False
                NomeBotoes()
                'PreencherOcorrencias()
                If Request("Numero") IsNot Nothing Then
                    If Session("Atendimento") IsNot Nothing Then 'inclusao
                        Dim vAtendimento() As String = DirectCast(Session("Atendimento"), String())
                        ReDim Preserve vAtendimento(5)
                        hdnDtEmissaoOS.Value = vAtendimento(5)
                        PreencherOcorrencias(vAtendimento(5))
                        PreencherCombos(HttpContext.Current.Session("Regiao").ToString)
                        oDataInicio.Text = CStr(Date.Now)
                        oDataTermino.Text = CStr(Date.Now)
                        lblSerie.Text = vAtendimento(0)
                        Incluir(vAtendimento(4))
                        lblNumero.Text = Request("Numero").Substring(0, 8)
                        lblSequencia.Text = Request("Numero").Substring(8, 2)
                        btnVoltar.CommandArgument = lblNumero.Text.Trim.Substring(0, 6)
                        txtModoAtendimento.Value = "I"
                        drpOcorrencia.SelectedValue = vAtendimento(1)
                        'valida garantia da base instalada
                        ValidaGarantia(vAtendimento(2))
                        lblVersaoSoft.Text = vAtendimento(3)
                        Session.Remove("Atendimento")

                    Else 'alteracao
                        PreencherCombos()
                        Selecionar(Request("Numero").Trim)
                        lblNumero.Text = Request("Numero").Substring(0, 8)
                        lblSequencia.Text = Request("Numero").Substring(8, 2)
                        txtModoAtendimento.Value = "A"
                    End If
                    If Not oAtendimento.VerificaVersaoSoftware(lblSerie.Text) Then
                        lblMsgAtualizarVersao.Visible = True
                    End If
                End If
            End If
        Catch ex As Exception
            Dim oRet As New ctlRetornoGenerico
            ctlUtil.EscreverLogErro("DetalheAtendimento - Page_load: " & ex.Message())
            oRet.Mensagem = ctlUtil.sMsgErroPadrao
            oMensagem.SetMessage(oRet)
        End Try
    End Sub

    Private Sub Incluir(ByVal CodigoEtapa As String)
        Dim dt As New DataTable
        dt = ObterEstrutura()
        Dim dtEtapa As DataTable
        dtEtapa = ObterEstruturaEtapa()
        'necessario este preenchimento pq os dados de carregamento inicial vem da ordem de servico
        dtEtapa.Rows(0).Item("Item") = ObterIndiceEtapa()
        dtEtapa.Rows(0).Item("CodigoEtapa") = CodigoEtapa
        Preencher(dt, dtEtapa)
        pnlCabecalho.Enabled = True
    End Sub

    Private Function ObterEstrutura(Optional ByVal bAdicionarLinha As Boolean = True) As DataTable

        Dim dt As New DataTable

        dt.Columns.Add("Filial")
        dt.Columns.Add("NumeroOS")
        dt.Columns.Add("Sequencia")
        dt.Columns.Add("Item")
        dt.Columns.Add("hdnAlteracaoItem")
        dt.Columns.Add("chkEnviaAnalise")

        dt.Columns.Add("NumeroSerieProduto")
        dt.Columns.Add("TipoAtendimento")
        dt.Columns.Add("CodigoProduto")
        dt.Columns.Add("DescricaoProduto")
        dt.Columns.Add("CodigoTecnico")
        dt.Columns.Add("Comentario")

        dt.Columns.Add("NomeTecnico")
        dt.Columns.Add("DataInicio")
        dt.Columns.Add("HoraInicio")
        dt.Columns.Add("DataTermino")
        dt.Columns.Add("HoraTermino")
        dt.Columns.Add("CodigoOcorrencia")
        dt.Columns.Add("DescricaoOcorrencia")
        dt.Columns.Add("CodigoGarantia")
        dt.Columns.Add("DescricaoGarantia")
        dt.Columns.Add("CodigoStatus")
        dt.Columns.Add("DescricaoStatus")
        dt.Columns.Add("HorasFaturadas")
        dt.Columns.Add("Translado")
        dt.Columns.Add("CodigoIncluirItemOS")
        dt.Columns.Add("DescricaoIncluirItemOS")
        dt.Columns.Add("ROTAprovado")
        dt.Columns.Add("Versao")
        dt.Columns.Add("VersaoAtualizada")

        dt.Columns.Add("CodDefeito")
        dt.Columns.Add("CodServico")
        dt.Columns.Add("DescricaoROT")
        dt.Columns.Add("DefeitoConstatado")
        dt.Columns.Add("CausaProvavel")
        dt.Columns.Add("ServicoExecutado")
        dt.Columns.Add("BloqueioAtendimento")
        dt.Columns.Add("Aprovacao2")
        dt.Columns.Add("DescAprov2")

        dt.Columns.Add("CodigoItem")
        dt.Columns.Add("DescricaoItem")
        dt.Columns.Add("NumeroSeriePeca")
        dt.Columns.Add("Quantidade")
        dt.Columns.Add("NumeroLote")
        dt.Columns.Add("CodigoFabricante")
        dt.Columns.Add("D_E_L_E_T_")

        If bAdicionarLinha Then
            AdicionarItem(dt)
        End If

        Return dt

    End Function

    Private Function ObterEstruturaEtapa(Optional ByVal bAdicionarLinha As Boolean = True) As DataTable

        Dim dtEtapa As New DataTable

        dtEtapa.Columns.Add("Item")
        dtEtapa.Columns.Add("CodigoEtapa")
        dtEtapa.Columns.Add("Descricao")
        dtEtapa.Columns.Add("DataInicio")
        dtEtapa.Columns.Add("DataFim")
        dtEtapa.Columns.Add("HoraInicio")
        dtEtapa.Columns.Add("HoraFim")
        dtEtapa.Columns.Add("D_E_L_E_T_")

        If bAdicionarLinha Then
            AdicionarItemEtapa(dtEtapa)
        End If

        Return dtEtapa

    End Function

    Private Sub Selecionar(ByVal sNumero As String)
        Dim cn As New ctlAtendimento
        Dim reader As SqlDataReader = cn.Selecionar(sNumero)
        If reader.HasRows() Then
            Dim dt As New DataTable
            If reader IsNot Nothing Then
                dt.Load(reader)
                reader.Close()
            End If
            'consulta etapas
            Dim dtEtapa As New DataTable
            dtEtapa = cn.ListarEtapasAtendimento(sNumero)
            hdnDtEmissaoOS.Value = dt.Rows(0).Item("DataEmissaoItemOS").ToString
            PreencherOcorrencias(hdnDtEmissaoOS.Value)
            Preencher(dt, dtEtapa)
            btnIncluir.Visible = False

            'valida garantia da base instalada
            ValidaGarantia(dt.Rows(0).Item("DATAGARANTIA"))

        End If
    End Sub

    Private Sub Preencher(Optional ByVal dt As DataTable = Nothing, Optional ByVal dtEtapa As DataTable = Nothing)

        Dim oRet As New ctlRetornoGenerico
        Dim oAtend As New ctlAtendimento
        Dim dtVersao As New DataTable

        'preenche dados a partir do xml gravado
        If File.Exists(NomeArquivoXml()) Then
            Dim ds = New DataSet
            ds.ReadXml(NomeArquivoXml())
            If (ds.Tables.Count > 0) Then
                If ds.Tables(0).Rows(0).Item("Atendente").ToString.Trim = HttpContext.Current.User.Identity.Name.Trim _
                    And (Request("Numero").ToString) = (ds.Tables(0).Rows(0).Item("NumeroOs").ToString & ds.Tables(0).Rows(0).Item("Sequencia").ToString) Then

                    dt = ds.Tables(0)
                    hdnAlteracaoItem.Value = dt.Rows(0).Item("hdnAlteracaoItem").ToString
                    chkEnviaAnalise.Checked = CBool(dt.Rows(0).Item("chkEnviaAnalise").ToString)
                    'printar msg na tela informando que existem dados alterados e não salvos
                    If Session("Atendimento") IsNot Nothing Then
                        oRet.Mensagem = "Existem itens alterados e/ou incluídos que não foram salvos no servidor. Por favor clicar no botão incluir."
                    Else
                        oRet.Mensagem = "Existem itens alterados e/ou incluídos que não foram salvos no servidor. Por favor clicar no botão alterar."
                    End If

                    'preenche tabela de etapas
                    If File.Exists(NomeArquivoXmlEtapa()) Then
                        Dim dsEtapa = New DataSet
                        dsEtapa.ReadXml(NomeArquivoXmlEtapa())
                        If (dsEtapa.Tables.Count > 0) Then
                            dtEtapa = dsEtapa.Tables(0)
                        End If
                    End If

                    oRet.Sucesso = True
                    oMensagem.SetMessage(oRet)

                End If
            End If
        End If

        grdItens.DataSource = dt
        grdItens.DataBind()

        grdEtapas.DataSource = dtEtapa
        grdEtapas.DataBind()

        If Request("Numero") IsNot Nothing Then
            If Not Request("Numero").Trim.Contains("Serie") Then
                lblNumero.Text = dt.Rows(0).Item("NumeroOS").ToString
                lblSequencia.Text = dt.Rows(0).Item("Sequencia").ToString
                lblSerie.Text = dt.Rows(0).Item("NumeroSerieProduto").ToString
                lblVersaoSoft.Text = dt.Rows(0).Item("Versao").ToString
                hdnTpAtendimento.Value = dt.Rows(0).Item("TipoAtendimento").ToString.Trim
            End If
        End If

        'Buscar ultima versao do software
        dtVersao = oAtend.VerificarVersaoAtualSoftware(lblSerie.Text)
        If dtVersao.Rows.Count > 0 Then
            lblTitVersaoAtual.Visible = True
            lblVersaoAtual.Visible = True
            lblVersaoAtual.Text = dtVersao.Rows(0).Item("Versao").ToString
        End If

        If Not String.IsNullOrEmpty(dt.Rows(0).Item("CodigoTecnico").ToString) AndAlso dt.Rows(0).Item("CodigoTecnico").ToString.Trim <> "00001" Then
            drpTecnico.SelectedValue = dt.Rows(0).Item("CodigoTecnico").ToString
        End If
        If Not String.IsNullOrEmpty(dt.Rows(0).Item("DataInicio").ToString) Then
            oDataInicio.Text = CDate(dt.Rows(0).Item("DataInicio")).ToShortDateString
        End If
        If Not String.IsNullOrEmpty(dt.Rows(0).Item("HoraInicio").ToString) Then
            oHoraInicio.Text = CDate(dt.Rows(0).Item("HoraInicio")).ToShortTimeString
        End If
        If Not String.IsNullOrEmpty(dt.Rows(0).Item("DataTermino").ToString) Then
            oDataTermino.Text = CDate(dt.Rows(0).Item("DataTermino")).ToShortDateString
        End If
        If Not String.IsNullOrEmpty(dt.Rows(0).Item("HoraTermino").ToString) Then
            oHoraTermino.Text = CDate(dt.Rows(0).Item("HoraTermino")).ToShortTimeString
        End If
        txtTranslado.Text = dt.Rows(0).Item("Translado").ToString
        If Not String.IsNullOrEmpty(dt.Rows(0).Item("CodigoOcorrencia").ToString) Then
            drpOcorrencia.SelectedValue = dt.Rows(0).Item("CodigoOcorrencia").ToString.Trim
        End If
        If Not String.IsNullOrEmpty(dt.Rows(0).Item("CodigoIncluirItemOS").ToString) Then
            drpIncluirItemOS.SelectedValue = dt.Rows(0).Item("CodigoIncluirItemOS").ToString.Trim
        End If
        If Not String.IsNullOrEmpty(dt.Rows(0).Item("CodigoStatus").ToString) Then
            Dim status As String = dt.Rows(0).Item("CodigoStatus").ToString.Trim
            drpStatus.SelectedValue = status
            If status = "1" Then
                drpStatus.Enabled = False
            End If
        End If

        drpDefeito.SelectedValue = dt.Rows(0).Item("CodDefeito").ToString
        drpServico.SelectedValue = dt.Rows(0).Item("CodServico").ToString
        'drpVersaoAtu.SelectedValue = dt.Rows(0).Item("VersaoAtualizada").ToString

        If Not String.IsNullOrEmpty(dt.Rows(0).Item("ROTAprovado").ToString) Then
            drpROTAprovado.SelectedValue = dt.Rows(0).Item("ROTAprovado").ToString.Trim
        End If
        drpAprovacao2.SelectedValue = dt.Rows(0).Item("Aprovacao2").ToString.Trim
        oHorasFaturadas.Text = dt.Rows(0).Item("HorasFaturadas").ToString
        Dim rot As String = dt.Rows(0).Item("DescricaoROT").ToString
        Dim sAnaliseOS2 As String = dt.Rows(0).Item("DescAprov2").ToString
        Dim defeito As String = dt.Rows(0).Item("DefeitoConstatado").ToString
        Dim causa As String = dt.Rows(0).Item("CausaProvavel").ToString
        Dim servico As String = dt.Rows(0).Item("ServicoExecutado").ToString
        Dim bIE As Boolean = False

        If Request.Browser.Browser = "IE" Then
            bIE = True
        End If

        If rot.Length > 0 And oRet.Mensagem.Trim = "" And Not bIE Then
            txtROT.Text = rot.Substring(0, rot.Length - 1)
        Else
            txtROT.Text = rot
        End If

        If sAnaliseOS2.Length > 0 And oRet.Mensagem.Trim = "" And Not bIE Then
            txtROT.Text = sAnaliseOS2.Substring(0, sAnaliseOS2.Length - 1)
        Else
            txtAnaliseOS2.Text = sAnaliseOS2
        End If

        If defeito.Length > 0 And oRet.Mensagem.Trim = "" And Not bIE Then
            txtDefeito.Text = defeito.Substring(0, defeito.Length - 1)
        Else
            txtDefeito.Text = defeito.Trim
        End If
        If causa.Length > 0 And oRet.Mensagem.Trim = "" And Not bIE Then
            txtCausa.Text = causa.Substring(0, causa.Length - 1)
        Else
            txtCausa.Text = causa.Trim
        End If
        If servico.Length > 0 And oRet.Mensagem.Trim = "" And Not bIE Then
            txtServExecutado.Text = servico.Substring(0, servico.Length - 1)
        Else
            txtServExecutado.Text = servico.Trim
        End If

        If Session("Atendimento") IsNot Nothing Then
            HabilitarModo("I")
        ElseIf drpStatus.SelectedValue = "1" Then 'ENCERRADO
            HabilitarModo("E")
        ElseIf dt.Rows(0).Item("TipoAtendimento").ToString.Trim = "2" Then
            HabilitarModo("P")
        ElseIf dt.Rows(0).Item("BloqueioAtendimento").ToString.Trim = "2" Then
            HabilitarModo("V")
        Else
            HabilitarModo("A")
        End If

        'corrigir problema de alteracao de atendimento
        'caso atendimento tenha sido gravado sem item, ao tentar incluir dá erro pois não item está em branco, sempre deixar preenchido
        If String.IsNullOrEmpty(dt.Rows(0).Item("Item").ToString) Then
            dt.Rows(0).Item("Item") = ObterIndice()
        End If

        ViewState.Add("Atendimento", dt)
        ViewState.Add("Etapa", dtEtapa)
    End Sub

    Private Sub Recarregar(ByVal sNumero As String)
        Selecionar(sNumero)
    End Sub

    Private Sub AdicionarItem(ByRef dt As DataTable, Optional ByVal nIndice As Integer = 0)
        Dim nItem As Integer = 0
        Dim dr As DataRow = dt.NewRow

        dr("Filial") = ""
        dr("NumeroOS") = ""
        dr("Sequencia") = lblSequencia.Text
        dr("Item") = ObterIndice()
        dr("hdnAlteracaoItem") = ""
        dr("chkEnviaAnalise") = ""
        dr("NumeroSerieProduto") = lblSerie.Text
        dr("TipoAtendimento") = ""
        dr("CodigoProduto") = ""
        dr("DescricaoProduto") = ""
        dr("CodigoTecnico") = ""
        dr("Comentario") = ""

        dr("DataInicio") = Date.Now
        dr("HoraInicio") = ""
        dr("DataTermino") = Date.Now
        dr("HoraTermino") = ""

        dr("CodigoOcorrencia") = ""
        dr("DescricaoOcorrencia") = ""
        dr("CodigoGarantia") = ""
        dr("DescricaoGarantia") = ""
        dr("CodigoStatus") = ""
        dr("DescricaoStatus") = ""
        dr("HorasFaturadas") = ""
        dr("Translado") = ""
        dr("CodigoIncluirItemOS") = ""
        dr("DescricaoIncluirItemOS") = ""
        dr("ROTAprovado") = ""
        dr("Versao") = ""
        dr("VersaoAtualizada") = ""

        dr("CodDefeito") = ""
        dr("CodServico") = ""
        dr("DescricaoROT") = ""
        dr("DefeitoConstatado") = ""
        dr("CausaProvavel") = ""
        dr("ServicoExecutado") = ""
        dr("BloqueioAtendimento") = ""
        dr("Aprovacao2") = ""
        dr("DescAprov2") = ""


        dr("CodigoItem") = ""
        dr("NumeroSeriePeca") = ""
        dr("DescricaoItem") = ""
        dr("Quantidade") = 0
        dr("NumeroLote") = ""
        dr("CodigoFabricante") = ""
        dr("D_E_L_E_T_") = ""

        If nIndice = 0 Then
            dt.Rows.Add(dr)
        Else
            dt.Rows.InsertAt(dr, nIndice)
        End If

    End Sub

    Private Sub AdicionarItemEtapa(ByRef dt As DataTable, Optional ByVal nIndice As Integer = 0)
        Dim nItem As Integer = 0
        Dim dr As DataRow = dt.NewRow

        dr("Item") = ObterIndiceEtapa()
        dr("CodigoEtapa") = ""
        dr("Descricao") = ""
        dr("DataInicio") = Date.Now
        dr("DataFim") = Date.Now
        dr("HoraInicio") = ""
        dr("HoraFim") = ""
        dr("D_E_L_E_T_") = ""

        If nIndice = 0 Then
            dt.Rows.Add(dr)
        Else
            dt.Rows.InsertAt(dr, nIndice)
        End If

    End Sub

    Private Sub grdItens_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdItens.RowCommand
        Dim gv As GridView = DirectCast(sender, GridView)
        Dim dt As DataTable = DirectCast(ViewState("Atendimento"), DataTable)
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
                Case "Delete"
                    RemoverItem(grdItens.Rows(nIndice), nIndice)
                    Dim cont As Integer = 0
                    If dt.Rows.Count = 0 Then
                        AdicionarItem(dt)
                    Else
                        While cont < dt.Rows.Count
                            Dim D_E_L_E_T_ As String = dt.Rows(cont).Item("D_E_L_E_T_").ToString
                            If D_E_L_E_T_.Equals("*") Then
                                cont += 1
                                If cont = dt.Rows.Count Then
                                    AdicionarItem(dt)
                                End If
                            Else
                                cont = dt.Rows.Count
                            End If
                        End While
                    End If
            End Select
            ViewState.Add("Atendimento", dt)
            grdItens.DataSource = dt
            grdItens.DataBind()


        End If
    End Sub

    Private Sub grdEtapas_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdEtapas.RowCommand
        Dim gv As GridView = DirectCast(sender, GridView)
        Dim dt As DataTable = DirectCast(ViewState("Etapa"), DataTable)
        If Not (e.CommandName = "Busca" Or e.CommandName = "") Then
            Dim nIndice As Integer = Integer.Parse(e.CommandArgument.ToString)
            Select Case e.CommandName
                Case "New"
                    nIndice = nIndice + 1
                    AdicionarItemEtapa(dt, nIndice)
                    grdEtapas.EditIndex = nIndice
                    grdEtapas.SelectedIndex = nIndice
                Case "Edit"
                    grdEtapas.EditIndex = nIndice
                    grdEtapas.SelectedIndex = nIndice
                Case "Update"
                    AtualizarItemEtapa(grdEtapas.Rows(nIndice), nIndice)
                    grdEtapas.EditIndex = -1
                Case "Cancel"
                    'se for alteracao, basta cacelar. se for inclusao, eh necessário remover a linha
                    grdEtapas.EditIndex = -1
                Case "Delete"
                    RemoverItemEtapa(grdEtapas.Rows(nIndice), nIndice)
                    Dim cont As Integer = 0
                    If dt.Rows.Count = 0 Then
                        AdicionarItemEtapa(dt)
                    Else
                        While cont < dt.Rows.Count
                            Dim D_E_L_E_T_ As String = dt.Rows(cont).Item("D_E_L_E_T_").ToString
                            If D_E_L_E_T_.Equals("*") Then
                                cont += 1
                                If cont = dt.Rows.Count Then
                                    AdicionarItemEtapa(dt)
                                End If
                            Else
                                cont = dt.Rows.Count
                            End If
                        End While
                    End If
            End Select
            ViewState.Add("Etapa", dt)
            grdEtapas.DataSource = dt
            grdEtapas.DataBind()
        End If
    End Sub

    Private Sub AtualizarItem(ByVal gr As GridViewRow, ByVal nIndice As Integer)
        Dim dt As DataTable
        dt = DirectCast(ViewState("Atendimento"), DataTable)
        Dim dr As DataRow = dt.NewRow()
        dr.ItemArray = dt.Rows(nIndice).ItemArray
        'verifica se existe alteração de item
        If dr("CodigoItem").ToString.Trim <> DirectCast(gr.FindControl("oProdutoView"), ProdutoView).Codigo.Trim Then
            hdnAlteracaoItem.Value = "S"
            updCabecalho.Update()
        End If
        dr("Item") = DirectCast(gr.FindControl("lblItem"), Label).Text.Trim
        dr("CodigoItem") = DirectCast(gr.FindControl("oProdutoView"), ProdutoView).Codigo.Trim
        dr("DescricaoItem") = DirectCast(gr.FindControl("oProdutoView"), ProdutoView).Nome.Trim
        dr("NumeroSeriePeca") = DirectCast(gr.FindControl("txtSeriePeca"), TextBox).Text.Trim
        dr("Quantidade") = DirectCast(gr.FindControl("txtQuantidade"), TextBox).Text.Trim
        dr("NumeroLote") = DirectCast(gr.FindControl("txtLote"), TextBox).Text.Trim
        dt.Rows.RemoveAt(nIndice)
        dt.Rows.InsertAt(dr, nIndice)
        dt.AcceptChanges()
    End Sub

    Private Sub AtualizarItemEtapa(ByVal gr As GridViewRow, ByVal nIndice As Integer)
        Dim dt As DataTable
        dt = DirectCast(ViewState("Etapa"), DataTable)
        Dim dr As DataRow = dt.NewRow()
        dr.ItemArray = dt.Rows(nIndice).ItemArray
        dr("Item") = DirectCast(gr.FindControl("lblItemEtapa"), Label).Text.Trim
        dr("CodigoEtapa") = DirectCast(gr.FindControl("drpEtapa"), DropDownList).SelectedValue.Trim
        dr("Descricao") = DirectCast(gr.FindControl("drpEtapa"), DropDownList).SelectedItem.Text
        dr("DataInicio") = DirectCast(gr.FindControl("oDtInicio"), DateBox).Text
        dr("HoraInicio") = DirectCast(gr.FindControl("oHrInicio"), HourBox).Text
        dr("DataFim") = DirectCast(gr.FindControl("oDtFim"), DateBox).Text
        dr("HoraFim") = DirectCast(gr.FindControl("oHrFim"), HourBox).Text
        dt.Rows.RemoveAt(nIndice)
        dt.Rows.InsertAt(dr, nIndice)
        dt.AcceptChanges()
    End Sub

    Private Sub grdItens_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles grdItens.RowDeleting
    End Sub

    Private Sub grdItens_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles grdItens.RowEditing
        If hdnTpAtendimento.Value = "2" Then
            Dim nIndice = e.NewEditIndex
            DirectCast(grdItens.Rows(nIndice).FindControl("txtQuantidade"), TextBox).Enabled = False
            DirectCast(grdItens.Rows(nIndice).FindControl("oProdutoView"), ProdutoView).Enabled = False
            DirectCast(grdItens.Rows(nIndice).FindControl("oProdutoView"), ProdutoView).HabilitarEdicao = False
        End If

    End Sub

    Private Sub grdItens_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles grdItens.RowUpdating
    End Sub

    Private Sub grdItens_RowCancelingEdit(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCancelEditEventArgs) Handles grdItens.RowCancelingEdit
    End Sub

    Private Sub grdEtapas_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles grdEtapas.RowDeleting
    End Sub

    Private Sub grdEtapas_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles grdEtapas.RowEditing
    End Sub

    Private Sub grdEtapas_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles grdEtapas.RowUpdating
        If grdEtapas.Rows.Count - 1 = e.RowIndex Then
            Dim oDtTermino = DirectCast(grdEtapas.Rows(e.RowIndex).FindControl("oDtFim"), DateBox).Text
            Dim oHrTermino = DirectCast(grdEtapas.Rows(e.RowIndex).FindControl("oHrFim"), HourBox).Text
            If oDtTermino <> "" And oHrTermino <> "" Then
                oDataTermino.Text = oDtTermino
                oHoraTermino.Text = oHrTermino
                updCabecalho.Update()
            End If
        End If
    End Sub

    Private Sub grdEtapas_RowCancelingEdit(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCancelEditEventArgs) Handles grdEtapas.RowCancelingEdit
    End Sub

    Protected Sub oProduto_SelecionarOnClick(ByVal obj As Object)
    End Sub

    Private Sub btnIncluir_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnIncluir.Click
        Dim oRet As New ctlRetornoGenerico

        Try
            Dim cn As New ctlAtendimento
            Dim sNumero As String = lblNumero.Text
            Dim sUsuario As String = HttpContext.Current.User.Identity.Name
            oRet = Validar(True)
            If oRet.Sucesso Then
                Atender()
            Else
                oMensagem.SetMessage(oRet)
            End If
        Catch ex As Exception
            oRet.Sucesso = False
            ctlUtil.EscreverLogErro("DetalheAtendimento - btnIncluir_Click: " & ex.Message())
            oRet.Mensagem = ctlUtil.sMsgErroPadrao
            oMensagem.SetMessage(oRet)
        End Try

    End Sub

    Private Sub btnAlterar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAlterar.Click
        Dim oRet As New ctlRetornoGenerico

        Try
            Dim sNumero As String = lblNumero.Text
            Dim sUsuario As String = HttpContext.Current.User.Identity.Name
            oRet = Validar(False)
            If oRet.Sucesso Then
                Atender()
            Else
                oMensagem.SetMessage(oRet)
            End If
        Catch ex As Exception
            oRet.Sucesso = False
            ctlUtil.EscreverLogErro("DetalheAtendimento - btnAlterar_click: " & ex.Message())
            oRet.Mensagem = ctlUtil.sMsgErroPadrao
            oMensagem.SetMessage(oRet)
        End Try

    End Sub

    Private Sub btnAlterarComPedido_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAlterarComPedido.Click
        Dim oRet As New ctlRetornoGenerico

        Try
            Dim sNumero As String = lblNumero.Text.Trim & lblSequencia.Text.Trim
            Dim sUsuario As String = HttpContext.Current.User.Identity.Name

            Dim dt As DataTable = DirectCast(ViewState("Atendimento"), DataTable)
            Dim dtEtapa As DataTable = DirectCast(ViewState("Etapa"), DataTable)
            Dim oAtendimento As New ctlAtendimento
            Dim sCodProd As String = ""
            Dim sCodItem As String = ""
            Dim sLote As String = ""
            Dim sNrSerie As String = ""

            For Each dr As DataRow In dt.Rows
                sCodProd += dr.Item("CodigoItem").ToString & ";"
                sCodItem += dr.Item("Item").ToString & ";"
                sNrSerie += dr.Item("NumeroSeriePeca").ToString & ";"
                sLote += dr.Item("NumeroLote").ToString & ";"
            Next

            If sCodItem.Length > 1 Then
                IncluirEtapas()
                oAtendimento.AtenderPedidoGerado(sNumero, lblSerie.Text.Trim, sCodProd, sCodItem, sLote, sNrSerie, txtCausa.Text.Trim & " ", txtServExecutado.Text.Trim & " ", drpStatus.SelectedValue.Trim)
                Selecionar(sNumero)
                HabilitarModo("P")
                oRet.Sucesso = True
                oRet.Mensagem = "Atendimento " & sNumero & " gravado com sucesso."
            Else
                oRet.Sucesso = False
                oRet.Mensagem = "Para que o arquivo possa ser enviado para gravação, é necessário existir pelo menos um item."
            End If

        Catch ex As Exception
            oRet.Sucesso = False
            ctlUtil.EscreverLogErro("DetalheAtendimento - btnAlterar_click: " & ex.Message())
            oRet.Mensagem = ctlUtil.sMsgErroPadrao
        End Try
        oMensagem.SetMessage(oRet)
    End Sub

    Private Sub Atender()
        Dim oRet As New ctlRetornoGenerico
        Dim sGeraROT As Boolean = True
        Try
            Dim cn As New ctlAtendimento

            Dim dt As DataTable = DirectCast(ViewState("Atendimento"), DataTable)
            Dim dtEtapa As DataTable = DirectCast(ViewState("Etapa"), DataTable)
            Dim oAtendimento As New ctlAtendimento()
            'chamada da criacao do xml contendo todos os dados da pagina
            CriarXmlComItensOS(dt)
            CriarXmlComEtapa(dtEtapa)

            If (txtPossuiGarantia.Value = "N" And chkEnviaAnalise.Checked = False) Or hdnAlteracaoItem.Value <> "S" Then
                sGeraROT = False
            End If

            If sGeraROT Then
                oAtendimento.BloqueioAtendimento = "2"
                If drpROTAprovado.SelectedIndex < 1 Then
                    oAtendimento.AprovadoAtendimento = "3"
                Else
                    oAtendimento.Aprovado2Atendimento = "3"
                End If
            End If

            oAtendimento.ModoAtendimento = txtModoAtendimento.Value 'I=Inclusao;A=Alteracao
            oAtendimento.NumeroAtendimentoOS = lblNumero.Text.Trim
            oAtendimento.SequenciaAtendimentoOS = lblSequencia.Text.Trim
            oAtendimento.NumeroSerieEquipamento = lblSerie.Text.Trim
            oAtendimento.CodigoTecnico = drpTecnico.SelectedValue
            oAtendimento.DataInicio = CDate(oDataInicio.Text)
            oAtendimento.HoraInicio = oHoraInicio.Text.Trim
            oAtendimento.DataTermino = CDate(oDataTermino.Text)
            oAtendimento.HoraTermino = oHoraTermino.Text.Trim
            oAtendimento.Translado = txtTranslado.Text.Trim
            oAtendimento.CodigoOcorrencia = drpOcorrencia.Text.Trim
            oAtendimento.CodigoStatus = drpStatus.SelectedValue
            oAtendimento.HorasFaturadas = oHorasFaturadas.Text.Trim
            oAtendimento.IncluirItemOS = drpIncluirItemOS.SelectedValue
            oAtendimento.Defeito = txtDefeito.Text.Trim
            oAtendimento.Causa = txtCausa.Text.Trim
            oAtendimento.ServicoExecutado = txtServExecutado.Text.Trim
            If lblVersaoSoft.Text.Trim <> "-" Then
                oAtendimento.VersaoSoftware = lblVersaoSoft.Text
            End If
            'oAtendimento.VersaoAtual = drpVersaoAtu.SelectedValue
            oAtendimento.CodigoServico = drpServico.SelectedValue
            oAtendimento.CodigoDefeito = drpDefeito.SelectedValue
            oAtendimento.PutItens(dt)
            oRet = cn.Atender(oAtendimento)
            'oRet.Sucesso = True
            If oRet.Sucesso Then
                IncluirEtapas()
                'caso inclusao for efetuada com sucesso deleta arquivo xml
                DeletaArquivoXml()
                DeletaArquivoXmlEtapa()
                Selecionar(oRet.Chave.Substring(0, 2))
                If sGeraROT Then 'BLOQUEADO AGUARDANDO RETORNO AVALIACAO DO ROT
                    Dim sRetEmail As String
                    sRetEmail = ctlEmail.enviaMensagemEmailROT(lblNumero.Text.Trim)
                    oRet.Mensagem = oRet.Mensagem & sRetEmail
                    HabilitarModo("V")
                Else
                    txtModoAtendimento.Value = "A"
                    HabilitarModo("A")
                End If

            End If
        Catch ex As Exception
            oRet.Sucesso = False
            If txtModoAtendimento.Value = "I" Then
                ctlUtil.EscreverLogErro("DetalheAtendimento - btnIncluir_Click: " & ex.Message())
            Else
                ctlUtil.EscreverLogErro("DetalheAtendimento - btnAlterar_click: " & ex.Message())
            End If
            oRet.Mensagem = ctlUtil.sMsgErroPadrao

        End Try
        oMensagem.SetMessage(oRet)
    End Sub

    Private Sub IncluirEtapas()

        Dim oAtend As New ctlAtendimento
        Dim sItem As String = ""
        Dim sCodEtapa As String = ""
        Dim sDescricao As String = ""
        Dim sDataInicio As String = ""
        Dim sDataInicio1 As Date
        Dim sHoraInicio As String = ""
        Dim sDataFim As String = ""
        Dim sDataFim1 As Date
        Dim sHoraFim As String = ""
        Dim sDeletado As String = ""

        For Each gr As GridViewRow In grdEtapas.Rows
            sItem += DirectCast(gr.FindControl("lblItemEtapa"), Label).Text & ";"
            sCodEtapa += DirectCast(gr.FindControl("drpEtapa"), DropDownList).SelectedValue & ";"
            sDescricao += DirectCast(gr.FindControl("drpEtapa"), DropDownList).SelectedItem.ToString.Trim & ";"
            sDataInicio1 = CDate(DirectCast(gr.FindControl("oDtInicio"), DateBox).Text)
            sDataInicio += Year(sDataInicio1) & Month(sDataInicio1).ToString.PadLeft(2, CChar("0")) & Day(sDataInicio1).ToString.PadLeft(2, CChar("0")) & ";"
            sHoraInicio += DirectCast(gr.FindControl("oHrInicio"), HourBox).Text & ";"
            sDataFim1 = CDate(DirectCast(gr.FindControl("oDtFim"), DateBox).Text)
            sDataFim += Year(sDataFim1) & Month(sDataFim1).ToString.PadLeft(2, CChar("0")) & Day(sDataFim1).ToString.PadLeft(2, CChar("0")) & ";"
            sHoraFim += DirectCast(gr.FindControl("oHrFim"), HourBox).Text & ";"
            sDeletado += DirectCast(gr.FindControl("lblD_E_L_E_T_"), Label).Text & ";"
        Next

        oAtend.IncluirEtapasAtendimento(sItem, lblNumero.Text.Trim & lblSequencia.Text.Trim, sCodEtapa, sDescricao, sDataInicio, sHoraInicio, sDataFim, sHoraFim, sDeletado)
        'oAtend.IncluirEtapasAtendimento(lblNumero.Text.Trim, DirectCast(ViewState("Etapa"), DataTable))

    End Sub

    Private Sub RemoverItem(ByVal gr As GridViewRow, ByVal nIndice As Integer)
        Dim lblRegistro = DirectCast(gr.FindControl("lblRegistro"), Label)
        Dim dt As DataTable
        dt = DirectCast(ViewState("Atendimento"), DataTable)
        Dim dr As DataRow = dt.NewRow()
        dr.ItemArray = dt.Rows(nIndice).ItemArray
        dt.Rows.RemoveAt(nIndice)
        If Not String.IsNullOrEmpty(lblRegistro.Text) Then
            dr("D_E_L_E_T_") = "*"
            dt.Rows.InsertAt(dr, nIndice)
            dt.AcceptChanges()
        End If
    End Sub

    Private Sub RemoverItemEtapa(ByVal gr As GridViewRow, ByVal nIndice As Integer)
        Dim dt As DataTable
        dt = DirectCast(ViewState("Etapa"), DataTable)
        Dim dr As DataRow = dt.NewRow()
        dr.ItemArray = dt.Rows(nIndice).ItemArray

        dt.Rows.RemoveAt(nIndice)

        dr("D_E_L_E_T_") = "*"
        dt.Rows.InsertAt(dr, nIndice)
        dt.AcceptChanges()
    End Sub

    Private Function Validar(ByVal bInclusao As Boolean) As ctlRetornoGenerico
        Dim oRet As New ctlRetornoGenerico
        oRet.Sucesso = False
        If drpTecnico.SelectedIndex < 1 Then
            oRet.Mensagem = "O nome do técnico não foi fornecido."
        ElseIf drpOcorrencia.SelectedIndex < 1 Then
            oRet.Mensagem = "A ocorrência não foi preenchida."
        ElseIf IsDate(oDataInicio.Text) = False Or oDataInicio.Text.Contains("_") Then
            oRet.Mensagem = "Data inicial inválida."
        ElseIf IsDate(oDataTermino.Text) = False Or oDataTermino.Text.Contains("_") Then
            oRet.Mensagem = "Data final inválida."
        ElseIf CDate(oDataInicio.Text) > CDate(oDataTermino.Text) Then
            oRet.Mensagem = "A data inicial não pode ser maior que a data final."
        ElseIf oHoraInicio.Text.Trim = "" Then
            oRet.Mensagem = "A hora inicial não foi preenchida."
        ElseIf Not oHoraInicio.IsValid Then
            oRet.Mensagem = "A hora inicial está inválida."
        ElseIf oHoraTermino.Text.Trim = "" Then
            oRet.Mensagem = "A hora final não foi preenchida."
        ElseIf Not oHoraTermino.IsValid Then
            oRet.Mensagem = "A hora final está inválida."
        ElseIf (CDate(oDataInicio.Text).ToShortDateString = CDate(oDataTermino.Text).ToShortDateString) AndAlso _
        (CDate(oHoraInicio.Text).ToShortTimeString > CDate(oHoraTermino.Text).ToShortTimeString) Then
            oRet.Mensagem = "A hora inicial não pode ser maior que a hora final se o atendimento foi realizado no mesmo dia ."
        ElseIf drpIncluirItemOS.SelectedIndex < 1 Then
            oRet.Mensagem = "O campo incluir item OS não foi preenchido."
            'ElseIf drpVersaoAtu.SelectedIndex < 1 Then
            '    oRet.Mensagem = "O campo versão de software não foi preenchido."
        ElseIf drpDefeito.SelectedIndex < 1 Then
            oRet.Mensagem = "O campo defeito não foi preenchido."
        ElseIf drpServico.SelectedIndex < 1 Then
            oRet.Mensagem = "O campo serviço não foi preenchido."
        ElseIf txtDefeito.Text.Trim = "" Then
            oRet.Mensagem = "A descrição do defeito não foi preenchido."
            'ElseIf txtCausa.Text.Trim = "" Then
            '    oRet.Mensagem = "A descrição da causa não foi preenchida."
            'ElseIf txtServExecutado.Text.Trim = "" Then
            '    oRet.Mensagem = "A descrição do serviço executado não foi preenchida."
        ElseIf Not ValidarEtapas(oRet) Then
            'Etapas invalidas
        ElseIf Not ValidarItens(oRet, bInclusao) Then
            'Itens inválido
        Else
            oRet.Sucesso = True
        End If
        Return oRet
    End Function

    Private Function ValidarItens(ByVal oRet As ctlRetornoGenerico, ByVal bInclusao As Boolean) As Boolean
        Dim bRet As Boolean = False
        oRet.Mensagem = ""
        If grdItens.EditIndex > -1 Then
            oRet.Mensagem = "A linha " + (grdItens.EditIndex + 1).ToString + " encontra-se em edição e precisa ser confirmada ou cancelada."
        Else
            For Each gr As GridViewRow In grdItens.Rows
                Dim oProdutoView As ProdutoView = DirectCast(gr.FindControl("oProdutoView"), ProdutoView)
                Dim txtQuantidade As TextBox = DirectCast(gr.FindControl("txtQuantidade"), TextBox)
                Dim txtNumSerie As TextBox = DirectCast(gr.FindControl("txtSeriePeca"), TextBox)
                Dim txtLote As TextBox = DirectCast(gr.FindControl("txtLote"), TextBox)
                If Not String.IsNullOrEmpty(oProdutoView.Codigo) Then
                    If Not oProdutoView.IsValid Then
                        oRet.Mensagem = "A linha " + (gr.RowIndex + 1).ToString + " não contém um código de produto válido."
                    ElseIf String.IsNullOrEmpty(txtQuantidade.Text.Trim) Then
                        oRet.Mensagem = "Informe a quantidade de peças na linha " + (gr.RowIndex + 1).ToString + "."
                    ElseIf Not IsNumeric(txtQuantidade.Text.Trim) Then
                        oRet.Mensagem = "Digite apenas número no campo quantidade da linha " + (gr.RowIndex + 1).ToString + "."
                    ElseIf Not CInt(txtQuantidade.Text.Trim) > 0 Then
                        oRet.Mensagem = "Informe a quantidade de peças na linha " + (gr.RowIndex + 1).ToString + "."
                    End If
                End If
            Next
        End If
        If oRet.Mensagem.Trim.Length = 0 Then
            bRet = True
        End If
        Return bRet
    End Function

    Private Function ValidarEtapas(ByVal oRet As ctlRetornoGenerico) As Boolean
        Dim bRet As Boolean = False
        oRet.Mensagem = ""
        If grdEtapas.EditIndex > -1 Then
            oRet.Mensagem = "A linha " + (grdEtapas.EditIndex + 1).ToString + " da etapa encontra-se em edição e precisa ser confirmada ou cancelada."
        Else
            For Each gr As GridViewRow In grdEtapas.Rows
                Dim drpEtapa = DirectCast(gr.FindControl("drpEtapa"), DropDownList)
                Dim oDtInicio = DirectCast(gr.FindControl("oDtInicio"), DateBox)
                Dim oHrInicio = DirectCast(gr.FindControl("oHrInicio"), HourBox)
                Dim oDtFim = DirectCast(gr.FindControl("oDtFim"), DateBox)
                Dim oHrFim = DirectCast(gr.FindControl("oHrFim"), HourBox)
                Dim lblDelete = DirectCast(gr.FindControl("lblD_E_L_E_T_"), Label).Text

                If lblDelete <> "*" Then
                    If drpEtapa.SelectedValue = "" Then
                        oRet.Mensagem = "Informe a etapa na linha " + (gr.RowIndex + 1).ToString + "."
                    ElseIf Not IsDate(oDtInicio.Text) Then
                        oRet.Mensagem = "Informe a data de início na linha " + (gr.RowIndex + 1).ToString + "."
                    ElseIf Not oHrInicio.IsValid Then
                        oRet.Mensagem = "Informe a hora de início na linha " + (gr.RowIndex + 1).ToString + "."
                    ElseIf Not IsDate(oDtFim.Text) Then
                        oRet.Mensagem = "Informe a data de término na linha " + (gr.RowIndex + 1).ToString + "."
                    ElseIf Not oHrFim.IsValid Then
                        oRet.Mensagem = "Informe a hora de término na linha " + (gr.RowIndex + 1).ToString + "."
                    ElseIf CDate(oDtInicio.Text) > CDate(oDtFim.Text) Then
                        oRet.Mensagem = "A data de início não pode ser maior que a data de término na linha " + (gr.RowIndex + 1).ToString + "."
                    ElseIf (CDate(oDtInicio.Text).ToShortDateString = CDate(oDtFim.Text).ToShortDateString) AndAlso _
        (CDate(oHrInicio.Text).ToShortTimeString > CDate(oHrFim.Text).ToShortTimeString) Then
                        oRet.Mensagem = "A hora de início não pode ser maior que a hora de término se a estapa foi realizado no mesmo dia " + (gr.RowIndex + 1).ToString + "."
                    End If
                End If
            Next
        End If
        If oRet.Mensagem.Trim.Length = 0 Then
            bRet = True
        End If
        Return bRet
    End Function

    Private Sub grdItens_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdItens.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim txtSeriePeca = DirectCast(e.Row.FindControl("txtSeriePeca"), TextBox)
            Dim lblD_E_L_E_T_ As Label = DirectCast(e.Row.FindControl("lblD_E_L_E_T_"), Label)

            If lblD_E_L_E_T_.Text.Equals("*") Then
                e.Row.Visible = False
            Else
                Dim linhaAtual As Integer = e.Row.RowIndex
                Dim ultimaLinhaVisivel As Integer = ObterUltimaLinhaNaoDeletada(linhaAtual)
                'If ultimaLinhaVisivel <> -1 Then
                '    Dim cell As GridViewRow = grdItens.Rows(ultimaLinhaVisivel)
                '    If cell.BackColor = Nothing Then
                '        e.Row.BackColor = System.Drawing.Color.FromArgb(204, 255, 221)
                '    Else
                '        e.Row.BackColor = Nothing
                '    End If
                'Else
                '    e.Row.BackColor = System.Drawing.Color.FromArgb(204, 255, 221)
                'End If
            End If

        End If
    End Sub

    Private Sub grdEtapas_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdEtapas.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim lblItemEtapa = DirectCast(e.Row.FindControl("lblItemEtapa"), Label)
            Dim lblD_E_L_E_T_ As Label = DirectCast(e.Row.FindControl("lblD_E_L_E_T_"), Label)

            If lblD_E_L_E_T_.Text.Equals("*") Then
                e.Row.Visible = False
            Else
                Dim linhaAtual As Integer = e.Row.RowIndex
                Dim ultimaLinhaVisivel As Integer = ObterUltimaLinhaNaoDeletadaEtapa(linhaAtual)
                If ultimaLinhaVisivel <> -1 Then
                    Dim cell As GridViewRow = grdEtapas.Rows(ultimaLinhaVisivel)
                    If cell.BackColor = Nothing Then
                        e.Row.BackColor = System.Drawing.Color.FromArgb(204, 255, 221)
                    Else
                        e.Row.BackColor = Nothing
                    End If
                Else
                    e.Row.BackColor = System.Drawing.Color.FromArgb(204, 255, 221)
                End If
            End If

            Dim nColunaTotal As Integer = e.Row.Cells.Count - 1
            Dim drpEtapa As DropDownList = DirectCast(e.Row.FindControl("drpEtapa"), DropDownList)
            Dim reader As SqlDataReader
            Dim os As New ctlOrdemServico
            Dim sSelecionado As String
            Dim bAtivo As Boolean = False

            'USADO PARA NAO MOSTRAR MAIS OS ITENS INATIVOS A PARTIR DA DATA DE IMPANTACAO, PARA NAO PERDER HISTORICO
            If IsDate(hdnDtEmissaoOS.Value) Then
                If Date.Compare(CDate(hdnDtEmissaoOS.Value), CDate(Session("DataCorte"))) >= 0 Then
                    bAtivo = True
                End If
            End If

            reader = os.ListarEtapasOS(bAtivo)
            If reader.HasRows Then
                drpEtapa.DataSource = reader
                drpEtapa.DataBind()
                drpEtapa.Items.Insert(0, "Nenhum")
                drpEtapa.Items(0).Value = ""
                sSelecionado = DataBinder.Eval(e.Row.DataItem, "CodigoEtapa").ToString.Trim

                If drpEtapa.Items.FindByValue(sSelecionado) IsNot Nothing Then
                    drpEtapa.Items.FindByValue(sSelecionado).Selected = True
                End If

                'If Not String.IsNullOrEmpty(sSelecionado) Then
                '    drpEtapa.SelectedItem.Value = sSelecionado
                'End If
            End If

        End If

    End Sub

    Private Function ObterUltimaLinhaNaoDeletada(ByVal linhaAtual As Integer) As Integer
        If linhaAtual > 0 Then
            Dim cell As GridViewRow = grdItens.Rows(linhaAtual - 1)
            Dim lblD_E_L_E_T_ As Label = DirectCast(cell.Cells(6).FindControl("lblD_E_L_E_T_"), Label)
            If lblD_E_L_E_T_.Text.Equals("*") Then
                Return ObterUltimaLinhaNaoDeletada(linhaAtual - 1)
            Else
                Return linhaAtual - 1
            End If
        Else
            Return -1
        End If
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

    Private Sub PreencherCombos(Optional ByVal regiao As String = Nothing)
        Dim reader As SqlDataReader
        Dim ct As New ctlAtendimento

        reader = ct.ListarTecnicos(regiao)
        drpTecnico.DataSource = reader
        drpTecnico.DataBind()
        drpTecnico.Items.Insert(0, "Nenhum")

        reader = ct.ListarDefeitosServicos("D")
        drpDefeito.DataSource = reader
        drpDefeito.DataBind()
        drpDefeito.Items.Insert(0, "Nenhum")

        reader = ct.ListarDefeitosServicos("S")
        drpServico.DataSource = reader
        drpServico.DataBind()
        drpServico.Items.Insert(0, "Nenhum")

        reader.Close()

    End Sub

    Private Sub PreencherOcorrencias(sData As String)
        Dim reader As SqlDataReader
        Dim ct As New ctlChamadoTecnico
        Dim bAtivo As Boolean = False

        'USADO PARA NAO MOSTRAR MAIS OS ITENS INATIVOS A PARTIR DA DATA DE IMPANTACAO, PARA NAO PERDER HISTORICO
        If IsDate(sData) Then
            If Date.Compare(CDate(sData), CDate(Session("DataCorte"))) >= 0 Then
                bAtivo = True
            End If
        End If

        reader = ct.ListarOcorrencias(bAtivo)
        drpOcorrencia.DataSource = reader
        drpOcorrencia.DataBind()
        drpOcorrencia.Items.Insert(0, "Nenhum")
        reader.Close()
    End Sub

    Private Sub btnVoltar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnVoltar.Click
        If String.IsNullOrEmpty(btnVoltar.CommandArgument) Then
            Response.Redirect("ConsultaAtendimento.aspx")
        Else
            Response.Redirect("DetalheOrdemServico.aspx?Numero=" + btnVoltar.CommandArgument)
        End If
    End Sub

    Private Sub btnImprimir_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnImprimir.Click
        Dim aOpcao() As String
        ReDim Preserve aOpcao(5)
        aOpcao(0) = ""
        aOpcao(1) = "Defina o modelo de impressão."
        aOpcao(2) = "Aprovador/Técnico"
        aOpcao(3) = "ImpressaoAtendimento.aspx?Numero=" + lblNumero.Text.Trim + lblSequencia.Text.Trim + "?Modo=A"
        aOpcao(4) = "Cliente/Aprovador/Técnico"
        aOpcao(5) = "ImpressaoAtendimento.aspx?Numero=" + lblNumero.Text.Trim + lblSequencia.Text.Trim + "?Modo=B"
        oPopupBox.Exibir(aOpcao)
    End Sub

    Private Sub HabilitarModo(ByVal codigo As String)
        If codigo = "I" Then
            TituloPagina("Atendimento da OS - Incluir")
            'Campos
            drpTecnico.Enabled = True
            oDataInicio.Enabled = True
            oHoraInicio.Enabled = True
            oDataTermino.Enabled = True
            oHoraTermino.Enabled = True
            'Botões
            btnIncluir.Visible = True
            btnAlterar.Visible = False
            btnImprimir.Visible = False
        ElseIf codigo = "A" Then
            TituloPagina("Atendimento da OS - Alterar")
            'Campos
            drpTecnico.Enabled = False
            oDataInicio.Enabled = False
            oHoraInicio.Enabled = False
            oDataTermino.Enabled = False
            oHoraTermino.Enabled = False

            'Botões
            btnIncluir.Visible = False
            btnAlterar.Visible = True
            btnImprimir.Visible = True
        ElseIf codigo = "V" Or codigo = "E" Then
            If codigo = "V" Then
                TituloPagina("Atendimento da OS - Visualizar - Aguardando análise Intermed")
            Else
                TituloPagina("Atendimento da OS encerrado - Apenas visualização")
            End If

            'Campos
            drpTecnico.Enabled = False
            oDataInicio.Enabled = False
            oHoraInicio.Enabled = False
            oDataTermino.Enabled = False
            oHoraTermino.Enabled = False
            drpOcorrencia.Enabled = False
            drpIncluirItemOS.Enabled = False
            txtCausa.ReadOnly = True
            txtDefeito.ReadOnly = True
            txtServExecutado.ReadOnly = True
            drpDefeito.Enabled = False
            drpServico.Enabled = False
            grdItens.Columns(0).Visible = False
            grdEtapas.Columns(0).Visible = False

            'Botões
            btnIncluir.Visible = False
            btnAlterar.Visible = False
            btnImprimir.Visible = True
        ElseIf codigo = "P" Then
            TituloPagina("Atendimento da OS - Alterar - Pedido Gerado")
            'Campos
            drpTecnico.Enabled = False
            oDataInicio.Enabled = False
            oHoraInicio.Enabled = False
            oDataTermino.Enabled = False
            oHoraTermino.Enabled = False
            drpOcorrencia.Enabled = False
            drpIncluirItemOS.Enabled = False
            txtCausa.ReadOnly = False
            txtDefeito.ReadOnly = True
            txtServExecutado.ReadOnly = False
            drpDefeito.Enabled = False
            drpServico.Enabled = False
            'grdEtapas.Columns(0).Visible = False
            If drpStatus.SelectedValue = "2" Then 'aberto
                drpStatus.Enabled = True
            Else
                drpStatus.Enabled = False
            End If
            DirectCast(grdItens.Columns(0), CommandField).ShowDeleteButton = False
            DirectCast(grdItens.Columns(0), CommandField).ShowInsertButton = False
            grdItens.DataBind()

            'Botões
            btnIncluir.Visible = False
            btnAlterar.Visible = False
            btnAlterarComPedido.Visible = True
            btnImprimir.Visible = True
        End If

        If Len(txtROT.Text.Trim) > 0 Then
            pnlAprovacaoOS.Visible = True
            pnlDescricaoROT.Visible = True
            If Len(txtAnaliseOS2.Text.Trim) > 0 Then
                pnlDescricaoOS2.Visible = True
                lblAprovacao2.Visible = True
                drpAprovacao2.Visible = True
            End If
        End If
    End Sub

    Private Function ObterIndice() As String
        Dim indice As Integer = 0
        Dim dt As DataTable
        dt = DirectCast(ViewState("Atendimento"), DataTable)
        If dt IsNot Nothing Then
            For Each dr As DataRow In dt.Rows
                If CInt(dr("Item")) > indice Then
                    indice = CInt(dr("Item"))
                End If
            Next
            indice += 1
            If indice >= 10 Then
                Return indice.ToString
            Else
                Return "0" + indice.ToString
            End If
        End If
        Return "01"
    End Function

    Private Function ObterIndiceEtapa() As String
        Dim indice As Integer = 0
        Dim dt As DataTable
        dt = DirectCast(ViewState("Etapa"), DataTable)
        If dt IsNot Nothing Then
            For Each dr As DataRow In dt.Rows
                If CInt(dr("Item")) > indice Then
                    indice = CInt(dr("Item"))
                End If
            Next
            indice += 1
            If indice >= 10 Then
                Return indice.ToString
            Else
                Return "0" + indice.ToString
            End If
        End If
        Return "01"
    End Function

    Private Sub CriarXmlComItensOS(ByVal dt As DataTable)

        Dim writer As New XmlTextWriter(NomeArquivoXml(), Nothing)
        Dim sTipo As String = ""

        'inicia o documento xml
        writer.WriteStartDocument()

        'define a indentação do arquivo
        writer.Formatting = Formatting.Indented

        'escreve um comentario
        writer.WriteComment("Cabeçalho e itens de Atendimento de OS")

        'escreve o elmento raiz
        writer.WriteStartElement("AtendimentoOS")

        For Each dr As DataRow In dt.Rows
            writer.WriteStartElement("itens")

            writer.WriteElementString("Atendente", HttpContext.Current.User.Identity.Name)

            'Atendimento de OS
            writer.WriteElementString("Filial", "")
            writer.WriteElementString("NumeroOS", lblNumero.Text)
            writer.WriteElementString("Sequencia", lblSequencia.Text)
            writer.WriteElementString("hdnAlteracaoItem", hdnAlteracaoItem.Value)
            writer.WriteElementString("chkEnviaAnalise", chkEnviaAnalise.Checked.ToString)


            writer.WriteElementString("NumeroSerieProduto", lblSerie.Text)
            writer.WriteElementString("TipoAtendimento", hdnTpAtendimento.Value)
            writer.WriteElementString("CodigoProduto", "")
            writer.WriteElementString("DescricaoProduto", "")
            writer.WriteElementString("CodigoTecnico", drpTecnico.SelectedValue)

            writer.WriteElementString("NomeTecnico", "")
            writer.WriteElementString("DataInicio", oDataInicio.Text)
            writer.WriteElementString("HoraInicio", oHoraInicio.Text)
            writer.WriteElementString("DataTermino", oDataTermino.Text)
            writer.WriteElementString("HoraTermino", oHoraTermino.Text)
            writer.WriteElementString("CodigoOcorrencia", drpOcorrencia.SelectedValue)
            writer.WriteElementString("DescricaoOcorrencia", "")
            writer.WriteElementString("CodigoGarantia", "")
            writer.WriteElementString("DescricaoGarantia", "")
            writer.WriteElementString("CodigoStatus", drpStatus.SelectedValue)
            writer.WriteElementString("DescricaoStatus", "")
            writer.WriteElementString("HorasFaturadas", oHorasFaturadas.Text)
            writer.WriteElementString("Translado", txtTranslado.Text)
            writer.WriteElementString("CodigoIncluirItemOS", drpIncluirItemOS.SelectedValue)
            writer.WriteElementString("DescricaoIncluirItemOS", "")
            writer.WriteElementString("ROTAprovado", drpROTAprovado.Text)
            writer.WriteElementString("Versao", lblVersaoSoft.Text)
            'writer.WriteElementString("VersaoAtualizada", drpVersaoAtu.SelectedValue)

            writer.WriteElementString("CodDefeito", drpDefeito.SelectedValue)
            writer.WriteElementString("CodServico", drpServico.SelectedValue)
            writer.WriteElementString("DescricaoROT", txtROT.Text)
            writer.WriteElementString("DefeitoConstatado", txtDefeito.Text)
            writer.WriteElementString("CausaProvavel", txtCausa.Text)
            writer.WriteElementString("ServicoExecutado", txtServExecutado.Text)
            writer.WriteElementString("BloqueioAtendimento", lblDtGarantia.Text)
            writer.WriteElementString("Aprovacao2", drpAprovacao2.SelectedValue)
            writer.WriteElementString("DescAprov2", txtAnaliseOS2.Text)


            'Item do Atendimento da OS
            writer.WriteElementString("Item", dr.Item("Item").ToString)
            writer.WriteElementString("CodigoItem", dr.Item("CodigoItem").ToString)
            writer.WriteElementString("DescricaoItem", dr.Item("DescricaoItem").ToString)
            writer.WriteElementString("NumeroSeriePeca", dr.Item("NumeroSeriePeca").ToString)
            writer.WriteElementString("Quantidade", dr.Item("Quantidade").ToString)
            writer.WriteElementString("NumeroLote", dr.Item("NumeroLote").ToString)
            writer.WriteElementString("CodigoFabricante", dr.Item("CodigoFabricante").ToString)
            writer.WriteElementString("D_E_L_E_T_", dr.Item("D_E_L_E_T_").ToString)


            writer.WriteEndElement()
        Next

        ' encerra o elemento raiz
        writer.WriteFullEndElement()

        'Escreve o XML para o arquivo e fecha o objeto escritor
        writer.Close()
    End Sub

    Private Sub DeletaArquivoXml()
        If File.Exists(NomeArquivoXml()) Then
            File.Delete(NomeArquivoXml())
        End If
    End Sub

    Private Sub CriarXmlComEtapa(ByVal dtEtapa As DataTable)

        Dim writer As New XmlTextWriter(NomeArquivoXmlEtapa(), Nothing)
        Dim sTipo As String = ""

        'inicia o documento xml
        writer.WriteStartDocument()

        'define a indentação do arquivo
        writer.Formatting = Formatting.Indented

        'escreve um comentario
        writer.WriteComment("Etapas de Atendimento de OS")

        'escreve o elmento raiz
        writer.WriteStartElement("Etapas")

        For Each dr As DataRow In dtEtapa.Rows
            writer.WriteStartElement("itens")

            'Etapas
            writer.WriteElementString("Item", dr.Item("Item").ToString)
            writer.WriteElementString("CodigoEtapa", dr.Item("CodigoEtapa").ToString)
            writer.WriteElementString("Descricao", dr.Item("Descricao").ToString)
            writer.WriteElementString("DataInicio", dr.Item("DataInicio").ToString)
            writer.WriteElementString("DataFim", dr.Item("DataFim").ToString)
            writer.WriteElementString("HoraInicio", dr.Item("HoraInicio").ToString)
            writer.WriteElementString("HoraFim", dr.Item("HoraFim").ToString)
            writer.WriteElementString("D_E_L_E_T_", dr.Item("D_E_L_E_T_").ToString)

            writer.WriteEndElement()
        Next

        ' encerra o elemento raiz
        writer.WriteFullEndElement()

        'Escreve o XML para o arquivo e fecha o objeto escritor
        writer.Close()
    End Sub

    Private Sub DeletaArquivoXmlEtapa()
        If File.Exists(NomeArquivoXmlEtapa()) Then
            File.Delete(NomeArquivoXmlEtapa())
        End If
    End Sub

    ' Retorna o nome e caminho do arquivo xml
    Private Function NomeArquivoXml() As String

        Dim sNomeArquivo As String = Server.MapPath("~") & "\arquivos_xml_erros_os_online"

        If Not Directory.Exists(sNomeArquivo) Then
            Directory.CreateDirectory(sNomeArquivo)
        End If

        Return sNomeArquivo & "\atendimento_os_" & HttpContext.Current.Session("NomeUsuario").ToString.Trim & ".xml"

    End Function

    ' Retorna o nome e caminho do arquivo xml
    Private Function NomeArquivoXmlEtapa() As String

        Dim sNomeArquivo As String = Server.MapPath("~") & "\arquivos_xml_erros_os_online"

        If Not Directory.Exists(sNomeArquivo) Then
            Directory.CreateDirectory(sNomeArquivo)
        End If

        Return sNomeArquivo & "\atendimento_os_etapa_" & HttpContext.Current.Session("NomeUsuario").ToString.Trim & ".xml"

    End Function

    Private Sub ValidaGarantia(ByVal dtGarantia As Object)

        'valida garantia da base instalada
        txtPossuiGarantia.Value = "N" 'Setar default sem garantia
        If IsDate(dtGarantia) Then
            lblDtGarantia.Text = CDate(dtGarantia).ToShortDateString
            If CDate(lblDtGarantia.Text) >= Date.Today Then
                lblDtGarantia.Text += " - com garantia"
                txtPossuiGarantia.Value = "S" 'Esta base instalada possui garantia
                pnlEnviaAnalise.Visible = False
            Else
                lblDtGarantia.CssClass = "semGarantia"
                lblDtGarantia.Text += " - sem garantia"
            End If
        Else
            lblDtGarantia.Text = "Equipamento sem garantia"
        End If

    End Sub

    Private Function ObterUltimaLinhaNaoDeletadaEtapa(ByVal linhaAtual As Integer) As Integer
        If linhaAtual > 0 Then
            Dim lblD_E_L_E_T_ As Label = DirectCast(grdEtapas.Rows(linhaAtual - 1).FindControl("lblD_E_L_E_T_"), Label)
            If lblD_E_L_E_T_.Text.Equals("*") Then
                Return ObterUltimaLinhaNaoDeletadaEtapa(linhaAtual - 1)
            Else
                Return linhaAtual - 1
            End If
        Else
            Return -1
        End If
    End Function

End Class