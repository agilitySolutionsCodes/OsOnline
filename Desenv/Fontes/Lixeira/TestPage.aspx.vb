Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Drawing.Printing

Public Class TestPage
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim cn As SqlConnection = ctlUtil.GetConnection()
        cn.Open()
    End Sub

    Protected Sub btnVai_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnVai.Click
        Dim pd As New PrintDocument
        pd.Print()
        lblTeste.Text = "impressao 2"
        pd.Print()
    End Sub

End Class