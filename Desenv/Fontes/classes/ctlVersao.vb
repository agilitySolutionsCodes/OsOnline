Imports System.Data.SqlClient

Public Class ctlVersao

    Public Function Pesquisar() As DataTable

        Dim dt As New DataTable
        Dim conn = ctlUtil.GetConnection

        Dim cmd As New SqlCommand("PR_LISTAR_ARQUIVOS", conn)

        cmd.CommandTimeout = conn.ConnectionTimeout
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add(New SqlParameter("@P_LICENCIADO", HttpContext.Current.User.Identity.Name))

        Dim da As New SqlDataAdapter(cmd)
        da.Fill(dt)
        Return dt

    End Function

    Public Function InserirDownloadLicenciado(ByVal sDocumento As String, ByVal sRevisao As String) As SqlDataReader

        Dim conn = ctlUtil.GetConnection
        Dim cmd As New SqlCommand("PR_INSERIR_DOWNLOAD", conn)

        cmd.CommandTimeout = conn.ConnectionTimeout
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add(New SqlParameter("@P_LICENCIADO", HttpContext.Current.User.Identity.Name))
        cmd.Parameters.Add(New SqlParameter("@P_DOCUMENTO", sDocumento))
        cmd.Parameters.Add(New SqlParameter("@P_REVISAO", sRevisao))

        conn.Open()
        Return cmd.ExecuteReader(CommandBehavior.CloseConnection)
    End Function

End Class
