Public Class ctlItemBaseInstalada

    Public Property Acessorio As String
    Public Property Serie As String
    Public Property Lote As String

    Public Function PutSiga(ByVal oItem As Object) As Object
        CallByName(oItem, "Codigo", CallType.Let, Acessorio)
        CallByName(oItem, "Serie", CallType.Let, Serie)
        CallByName(oItem, "Lote", CallType.Let, Lote)
        Return oItem
    End Function

End Class
