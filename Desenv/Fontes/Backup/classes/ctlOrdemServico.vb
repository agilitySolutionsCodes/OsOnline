Imports System.Data.SqlClient
Imports System.ComponentModel

<Serializable()> _
Public Class ctlOrdemServico

    Public Property Chamado As String
    Public Property OrdemServico As String
    Public Property Emissao As Date
    Public Property Cliente As ctlCliente
    Public Property Contato As String
    Public Property Telefone As String
    Public Property OSParceiro As String
    Public Property SituacaoOS As String
    Public Property CodigoTipo As String
    Public Property CodigoOrigem As String
    Public Property Itens As New List(Of ctlItemOrdemServico)

    Public Sub New()
    End Sub


    Public Function PutSiga(ByVal oObj As wsmicrosiga.ordemservico.FICHAORDEMSERVICOSTRUCT) As Object
        oObj.CNPJ = Cliente.CNPJ
        oObj.CODIGOCLIENTE = Cliente.Codigo
        oObj.CONTATO = Contato
        oObj.TELEFONE = Telefone
        oObj.NUMEROOSPARC = OSParceiro
        oObj.TIPO = CodigoTipo
        oObj.TIPOORDEMSERVICO = CodigoOrigem
        oObj.NUMEROCHAMADO = Chamado
        oObj.NUMEROORDEMSERVICO = OrdemServico
        oObj.CHAMADOOS = "OS" 'OS

        Return (oObj)

    End Function

    Public Sub PutItens(ByVal dt As DataTable)

        For Each dr As DataRow In dt.Rows
            Dim oItem As New ctlItemOrdemServico
            oItem.ItemOs = dr("ItemOs").ToString
            oItem.CodigoProduto = dr("CodigoProduto").ToString
            oItem.CodigoClassificacao = dr("CodigoClassificacao").ToString
            oItem.CodigoSituacao = dr("CodigoSituacaoOS").ToString
            oItem.CodigoOcorrencia = dr("CodigoOcorrencia").ToString
            oItem.CodigoEtapa = dr("CodigoEtapa").ToString
            oItem.NumeroSerieProduto = dr("NumeroSerieProduto").ToString.Trim
            oItem.D_E_L_E_T_ = dr("D_E_L_E_T_").ToString
            Itens.Add(oItem)
        Next
    End Sub

    Public Enum TipoPesquisa
        Todos
        <Description("Nr OS")> _
        Número
        <Description("Nr OS Antiga")> _
        OSAntiga
        <Description("CNPJ Cliente")> _
        Cliente
        <Description("Data Emissão")> _
        Emissão
        <Description("Nr OS Parceiro")> _
        OSParceiro
        <Description("Nr Chamado")> _
        NrChamado
        <Description("Nr Série Equipamento")> _
        [Equipamento]
    End Enum

    Public Function Listar(ByVal oTipoPesquisa As TipoPesquisa, ByVal oParametros As Object) As SqlDataReader
        Dim conn = ctlUtil.GetConnection
        Dim cmd As New SqlCommand("PR_LISTAR_ORDEM_SERVICO_OS_02", conn)

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
                cmd.Parameters.Add(New SqlParameter("@P_NUMEROOS", DirectCast(oParametros, String)))
            Case TipoPesquisa.OSParceiro
                cmd.Parameters.Add(New SqlParameter("@P_OSPARCEIRO", DirectCast(oParametros, String)))
            Case TipoPesquisa.OSAntiga
                cmd.Parameters.Add(New SqlParameter("@P_OSANTIGA", DirectCast(oParametros, String)))
            Case TipoPesquisa.Todos
                cmd.Parameters.Add(New SqlParameter("@P_CODIGOSITUACAO", DirectCast(oParametros, String)))
            Case TipoPesquisa.NrChamado
                cmd.Parameters.Add(New SqlParameter("@P_NUMEROCHAMADO", DirectCast(oParametros, String)))
            Case TipoPesquisa.Equipamento
                cmd.Parameters.Add(New SqlParameter("@P_NUMEROSERIE", DirectCast(oParametros, String)))
        End Select
        conn.Open()

        Return cmd.ExecuteReader(CommandBehavior.CloseConnection)
    End Function


    Public Function Selecionar(ByVal numero As String, Optional ByVal item As String = Nothing, Optional ByVal ligado As Boolean = True) As SqlDataReader
        Dim conn = ctlUtil.GetConnection
        Dim cmd As New SqlCommand("PR_LISTAR_ITENS_ORDEM_SERVICO_OS_02", conn)

        cmd.CommandTimeout = conn.ConnectionTimeout
        cmd.CommandType = CommandType.StoredProcedure
        If ligado Then
            cmd.Parameters.Add(New SqlParameter("@P_LICENCIADO", HttpContext.Current.User.Identity.Name)) 'DirectCast(HttpContext.Current.Session("Usuario2"), ctlUsuario).UserCode))
        End If
        cmd.Parameters.Add(New SqlParameter("@P_FILIAL", ctlUtil.GetFilial)) 'DirectCast(HttpContext.Current.Session("Usuario2"), Usuario).Filial))
        cmd.Parameters.Add(New SqlParameter("@P_NUMEROOS", numero))
        cmd.Parameters.Add(New SqlParameter("@P_ITEMOS", item))
        conn.Open()
        Return cmd.ExecuteReader(CommandBehavior.CloseConnection)
    End Function


    Public Function ListarPorChamado(ByVal NrCham As String) As SqlDataReader
        Dim conn = ctlUtil.GetConnection
        Dim cmd As New SqlCommand("PR_LISTAR_ITENS_ORDEM_SERVICO_OS_02", conn)

        cmd.CommandTimeout = conn.ConnectionTimeout
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add(New SqlParameter("@P_LICENCIADO", HttpContext.Current.User.Identity.Name)) 'DirectCast(HttpContext.Current.Session("Usuario2"), ctlUsuario).UserCode))
        cmd.Parameters.Add(New SqlParameter("@P_FILIAL", ctlUtil.GetFilial)) 'DirectCast(HttpContext.Current.Session("Usuario2"), Usuario).Filial))
        cmd.Parameters.Add(New SqlParameter("@P_NUMEROCHAMADO", NrCham))
        conn.Open()
        Return cmd.ExecuteReader(CommandBehavior.CloseConnection)
    End Function

    Public Function Incluir(ByVal oFicha As ctlOrdemServico) As ctlRetornoGenerico
        Dim oWS As New wsmicrosiga.ordemservico.WSORDEMSERVICO
        Dim oTokenSiga As New wsmicrosiga.ordemservico.TKNSTRUCT
        oTokenSiga.CONTEUDO = HttpContext.Current.Session("NomeUsuario").ToString
        oTokenSiga.SENHA = DirectCast(HttpContext.Current.Session("Usuario2"), ctlUsuario).Hash
        Dim oFichaSiga As New wsmicrosiga.ordemservico.FICHAORDEMSERVICOSTRUCT
        oFichaSiga = DirectCast(oFicha.PutSiga(oFichaSiga), wsmicrosiga.ordemservico.FICHAORDEMSERVICOSTRUCT)
        Dim oItensD As New List(Of wsmicrosiga.ordemservico.ITEMORDEMSERVICOSTRUCT)
        For Each oItem As ctlItemOrdemServico In oFicha.Itens
            Dim oItemD As New wsmicrosiga.ordemservico.ITEMORDEMSERVICOSTRUCT
            oItemD = DirectCast(oItem.PutSiga(oItemD), wsmicrosiga.ordemservico.ITEMORDEMSERVICOSTRUCT)
            oItensD.Add(oItemD)
        Next
        oFichaSiga.ITENS = oItensD.ToArray

        Return New ctlRetornoGenerico(oWS.INCLUIROS(oTokenSiga, oFichaSiga))
    End Function

    Public Function Alterar(ByVal oFicha As ctlOrdemServico) As ctlRetornoGenerico
        Dim oWS As New wsmicrosiga.ordemservico.WSORDEMSERVICO
        Dim oTokenSiga As New wsmicrosiga.ordemservico.TKNSTRUCT
        oTokenSiga.CONTEUDO = HttpContext.Current.Session("NomeUsuario").ToString
        oTokenSiga.SENHA = DirectCast(HttpContext.Current.Session("Usuario2"), ctlUsuario).Hash
        Dim oFichaSiga As New wsmicrosiga.ordemservico.FICHAORDEMSERVICOSTRUCT
        oFichaSiga = DirectCast(oFicha.PutSiga(oFichaSiga), wsmicrosiga.ordemservico.FICHAORDEMSERVICOSTRUCT)
        Dim oItensD As New List(Of wsmicrosiga.ordemservico.ITEMORDEMSERVICOSTRUCT)
        For Each oItem As ctlItemOrdemServico In oFicha.Itens
            Dim oItemD As New wsmicrosiga.ordemservico.ITEMORDEMSERVICOSTRUCT
            oItemD = DirectCast(oItem.PutSiga(oItemD), wsmicrosiga.ordemservico.ITEMORDEMSERVICOSTRUCT)
            oItensD.Add(oItemD)
        Next
        oFichaSiga.ITENS = oItensD.ToArray
        Return New ctlRetornoGenerico(oWS.ALTERAR(oTokenSiga, oFichaSiga))
    End Function

    Public Function ListarOrigensOS() As SqlDataReader
        Dim conn = ctlUtil.GetConnection
        Dim cmd As New SqlCommand("PR_LISTAR_ORIGENSOS_02", conn)

        cmd.CommandTimeout = conn.ConnectionTimeout
        cmd.Parameters.Add(New SqlParameter("@P_FILIAL", ctlUtil.GetFilial))
        cmd.CommandType = CommandType.StoredProcedure
        conn.Open()
        Return cmd.ExecuteReader(CommandBehavior.CloseConnection)
    End Function

    Public Function ListarEtapasOS(Optional bAtivo As Boolean = False) As SqlDataReader
        Dim conn = ctlUtil.GetConnection
        Dim cmd As New SqlCommand("PR_LISTAR_ETAPASOS_02", conn)

        cmd.CommandTimeout = conn.ConnectionTimeout
        cmd.Parameters.Add(New SqlParameter("@P_FILIAL", ctlUtil.GetFilial))
        cmd.Parameters.Add(New SqlParameter("@P_ATIVO", bAtivo))
        cmd.CommandType = CommandType.StoredProcedure
        conn.Open()
        Return cmd.ExecuteReader(CommandBehavior.CloseConnection)
    End Function

End Class
