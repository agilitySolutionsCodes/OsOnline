Public Class ctlItemAtendimento

    Public Property NumeroItem As String
    Public Property NumeroSeriePeca As String
    Public Property codigoItem As String
    Public Property QuantidadeItem As Integer
    Public Property CodigoServico As String
    Public Property NumeroLote As String
    Public Property D_E_L_E_T_ As String

    Public Function PutSiga(ByVal oItem As Object) As Object
        CallByName(oItem, "NumeroItem", CallType.Let, NumeroItem)
        CallByName(oItem, "CodigoProdutoNovo", CallType.Let, codigoItem)
        CallByName(oItem, "NumeroSerieNovo", CallType.Let, NumeroSeriePeca)
        CallByName(oItem, "NumeroSeriePecas", CallType.Let, NumeroSeriePeca)
        CallByName(oItem, "Quantidade", CallType.Let, QuantidadeItem)
        CallByName(oItem, "NumeroLote", CallType.Let, NumeroLote)
        CallByName(oItem, "D_E_L_E_T_", CallType.Let, D_E_L_E_T_)
        Return oItem
    End Function

End Class
