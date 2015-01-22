Public Class PopupBox
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

    Public Property CommandArgument() As String
        Get
            Return btnOk.CommandArgument
        End Get
        Set(ByVal value As String)
            btnOk.CommandArgument = value
        End Set
    End Property

    Public Property Opcao1() As String
        Get
            Return rdbOpcao1.Text
        End Get
        Set(ByVal value As String)
            rdbOpcao1.Text = value + "<br/>"
        End Set
    End Property

    Public Property Valor1() As String
        Get
            Return lblValor1.Text
        End Get
        Set(ByVal value As String)
            If String.IsNullOrEmpty(value) Then
                rdbOpcao1.Visible = False
            Else
                lblValor1.Text = value
            End If
        End Set
    End Property

    Public Property Opcao2() As String
        Get
            Return rdbOpcao2.Text
        End Get
        Set(ByVal value As String)
            rdbOpcao2.Text = value + "<br/>"
        End Set
    End Property

    Public Property Valor2() As String
        Get
            Return lblValor2.Text
        End Get
        Set(ByVal value As String)
            If String.IsNullOrEmpty(value) Then
                rdbOpcao2.Visible = False
            Else
                lblValor2.Text = value
            End If
        End Set
    End Property

    Public Sub Exibir(ByVal aOpcao() As String)
        Titulo = aOpcao(0)
        lblMensagem.Text = aOpcao(1)
        Opcao1 = aOpcao(2)
        Valor1 = aOpcao(3)
        Opcao2 = aOpcao(4)
        Valor2 = aOpcao(5)
        HiddenForModal_ModalPopupExtender.Show()
    End Sub

    Private Sub btnOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnOk.Click
        If Not String.IsNullOrEmpty(btnOk.CommandArgument) Then
            Response.Redirect(btnOk.CommandArgument)
        Else
            If rdbOpcao1.Checked Then
                Response.Redirect(Valor1)
            ElseIf rdbOpcao2.Checked Then
                Response.Redirect(Valor2)
            Else
                HiddenForModal_ModalPopupExtender.Show()
            End If
        End If
    End Sub

End Class