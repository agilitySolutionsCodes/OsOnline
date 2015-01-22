Imports System.Data.SqlClient
Imports System.IO
Imports System.Xml

Public Class DetalheChamadoTecnico
    Inherits BaseWebUI


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                DirectCast(Me.Master.Controls(0).Controls(3).Controls(7).FindControl("menuOsOnline"), Menu).Visible = False
                NomeBotoes()
                If Request("Numero") Is Nothing Then

                    Incluir()
                ElseIf Request("Numero").Trim.Contains("OSX") Then
                    Dim dt As New DataTable
                    Dim os As New ctlOrdemServico
                    dt.Load(os.Selecionar(Request("Numero").Substring(0, 6)))
                    If dt.Rows.Count > 0 Then
                        HabilitarModo("OSX")
                        ViewState.Add("ChamadoTecnico", dt) 'ADICIONADO AGORA
                        grdItens.DataSource = dt
                        grdItens.DataBind()
                        Dim dr As DataRow = dt.Rows(0)
                        lblNumero.Text = "000000"
                        lblEmissao.Text = DirectCast(dr("DataEmissao"), Date).ToString("dd/MM/yyyy")
                        txtCliente.Codigo = dr("CodigoCliente").ToString
                        txtCliente.Loja = dr("LojaCliente").ToString
                        txtCliente.Nome = dr("NomeCliente").ToString
                        txtCliente.CPF_CNPJ = dr("CGCCliente").ToString
                        drpTipo.SelectedValue = dt.Rows(0).Item("CodigoTipoChamado").ToString
                        'If dt.Rows(0).Item("CodigoTipoChamado").ToString = "E" Then
                        '    drpTipo.SelectedIndex = 2
                        'ElseIf dt.Rows(0).Item("CodigoTipoChamado").ToString = "I" Then
                        '    drpTipo.SelectedIndex = 1
                        'End If
                    End If
                Else
                    Dim numero As String = Request("Numero").Substring(0, 8)
                    Selecionar(numero)
                End If
            End If
        Catch ex As Exception
            Dim oRet As New ctlRetornoGenerico
            ctlUtil.EscreverLogErro("DetalheChamadoTecnico - Page_load: " & ex.Message())
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
        lblNumero.Text = "000000"
        btnAlterar.Visible = False
        btnNovo.Visible = False
    End Sub

    Private Function ObterEstrutura(Optional ByVal bAdicionarLinha As Boolean = True) As DataTable

        Dim dt As New DataTable

        dt.Columns.Add("Atendente")

        'Chamado Técnico
        dt.Columns.Add("DescricaoStatusChamado")
        dt.Columns.Add("CodigoStatusChamado")
        dt.Columns.Add("Filial")
        dt.Columns.Add("NumeroChamado")
        dt.Columns.Add("NumeroOS")
        dt.Columns.Add("NumeroAtendimentoOS")
        dt.Columns.Add("DataEmissao", GetType(DateTime))
        dt.Columns.Add("HoraEmissao")
        dt.Columns.Add("TipoChamado")
        dt.Columns.Add("NomeContato")
        dt.Columns.Add("TelefoneContato")
        dt.Columns.Add("CodigoTipoChamado")
        dt.Columns.Add("ChamadoAntigo")
        dt.Columns.Add("OSALTERADA")

        'Cliente
        dt.Columns.Add("CodigoCliente")
        dt.Columns.Add("NomeCliente")
        dt.Columns.Add("CGCCliente")
        dt.Columns.Add("LojaCliente")

        'Item do Chamado Técnico
        dt.Columns.Add("ItemChamado")
        dt.Columns.Add("CodigoSituacao")
        dt.Columns.Add("DescricaoSituacao")
        dt.Columns.Add("CodigoClassificacao")
        dt.Columns.Add("DescricaoClassificacao")
        dt.Columns.Add("CodigoOcorrencia")
        dt.Columns.Add("DescricaoOcorrencia")
        dt.Columns.Add("Comentario")
        dt.Columns.Add("D_E_L_E_T_")

        'Produto
        dt.Columns.Add("CodigoProduto")
        dt.Columns.Add("DescricaoProduto")
        dt.Columns.Add("NumeroSerieProduto")
        dt.Columns.Add("DescrGarantia")

        If bAdicionarLinha Then
            AdicionarItem(dt)
        End If

        Return dt

    End Function

    Private Sub Selecionar(ByVal sNumero As String)
        Dim ct As New ctlChamadoTecnico
        Dim reader As SqlDataReader = Nothing 'CType(1, ctlChamadoTecnico.TipoPesquisa)
        If Request("Numero") IsNot Nothing Then
            If Request("Numero").Trim.Contains("H") Then
                reader = ct.Selecionar(sNumero, False)
            Else
                reader = ct.Selecionar(sNumero)
            End If
        Else
            reader = ct.Selecionar(sNumero)
        End If
        If Not reader.HasRows() Then
            Throw New Exception("O Chamado Técnico '" + sNumero + "' não foi localizada.")
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

        'preenche dados a partir do xml gravado
        If File.Exists(NomeArquivoXml()) Then
            Dim ds = New DataSet
            ds.ReadXml(NomeArquivoXml())
            If (ds.Tables.Count > 0) Then
                If String.IsNullOrEmpty(dt.Rows(0).Item("NumeroChamado").ToString) Then dt.Rows(0).Item("NumeroChamado") = "000000"
                If ds.Tables(0).Rows(0).Item("Licenciado").ToString.Trim = HttpContext.Current.User.Identity.Name.Trim _
                    And dt.Rows(0).Item("NumeroChamado").ToString = ds.Tables(0).Rows(0).Item("NumeroChamado").ToString Then
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

        lblNumero.Text = dt.Rows(0).Item("NumeroChamado").ToString
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
        txtNumOS.Value = dt.Rows(0).Item("NumeroOS").ToString
        If Not String.IsNullOrEmpty(dt.Rows(0).Item("ChamadoAntigo").ToString.Trim) Then
            txtChamadoAntigo.Text = dt.Rows(0).Item("ChamadoAntigo").ToString
            lblChamadoAntigo.Visible = True
            txtChamadoAntigo.Visible = True
        End If

        If Not String.IsNullOrEmpty(dt.Rows(0).Item("CodigoSituacao").ToString) Then
            ViewState.Add("CodigoSituacao", CInt(dt.Rows(0).Item("CodigoSituacao").ToString))
        End If
        drpTipo.SelectedValue = dt.Rows(0).Item("CodigoTipoChamado").ToString
        'If dt.Rows(0).Item("CodigoTipoChamado").ToString = "E" Then
        '    drpTipo.SelectedIndex = 2
        'ElseIf dt.Rows(0).Item("CodigoTipoChamado").ToString = "I" Then
        '    drpTipo.SelectedIndex = 1
        'End If

        'VERIFICAR SE EXISTE ALGUMA OS COM STATUS <> 1, CASO POSITIVO, BLOQUEAR ALTERACAO
        If dt.Rows(0).Item("Atendente").ToString.Trim = HttpContext.Current.User.Identity.Name Then
            If CInt(dt.Rows(0).Item("OSALTERADA")) > 0 Then
                HabilitarModo("V")
            Else
                HabilitarModo("A")
            End If
        Else
            HabilitarModo("I")
        End If
        ViewState.Add("ChamadoTecnico", dt)
        grdItens.DataSource = dt
        grdItens.DataBind()
    End Sub

    Private Sub AdicionarItem(ByRef dt As DataTable, Optional ByVal nIndice As Integer = 0)

        Dim nItem As Integer = 0
        Dim dr As DataRow = dt.NewRow

        dr("Atendente") = ""

        'Chamado Técnico
        dr("DescricaoStatusChamado") = ""
        dr("CodigoStatusChamado") = ""
        dr("Filial") = ""
        dr("NumeroChamado") = ""
        dr("NumeroOS") = ""
        dr("NumeroAtendimentoOS") = ""
        dr("DataEmissao") = Now.Date
        dr("HoraEmissao") = ""
        dr("TipoChamado") = ""
        dr("NomeContato") = ""
        dr("TelefoneContato") = ""
        dr("CodigoTipoChamado") = ""
        dr("OSALTERADA") = "0"

        'Cliente
        dr("CodigoCliente") = ""
        dr("NomeCliente") = ""
        dr("CGCCliente") = ""
        dr("LojaCliente") = ""

        'Item do Chamado Técnico
        dr("ItemChamado") = ObterIndice() 'CStr(CInt(dt(nIndice - 1)("ItemChamado")) + 1)
        dr("CodigoSituacao") = ""
        dr("DescricaoSituacao") = ""
        dr("CodigoClassificacao") = ""
        dr("DescricaoClassificacao") = ""
        dr("CodigoOcorrencia") = ""
        dr("DescricaoOcorrencia") = ""
        dr("Comentario") = ""
        dr("D_E_L_E_T_") = ""

        'Produto
        dr("CodigoProduto") = ""
        dr("DescricaoProduto") = ""
        dr("NumeroSerieProduto") = ""
        dr("ChamadoAntigo") = ""
        dr("DescrGarantia") = ""

        If nIndice = 0 Then
            dt.Rows.Add(dr)
        Else
            dt.Rows.InsertAt(dr, nIndice)
        End If

    End Sub

    Private Sub grdItens_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdItens.RowCommand
        Dim gv As GridView = DirectCast(sender, GridView)
        Dim dt As DataTable = DirectCast(ViewState("ChamadoTecnico"), DataTable)
        'dt = ctlAplicacao.Ordenar(dt, gvSortExpression, gvSortDirection)
        If e.CommandName = "Busca" OrElse e.CommandName = "Sort" OrElse e.CommandName = "" Then
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
                    'Se for alteração, basta cancelar. se for inclusão, eh necessário remover a linha
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
            grdItens.DataSource = dt
            grdItens.DataBind()
        End If
        ViewState.Add("ChamadoTecnico", dt)
    End Sub

    Private Sub grdItens_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles grdItens.RowDeleting
    End Sub

    Private Sub grdItens_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles grdItens.RowEditing
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
        Dim sCodigoProduto As String = ""
        Dim sNomeProduto As String = ""
        Dim oBaseInstaladaView = DirectCast(gr.FindControl("oBaseInstaladaView"), BaseInstaladaView)
        Dim drpClassificacao = DirectCast(gr.FindControl("drpClassificacao"), DropDownList)
        Dim drpSituacao = DirectCast(gr.FindControl("drpSituacao"), DropDownList)
        Dim drpOcorrencia = DirectCast(gr.FindControl("drpOcorrencia"), DropDownList)
        Dim drpEtapa = DirectCast(gr.FindControl("drpEtapa"), DropDownList)
        Dim oDescriptionBox = DirectCast(gr.FindControl("oDescriptionBox"), DescriptionBox)
        Dim dt As DataTable
        dt = DirectCast(ViewState("ChamadoTecnico"), DataTable)
        Dim dr As DataRow = dt.NewRow()
        dr.ItemArray = dt.Rows(nIndice).ItemArray
        dr("CodigoProduto") = oBaseInstaladaView.Codigo
        dr("DescricaoProduto") = oBaseInstaladaView.Nome.Trim
        dr("NumeroSerieProduto") = oBaseInstaladaView.Serie
        dr("DescrGarantia") = oBaseInstaladaView.Garantia
        dr("CodigoSituacao") = drpSituacao.SelectedValue
        dr("DescricaoSituacao") = drpSituacao.SelectedItem.Text.Trim
        dr("CodigoClassificacao") = drpClassificacao.SelectedValue
        dr("DescricaoClassificacao") = drpClassificacao.SelectedItem.Text.Trim
        dr("CodigoOcorrencia") = drpOcorrencia.SelectedValue
        dr("DescricaoOcorrencia") = drpOcorrencia.SelectedItem.Text.Trim
        dr("Comentario") = oDescriptionBox.Descricao.Trim
        dt.Rows.RemoveAt(nIndice)
        dt.Rows.InsertAt(dr, nIndice)
        dt.AcceptChanges()
    End Sub

    Private Sub RemoverItem(ByVal gr As GridViewRow, ByVal nIndice As Integer)
        Dim lblRegistro = DirectCast(gr.FindControl("lblRegistro"), Label)
        Dim dt As DataTable
        dt = DirectCast(ViewState("ChamadoTecnico"), DataTable)
        Dim dr As DataRow = dt.NewRow()
        dr.ItemArray = dt.Rows(nIndice).ItemArray
        dt.Rows.RemoveAt(nIndice)
        If Not String.IsNullOrEmpty(lblRegistro.Text) Then
            dr("D_E_L_E_T_") = "*"
            dt.Rows.InsertAt(dr, nIndice)
            dt.AcceptChanges()
        End If
    End Sub

    Private Sub btnNovo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNovo.Click
        Response.Redirect("DetalheChamadoTecnico.aspx")
    End Sub

    Private Sub btnIncluir_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnIncluir.Click
        VerificaBaseInstaladaChamado(True)
    End Sub

    Private Sub btnAlterar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAlterar.Click
        VerificaBaseInstaladaChamado(False)
    End Sub

    Private Sub VerificaBaseInstaladaChamado(ByVal bIncluiChamado As Boolean)

        Dim oRet As New ctlRetornoGenerico

        Try
            Dim ct As New ctlChamadoTecnico
            Dim bi As New ctlBaseInstalada
            Dim sNumero As String = lblNumero.Text
            Dim sUsuario As String = HttpContext.Current.User.Identity.Name
            Dim bAlteraBase As Boolean = False
            Dim sSerie As String = ""
            Dim sListaSeries As String = ""

            oRet = Validar()
            If oRet.Sucesso Then
                Dim dt As DataTable = DirectCast(ViewState("ChamadoTecnico"), DataTable)
                Dim oChamado As New ctlChamadoTecnico()
                Dim oCliente As New ctlCliente(txtCliente.Codigo, txtCliente.Loja, txtCliente.Nome, txtCliente.CPF_CNPJ)
                'chamada da criacao do xml contendo todos os dados da pagina
                CriarXmlComItensOS(dt)

                oChamado.Numero = lblNumero.Text
                oChamado.Cliente = oCliente
                oChamado.Contato = txtContato.Text.Trim
                oChamado.Telefone = oPhoneBox.Text.Trim
                oChamado.CodigoTipo = drpTipo.SelectedValue
                oChamado.NumeroOS = txtNumOS.Value
                oChamado.PutItens(dt)

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
                    Session.Add("oChamado", oChamado)
                    Session.Add("oItens", dt)
                    Session.Add("bIncluiChamado", bIncluiChamado)
                    Session.Add("sListaSeries", sListaSeries)
                    Session.Add("sTipo", "CT")
                    Response.Redirect("AlteracaoBaseInstalada.aspx")
                End If

                oRet = GravarChamado(oChamado, oCliente, dt, True, bIncluiChamado)
            End If

        Catch ex As Exception
            oRet.Sucesso = False
            If bIncluiChamado Then
                ctlUtil.EscreverLogErro("DetalheChamadoTecnico - btnIncluir_click: " & ex.Message())
            Else
                ctlUtil.EscreverLogErro("DetalheChamadoTecnico - btnAlterar_click: " & ex.Message())
            End If
            oRet.Mensagem = ctlUtil.sMsgErroPadrao

        End Try
        oMensagem.SetMessage(oRet)
    End Sub

    Public Function GravarChamado(ByVal oChamado As ctlChamadoTecnico, ByVal oCliente As ctlCliente, ByVal dt As DataTable, ByVal bSeleciona As Boolean, ByVal bIncluiChamado As Boolean) As ctlRetornoGenerico

        Dim oRet As New ctlRetornoGenerico
        Dim ct As New ctlChamadoTecnico
        Dim bi As New ctlBaseInstalada

        Try
            If bIncluiChamado Then
                oRet = ct.Incluir(oChamado)
            Else
                oRet = ct.Alterar(oChamado)
            End If

            If oRet.Sucesso Then
                Dim nrChamado As String = oRet.Chave.Substring(2)
                Dim os As New ctlOrdemServico
                Dim reader As SqlDataReader
                Dim sNumSerie As String = ""
                Dim sCodProduto As String = ""
                'caso inclusao for efetuada com sucesso deleta arquivo xml
                DeletaArquivoXml()

                'INCLUIR DADOS NA GRUPO X SERIE
                For Each dr As DataRow In dt.Rows
                    sNumSerie += dr("NumeroSerieProduto").ToString & ";"
                    sCodProduto += dr("CodigoProduto").ToString & ";"
                Next
                bi.IncluirItemGrupoSerie(oCliente, sNumSerie, sCodProduto, nrChamado, "CT")

                If bSeleciona Then
                    Selecionar(nrChamado)
                End If

                reader = os.ListarPorChamado(nrChamado)
                If reader.HasRows Then
                    reader.Read()
                    If Not String.IsNullOrEmpty(reader("NumeroOS").ToString) Then
                        Dim mensagem As String = ""
                        mensagem = "OS do chamado: " + reader("NumeroOS").ToString
                        oRet.Mensagem += "<br/> <br/>" + mensagem
                    End If
                End If
                If bSeleciona Then
                    HabilitarModo("A")
                End If
            End If
        Catch ex As Exception
            oRet.Sucesso = False
            ctlUtil.EscreverLogErro("DetalheChamadoTecnico - AlteraChamado: " & ex.Message())
            oRet.Mensagem = ctlUtil.sMsgErroPadrao
        End Try

        Return oRet
    End Function

    Private Function Validar() As ctlRetornoGenerico
        Dim oRet As New ctlRetornoGenerico
        oRet.Sucesso = False
        If drpTipo.SelectedIndex < 1 Then
            oRet.Mensagem = "O tipo não foi preenchido."
        ElseIf Not txtCliente.IsValid Then
            oRet.Mensagem = "Selecione um cliente."
        ElseIf txtContato.Text.Trim = "" Then
            oRet.Mensagem = "Forneça o nome do contato."
        ElseIf oPhoneBox.IsEmpty() Then
            oRet.Mensagem = "Forneça um telefone de contato."
        ElseIf Not ValidarItens(oRet) Then
            'Itens inválido
        Else
            oRet.Sucesso = True
        End If
        Return oRet
    End Function

    Private Function ValidarItens(ByVal oRet As ctlRetornoGenerico) As Boolean
        Dim bRet As Boolean = False
        oRet.Mensagem = ""
        If grdItens.EditIndex > -1 Then
            oRet.Mensagem = "A linha " + (grdItens.EditIndex + 1).ToString + " encontra-se em edição e precisa ser confirmada ou cancelada."
        Else
            For Each gr As GridViewRow In grdItens.Rows
                Dim lblItemChamado = DirectCast(gr.FindControl("lblItemChamado"), Label)
                Dim drpSituacao As DropDownList = DirectCast(gr.FindControl("drpSituacao"), DropDownList)
                Dim oBaseInstaladaBox As BaseInstaladaBox = DirectCast(gr.FindControl("oBaseInstaladaBox"), BaseInstaladaBox)
                Dim drpClassificacao As DropDownList = DirectCast(gr.FindControl("drpClassificacao"), DropDownList)
                Dim drpOcorrencia As DropDownList = DirectCast(gr.FindControl("drpOcorrencia"), DropDownList)
                Dim drpEtapa As DropDownList = DirectCast(gr.FindControl("drpEtapa"), DropDownList)
                Dim lblD_E_L_E_T_ = DirectCast(gr.FindControl("lblD_E_L_E_T_"), Label)
                'If Not String.IsNullOrEmpty(lblD_E_L_E_T_.Text) Then
                If lblD_E_L_E_T_.Text.Trim <> "*" Then
                    If String.IsNullOrEmpty(oBaseInstaladaBox.Serie) Then
                        oRet.Mensagem = "O número de série não foi informado na linha " + (gr.RowIndex + 1).ToString + "."
                    ElseIf String.IsNullOrEmpty(oBaseInstaladaBox.Codigo) Then
                        oRet.Mensagem = "A linha " + (gr.RowIndex + 1).ToString + " não contém um Produto válido. Favor informar um Produto."
                    ElseIf drpClassificacao.SelectedIndex < 1 Then
                        oRet.Mensagem = "A classificação não foi preenchida na linha " + (gr.RowIndex + 1).ToString + "."
                    ElseIf drpOcorrencia.SelectedIndex < 1 Then
                        oRet.Mensagem = "A ocorrência não foi preenchida na linha " + (gr.RowIndex + 1).ToString + "."
                    End If
                    Dim duplicidade As String = ProcurarMesmaOcorrencia(lblItemChamado.Text.Trim, oBaseInstaladaBox.Serie, drpOcorrencia.SelectedValue.Trim, lblD_E_L_E_T_.Text.Trim)
                    If duplicidade <> "" Then
                        oRet.Mensagem = "O equipamento e a ocorrência do item " + lblItemChamado.Text + " é a mesma do item " + duplicidade + ", favor alterar a ocorrência."
                        Exit For
                    End If
                End If
                'End If
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
    ''' <param name="D_E_L_E_T"></param>
    ''' <returns>Retorna o número do registro duplicado, se não existir duplicidade retorna em branco</returns>
    ''' <remarks></remarks>

    Private Function ProcurarMesmaOcorrencia(ByVal nrItem As String, ByVal nrSerie As String, ByVal ocorrencia As String, ByVal D_E_L_E_T As String) As String
        Dim dt As DataTable = DirectCast(ViewState("ChamadoTecnico"), DataTable)
        'Dim dv As New DataView(dt)
        'dv.DefaultView.RowFilter = "D_E_L_E_T_ <> '*'"
        Dim str As String = ""
        For indice = 0 To dt.Rows.Count - 1
            Dim dr As DataRow = dt.Rows(indice)
            If nrSerie = dr("NumeroSerieProduto").ToString.Trim AndAlso ocorrencia = dr("CodigoOcorrencia").ToString.Trim _
                AndAlso D_E_L_E_T <> "*" AndAlso nrItem <> dr("ItemChamado").ToString.Trim Then
                str = dr("ItemChamado").ToString.Trim
                Exit For
            End If
        Next
        Return str
    End Function

    Private Sub grdItens_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdItens.RowDataBound

        Dim bAtivo As Boolean = False

        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim drpClassificacao As DropDownList = DirectCast(e.Row.FindControl("drpClassificacao"), DropDownList)
            Dim drpSituacao As DropDownList = DirectCast(e.Row.FindControl("drpSituacao"), DropDownList)
            Dim drpOcorrencia As DropDownList = DirectCast(e.Row.FindControl("drpOcorrencia"), DropDownList)
            Dim btnAtender As AutoHideButton = DirectCast(e.Row.FindControl("btnAtender"), AutoHideButton)
            Dim lblD_E_L_E_T_ As Label = DirectCast(e.Row.FindControl("lblD_E_L_E_T_"), Label)
            Dim dt As DataTable = DirectCast(ViewState("ChamadoTecnico"), DataTable)
            Dim numero As String = Request("Numero")
            Dim reader As SqlDataReader = Nothing
            Dim ct As New ctlChamadoTecnico
            Dim sSelecionado As String

            If IsDate(lblEmissao.Text) Then
                If Date.Compare(CDate(lblEmissao.Text), CDate(Session("DataCorte"))) >= 0 Then
                    bAtivo = True
                End If
            End If

            'Preenche o drop dentro do grid
            If drpSituacao IsNot Nothing Then
                drpSituacao.Items.Insert(0, "Chamado")
                drpSituacao.Items(0).Value = "1"
                drpSituacao.Items.Insert(1, "O.S")
                drpSituacao.Items(1).Value = "3"
                drpSituacao.Items.Insert(2, "Encerrado")
                drpSituacao.Items(2).Value = "5"
                sSelecionado = DataBinder.Eval(e.Row.DataItem, "CodigoSituacao").ToString.Trim
                If Not String.IsNullOrEmpty(sSelecionado) Then
                    drpSituacao.Items.FindByValue(sSelecionado).Selected = True
                    If CInt(sSelecionado) <> 1 Then
                        drpSituacao.Enabled = False
                        'Dim gv = CType(e, GridView)
                        'Dim cf = CType(gv.Columns(0), CommandField)
                        'cf.ShowDeleteButton = False
                    End If
                Else
                    If Not String.IsNullOrEmpty(numero) Then
                        If numero.Contains("OSX") Then
                            drpSituacao.SelectedValue = "3"
                            drpSituacao.Enabled = False
                        End If
                    End If
                End If
            End If

            reader = ct.ListarClassificacoes(bAtivo)
            If drpClassificacao IsNot Nothing Then
                If reader.HasRows Then
                    drpClassificacao.DataSource = reader
                    drpClassificacao.DataBind()
                    drpClassificacao.Items.Insert(0, "Nenhum")
                    drpClassificacao.Items(0).Value = ""
                    sSelecionado = DataBinder.Eval(e.Row.DataItem, "CodigoClassificacao").ToString.Trim
                    If Not String.IsNullOrEmpty(sSelecionado) Then
                        drpClassificacao.Items.FindByValue(sSelecionado).Selected = True
                    End If
                End If
                reader.Close()
            End If

            reader = Nothing
            reader = ct.ListarOcorrencias(bAtivo)
            If reader.HasRows Then
                drpOcorrencia.DataSource = reader
                drpOcorrencia.DataBind()
                drpOcorrencia.Items.Insert(0, "Nenhum")
                drpOcorrencia.Items(0).Value = ""
                sSelecionado = DataBinder.Eval(e.Row.DataItem, "CodigoOcorrencia").ToString.Trim
                If Not String.IsNullOrEmpty(sSelecionado) Then
                    drpOcorrencia.Items.FindByValue(sSelecionado).Selected = True
                End If
            End If

            If lblD_E_L_E_T_.Text.Equals("*") Then
                e.Row.Visible = False
            Else
                Dim linhaAtual As Integer = e.Row.RowIndex
                Dim ultimaLinhaVisivel As Integer = ObterUltimaLinhaNaoDeletada(linhaAtual)
                If ultimaLinhaVisivel <> -1 Then
                    Dim cell As GridViewRow = grdItens.Rows(ultimaLinhaVisivel)
                    If cell.BackColor = Nothing Then
                        e.Row.BackColor = System.Drawing.Color.FromArgb(204, 255, 221)
                    Else
                        e.Row.BackColor = Nothing
                    End If
                Else
                    e.Row.BackColor = System.Drawing.Color.FromArgb(204, 255, 221)
                End If
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
        Response.Redirect("DetalheAtendimento.aspx?Numero=" + btnAtender.CommandArgument)
    End Sub

    Private Sub btnVoltar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnVoltar.Click
        Response.Redirect("ConsultaChamadoTecnico.aspx")
    End Sub

    Private Sub HabilitarModo(ByVal codigo As String)
        If codigo = "I" Then
            TituloPagina("Chamado Técnico - Incluir")
            btnNovo.Visible = False
            btnIncluir.Visible = True
            btnAlterar.Visible = False
            btnLimpar.Visible = True
        ElseIf codigo = "A" Then
            TituloPagina("Chamado Técnico - Alterar")
            btnNovo.Visible = True
            btnIncluir.Visible = False
            btnAlterar.Visible = True
            txtCliente.Enabled = False
            btnLimpar.Visible = False
        ElseIf codigo = "V" Then
            TituloPagina("Chamado Técnico - Visualizar")
            btnNovo.Visible = True
            btnIncluir.Visible = False
            btnAlterar.Visible = False
            txtCliente.Enabled = False
            txtContato.Enabled = False
            oPhoneBox.Enabled = False
            grdItens.Columns(0).Visible = False
            drpTipo.Enabled = False
            btnLimpar.Visible = False
        ElseIf codigo = "OSX" Then
            TituloPagina("Chamado Técnico - Incluir")
            btnNovo.Visible = False
            btnIncluir.Visible = True
            btnAlterar.Visible = False
            txtCliente.Enabled = False
        End If
    End Sub

    Private Function ObterIndice() As String
        Dim indice As Integer = 0
        Dim dt As DataTable
        dt = DirectCast(ViewState("ChamadoTecnico"), DataTable)
        If dt IsNot Nothing Then
            For Each dr As DataRow In dt.Rows
                If CInt(dr("ItemChamado")) > indice Then
                    indice = CInt(dr("ItemChamado"))
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

    Private Sub grdItens_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdItens.PageIndexChanging
        Dim gv As GridView = DirectCast(sender, GridView)
        Dim dt As DataTable = DirectCast(ViewState("ChamadoTecnico"), DataTable)
        gv.DataSource = ctlAplicacao.Ordenar(dt, gvSortExpression, gvSortDirection)
        gv.PageIndex = e.NewPageIndex
        gv.DataBind()
    End Sub

    Private Sub grdItens_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdItens.Sorting
        Dim gv As GridView = DirectCast(sender, GridView)
        Dim dt As DataTable = TryCast(ViewState("ChamadoTecnico"), DataTable)
        If Not String.IsNullOrEmpty(e.SortExpression) Then
            If gvSortExpression = e.SortExpression Then
                gvSortDirection = ObterDirecao()
            Else
                gvSortDirection = "ASC"
            End If
            gvSortExpression = e.SortExpression
        End If
        gv.DataSource = ctlAplicacao.Ordenar(dt, e.SortExpression, gvSortDirection)
        gv.DataBind()
    End Sub

    Private Function ObterDirecao() As String
        Dim newSortDirection As String = String.Empty
        Select Case gvSortDirection
            Case "DESC"
                newSortDirection = "ASC"
                Exit Select
            Case "ASC"
                newSortDirection = "DESC"
                Exit Select
        End Select
        Return newSortDirection
    End Function

    Public Property gvSortDirection() As String
        Get
            Return If(TryCast(ViewState("SortDirection"), String), "ASC")
        End Get
        Set(ByVal value As String)
            ViewState("SortDirection") = value
        End Set
    End Property

    Public Property gvSortExpression() As String
        Get
            Return If(TryCast(ViewState("SortExpression"), String), "")
        End Get
        Set(ByVal value As String)
            ViewState("SortExpression") = value
        End Set
    End Property

    Private Sub CriarXmlComItensOS(ByVal dt As DataTable)

        Dim writer As New XmlTextWriter(NomeArquivoXml(), Nothing)
        'Dim sTipoChamado As String = ""

        'If drpTipo.SelectedIndex = 1 Then
        '    sTipoChamado = "I"
        'ElseIf drpTipo.SelectedIndex = 2 Then
        '    sTipoChamado = "E"
        'End If


        'inicia o documento xml
        writer.WriteStartDocument()

        'define a indentação do arquivo
        writer.Formatting = Formatting.Indented

        'escreve um comentario
        writer.WriteComment("Cabeçalho e itens do Chamado Tecnico")

        'escreve o elmento raiz
        writer.WriteStartElement("ChamadoTecnico")

        For Each dr As DataRow In dt.Rows
            writer.WriteStartElement("itens")

            If CInt(lblNumero.Text) = 0 Then
                writer.WriteElementString("Atendente", "")
            Else
                writer.WriteElementString("Atendente", HttpContext.Current.User.Identity.Name)
            End If
            writer.WriteElementString("Licenciado", HttpContext.Current.User.Identity.Name)



            'Chamado Técnico
            writer.WriteElementString("DescricaoStatusChamado", "")
            writer.WriteElementString("CodigoStatusChamado", "")
            writer.WriteElementString("Filial", "")
            writer.WriteElementString("NumeroChamado", lblNumero.Text)
            writer.WriteElementString("NumeroOS", txtNumOS.Value)
            writer.WriteElementString("NumeroAtendimentoOS", "")
            writer.WriteElementString("DataEmissao", lblEmissao.Text)
            writer.WriteElementString("HoraEmissao", "")
            writer.WriteElementString("TipoChamado", "")
            writer.WriteElementString("NomeContato", txtContato.Text)
            writer.WriteElementString("TelefoneContato", oPhoneBox.Text)
            writer.WriteElementString("CodigoTipoChamado", drpTipo.SelectedValue) 'sTipoChamado)
            writer.WriteElementString("ChamadoAntigo", txtChamadoAntigo.Text)
            writer.WriteElementString("OSALTERADA", "0")

            'Cliente
            writer.WriteElementString("CodigoCliente", txtCliente.Codigo)
            writer.WriteElementString("NomeCliente", txtCliente.Nome)
            writer.WriteElementString("CGCCliente", txtCliente.CPF_CNPJ)
            writer.WriteElementString("LojaCliente", txtCliente.Loja)

            'Item do Chamado Técnico
            writer.WriteElementString("ItemChamado", dr.Item("ItemChamado").ToString)
            writer.WriteElementString("CodigoSituacao", dr.Item("CodigoSituacao").ToString)
            writer.WriteElementString("DescricaoSituacao", dr.Item("DescricaoSituacao").ToString)
            writer.WriteElementString("CodigoClassificacao", dr.Item("CodigoClassificacao").ToString)
            writer.WriteElementString("DescricaoClassificacao", dr.Item("DescricaoClassificacao").ToString)
            writer.WriteElementString("CodigoOcorrencia", dr.Item("CodigoOcorrencia").ToString)
            writer.WriteElementString("DescricaoOcorrencia", dr.Item("DescricaoOcorrencia").ToString)
            writer.WriteElementString("Comentario", dr.Item("Comentario").ToString)
            writer.WriteElementString("D_E_L_E_T_", dr.Item("D_E_L_E_T_").ToString)

            'Produto
            writer.WriteElementString("CodigoProduto", dr.Item("CodigoProduto").ToString)
            writer.WriteElementString("DescricaoProduto", dr.Item("DescricaoProduto").ToString)
            writer.WriteElementString("NumeroSerieProduto", dr.Item("NumeroSerieProduto").ToString)
            writer.WriteElementString("DescrGarantia", dr.Item("DescrGarantia").ToString)

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

        If Not My.Computer.FileSystem.DirectoryExists(sNomeArquivo) Then
            Directory.CreateDirectory(sNomeArquivo)
        End If

        Return sNomeArquivo & "\chamado_tecnico_" & HttpContext.Current.Session("NomeUsuario").ToString.Trim & ".xml"

    End Function

    Private Sub btnLimpar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLimpar.Click

        'APENAS APAGA O ARQUIVO XML DE INCLUSAO
        If File.Exists(NomeArquivoXml()) Then
            Dim ds = New DataSet
            ds.ReadXml(NomeArquivoXml())
            If (ds.Tables.Count > 0) Then
                If ds.Tables(0).Rows(0).Item("NumeroChamado").ToString.Trim = "000000" Then
                    DeletaArquivoXml()
                End If
            End If
        End If
        Response.Redirect("DetalheChamadoTecnico.aspx")

    End Sub

    'Obtem o próximo índice do item
    'Se o último item é 03, retornará 04 independente se está com a marcação * (D_E_L_E_T_)

End Class