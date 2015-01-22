Imports System.Data.SqlClient

Partial Public Class ConsultaBaseInstalada
    Inherits BaseWebUI

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                'btnIncluir.OnClientClick = "javascript:window.open('DetalheBaseInstalada.aspx','_new','directories=no,location=yes,menubar=yes,status=yes,toolbar=yes,scrollbars=yes,resizable=yes,titlebar = yes, fullscreen = no'); return false;"
                CarregarTiposPesquisa()
                FormatarParametroPesquisa()
                TituloPagina("Base Instalada")
            End If
        Catch ex As Exception
            Dim oRet As New ctlRetornoGenerico
            ctlUtil.EscreverLogErro("ConsultaBaseInstalada - Page_load: " & ex.Message())
            oRet.Mensagem = ctlUtil.sMsgErroPadrao

            oRet.Sucesso = False
            oMensagem.SetMessage(oRet)

        End Try
    End Sub

    Private Sub CarregarTiposPesquisa()
        drpTipoPequisa.Items.Clear()
        Dim aValor As Array = System.Enum.GetValues(GetType(ctlBaseInstalada.TipoPesquisa))
        For Each oItem In aValor
            drpTipoPequisa.Items.Add(New ListItem(ctlAplicacao.GetEnumDescription(CType(oItem, [Enum])), DirectCast(oItem, Integer).ToString))
        Next
    End Sub

    Private Sub Pesquisar()

        Dim oRet As New ctlRetornoGenerico
        Dim dt As DataTable = Nothing
        Dim cn As New ctlBaseInstalada

        Try

            Dim oTipoSelecionado As ctlBaseInstalada.TipoPesquisa
            oTipoSelecionado = DirectCast(Integer.Parse(drpTipoPequisa.SelectedValue), ctlBaseInstalada.TipoPesquisa)
            Dim oParametros As Object = Nothing

            Select Case oTipoSelecionado
                Case ctlBaseInstalada.TipoPesquisa.Cliente
                    oParametros = txtCliente.GetArray
                Case ctlBaseInstalada.TipoPesquisa.Equipamento
                    oParametros = txtParametroPesquisa.Text
            End Select

            oRet = Validar(oTipoSelecionado, oParametros)

            If oRet.Sucesso Then
                'dt = cn.Pesquisar(oTipoSelecionado, oParametros, False)
                dt = cn.PesquisarBasesInstaladas(oTipoSelecionado, oParametros, False)
                oMensagem.ClearMessage()
                If Not dt.Rows.Count > 0 Then
                    oMensagem.SetMessage("A pesquisa não retornou resultados")
                    pnlResultado.Visible = False
                Else
                    pnlResultado.Visible = True
                    grdItens.DataSource = dt
                    grdItens.DataBind()
                    ViewState("BaseInstalada") = dt
                End If
            Else
                oMensagem.SetMessage(oRet)
            End If

        Catch ex As Exception
            ctlUtil.EscreverLogErro("ConsultaBaseInstalada - Pesquisar: " & ex.Message())
            oRet.Mensagem = ctlUtil.sMsgErroPadrao

            oRet.Sucesso = False
            oMensagem.SetMessage(oRet)
        End Try

    End Sub

    Public Function Validar(ByVal oTipoSelecionado As ctlBaseInstalada.TipoPesquisa, ByVal oParametros As Object) As ctlRetornoGenerico

        Dim oRet As New ctlRetornoGenerico
        Dim bSucesso As Boolean = False
        Dim sMensagem As String = ""

        If Not IsArray(oParametros) Then
            If oParametros.ToString.Trim.Length = 0 Then
                sMensagem = "Forneça um parâmetro de pesquisa"
            Else
                bSucesso = True
            End If
        ElseIf DirectCast(oParametros, String())(2).Length = 0 And (DirectCast(oParametros, String())(0).Length = 0 Or DirectCast(oParametros, String())(1).Length = 0) Then
            sMensagem = "Forneça um parâmetro de pesquisa"
        Else
            bSucesso = True
        End If

        With oRet
            .Chave = ""
            .Codigo = 0
            .Mensagem = sMensagem
            .Sucesso = bSucesso
        End With

        Return oRet

    End Function

    Private Sub FormatarParametroPesquisa()
        Dim oTipoSelecionado As New ctlBaseInstalada.TipoPesquisa
        Limpar()
        oTipoSelecionado = DirectCast(Integer.Parse(drpTipoPequisa.SelectedValue), ctlBaseInstalada.TipoPesquisa)
        txtParametroPesquisa.Visible = False
        txtCliente.Visible = False
        txtEmissao.Visible = False
        Select Case oTipoSelecionado
            Case ctlBaseInstalada.TipoPesquisa.Cliente
                txtCliente.Visible = True
                txtCliente.Enabled = True
            Case ctlBaseInstalada.TipoPesquisa.Equipamento
                txtParametroPesquisa.Visible = True
        End Select
    End Sub

    Protected Sub drpTipoPequisa_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles drpTipoPequisa.SelectedIndexChanged
        FormatarParametroPesquisa()
    End Sub

    Public Sub Limpar()
        txtParametroPesquisa.Text = ""
        txtEmissao.Clear()
        txtCliente.Clear()
        'txtVendedor.Clear()
        oMensagem.ClearMessage()
        grdItens.DataSource = New DataTable
        grdItens.DataBind()
        pnlResultado.Visible = False
    End Sub

    Private Sub txtCliente_SelecionarOnClick(ByVal oSelecao As Object) Handles txtCliente.SelecionarOnClick
        Pesquisar()
    End Sub

    Private Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        Pesquisar()
    End Sub

    Private Sub TituloPagina(ByVal sTexto As String)
        DirectCast(Me.Master.Controls(0).Controls(3).Controls(7).FindControl("oBarraUsuario"), BarraUsuario).PaginaAtual = sTexto
    End Sub

    Private Sub grdItens_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdItens.PageIndexChanging
        Dim gv As GridView = DirectCast(sender, GridView)
        Dim dt As DataTable = DirectCast(ViewState("BaseInstalada"), DataTable)
        gv.DataSource = ctlAplicacao.Ordenar(dt, gvSortExpression, gvSortDirection)
        gv.PageIndex = e.NewPageIndex
        gv.DataBind()
    End Sub

    Private Sub grdItens_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdItens.Sorting
        Dim gv As GridView = DirectCast(sender, GridView)
        Dim dt As DataTable = TryCast(ViewState("BaseInstalada"), DataTable)
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

End Class