Imports System.Data.SqlClient

Public Class ImpressaoAtendimento
    Inherits BaseWebUI

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                If Request("Numero") IsNot Nothing Then
                    Dim reader As SqlDataReader = Nothing
                    Dim numero As String = Request("Numero").Substring(0, 10)
                    Dim nrAtendimentoOS As String = Request("Numero").Substring(0, 8)
                    Dim sequencia As String = Request("Numero").Substring(8, 2)
                    Dim nrOS As String = Request("Numero").Substring(0, 6)
                    Dim item As String = Request("Numero").Substring(6, 2)
                    Dim modo As String = Request("Numero").Substring(16)
                    If modo = "A" OrElse modo = "C" Then
                        pnlCliente.Visible = False
                    End If
                    If modo = "A" OrElse modo = "B" Then
                        Dim cn As New ctlAtendimento
                        reader = cn.Selecionar(numero)
                        btnVoltar.CommandArgument = "ConsultaAtendimento.aspx?Numero=" + numero
                        If reader.HasRows() Then
                            Preencher(reader)
                        End If
                    Else
                        Dim cn As New ctlOrdemServico
                        reader = cn.Selecionar(nrOS, item)
                        If reader.HasRows() Then
                            lblNumero.Text = nrOS 'nrAtendimentoOS
                            lblSequencia.Text = item 'sequencia
                            Dim dt As New DataTable
                            If reader IsNot Nothing Then
                                dt.Load(reader)
                                reader.Close()
                            End If
                            PreencherCabecalho(dt)
                            btnVoltar.CommandArgument = "DetalheOrdemServico.aspx?Numero=" + nrOS
                        Else
                            btnVoltar.CommandArgument = "ConsultaOrdemServico.aspx"
                        End If
                        PreencherEmBranco(reader)
                        lblTitSequencia.Text = "Item"
                    End If
                End If
            End If
        Catch ex As Exception
            Dim oRet As New ctlRetornoGenerico
            ctlUtil.EscreverLogErro("ImpressaoAtendimento - Page_load: " & ex.Message())
            oRet.Mensagem = ctlUtil.sMsgErroPadrao

            oRet.Sucesso = False
            oMensagem.SetMessage(oRet)
        End Try
    End Sub

    Private Sub Preencher(ByVal reader As SqlDataReader)

        Dim dt As New DataTable
        If reader IsNot Nothing Then
            dt.Load(reader)
            reader.Close()
        End If

        PreencherCabecalho(dt)

        lblNumero.Text = dt.Rows(0).Item("NumeroOS").ToString.Trim
        lblSequencia.Text = dt.Rows(0).Item("Sequencia").ToString.Trim
        


        If dt.Rows(0).Item("DataInicio") IsNot Nothing Then
            If IsDate(dt.Rows(0).Item("DataInicio")) Then
                lblDataInicio.Text = DirectCast(dt.Rows(0).Item("DataInicio"), Date).ToShortDateString
            Else
                lblDataInicio.Text = dt.Rows(0).Item("DataInicio").ToString
            End If
        End If
        If Not String.IsNullOrEmpty(dt.Rows(0).Item("HoraInicio").ToString) Then
            If IsDate(dt.Rows(0).Item("HoraInicio")) Then
                lblHoraInicio.Text = CDate(dt.Rows(0).Item("HoraInicio")).ToShortTimeString
            Else
                lblHoraInicio.Text = dt.Rows(0).Item("HoraInicio").ToString
            End If
        End If
        If Not String.IsNullOrEmpty(dt.Rows(0).Item("DataTermino").ToString) Then
            If IsDate(dt.Rows(0).Item("DataTermino")) Then
                lblDataTermino.Text = CDate(dt.Rows(0).Item("DataTermino")).ToShortDateString
            Else
                lblDataTermino.Text = dt.Rows(0).Item("DataTermino").ToString
            End If
        End If
        If Not String.IsNullOrEmpty(dt.Rows(0).Item("HoraTermino").ToString) Then
            If IsDate(dt.Rows(0).Item("HoraTermino")) Then
                lblHoraTermino.Text = CDate(dt.Rows(0).Item("HoraTermino")).ToShortTimeString
            Else
                lblHoraTermino.Text = dt.Rows(0).Item("HoraTermino").ToString
            End If
        End If

        Dim defeito As String = dt.Rows(0).Item("DefeitoConstatado").ToString
        Dim causa As String = dt.Rows(0).Item("CausaProvavel").ToString
        Dim servico As String = dt.Rows(0).Item("ServicoExecutado").ToString

        If defeito.Length > 0 Then
            lblDefeito.Text = defeito.Substring(0, defeito.Length - 1)
        Else
            lblDefeito.Text = defeito
        End If
        If causa.Length > 0 Then
            lblCausa.Text = causa.Substring(0, causa.Length - 1)
        Else
            lblCausa.Text = causa
        End If
        If servico.Length > 0 Then
            lblServExecutado.Text = servico.Substring(0, servico.Length - 1)
        Else
            lblServExecutado.Text = servico
        End If

        dt.DefaultView.RowFilter = " D_E_L_E_T_ <> '*' "
        grdItens.DataSource = dt
        grdItens.DataBind()

    End Sub

    Private Sub btnVoltar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnVoltar.Click
        'Dim vAtendimento() As String = Nothing
        'ReDim Preserve vAtendimento(1)
        'vAtendimento(0) = lblSerie.Text
        'vAtendimento(1) = lblCodigoOcorrencia.Text
        'Session("Atendimento") = vAtendimento
        Response.Redirect(btnVoltar.CommandArgument)
    End Sub

    Private Sub grdItens_DataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdItens.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lblQuantidade = DirectCast(e.Row.FindControl("lblQuantidade"), Label)
            Dim lblProduto = DirectCast(e.Row.FindControl("lblProduto"), Label)
            Dim lblCodigo = DirectCast(e.Row.FindControl("lblCodigo"), Label)
            Dim lblSerie = DirectCast(e.Row.FindControl("lblSerie"), Label)
            Dim lblLote = DirectCast(e.Row.FindControl("lblLote"), Label)
            If lblQuantidade.Text.Trim = "0" OrElse String.IsNullOrEmpty(lblQuantidade.Text.Trim) Then
                lblQuantidade.Text = "N/C"
            End If
            If String.IsNullOrEmpty(lblCodigo.Text.Trim) Then
                lblCodigo.Text = "N/C"
            End If
            If String.IsNullOrEmpty(lblProduto.Text.Trim) Then
                lblProduto.Text = "N/C"
            End If
            If String.IsNullOrEmpty(lblSerie.Text.Trim) Then
                lblSerie.Text = "N/C"
            End If
            If String.IsNullOrEmpty(lblLote.Text.Trim) Then
                lblLote.Text = "N/C"
            End If
            Dim reader As SqlDataReader = Nothing
        End If
    End Sub

    Private Sub PreencherEmBranco(ByVal reader As SqlDataReader)

        Dim dt As New DataTable
        If reader IsNot Nothing Then
            dt.Load(reader)
            reader.Close()
        End If

        'lblGarantia.Text = "Não"

        lblDataInicio.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + "/" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + "/" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
        lblHoraInicio.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + ":" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
        lblDataTermino.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + "/" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + "/" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
        lblHoraTermino.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + ":" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"

        lblDefeito.Text = "<br/>" + "_ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _" + "<br/> <br/>" + _
                                  "_ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _" + "<br/> <br/>" + _
                                  "_ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _"
        lblCausa.Text = "<br/>" + "_ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _" + "<br/> <br/>" + _
                              "_ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _" + "<br/> <br/>" + _
                              "_ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _"
        lblServExecutado.Text = "<br/>" + "_ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _" + "<br/> <br/>" + _
                                 "_ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _" + "<br/> <br/>" + _
                                 "_ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _"

        dt = New DataTable
        dt.Columns.Add("NumeroSeriePeca")
        dt.Columns.Add("CodigoItem")
        dt.Columns.Add("DescricaoItem")
        dt.Columns.Add("Quantidade")
        dt.Columns.Add("NumeroLote")

        For cont = 1 To 17
            Dim dr As DataRow = dt.NewRow
            If (cont Mod 2) <> 0 Then
                dr("NumeroSeriePeca") = "_ _ _ _ _ _ _ _ _ _ _ _ _"
                dr("CodigoItem") = "_ _ _ _ _ _ _ _ _"
                dr("DescricaoItem") = "_ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _"
                dr("Quantidade") = "_ _ _ _ _"
                dr("NumeroLote") = "_ _ _ _ _ _ _ _"
            Else
                dr("NumeroSeriePeca") = "<br/>"
                dr("CodigoItem") = "<br/>"
                dr("DescricaoItem") = "<br/>"
                dr("Quantidade") = "<br/>"
                dr("NumeroLote") = "<br/>"
            End If
            dt.Rows.Add(dr)
        Next

        grdItens.DataSource = dt
        grdItens.DataBind()

    End Sub

    Private Sub PreencherCabecalho(ByVal dt As DataTable)

        If String.IsNullOrEmpty(dt.Rows(0).Item("FilialCliente").ToString.Trim) Then
            lblCliente.Text = dt.Rows(0).Item("CodigoCliente").ToString _
                               + " - " + dt.Rows(0).Item("NomeCliente").ToString
        Else
            lblCliente.Text = dt.Rows(0).Item("CodigoCliente").ToString _
                               + "/" + dt.Rows(0).Item("FilialCliente").ToString _
                               + " - " + dt.Rows(0).Item("NomeCliente").ToString
        End If

        lblEndereco.Text = dt.Rows(0).Item("EnderecoCliente").ToString _
                           + " - " + dt.Rows(0).Item("BairroCliente").ToString _
                           + " - " + dt.Rows(0).Item("CidadeCliente").ToString _
                           + "/" + dt.Rows(0).Item("EstadoCliente").ToString _
                           + " - " + dt.Rows(0).Item("CepCliente").ToString

        If Not String.IsNullOrEmpty(dt.Rows(0).Item("NumeroChamado").ToString.Trim) Then
            lblChamado.Text = dt.Rows(0).Item("NumeroChamado").ToString
            If Not String.IsNullOrEmpty(dt.Rows(0).Item("DataEmissao").ToString.Trim) Then
                lblDataChamado.Text = DirectCast(dt.Rows(0).Item("DataEmissao"), Date).ToShortDateString
            End If
            If Not String.IsNullOrEmpty(dt.Rows(0).Item("HoraEmissao").ToString.Trim) Then
                lblHoraChamado.Text = dt.Rows(0).Item("HoraEmissao").ToString
            End If
            lblObservacao.Text = dt.Rows(0).Item("Comentario").ToString

            If Not String.IsNullOrEmpty(dt.Rows(0).Item("NomeContato").ToString.Trim) Then
                lblContato.Text = dt.Rows(0).Item("NomeContato").ToString
            End If
            If Not String.IsNullOrEmpty(dt.Rows(0).Item("TelefoneContato").ToString.Trim) Then
                lblTelefone.Text = dt.Rows(0).Item("TelefoneContato").ToString
            End If
        End If

        If Not String.IsNullOrEmpty(dt.Rows(0).Item("CodigoProduto").ToString.Trim) Then
            lblProduto.Text = dt.Rows(0).Item("CodigoProduto").ToString _
                + " - " + dt.Rows(0).Item("DescricaoProduto").ToString
            lblSerie.Text = dt.Rows(0).Item("NumeroSerieProduto").ToString
        End If

        If Not String.IsNullOrEmpty(dt.Rows(0).Item("CodigoOcorrencia").ToString.Trim) Then
            lblOcorrencia.Text = dt.Rows(0).Item("CodigoOcorrencia").ToString
        End If

        If Not String.IsNullOrEmpty(dt.Rows(0).Item("DescricaoOcorrencia").ToString.Trim) Then
            lblOcorrencia.Text += " - " + dt.Rows(0).Item("DescricaoOcorrencia").ToString
        End If
        If Not String.IsNullOrEmpty(dt.Rows(0).Item("CodigoOcorrencia").ToString.Trim) Then
            lblCodigoOcorrencia.Text = dt.Rows(0).Item("CodigoOcorrencia").ToString
        End If

        If IsDate(dt.Rows(0).Item("DataGarantia")) Then
            lblGarantia.Text = Left(dt.Rows(0).Item("DataGarantia").ToString, 10)
            If CDate(dt.Rows(0).Item("DataGarantia")) < Date.Today Then
                lblGarantia.Text += " - Fora da garantia"
            Else
                lblGarantia.Text += " - Em garantia"
            End If
        Else
            lblGarantia.Text = "Não"
        End If

    End Sub

End Class