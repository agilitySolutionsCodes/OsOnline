Imports System.Globalization
Imports System.Data.SqlClient
Imports System.Data
Imports System.IO
Imports System.Xml
Imports System.Net
Imports System.Security

Partial Public Class DetalheOrdemServico
    Inherits BaseWebUI


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                DirectCast(Me.Master.Controls(0).Controls(3).Controls(7).FindControl("menuOsOnline"), Menu).Visible = False
                NomeBotoes()
                'PreencherOrigensOS()
                containerAnexo.Visible = False
                If Request("Numero") Is Nothing Then
                    Incluir()
                Else
                    Dim numero As String = Request("Numero").Substring(0, 6)
                    Selecionar(numero)
                End If
            End If

        Catch ex As Exception
            Dim oRet As New ctlRetornoGenerico
            ctlUtil.EscreverLogErro("DetalheOrdemServico - Page_load: " & ex.Message())
            oRet.Mensagem = ctlUtil.sMsgErroPadrao

            oRet.Sucesso = False
            oMensagem.SetMessage(oRet)
        End Try
    End Sub

    Private Sub Incluir()
        Dim dt As DataTable = ObterEstrutura()
        oMensagem.ClearMessage()
        Preencher(dt)
        pnlCabecalho.Enabled = True
        lblOrdemServico.Text = "000000"

        containerAnexados.Visible = False
        btnAlterar.Visible = False
        btnNovo.Visible = False
        btnAnexar.Visible = True
    End Sub

    Private Function ObterEstrutura(Optional ByVal bAdicionarLinha As Boolean = True) As DataTable

        Dim dt As New DataTable

        dt.Columns.Add("Atendente")

        'Ordem de Servi;o
        dt.Columns.Add("CodigoStatusOS")
        dt.Columns.Add("DescricaoStatusOS")
        dt.Columns.Add("NumeroOS")
        dt.Columns.Add("NumeroChamado")
        dt.Columns.Add("ItemChamado")
        dt.Columns.Add("DataEmissao", GetType(DateTime))
        dt.Columns.Add("HoraEmissao")
        dt.Columns.Add("CodigoOrigem")
        dt.Columns.Add("DescricaoOrigem")
        dt.Columns.Add("CodigoTipoChamado")
        dt.Columns.Add("TipoChamado")
        dt.Columns.Add("NomeContato")
        dt.Columns.Add("TelefoneContato")
        dt.Columns.Add("OSParceiro")
        dt.Columns.Add("OSAntiga")

        'Cliente
        dt.Columns.Add("FilialCliente")
        dt.Columns.Add("CodigoCliente")
        dt.Columns.Add("NomeCliente")
        dt.Columns.Add("CGCCliente")
        dt.Columns.Add("LojaCliente")

        'Item da Ordem de Serviço
        dt.Columns.Add("ProximoAtendimento")
        dt.Columns.Add("ItemOS")
        dt.Columns.Add("DescricaoSituacaoOS")
        dt.Columns.Add("CodigoSituacaoOS")
        dt.Columns.Add("CodigoClassificacao")
        dt.Columns.Add("DescricaoClassificacao")
        dt.Columns.Add("CodigoOcorrencia")
        dt.Columns.Add("DescricaoOcorrencia")
        dt.Columns.Add("CodigoEtapa")
        dt.Columns.Add("DescricaoEtapa")
        dt.Columns.Add("DescricaoSituacao")
        dt.Columns.Add("CodigoSituacao")
        dt.Columns.Add("D_E_L_E_T_")
        dt.Columns.Add("DataGarantia")
        dt.Columns.Add("Versao")


        'Produto
        dt.Columns.Add("CodigoProduto")
        dt.Columns.Add("DescricaoProduto")
        dt.Columns.Add("NumeroSerieProduto")
        dt.Columns.Add("DescrGarantia")

        'Sub Item
        dt.Columns.Add("SubItem")
        dt.Columns.Add("DescricaoSubItem")
        dt.Columns.Add("CodigoServico")
        dt.Columns.Add("DescricaoServico")
        dt.Columns.Add("Quantidade")
        dt.Columns.Add("ValorUnitario")
        dt.Columns.Add("Total")

        dt.Columns.Add("Comentario")

        If bAdicionarLinha Then
            AdicionarItem(dt)
        End If
        Return dt

    End Function

    Private Sub Selecionar(ByVal sNumero As String)
        Dim ct As New ctlOrdemServico

        Dim reader As SqlDataReader = Nothing
        Dim readerAnexos As SqlDataReader = Nothing

        If Request("Numero") IsNot Nothing Then
            If Request("Numero").Trim.Contains("H") Then
                reader = ct.Selecionar(sNumero, , False)
            Else
                reader = ct.Selecionar(sNumero)
                readerAnexos = ct.SelecionarAnexos(sNumero)
            End If
        Else
            reader = ct.Selecionar(sNumero)
            readerAnexos = ct.SelecionarAnexos(sNumero)
        End If

        If Not reader.HasRows() Then
            Throw New Exception("A Ordem de Serviço '" + sNumero + "' não foi localizada.")
        Else
            Dim dt As New DataTable
            Dim dtAnexos As New DataTable
            If reader IsNot Nothing Then
                dt.Load(reader)
                reader.Close()
                If readerAnexos IsNot Nothing Then
                    dtAnexos.Load(readerAnexos)
                    readerAnexos.Close()
                End If
            End If
            Preencher(dt)

            If dtAnexos.Rows.Count > 0 Then
                PreencherAnexos(dtAnexos)
                containerAnexados.Visible = True
                containerAnexo.Visible = False
            Else
                containerAnexados.Visible = False
            End If
        End If
    End Sub

    Private Sub PreencherAnexos(Optional ByVal DtAnexos As DataTable = Nothing)

        Dim vConteudo As String = ""
        Dim urlArquiv As String = "http://www.intermed.com.br/osonline/OS_downloads/"

        For Each dr As DataRow In DtAnexos.Rows

            vConteudo += "<div class='anexo'>"
            vConteudo += "<img alt='Documentos Anexados a OS' src='App_Themes/padrao/ImgAnexo.gif' class='iconeAnexo' />"
            vConteudo += "<a href='" + urlArquiv + dr("NomeArquivo").ToString + "'" + " runat='server' visible='false' style='text-decoration: underline; margin-left: 10px;'>" + dr("DescArquivo").ToString + "</a>"
            vConteudo += "</div>"
        Next

        LtlConteudo.Text = vConteudo

    End Sub

    Private Sub Preencher(Optional ByVal dt As DataTable = Nothing)

        'preenche dados a partir do xml gravado
        If File.Exists(NomeArquivoXml()) Then
            Dim ds = New DataSet
            ds.ReadXml(NomeArquivoXml())
            If (ds.Tables.Count > 0) Then
                If String.IsNullOrEmpty(dt.Rows(0).Item("NumeroOs").ToString) Then dt.Rows(0).Item("NumeroOs") = "000000"
                If ds.Tables(0).Rows(0).Item("Licenciado").ToString.Trim = HttpContext.Current.User.Identity.Name.Trim _
                    And dt.Rows(0).Item("NumeroOs").ToString = ds.Tables(0).Rows(0).Item("NumeroOs").ToString Then
                    dt = ds.Tables(0)

                    'se for inclusao desabilita alteracao de cliente - porque produtos tem relacao direta com cliente
                    If String.IsNullOrEmpty(dt.Rows(0).Item("Atendente").ToString) Then
                        txtCliente.Enabled = False
                    End If

                    'printar msg na tela informando que existem dados alterados e não salvos
                    Dim oRet As New ctlRetornoGenerico
                    oRet.Mensagem = "Existem itens alterados e/ou incluídos que não foram salvos no servidor. Por favor clicar no botão confirmar."
                    oRet.Sucesso = True
                    oMensagem.SetMessage(oRet)

                End If
            End If
        End If

        Dim dtII As New DataTable
        dtII = dt

        lblChamado.Text = dt.Rows(0).Item("NumeroChamado").ToString
        lblOrdemServico.Text = dt.Rows(0).Item("NumeroOs").ToString

        grdItens.DataSource = dtII
        grdItens.DataBind()


        If VarType(dt.Rows(0).Item("DataEmissao")) = CDbl(Str(8)) Then
            lblEmissao.Text = dt.Rows(0).Item("DataEmissao").ToString()
        Else
            lblEmissao.Text = DirectCast(dt.Rows(0).Item("DataEmissao"), Date).ToString("dd/MM/yyyy")
        End If

        txtCliente.Codigo = dt.Rows(0).Item("CodigoCliente").ToString
        txtCliente.Loja = dt.Rows(0).Item("LojaCliente").ToString
        txtCliente.Nome = dt.Rows(0).Item("NomeCliente").ToString
        txtCliente.CPF_CNPJ = dt.Rows(0).Item("CGCCliente").ToString
        txtContato.Text = dt.Rows(0).Item("NomeContato").ToString
        oPhoneBox.Text = dt.Rows(0).Item("TelefoneContato").ToString
        If String.IsNullOrEmpty(lblChamado.Text.Trim) Then
            grdItens.Columns(6).Visible = False
        End If
        txtOSParceiro.Text = dt.Rows(0).Item("OSParceiro").ToString
        If Not String.IsNullOrEmpty(dt.Rows(0).Item("OSAntiga").ToString.Trim) Then
            txtOSAntiga.Text = dt.Rows(0).Item("OSAntiga").ToString
            lblOSAntiga.Visible = True
            txtOSAntiga.Visible = True
        End If
        If Not String.IsNullOrEmpty(dt.Rows(0).Item("CodigoSituacao").ToString) Then
            ViewState.Add("CodigoSituacao", CInt(dt.Rows(0).Item("CodigoSituacao").ToString))
        End If
        'If Not String.IsNullOrEmpty(dt.Rows(0).Item("CodigoOrigem").ToString) Then
        '    drpOrigem.SelectedValue = dt.Rows(0).Item("CodigoOrigem").ToString
        'End If
        drpTipo.SelectedValue = dt.Rows(0).Item("CodigoTipoChamado").ToString
        btnImprimir.CommandArgument = dt.Rows(0).Item("NumeroOs").ToString.Trim

        Dim drpSituacao As DropDownList
        For Each gr As GridViewRow In grdItens.Rows
            drpSituacao = DirectCast(gr.FindControl("drpSituacao"), DropDownList)
            If drpSituacao.SelectedValue.Trim = "4" Then
                btnImpimirAtend.Visible = True
            End If
            If drpSituacao.SelectedValue = "3" Or drpSituacao.SelectedValue = "2" Then
                btnImpimirAtend.Visible = True
                DirectCast(gr.FindControl("btnAtender"), AutoHideButton).Visible = False
            End If
        Next

        If dt.Rows(0).Item("Atendente").ToString.Trim = HttpContext.Current.User.Identity.Name.Trim Then
            HabilitarModo("A")
        Else
            HabilitarModo("I")
        End If

        ViewState.Add("OrdemServico", dt)
    End Sub

    Private Sub AdicionarItem(ByRef dt As DataTable, Optional ByVal nIndice As Integer = 0)

        Dim nItem As Integer = 0
        Dim dr As DataRow = dt.NewRow
        dr("Atendente") = ""

        'Ordem de Servico
        dr("CodigoStatusOS") = ""
        dr("DescricaoStatusOS") = ""
        dr("NumeroOS") = ""
        dr("NumeroChamado") = ""
        dr("ItemChamado") = ""
        dr("DataEmissao") = Now.Date
        dr("HoraEmissao") = ""
        dr("CodigoOrigem") = ""
        dr("DescricaoOrigem") = ""
        dr("CodigoTipoChamado") = ""
        dr("TipoChamado") = ""
        dr("NomeContato") = ""
        dr("TelefoneContato") = ""
        dr("OSParceiro") = ""

        'Cliente
        dr("FilialCliente") = ""
        dr("CodigoCliente") = ""
        dr("NomeCliente") = ""
        dr("CGCCliente") = ""
        dr("LojaCliente") = ""

        'Item da Ordem de Serviço
        dr("ItemOS") = ObterIndice()
        dr("DescricaoSituacaoOS") = ""
        dr("CodigoSituacaoOS") = ""
        dr("CodigoClassificacao") = ""
        dr("DescricaoClassificacao") = ""
        dr("CodigoOcorrencia") = ""
        dr("DescricaoOcorrencia") = ""
        dr("CodigoEtapa") = ""
        dr("DescricaoEtapa") = ""
        dr("DescricaoSituacao") = ""
        dr("CodigoSituacao") = ""
        dr("D_E_L_E_T_") = ""
        dr("DataGarantia") = Date.Today
        dr("Versao") = ""

        'Produto
        dr("CodigoProduto") = ""
        dr("DescricaoProduto") = ""
        dr("NumeroSerieProduto") = ""
        dr("DescrGarantia") = ""

        '!?
        'dr("NumeroChamado1") = ""
        dr("Comentario") = ""

        If nIndice = 0 Then
            dt.Rows.Add(dr)
        Else
            dt.Rows.InsertAt(dr, nIndice)
        End If

    End Sub

    Private Sub grdItens_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdItens.RowCommand
        Dim gv As GridView = DirectCast(sender, GridView)
        Dim dt As DataTable = DirectCast(ViewState("OrdemServico"), DataTable)
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
            ViewState.Add("OrdemServico", dt)
            grdItens.DataSource = dt
            grdItens.DataBind()
            'nunca permitir alteração da situação quando for inclusao
            If e.CommandName.Equals("New") And CInt(lblOrdemServico.Text).Equals(0) Then
                DirectCast(grdItens.Rows(nIndice).FindControl("drpSituacao"), DropDownList).Enabled = False
            End If
        End If
    End Sub

    Private Sub grdItens_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles grdItens.RowDeleting
    End Sub

    Private Sub grdItens_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles grdItens.RowEditing
        If CInt(lblOrdemServico.Text).Equals(0) Then
            DirectCast(grdItens.Rows(e.NewEditIndex).FindControl("drpSituacao"), DropDownList).Enabled = False
        End If
    End Sub

    Private Sub grdItens_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles grdItens.RowUpdating
        'CASO NAO EXISTA UM CLIENTE PREENCHIDO E A SESSION VIER COM DADOS DA CONSULTA DA BASE INSTALADA PREENCHER
        If String.IsNullOrEmpty(txtCliente.Text) And TypeOf (HttpContext.Current.Session("oCliente")) Is Object Then
            Dim oCliente As New ctlCliente
            oCliente = DirectCast(HttpContext.Current.Session("oCliente"), ctlCliente)
            txtCliente.Codigo = oCliente.Codigo
            txtCliente.Loja = oCliente.Loja
            txtCliente.Nome = oCliente.Nome
            txtCliente.CPF_CNPJ = oCliente.CNPJ
            txtCliente.ExibirBusca = False
            txtCliente.Enabled = False
            Session.Add("CodigoCliente", oCliente.Codigo)
            Session.Remove("oCliente")
            pnlUpdate.Update()
        End If
    End Sub

    Private Sub grdItens_RowCancelingEdit(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCancelEditEventArgs) Handles grdItens.RowCancelingEdit
    End Sub

    Private Sub AtualizarItem(ByVal gr As GridViewRow, ByVal nIndice As Integer)
        Dim oBaseInstaladaView = DirectCast(gr.FindControl("oBaseInstaladaView"), BaseInstaladaView)
        Dim txtPecaSerie = DirectCast(gr.FindControl("txtPecaSerie"), TextBox)
        Dim drpClassificacao = DirectCast(gr.FindControl("drpClassificacao"), DropDownList)
        Dim drpSituacao = DirectCast(gr.FindControl("drpSituacao"), DropDownList)
        Dim drpOcorrencia = DirectCast(gr.FindControl("drpOcorrencia"), DropDownList)
        Dim drpEtapa = DirectCast(gr.FindControl("drpEtapa"), DropDownList)
        Dim lblChamadoPorItem = DirectCast(gr.FindControl("lblChamado"), Label)
        Dim dt As DataTable
        dt = DirectCast(ViewState("OrdemServico"), DataTable)
        Dim dr As DataRow = dt.NewRow()
        dr.ItemArray = dt.Rows(nIndice).ItemArray
        dr("CodigoProduto") = oBaseInstaladaView.Codigo.Trim
        dr("DescricaoProduto") = oBaseInstaladaView.Nome.Trim
        dr("NumeroSerieProduto") = oBaseInstaladaView.Serie.Trim
        dr("DescrGarantia") = oBaseInstaladaView.Garantia.Trim
        dr("CodigoClassificacao") = drpClassificacao.SelectedValue
        If Not String.IsNullOrEmpty(lblChamadoPorItem.Text.Trim) Then
            dr("DescricaoClassificacao") = drpClassificacao.SelectedItem.Text.Trim
        End If
        dr("CodigoSituacaoOS") = drpSituacao.SelectedValue
        dr("CodigoOcorrencia") = drpOcorrencia.SelectedValue
        dr("DescricaoOcorrencia") = drpOcorrencia.SelectedItem.Text
        dr("CodigoEtapa") = drpEtapa.SelectedValue
        dr("DescricaoEtapa") = drpEtapa.SelectedItem.Text
        dt.Rows.RemoveAt(nIndice)
        dt.Rows.InsertAt(dr, nIndice)
        dt.AcceptChanges()
    End Sub

    Private Sub RemoverItem(ByVal gr As GridViewRow, ByVal nIndice As Integer)
        Dim lblRegistro = DirectCast(gr.FindControl("lblRegistro"), Label)
        Dim dt As DataTable
        dt = DirectCast(ViewState("OrdemServico"), DataTable)
        Dim dr As DataRow = dt.NewRow()
        dr.ItemArray = dt.Rows(nIndice).ItemArray
        dt.Rows.RemoveAt(nIndice)
        If Not String.IsNullOrEmpty(lblRegistro.Text) Then
            dr("D_E_L_E_T_") = "*"
            dt.Rows.InsertAt(dr, nIndice)
            dt.AcceptChanges()
        End If
    End Sub

    Private Sub btnIncluir_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnIncluir.Click
        VerificaBaseInstaladaOS(True)
    End Sub

    Private Sub btnNovo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNovo.Click
        Response.Redirect("DetalheOrdemServico.aspx")
    End Sub

    Private Sub btnAlterar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAlterar.Click
        VerificaBaseInstaladaOS(False)
    End Sub

    Private Sub VerificaBaseInstaladaOS(ByVal bIncluiOS As Boolean)

        Dim oRet As New ctlRetornoGenerico

        Try
            Dim ct As New ctlOrdemServico
            Dim bi As New ctlBaseInstalada
            Dim bAlteraBase As Boolean = False
            Dim sSerie As String = ""
            Dim sListaSeries As String = ""

            'oRet = ValidaArquivosAnexo()

            oRet = Validar()
            If oRet.Sucesso Then
                Dim dt As DataTable = DirectCast(ViewState("OrdemServico"), DataTable)
                Dim oOrdemServico As New ctlOrdemServico
                Dim oCliente As New ctlCliente(txtCliente.Codigo, txtCliente.Loja, txtCliente.Nome, txtCliente.CPF_CNPJ)
                'chamada da criacao do xml contendo todos os dados da pagina
                CriarXmlComItensOS(dt)

                oOrdemServico.Chamado = lblChamado.Text
                oOrdemServico.OrdemServico = lblOrdemServico.Text
                oOrdemServico.Cliente = oCliente
                oOrdemServico.Contato = txtContato.Text.Trim
                oOrdemServico.Telefone = oPhoneBox.Text.Trim
                oOrdemServico.OSParceiro = txtOSParceiro.Text
                oOrdemServico.CodigoTipo = drpTipo.SelectedValue
                oOrdemServico.CodigoOrigem = "" 'drpOrigem.SelectedValue
                oOrdemServico.PutItens(dt)

                If UploadAnexoUm.HasFile Then
                    If Not Session("UploadAnexoUm") Is Nothing Then
                        Dim arq As String = Session("UploadAnexoUm").ToString
                        oOrdemServico.Anexos.Add(New ctlAnexo(arq, TxtDescArquivoUm.Text))
                    Else
                        oOrdemServico.Anexos.Add(New ctlAnexo(UploadAnexoUm.FileName.ToString(), TxtDescArquivoUm.Text))
                    End If
                End If

                If UploadAnexoDois.HasFile Then
                    If Not Session("UploadAnexoDois") Is Nothing Then
                        Dim arq As String = Session("UploadAnexoDois").ToString
                        oOrdemServico.Anexos.Add(New ctlAnexo(arq, TxtDescArquivoDois.Text))
                    Else
                        oOrdemServico.Anexos.Add(New ctlAnexo(UploadAnexoDois.FileName.ToString(), TxtDescArquivoDois.Text))
                    End If
                End If

                If UploadAnexoTres.HasFile Then
                    If Not Session("UploadAnexoTres") Is Nothing Then
                        Dim arq As String = Session("UploadAnexoTres").ToString
                        oOrdemServico.Anexos.Add(New ctlAnexo(arq, TxtDescArquivoTres.Text))
                    Else
                        oOrdemServico.Anexos.Add(New ctlAnexo(UploadAnexoTres.FileName.ToString(), TxtDescArquivoTres.Text))
                    End If
                End If

                'redireciona para tela de Alteração de Base Instalada
                For Each dr As DataRow In dt.Rows
                    If (dr.Item("CodigoOcorrencia").ToString = "000006" Or dr.Item("CodigoOcorrencia").ToString = "000201") And dr.Item("D_E_L_E_T_").ToString.Trim = "" Then 'codigo de ocorrencia 000006 = instalacao ou 000201 = servico - instalacao
                        sSerie = dr.Item("NumeroSerieProduto").ToString.Trim
                        bAlteraBase = bi.BaseNecessitaCadastro(sSerie)
                        If bAlteraBase Then
                            sListaSeries += sSerie & ";"
                        End If
                    End If
                Next
                If sListaSeries.Trim <> "" Then
                    Session.Add("oChamado", oOrdemServico)
                    Session.Add("oItens", dt)
                    Session.Add("bIncluiChamado", bIncluiOS)
                    Session.Add("sListaSeries", sListaSeries)
                    Session.Add("sTipo", "OS")
                    Response.Redirect("AlteracaoBaseInstalada.aspx")
                End If

                oRet = GravarOS(oOrdemServico, oCliente, dt, True, bIncluiOS)
            End If

        Catch ex As Exception
            oRet.Sucesso = False
            If bIncluiOS Then
                ctlUtil.EscreverLogErro("DetalheOrdemServico - btnIncluir_click: " & ex.Message())
            Else
                ctlUtil.EscreverLogErro("DetalheOrdemServico - btnAlterar_click: " & ex.Message())
            End If
            oRet.Mensagem = ctlUtil.sMsgErroPadrao

        End Try
        oMensagem.SetMessage(oRet)
    End Sub

    Public Function GravarOS(ByVal oOrdemServico As ctlOrdemServico, ByVal oCliente As ctlCliente, ByVal dt As DataTable, ByVal bSeleciona As Boolean, ByVal bIncluiOS As Boolean) As ctlRetornoGenerico

        Dim oRet As New ctlRetornoGenerico

        Try
            Dim ct As New ctlOrdemServico

            If bIncluiOS Then
                oRet = ct.Incluir(oOrdemServico)
            Else
                oRet = ct.Alterar(oOrdemServico)
            End If

            If oRet.Sucesso Then
                'caso inclusao for efetuada com sucesso deleta arquivo xml
                DeletaArquivoXml()

                Dim sNumSerie As String = ""
                Dim sCodProduto As String = ""
                Dim nrOS As String = oRet.Chave.Substring(2)
                Dim bi As New ctlBaseInstalada

                'INCLUIR DADOS NA GRUPO X SERIE
                For Each dr As DataRow In dt.Rows
                    sNumSerie += dr("NumeroSerieProduto").ToString & ";"
                    sCodProduto += dr("CodigoProduto").ToString & ";"
                Next
                bi.IncluirItemGrupoSerie(oCliente, sNumSerie, sCodProduto, nrOS, "OS")

                If bSeleciona Then
                    Selecionar(nrOS)
                End If
            End If
        Catch ex As Exception
            oRet.Sucesso = False
            ctlUtil.EscreverLogErro("DetalheOrdemServico - GravarOS: " & ex.Message())
            oRet.Mensagem = ctlUtil.sMsgErroPadrao

        End Try
        Return oRet

    End Function

    Private Function Validar() As ctlRetornoGenerico
        Dim oRet As New ctlRetornoGenerico
        'oRet.Sucesso = False

        'Variáveis criadas para validação de extensão ao anexar documento
        Dim appSettings As NameValueCollection = ConfigurationManager.AppSettings
        'Variáveis criadas para validação de extensão ao anexar documento
        Dim anexoUm As String = Path.GetExtension(UploadAnexoUm.FileName.ToString())
        Dim anexoDois As String = Path.GetExtension(UploadAnexoDois.FileName.ToString())
        Dim anexoTres As String = Path.GetExtension(UploadAnexoTres.FileName.ToString())
        'Variáveis criadas para validação de extensão ao anexar documento

        Dim sChave = appSettings("Extensao").Split(CChar(","))

        If drpTipo.SelectedIndex < 1 Then
            oRet.Mensagem = "O tipo não foi preenchido."
        ElseIf Not txtCliente.IsValid Then
            oRet.Mensagem = "Selecione um cliente."
        ElseIf txtContato.Text.Trim = "" Then 'And Not String.IsNullOrEmpty(lblChamado.Text.Trim) Then
            oRet.Mensagem = "Forneça o nome do contato."
        ElseIf oPhoneBox.Text.Trim = "" Then 'And Not String.IsNullOrEmpty(lblChamado.Text.Trim) Then
            oRet.Mensagem = "Forneça um telefone de contato."
        End If

        'Validação para anexo de arquivo
        If UploadAnexoUm.HasFile = True Then

            '1° Validação para anexo de arquivo
            If UploadAnexoUm.FileName = UploadAnexoDois.FileName Or UploadAnexoUm.FileName = UploadAnexoTres.FileName Then
                oRet.Mensagem = "Não é possível anexar dois arquvivos iguais"
                oRet.Sucesso = False
            Else
                oRet.Sucesso = True
                If String.IsNullOrEmpty(TxtDescArquivoUm.Text) = True Then
                    oRet.Mensagem = "Ao anexar um arquivo o mesmo deve possuir uma descrição"
                    oRet.Sucesso = False

                Else
                    oRet.Sucesso = True
                    If Not oRet.Sucesso = False Then
                        For Each item In sChave
                            If anexoUm = item Then
                                Try
                                    Dim contador As Integer
                                    Dim arquiv As String
                                    Dim tamanhoVetor As Integer
                                    Dim nomeArquivoUm As String = ""
                                    Dim tamanhoExtensao As Integer = anexoUm.Length

                                    Dim fileBreak As String() = UploadAnexoUm.FileName.Split(New Char() {"."c})

                                    tamanhoVetor = fileBreak.Length

                                    'arquiv = "C:/Projetos/osonline/Desenv/Fontes/OS_downloads/" & UploadAnexoUm.FileName.ToString
                                    arquiv = "E:/PROJETO_OSONLINE/osonline/OS_downloads_temp/" & UploadAnexoUm.FileName.ToString

                                    nomeArquivoUm = UploadAnexoUm.FileName.ToString

                                    If "." & fileBreak(tamanhoVetor - 1) = anexoUm Then
                                        While System.IO.File.Exists(arquiv) Or ExisteAnexo(nomeArquivoUm)
                                            contador += 1

                                            nomeArquivoUm = UploadAnexoUm.FileName.ToString.Remove(UploadAnexoUm.FileName.Length - tamanhoExtensao) & "(" & contador.ToString() & ")" & "." & fileBreak(tamanhoVetor - 1)
                                            'arquiv = "C:/Projetos/osonline/Desenv/Fontes/OS_downloads/" & nomeArquivoUm
                                            arquiv = "E:/PROJETO_OSONLINE/osonline/OS_downloads_temp/" & nomeArquivoUm
                                        End While
                                    End If

                                    UploadAnexoUm.SaveAs(arquiv)

                                    Session.Add("UploadAnexoUm", nomeArquivoUm)

                                Catch ex As Exception
                                    Throw ex
                                End Try

                                Exit For
                            Else
                                oRet.Sucesso = False
                                oRet.Mensagem = "Extensão do 1° arquivo anexado inválida"
                            End If
                        Next
                    End If
                End If
            End If
        End If

        If UploadAnexoDois.HasFile = True Then

            '2° Validação para anexo de arquivo
            If UploadAnexoDois.FileName = UploadAnexoUm.FileName Or UploadAnexoDois.FileName = UploadAnexoTres.FileName Then
                oRet.Mensagem = "Não é possível anexar dois arquvivos iguais"
                oRet.Sucesso = False
            Else
                oRet.Sucesso = True
                If String.IsNullOrEmpty(TxtDescArquivoDois.Text) = True Then
                    oRet.Mensagem = "Ao anexar um arquivo o mesmo deve possuir uma descrição"
                    oRet.Sucesso = False

                Else
                    oRet.Sucesso = True
                    If Not oRet.Sucesso = False Then
                        For Each item In sChave
                            If anexoDois = item Then
                                Try
                                    Dim contador As Integer
                                    Dim arquiv As String
                                    Dim tamanhoVetor As Integer
                                    Dim nomeArquivoDois As String = ""
                                    Dim tamanhoExtensao As Integer = anexoDois.Length

                                    Dim fileBreak As String() = UploadAnexoDois.FileName.Split(New Char() {"."c})

                                    tamanhoVetor = fileBreak.Length

                                    arquiv = "E:/PROJETO_OSONLINE/osonline/OS_downloads_temp/" & UploadAnexoDois.FileName.ToString
                                    nomeArquivoDois = UploadAnexoDois.FileName.ToString

                                    If "." & fileBreak(tamanhoVetor - 1) = anexoDois Then
                                        While System.IO.File.Exists(arquiv) Or ExisteAnexo(nomeArquivoDois)
                                            contador += 1

                                            nomeArquivoDois = UploadAnexoDois.FileName.ToString.Remove(UploadAnexoDois.FileName.Length - tamanhoExtensao) & "(" & contador.ToString() & ")" & "." & fileBreak(tamanhoVetor - 1)
                                            arquiv = "E:/PROJETO_OSONLINE/osonline/OS_downloads_temp/" & nomeArquivoDois
                                        End While
                                    End If

                                    UploadAnexoDois.SaveAs(arquiv)
                                    Session.Add("UploadAnexoDois", nomeArquivoDois)

                                Catch ex As Exception
                                    Throw ex
                                End Try

                                Exit For
                            Else
                                oRet.Sucesso = False
                                oRet.Mensagem = "Extensão do 2° arquivo anexado inválida"
                            End If
                        Next
                    End If
                End If
            End If
        End If

        If UploadAnexoTres.HasFile = True Then

            '3° Validação para anexo de arquivo
            If UploadAnexoTres.FileName = UploadAnexoUm.FileName Or UploadAnexoTres.FileName = UploadAnexoDois.FileName Then
                oRet.Mensagem = "Não é possível anexar dois arquvivos iguais"
                oRet.Sucesso = False
            Else
                oRet.Sucesso = True
                If String.IsNullOrEmpty(TxtDescArquivoTres.Text) = True Then
                    oRet.Mensagem = "Ao anexar um arquivo o mesmo deve possuir uma descrição"
                    oRet.Sucesso = False

                Else
                    oRet.Sucesso = True
                    If Not oRet.Sucesso = False Then
                        For Each item In sChave
                            If anexoTres = item Then
                                Try
                                    Dim contador As Integer
                                    Dim arquiv As String
                                    Dim tamanhoVetor As Integer
                                    Dim nomeArquivoTres As String = ""
                                    Dim tamanhoExtensao As Integer = anexoTres.Length

                                    Dim fileBreak As String() = UploadAnexoTres.FileName.Split(New Char() {"."c})

                                    tamanhoVetor = fileBreak.Length

                                    arquiv = "E:/PROJETO_OSONLINE/osonline/OS_downloads_temp/" & UploadAnexoTres.FileName.ToString
                                    nomeArquivoTres = UploadAnexoTres.FileName.ToString

                                    If "." & fileBreak(tamanhoVetor - 1) = anexoTres Then
                                        While System.IO.File.Exists(arquiv) Or ExisteAnexo(nomeArquivoTres)
                                            contador += 1

                                            nomeArquivoTres = UploadAnexoTres.FileName.ToString.Remove(UploadAnexoTres.FileName.Length - tamanhoExtensao) & "(" & contador.ToString() & ")" & "." & fileBreak(tamanhoVetor - 1)
                                            arquiv = "E:/PROJETO_OSONLINE/osonline/OS_downloads_temp/" & nomeArquivoTres
                                        End While
                                    End If

                                    UploadAnexoTres.SaveAs(arquiv)
                                    Session.Add("UploadAnexoDois", nomeArquivoTres)

                                Catch ex As Exception
                                    Throw ex
                                End Try

                                Exit For
                            Else
                                oRet.Sucesso = False
                                oRet.Mensagem = "Extensão do 3° arquivo anexado inválida"
                            End If
                        Next
                    End If
                End If
            End If
        End If

        'Itens inválido
        If Not oRet.Sucesso = False Then

            If Not ValidarItens(oRet) Then

            Else
                oRet.Sucesso = True
            End If
        End If
        Return oRet
    End Function

    Private Function ExisteAnexo(ByVal nomeArquivo As String) As Boolean

        Dim ct As New ctlOrdemServico
        Dim readerAnexo As SqlDataReader = Nothing
        Dim bRet As Boolean = False

        readerAnexo = ct.ValidarNomeAnexo(nomeArquivo)

        bRet = readerAnexo.HasRows

        Return bRet

    End Function

    Private Function ValidarItens(ByVal oRet As ctlRetornoGenerico) As Boolean
        Dim bRet As Boolean = False
        Dim lblItemOS As Label
        Dim drpSituacao As DropDownList
        Dim oBaseInstaladaBox As BaseInstaladaBox
        Dim drpClassificacao As DropDownList
        Dim drpOcorrencia As DropDownList
        Dim drpEtapa As DropDownList
        Dim lblD_E_L_E_T_ As Label
        Dim lblChamadoPorItem As Label

        oRet.Mensagem = ""
        If grdItens.EditIndex > -1 Then
            oRet.Mensagem = "A linha " + (grdItens.EditIndex + 1).ToString + " encontra-se em edição e precisa ser confirmada ou cancelada."
        Else
            For Each gr As GridViewRow In grdItens.Rows
                lblItemOS = DirectCast(gr.FindControl("lblItemOS"), Label)
                drpSituacao = DirectCast(gr.FindControl("drpSituacao"), DropDownList)
                oBaseInstaladaBox = DirectCast(gr.FindControl("oBaseInstaladaBox"), BaseInstaladaBox)
                drpClassificacao = DirectCast(gr.FindControl("drpClassificacao"), DropDownList)
                drpOcorrencia = DirectCast(gr.FindControl("drpOcorrencia"), DropDownList)
                drpEtapa = DirectCast(gr.FindControl("drpEtapa"), DropDownList)
                lblD_E_L_E_T_ = DirectCast(gr.FindControl("lblD_E_L_E_T_"), Label)
                lblChamadoPorItem = DirectCast(gr.FindControl("lblChamado"), Label)

                If lblD_E_L_E_T_.Text.Trim <> "*" Then
                    If String.IsNullOrEmpty(oBaseInstaladaBox.Serie) Then
                        oRet.Mensagem = "O número de série não foi informado na linha " + (gr.RowIndex + 1).ToString + "."
                    ElseIf String.IsNullOrEmpty(oBaseInstaladaBox.Codigo) Then
                        oRet.Mensagem = "A linha " + (gr.RowIndex + 1).ToString + " não contém um Produto válido. Favor informar um Produto."
                    ElseIf Not String.IsNullOrEmpty(lblChamadoPorItem.Text.Trim) And drpClassificacao.SelectedIndex < 1 Then
                        oRet.Mensagem = "A classificação não foi preenchida na linha " + (gr.RowIndex + 1).ToString + "."
                    ElseIf drpOcorrencia.SelectedIndex < 1 Then
                        oRet.Mensagem = "A ocorrência não foi preenchida na linha " + (gr.RowIndex + 1).ToString + "."
                    ElseIf drpEtapa.SelectedIndex < 1 Then
                        oRet.Mensagem = "A etapa não foi preenchida na linha " + (gr.RowIndex + 1).ToString + "."
                    End If
                    Dim duplicidade As String = ProcurarMesmaOcorrencia(lblItemOS.Text.Trim, oBaseInstaladaBox.Serie, drpOcorrencia.SelectedValue.Trim, lblD_E_L_E_T_.Text.Trim)
                    If duplicidade <> "" Then
                        oRet.Mensagem = "O equipamento e a ocorrência do item " + lblItemOS.Text + " é a mesma do item " + duplicidade + ", favor alterar a ocorrência."
                        Exit For
                    End If
                End If

            Next
        End If
        If oRet.Mensagem.Trim.Length = 0 Then
            bRet = True
        End If
        Return bRet
    End Function

    ''' <summary>
    ''' Verifica se o número de série e a ocorrência são iguais em itens diferentes.
    ''' </summary>
    ''' <param name="nrItem"></param>
    ''' <param name="nrSerie"></param>
    ''' <param name="ocorrencia"></param>
    ''' <param name="D_E_L_E_T_"></param>
    ''' <returns>Retorna o número do registro duplicado, se não existir duplicidade retorna em branco</returns>
    ''' <remarks></remarks>
    ''' 

    Private Function ProcurarMesmaOcorrencia(ByVal nrItem As String, ByVal nrSerie As String, ByVal ocorrencia As String, ByVal D_E_L_E_T_ As String) As String
        Dim dt As DataTable = DirectCast(ViewState("OrdemServico"), DataTable)
        dt.DefaultView.RowFilter = "D_E_L_E_T_ <> '*'"
        Dim str As String = ""
        For indice = 0 To dt.Rows.Count - 1
            Dim dr As DataRow = dt.Rows(indice)
            If nrSerie = dr("NumeroSerieProduto").ToString.Trim AndAlso ocorrencia = dr("CodigoOcorrencia").ToString.Trim _
                AndAlso D_E_L_E_T_ <> "*" AndAlso nrItem <> dr("ItemOS").ToString.Trim Then
                str = dr("ItemOS").ToString.Trim
                Exit For
            End If
        Next
        Return str
    End Function

    Private Sub grdItens_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdItens.RowDataBound

        Dim nColunaTotal As Integer = e.Row.Cells.Count - 1
        Dim bAtivo As Boolean = False

        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim lblItemOS = DirectCast(e.Row.FindControl("lblItemOS"), Label)
            Dim oBaseInstaladaBox As BaseInstaladaBox = DirectCast(e.Row.FindControl("oBaseInstaladaBox"), BaseInstaladaBox)
            Dim drpClassificacao As DropDownList = DirectCast(e.Row.FindControl("drpClassificacao"), DropDownList)
            Dim drpSituacao As DropDownList = DirectCast(e.Row.FindControl("drpSituacao"), DropDownList)
            Dim drpOcorrencia As DropDownList = DirectCast(e.Row.FindControl("drpOcorrencia"), DropDownList)
            Dim txtServico As TextBox = DirectCast(e.Row.FindControl("txtServico"), TextBox)
            Dim drpEtapa As DropDownList = DirectCast(e.Row.FindControl("drpEtapa"), DropDownList)
            Dim btnAtender As AutoHideButton = DirectCast(e.Row.FindControl("btnAtender"), AutoHideButton)
            Dim btnPrint As AutoHideButton = DirectCast(e.Row.FindControl("btnPrint"), AutoHideButton)
            Dim lblD_E_L_E_T_ As Label = DirectCast(e.Row.FindControl("lblD_E_L_E_T_"), Label)
            Dim dt As DataTable = DirectCast(ViewState("OrdemServico"), DataTable)

            Dim reader As SqlDataReader = Nothing
            Dim ct As New ctlChamadoTecnico
            Dim os As New ctlOrdemServico
            Dim sSelecionado As String

            'USADO PARA NAO MOSTRAR MAIS OS ITENS INATIVOS A PARTIR DA DATA DE IMPANTACAO, PARA NAO PERDER HISTORICO
            If IsDate(lblEmissao.Text) Then
                If Date.Compare(CDate(lblEmissao.Text), CDate(Session("DataCorte"))) >= 0 Then
                    bAtivo = True
                End If
            End If

            'Preenche o drop dentro do grid
            If drpSituacao IsNot Nothing Then
                drpSituacao.Items.Insert(0, "O.S")
                drpSituacao.Items(0).Value = "1"
                drpSituacao.Items.Insert(1, "Pedido Gerado")
                drpSituacao.Items(1).Value = "2"
                drpSituacao.Items.Insert(1, "Em atendimento")
                drpSituacao.Items(1).Value = "3"
                drpSituacao.Items.Insert(2, "Atendido")
                drpSituacao.Items(2).Value = "4"
                sSelecionado = DataBinder.Eval(e.Row.DataItem, "CodigoSituacaoOS").ToString.Trim
                If Not String.IsNullOrEmpty(sSelecionado) Then

                    If drpSituacao.Items.FindByValue(sSelecionado) IsNot Nothing Then
                        drpSituacao.Items.FindByValue(sSelecionado).Selected = True
                    End If

                    If CInt(sSelecionado) = 4 Or CInt(sSelecionado) = 2 Then
                        'drpEtapa.Enabled = False
                        btnAtender.Visible = False
                        btnPrint.Visible = False
                        drpSituacao.Enabled = False
                    End If
                End If
            End If

            'apenas carrega classificacao se existir chamado relacionado com aquele item
            If Not String.IsNullOrEmpty(DataBinder.Eval(e.Row.DataItem, "NumeroChamado").ToString.Trim) Then
                reader = ct.ListarClassificacoes(bAtivo)
                If drpClassificacao IsNot Nothing Then
                    If reader.HasRows Then
                        drpClassificacao.DataSource = reader
                        drpClassificacao.DataBind()
                        drpClassificacao.Items.Insert(0, "Nenhum")
                        drpClassificacao.Items(0).Value = ""
                        sSelecionado = DataBinder.Eval(e.Row.DataItem, "CodigoClassificacao").ToString.Trim
                        If Not String.IsNullOrEmpty(sSelecionado) Then

                            If drpClassificacao.Items.FindByValue(sSelecionado) IsNot Nothing Then
                                drpClassificacao.Items.FindByValue(sSelecionado).Selected = True
                            End If

                        End If
                    End If
                    reader.Close()


                    reader = Nothing
                End If
            Else
                drpClassificacao.Visible = False
            End If

            reader = ct.ListarOcorrencias(bAtivo)
            If reader.HasRows Then
                drpOcorrencia.DataSource = reader
                drpOcorrencia.DataBind()
                drpOcorrencia.Items.Insert(0, "Nenhum")
                drpOcorrencia.Items(0).Value = ""
                sSelecionado = DataBinder.Eval(e.Row.DataItem, "CodigoOcorrencia").ToString.Trim
                If Not String.IsNullOrEmpty(sSelecionado) Then
                    If drpOcorrencia.Items.FindByValue(sSelecionado) IsNot Nothing Then
                        drpOcorrencia.Items.FindByValue(sSelecionado).Selected = True
                    End If
                End If
                reader.Close()
            End If

            reader = Nothing
            reader = os.ListarEtapasOS(bAtivo)
            If reader.HasRows Then
                drpEtapa.DataSource = reader
                drpEtapa.DataBind()
                drpEtapa.Items.Insert(0, "Nenhum")
                drpEtapa.Items(0).Value = ""
                sSelecionado = DataBinder.Eval(e.Row.DataItem, "CodigoEtapa").ToString.Trim
                If Not String.IsNullOrEmpty(sSelecionado) Then
                    If drpEtapa.Items.FindByValue(sSelecionado) IsNot Nothing Then
                        drpEtapa.Items.FindByValue(sSelecionado).Selected = True
                    End If
                Else
                    'drpEtapa.Items(1).Selected = True
                End If
            End If

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

            reader.Close()

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

    Protected Sub btnAtender_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim btnAtender As AutoHideButton = DirectCast(sender, AutoHideButton)
        For count As Integer = 0 To grdItens.Rows.Count - 1
            Dim wucAutoHideButton = DirectCast(grdItens.Rows(count).FindControl("btnAtender"), AutoHideButton)
            If wucAutoHideButton.CommandArgument = btnAtender.CommandArgument Then
                Dim oBaseInstaladaBox = DirectCast(grdItens.Rows(count).FindControl("oBaseInstaladaBox"), BaseInstaladaBox)
                Dim drpOcorrencia = DirectCast(grdItens.Rows(count).FindControl("drpOcorrencia"), DropDownList)
                Dim drpEtapa = DirectCast(grdItens.Rows(count).FindControl("drpEtapa"), DropDownList)
                Dim dtGarantia = DirectCast(grdItens.Rows(count).FindControl("lblDtGarantia"), Label)
                Dim sVersao = DirectCast(grdItens.Rows(count).FindControl("lblVersao"), Label)
                Dim vAtendimento() As String = Nothing
                ReDim Preserve vAtendimento(5)
                vAtendimento(0) = oBaseInstaladaBox.Serie.Trim
                vAtendimento(1) = drpOcorrencia.SelectedValue.Trim
                If IsDate(dtGarantia.Text) Then
                    vAtendimento(2) = CDate(dtGarantia.Text).ToShortDateString
                End If
                vAtendimento(3) = sVersao.Text
                vAtendimento(4) = drpEtapa.SelectedValue.Trim
                vAtendimento(5) = lblEmissao.Text
                Session("Atendimento") = vAtendimento
                Response.Redirect("DetalheAtendimento.aspx?Numero=" + btnAtender.CommandArgument)
            End If
        Next
    End Sub

    Protected Sub btnPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim btnPrint = DirectCast(sender, AutoHideButton)
        Dim numero As String = btnPrint.CommandArgument
        Dim aOpcao() As String
        ReDim Preserve aOpcao(5)
        aOpcao(0) = ""
        aOpcao(1) = "Defina o modelo de impressão."
        aOpcao(2) = "Aprovador/Técnico"
        aOpcao(3) = "ImpressaoAtendimento.aspx?Numero=" + numero + "?Modo=C"
        aOpcao(4) = "Cliente/Aprovador/Técnico"
        aOpcao(5) = "ImpressaoAtendimento.aspx?Numero=" + numero + "?Modo=D"
        oPopupBox.Exibir(aOpcao)
    End Sub

    Private Sub btnImprimir_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnImprimir.Click
        Response.Redirect("ImpressaoOrdemServico.aspx?Numero=" + btnImprimir.CommandArgument)
    End Sub

    Private Sub btnImpimirAtend_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnImpimirAtend.Click
        Response.Redirect("ImpressaoOSAtendimento.aspx?Numero=" + btnImprimir.CommandArgument)
    End Sub

    Private Sub HabilitarCamposOS()
        'lblTitOrigem.Visible = True
        'drpOrigem.Visible = True
        grdItens.Columns(5).Visible = True
        grdItens.Columns(6).Visible = True
        btnImprimir.Visible = True
    End Sub

    Private Sub DesabilitarCamposOS()
        'lblTitOrigem.Visible = False
        'drpOrigem.Visible = False
        grdItens.Columns(5).Visible = False
        grdItens.Columns(6).Visible = False
        btnImprimir.Visible = False
    End Sub

    'Private Function ValidaArquivosAnexo() As ctlRetornoGenerico

    '    Dim oRet As New ctlRetornoGenerico
    '    oRet.Sucesso = True

    '    Dim appSettings As NameValueCollection = ConfigurationManager.AppSettings
    '    Dim anexoUm As String = Path.GetExtension(UploadAnexoOne.FileName.ToString())
    '    Dim sChave = appSettings("Extensao").ToString()

    '    If UploadAnexoOne.HasFile = True Then

    '        If Not anexoUm = sChave Then

    '            oRet.Sucesso = False
    '            oRet.Mensagem = "Somente são válidos arquivos com extensão" + sChave.ToString()

    '        Else

    '            UploadAnexoOne.SaveAs(Server.MapPath("~/OS_downloads") + UploadAnexoOne.FileName.ToString())

    '        End If

    '    End If

    '    Return oRet

    'End Function

    Private Sub btnVoltar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnVoltar.Click
        Response.Redirect("ConsultaOrdemServico.aspx")
    End Sub

    Private Sub HabilitarModo(ByVal codigo As String)
        If codigo = "I" Then
            TituloPagina("Ordem de Serviço - Incluir")
            btnNovo.Visible = True
            btnIncluir.Visible = True
            btnAlterar.Visible = False
            btnImprimir.Visible = False
            grdItens.Columns(1).Visible = False
            btnLimpar.Visible = True
        ElseIf codigo = "A" Then
            TituloPagina("Ordem de Serviço - Alterar")
            btnNovo.Visible = False
            btnIncluir.Visible = False
            btnAlterar.Visible = True
            btnImprimir.Visible = True
            txtCliente.Enabled = False
            grdItens.Columns(1).Visible = True
            btnLimpar.Visible = False
        End If
    End Sub

    Private Function ObterIndice() As String
        Dim indice As String
        Dim nPosicao As Integer
        Dim sVar1 As String
        Dim sVar2 As String
        Dim dt As DataTable
        Dim sItem1 As String = "ABCDEFGHIJKLMNOPQRSTUVXYWZ"
        Dim sItem2 As String = "0123456789ABCDEFGHIJKLMNOPQRSTUVXYWZ"

        dt = DirectCast(ViewState("OrdemServico"), DataTable)
        If dt IsNot Nothing Then
            nPosicao = dt.Rows.Count - 1
            indice = dt.Rows(nPosicao).Item("ItemOS").ToString

            If IsNumeric(indice) And indice <> "99" Then
                indice = (CInt(indice) + 1).ToString.Trim.PadLeft(2, CChar("0"))
            Else
                sVar1 = Left(indice, 1)
                sVar2 = Right(indice, 1)
                If sVar2 = "Z" Then
                    indice = sItem1.Substring(sItem1.IndexOf(sVar1) + 1, 1) & Left(sItem2, 1)
                Else
                    indice = sVar1 & sItem2.Substring(sItem2.IndexOf(sVar2) + 1, 1)
                End If
            End If
        Else
            indice = "01"
        End If
        Return indice
    End Function

    Private Function PossuiChamadoEmBrancoComStatusAberto() As Boolean

        Dim dt As DataTable = DirectCast(ViewState("OrdemServico"), DataTable)
        Dim bRet As Boolean = False
        If String.IsNullOrEmpty(dt.Rows(0)("NumeroChamado").ToString.Trim) And dt.Rows(0)("CodigoStatusOS").Equals("A") Then
            bRet = True
        End If

        Return bRet
    End Function

    Private Sub CriarXmlComItensOS(ByVal dt As DataTable)

        Dim writer As New XmlTextWriter(NomeArquivoXml(), Nothing)
        'Dim sTipo As String = ""

        'inicia o documento xml
        writer.WriteStartDocument()

        'define a indentação do arquivo
        writer.Formatting = Formatting.Indented

        'escreve um comentario
        writer.WriteComment("Cabeçalho e itens de Ordem de Servico")

        'escreve o elmento raiz
        writer.WriteStartElement("OrdemServico")

        'If drpTipo.SelectedIndex.ToString = "2" Then
        '    sTipo = "E"
        'ElseIf drpTipo.SelectedIndex.ToString = "1" Then
        '    sTipo = "I"
        'End If

        For Each dr As DataRow In dt.Rows
            writer.WriteStartElement("itens")

            If CInt(lblOrdemServico.Text) = 0 Then
                writer.WriteElementString("Atendente", "")
            Else
                writer.WriteElementString("Atendente", HttpContext.Current.User.Identity.Name)
            End If
            writer.WriteElementString("Licenciado", HttpContext.Current.User.Identity.Name)

            'Ordem de Servico
            writer.WriteElementString("CodigoStatusOS", "")
            writer.WriteElementString("DescricaoStatusOS", "")
            writer.WriteElementString("NumeroOS", lblOrdemServico.Text)
            writer.WriteElementString("NumeroChamado", lblChamado.Text)
            writer.WriteElementString("ItemChamado", "")
            writer.WriteElementString("DataEmissao", lblEmissao.Text)
            writer.WriteElementString("HoraEmissao", "")
            writer.WriteElementString("CodigoOrigem", "") 'drpOrigem.SelectedValue)
            writer.WriteElementString("DescricaoOrigem", "")
            writer.WriteElementString("CodigoTipoChamado", drpTipo.SelectedValue)
            writer.WriteElementString("TipoChamado", "")
            writer.WriteElementString("NomeContato", txtContato.Text)
            writer.WriteElementString("TelefoneContato", oPhoneBox.Text)
            writer.WriteElementString("OSParceiro", txtOSParceiro.Text)
            writer.WriteElementString("OSAntiga", txtOSAntiga.Text)

            'Cliente
            writer.WriteElementString("FilialCliente", "") 'ver o que preencher
            writer.WriteElementString("CodigoCliente", txtCliente.Codigo)
            writer.WriteElementString("NomeCliente", txtCliente.Nome)
            writer.WriteElementString("CGCCliente", txtCliente.CPF_CNPJ)
            writer.WriteElementString("LojaCliente", txtCliente.Loja)

            'Item da Ordem de Serviço
            writer.WriteElementString("ProximoAtendimento", dr.Item("ProximoAtendimento").ToString)
            writer.WriteElementString("ItemOS", dr.Item("ItemOS").ToString)
            writer.WriteElementString("DescricaoSituacaoOS", dr.Item("DescricaoSituacaoOS").ToString)
            writer.WriteElementString("CodigoSituacaoOS", dr.Item("CodigoSituacaoOS").ToString)
            writer.WriteElementString("CodigoClassificacao", dr.Item("CodigoClassificacao").ToString)
            writer.WriteElementString("DescricaoClassificacao", dr.Item("DescricaoClassificacao").ToString)
            writer.WriteElementString("CodigoOcorrencia", dr.Item("CodigoOcorrencia").ToString)
            writer.WriteElementString("DescricaoOcorrencia", dr.Item("DescricaoOcorrencia").ToString)
            writer.WriteElementString("CodigoEtapa", dr.Item("CodigoEtapa").ToString)
            writer.WriteElementString("DescricaoEtapa", dr.Item("DescricaoEtapa").ToString)
            writer.WriteElementString("DescricaoSituacao", dr.Item("DescricaoSituacao").ToString)
            writer.WriteElementString("CodigoSituacao", dr.Item("CodigoSituacao").ToString)
            writer.WriteElementString("D_E_L_E_T_", dr.Item("D_E_L_E_T_").ToString)
            writer.WriteElementString("DataGarantia", dr.Item("DataGarantia").ToString)
            writer.WriteElementString("Versao", dr.Item("Versao").ToString)


            'Produto
            writer.WriteElementString("CodigoProduto", dr.Item("CodigoProduto").ToString)
            writer.WriteElementString("DescricaoProduto", dr.Item("DescricaoProduto").ToString)
            writer.WriteElementString("NumeroSerieProduto", dr.Item("NumeroSerieProduto").ToString)
            writer.WriteElementString("DescrGarantia", dr.Item("DescrGarantia").ToString)

            'Sub Item
            writer.WriteElementString("SubItem", "")
            writer.WriteElementString("DescricaoSubItem", "")
            writer.WriteElementString("CodigoServico", "")
            writer.WriteElementString("DescricaoServico", "")
            writer.WriteElementString("Quantidade", "")
            writer.WriteElementString("ValorUnitario", "")
            writer.WriteElementString("Total", "")

            writer.WriteElementString("Comentario", "")

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

    ' Retorna o nome e caminho do arquivo xml
    Private Function NomeArquivoXml() As String

        Dim sNomeArquivo As String = Server.MapPath("~") & "\arquivos_xml_erros_os_online"

        If Not Directory.Exists(sNomeArquivo) Then
            Directory.CreateDirectory(sNomeArquivo)
        End If

        Return sNomeArquivo & "\ordem_servico_" & HttpContext.Current.Session("NomeUsuario").ToString.Trim & ".xml"

    End Function

    Private Sub btnLimpar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLimpar.Click
        'APENAS APAGA O ARQUIVO XML DE INCLUSAO
        If File.Exists(NomeArquivoXml()) Then
            Dim ds = New DataSet
            ds.ReadXml(NomeArquivoXml())
            If (ds.Tables.Count > 0) Then
                If ds.Tables(0).Rows(0).Item("NumeroOs").ToString.Trim = "000000" Then
                    DeletaArquivoXml()
                End If
            End If
        End If

        Response.Redirect("DetalheOrdemServico.aspx")

    End Sub

    Private Sub btnAnexar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAnexar.Click

        containerAnexo.Visible = True
        containerAnexados.Visible = False
        btnAnexar.Visible = False
    End Sub

    'Private Sub PreencherOrigensOS()
    '    Dim reader As SqlDataReader
    '    Dim os As New ctlOrdemServico
    '    reader = os.ListarOrigensOS
    '    drpOrigem.DataSource = reader
    '    drpOrigem.DataBind()
    '    drpOrigem.Items.Insert(0, "Nenhum")
    '    reader.Close()
    'End Sub

End Class