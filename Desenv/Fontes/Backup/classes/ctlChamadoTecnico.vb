Imports System.Data.SqlClient
Imports System.ComponentModel

<Serializable()> _
Public Class ctlChamadoTecnico

    Public Property Numero As String
    Public Property Emissao As Date
    Public Property Cliente As ctlCliente
    Public Property Contato As String
    Public Property Telefone As String
    Public Property Situacao As String
    Public Property CodigoTipo As String
    Public Property NumeroOS As String
    Public Property Itens As New List(Of ctlItemChamadoTecnico)

    Public Function PutSiga(ByVal oObj As wsmicrosiga.ordemservico.FICHAORDEMSERVICOSTRUCT) As Object
        oObj.CNPJ = Cliente.CNPJ
        oObj.CODIGOCLIENTE = Cliente.Codigo '+ Cliente.Loja
        oObj.CONTATO = Contato
        oObj.TELEFONE = Telefone
        oObj.TIPO = CodigoTipo
        oObj.TIPOORDEMSERVICO = "02"
        oObj.NUMEROCHAMADO = Numero
        oObj.NUMEROORDEMSERVICO = NumeroOS
        oObj.CHAMADOOS = "CT" 'chamado tecnico
        Return (oObj)
    End Function

    Public Sub PutItens(ByVal dt As DataTable)
        For Each dr As DataRow In dt.Rows
            Dim oItem As New ctlItemChamadoTecnico
            oItem.ItemChamado = dr("ItemChamado").ToString
            oItem.CodigoSituacao = dr("CodigoSituacao").ToString
            oItem.CodigoClassificacao = dr("CodigoClassificacao").ToString
            oItem.CodigoProduto = dr("CodigoProduto").ToString
            oItem.NumeroSerieProduto = dr("NumeroSerieProduto").ToString.Trim
            oItem.CodigoOcorrencia = dr("CodigoOcorrencia").ToString
            oItem.Comentario = dr("Comentario").ToString
            oItem.D_E_L_E_T_ = dr("D_E_L_E_T_").ToString
            Itens.Add(oItem)
        Next
    End Sub

    Public Enum TipoPesquisa
        [Todos]
        <Description("Nr Chamado")> _
        [Número]
        <Description("Nr Chamado Antigo")> _
        [ChamadoAntigo]
        <Description("CNPJ Cliente")> _
        [Cliente]
        <Description("Data Emissão")> _
        [Emissão]
        <Description("Nr Série Equipamento")> _
        [Equipamento]
    End Enum

    Public Function Listar(ByVal oTipoPesquisa As TipoPesquisa, ByVal oParametros As Object) As SqlDataReader
        Dim conn = ctlUtil.GetConnection
        Dim cmd As New SqlCommand("PR_LISTAR_CHAMADOS_TECNICOS_02", conn)

        cmd.CommandTimeout = conn.ConnectionTimeout
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add(New SqlParameter("@P_LICENCIADO", HttpContext.Current.User.Identity.Name))
        cmd.Parameters.Add(New SqlParameter("@P_FILIAL", ctlUtil.GetFilial))
        Select Case oTipoPesquisa
            Case TipoPesquisa.Cliente
                Dim aParametros As String()
                aParametros = DirectCast(oParametros, String())
                cmd.Parameters.Add(New SqlParameter("@P_CODIGOCLIENTE", aParametros(0).ToString))
            Case TipoPesquisa.Emissão
                cmd.Parameters.Add(New SqlParameter("@P_EMISSAO", DirectCast(oParametros, Date)))
            Case TipoPesquisa.Número
                cmd.Parameters.Add(New SqlParameter("@P_NUMEROCHAMADO", DirectCast(oParametros, String)))
            Case TipoPesquisa.ChamadoAntigo
                cmd.Parameters.Add(New SqlParameter("@P_CHAMADOANTIGO", DirectCast(oParametros, String)))
            Case TipoPesquisa.Equipamento
                cmd.Parameters.Add(New SqlParameter("@P_NUMEROSERIE", DirectCast(oParametros, String)))
        End Select
        conn.Open()
        Return cmd.ExecuteReader(CommandBehavior.CloseConnection)
    End Function

    Public Function Selecionar(ByVal numero As String, Optional ByVal ligado As Boolean = True) As SqlDataReader
        Dim conn = ctlUtil.GetConnection
        Dim cmd As New SqlCommand("PR_LISTAR_ITENS_CHAMADOS_TECNICOS_02", conn)

        cmd.CommandTimeout = conn.ConnectionTimeout
        cmd.CommandType = CommandType.StoredProcedure
        If ligado Then
            cmd.Parameters.Add(New SqlParameter("@P_LICENCIADO", HttpContext.Current.User.Identity.Name)) 'DirectCast(HttpContext.Current.Session("Usuario2"), ctlUsuario).UserCode))
        End If
        cmd.Parameters.Add(New SqlParameter("@P_FILIAL", ctlUtil.GetFilial)) 'DirectCast(HttpContext.Current.Session("Usuario2"), Usuario).Filial))
        cmd.Parameters.Add(New SqlParameter("@P_NUMEROCHAMADO", numero))
        conn.Open()
        Return cmd.ExecuteReader(CommandBehavior.CloseConnection)
    End Function

    Public Function Incluir(ByVal oFicha As ctlChamadoTecnico) As ctlRetornoGenerico
        Dim oWS As New wsmicrosiga.ordemservico.WSORDEMSERVICO
        Dim oTokenSiga As New wsmicrosiga.ordemservico.TKNSTRUCT
        oTokenSiga.CONTEUDO = HttpContext.Current.Session("NomeUsuario").ToString
        oTokenSiga.SENHA = DirectCast(HttpContext.Current.Session("Usuario2"), ctlUsuario).Hash
        Dim oFichaSiga As New wsmicrosiga.ordemservico.FICHAORDEMSERVICOSTRUCT
        oFichaSiga = DirectCast(oFicha.PutSiga(oFichaSiga), wsmicrosiga.ordemservico.FICHAORDEMSERVICOSTRUCT)
        Dim oItensD As New List(Of wsmicrosiga.ordemservico.ITEMORDEMSERVICOSTRUCT)
        For Each oItem As ctlItemChamadoTecnico In oFicha.Itens
            Dim oItemD As New wsmicrosiga.ordemservico.ITEMORDEMSERVICOSTRUCT
            oItemD = DirectCast(oItem.PutSiga(oItemD), wsmicrosiga.ordemservico.ITEMORDEMSERVICOSTRUCT)
            oItensD.Add(oItemD)
        Next
        oFichaSiga.ITENS = oItensD.ToArray
        Return New ctlRetornoGenerico(oWS.INCLUIR(oTokenSiga, oFichaSiga))
    End Function


    Public Function Alterar(ByVal oFicha As ctlChamadoTecnico) As ctlRetornoGenerico
        Dim oWS As New wsmicrosiga.ordemservico.WSORDEMSERVICO
        Dim oTokenSiga As New wsmicrosiga.ordemservico.TKNSTRUCT
        oTokenSiga.CONTEUDO = HttpContext.Current.Session("NomeUsuario").ToString
        oTokenSiga.SENHA = DirectCast(HttpContext.Current.Session("Usuario2"), ctlUsuario).Hash
        Dim oFichaSiga As New wsmicrosiga.ordemservico.FICHAORDEMSERVICOSTRUCT
        oFichaSiga = DirectCast(oFicha.PutSiga(oFichaSiga), wsmicrosiga.ordemservico.FICHAORDEMSERVICOSTRUCT)
        Dim oItensD As New List(Of wsmicrosiga.ordemservico.ITEMORDEMSERVICOSTRUCT)
        For Each oItem As ctlItemChamadoTecnico In oFicha.Itens
            Dim oItemD As New wsmicrosiga.ordemservico.ITEMORDEMSERVICOSTRUCT
            oItemD = DirectCast(oItem.PutSiga(oItemD), wsmicrosiga.ordemservico.ITEMORDEMSERVICOSTRUCT)
            oItensD.Add(oItemD)
        Next
        oFichaSiga.ITENS = oItensD.ToArray

        Return New ctlRetornoGenerico(oWS.ALTERAR(oTokenSiga, oFichaSiga))
    End Function

    Public Function ListarClassificacoes(Optional bAtivo As Boolean = False) As SqlDataReader
        Dim conn = ctlUtil.GetConnection
        Dim cmd As New SqlCommand("PR_LISTAR_CLASSIFICACOES_02", conn)

        cmd.CommandTimeout = conn.ConnectionTimeout
        cmd.Parameters.Add(New SqlParameter("@P_FILIAL", ctlUtil.GetFilial))
        cmd.Parameters.Add(New SqlParameter("@P_ATIVO", bAtivo))
        cmd.CommandType = CommandType.StoredProcedure
        conn.Open()
        Return cmd.ExecuteReader(CommandBehavior.CloseConnection)
    End Function

    Public Function ListarOcorrencias(Optional bAtivo As Boolean = False) As SqlDataReader
        Dim conn = ctlUtil.GetConnection
        Dim cmd As New SqlCommand("PR_LISTAR_OCORRENCIAS_02", conn)

        cmd.CommandTimeout = conn.ConnectionTimeout
        cmd.Parameters.Add(New SqlParameter("@P_ATIVO", bAtivo))
        cmd.CommandType = CommandType.StoredProcedure
        conn.Open()
        Return cmd.ExecuteReader(CommandBehavior.CloseConnection)
    End Function


End Class
