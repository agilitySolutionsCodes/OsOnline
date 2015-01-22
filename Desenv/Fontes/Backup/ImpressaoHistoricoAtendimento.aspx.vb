Imports System.Data.SqlClient

Public Class ImpressaoHistoricoAtendimento
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            Dim sNumSerie = Request("NumSerie")

            If sNumSerie.Trim <> "" Then
                Preencher(sNumSerie)
            Else
                Dim oRet As New ctlRetornoGenerico
                oRet.Sucesso = False
                ctlUtil.EscreverLogErro("ImpressaoHistoricoAtendimento: Usuario tentou imprimir um documento que não possuia numero de serie")
                oRet.Mensagem = "Erro na impressão do documento. Número de série não encontrado. Favor reiniciar o processo."
                oMensagem.SetMessage(oRet)
            End If
        End If

    End Sub

    Private Sub Preencher(sNumSerie As String)
        Try
            Dim oHistorico As New ctlHistoricoProduto
            Dim dt As New DataTable
            Dim dtItens As New DataTable
            Dim sConteudo As String = ""
            Dim nCont As Integer = 0
            Dim sClass As String = ""
            Dim sClass2 As String = ""

            dt = oHistorico.ListarAtendimento(sNumSerie)

            If dt.Rows.Count = 0 Then
                oMensagem.SetMessage("A pesquisa não retornou resultados")
            Else

                lblSerie.Text = sNumSerie


                For Each dr As DataRow In dt.Rows
                    sConteudo += "<table width='100%' style='color: #000000'>"
                    sConteudo += "<tr>"
                    sConteudo += "<td class='tdBTGrossa'>"
                    sConteudo += "<br/>"
                    sConteudo += "<span class='label'>Nr. Atendimento:&nbsp;&nbsp;</span>" & dr.Item("NumAtendimento").ToString & "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                    sConteudo += "<span class='label'>Sequência:&nbsp;&nbsp;</span>" & dr.Item("Sequencia").ToString & "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                    sConteudo += "<span class='label'>Ocorrência:&nbsp;&nbsp;</span>" & dr.Item("Ocorrencia").ToString
                    sConteudo += "<br/><br/></td>"
                    sConteudo += "</tr>"

                    sConteudo += "<tr>"
                    sConteudo += "<td> <br/> "
                    sConteudo += "<span class='label'>Defeito:&nbsp;&nbsp;</span>" & dr.Item("DefeitoConstatado").ToString & " <br/> <br/>"
                    sConteudo += "<span class='label'>Causa:&nbsp;&nbsp;</span>" & dr.Item("CausaProvavel").ToString & " <br/> <br/>"
                    sConteudo += "<span class='label'>Serv. Executado:&nbsp;&nbsp;</span>" & dr.Item("ServicoExecutado").ToString & " <br/> <br/> "
                    sConteudo += " <br/></td>"
                    sConteudo += "</tr>"

                    dtItens = oHistorico.ListarItensAtendimento(dr.Item("NumAtendimento").ToString)

                    If dtItens.Rows.Count > 0 Then
                        sConteudo += "<br/><br/><table cellpadding='5' cellspacing='0' width='100%' style='color: #000000;'>"
                        sConteudo += "<tr>"
                        sConteudo += "<td class='tdBLT'><span class='label'>Item</span></td>"
                        sConteudo += "<td class='tdBLT'><span class='label'>Código Peça</span></td>"
                        sConteudo += "<td class='tdBLTR'><span class='label'>Descrição Peça</span></td>"
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
                        sConteudo += "<td class='" & sClass & "'>" & drItens.Item("CodProduto").ToString & "</td>"
                        sConteudo += "<td class='" & sClass2 & "'>" & drItens.Item("Descricao").ToString & "</td>"

                        sConteudo += "</tr>"
                    Next

                    If dtItens.Rows.Count > 0 Then
                        sConteudo += "</table>"
                    End If

                    sConteudo += "</table>"

                Next

                litConteudo.Text = sConteudo

            End If

        Catch ex As Exception
            Dim oRet As New ctlRetornoGenerico
            oRet.Sucesso = False
            ctlUtil.EscreverLogErro("ConsultaHistoricoProduto - Imprimir: " & ex.Message())
            oRet.Mensagem = ctlUtil.sMsgErroPadrao
            oMensagem.SetMessage(oRet)
        End Try
    End Sub

    Private Sub btnVoltar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnVoltar.Click
        Response.Redirect("ConsultaHistoricoProduto.aspx?NumSerie=" & lblSerie.Text)
    End Sub

End Class