Public Class ctlItemOrdemServico

    Public Property ItemOs As String
    Public Property NumeroSerieProduto As String
    Public Property CodigoProduto As String
    Public Property CodigoClassificacao As String
    Public Property CodigoOcorrencia As String
    Public Property CodigoEtapa As String
    Public Property D_E_L_E_T_ As String
    Public Property CodigoSituacao As String = ""
    Public Property CodigoStatus As String = ""

    Public Function PutSiga(ByVal oItem As Object) As Object
        CallByName(oItem, "CodigoProduto", CallType.Let, CodigoProduto) '132.00000 - RESPIRADOR INTER-5
        CallByName(oItem, "CodigoClassificacao", CallType.Let, CodigoClassificacao) 'Corretiva
        CallByName(oItem, "CodigoOcorrencia", CallType.Let, CodigoOcorrencia) 'Defeito Flu
        CallByName(oItem, "CodigoEtapa", CallType.Let, CodigoEtapa) 'Instalação
        CallByName(oItem, "CodigoSituacao", CallType.Let, CodigoSituacao)
        CallByName(oItem, "CodigoStatus", CallType.Let, "A") 'CodigoStatus)
        CallByName(oItem, "NumeroSerie", CallType.Let, NumeroSerieProduto)
        CallByName(oItem, "NumeroItem", CallType.Let, ItemOs)
        CallByName(oItem, "D_E_L_E_T_", CallType.Let, D_E_L_E_T_)
        Return oItem
    End Function

End Class
