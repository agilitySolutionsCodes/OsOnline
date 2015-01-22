Partial Public Class MasterPage
    Inherits System.Web.UI.MasterPage

    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim pagina As String = System.IO.Path.GetFileName(HttpContext.Current.Request.FilePath)

        If pagina <> "Login.aspx" Then
            If Session("Usuario2") Is Nothing Then
                Session.Abandon()
                Response.Redirect("Login.aspx")
            End If
            If Session("perfilAcesso").ToString() = "" Or Session("perfilAcesso").ToString = "N" Then
                Dim cph As ContentPlaceHolder = DirectCast(Me.FindControl("cMaster"), ContentPlaceHolder)
                Dim mTopMenu As Menu = DirectCast(cph.FindControl("menuOsOnline"), Menu)

                Dim menuItems As MenuItemCollection = mTopMenu.Items
                Dim adminItem As New MenuItem()
                For Each menuItem As MenuItem In menuItems
                    If menuItem.Text = "Documentos" Then
                        adminItem = menuItem
                    End If
                Next
                menuItems.Remove(adminItem)
            End If

        End If

    End Sub

End Class