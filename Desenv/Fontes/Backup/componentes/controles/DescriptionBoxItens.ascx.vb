Imports System.Data.SqlClient

Public Class DescriptionBoxItens
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If lblNumAtendimento.Value.Trim <> "" Then

            Try
                Dim oHistorico As New ctlHistoricoProduto
                Dim dt As New DataTable

                dt = oHistorico.ListarItensAtendimento(lblNumAtendimento.Value)

                grdItens.DataSource = dt
                grdItens.DataBind()

            Catch ex As Exception
                Dim oRet As New ctlRetornoGenerico
                oRet.Sucesso = False
                ctlUtil.EscreverLogErro("ConsultaHistoricoProduto - ListarItensAtendimento - Page_load: " & ex.Message())
                oRet.Mensagem = "Erro ao consultar itens do atendimento: " & ex.Message
                oMensagem.SetMessage(oRet)
            End Try

        End If
    End Sub

    Public Property Descricao() As String
        Get
            Return lblNumAtendimento.Value
        End Get
        Set(ByVal value As String)
            lblNumAtendimento.Value = value
        End Set
    End Property

    Public Property Enabled() As Boolean
        Get
            Return imgDescricao.Enabled
        End Get
        Set(ByVal value As Boolean)
            imgDescricao.Enabled = value
        End Set
    End Property

    Public Property ToolTip() As String
        Get
            Return imgDescricao.ToolTip
        End Get
        Set(ByVal value As String)
            imgDescricao.ToolTip = value
        End Set
    End Property

    Protected Sub btnFechar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFechar.Click
        pnlDescricao.Visible = False
        imgDescricao.Visible = True
    End Sub

    Protected Sub imgDescricao_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgDescricao.Click
        imgDescricao.Visible = False
        pnlDescricao.Visible = True
    End Sub

    Protected Sub imgFechar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgFechar.Click
        pnlDescricao.Visible = False
        imgDescricao.Visible = True
    End Sub

End Class