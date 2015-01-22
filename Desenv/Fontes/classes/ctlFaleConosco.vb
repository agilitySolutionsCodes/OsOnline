Imports System.Data.SqlClient
Imports System.ComponentModel

<Serializable()> _
Public Class ctlFaleConosco

#Region "Propriedades"

    Public Property dataAbertura As Date
    Public Property nomeSolicitante As String
    Public Property assuntoChamado As String
    Public Property descricaoChamado As String
    Public Property status As String
    Public Property nomeAtendente As String
    Public Property dataFinalizaca As Date
    Public Property Anexos As New List(Of ctlAnexo)

#End Region

    Public Enum TipoPesquisa
        Todos
        <Description("Nr Chamado")> _
        NrChamado
        <Description("Por Licenciado")> _
        Licenciado
        <Description("Data Emissão")> _
        Emissao
    End Enum

    Public Function IncluirChamado(ByVal sLicenciado As String, ByVal sEmailLicenciado As String, ByVal sAssunto As String, ByVal sDescricao As String, ByVal sDataAbertura As String, ByVal sNomeLicenciado As String) As SqlDataReader

        Dim conn = ctlUtil.GetConnection
        Dim cmd As New SqlCommand("PR_INSERIR_CHAMADO_FALE_CONOSCO", conn)

        cmd.CommandTimeout = conn.ConnectionTimeout
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add(New SqlParameter("@P_FILIAL", ctlUtil.GetFilial))
        cmd.Parameters.Add(New SqlParameter("@P_LICENCIADO", sLicenciado))
        cmd.Parameters.Add(New SqlParameter("@P_EMAILLICENCIADO", sEmailLicenciado))
        cmd.Parameters.Add(New SqlParameter("@P_ASSUNTO", sAssunto))
        cmd.Parameters.Add(New SqlParameter("@P_DESCRICAO", sDescricao))
        cmd.Parameters.Add(New SqlParameter("@P_DATAABERTURA", sDataAbertura))
        cmd.Parameters.Add(New SqlParameter("@P_NOMELICENCIADO", sNomeLicenciado))
        conn.Open()
        Return cmd.ExecuteReader(CommandBehavior.CloseConnection)
    End Function

    Public Function ListarChamados(ByVal oTipoPesquisa As TipoPesquisa, ByVal oParametros As Object) As SqlDataReader
        Dim conn = ctlUtil.GetConnection
        Dim cmd As New SqlCommand("PR_LISTA_CHAMADOS_FALE_CONOSCO", conn)

        cmd.CommandTimeout = conn.ConnectionTimeout
        cmd.CommandType = CommandType.StoredProcedure

        Select Case oTipoPesquisa

            
            Case TipoPesquisa.Licenciado
                cmd.Parameters.Add(New SqlParameter("@P_LICENCIADO", DirectCast(oParametros, String)))
            Case TipoPesquisa.NrChamado
                cmd.Parameters.Add(New SqlParameter("@P_NUMEROCHAMADO", DirectCast(oParametros, String)))
            Case TipoPesquisa.Todos
                'Lista todos os chamados
            Case TipoPesquisa.Emissao
                cmd.Parameters.Add(New SqlParameter("@P_DATAEMISSAO", DirectCast(oParametros, Date)))
        End Select
        conn.Open()

        Return cmd.ExecuteReader(CommandBehavior.CloseConnection)
    End Function

End Class
