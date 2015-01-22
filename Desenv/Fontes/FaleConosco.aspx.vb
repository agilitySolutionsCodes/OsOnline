Imports System.Net.Mail
Imports System.IO


Public Class FaleConosco
    Inherits BaseWebUI

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            TituloPagina("Fale Conosco")
        End If

    End Sub

    Private Sub TituloPagina(ByVal sTexto As String)
        DirectCast(Me.Master.Controls(0).Controls(3).Controls(7).FindControl("oBarraUsuario"), BarraUsuario).PaginaAtual = sTexto
    End Sub

    Private Sub btnEnviar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEnviar.Click

        Dim sParam As String
        Dim oRet As New ctlRetornoGenerico
        Dim sMsgRet As String = ""
        Dim cBody As String

        'SE FOR REGULATORIA MANDA PARA THAYS, CASO CONTRARIO MANDA PARA CENTRAL TECNICA
        If drpAssunto.SelectedValue.Trim = "R" Then
            sParam = ctlUtil.GetParam("MV_OOFALEC")
        Else
            sParam = ctlUtil.GetParam("MV_OOFALEO")
        End If

        Try
            If sParam.Trim.Contains("@") Then
                oRet = Validar()

                If UploadAnexoUm.HasFile = True Then

                    If oRet.Sucesso Then

                        'Variáveis criadas para validação de extensão ao anexar documento
                        Dim appSettings As NameValueCollection = ConfigurationManager.AppSettings
                        Dim sChave = appSettings("Extensao").Split(CChar(","))
                        Dim anexoUm As String = Path.GetExtension(UploadAnexoUm.FileName.ToString())

                        For Each item In sChave

                            If anexoUm = item Then

                                Try
                                    Dim arquivo As String = ""
                                    arquivo = "E:/PROJETO_OSONLINE/osonlinehomologacao/OS_TempFiles/" & UploadAnexoUm.FileName.ToString
                                    'arquivo = "C:/Projetos/osonline/Desenv/Fontes/OS_TempFiles/" & UploadAnexoUm.FileName.ToString

                                    UploadAnexoUm.SaveAs(arquivo)

                                    'Dim nomeArquivo = System.IO.Path.GetFileName(UploadAnexoUm.PostedFile.FileName)
                                    'Dim caminho = Server.MapPath(UploadAnexoUm.FileName)
                                    HttpContext.Current.Session.Add("AnexoFaleConosco", UploadAnexoUm)
                                    HttpContext.Current.Session.Add("CaminhoAnexo", arquivo)

                                    Exit For

                                Catch ex As Exception
                                    Throw ex
                                End Try

                            Else
                                oRet.Sucesso = False
                                oRet.Mensagem = "Extensão do arquivo anexado é inválida"
                            End If
                        Next
                    End If
                    'Else
                    '    oRet.Sucesso = False
                    '    If drpAssunto.SelectedValue.Trim = "R" Then
                    '        sParam = "MV_OOFALEC"
                    '        oRet.Sucesso = True
                    '    Else
                    '        sParam = "MV_OOFALEO"
                    '        oRet.Sucesso = True
                    '    End If
                    '    oRet.Mensagem = "Necessário preencher o parâmetro " & sParam & " no Protheus. Favor entrar em contato com a Intermed."
                    'End If
                End If
            End If

            If oRet.Sucesso Then
                'envia email para responsavel com dados
                cBody = "<html><head><link rel=""stylesheet"" type=""text/css"" href=""http://www.intermed.com.br/osonline/App_Themes/padrao/Estilos.css""></head>"
                cBody += "<body><form>"
                cBody += "<center><table class=""formulario"" style=""width:680px;"" >"
                cBody += "<tr><td><img style=""border-width:0px;"" src=""http://www.intermed.com.br/osonline/App_Themes/padrao/TopoEmail.gif"" title=""Carefusion""></td></tr>"
                cBody += "<tr><td class=""label"" align=""center"">FALE CONOSCO</td></tr>"
                cBody += "<tr><td align=""left""><strong>Licenciado:</strong> " & HttpContext.Current.Session("NomeUsuario").ToString & "</td></tr>"
                cBody += "<tr><td align=""left""><strong>Nome:</strong> " & txtNome.Text.Trim & "</td></tr>"
                cBody += "<tr><td align=""left""><strong>E-mail:</strong> " & txtEmail.Text.Trim & "</td></tr>"
                cBody += "<tr><td align=""left""><strong>Assunto:</strong> " & drpAssunto.SelectedItem.ToString & "</td></tr>"
                cBody += "<tr><td align=""left""><strong>Descrição:</strong> " & txtDescricao.Text & "</td></tr>"
                cBody += "<tr><td align=""left""><strong><a href=mailto:" & HttpContext.Current.Session("EmailUsuario").ToString() & ">Responda aqui</a>"
                cBody += "<tr><td><img style=""border-width:0px;"" src=""http://www.intermed.com.br/osonline/App_Themes/padrao/RodapeEmail.gif"" title=""Intermed""></div>"
                cBody += "</td></tr></table></center>"
                cBody += "</form></body></html>"

                sMsgRet = ctlEmail.enviaMensagemEmail(sParam.Trim, "", "Fale Conosco - OsOnline", cBody)
                oRet.Mensagem = sMsgRet
                ApagarCampos()
            End If

            oMensagem.SetMessage(oRet)

        Catch ex As Exception

        End Try
    End Sub

    Private Sub ApagarCampos()
        txtNome.Text = ""
        txtEmail.Text = ""
        drpAssunto.SelectedValue = ""
        txtDescricao.Text = ""
    End Sub

    Private Function Validar() As ctlRetornoGenerico

        Dim oRet As New ctlRetornoGenerico

        oRet.Sucesso = False

        If txtNome.Text.Trim = "" Then
            oRet.Mensagem = "Por favor preencha o nome para contato."
        ElseIf txtEmail.Text.Trim = "" Then
            oRet.Mensagem = "Por favor preencha o e-mail."
        ElseIf Not txtEmail.Text.Contains("@") Then
            oRet.Mensagem = "Por favor preencha um e-mail válido."
        ElseIf drpAssunto.SelectedIndex < 1 Then
            oRet.Mensagem = "Por favor, selecione um assunto."
        ElseIf txtDescricao.Text.Trim = "" Then
            oRet.Mensagem = "Por favor escreva uma descrição para a mensagem."
        Else
            oRet.Sucesso = True
        End If

        Return oRet

    End Function

End Class