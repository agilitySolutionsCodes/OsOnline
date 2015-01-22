Imports System.ComponentModel
Imports System.Security.Principal
Imports System.Data.SqlClient

Public Class Login
    Inherits BaseWebUI

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    End Sub

    Private Sub objLogin_Authenticate(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.AuthenticateEventArgs) Handles objLogin.Authenticate
        Dim oRet As New ctlRetornoGenerico
        Dim oLogin As WebControls.Login = DirectCast(sender, WebControls.Login)

        Try
            Dim oRetWs As New wsmicrosiga.useragility.VALIDASTRUCT

            oRetWs = ctlSeguranca.Autenticar(oLogin.UserName.ToString(), oLogin.Password.ToString())
            If oRetWs.RETORNO.SUCESSO Then
                Dim oAuth As New ctlUsuario(oRetWs.USUARIO)
                Session("Usuario2") = oAuth
                Dim aGrupos() As String = oAuth.Perfis.ToArray
                Dim sGrupos As String = Join(aGrupos, "|")

                Session.Add("perfilAcesso", oRetWs.USUARIO.ACESSOMENU)

                Dim ticket As New FormsAuthenticationTicket(1, oAuth.UserCode, Now, Now.AddMinutes(15), True, sGrupos)
                Dim sRegiao = ""
                Dim aRegioes() As String = oAuth.sRegioes.Trim().Split(CChar(","))

                For Each regiao In aRegioes
                    sRegiao += "'" + Trim(regiao) + "',"
                Next
                sRegiao = sRegiao.Substring(0, Len(sRegiao) - 1)

                HttpContext.Current.Session.Add("Regiao", sRegiao)
                HttpContext.Current.Session.Add("Grupo", oAuth.sGrupoUsuario)
                HttpContext.Current.Session.Add("NomeUsuario", oAuth.UserName)
                'Session E-mail Usário
                HttpContext.Current.Session.Add("EmailUsuario", oAuth.EmailUsuario)

                'SESSAO CRIADA PARA USAR COMO DATA DE CORTE ONDE AS CLASSIFICACOES, OCORRENCIAS E ETAPAS INATIVAS NAO FICARAO MAIS DISPONIVEIS PARA NOVAS INCLUSOES
                HttpContext.Current.Session.Add("DataCorte", CDate("08/10/2012"))
                ConsultarDadosEmail()
                Response.Cookies(".ASPXAUTH").Value = System.Web.Security.FormsAuthentication.Encrypt(ticket)
                'FormsAuthentication.RedirectFromLoginPage(oAuth.UserName, False)
                FormsAuthentication.RedirectFromLoginPage(oAuth.UserCode, False)
            Else
                ctlUtil.EscreverLogErro("Login - Authenticate: Login: " & oLogin.UserName.ToString() & " - Senha: " & oLogin.Password.ToString() & " - " & oRetWs.RETORNO.MENSAGEM.Trim())
                oLogin.FailureText = oRetWs.RETORNO.MENSAGEM.Trim()
            End If
        Catch ex As Exception
            ctlUtil.EscreverLogErro("Login - Authenticate: " & ex.Message())
            oLogin.FailureText = ctlUtil.sMsgErroPadrao

        End Try
    End Sub

    Private Sub ConsultarDadosEmail()
        Try
            Dim aEmail As New ArrayList
            Dim sServidor As String = ctlUtil.GetParam("MV_RELSERV") 'Servidor de email
            Dim sUsuario As String = ctlUtil.GetParam("MV_RELACNT").ToLower 'Login do email 
            Dim sSenha As String = ctlUtil.GetParam("MV_RELPSW").ToLower 'Senha do email
            Dim sAuth As String = ctlUtil.GetParam("MV_RELAUTH") 'Utiliza autenticacao? 
            Dim sUsuarioAuth As String = ctlUtil.GetParam("MV_RELAUSR").ToLower 'Login de autenticacao 
            Dim sSenhaAuth As String = ctlUtil.GetParam("MV_RELAPSW").ToLower 'Senha de autenticacao
            Dim sTLS As String = ctlUtil.GetParam("MV_RELTLS") 'Utiliza TLS?


            aEmail.Add(sServidor.Replace(":25", ""))
            aEmail.Add(sUsuario)
            aEmail.Add(sSenha)
            aEmail.Add(sAuth)
            aEmail.Add(sUsuarioAuth)
            aEmail.Add(sSenhaAuth)

            HttpContext.Current.Session.Add("DadosSMTP", aEmail)

        Catch ex As Exception
            ctlUtil.EscreverLogErro("Erro ao consultar parametros de envio de e-mail: " & ex.Message())
        End Try

    End Sub

End Class


