Public Class PopupBoxYesNo
    Inherits System.Web.UI.UserControl

    Public Property Titulo() As String
        Get
            Return lblTitulo.Text
        End Get
        Set(ByVal value As String)
            lblTitulo.Text = value
        End Set
    End Property

    Public Property Mensagem() As String
        Get
            Return lblMensagem.Text
        End Get
        Set(ByVal value As String)
            lblMensagem.Text = value
        End Set
    End Property

    Public Property SelecionadoSim() As String
        Get
            Return txtSelecionadoSim.Value
        End Get
        Set(ByVal value As String)
            txtSelecionadoSim.Value = value
        End Set
    End Property

    Public Property URLChamada() As String
        Get
            Return btnSim.CommandArgument
        End Get
        Set(ByVal value As String)
            btnSim.CommandArgument = value
        End Set
    End Property

    Public Sub Exibir(ByVal aOpcao() As String)
        Titulo = aOpcao(0)
        lblMensagem.Text = aOpcao(1)
        HiddenForModal_ModalPopupExtender.Show()
    End Sub

    Protected Sub btnSim_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSim.Click
        
        If Not String.IsNullOrEmpty(btnSim.CommandArgument) Then
            Response.Redirect(btnSim.CommandArgument)
        Else
            HiddenForModal_ModalPopupExtender.Show()
        End If

    End Sub

    Protected Sub btnNao_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnNao.Click
        HiddenForModal_ModalPopupExtender.Hide()
    End Sub
End Class