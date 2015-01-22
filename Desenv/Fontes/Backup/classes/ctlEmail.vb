Imports System.Net.Mail

Public Class ctlEmail

    ' </summary>
    ' <param name="from">Endereco do Remetente</param>
    ' <param name="recepient">Destinatario</param>
    ' <param name="bcc">recipiente Bcc</param>
    ' <param name="cc">recipiente Cc</param>
    ' <param name="subject">Assunto do email</param>
    ' <param name="body">Corpo da mensagem de email</param>

    Public Shared Function enviaMensagemEmail(ByVal recepient As String, ByVal cc As String, ByVal subject As String, ByVal body As String) As String
        Dim sMsgRet As String
        Dim aSMTP As ArrayList = DirectCast(HttpContext.Current.Session("DadosSMTP"), ArrayList)

        Try

            If aSMTP.Count > 1 Then

                Dim SmtpServer As New SmtpClient(CStr(aSMTP(0)), 25)
                Dim mail As New MailMessage()
                Dim aEmail As Array = {""}
                SmtpServer.Credentials = New Net.NetworkCredential(CStr(aSMTP(1)), CStr(aSMTP(2)))
                SmtpServer.UseDefaultCredentials = False
                mail = New MailMessage()
                mail.From = New MailAddress(CStr(aSMTP(1)))

                If recepient.Contains(";") Then
                    aEmail = recepient.Split(CChar(";"))
                ElseIf recepient.Contains(",") Then
                    aEmail = recepient.Split(CChar(","))
                Else
                    aEmail.SetValue(recepient, 0)
                End If

                For Each email As String In aEmail
                    If Not String.IsNullOrEmpty(email) Then
                        mail.To.Add(email.ToLower)
                    End If
                Next

                If Not cc Is Nothing And cc <> String.Empty Then
                    mail.CC.Add(New MailAddress(cc))
                End If
                mail.Subject = subject
                mail.Body = body
                ' Define o formato do email como HTML
                mail.IsBodyHtml = True
                ' Define a prioridade da mensagem como normal
                mail.Priority = MailPriority.Normal

                SmtpServer.Send(mail)

                sMsgRet = "E-mail enviado com sucesso."
            Else
                ctlUtil.EscreverLogErro("ctlEmail - enviaMensagemEmail - Usuário: " & HttpContext.Current.Session("NomeUsuario").ToString & " .Erro envio e-mail: Session de SMTP em branco.")
                sMsgRet = "Erro ao enviar e-mail. Dados de SMTP não preenchidos. Favor entrar em contato com o administrador do sistema."
            End If
        Catch ex As Exception
            ctlUtil.EscreverLogErro("ctlEmail - enviaMensagemEmail - Usuário: " & HttpContext.Current.Session("NomeUsuario").ToString & " .Erro envio e-mail: " & ex.Message)
            sMsgRet = "Erro ao enviar e-mail. " & ex.Message
        End Try

        Return sMsgRet

    End Function


    Public Shared Function enviaMensagemEmailROT(ByVal sAtendimento As String) As String

        Dim sRet As String = ""
        Dim recepient As String = ctlUtil.GetParam("MV_OOROT")
        Dim aSMTP As ArrayList = DirectCast(HttpContext.Current.Session("DadosSMTP"), ArrayList)

        Try
            If Not recepient.Trim = "" Then
                If aSMTP.Count > 1 Then

                    Dim cBody As String
                    Dim SmtpServer As New SmtpClient(CStr(aSMTP(0)), 25)
                    Dim mail As New MailMessage()
                    Dim aEmail As Array = {""}
                    SmtpServer.Credentials = New Net.NetworkCredential(CStr(aSMTP(1)), CStr(aSMTP(2)))
                    mail = New MailMessage()
                    mail.From = New MailAddress(CStr(aSMTP(1)))

                    If recepient.Contains(";") Then
                        aEmail = recepient.Split(CChar(";"))
                    ElseIf recepient.Contains(",") Then
                        aEmail = recepient.Split(CChar(","))
                    Else
                        aEmail.SetValue(recepient, 0)
                    End If

                    For Each email As String In aEmail
                        If Not String.IsNullOrEmpty(email) Then
                            mail.To.Add(email.ToLower)
                        End If
                    Next

                    cBody = "<html><head><link rel=""stylesheet"" type=""text/css"" href=""http://www.intermed.com.br/osonline/App_Themes/padrao/Estilos.css""></head>"
                    cBody += "<body><form>"
                    cBody += "<center><table class=""formulario"" style=""width:680px;"" cellspacing=""10px"">"
                    cBody += "<tr><td><img style=""border-width:0px;"" src=""http://www.intermed.com.br/osonline/App_Themes/padrao/TopoEmail.gif"" title=""Intermed""></td></tr>"
                    cBody += "<tr><td class=""label"" align=""center"">AGUARDANDO ANÁLISE INTERMED</td></tr>"
                    cBody += "<tr><td align=""center""><strong>Licenciado:</strong> " & HttpContext.Current.Session("NomeUsuario").ToString & "</td></tr>"
                    cBody += "<tr><td align=""center""><strong>Atendimento de OS nº:</strong> " & sAtendimento & "</td></tr>"
                    cBody += "<tr><td><img style=""border-width:0px;"" src=""http://www.intermed.com.br/osonline/App_Themes/padrao/RodapeEmail.gif"" title=""Intermed""></div>"
                    cBody += "</td></tr></table></center>"
                    cBody += "</form></body></html>"

                    mail.Subject = "Solicitação de análise de Ordem de Serviço"
                    mail.Body = cBody
                    ' Define o formato do email como HTML
                    mail.IsBodyHtml = True
                    ' Define a prioridade da mensagem como normal
                    mail.Priority = MailPriority.Normal

                    SmtpServer.Send(mail)

                    sRet = " Enviado para analise da Intermed."
                End If
            Else
                sRet = " Erro ao enviar e-mail para análise da Intermed. Parâmetro MV_OOROT em branco."
                ctlUtil.EscreverLogErro("Erro ao enviar e-mail para análise da Intermed. Parâmetro MV_OOROT em branco.")
            End If
        Catch ex As Exception
            sRet = " Erro ao enviar e-mail para análise da Intermed."
            ctlUtil.EscreverLogErro("Envio de e-mail para análise Intermed: " & ex.Message())
        End Try

        Return sRet

    End Function

End Class
