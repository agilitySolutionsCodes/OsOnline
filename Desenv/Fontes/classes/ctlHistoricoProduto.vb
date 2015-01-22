Imports System.Data.SqlClient
Imports System.ComponentModel

Public Class ctlHistoricoProduto

    Public Enum TipoPesquisa
        <Description("Nr Série Equipamento")> _
        [Equipamento]
    End Enum

    Public Function Listar(ByVal oTipoPesquisa As TipoPesquisa, ByVal oParametros As Object, _
                                           Optional ByVal sCodigoSituacao As String = Nothing, _
                                           Optional ByVal sNumeroChamadoTecnico As String = Nothing, _
                                           Optional ByVal sNumeroOS As String = Nothing) As SqlDataReader
        Dim conn = ctlUtil.GetConnection
        Dim cmd As New SqlCommand("PR_LISTAR_HISTORICOPRODUTO_02", conn)

        cmd.CommandTimeout = conn.ConnectionTimeout
        cmd.CommandType = CommandType.StoredProcedure
        Select Case oTipoPesquisa
            Case TipoPesquisa.Equipamento
                cmd.Parameters.Add(New SqlParameter("@P_NUMEROSERIE", DirectCast(oParametros, String)))
                cmd.Parameters.Add(New SqlParameter("@P_CODIGOSITUACAO", sCodigoSituacao))
                cmd.Parameters.Add(New SqlParameter("@P_FILIAL", ctlUtil.GetFilial))
                cmd.Parameters.Add(New SqlParameter("@P_LICENCIADO", HttpContext.Current.User.Identity.Name)) 'DirectCast(HttpContext.Current.Session("Usuario2"), ctlUsuario).UserCode))
        End Select
        conn.Open()
        Return cmd.ExecuteReader(CommandBehavior.CloseConnection)
    End Function

    Public Function ListarOS(ByVal oTipoPesquisa As TipoPesquisa, ByVal oParametros As Object) As SqlDataReader
        Dim conn = ctlUtil.GetConnection
        Dim cmd As New SqlCommand("PR_LISTAR_HISTORICOPRODUTO_OS", conn)

        cmd.CommandTimeout = conn.ConnectionTimeout
        cmd.CommandType = CommandType.StoredProcedure
        Select Case oTipoPesquisa
            Case TipoPesquisa.Equipamento
                cmd.Parameters.Add(New SqlParameter("@P_NUMEROSERIE", DirectCast(oParametros, String)))
                cmd.Parameters.Add(New SqlParameter("@P_FILIAL", ctlUtil.GetFilial))
                cmd.Parameters.Add(New SqlParameter("@P_LICENCIADO", HttpContext.Current.User.Identity.Name))
        End Select
        conn.Open()
        Return cmd.ExecuteReader(CommandBehavior.CloseConnection)
    End Function


    Public Function ListarAtendimento(ByVal oTipoPesquisa As TipoPesquisa, ByVal oParametros As Object) As SqlDataReader
        Dim conn = ctlUtil.GetConnection
        Dim cmd As New SqlCommand("PR_LISTAR_TODOS_ATENDIMENTOS", conn)

        cmd.CommandTimeout = conn.ConnectionTimeout
        cmd.CommandType = CommandType.StoredProcedure
        Select Case oTipoPesquisa
            Case TipoPesquisa.Equipamento
                cmd.Parameters.Add(New SqlParameter("@P_NUMEROSERIE", DirectCast(oParametros, String)))
                cmd.Parameters.Add(New SqlParameter("@P_FILIAL", ctlUtil.GetFilial))
        End Select
        conn.Open()
        Return cmd.ExecuteReader(CommandBehavior.CloseConnection)
    End Function


    Public Function ListarAtendimento(ByVal sNumSerie As String) As DataTable
        Dim conn = ctlUtil.GetConnection
        Dim dt As New DataTable
        Dim cmd As New SqlCommand("PR_LISTAR_TODOS_ATENDIMENTOS", conn)

        cmd.CommandTimeout = conn.ConnectionTimeout
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add(New SqlParameter("@P_NUMEROSERIE", sNumSerie))
        cmd.Parameters.Add(New SqlParameter("@P_FILIAL", ctlUtil.GetFilial))
        Dim da As New SqlDataAdapter(cmd)
        da.Fill(dt)
        Return dt
    End Function

    Public Function ListarItensAtendimento(ByVal sNumeroAtendimento As String) As DataTable
        Dim dt As New DataTable
        Dim conn = ctlUtil.GetConnection
        Dim cmd As New SqlCommand("PR_LISTAR_PECAS_ATENDIMENTO", conn)

        cmd.CommandTimeout = conn.ConnectionTimeout
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add(New SqlParameter("@P_NUMATENDIMENTO", sNumeroAtendimento))
        cmd.Parameters.Add(New SqlParameter("@P_FILIAL", ctlUtil.GetFilial))
        Dim da As New SqlDataAdapter(cmd)
        da.Fill(dt)
        Return dt
    End Function


    'Public Function ListarAtendimentosParaImpressao(sNumeroAtendimento As String) As DataTable
    '    Dim conn = ctlUtil.GetConnection
    '    Dim dt As New DataTable
    '    Dim cmd As New SqlCommand("PR_LISTAR_ATENDIMENTOS_PARA_IMPRESSAO", conn)

    '    cmd.CommandTimeout = conn.ConnectionTimeout
    '    cmd.CommandType = CommandType.StoredProcedure
    '    cmd.Parameters.Add(New SqlParameter("@P_NUMATENDIMENTO", sNumeroAtendimento))
    '    cmd.Parameters.Add(New SqlParameter("@P_FILIAL", ctlUtil.GetFilial))
    '    Dim da As New SqlDataAdapter(cmd)
    '    da.Fill(dt)
    '    Return dt
    'End Function

End Class
