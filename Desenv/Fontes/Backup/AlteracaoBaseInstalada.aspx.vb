Imports System.Data.SqlClient

Public Class AlteracaoBaseInstalada
    Inherits BaseWebUI

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("sListaSeries").ToString <> "" Then
                TituloPagina("Base Instalada - Alteração")
                If Session("sTipo").ToString = "CT" Then
                    lblTitBaseInstalada.Text = "Prencha os dados da base instalada para que o seu chamado seja incluído com sucesso"
                Else
                    lblTitBaseInstalada.Text = "Prencha os dados da base instalada para que sua ordem de serviço seja incluída com sucesso"
                End If

                Preencher()
            Else
                Dim oRet As New ctlRetornoGenerico
                oRet.Sucesso = False
                oRet.Mensagem = "Erro no carregamento da página. Favor voltar para chamado técnico e efetuar o processo novamente."
                oMensagem.SetMessage(oRet)
            End If
        End If
    End Sub

    Private Sub grdItens_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdItens.RowDataBound

        Dim nColunaTotal As Integer = e.Row.Cells.Count - 1

        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim drpTecnico As DropDownList = DirectCast(e.Row.FindControl("drpTecnico"), DropDownList)
            Dim drpEstado As DropDownList = DirectCast(e.Row.FindControl("drpEstado"), DropDownList)
            Dim drpCidade As DropDownList = DirectCast(e.Row.FindControl("drpCidade"), DropDownList)
            Dim reader As SqlDataReader = Nothing
            Dim ct As New ctlAtendimento
            Dim oBase As New ctlBaseInstalada
            Dim os As New ctlOrdemServico
            Dim sSelecionado As String


            reader = oBase.ListarEstados
            If reader.HasRows Then
                drpEstado.DataSource = reader
                drpEstado.DataBind()
                drpEstado.Items.Insert(0, "Nenhum")
                drpEstado.Items(0).Value = ""
                sSelecionado = DataBinder.Eval(e.Row.DataItem, "Sigla").ToString.Trim
                If Not String.IsNullOrEmpty(sSelecionado) Then
                    drpEstado.Items.FindByValue(sSelecionado).Selected = True
                End If
                reader.Close()
            End If

            If drpEstado.SelectedValue.Trim <> "" Then
                reader = oBase.ListarCidades(drpEstado.SelectedValue)
                If reader.HasRows Then
                    drpCidade.DataSource = reader
                    drpCidade.DataBind()
                    drpCidade.Items.Insert(0, "Nenhum")
                    drpCidade.Items(0).Value = ""
                    sSelecionado = DataBinder.Eval(e.Row.DataItem, "Cidade").ToString.Trim
                    If Not String.IsNullOrEmpty(sSelecionado) Then
                        drpCidade.Items.FindByValue(sSelecionado).Selected = True
                    End If
                    reader.Close()
                Else
                    drpCidade.Items.Insert(0, "Nenhum")
                    drpCidade.Items(0).Value = ""
                End If
            End If


            reader = ct.ListarTecnicos(Session("Regiao").ToString)
            If reader.HasRows Then
                drpTecnico.DataSource = reader
                drpTecnico.DataBind()
                drpTecnico.Items.Insert(0, "Nenhum")
                drpTecnico.Items(0).Value = ""
                sSelecionado = DataBinder.Eval(e.Row.DataItem, "CODIGOTECNICO").ToString.Trim
                If Not String.IsNullOrEmpty(sSelecionado) Then
                    If drpTecnico.Items.FindByValue(sSelecionado) IsNot Nothing Then
                        drpTecnico.Items.FindByValue(sSelecionado).Selected = True
                    End If
                End If
                reader.Close()
            End If

            reader = Nothing

        End If

    End Sub

    Private Sub Preencher()
        Dim base As New ctlBaseInstalada
        Dim dt As DataTable
        Dim oRet As New ctlRetornoGenerico

        Try
            dt = base.ListarBases(Session("sListaSeries").ToString)
            grdItens.DataSource = dt
            grdItens.DataBind()

            ViewState.Add("BaseInstalada", dt)

        Catch ex As Exception
            oRet.Sucesso = False
            ctlUtil.EscreverLogErro("AlteracaoBaseInstalada - Preencher: " & ex.Message())
            oRet.Mensagem = ctlUtil.sMsgErroPadrao
            oMensagem.SetMessage(oRet)
        End Try
    End Sub

    Private Sub btnConfirmar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConfirmar.Click

        Dim base As New ctlBaseInstalada
        Dim oRet As New ctlRetornoGenerico
        Dim oRetChamado As New ctlRetornoGenerico
        Dim oCliente As ctlCliente

        Try
            oRet = Validar()
            If oRet.Sucesso Then

                Dim lblNumSerie As String = ""
                Dim txtEndereco As String = ""
                Dim txtLocal As String = ""
                Dim txtContato As String = ""
                Dim txtTel As String = ""
                Dim txtDtInst As String = ""
                Dim drpTecnico As String = ""
                Dim drpEstado As String = ""
                Dim drpCidade As String = ""
                Dim sData As String = ""
                Dim sData2 As String = ""

                oRet.Sucesso = False
                For Each gr As GridViewRow In grdItens.Rows
                    lblNumSerie += DirectCast(gr.FindControl("lblNumSerie"), Label).Text & ";"
                    txtEndereco += DirectCast(gr.FindControl("txtEndereco"), TextBox).Text & ";"
                    txtLocal += DirectCast(gr.FindControl("txtLocal"), TextBox).Text & ";"
                    txtContato += DirectCast(gr.FindControl("txtContato"), TextBox).Text & ";"
                    txtTel += DirectCast(gr.FindControl("oPhoneBox"), PhoneBox).Text & ";"
                    sData = DirectCast(gr.FindControl("oDataInstalacao"), DateBox).Text
                    If IsDate(sData) Then
                        sData2 = Right(sData, 4) & sData.Substring(3, 2) & Left(sData, 2)
                    Else
                        sData2 = ""
                    End If
                    txtDtInst += sData2 & ";"
                    drpTecnico += DirectCast(gr.FindControl("drpTecnico"), DropDownList).SelectedValue & ";"
                    drpEstado += DirectCast(gr.FindControl("drpEstado"), DropDownList).SelectedValue & ";"
                    drpCidade += DirectCast(gr.FindControl("drpCidade"), DropDownList).SelectedValue & ";"
                    'sCidade += DirectCast(gr.FindControl("txtCidade"), TextBox).Text & ";"
                Next

                base.Salvar(lblNumSerie, txtEndereco, txtLocal, txtDtInst, txtTel, txtContato, drpTecnico, drpEstado, drpCidade)
                oRet.Sucesso = True
                oRet.Mensagem = "Dados da base instalada atualizados com sucesso."

                If Session("sTipo").ToString = "CT" Then
                    Dim oChamado As New DetalheChamadoTecnico
                    Dim oCT As ctlChamadoTecnico
                    Oct = DirectCast(Session("oChamado"), ctlChamadoTecnico)
                    oCliente = DirectCast(DirectCast(Session("oChamado"), ctlChamadoTecnico).Cliente, ctlCliente)

                    oRetChamado = oChamado.GravarChamado(oCT, oCliente, DirectCast(Session("oItens"), DataTable), False, DirectCast(Session("bIncluiChamado"), Boolean))
                Else
                    Dim oChamado As New DetalheOrdemServico
                    Dim oCT As ctlOrdemServico
                    Oct = DirectCast(Session("oChamado"), ctlOrdemServico)
                    oCliente = DirectCast(DirectCast(Session("oChamado"), ctlOrdemServico).Cliente, ctlCliente)

                    oRetChamado = oChamado.GravarOS(oCT, oCliente, DirectCast(Session("oItens"), DataTable), False, DirectCast(Session("bIncluiChamado"), Boolean))
                End If
                

                oRet.Mensagem += "<br/>" & oRetChamado.Mensagem
                Session.Remove("oChamado")
                Session.Remove("oItens")
                Session.Remove("bIncluiChamado")
                Session.Remove("sListaSeries")
                Session.Remove("sTipo")
                btnConfirmar.Enabled = False

                EnviaEmail()

            End If

        Catch ex As Exception
            oRet.Sucesso = False
            ctlUtil.EscreverLogErro("AlteracaoBaseInstalada - btnConfirmar_Click: " & ex.Message())
            oRet.Mensagem = ctlUtil.sMsgErroPadrao
        End Try
        oMensagem.SetMessage(oRet)
    End Sub

    Private Sub EnviaEmail()

        Dim cBody As String
        Dim sNumSerie As String
        Dim sEndereco As String
        Dim sLocal As String
        Dim sContato As String
        Dim sTel As String
        Dim sDtInst As String
        Dim sTelFormatado As String = ""
        Dim drpTecnico As String
        Dim sEstado As String
        Dim sCidade As String
        Dim sParam As String = ctlUtil.GetParam("MV_OOBASEI")
        Dim oRet As New ctlRetornoGenerico
        Dim sMsgRet As String

        If Not String.IsNullOrEmpty(sParam.Trim) Then
            'envia email para responsavel com dados
            cBody = "<html><head><link rel=""stylesheet"" type=""text/css"" href=""http://www.intermed.com.br/osonline/App_Themes/padrao/Estilos.css""></head>"
            cBody += "<body><form>"
            cBody += "<center><table class=""formulario"" style=""width:680px;"" >"
            cBody += "<tr><td><img style=""border-width:0px;"" src=""http://www.intermed.com.br/osonline/App_Themes/padrao/TopoEmail.gif"" title=""Intermed""></td></tr>"
            cBody += "<tr><td class=""label"" align=""center"">ALTERAÇÃO DA BASE INSTALADA</td></tr>"

            For Each gr As GridViewRow In grdItens.Rows
                sNumSerie = DirectCast(gr.FindControl("lblNumSerie"), Label).Text
                sEndereco = DirectCast(gr.FindControl("txtEndereco"), TextBox).Text
                sLocal = DirectCast(gr.FindControl("txtLocal"), TextBox).Text
                sCidade = DirectCast(gr.FindControl("drpCidade"), DropDownList).SelectedItem.ToString.Trim
                sEstado = DirectCast(gr.FindControl("drpEstado"), DropDownList).SelectedItem.ToString.Trim
                sContato = DirectCast(gr.FindControl("txtContato"), TextBox).Text
                sTel = DirectCast(gr.FindControl("oPhoneBox"), PhoneBox).Text
                sDtInst = DirectCast(gr.FindControl("oDataInstalacao"), DateBox).Text
                drpTecnico = DirectCast(gr.FindControl("drpTecnico"), DropDownList).SelectedItem.ToString.Trim
                sTelFormatado = sTel
                If sTel.Length >= 10 Then
                    sTelFormatado = "(" & Left(sTel, 2) & ") " & sTel.Substring(2, 4) & "-" & Right(sTel, 4)
                End If
                cBody += "<tr><td align=""left""><hr/></tr>"
                cBody += "<tr><td align=""left""><strong>Nº de Série:</strong> " & sNumSerie & "</td></tr>"
                cBody += "<tr><td align=""left""><strong>Licenciado:</strong> " & HttpContext.Current.Session("NomeUsuario").ToString & "</td></tr>"
                cBody += "<tr><td align=""left""><strong>Local Instalação:</strong> " & sLocal & "</td></tr>"
                cBody += "<tr><td align=""left""><strong>Endereço:</strong> " & sEndereco & "</td></tr>"
                cBody += "<tr><td align=""left""><strong>Cidade:</strong> " & sCidade & "</td></tr>"
                cBody += "<tr><td align=""left""><strong>Estado:</strong> " & sEstado & "</td></tr>"
                cBody += "<tr><td align=""left""><strong>Telefone:</strong> " & sTelFormatado & "</td></tr>"
                cBody += "<tr><td align=""left""><strong>Contato:</strong> " & sContato & "</td></tr>"
                cBody += "<tr><td align=""left""><strong>Técnico:</strong> " & drpTecnico & "</td></tr>"
                cBody += "<tr><td align=""left""><strong>Data da Instalação:</strong> " & sDtInst & "</td></tr>"
            Next

            cBody += "<tr><td><img style=""border-width:0px;"" src=""http://www.intermed.com.br/osonline/App_Themes/padrao/RodapeEmail.gif"" title=""Intermed""></div>"
            cBody += "</td></tr></table></center>"
            cBody += "</form></body></html>"

            sMsgRet = ctlEmail.enviaMensagemEmail(sParam, "", "Alteração Base Instalada", cBody)
        Else
            oRet.Sucesso = False
            oRet.Mensagem = "Necessário preencher o parâmetro MV_OOBASEI no Protheus. Favor entrar em contato com a Intermed."
        End If
        oMensagem.SetMessage(oRet)
    End Sub

    Private Function Validar() As ctlRetornoGenerico
        Dim txtEndereco As String
        Dim txtLocal As String
        Dim txtContato As String
        Dim txtTel As String
        Dim txtDtInst As String
        Dim drpCidade As DropDownList
        Dim drpTecnico As DropDownList
        Dim drpEstado As DropDownList
        Dim oRet As New ctlRetornoGenerico

        oRet.Sucesso = False
        oRet.Mensagem = ""
        For Each gr As GridViewRow In grdItens.Rows
            If Session("sListaSeries").ToString.Contains(DirectCast(gr.FindControl("lblNumSerie"), Label).Text.Trim) Then

                txtEndereco = DirectCast(gr.FindControl("txtEndereco"), TextBox).Text
                txtLocal = DirectCast(gr.FindControl("txtLocal"), TextBox).Text
                txtContato = DirectCast(gr.FindControl("txtContato"), TextBox).Text
                txtTel = DirectCast(gr.FindControl("oPhoneBox"), PhoneBox).Text
                txtDtInst = DirectCast(gr.FindControl("oDataInstalacao"), DateBox).Text
                drpTecnico = DirectCast(gr.FindControl("drpTecnico"), DropDownList)
                drpEstado = DirectCast(gr.FindControl("drpEstado"), DropDownList)
                'txtCidade = DirectCast(gr.FindControl("txtCidade"), TextBox).Text
                drpCidade = DirectCast(gr.FindControl("drpCidade"), DropDownList)

                If txtEndereco.Trim = "" Then
                    oRet.Mensagem = "O endereço não foi informado na linha " + (gr.RowIndex + 1).ToString + "."
                ElseIf txtLocal.Trim = "" Then
                    oRet.Mensagem = "O local não foi informado na linha " + (gr.RowIndex + 1).ToString + "."
                ElseIf drpCidade.SelectedIndex < 1 Then
                    oRet.Mensagem = "A cidade não foi informada na linha " + (gr.RowIndex + 1).ToString + "."
                ElseIf drpEstado.SelectedIndex < 1 Then
                    oRet.Mensagem = "O estado não foi informado na linha " + (gr.RowIndex + 1).ToString + "."
                ElseIf txtContato.Trim = "" Then
                    oRet.Mensagem = "O contato não foi informado na linha " + (gr.RowIndex + 1).ToString + "."
                ElseIf txtTel.Trim = "" Then
                    oRet.Mensagem = "O telefone não foi informado na linha " + (gr.RowIndex + 1).ToString + "."
                ElseIf Not IsDate(txtDtInst) Then
                    oRet.Mensagem = "A data não foi informada na linha " + (gr.RowIndex + 1).ToString + "."
                ElseIf drpTecnico.SelectedIndex < 1 Then
                    oRet.Mensagem = "O técnico não foi informado na linha " + (gr.RowIndex + 1).ToString + "."
                End If
            End If
        Next
        If oRet.Mensagem.Trim.Length = 0 Then
            oRet.Sucesso = True
        End If
        Return oRet
    End Function

    Private Sub TituloPagina(ByVal sTexto As String)
        DirectCast(Me.Master.Controls(0).Controls(3).Controls(7).FindControl("oBarraUsuario"), BarraUsuario).PaginaAtual = sTexto
    End Sub

    Private Sub btnVoltar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnVoltar.Click
        Response.Redirect("ConsultaChamadoTecnico.aspx")
    End Sub

    Public Sub CarregarCidade(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim oBase As New ctlBaseInstalada
        Dim reader As SqlDataReader
        Dim sEstadoAtual As String = DirectCast(sender, DropDownList).SelectedValue
        Dim oRet As New ctlRetornoGenerico
        Dim nLinha = CType(CType(sender, Control).NamingContainer, GridViewRow).RowIndex
        Dim drpCidade = DirectCast(grdItens.Rows(nLinha).FindControl("drpCidade"), DropDownList)


        Try
            If sEstadoAtual.Trim <> "" Then
                reader = oBase.ListarCidades(sEstadoAtual)
                If reader.HasRows Then
                    drpCidade.DataSource = reader
                    drpCidade.DataBind()
                    drpCidade.Items.Insert(0, "Nenhum")
                    drpCidade.Items(0).Value = ""
                    reader.Close()
                Else
                    drpCidade.Items.Insert(0, "Nenhum")
                    drpCidade.Items(0).Value = ""
                End If
            End If

        Catch ex As Exception
            oRet.Sucesso = False
            oRet.Mensagem = ex.Message
            oMensagem.SetMessage(oRet)
        End Try

    End Sub

End Class