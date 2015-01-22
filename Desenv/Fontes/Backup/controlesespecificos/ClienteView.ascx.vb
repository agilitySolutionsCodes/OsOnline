Imports System.Data.SqlClient
Partial Public Class ClienteView
    Inherits System.Web.UI.UserControl

    Public Event SelecionarOnClick(ByVal oSelecao As Object)
    Public Event PaginarOnClick()
    Public Event CancelarOnClick()
    Public Event FecharOnClick()
    Public Event PesquisarOnClick()


    Public Property Codigo() As String
        Get
            Return txtCodigo.Text
        End Get
        Set(ByVal value As String)
            txtCodigo.Text = value
        End Set
    End Property

    Public Property Loja() As String
        Get
            Return txtLoja.Text
        End Get
        Set(ByVal value As String)
            txtLoja.Text = value
        End Set
    End Property

    Public Property CPF_CNPJ() As String
        Get
            Return txtCnpj.Text.Replace(".", "").Replace("/", "").Replace("-", "")
        End Get
        Set(ByVal value As String)
            txtCnpj.Text = value
        End Set
    End Property

    Public Property Nome() As String
        Get
            Return lblNome.Text
        End Get
        Set(ByVal value As String)
            lblNome.Text = value
        End Set
    End Property

    Public Property CssClass() As String
        Get
            Return txtCodigo.CssClass
        End Get
        Set(ByVal value As String)
            txtCodigo.CssClass = value
            txtLoja.CssClass = value
            lblNome.CssClass = value
            lblBarra.CssClass = value
        End Set
    End Property

    Public Property SomenteLeitura() As Boolean
        Get
            Return txtCnpj.ReadOnly
        End Get
        Set(ByVal value As Boolean)
            txtCodigo.ReadOnly = value
            txtLoja.ReadOnly = value
            txtCnpj.ReadOnly = value
            btnBusca.Enabled = Not value
        End Set
    End Property

    Public ReadOnly Property Text() As String
        Get
            Return txtCodigo.Text + txtLoja.Text
        End Get
    End Property

    Public Function GetArray() As String()
        Dim aRet As New List(Of String)
        aRet.Add(txtCodigo.Text)
        aRet.Add(txtLoja.Text)
        aRet.Add(txtCnpj.Text)
        Return aRet.ToArray()
    End Function

    Public Sub Clear()
        txtCodigo.Text = ""
        txtLoja.Text = ""
        txtCnpj.Text = ""
        lblNome.Text = ""
    End Sub

    Public Property Enabled() As Boolean
        Get
            Return txtCodigo.Enabled
        End Get
        Set(ByVal value As Boolean)
            txtCodigo.Enabled = value
            txtLoja.Enabled = value
            txtCnpj.Enabled = value
            lblNome.Enabled = value
            btnBusca.Visible = value
        End Set
    End Property

    Private Sub oConsulta_SelecionarOnClick(ByVal oSelecao As Object) Handles oDetalhe.SelecionarOnClick
        Dim dr As DataRow
        dr = DirectCast(oSelecao, DataRow)
        txtCodigo.Text = dr("CODIGO").ToString
        txtLoja.Text = dr("LOJA").ToString
        txtCnpj.Text = dr("CPFCNPJ").ToString
        lblNome.Text = dr("NOME").ToString
        Session.Add("CodigoCliente", txtCodigo.Text)
        updCabecalho.Update()
        oDetalhe.FecharJanela()
        lblFound.Text = (True).ToString
        imgNotFound.Visible = False 'Quando o cliente é localizado, não permite alterar o mesmo
        ExibirBusca = False
        txtCnpj.Enabled = False
        RaiseEvent SelecionarOnClick(oSelecao)
    End Sub

    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        txtCodigo.Attributes.Add("onchange", "javascript:fnClientViewPreenchido('" + txtCodigo.ClientID + "','" + txtLoja.ClientID + "', '" + txtCodigo.ClientID.Replace("_", "$") + "'); ")
        txtLoja.Attributes.Add("onchange", "javascript:fnClientViewPreenchido('" + txtCodigo.ClientID + "','" + txtLoja.ClientID + "', '" + txtLoja.ClientID.Replace("_", "$") + "'); ")
        txtCnpj.Attributes.Add("onchange", "javascript:fnClientViewCPFCNPJ('" + txtCnpj.ClientID + "', '" + txtCnpj.ClientID.Replace("_", "$") + "'); ")
        Session.Item("CodigoCliente") = txtCodigo.Text.Trim
        'If IsPostBack Then
        '    If TypeOf sender Is ClienteView Then
        '        Dim sCodigo As String = txtCodigo.Text.Trim
        '        Dim sLoja As String = txtLoja.Text.Trim
        '        Dim sCPF_CNPJ As String = txtCnpj.Text.Trim
        '        lblFound.Text = (False).ToString
        '        If sCodigo.Length > 0 And sLoja.Length > 0 AndAlso DirectCast(sender, ClienteView).TipoBuscaSelecionado = ctlCliente.TipoPesquisa.Código Then
        '            Selecionar(sCodigo, sLoja)
        '            Session.Add("CodigoCliente", sCodigo)
        '        ElseIf sCPF_CNPJ.Length > 0 And DirectCast(sender, ClienteView).TipoBuscaSelecionado = ctlCliente.TipoPesquisa.CPF_CNPJ Then
        '            Selecionar(sCPF_CNPJ)
        '            Session.Add("CodigoCliente", sCodigo)
        '        Else
        '            lblNome.Text = ""
        '            Session.Add("CodigoCliente", "")
        '        End If
        '        imgNotFound.Visible = Not Boolean.Parse(lblFound.Text)
        '        If imgNotFound.Visible Then
        '            lblNome.Text = ""
        '            Session.Add("CodigoCliente", "")
        '        End If
        '    End If
        'End If
    End Sub

    Private Function validarRegiaoCliente(ByVal dt As DataTable, ByVal regiaoUsuario As String) As Boolean
        Dim existeRegiao As Boolean = False
        Dim aRegiaoUsuario() As String = Split(regiaoUsuario, ",")
        If Not String.IsNullOrEmpty(regiaoUsuario) Then
            Dim regiaoCliente As String = dt(0)("Regiao").ToString
            For Each auxRegiaoUsuario In aRegiaoUsuario.ToArray
                If regiaoCliente.Contains(auxRegiaoUsuario.Trim) Then
                    existeRegiao = True
                    Exit For
                End If
            Next
        End If
        Return existeRegiao
    End Function

    Private Sub Pesquisar(ByVal sCodigo As String, ByVal sLoja As String)
        If sCodigo.Length > 0 And sLoja.Length > 0 Then
            Dim aParm As New List(Of String)
            aParm.Add(sCodigo)
            aParm.Add(sLoja)
            Dim cn As New ctlCliente
            Dim dt As DataTable
            dt = cn.Pesquisar(ctlCliente.TipoPesquisa.Código, aParm.ToArray)
            lblNome.Text = ""
            lblFound.Text = (False).ToString
            If dt.Rows.Count > 0 Then
                lblFound.Text = (True).ToString
                Dim dr As DataRow = dt.Rows(0)
                lblNome.Text = dr("Nome").ToString
                txtCodigo.Text = dr("CODIGO").ToString
                txtLoja.Text = dr("LOJA").ToString
                txtCnpj.Text = dr("CPFCPNJ").ToString
                Session.Add("CodigoCliente", txtCodigo.Text)
            End If
            imgNotFound.Visible = Not Boolean.Parse(lblFound.Text)
        End If
    End Sub

    Protected Sub btnBusca_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnBusca.Click
        oDetalhe.Exibir()
    End Sub

    Public ReadOnly Property IsValid() As Boolean
        Get
            If lblNome.Text.Trim.Length > 0 And txtCodigo.Text.Trim.Length > 0 Then 'Boolean.Parse(lblFound.Text) Then
                Return True
            Else
                Return False
            End If
        End Get
    End Property

    Public Property ExibirBusca() As Boolean
        Get
            Return btnBusca.Visible
        End Get
        Set(ByVal value As Boolean)
            btnBusca.Visible = value
        End Set
    End Property

    Public Property TipoBuscaSelecionado() As ctlCliente.TipoPesquisa
        Get
            If txtLoja.Visible Then
                Return ctlCliente.TipoPesquisa.Código
            Else
                Return ctlCliente.TipoPesquisa.CPF_CNPJ
            End If
        End Get
        Set(ByVal value As ctlCliente.TipoPesquisa)
            If value = ctlCliente.TipoPesquisa.Código Then
                txtCodigo.Visible = True
                txtLoja.Visible = True
                lblBarra.Visible = True
                txtCnpj.Visible = False
            ElseIf value = ctlCliente.TipoPesquisa.CPF_CNPJ Then
                txtCodigo.Visible = False
                txtLoja.Visible = False
                lblBarra.Visible = False
                txtCnpj.Visible = True
            End If
        End Set
    End Property

    Private Sub txtCnpj_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCnpj.TextChanged
        Dim sCNPJ As String = DirectCast(sender, TextBox).Text.Trim
        If sCNPJ.Length > 0 Then
            Dim cn As New ctlCliente
            Dim dt As DataTable
            dt = cn.Pesquisar(CType(ctlCliente.TipoPesquisa.CPF_CNPJ, ctlCliente.TipoPesquisa), sCNPJ.ToString)
            lblNome.Text = ""
            txtCodigo.Text = ""
            txtLoja.Text = ""
            lblFound.Text = (False).ToString
            Dim dr As DataRow
            If dt.Rows.Count > 0 Then
                dr = dt.Rows(0)
                lblFound.Text = (True).ToString
                lblNome.Text = dr("Nome").ToString
                txtCodigo.Text = dr("CODIGO").ToString
                txtLoja.Text = dr("LOJA").ToString
                If ExibirBusca Then
                    Session.Add("CodigoCliente", txtCodigo.Text)
                    imgNotFound.Visible = False 'Quando o cliente é localizado, não permite alterar o mesmo
                    ExibirBusca = False
                    txtCnpj.Enabled = False
                End If
            Else
                dr = dt.NewRow()
            End If
            imgNotFound.Visible = Not Boolean.Parse(lblFound.Text)
            RaiseEvent SelecionarOnClick(dr)
        End If
    End Sub

    'Private Sub Selecionar(ByVal sCPF_CNPJ As String)
    '    sCPF_CNPJ = sCPF_CNPJ.Replace(".", "").Replace("/", "").Replace("-", "").Trim
    '    If sCPF_CNPJ.Length >= 11 Then
    '        Dim cn As New ctlCliente
    '        Dim reader As SqlDataReader = cn.SelecionarCPFCNPJ(sCPF_CNPJ)
    '        Dim dt As New DataTable
    '        dt.Load(reader)
    '        reader.Close()
    '        lblNome.Text = ""
    '        lblFound.Text = (False).ToString
    '        Dim dr As DataRow = dt.NewRow
    '        If dt.Rows.Count > 0 Then
    '            Dim regiaoUsuario As String = TryCast(Session("Regiao"), String)
    '            If Not String.IsNullOrEmpty(regiaoUsuario) Then
    '                If validarRegiaoCliente(dt, regiaoUsuario) Then
    '                    lblFound.Text = (True).ToString
    '                    dr = dt.Rows(0)
    '                    lblNome.Text = dr("Nome").ToString
    '                    txtCodigo.Text = dr("CODIGO").ToString
    '                    txtLoja.Text = dr("LOJA").ToString
    '                    txtCnpj.Text = dr("CPFCNPJ").ToString
    '                    txtCnpj.Enabled = False
    '                    imgNotFound.ToolTip = ""
    '                    Session.Add("CodigoCliente", txtCodigo.Text)
    '                Else
    '                    imgNotFound.ToolTip = "O usuário atual não atende a mesma região que o cliente!"
    '                End If
    '            Else
    '                imgNotFound.ToolTip = "O usuário atual não possui nenhuma região cadastrada!"
    '            End If
    '        End If
    '    imgNotFound.Visible = Not Boolean.Parse(lblFound.Text)
    '    RaiseEvent SelecionarOnClick(dr)
    '    End If
    'End Sub

    'Public Enum TipoBusca
    '   Codigo
    '  CPF_CPNJ
    'End Enum

    'Private Sub Selecionar(ByVal sCodigo As String, ByVal sLoja As String)
    '    If sCodigo.Length > 0 And sLoja.Length > 0 Then
    '        Dim aParm As New List(Of String)
    '        aParm.Add(sCodigo)
    '        aParm.Add(sLoja)
    '        Dim cn As New ctlCliente
    '        Dim dt As DataTable
    '        dt = cn.Selecionar(aParm.ToArray)
    '        lblNome.Text = ""
    '        lblFound.Text = (False).ToString
    '        Dim dr As DataRow
    '        If dt.Rows.Count > 0 Then
    '            lblFound.Text = (True).ToString
    '            dr = dt.Rows(0)
    '            lblNome.Text = dr("Nome").ToString
    '            txtCodigo.Text = dr("CODIGO").ToString
    '            txtLoja.Text = dr("LOJA").ToString
    '            txtCnpj.Text = dr("CPFCNPJ").ToString
    '            Session.Add("CodigoCliente", txtCodigo.Text)
    '        Else
    '            dr = dt.NewRow
    '        End If
    '        imgNotFound.Visible = Not Boolean.Parse(lblFound.Text)
    '        RaiseEvent SelecionarOnClick(dr)
    '    End If
    'End Sub

End Class