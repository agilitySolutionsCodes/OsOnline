Imports System.Data.SqlClient
Imports System.ComponentModel

Public Class ctlCliente

    Sub New()
        ' TODO: Complete member initialization 
    End Sub

    Public Property Codigo As String
    Public Property Nome As String
    Public Property CNPJ As String
    Public Property Loja As String

    Public Sub New(ByVal sCodigo As String, ByVal sLoja As String, ByVal sNome As String, ByVal sCNPJ As String)
        Codigo = sCodigo
        Loja = sLoja
        Nome = sNome
        CNPJ = sCNPJ
    End Sub

    Public Enum TipoPesquisa
        [Código]
        [Nome]
        [Apelido]
        <Description("CPF/CNPJ")> _
        CPF_CNPJ
    End Enum

    Public Function Pesquisar(ByVal oTipo As TipoPesquisa, ByVal oParametros As Object) As DataTable
        Dim conn = ctlUtil.GetConnection
        Dim cmd As New SqlCommand("PR_LISTAR_CLIENTE_OS_02", conn)

        cmd.CommandTimeout = conn.ConnectionTimeout
        cmd.CommandType = CommandType.StoredProcedure

        Dim cn As New ctlUsuario()
        Dim dt As New DataTable
        Select Case oTipo
            Case TipoPesquisa.Código
                Dim codigo As String
                If IsArray(oParametros) Then
                    Dim aParametros As String()
                    aParametros = DirectCast(oParametros, String())
                    codigo = aParametros(0).ToString
                Else
                    codigo = oParametros.ToString
                End If
                cmd.Parameters.Add(New SqlParameter("@P_CODIGO", codigo))
            Case TipoPesquisa.Nome
                cmd.Parameters.Add(New SqlParameter("@P_NOME", DirectCast(oParametros, String)))
            Case TipoPesquisa.Apelido
                cmd.Parameters.Add(New SqlParameter("@P_APELIDO", DirectCast(oParametros, String)))
            Case ctlCliente.TipoPesquisa.CPF_CNPJ
                cmd.Parameters.Add(New SqlParameter("@P_CPFCPNJ", DirectCast(oParametros.ToString().Replace(".", "").Replace("-", "").Replace("/", ""), String)))
        End Select
        cmd.Parameters.Add(New SqlParameter("@P_REGIAO", HttpContext.Current.Session("Regiao").ToString))
        cmd.Parameters.Add(New SqlParameter("@P_GRUPO", HttpContext.Current.Session("Grupo").ToString))
        cmd.Parameters.Add(New SqlParameter("@P_FILIAL", ctlUtil.GetFilial))
        Dim da As New SqlDataAdapter(cmd)
        da.Fill(dt)
        Return dt
    End Function

    Public Function ListarTiposPesquisa() As Array
        Return System.Enum.GetValues(GetType(TipoPesquisa))
    End Function

End Class
