Public Class ctlUsuario

    Dim sFilial As String = ""
    Dim sEmpresa As String = ""
    Dim sUserName As String = ""
    Dim sUserCode As String = ""
    Dim sUserFullName As String = ""
    Dim sDepartamento As String = ""
    Dim sMatricula As String = ""
    Dim sHash As String = ""
    Dim sFilPar As String = ""
    Public Perfis As New List(Of String)
    Public sRegioes As String = ""
    Public sGrupoUsuario As String = ""

    Public Sub New(ByVal oObjSiga As Object)
        sUserFullName = CStr(CallByName(oObjSiga, "Nome", CallType.Get))
        sUserCode = CStr(CallByName(oObjSiga, "UserId", CallType.Get))
        Dim oGrupos As String() = DirectCast(CallByName(oObjSiga, "Grupos", CallType.Get), String())
        For Each it As String In oGrupos
            Perfis.Add(it)
        Next
        sUserName = CStr(CallByName(oObjSiga, "UserName", CallType.Get))
        Dim oObj As Object = CallByName(oObjSiga, "Token", CallType.Get)
        sHash = CStr(CallByName(oObj, "Senha", CallType.Get))
        Dim oEmpresa As Object = CallByName(oObj, "EmpresaFilial", CallType.Get)
        sEmpresa = CStr(CallByName(oEmpresa, "Empresa", CallType.Get))
        sRegioes = CStr(CallByName(oObjSiga, "Regioes", CallType.Get))
        sFilial = CStr(CallByName(oEmpresa, "Filial", CallType.Get))
        sGrupoUsuario = CStr(CallByName(oObjSiga, "GrupoUsuario", CallType.Get))
    End Sub

    Sub New()
        ' TODO: Complete member initialization 
    End Sub

    Public Property Filial() As String
        Get
            Return sFilial
        End Get
        Set(ByVal value As String)
            sFilial = value
        End Set
    End Property

    Public Property FilPar() As String
        Get
            Return sFilPar
        End Get
        Set(ByVal value As String)
            sFilPar = value
        End Set
    End Property


    Public Property Empresa() As String
        Get
            Return sEmpresa
        End Get
        Set(ByVal value As String)
            sEmpresa = value
        End Set
    End Property

    Public Property UserCode() As String
        Get
            Return sUserCode
        End Get
        Set(ByVal value As String)
            sUserCode = value
        End Set
    End Property

    Public Property UserName() As String
        Get
            Return sUserName
        End Get
        Set(ByVal value As String)
            sUserName = value
        End Set
    End Property

    Public Property UserFullName() As String
        Get
            Return sUserFullName
        End Get
        Set(ByVal value As String)
            sUserFullName = value
        End Set
    End Property

    Public Property Departamento() As String
        Get
            Return sDepartamento
        End Get
        Set(ByVal value As String)
            sDepartamento = value
        End Set
    End Property

    Public Property Matricula() As String
        Get
            Return sMatricula
        End Get
        Set(ByVal value As String)
            sMatricula = value
        End Set
    End Property

    Public Property GrupoUsuario() As String
        Get
            Return sGrupoUsuario
        End Get
        Set(ByVal value As String)
            sGrupoUsuario = value
        End Set
    End Property

    Public ReadOnly Property Hash As String
        Get
            Return sHash
        End Get
    End Property

End Class
