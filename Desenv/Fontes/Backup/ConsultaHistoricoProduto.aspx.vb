Imports System.Data.SqlClient

Public Class ConsultaHistoricoProduto
    Inherits BaseWebUI

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                CarregarTiposPesquisa()
                TituloPagina("Histórico do Produto")
                tcHistoricoProduto.Visible = False
            End If
        Catch ex As Exception
            Dim oRet As New ctlRetornoGenerico
            ctlUtil.EscreverLogErro("ConsultaHistoricoProduto - Page_load: " & ex.Message())
            oRet.Mensagem = ctlUtil.sMsgErroPadrao

            oRet.Sucesso = False
            oMensagem.SetMessage(oRet)
        End Try
    End Sub

    Private Sub CarregarTiposPesquisa()
        drpTipoPequisa.Items.Clear()
        Dim aValor As Array = System.Enum.GetValues(GetType(ctlHistoricoProduto.TipoPesquisa))
        For Each oItem In aValor
            drpTipoPequisa.Items.Add(New ListItem(ctlAplicacao.GetEnumDescription(CType(oItem, [Enum])), DirectCast(oItem, Integer).ToString))
        Next
    End Sub

    Private Sub Pesquisar()

        Dim oRet As New ctlRetornoGenerico
        Dim reader As SqlDataReader = Nothing
        Dim oHistorico As New ctlHistoricoProduto

        Try

            Dim oTipoSelecionado As ctlHistoricoProduto.TipoPesquisa
            oTipoSelecionado = DirectCast(Integer.Parse(drpTipoPequisa.SelectedValue), ctlHistoricoProduto.TipoPesquisa)
            Dim oParametros As Object = Nothing
            Select Case oTipoSelecionado
                Case ctlHistoricoProduto.TipoPesquisa.Equipamento
                    oParametros = txtParametroPesquisa.Text
                    hdnNumSerie.Value = txtParametroPesquisa.Text
            End Select
            oRet = Validar(oTipoSelecionado, oParametros)

            If oRet.Sucesso Then

                oMensagem.ClearMessage()

                reader = oHistorico.Listar(oTipoSelecionado, oParametros, "1")
                If Not reader.HasRows Then
                    tcHistoricoProduto.Visible = False
                Else
                    tcHistoricoProduto.Visible = True
                    Session.Add("NumeroSerie", oParametros.ToString)
                End If
                Dim dt As New DataTable
                dt.Load(reader)
                grdChamado.DataSource = dt
                grdChamado.DataBind()
                reader.Close()
                ViewState("ChamadoTecnico") = dt

                reader = oHistorico.ListarOS(oTipoSelecionado, oParametros)
                If Not reader.HasRows Then
                    If tcHistoricoProduto.Visible = False Then
                        oMensagem.SetMessage("A pesquisa não retornou resultados")
                    End If
                Else
                    tcHistoricoProduto.Visible = True
                    Session.Add("NumeroSerie", oParametros.ToString)
                End If
                dt = New DataTable
                dt.Load(reader)
                grdOs.DataSource = dt
                grdOs.DataBind()
                reader.Close()
                ViewState("OrdemServico") = dt

                reader = oHistorico.ListarAtendimento(oTipoSelecionado, oParametros)
                If Not reader.HasRows Then
                    If tcHistoricoProduto.Visible = False Then
                        oMensagem.SetMessage("A pesquisa não retornou resultados")
                    End If
                Else
                    tcHistoricoProduto.Visible = True
                    Session.Add("NumeroSerie", oParametros.ToString)
                End If
                dt = New DataTable
                dt.Load(reader)
                grdAtendimento.DataSource = dt
                grdAtendimento.DataBind()
                reader.Close()
                ViewState("Atendimento") = dt

            Else
                oMensagem.SetMessage(oRet)
            End If

        Catch ex As Exception
            ctlUtil.EscreverLogErro("ConsultaHistoricoProduto - Pesquisar: " & ex.Message())
            oRet.Mensagem = ctlUtil.sMsgErroPadrao

            oRet.Sucesso = False
            oMensagem.SetMessage(oRet)
        Finally
            If reader IsNot Nothing Then
                reader.Close()
            End If
        End Try

    End Sub

    Public Function Validar(ByVal oTipoSelecionado As ctlHistoricoProduto.TipoPesquisa, ByVal oParametros As Object) As ctlRetornoGenerico

        Dim oRet As New ctlRetornoGenerico
        Dim bSucesso As Boolean = False
        Dim sMensagem As String = ""

        If Not IsArray(oParametros) Then
            If oParametros.ToString.Trim.Length = 0 Then
                sMensagem = "Forneça um parâmetro de pesquisa"
            Else
                bSucesso = True
            End If
        Else
            If DirectCast(oParametros, String())(2).Length = 0 And (DirectCast(oParametros, String())(0).Length = 0 Or DirectCast(oParametros, String())(1).Length = 0) Then
                sMensagem = "Forneça um parâmetro de pesquisa"
            Else
                bSucesso = True
            End If
        End If

        With oRet
            .Chave = ""
            .Codigo = 0
            .Mensagem = sMensagem
            .Sucesso = bSucesso
        End With

        Return oRet

    End Function

    Public Sub Limpar()
        txtParametroPesquisa.Text = ""
        oMensagem.ClearMessage()
        grdOs.DataSource = New DataTable
        grdOs.DataBind()
        tcHistoricoProduto.Visible = False
    End Sub

    Private Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        Pesquisar()
    End Sub

    Private Sub TituloPagina(ByVal sTexto As String)
        DirectCast(Me.Master.Controls(0).Controls(3).Controls(7).FindControl("oBarraUsuario"), BarraUsuario).PaginaAtual = sTexto
    End Sub

    Private Sub grdChamado_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdChamado.PageIndexChanging
        Dim gv As GridView = DirectCast(sender, GridView)
        Dim dt As DataTable = DirectCast(ViewState("ChamadoTecnico"), DataTable)
        gv.DataSource = ctlAplicacao.Ordenar(dt, gvSortExpression, gvSortDirection)
        gv.PageIndex = e.NewPageIndex
        gv.DataBind()
    End Sub

    Private Sub grdChamado_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdChamado.Sorting
        Dim gv As GridView = DirectCast(sender, GridView)
        Dim dt As DataTable = TryCast(ViewState("ChamadoTecnico"), DataTable)
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

    Private Sub grdOs_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdOs.PageIndexChanging
        Dim gv As GridView = DirectCast(sender, GridView)
        Dim dt As DataTable = DirectCast(ViewState("OrdemServico"), DataTable)
        gv.DataSource = ctlAplicacao.Ordenar(dt, gvSortExpression, gvSortDirection)
        gv.PageIndex = e.NewPageIndex
        gv.DataBind()
    End Sub

    Private Sub grdOs_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdOs.Sorting
        Dim gv As GridView = DirectCast(sender, GridView)
        Dim dt As DataTable = TryCast(ViewState("OrdemServico"), DataTable)
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

    Private Sub grdAtendimento_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdAtendimento.PageIndexChanging
        Dim gv As GridView = DirectCast(sender, GridView)
        Dim dt As DataTable = DirectCast(ViewState("Atendimento"), DataTable)
        gv.DataSource = ctlAplicacao.Ordenar(dt, gvSortExpression, gvSortDirection)
        gv.PageIndex = e.NewPageIndex
        gv.DataBind()
    End Sub

    Private Sub grdAtendimento_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdAtendimento.Sorting
        Dim gv As GridView = DirectCast(sender, GridView)
        Dim dt As DataTable = TryCast(ViewState("Atendimento"), DataTable)
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


    Private Sub btnImprimir_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnImprimir.Click
        Response.Redirect("ImpressaoHistoricoAtendimento.aspx?NumSerie=" & hdnNumSerie.Value)
    End Sub

End Class