'TODO: PERMITIR A SELECAO DAS COLUNAS EXIBIDAS
'TODO: PERMITIR NOMEAR AS COLUNAS EXIBIDAS

Imports System.Reflection

Partial Public Class ConsultaPadrao
    Inherits System.Web.UI.UserControl

    Private sProcedure As String = ""
    Private Const sListarTiposPesquisa As String = "ListarTiposPesquisa"
    Private Const sPesquisar As String = "Pesquisar"
    Private sDataKeys As String = "CODIGO"
    Private oItemSelecionado As New Object
    Private drSelecionado As DataRow
    Public Event Fechar()
    Public Event SelecionarOnClick(ByVal oSelecao As Object)
    Public Event PaginarOnClick()
    Public Event CancelarOnClick()
    Public Event PesquisarOnClick(ByVal oTipoPesquisa As Object, ByVal oParametros As Object, ByVal oResultado As Object)
    Public Event Ordenar()

    Public ReadOnly Property Item() As Object
        Get
            Return drSelecionado
        End Get
    End Property

    Public Sub PreencherTiposPesquisa()
        Dim oObj As Object = Activator.CreateInstance(Assembly.GetExecutingAssembly().GetType(sProcedure, True, True))
        drpTipoPesquisa.Items.Clear()
        Dim aValor As Array = DirectCast(CallByName(oObj, sListarTiposPesquisa, CallType.Method), Array)
        Dim oItem As Object
        For Each oItem In aValor
            'drpTipoPesquisa.Items.Add(New ListItem(oItem.ToString, DirectCast(oItem, Integer).ToString))
            drpTipoPesquisa.Items.Add(New ListItem(ctlAplicacao.GetEnumDescription(CType(oItem, [Enum])), DirectCast(oItem, Integer).ToString))
        Next
    End Sub

    Public Property TipoPesquisaCssClass() As String
        Get
            Return drpTipoPesquisa.CssClass
        End Get
        Set(ByVal value As String)
            drpTipoPesquisa.CssClass = value
        End Set
    End Property

    Public Property ParametroCssClass() As String
        Get
            Return txtParametroPesquisa.CssClass
        End Get
        Set(ByVal value As String)
            txtParametroPesquisa.CssClass = value
        End Set
    End Property

    Public Property Procedure() As String
        Get
            Return sProcedure
        End Get
        Set(ByVal value As String)
            sProcedure = value
        End Set
    End Property

    Public Property ResultadoCabecalhoCssClass() As String
        Get
            Return grdPesquisar.HeaderStyle.CssClass
        End Get
        Set(ByVal value As String)
            grdPesquisar.HeaderStyle.CssClass = value
        End Set
    End Property

    Public Sub Pesquisar(ByVal sTipo As String, ByVal oConteudo As Object)
        Try
            Dim oObj As Object = Activator.CreateInstance(Assembly.GetExecutingAssembly().GetType(sProcedure, True, True))
            Dim dt As DataTable = Nothing
            Dim oParm As New List(Of Object)
            oParm.Add(sTipo)
            oParm.Add(oConteudo)
            grdPesquisar.DataSource = Nothing
            If sTipo.ToString.Trim = "1" AndAlso String.IsNullOrEmpty(oConteudo.ToString.Trim) Then
                oMensagem.SetMessage("Informe no mínimo 3 caracteres", "W")
            ElseIf txtParametroPesquisa.Text.Trim.Length < 3 Then
                oMensagem.SetMessage("Informe no mínimo 3 caracteres", "W")
            Else
                dt = DirectCast(CallByName(oObj, sPesquisar, CallType.Method, oParm.ToArray()), DataTable)
                If dt.Rows.Count = 0 Then
                    oMensagem.SetMessage("A pesquisa não retornou nenhum resultado", "W")
                Else
                    If oObj.ToString = "OrdemServico.ctlCliente" Then
                        Dim regiaoUsuario As String = TryCast(Session("Regiao"), String)
                        If Not String.IsNullOrEmpty(regiaoUsuario) Then
                            'If validarRegiaoCliente(dt, regiaoUsuario) Then
                            oMensagem.ClearMessage()
                            ViewState.Add("Resultado", dt)
                            grdPesquisar.DataSource = dt
                            'Else
                            '    oMensagem.SetMessage("O usuário atual não atende a mesma região que o cliente!", "W")
                            'End If
                        Else
                            oMensagem.SetMessage("O usuário atual não possui nenhuma região cadastrada!", "W")
                        End If
                    Else
                        oMensagem.ClearMessage()
                        ViewState.Add("Resultado", dt)
                        grdPesquisar.DataSource = dt
                    End If
                End If
            End If
            grdPesquisar.DataBind()
            RaiseEvent PesquisarOnClick(sTipo, oConteudo, dt)
        Catch ex As Exception
            oMensagem.SetMessage(ex.Message.ToString, "W")
        End Try
    End Sub

    Public Sub Pesquisar()
        Pesquisar(drpTipoPesquisa.SelectedValue, txtParametroPesquisa.Text)
    End Sub

    Private Sub grdPesquisar_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdPesquisar.PageIndexChanging
        Dim dt As DataTable = DirectCast(ViewState("Resultado"), DataTable)
        grdPesquisar.PageIndex = e.NewPageIndex
        grdPesquisar.DataSource = dt
        grdPesquisar.DataBind()
        RaiseEvent PaginarOnClick()
    End Sub

    Private Sub grdPesquisar_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdPesquisar.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim row As GridViewRow = e.Row
            Dim selectCell As TableCell = row.Cells(0)
            If (selectCell.Controls.Count > 0) Then
                Dim selectControl As LinkButton = DirectCast(selectCell.Controls(0), LinkButton)
                If Not IsNothing(selectControl) Then
                    selectControl.Text = "Selecionar"
                End If
            End If
        End If
    End Sub

    Protected Sub grdPesquisar_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles grdPesquisar.SelectedIndexChanged
        Dim dt As New DataTable
        Dim gh As GridViewRow = grdPesquisar.HeaderRow
        For Each cel As TableCell In gh.Cells
            If cel.HasControls Then
                If TypeOf cel.Controls(0) Is LinkButton Then
                    dt.Columns.Add(New DataColumn(DirectCast(cel.Controls(0), LinkButton).Text, GetType(Object)))
                Else
                    dt.Columns.Add(New DataColumn("", GetType(Object)))
                End If
                'Else
                'dt.Columns.Add(New DataColumn("", GetType(Object)))
            End If
        Next
        drSelecionado = dt.NewRow
        Dim gr As GridViewRow
        Dim i As Integer = -1
        gr = DirectCast(sender, GridView).SelectedRow
        For Each cel As TableCell In gr.Cells
            If i >= 0 Then
                drSelecionado(i) = cel.Text
            End If
            i += 1
        Next
        RaiseEvent SelecionarOnClick(drSelecionado)
        Limpar()
    End Sub

    Private Sub grdPesquisar_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdPesquisar.Sorting
        Dim dv As New DataView(DirectCast(ViewState("Resultado"), DataTable))
        Dim gv As GridView = DirectCast(sender, GridView)
        Dim sCampoSort As String = e.SortExpression
        If gv.SortDirection = SortDirection.Descending Then
            sCampoSort += " DESC"
        End If
        dv.Sort = sCampoSort
        ViewState("Resultado") = dv.ToTable
        grdPesquisar.DataSource = DirectCast(ViewState("Resultado"), DataTable)
        grdPesquisar.DataBind()
        RaiseEvent Ordenar()
    End Sub

    'Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    '   If Not IsPostBack Then
    '       oMensagem.ClearMessage()
    '      PreencherTiposPesquisa()
    '  End If
    'End Sub

    Private Sub Limpar()
        Dim dt As DataTable = DirectCast(ViewState("Resultado"), DataTable)
        oMensagem.ClearMessage()
        If Not dt Is Nothing Then
            dt.Rows.Clear()
            grdPesquisar.DataSource = dt
            grdPesquisar.DataBind()
            ViewState.Remove("Resultado")
        End If
        txtParametroPesquisa.Text = ""
    End Sub

    Public Property AlternatingRowStyle() As String
        Get
            Return grdPesquisar.AlternatingRowStyle.CssClass
        End Get
        Set(ByVal value As String)
            grdPesquisar.AlternatingRowStyle.CssClass = value
        End Set
    End Property

    Public Property RowStyle() As String
        Get
            Return grdPesquisar.RowStyle.CssClass
        End Get
        Set(ByVal value As String)
            grdPesquisar.RowStyle.CssClass = value
        End Set
    End Property

    Public Property HeaderStyle() As String
        Get
            Return grdPesquisar.HeaderStyle.CssClass
        End Get
        Set(ByVal value As String)
            grdPesquisar.HeaderStyle.CssClass = value
        End Set
    End Property

    Public Property GridCssClass() As String
        Get
            Return grdPesquisar.CssClass
        End Get
        Set(ByVal value As String)
            grdPesquisar.CssClass = value
        End Set
    End Property

    Public Property PesquisarCssClass() As String
        Get
            Return wucAutoHideButton.CssButton 'btnPesquisar.CssButton
        End Get
        Set(ByVal value As String)
            wucAutoHideButton.CssButton = value 'btnPesquisar.CssButton = value
        End Set
    End Property

    Public Sub FecharJanela()
        Limpar()
        RaiseEvent Fechar()
        RaiseEvent CancelarOnClick()
    End Sub

    Public Property ParametroPesquisa() As String
        Get
            Return txtParametroPesquisa.Text
        End Get
        Set(ByVal value As String)
            txtParametroPesquisa.Text = value
        End Set
    End Property

    Public Property ExibirParametros() As Boolean
        Get
            Return pnlParametros.Visible
        End Get
        Set(ByVal value As Boolean)
            pnlParametros.Visible = value
        End Set
    End Property

    Public Property ExibirPainelParametros() As Boolean
        Get
            Return pnlParametros.Visible
        End Get
        Set(ByVal value As Boolean)
            pnlParametros.Visible = value
        End Set
    End Property

    Private Sub drpTipoPesquisa_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpTipoPesquisa.TextChanged
        AjustarTipoPesquisa()
    End Sub

    Private Sub AjustarTipoPesquisa()
        If drpTipoPesquisa.SelectedItem.Text IsNot Nothing Then
            If drpTipoPesquisa.SelectedItem.Text = "Cliente" Then
                txtParametroPesquisa.Enabled = False
                If Session("CodigoCliente") IsNot Nothing Then
                    txtParametroPesquisa.Text = Session("CodigoCliente").ToString
                End If
            Else
                txtParametroPesquisa.Enabled = True
                txtParametroPesquisa.Text = ""
            End If
        End If
    End Sub

    Private Function validarRegiaoCliente(ByVal dt As DataTable, ByVal regiaoUsuario As String) As Boolean
        Dim existeRegiao As Boolean = False
        Dim aRegiaoUsuario() As String = Split(regiaoUsuario, ",")
        If Not String.IsNullOrEmpty(regiaoUsuario) Then
            Dim regiaoCliente As String = dt(0)("Regiao").ToString
            For Each auxRegiaoUsuario In aRegiaoUsuario.ToArray
                If regiaoCliente.Contains(auxRegiaoUsuario.Replace("'", "")) Then
                    existeRegiao = True
                    Exit For
                End If
            Next
        End If
        Return existeRegiao
    End Function

    Private Sub wucAutoHideButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles wucAutoHideButton.Click
        Pesquisar()
    End Sub

End Class