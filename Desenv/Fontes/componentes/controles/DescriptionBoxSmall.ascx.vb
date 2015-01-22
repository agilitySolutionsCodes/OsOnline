Public Class DescriptionBoxSmall
    Inherits System.Web.UI.UserControl

    Public Property Descricao() As String
        Get
            Return txtDescricao.Text
        End Get
        Set(ByVal value As String)
            txtDescricao.Text = value.Replace(Chr(0), "")
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