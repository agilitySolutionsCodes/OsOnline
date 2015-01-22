Imports System.Data.SqlClient
Imports System.Drawing.Printing

Public Class ImpressaoOrdemServico
    Inherits BaseWebUI

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                Dim os As New ctlOrdemServico
                Dim reader As SqlDataReader
                reader = os.Selecionar(Request("Numero"))
                If reader.HasRows() Then
                    Preencher(reader)
                End If
            End If
        Catch ex As Exception
            Dim oRet As New ctlRetornoGenerico
            ctlUtil.EscreverLogErro("ImpressaoOrdemServico - Page_load: " & ex.Message())
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

        dt.DefaultView.RowFilter = " D_E_L_E_T_ <> '*' "
        grdItens.DataSource = dt
        grdItens.DataBind()

        lblNumero.Text = dt.Rows(0).Item("NumeroOS").ToString
        If String.IsNullOrEmpty(dt.Rows(0).Item("FilialCliente").ToString.Trim) Then
            lblCliente.Text = dt.Rows(0).Item("CodigoCliente").ToString _
                   + " - " + dt.Rows(0).Item("NomeCliente").ToString
        Else
            lblCliente.Text = dt.Rows(0).Item("CodigoCliente").ToString _
                   + "/" + dt.Rows(0).Item("FilialCliente").ToString _
                   + " - " + dt.Rows(0).Item("NomeCliente").ToString
        End If
        lblDataEmissao.Text = DirectCast(dt.Rows(0).Item("DataEmissao"), Date).ToShortDateString
        lblAtendente.Text = dt.Rows(0).Item("Atendente").ToString

    End Sub

    Private Sub btnVoltar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnVoltar.Click
        Response.Redirect("ConsultaOrdemServico.aspx?Numero=" + lblNumero.Text)
    End Sub

End Class