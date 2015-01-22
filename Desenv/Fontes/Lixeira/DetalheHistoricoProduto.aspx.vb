Imports System.Data.SqlClient

Public Class DetalheHistoricoProduto
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                DirectCast(Me.Master.Controls(0).Controls(3).Controls(7).FindControl("menuOsOnline"), Menu).Visible = False
                Dim cn As New ctlHistoricoProduto
                'Dim reader As SqlDataReader
                'If Request("NumeroChamado") IsNot Nothing Then
                '    reader = cn.Listar(CType(0, ctlHistoricoProduto.TipoPesquisa), CStr(Session("NumeroSerie")), "1", Request("NumeroChamado"))
                'Else
                '    reader = cn.Listar(CStr(Session("NumeroSerie")), Nothing, Request("NumeroOS"))
                'End If
                'If reader.HasRows() Then
                '    TituloPagina("Histórico do Produto - Visualizar")
                '    Preencher(reader)
                'Else
                '    TituloPagina("Histórico do Produto - Incluir")
                'End If
            End If
        Catch ex As Exception
            Dim oRet As New ctlRetornoGenerico
            ctlUtil.EscreverLogErro("DetalheHistoricoProduto - Page_load: " & ex.Message())
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
        lblSerie.Text = Session("NumeroSerie").ToString
        lblProduto.Text = dt.Rows(0).Item("DescricaoProduto").ToString
        lblNumeroOs.Text = dt.Rows(0).Item("NumeroOs").ToString
        lblItem.Text = dt.Rows(0).Item("ItemOs").ToString
        lblCNPJ.Text = dt.Rows(0).Item("CgcCliente").ToString
        lblCliente.Text = dt.Rows(0).Item("NomeCliente").ToString
        lblSituacao.Text = dt.Rows(0).Item("DescricaoSituacao").ToString
        lblOcorrencia.Text = dt.Rows(0).Item("DescricaoOcorrencia").ToString
        lblServico.Text = dt.Rows(0).Item("Servico").ToString
        lblEtapa.Text = dt.Rows(0).Item("DescricaoEtapa").ToString
        lblDataInclusão.Text = dt.Rows(0).Item("DataInclusao").ToString
        lblHoraInclusao.Text = dt.Rows(0).Item("HoraInclusao").ToString
        lblNumeroChamado.Text = dt.Rows(0).Item("NumeroChamado").ToString
        lblNumeroOrcamento.Text = dt.Rows(0).Item("NumeroOrcamento").ToString
        lblTipo.Text = dt.Rows(0).Item("Descricaotipo").ToString
        lblClassificacao.Text = dt.Rows(0).Item("DescricaoClassificacao").ToString
        lblStatus.Text = dt.Rows(0).Item("DescricaoStatus").ToString
    End Sub

    Private Sub TituloPagina(ByVal sTexto As String)
        DirectCast(Me.Master.Controls(0).Controls(3).Controls(7).FindControl("oBarraUsuario"), BarraUsuario).PaginaAtual = sTexto
    End Sub


End Class