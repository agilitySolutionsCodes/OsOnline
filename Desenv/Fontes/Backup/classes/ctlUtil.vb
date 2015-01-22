Imports System.Web
Imports System
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Configuration
Imports System.Windows.Forms

Public Class ctlUtil

    Public Shared sMsgErroPadrao As String = "Não foi possível comunicação com o servidor. Por favor, tente mais tarde."

    Public Shared Function GetConnection() As SqlConnection
        Dim reader As New System.Configuration.AppSettingsReader
        Dim sConnection As String = ""
        Dim sNomeConnection As String = ""
        Dim sAmbiente As String = reader.GetValue("Ambiente", GetType(String)).ToString()
        sConnection = ConfigurationManager.ConnectionStrings(sAmbiente).ConnectionString
        Dim sqlConn As SqlConnection = New SqlConnection(sConnection)
        Return sqlConn
    End Function

    Public Shared Function GetFilial() As String
        Dim reader As New System.Configuration.AppSettingsReader
        Dim sAmbiente As String = reader.GetValue("Ambiente", GetType(String)).ToString()
        Return reader.GetValue("Filial" + sAmbiente, GetType(String)).ToString()
    End Function

    Public Shared Function GetLoja() As String
        Dim reader As New System.Configuration.AppSettingsReader
        Return reader.GetValue("Loja", GetType(String)).ToString()
    End Function

    Public Shared Function GetAmbiente() As String
        Dim reader As New System.Configuration.AppSettingsReader
        Return reader.GetValue("Ambiente", GetType(String)).ToString()
    End Function

    Public Shared Function GetCaminhoWS(ByVal sAmbiente As String) As String
        Dim reader As New System.Configuration.AppSettingsReader
        Return reader.GetValue("CaminhoWS" + sAmbiente, GetType(String)).ToString()
    End Function

    Public Shared Function GetCaminho() As String
        Dim caminhoArquivo As String = Application.StartupPath
        If caminhoArquivo.IndexOf("\bin\Debug") <> -1 Then
            caminhoArquivo = caminhoArquivo.Replace("\bin\Debug", "")
        ElseIf caminhoArquivo.IndexOf("\bin\Release") <> -1 Then
            caminhoArquivo = caminhoArquivo.Replace("\bin\Release", "")
        End If
        Return caminhoArquivo
    End Function

    Public Shared Sub EscreverLogErro(ByVal strComments As String)
        Dim erroLog As New Erro
        erroLog.EscreverLog(strComments)
    End Sub


    Public Shared Function GetParam(ByVal sNomeParam As String) As String
        Dim oParam As New wsmicrosiga.utilagility.WSUTILAGILITY
        Dim sRetorno As String
        Dim oTokenSiga As New wsmicrosiga.utilagility.TKNSTRUCT

        oTokenSiga.CONTEUDO = HttpContext.Current.Session("NomeUsuario").ToString
        oTokenSiga.SENHA = DirectCast(HttpContext.Current.Session("Usuario2"), ctlUsuario).Hash

        sRetorno = oParam.WEBGETMV(oTokenSiga, sNomeParam)
        Return sRetorno
    End Function

End Class
