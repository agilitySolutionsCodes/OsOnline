Public Class ctlItemChamadoTecnico

    Public Property ItemChamado As String
    Public Property NumeroSerieProduto As String
    Public Property CodigoProduto As String
    Public Property CodigoClassificacao As String
    Public Property CodigoOcorrencia As String
    Public Property Comentario As String
    Public Property D_E_L_E_T_ As String
    Public Property CodigoSituacao As String = ""
    Public Property CodigoStatus As String = ""

    Public Function PutSiga(ByVal oItem As Object) As Object
        CallByName(oItem, "CodigoProduto", CallType.Let, CodigoProduto)
        CallByName(oItem, "CodigoClassificacao", CallType.Let, CodigoClassificacao)
        CallByName(oItem, "CodigoOcorrencia", CallType.Let, CodigoOcorrencia)
        CallByName(oItem, "Comentario", CallType.Let, Comentario)
        CallByName(oItem, "CodigoEtapa", CallType.Let, "")
        CallByName(oItem, "CodigoSituacao", CallType.Let, CodigoSituacao)
        CallByName(oItem, "CodigoStatus", CallType.Let, "A")
        CallByName(oItem, "NumeroSerie", CallType.Let, NumeroSerieProduto)
        CallByName(oItem, "NumeroItem", CallType.Let, ItemChamado)
        CallByName(oItem, "D_E_L_E_T_", CallType.Let, D_E_L_E_T_)
        Return oItem
    End Function

End Class
