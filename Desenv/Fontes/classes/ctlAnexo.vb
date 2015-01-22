Imports System.Data.SqlClient

Public Class ctlAnexo

    Public Property NomeArquivo As String
    Public Property Descricao As String

    Public Sub New(NomeArquivo As String, Descricao As String)

        Me.NomeArquivo = NomeArquivo
        Me.Descricao = Descricao

    End Sub

    Public Function PutSiga(ByVal oAnexo As Object) As Object
        CallByName(oAnexo, "NomeArquivo", CallType.Let, NomeArquivo)
        CallByName(oAnexo, "DescArquivo", CallType.Let, Descricao)
        Return oAnexo
    End Function

    
End Class
