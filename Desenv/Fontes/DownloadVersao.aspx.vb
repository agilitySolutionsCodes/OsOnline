Imports System.Net
Imports System.Windows.Forms
Imports System.IO

Public Class DownloadVersao
    Inherits BaseWebUI
    'Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            TituloPagina("Documentos")
            CarregarArquivos()
        End If
    End Sub

    Private Function DownloadArquivo(ByVal sArquivo As String, ByVal sDocumento As String, ByVal sRevisao As String, sender As Object) As ctlRetornoGenerico

        Dim oRet As New ctlRetornoGenerico

        Try

            Dim sNomeArquivo = sArquivo.Substring(sArquivo.LastIndexOf("\") + 1, Len(sArquivo) - sArquivo.LastIndexOf("\") - 1)
            Dim sCaminho As String = "http://www.intermed.com.br/osonline/Documentos_downloads/" & sNomeArquivo

            'Dim request = HttpWebRequest.Create(sCaminho)
            'request.Method = "HEAD"
            'request.Credentials = System.Net.CredentialCache.DefaultCredentials
            'Dim response = DirectCast(request.GetResponse(), HttpWebResponse)
            'If response.StatusCode = HttpStatusCode.OK Then

            Dim oVersao As New ctlVersao
            oVersao.InserirDownloadLicenciado(sDocumento, sRevisao)
            EnviaEmail(sDocumento, sRevisao)

            DirectCast(sender, ImageButton).ImageUrl = "imagens/arq_baixado_menor.png"
            DirectCast(sender, ImageButton).Enabled = False
            'DirectCast(sender, ImageButton).Attributes.Add("target", "_blank")

            'Process.Start(sCaminho)
            Response.Write("<script>window.open('" & sCaminho & "','arquivo');</script>")

            'oRet.Mensagem = "Download efetuado com sucesso."
            'Else
            'oRet.Sucesso = False
            'oRet.Mensagem = "Este arquivo ainda não foi atualizado. Tentar novamente após 5 minutos. "
            'End If


            'Response.Clear()
            'Response.ContentType = ReturnExtension(Arquivo.Extension)
            'Response.AddHeader("content-disposition", "attachment; filename=" & Arquivo.Name)
            'Response.AddHeader("Content-Length", Arquivo.Length.ToString())
            'Response.TransmitFile(Arquivo.FullName)
            'Response.Flush()
            'Response.End()
            'Else
            'oRet.Sucesso = False
            'oRet.Mensagem = "Este arquivo ainda não foi atualizado. Tentar novamente após 5 minutos. "
            'End If

        Catch ex As Exception
            oRet.Sucesso = False
            oRet.Mensagem = "Erro ao efetuar download de arquivo. "
            ctlUtil.EscreverLogErro("DownloadVersao - DownloadArquivo: " & ex.Message)
        End Try

        Return oRet

    End Function

    Private Sub CarregarArquivos()

        Try
            Dim oVersao As New ctlVersao
            Dim dt As New DataTable

            dt = oVersao.Pesquisar

            grdItens.DataSource = dt
            ViewState("Docs") = dt
            grdItens.DataBind()

        Catch ex As Exception
            Dim oRet As New ctlRetornoGenerico
            oRet.Sucesso = False
            ctlUtil.EscreverLogErro("DownloadVersao - CarregarArquivos: " & ex.Message())
            oRet.Mensagem = "Erro ao carregar arquivos."
            oMensagem.SetMessage(oRet)
        End Try

    End Sub

    Private Sub grdItens_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdItens.PageIndexChanging
        Dim gv As GridView = DirectCast(sender, GridView)
        Dim dt As DataTable = DirectCast(ViewState("Docs"), DataTable)
        gv.DataSource = ctlAplicacao.Ordenar(dt, gvSortExpression, gvSortDirection)
        gv.PageIndex = e.NewPageIndex
        gv.DataBind()
    End Sub

    Private Sub grdItens_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdItens.Sorting
        Dim gv As GridView = DirectCast(sender, GridView)
        Dim dt As DataTable = TryCast(ViewState("Docs"), DataTable)
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

    Private Sub TituloPagina(ByVal sTexto As String)
        DirectCast(Me.Master.Controls(0).Controls(3).Controls(7).FindControl("oBarraUsuario"), BarraUsuario).PaginaAtual = sTexto
    End Sub

    Public Sub DownloadArquivo(sender As Object, e As ImageClickEventArgs)
        Dim sDados As String
        Dim aDados As String()
        Dim oRet As New ctlRetornoGenerico


        sDados = DirectCast(sender, ImageButton).CommandArgument
        aDados = sDados.Split(CChar(";"))

        If aDados.Length > 2 Then

            oRet = DownloadArquivo(aDados(0).Trim, aDados(1).Trim, aDados(2).Trim, sender)
        Else
            oRet.Sucesso = False
            oRet.Mensagem = "Erro ao consultar registro. Campo caminho do arquivo, documento ou revisão estão em branco."
        End If

        If oRet.Sucesso = False Then
            oMensagem.SetMessage(oRet)
            updMensagem.Update()
        End If
    End Sub

    Private Sub EnviaEmail(sDocumento As String, sRevisao As String)

        Dim cBody As String
        Dim sParam As String = ctlUtil.GetParam("MV_OODDOC")
        Dim oRet As New ctlRetornoGenerico
        Dim sMsgRet As String

        If Not String.IsNullOrEmpty(sParam.Trim) Then
            'envia email para responsavel com dados
            cBody = "<html><head><link rel=""stylesheet"" type=""text/css"" href=""http://www.intermed.com.br/osonline/App_Themes/padrao/Estilos.css""></head>"
            cBody += "<body><form>"
            cBody += "<center><table class=""formulario"" style=""width:680px;"" >"
            cBody += "<tr><td><img style=""border-width:0px;"" src=""http://www.intermed.com.br/osonline/App_Themes/padrao/TopoEmail.gif"" title=""Intermed""></td></tr>"
            cBody += "<tr><td class=""label"" align=""center"">DOWNLOAD DE DOCUMENTO</td></tr>"

            cBody += "<tr><td align=""left""><hr/></tr>"
            cBody += "<tr><td align=""left""><strong>Documento:</strong> " & sDocumento & "</td></tr>"
            cBody += "<tr><td align=""left""><strong>Revisão:</strong> " & sRevisao & "</td></tr>"
            cBody += "<tr><td align=""left""><strong>Licenciado:</strong> " & HttpContext.Current.Session("NomeUsuario").ToString & "</td></tr>"
            cBody += "<tr><td align=""left""><strong>Data:</strong> " & Today().ToString("dd/MM/yyyy") & "</td></tr>"
            cBody += "<tr><td align=""left""><strong>Hora:</strong> " & TimeOfDay() & "</td></tr>"

            cBody += "<tr><td><img style=""border-width:0px;"" src=""http://www.intermed.com.br/osonline/App_Themes/padrao/RodapeEmail.gif"" title=""Intermed""></div>"
            cBody += "</td></tr></table></center>"
            cBody += "</form></body></html>"

            sMsgRet = ctlEmail.enviaMensagemEmail(sParam, "", "Download de Documento", cBody)
        Else
            oRet.Sucesso = False
            oRet.Mensagem = "Necessário preencher o parâmetro MV_OODDOC no Protheus. Favor entrar em contato com a Intermed."
            oMensagem.SetMessage(oRet)
        End If

    End Sub

    'Private Function ReturnExtension(ByVal fileExtension As String) As String

    '    Dim sRet As String = ""

    '    Select Case (fileExtension)
    '        Case ".htm"
    '        Case ".html"
    '        Case ".log"
    '            sRet = "text/HTML"
    '        Case ".txt"
    '            sRet = "text/plain"
    '        Case ".doc"
    '            sRet = "application/ms-word"
    '        Case ".tiff"
    '        Case ".tif"
    '            sRet = "image/tiff"
    '        Case ".asf"
    '            sRet = "video/x-ms-asf"
    '        Case ".avi"
    '            sRet = "video/avi"
    '        Case ".zip"
    '            sRet = "application/zip"
    '        Case ".xls"
    '        Case ".csv"
    '            sRet = "application/vnd.ms-excel"
    '        Case ".gif"
    '            sRet = "image/gif"
    '        Case ".jpg"
    '        Case "jpeg"
    '            sRet = "image/jpeg"
    '        Case ".bmp"
    '            sRet = "image/bmp"
    '        Case ".wav"
    '            sRet = "audio/wav"
    '        Case ".mp3"
    '            sRet = "audio/mpeg3"
    '        Case ".mpg"
    '        Case "mpeg"
    '            sRet = "video/mpeg"
    '        Case ".rtf"
    '            sRet = "application/rtf"
    '        Case ".asp"
    '            sRet = "text/asp"
    '        Case ".pdf"
    '            sRet = "application/pdf"
    '        Case ".fdf"
    '            sRet = "application/vnd.fdf"
    '        Case ".ppt"
    '            sRet = "application/mspowerpoint"
    '        Case ".dwg"
    '            sRet = "image/vnd.dwg"
    '        Case ".msg"
    '            sRet = "application/msoutlook"
    '        Case ".xml"
    '        Case ".sdxl"
    '            sRet = "application/xml"
    '        Case ".xdp"
    '            sRet = "application/vnd.adobe.xdp+xml"
    '        Case Else
    '            sRet = "application/octet-stream"
    '    End Select

    '    Return sRet

    'End Function

End Class