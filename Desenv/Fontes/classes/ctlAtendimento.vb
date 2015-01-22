Imports System.Data.SqlClient
Imports System.ComponentModel
Imports System.Threading

Public Class ctlAtendimento

    Public Property ModoAtendimento As String
    Public Property NumeroAtendimentoOS As String
    Public Property SequenciaAtendimentoOS As String
    Public Property NumeroSerieEquipamento As String
    Public Property CodigoTecnico As String
    Public Property DataInicio As Date
    Public Property HoraInicio As String
    Public Property DataTermino As Date
    Public Property HoraTermino As String
    Public Property Translado As String
    Public Property CodigoOcorrencia As String
    Public Property CodigoGarantia As String
    Public Property CodigoStatus As String
    Public Property HorasFaturadas As String
    Public Property IncluirItemOS As String
    Public Property Defeito As String
    Public Property Causa As String
    Public Property ServicoExecutado As String
    Public Property BloqueioAtendimento As String
    Public Property AprovadoAtendimento As String
    Public Property Aprovado2Atendimento As String
    Public Property VersaoSoftware As String
    Public Property VersaoAtual As String
    Public Property CodigoDefeito As String
    Public Property CodigoServico As String

    Public Property Itens As New List(Of ctlItemAtendimento)

    Public Sub New()
    End Sub

    Public Function PutSiga(ByVal oObj As wsmicrosiga.ordemservico.FICHAATENDIMENTOSTRUCT) As Object
        oObj.MODOATENDIMENTO = ModoAtendimento
        oObj.NUMEROATENDIMENTOOS = NumeroAtendimentoOS
        oObj.SEQUENCIAATENDIMENTOOS = SequenciaAtendimentoOS
        oObj.NUMEROSERIEEQUIP = NumeroSerieEquipamento
        oObj.INCLUIITEMOS = IncluirItemOS
        oObj.CAUSAPROVAVEL = Causa
        oObj.CODIGOSITUACAO = CodigoStatus
        oObj.CODIGOTECNICO = CodigoTecnico
        oObj.DATAINICIO = DataInicio
        oObj.DATAFIM = DataTermino
        oObj.DEFEITOCONSTATADO = Defeito
        oObj.HORAINICIO = HoraInicio
        oObj.HORAFIM = HoraTermino
        oObj.CODIGOOCORRENCIA = CodigoOcorrencia
        oObj.CODIGOGARANTIA = "2" 'CodigoGarantia
        oObj.SERVICOEXECUTADO = ServicoExecutado
        oObj.TRASLADO = Translado
        oObj.BLOQUEIOATENDIMENTO = BloqueioAtendimento
        oObj.APROVADOATENDIMENTO = AprovadoAtendimento
        oObj.APROVADO2ATENDIMENTO = Aprovado2Atendimento
        'oObj.VERSAOATUAL = VersaoAtual
        oObj.VERSAOSOFTWARE = VersaoSoftware
        oObj.CODIGODEFEITO = CodigoDefeito
        oObj.CODIGOSERVICO = CodigoServico
        Return (oObj)
    End Function

    Public Sub PutItens(ByVal dt As DataTable)
        For Each dr As DataRow In dt.Rows
            Dim oItem As New ctlItemAtendimento
            oItem.NumeroItem = dr("Item").ToString
            oItem.NumeroSeriePeca = dr("NumeroSeriePeca").ToString
            oItem.codigoItem = dr("CodigoItem").ToString
            oItem.QuantidadeItem = CInt(dr("Quantidade"))
            oItem.NumeroLote = dr("NumeroLote").ToString
            oItem.D_E_L_E_T_ = dr("D_E_L_E_T_").ToString
            Itens.Add(oItem)
        Next
    End Sub

    Public Enum TipoPesquisa
        [Todos]
        <Description("Nr OS")> _
        [NúmeroOS]
        <Description("Nr Atendimento OS")> _
        [Número]
        <Description("CNPJ Cliente")> _
        [Cliente]
        <Description("Nr Série Equipamento")> _
        [Equipamento]
    End Enum

    Public Function Listar(ByVal oTipoPesquisa As TipoPesquisa, ByVal oParametros As Object) As SqlDataReader
        Dim conn = ctlUtil.GetConnection
        Dim cmd As New SqlCommand("PR_LISTAR_ATENDIMENTOOS_02", conn)

        cmd.CommandTimeout = conn.ConnectionTimeout
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add(New SqlParameter("@P_LICENCIADO", HttpContext.Current.User.Identity.Name))
        cmd.Parameters.Add(New SqlParameter("@P_FILIAL", ctlUtil.GetFilial))
        Select Case oTipoPesquisa
            Case TipoPesquisa.Cliente
                Dim aParametros As String()
                aParametros = DirectCast(oParametros, String())
                cmd.Parameters.Add(New SqlParameter("@P_CODIGOCLIENTE", aParametros(0).ToString))
            Case TipoPesquisa.Número
                cmd.Parameters.Add(New SqlParameter("@P_NUMEROATENDIMENTOOS", DirectCast(oParametros, String)))
            Case TipoPesquisa.NúmeroOS
                cmd.Parameters.Add(New SqlParameter("@P_NUMEROOS", DirectCast(oParametros, String)))
            Case TipoPesquisa.Equipamento
                cmd.Parameters.Add(New SqlParameter("@P_NUMEROSERIE", DirectCast(oParametros, String)))
        End Select
        conn.Open()
        Return cmd.ExecuteReader(CommandBehavior.CloseConnection)
    End Function

    Public Function Selecionar(ByVal numero As String) As SqlDataReader
        Dim conn = ctlUtil.GetConnection
        Dim cmd As New SqlCommand("PR_LISTAR_ITENS_ATENDIMENTOOS_02", conn)

        cmd.CommandTimeout = conn.ConnectionTimeout
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add(New SqlParameter("@P_LICENCIADO", HttpContext.Current.User.Identity.Name)) 'DirectCast(HttpContext.Current.Session("Usuario2"), ctlUsuario).UserCode))
        cmd.Parameters.Add(New SqlParameter("@P_FILIAL", ctlUtil.GetFilial)) 'DirectCast(HttpContext.Current.Session("Usuario2"), Usuario).Filial))
        cmd.Parameters.Add(New SqlParameter("@P_NUMEROATENDIMENTOOS", numero))
        conn.Open()
        Return cmd.ExecuteReader(CommandBehavior.CloseConnection)
    End Function

    Public Function Atender(ByVal oFicha As ctlAtendimento) As ctlRetornoGenerico
        Dim oWS As New wsmicrosiga.ordemservico.WSORDEMSERVICO
        Thread.Sleep(200000)
        Dim oTokenSiga As New wsmicrosiga.ordemservico.TKNSTRUCT
        oTokenSiga.CONTEUDO = HttpContext.Current.Session("NomeUsuario").ToString
        oTokenSiga.SENHA = DirectCast(HttpContext.Current.Session("Usuario2"), ctlUsuario).Hash
        Dim oFichaSiga As New wsmicrosiga.ordemservico.FICHAATENDIMENTOSTRUCT
        oFichaSiga = DirectCast(oFicha.PutSiga(oFichaSiga), wsmicrosiga.ordemservico.FICHAATENDIMENTOSTRUCT)
        Dim oItensD As New List(Of wsmicrosiga.ordemservico.ITEMATENDIMENTOSTRUCT)
        For Each oItem As ctlItemAtendimento In oFicha.Itens
            Dim oItemD As New wsmicrosiga.ordemservico.ITEMATENDIMENTOSTRUCT
            oItemD = DirectCast(oItem.PutSiga(oItemD), wsmicrosiga.ordemservico.ITEMATENDIMENTOSTRUCT)
            oItensD.Add(oItemD)
        Next
        oFichaSiga.ITENS = oItensD.ToArray
        Return New ctlRetornoGenerico(oWS.ATENDER(oTokenSiga, oFichaSiga))
    End Function

    Public Function AtenderPedidoGerado(numero As String, sNrSerieCab As String, sCodProd As String, sCodItem As String, sLote As String, sNrsSerie As String, sCausa As String, sServicoExec As String, sStatus As String) As SqlDataReader
        Dim conn = ctlUtil.GetConnection
        Dim cmd As New SqlCommand("PR_ALTERAR_ITENS_ATENDIMENTOOS_02", conn)

        cmd.CommandTimeout = conn.ConnectionTimeout
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add(New SqlParameter("@P_FILIAL", ctlUtil.GetFilial))
        cmd.Parameters.Add(New SqlParameter("@P_NUMATENDIMENTO", numero))
        cmd.Parameters.Add(New SqlParameter("@P_NUMSERIE", sNrSerieCab))
        cmd.Parameters.Add(New SqlParameter("@P_CAUSA", sCausa))
        cmd.Parameters.Add(New SqlParameter("@P_SERVICOEXEC", sServicoExec))
        cmd.Parameters.Add(New SqlParameter("@P_STATUS", sStatus))
        cmd.Parameters.Add(New SqlParameter("@P_CODPRODUTOS", sCodProd))
        cmd.Parameters.Add(New SqlParameter("@P_CODITENS", sCodItem))
        cmd.Parameters.Add(New SqlParameter("@P_LOTES", sLote))
        cmd.Parameters.Add(New SqlParameter("@P_NRSERIES", sNrsSerie))
        conn.Open()
        Return cmd.ExecuteReader(CommandBehavior.CloseConnection)
    End Function

    Public Function ListarTecnicos(Optional ByVal regiao As String = Nothing) As SqlDataReader
        Dim conn = ctlUtil.GetConnection
        Dim cmd As New SqlCommand("PR_LISTAR_TECNICOS_02", conn)

        cmd.CommandTimeout = conn.ConnectionTimeout
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add(New SqlParameter("@P_REGIAO", regiao))
        conn.Open()
        Return cmd.ExecuteReader(CommandBehavior.CloseConnection)
    End Function

    Public Function ListarServicos() As SqlDataReader
        Dim conn = ctlUtil.GetConnection
        Dim cmd As New SqlCommand("PR_LISTAR_SERVICOS_02", conn)

        cmd.CommandTimeout = conn.ConnectionTimeout
        cmd.CommandType = CommandType.StoredProcedure
        conn.Open()
        Return cmd.ExecuteReader(CommandBehavior.CloseConnection)
    End Function

    Public Function ListarDefeitosServicos(ByVal sTipo As String) As SqlDataReader
        Dim conn = ctlUtil.GetConnection
        Dim cmd As New SqlCommand("PR_LISTAR_DEFEITOS_SERVICOS", conn)

        cmd.CommandTimeout = conn.ConnectionTimeout
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add(New SqlParameter("@P_TIPO", sTipo))
        conn.Open()
        Return cmd.ExecuteReader(CommandBehavior.CloseConnection)
    End Function

    Public Function VerificaVersaoSoftware(ByVal sNumSerie As String) As Boolean
        Dim conn = ctlUtil.GetConnection
        Dim cmd As New SqlCommand("PR_CONSULTAR_VERSAO_SOFT_USUARIO", conn)

        cmd.CommandTimeout = conn.ConnectionTimeout
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add(New SqlParameter("@P_LICENCIADO", HttpContext.Current.User.Identity.Name))
        cmd.Parameters.Add(New SqlParameter("@P_NUMSERIE", sNumSerie))
        cmd.Parameters.Add("@RETORNO", SqlDbType.Bit).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@MSGRETORNO", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output
        conn.Open()
        cmd.ExecuteReader(CommandBehavior.CloseConnection)
        'oRet.Sucesso = CBool(cmd.Parameters("@RETORNO").Value)
        'oRet.Mensagem = CStr(cmd.Parameters("@MSGRETORNO").Value)
        Return CBool(cmd.Parameters("@RETORNO").Value)
    End Function

    Public Function ListarEtapasAtendimento(ByVal sNumAtendimento As String) As DataTable
        Dim conn = ctlUtil.GetConnection
        Dim dt As New DataTable
        Dim cmd As New SqlCommand("PR_LISTAR_ETAPAS_ATENDIMENTO", conn)

        cmd.CommandTimeout = conn.ConnectionTimeout
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add(New SqlParameter("@P_FILIAL", ctlUtil.GetFilial))
        cmd.Parameters.Add(New SqlParameter("@P_NUMATENDIMENTO", sNumAtendimento))

        Dim da As New SqlDataAdapter(cmd)
        da.Fill(dt)
        Return dt

    End Function

    Public Function IncluirEtapasAtendimento(sItem As String, sNumAtend As String, sCodEtapa As String, sDescricao As String, sDataInicio As String, sHoraInicio As String, sDataFim As String, sHoraFim As String, sDeletado As String) As SqlDataReader
        'Public Function IncluirEtapasAtendimento(ByVal sNumAtend As String, ByVal dtEtapa As DataTable) As SqlDataReader
        Dim conn = ctlUtil.GetConnection
        Dim cmd As New SqlCommand("PR_INSERIR_ETAPAS_ATENDIMENTO", conn)

        cmd.CommandTimeout = conn.ConnectionTimeout
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add(New SqlParameter("@P_FILIAL", ctlUtil.GetFilial))
        cmd.Parameters.Add(New SqlParameter("@P_ITEM", sItem))
        cmd.Parameters.Add(New SqlParameter("@P_NUMATENDIMENTO", sNumAtend))
        'cmd.Parameters.Add(New SqlParameter("@P_TABETAPA", dtEtapa))
        cmd.Parameters.Add(New SqlParameter("@P_CODETAPA", sCodEtapa))
        cmd.Parameters.Add(New SqlParameter("@P_DESCRICAO", sDescricao))
        cmd.Parameters.Add(New SqlParameter("@P_DTINICIO", sDataInicio))
        cmd.Parameters.Add(New SqlParameter("@P_HRINICIO", sHoraInicio))
        cmd.Parameters.Add(New SqlParameter("@P_DTFIM", sDataFim))
        cmd.Parameters.Add(New SqlParameter("@P_HRFIM", sHoraFim))
        cmd.Parameters.Add(New SqlParameter("@P_DELETADO", sDeletado))
        conn.Open()
        Return cmd.ExecuteReader(CommandBehavior.CloseConnection)
    End Function

    Public Function ListarTopoAtendimentos(ByVal numero As String) As DataTable
        Dim conn = ctlUtil.GetConnection
        Dim dt As New DataTable
        Dim cmd As New SqlCommand("PR_LISTAR_TOPO_ATENDIMENTOS", conn)

        cmd.CommandTimeout = conn.ConnectionTimeout
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add(New SqlParameter("@P_LICENCIADO", HttpContext.Current.User.Identity.Name))
        cmd.Parameters.Add(New SqlParameter("@P_FILIAL", ctlUtil.GetFilial))
        cmd.Parameters.Add(New SqlParameter("@P_NUMEROOS", numero))
        Dim da As New SqlDataAdapter(cmd)
        da.Fill(dt)
        Return dt
    End Function

    Public Function ListarCabecalhoAtendimentos(ByVal numero As String) As DataTable
        Dim conn = ctlUtil.GetConnection
        Dim dt As New DataTable
        Dim cmd As New SqlCommand("PR_LISTAR_CABECALHO_ATENDIMENTOS", conn)

        cmd.CommandTimeout = conn.ConnectionTimeout
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add(New SqlParameter("@P_LICENCIADO", HttpContext.Current.User.Identity.Name))
        cmd.Parameters.Add(New SqlParameter("@P_FILIAL", ctlUtil.GetFilial))
        cmd.Parameters.Add(New SqlParameter("@P_NUMEROOS", numero))
        Dim da As New SqlDataAdapter(cmd)
        da.Fill(dt)
        Return dt
    End Function

    Public Function ListarItensAtendimento(ByVal sNumAtendimento As String) As DataTable
        Dim conn = ctlUtil.GetConnection
        Dim dt As New DataTable
        Dim cmd As New SqlCommand("PR_LISTAR_ITENS_ATENDIMENTOOS", conn)

        cmd.CommandTimeout = conn.ConnectionTimeout
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add(New SqlParameter("@P_FILIAL", ctlUtil.GetFilial))
        cmd.Parameters.Add(New SqlParameter("@P_NUMEROATENDIMENTOOS", sNumAtendimento))
        Dim da As New SqlDataAdapter(cmd)
        da.Fill(dt)
        Return dt
    End Function

    Public Function VerificarVersaoAtualSoftware(ByVal sNumSerie As String) As DataTable
        Dim conn = ctlUtil.GetConnection
        Dim dt As New DataTable
        Dim cmd As New SqlCommand("PR_LISTAR_VERSAO_ATUAL_SOFTWARE", conn)

        cmd.CommandTimeout = conn.ConnectionTimeout
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add(New SqlParameter("@P_FILIAL", ctlUtil.GetFilial))
        cmd.Parameters.Add(New SqlParameter("@P_NUMSERIE", sNumSerie))

        Dim da As New SqlDataAdapter(cmd)
        da.Fill(dt)
        Return dt

    End Function

End Class
