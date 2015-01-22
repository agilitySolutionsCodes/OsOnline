Imports System.Data.SqlClient
Imports System.ComponentModel

<Serializable()> _
Public Class ctlBaseInstalada

    Public Property NomeEquipamento As String
    Public Property CodigoEquipamento As String
    Public Property SerieEquipamento As String
    Public Property LoteEquipamento As String
    Public Property CodigoStatus As String
    Public Property Endereco As String
    Public Property Cliente As ctlCliente
    Public Property CodigoTecnico As String
    Public Property NotaFiscal As String
    Public Property SerieNota As String
    Public Property Pedido As String
    Public Property ItemPedido As String
    Public Property DataVenda As Date
    Public Property DataGarantia As Date
    Public Property LocalInstalacao As String
    Public Property Contato As String
    Public Property Telefone As String
    Public Property Email As String
    Public Property Mensagem As String
    Public Property Itens As New List(Of ctlItemBaseInstalada)

    Public Sub New()
    End Sub

    'Public Function PutSiga(ByVal oObj As wsmicrosiga.baseinstalada.FICHABASEINSTALADASTRUCT) As Object
    '    oObj.CNPJCLIENTE = Cliente.CNPJ
    '    oObj.CODIGOPRODUTO = CodigoEquipamento
    '    oObj.CODIGOTECNICO = CodigoTecnico
    '    oObj.CONTATO = Contato
    '    oObj.ENDERECO = Endereco
    '    oObj.ITEMPEDIDO = ItemPedido
    '    oObj.LOCALINSTALACAO = LocalInstalacao
    '    oObj.LOTE = LoteEquipamento
    '    oObj.MAILUSUARIO = Email
    '    oObj.MENSAGEMAPROVADOR = Mensagem
    '    oObj.NOTAFISCAL = NotaFiscal
    '    oObj.PEDIDO = Pedido '012521
    '    oObj.SERIE = SerieEquipamento
    '    oObj.SERIENOTA = SerieNota
    '    oObj.STATUSEQUIPAMENTO = CodigoStatus
    '    oObj.TELEFONE = Telefone
    '    Return (oObj)
    'End Function

    Public Sub PutItens(ByVal dt As DataTable)
        For Each dr As DataRow In dt.Rows
            Dim oItem As New ctlItemBaseInstalada
            oItem.Acessorio = dr("Acessorio").ToString
            oItem.Serie = dr("Serie").ToString
            oItem.Lote = dr("Lote").ToString
            Itens.Add(oItem)
        Next
    End Sub

    Public Enum TipoPesquisa
        <Description("Nr Série Equipamento")> _
        [Equipamento]
        [Cliente]
    End Enum

    Public Function Listar(ByVal oTipoPesquisa As TipoPesquisa, ByVal oParametros As Object) As SqlDataReader
        Dim conn = ctlUtil.GetConnection
        Dim cmd As New SqlCommand("PR_LISTAR_ITENS_BASES_INSTALADAS_02", conn)

        cmd.CommandTimeout = conn.ConnectionTimeout
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add(New SqlParameter("@P_FILIAL", ctlUtil.GetFilial)) 'DirectCast(HttpContext.Current.Session("Usuario2"), Usuario).Filial))
        Select Case oTipoPesquisa
            Case TipoPesquisa.Cliente
                Dim aParametros As String()
                aParametros = DirectCast(oParametros, String())
                cmd.Parameters.Add(New SqlParameter("@P_CODIGOCLIENTE", aParametros(0).ToString))
            Case TipoPesquisa.Equipamento
                cmd.Parameters.Add(New SqlParameter("@P_NUMEROSERIE", DirectCast(oParametros, String)))
        End Select
        conn.Open()
        Return cmd.ExecuteReader(CommandBehavior.CloseConnection)
    End Function

    'Usado no controle BaseInstaladaView.wuc e ConsultaBaseInstalada.aspx
    Public Function Pesquisar(ByVal oTipo As TipoPesquisa, ByVal oParametros As Object, _
                                         Optional ByVal ligado As Boolean = True) As DataTable

        Dim dt As New DataTable


        'PESQUISA TABELA GRUPO X SERIE CASO CLIENTE ESTEJA PREENCHIDO
        If Not String.IsNullOrEmpty(HttpContext.Current.Session("CodigoCliente").ToString) Then
            dt = PesquisarGrupoSerie(oTipo, oParametros, ligado)
        End If
        If oTipo = TipoPesquisa.Equipamento And dt.Rows.Count = 0 Then
            'CASO CLIENTE NAO ESTEJA PREENCHIDO, PESQUISAR NA BASE INSTALADA
            dt = PesquisarBaseInstalada(oTipo, oParametros, ligado)
            If dt.Rows.Count > 0 Then
                HttpContext.Current.Session.Add("NovoItem", "S")
            End If
        End If

        Return dt
    End Function

    Private Function PesquisarGrupoSerie(ByVal oTipo As TipoPesquisa, ByVal oParametros As Object, _
                                         Optional ByVal ligado As Boolean = True) As DataTable
        Dim conn = ctlUtil.GetConnection
        Dim cmd As New SqlCommand("PR_LISTAR_BASES_GRUPO_SERIE_02", conn)

        cmd.CommandTimeout = conn.ConnectionTimeout
        cmd.CommandType = CommandType.StoredProcedure
        Dim parmCodigo As New SqlParameter("@P_CODIGOCLIENTE", DBNull.Value)
        Dim parmNome As New SqlParameter("@P_NUMEROSERIE", DBNull.Value)
        Dim dt As New DataTable
        Select Case oTipo
            Case TipoPesquisa.Equipamento
                parmNome.Value = oParametros.ToString()
                If HttpContext.Current.Session("CodigoCliente") IsNot Nothing AndAlso ligado = True Then
                    parmCodigo.Value = DirectCast(HttpContext.Current.Session("CodigoCliente"), String).Trim
                End If
            Case TipoPesquisa.Cliente
                If IsArray(oParametros) Then
                    Dim aParametros As String()
                    aParametros = DirectCast(oParametros, String())
                    parmCodigo.Value = aParametros(0).ToString()
                Else
                    parmCodigo.Value = oParametros.ToString()
                End If
        End Select
        cmd.Parameters.Add(parmCodigo)
        cmd.Parameters.Add(parmNome)
        cmd.Parameters.Add(New SqlParameter("@P_FILIAL", ctlUtil.GetFilial))
        cmd.Parameters.Add(New SqlParameter("@P_GRUPO", HttpContext.Current.Session("Grupo").ToString))
        Dim da As New SqlDataAdapter(cmd)
        da.Fill(dt)
        Return dt
    End Function

    Private Function PesquisarBaseInstalada(ByVal oTipo As TipoPesquisa, ByVal oParametros As Object, _
                                         Optional ByVal ligado As Boolean = True) As DataTable
        Dim conn = ctlUtil.GetConnection
        Dim cmd As New SqlCommand("PR_LISTAR_BASE_INSTALADA_02", conn)

        cmd.CommandTimeout = conn.ConnectionTimeout
        cmd.CommandType = CommandType.StoredProcedure
        Dim parmCodigo As New SqlParameter("@P_CODIGOCLIENTE", DBNull.Value)
        Dim parmNome As New SqlParameter("@P_NUMEROSERIE", DBNull.Value)
        Dim dt As New DataTable
        Select Case oTipo
            Case TipoPesquisa.Equipamento
                parmNome.Value = oParametros.ToString()
                If HttpContext.Current.Session("CodigoCliente") IsNot Nothing AndAlso ligado = True Then
                    parmCodigo.Value = DirectCast(HttpContext.Current.Session("CodigoCliente"), String).Trim
                End If
            Case TipoPesquisa.Cliente
                If IsArray(oParametros) Then
                    Dim aParametros As String()
                    aParametros = DirectCast(oParametros, String())
                    parmCodigo.Value = aParametros(0).ToString()
                Else
                    parmCodigo.Value = oParametros.ToString()
                End If
        End Select
        cmd.Parameters.Add(parmCodigo)
        cmd.Parameters.Add(parmNome)
        cmd.Parameters.Add(New SqlParameter("@P_FILIAL", ctlUtil.GetFilial))
        Dim da As New SqlDataAdapter(cmd)
        da.Fill(dt)
        Return dt
    End Function

    Public Function PesquisarBasesInstaladas(ByVal oTipo As TipoPesquisa, ByVal oParametros As Object, _
                                         Optional ByVal ligado As Boolean = True) As DataTable
        Dim conn = ctlUtil.GetConnection
        Dim cmd As New SqlCommand("PR_LISTAR_BASES_INSTALADAS_02", conn)

        cmd.CommandTimeout = conn.ConnectionTimeout
        cmd.CommandType = CommandType.StoredProcedure
        Dim parmCodigo As New SqlParameter("@P_CODIGOCLIENTE", DBNull.Value)
        Dim parmNome As New SqlParameter("@P_NUMEROSERIE", DBNull.Value)
        Dim dt As New DataTable
        Select Case oTipo
            Case TipoPesquisa.Equipamento
                parmNome.Value = oParametros.ToString()
                If HttpContext.Current.Session("CodigoCliente") IsNot Nothing AndAlso ligado = True Then
                    parmCodigo.Value = DirectCast(HttpContext.Current.Session("CodigoCliente"), String).Trim
                End If
            Case TipoPesquisa.Cliente
                If IsArray(oParametros) Then
                    Dim aParametros As String()
                    aParametros = DirectCast(oParametros, String())
                    parmCodigo.Value = aParametros(0).ToString()
                Else
                    parmCodigo.Value = oParametros.ToString()
                End If
        End Select
        cmd.Parameters.Add(parmCodigo)
        cmd.Parameters.Add(parmNome)
        cmd.Parameters.Add(New SqlParameter("@P_FILIAL", ctlUtil.GetFilial))
        cmd.Parameters.Add(New SqlParameter("@P_GRUPO", HttpContext.Current.Session("Grupo")))
        Dim da As New SqlDataAdapter(cmd)
        da.Fill(dt)
        Return dt
    End Function

    'Public Function Incluir(ByVal oFicha As ctlBaseInstalada) As ctlRetornoGenerico
    '    'Dim oWS As New wsmicrosiga.baseinstalada.WSBASEINSTALADA
    '    'Dim oTokenSiga As New wsmicrosiga.baseinstalada.TOKENSTRUCT
    '    'oTokenSiga.CONTEUDO = HttpContext.Current.Session("NomeUsuario").ToString
    '    'oTokenSiga.SENHA = DirectCast(HttpContext.Current.Session("Usuario2"), ctlUsuario).Hash
    '    'Dim oFichaSiga As New wsmicrosiga.baseinstalada.FICHABASEINSTALADASTRUCT
    '    'oFichaSiga = DirectCast(oFicha.PutSiga(oFichaSiga), wsmicrosiga.baseinstalada.FICHABASEINSTALADASTRUCT)
    '    'Dim oItensD As New List(Of wsmicrosiga.baseinstalada.ACESSORIOBASEINSTALADASTRUCT)
    '    'For Each oItem As ctlItemBaseInstalada In oFicha.Itens
    '    '    Dim oItemD As New wsmicrosiga.baseinstalada.ACESSORIOBASEINSTALADASTRUCT
    '    '    oItemD = DirectCast(oItem.PutSiga(oItemD), wsmicrosiga.baseinstalada.ACESSORIOBASEINSTALADASTRUCT)
    '    '    oItensD.Add(oItemD)
    '    'Next
    '    'oFichaSiga.ACESSORIOS = oItensD.ToArray
    '    'Return New ctlRetornoGenerico(oWS.INCLUIR(oTokenSiga, oFichaSiga))
    'End Function

    Public Function ListarStatus() As SqlDataReader
        Dim conn = ctlUtil.GetConnection
        Dim cmd As New SqlCommand("PR_LISTAR_STATUS_BASE_INSTALADA_02", conn)

        cmd.CommandTimeout = conn.ConnectionTimeout
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add(New SqlParameter("@P_FILIAL", ctlUtil.GetFilial()))
        conn.Open()
        Return cmd.ExecuteReader(CommandBehavior.CloseConnection)
    End Function

    Public Function ListarTiposPesquisa() As Array
        Return System.Enum.GetValues(GetType(TipoPesquisa))
    End Function

    Public Function IncluirItemGrupoSerie(ByVal oCliente As ctlCliente, ByVal sNumSerie As String, ByVal sCodProduto As String, ByVal sNumero As String, ByVal sTipo As String) As SqlDataReader

        Dim conn = ctlUtil.GetConnection
        Dim cmd As New SqlCommand("PR_INCLUIR_ITEM_GRUPO_SERIE_02", conn)

        cmd.CommandTimeout = conn.ConnectionTimeout
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add(New SqlParameter("@P_FILIAL", ctlUtil.GetFilial))
        cmd.Parameters.Add(New SqlParameter("@P_CODIGOCLIENTE", oCliente.Codigo))
        cmd.Parameters.Add(New SqlParameter("@P_LOJA", oCliente.Loja))
        cmd.Parameters.Add(New SqlParameter("@P_NUMEROSERIE", sNumSerie))
        cmd.Parameters.Add(New SqlParameter("@P_GRUPO", HttpContext.Current.Session("Grupo").ToString))
        cmd.Parameters.Add(New SqlParameter("@P_CNPJ", oCliente.CNPJ))
        cmd.Parameters.Add(New SqlParameter("@P_CODIGOPRODUTO", sCodProduto))
        'CT=CHAMADO TECNICO OU OS = ORDEM SERVICO
        If sTipo = "CT" Then
            cmd.Parameters.Add(New SqlParameter("@P_NRCHAM", sNumero))
        Else
            cmd.Parameters.Add(New SqlParameter("@P_NUMOS", sNumero))
        End If
        conn.Open()
        Return cmd.ExecuteReader(CommandBehavior.CloseConnection)
    End Function

    Public Function BaseNecessitaCadastro(ByVal sNumSerie As String) As Boolean

        Dim conn = ctlUtil.GetConnection
        Dim cmd As New SqlCommand()
        Dim reader As SqlDataReader
        Dim bAltera As Boolean = False
        Dim dt As New DataTable

        cmd.CommandTimeout = conn.ConnectionTimeout
        cmd.CommandText = "SELECT AA3_ENDERE, AA3_INSLOC, AA3_DTINST FROM AA3020 (NOLOCK) WHERE AA3_NUMSER = '" & sNumSerie & "'"
        cmd.Connection = conn
        conn.Open()
        reader = cmd.ExecuteReader(CommandBehavior.CloseConnection)

        If reader.HasRows Then
            dt.Load(reader)
            reader.Close()

            If String.IsNullOrEmpty(dt.Rows(0).Item("AA3_ENDERE").ToString.Trim) Or String.IsNullOrEmpty(dt.Rows(0).Item("AA3_INSLOC").ToString.Trim) Or String.IsNullOrEmpty(dt.Rows(0).Item("AA3_DTINST").ToString.Trim) Then
                bAltera = True
            End If
        End If

        Return bAltera

    End Function

    Public Function Salvar(ByVal sNumSerie As String, ByVal sEnd As String, ByVal sLocal As String, ByVal sDataInst As String, ByVal sTel As String, ByVal sContato As String, ByVal sCodTec As String, ByVal sEstado As String, ByVal sCidade As String) As SqlDataReader

        Dim conn = ctlUtil.GetConnection
        Dim cmd As New SqlCommand("PR_ALTERAR_BASE_INSTALADA_02", conn)
        Dim dt As New DataTable

        cmd.CommandTimeout = conn.ConnectionTimeout
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add(New SqlParameter("@P_NUMEROSERIE", sNumSerie))
        cmd.Parameters.Add(New SqlParameter("@P_LOCAL", sLocal))
        cmd.Parameters.Add(New SqlParameter("@P_ENDERECO", sEnd))
        cmd.Parameters.Add(New SqlParameter("@P_CIDADE", sCidade))
        cmd.Parameters.Add(New SqlParameter("@P_ESTADO", sEstado))
        cmd.Parameters.Add(New SqlParameter("@P_TEL", sTel))
        cmd.Parameters.Add(New SqlParameter("@P_CONTATO", sContato))
        cmd.Parameters.Add(New SqlParameter("@P_CODTEC", sCodTec))
        cmd.Parameters.Add(New SqlParameter("@P_DTINSTAL", sDataInst))
        conn.Open()
        Return cmd.ExecuteReader(CommandBehavior.CloseConnection)

    End Function

    Public Function ListarBases(ByVal sListaNumSerie As String) As DataTable
        Dim conn = ctlUtil.GetConnection
        Dim dt As New DataTable
        Dim cmd As New SqlCommand("PR_LISTAR_BASES_02", conn)

        cmd.CommandTimeout = conn.ConnectionTimeout
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add(New SqlParameter("@P_FILIAL", ctlUtil.GetFilial))
        cmd.Parameters.Add(New SqlParameter("@P_NUMEROSERIE", sListaNumSerie))
        Dim da As New SqlDataAdapter(cmd)
        da.Fill(dt)
        Return dt
    End Function

    Public Function ListarEstados() As SqlDataReader
        Dim conn = ctlUtil.GetConnection
        Dim cmd As New SqlCommand("PR_LISTAR_ESTADOS", conn)

        cmd.CommandTimeout = conn.ConnectionTimeout
        cmd.CommandType = CommandType.StoredProcedure
        Dim dt As New DataTable
        cmd.Parameters.Add(New SqlParameter("@P_FILIAL", ctlUtil.GetFilial))
        conn.Open()
        Return cmd.ExecuteReader(CommandBehavior.CloseConnection)
    End Function

    Public Function ListarCidades(sEstado As String) As SqlDataReader
        Dim conn = ctlUtil.GetConnection
        Dim cmd As New SqlCommand("PR_LISTAR_CIDADES", conn)

        cmd.CommandTimeout = conn.ConnectionTimeout
        cmd.CommandType = CommandType.StoredProcedure
        Dim dt As New DataTable
        cmd.Parameters.Add(New SqlParameter("@P_FILIAL", ctlUtil.GetFilial))
        cmd.Parameters.Add(New SqlParameter("@P_ESTADO", sEstado))
        conn.Open()
        Return cmd.ExecuteReader(CommandBehavior.CloseConnection)
    End Function

End Class
