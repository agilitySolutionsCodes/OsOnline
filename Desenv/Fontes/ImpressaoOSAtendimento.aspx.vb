Public Class ImpressaoOSAtendimento
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            Dim sNumeroOS = Request("numero")

            If sNumeroOS.Trim <> "" Then
                Preencher(sNumeroOS)
            Else
                Dim oRet As New ctlRetornoGenerico
                oRet.Sucesso = False
                ctlUtil.EscreverLogErro("ImpressaoOSAtendimento: Usuario tentou imprimir um documento que não possuia numero de OS")
                oRet.Mensagem = "Erro na impressão do documento. Nº da ordem de serviço não encontrado. Favor reiniciar o processo."
                oMensagem.SetMessage(oRet)
            End If
        End If

    End Sub

    Private Sub Preencher(sNumeroOS As String)
        Try
            Dim oAtend As New ctlAtendimento
            Dim dt As New DataTable
            Dim dtItens As New DataTable
            Dim dtEtapas As New DataTable
            Dim sConteudo As String = ""
            Dim nCont As Integer = 0
            Dim sClass As String = ""
            Dim sClass2 As String = ""


            dt = oAtend.ListarTopoAtendimentos(sNumeroOS)
            If dt.Rows.Count > 0 Then
                lblCliente.Text = dt.Rows(0).Item("NomeCliente").ToString
                lblEndereco.Text = dt.Rows(0).Item("EnderecoCliente").ToString
                lblContato.Text = dt.Rows(0).Item("NomeContato").ToString
                lblTelefone.Text = dt.Rows(0).Item("TelefoneContato").ToString
                If dt.Rows(0).Item("NumeroChamado").ToString.Trim <> "" Then
                    lblChamado.Text = dt.Rows(0).Item("NumeroChamado").ToString
                    lblDataChamado.Text = Left(dt.Rows(0).Item("DataEmissao").ToString, 10)
                    lblHoraChamado.Text = dt.Rows(0).Item("HoraEmissao").ToString
                    lblObservacao.Text = dt.Rows(0).Item("Comentario").ToString
                End If
            End If


            dt = oAtend.ListarCabecalhoAtendimentos(sNumeroOS)


            If dt.Rows.Count = 0 Then
                oMensagem.SetMessage("A pesquisa não retornou resultados")
            Else
                lblNumero.Text = sNumeroOS

                For Each dr As DataRow In dt.Rows
                    sConteudo += "<table width='100%' style='color: #000000'>"
                    sConteudo += "<tr>"
                    sConteudo += "<td class='tdBTGrossa'>"
                    sConteudo += "<br/>"
                    sConteudo += "<span class='label'>Nr. Atendimento:&nbsp;&nbsp;</span>" & dr.Item("NumAtendimento").ToString & "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                    sConteudo += "<span class='label'>Sequência:&nbsp;&nbsp;</span>" & dr.Item("Sequencia").ToString & "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                    sConteudo += "<span class='label'>Nº Série:&nbsp;&nbsp;</span>" & dr.Item("NumeroSerieProduto").ToString
                    sConteudo += "<br/><br/></td>"
                    sConteudo += "</tr>"

                    sConteudo += "<tr>"
                    sConteudo += "<td><br/>"
                    sConteudo += "<span class='label'>Técnico:&nbsp;&nbsp;</span>" & dr.Item("NomeTecnico").ToString & "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                    sConteudo += "<span class='label'>Ocorrência:&nbsp;&nbsp;</span>" & dr.Item("Ocorrencia").ToString & "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                    sConteudo += "<br/><br/></td>"
                    sConteudo += "</tr>"

                    'sConteudo += "<tr>"
                    'sConteudo += "<td>"
                    'sConteudo += "<span class='label'>Garantia até:&nbsp;&nbsp;</span>" & Left(dr.Item("DataGarantia").ToString, 10) & "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                    'sConteudo += "<span class='label'>Versão original do software:&nbsp;&nbsp;</span>" & dr.Item("VersaoSoftware").ToString & "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                    'sConteudo += "<br/><br/></td>"
                    'sConteudo += "</tr>"

                    sConteudo += "<tr>"
                    sConteudo += "<td>"
                    sConteudo += "<span class='label'>Data Início:&nbsp;&nbsp;</span>" & Left(dr.Item("DataInicio").ToString, 10) & "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                    sConteudo += "<span class='label'>Hora Início:&nbsp;&nbsp;</span>" & dr.Item("HoraInicio").ToString & "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                    sConteudo += "<span class='label'>Data Término:&nbsp;&nbsp;</span>" & Left(dr.Item("DataTermino").ToString, 10) & "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                    sConteudo += "<span class='label'>Hora Término:&nbsp;&nbsp;</span>" & dr.Item("HoraTermino").ToString
                    'sConteudo += "<br/><br/></td>"
                    sConteudo += "</td>"
                    sConteudo += "</tr>"

                    'sConteudo += "<tr>"
                    'sConteudo += "<td>"
                    'sConteudo += "<span class='label'>Status:&nbsp;&nbsp;</span>" & dr.Item("DescricaoStatus").ToString & "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                    'sConteudo += "<span class='label'>Incluir Item OS:&nbsp;&nbsp;</span>" & dr.Item("DescricaoIncluirItemOS").ToString & "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                    'sConteudo += "<br/><br/></td>"
                    'sConteudo += "</tr>"
                    'sConteudo += "</table>"


                    'dtEtapas = oAtend.ListarEtapasAtendimento(dr.Item("NumAtendimento").ToString.Trim & dr.Item("Sequencia").ToString.Trim)

                    'If dtEtapas.Rows.Count > 0 Then
                    '    sConteudo += "<br/><br/><table cellpadding='5' cellspacing='0' width='100%' style='color: #000000;'>"
                    '    sConteudo += "<tr>"
                    '    sConteudo += "<td class='tdBLT'><span class='label'>Item</span></td>"
                    '    sConteudo += "<td class='tdBLT'><span class='label'>Etapa</span></td>"
                    '    sConteudo += "<td class='tdBLT'><span class='label'>Data Início</span></td>"
                    '    sConteudo += "<td class='tdBLT'><span class='label'>Hora Início</span></td>"
                    '    sConteudo += "<td class='tdBLT'><span class='label'>Data Término</span></td>"
                    '    sConteudo += "<td class='tdBLTR'><span class='label'>Hora Término</span></td>"
                    '    sConteudo += "</tr>"
                    'End If

                    'nCont = 0
                    'For Each drEtapas As DataRow In dtEtapas.Rows
                    '    nCont += 1
                    '    sConteudo += "<tr>"

                    '    If nCont = dtEtapas.Rows.Count Then
                    '        sClass = "tdBL"
                    '        sClass2 = "tdBLR"
                    '    Else
                    '        sClass = "tdL"
                    '        sClass2 = "tdLR"
                    '    End If

                    '    sConteudo += "<td class='" & sClass & "'>" & drEtapas.Item("Item").ToString & "</td>"
                    '    sConteudo += "<td class='" & sClass & "'>" & drEtapas.Item("Descricao").ToString & "</td>"
                    '    sConteudo += "<td class='" & sClass & "'>" & Left(drEtapas.Item("DataInicio").ToString, 10) & "</td>"
                    '    sConteudo += "<td class='" & sClass & "'>" & drEtapas.Item("HoraInicio").ToString & "</td>"
                    '    sConteudo += "<td class='" & sClass & "'>" & Left(drEtapas.Item("DataFim").ToString, 10) & "</td>"
                    '    sConteudo += "<td class='" & sClass2 & "'>" & drEtapas.Item("HoraFim").ToString & "</td>"

                    '    sConteudo += "</tr>"
                    'Next

                    'If dtEtapas.Rows.Count > 0 Then
                    '    sConteudo += "</table>"
                    'End If

                    'sConteudo += "<br/><table width='100%' style='color: #000000'>"
                    'sConteudo += "<tr>"
                    'sConteudo += "<td>"
                    'sConteudo += "<span class='label'>Defeito:&nbsp;&nbsp;</span>" & dr.Item("Defeito").ToString & "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                    'sConteudo += "<span class='label'>Servico:&nbsp;&nbsp;</span>" & dr.Item("Servico").ToString & "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                    'sConteudo += "<br/><br/></td>"
                    'sConteudo += "</tr>"

                    sConteudo += "<tr>"
                    sConteudo += "<td> <br/> "
                    sConteudo += "<span class='label'>Defeito:&nbsp;&nbsp;</span>" & dr.Item("DefeitoConstatado").ToString & " <br/> <br/>"
                    sConteudo += "<span class='label'>Causa:&nbsp;&nbsp;</span>" & dr.Item("CausaProvavel").ToString & " <br/> <br/>"
                    sConteudo += "<span class='label'>Serv. Executado:&nbsp;&nbsp;</span>" & dr.Item("ServicoExecutado").ToString & " <br/> <br/> "
                    sConteudo += " <br/></td>"
                    sConteudo += "</tr>"

                    dtItens = oAtend.ListarItensAtendimento(dr.Item("NumAtendimento").ToString.Trim & dr.Item("Sequencia").ToString.Trim)

                    If dtItens.Rows.Count > 0 Then
                        sConteudo += "<br/><br/><table cellpadding='5' cellspacing='0' width='100%' style='color: #000000;'>"
                        sConteudo += "<tr>"
                        sConteudo += "<td class='tdBLT'><span class='label'>Item</span></td>"
                        sConteudo += "<td class='tdBLT'><span class='label'>Peça</span></td>"
                        sConteudo += "<td class='tdBLT'><span class='label'>Quantidade</span></td>"
                        sConteudo += "<td class='tdBLT'><span class='label'>Nr. Série</span></td>"
                        sConteudo += "<td class='tdBLTR'><span class='label'>Lote</span></td>"
                        sConteudo += "</tr>"
                    End If

                    nCont = 0
                    For Each drItens As DataRow In dtItens.Rows
                        nCont += 1
                        sConteudo += "<tr>"

                        If nCont = dtItens.Rows.Count Then
                            sClass = "tdBL"
                            sClass2 = "tdBLR"
                        Else
                            sClass = "tdL"
                            sClass2 = "tdLR"
                        End If

                        sConteudo += "<td class='" & sClass & "'>" & drItens.Item("Item").ToString & "</td>"
                        sConteudo += "<td class='" & sClass & "'>" & drItens.Item("CodigoItem").ToString & " - " & drItens.Item("DescricaoItem").ToString & "</td>"
                        sConteudo += "<td class='" & sClass & "'>" & drItens.Item("Quantidade").ToString & "</td>"
                        sConteudo += "<td class='" & sClass & "'>" & drItens.Item("NumeroSeriePeca").ToString & "</td>"
                        sConteudo += "<td class='" & sClass2 & "'>" & drItens.Item("NumeroLote").ToString & "</td>"

                        sConteudo += "</tr>"
                    Next

                    If dtItens.Rows.Count > 0 Then
                        sConteudo += "</table><br/><br/>"
                    End If

                    sConteudo += "</table>"

                Next

                litConteudo.Text = sConteudo

            End If

        Catch ex As Exception
            Dim oRet As New ctlRetornoGenerico
            oRet.Sucesso = False
            ctlUtil.EscreverLogErro("ImpressaoOSAtendimento - Imprimir: " & ex.Message())
            oRet.Mensagem = ctlUtil.sMsgErroPadrao
            oMensagem.SetMessage(oRet)
        End Try
    End Sub


    Private Sub btnVoltar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnVoltar.Click
        Response.Redirect("DetalheOrdemServico.aspx?Numero=" & lblNumero.Text)
    End Sub

End Class