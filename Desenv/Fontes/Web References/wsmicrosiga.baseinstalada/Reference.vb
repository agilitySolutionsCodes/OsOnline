﻿'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:4.0.30319.17626
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict Off
Option Explicit On

Imports System
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Xml.Serialization

'
'This source code was auto-generated by Microsoft.VSDesigner, Version 4.0.30319.17626.
'
Namespace wsmicrosiga.baseinstalada
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17626"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code"),  _
     System.Web.Services.WebServiceBindingAttribute(Name:="WSBASEINSTALADASOAP", [Namespace]:="http://192.168.44.42:8000/")>  _
    Partial Public Class WSBASEINSTALADA
        Inherits System.Web.Services.Protocols.SoapHttpClientProtocol
        
        Private ALTERAROperationCompleted As System.Threading.SendOrPostCallback
        
        Private INCLUIROperationCompleted As System.Threading.SendOrPostCallback
        
        Private TRANSFERIROperationCompleted As System.Threading.SendOrPostCallback
        
        Private useDefaultCredentialsSetExplicitly As Boolean
        
        '''<remarks/>
        Public Sub New()
            MyBase.New
            Me.Url = Global.OrdemServico.My.MySettings.Default.OrdemServico_wsmicrosigaD_baseinstalada_WSBASEINSTALADA
            If (Me.IsLocalFileSystemWebService(Me.Url) = true) Then
                Me.UseDefaultCredentials = true
                Me.useDefaultCredentialsSetExplicitly = false
            Else
                Me.useDefaultCredentialsSetExplicitly = true
            End If
        End Sub
        
        Public Shadows Property Url() As String
            Get
                Return MyBase.Url
            End Get
            Set
                If (((Me.IsLocalFileSystemWebService(MyBase.Url) = true)  _
                            AndAlso (Me.useDefaultCredentialsSetExplicitly = false))  _
                            AndAlso (Me.IsLocalFileSystemWebService(value) = false)) Then
                    MyBase.UseDefaultCredentials = false
                End If
                MyBase.Url = value
            End Set
        End Property
        
        Public Shadows Property UseDefaultCredentials() As Boolean
            Get
                Return MyBase.UseDefaultCredentials
            End Get
            Set
                MyBase.UseDefaultCredentials = value
                Me.useDefaultCredentialsSetExplicitly = true
            End Set
        End Property
        
        '''<remarks/>
        Public Event ALTERARCompleted As ALTERARCompletedEventHandler
        
        '''<remarks/>
        Public Event INCLUIRCompleted As INCLUIRCompletedEventHandler
        
        '''<remarks/>
        Public Event TRANSFERIRCompleted As TRANSFERIRCompletedEventHandler
        
        '''<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://192.168.44.42:8000/ALTERAR", RequestNamespace:="http://192.168.44.42:8000/", ResponseElementName:="ALTERARRESPONSE", ResponseNamespace:="http://192.168.44.42:8000/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Function ALTERAR(ByVal TOKEN As TOKENSTRUCT, ByVal FICHABASEINSTALADA As FICHABASEINSTALADASTRUCT) As <System.Xml.Serialization.XmlElementAttribute("ALTERARRESULT")> RETORNOSTRUCT
            Dim results() As Object = Me.Invoke("ALTERAR", New Object() {TOKEN, FICHABASEINSTALADA})
            Return CType(results(0),RETORNOSTRUCT)
        End Function
        
        '''<remarks/>
        Public Overloads Sub ALTERARAsync(ByVal TOKEN As TOKENSTRUCT, ByVal FICHABASEINSTALADA As FICHABASEINSTALADASTRUCT)
            Me.ALTERARAsync(TOKEN, FICHABASEINSTALADA, Nothing)
        End Sub
        
        '''<remarks/>
        Public Overloads Sub ALTERARAsync(ByVal TOKEN As TOKENSTRUCT, ByVal FICHABASEINSTALADA As FICHABASEINSTALADASTRUCT, ByVal userState As Object)
            If (Me.ALTERAROperationCompleted Is Nothing) Then
                Me.ALTERAROperationCompleted = AddressOf Me.OnALTERAROperationCompleted
            End If
            Me.InvokeAsync("ALTERAR", New Object() {TOKEN, FICHABASEINSTALADA}, Me.ALTERAROperationCompleted, userState)
        End Sub
        
        Private Sub OnALTERAROperationCompleted(ByVal arg As Object)
            If (Not (Me.ALTERARCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg,System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent ALTERARCompleted(Me, New ALTERARCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub
        
        '''<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://192.168.44.42:8000/INCLUIR", RequestNamespace:="http://192.168.44.42:8000/", ResponseElementName:="INCLUIRRESPONSE", ResponseNamespace:="http://192.168.44.42:8000/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Function INCLUIR(ByVal TOKEN As TOKENSTRUCT, ByVal FICHABASEINSTALADA As FICHABASEINSTALADASTRUCT) As <System.Xml.Serialization.XmlElementAttribute("INCLUIRRESULT")> RETORNOSTRUCT
            Dim results() As Object = Me.Invoke("INCLUIR", New Object() {TOKEN, FICHABASEINSTALADA})
            Return CType(results(0),RETORNOSTRUCT)
        End Function
        
        '''<remarks/>
        Public Overloads Sub INCLUIRAsync(ByVal TOKEN As TOKENSTRUCT, ByVal FICHABASEINSTALADA As FICHABASEINSTALADASTRUCT)
            Me.INCLUIRAsync(TOKEN, FICHABASEINSTALADA, Nothing)
        End Sub
        
        '''<remarks/>
        Public Overloads Sub INCLUIRAsync(ByVal TOKEN As TOKENSTRUCT, ByVal FICHABASEINSTALADA As FICHABASEINSTALADASTRUCT, ByVal userState As Object)
            If (Me.INCLUIROperationCompleted Is Nothing) Then
                Me.INCLUIROperationCompleted = AddressOf Me.OnINCLUIROperationCompleted
            End If
            Me.InvokeAsync("INCLUIR", New Object() {TOKEN, FICHABASEINSTALADA}, Me.INCLUIROperationCompleted, userState)
        End Sub
        
        Private Sub OnINCLUIROperationCompleted(ByVal arg As Object)
            If (Not (Me.INCLUIRCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg,System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent INCLUIRCompleted(Me, New INCLUIRCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub
        
        '''<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://192.168.44.42:8000/TRANSFERIR", RequestNamespace:="http://192.168.44.42:8000/", ResponseElementName:="TRANSFERIRRESPONSE", ResponseNamespace:="http://192.168.44.42:8000/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Function TRANSFERIR(ByVal TOKEN As TOKENSTRUCT, ByVal SOLICITACAOTRANSFERENCIA As SOLICITACAOTRANSFERENCIASTRUCT) As <System.Xml.Serialization.XmlElementAttribute("TRANSFERIRRESULT")> RETORNOSTRUCT
            Dim results() As Object = Me.Invoke("TRANSFERIR", New Object() {TOKEN, SOLICITACAOTRANSFERENCIA})
            Return CType(results(0),RETORNOSTRUCT)
        End Function
        
        '''<remarks/>
        Public Overloads Sub TRANSFERIRAsync(ByVal TOKEN As TOKENSTRUCT, ByVal SOLICITACAOTRANSFERENCIA As SOLICITACAOTRANSFERENCIASTRUCT)
            Me.TRANSFERIRAsync(TOKEN, SOLICITACAOTRANSFERENCIA, Nothing)
        End Sub
        
        '''<remarks/>
        Public Overloads Sub TRANSFERIRAsync(ByVal TOKEN As TOKENSTRUCT, ByVal SOLICITACAOTRANSFERENCIA As SOLICITACAOTRANSFERENCIASTRUCT, ByVal userState As Object)
            If (Me.TRANSFERIROperationCompleted Is Nothing) Then
                Me.TRANSFERIROperationCompleted = AddressOf Me.OnTRANSFERIROperationCompleted
            End If
            Me.InvokeAsync("TRANSFERIR", New Object() {TOKEN, SOLICITACAOTRANSFERENCIA}, Me.TRANSFERIROperationCompleted, userState)
        End Sub
        
        Private Sub OnTRANSFERIROperationCompleted(ByVal arg As Object)
            If (Not (Me.TRANSFERIRCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg,System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent TRANSFERIRCompleted(Me, New TRANSFERIRCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub
        
        '''<remarks/>
        Public Shadows Sub CancelAsync(ByVal userState As Object)
            MyBase.CancelAsync(userState)
        End Sub
        
        Private Function IsLocalFileSystemWebService(ByVal url As String) As Boolean
            If ((url Is Nothing)  _
                        OrElse (url Is String.Empty)) Then
                Return false
            End If
            Dim wsUri As System.Uri = New System.Uri(url)
            If ((wsUri.Port >= 1024)  _
                        AndAlso (String.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) = 0)) Then
                Return true
            End If
            Return false
        End Function
    End Class
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.17626"),  _
     System.SerializableAttribute(),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code"),  _
     System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://192.168.44.42:8000/")>  _
    Partial Public Class TOKENSTRUCT
        
        Private cONTEUDOField As String
        
        Private eMPRESAFILIALField As EMPRESAFILIALSTRUCT
        
        Private mENSAGEMField As String
        
        Private mODULOField As String
        
        Private sENHAField As String
        
        Private sESSIONIDField As String
        
        '''<remarks/>
        Public Property CONTEUDO() As String
            Get
                Return Me.cONTEUDOField
            End Get
            Set
                Me.cONTEUDOField = value
            End Set
        End Property
        
        '''<remarks/>
        Public Property EMPRESAFILIAL() As EMPRESAFILIALSTRUCT
            Get
                Return Me.eMPRESAFILIALField
            End Get
            Set
                Me.eMPRESAFILIALField = value
            End Set
        End Property
        
        '''<remarks/>
        Public Property MENSAGEM() As String
            Get
                Return Me.mENSAGEMField
            End Get
            Set
                Me.mENSAGEMField = value
            End Set
        End Property
        
        '''<remarks/>
        Public Property MODULO() As String
            Get
                Return Me.mODULOField
            End Get
            Set
                Me.mODULOField = value
            End Set
        End Property
        
        '''<remarks/>
        Public Property SENHA() As String
            Get
                Return Me.sENHAField
            End Get
            Set
                Me.sENHAField = value
            End Set
        End Property
        
        '''<remarks/>
        Public Property SESSIONID() As String
            Get
                Return Me.sESSIONIDField
            End Get
            Set
                Me.sESSIONIDField = value
            End Set
        End Property
    End Class
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.17626"),  _
     System.SerializableAttribute(),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code"),  _
     System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://192.168.44.42:8000/")>  _
    Partial Public Class EMPRESAFILIALSTRUCT
        
        Private eMPRESAField As String
        
        Private fILIALField As String
        
        '''<remarks/>
        Public Property EMPRESA() As String
            Get
                Return Me.eMPRESAField
            End Get
            Set
                Me.eMPRESAField = value
            End Set
        End Property
        
        '''<remarks/>
        Public Property FILIAL() As String
            Get
                Return Me.fILIALField
            End Get
            Set
                Me.fILIALField = value
            End Set
        End Property
    End Class
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.17626"),  _
     System.SerializableAttribute(),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code"),  _
     System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://192.168.44.42:8000/")>  _
    Partial Public Class SOLICITACAOTRANSFERENCIASTRUCT
        
        Private cNPJANTERIORField As String
        
        Private cNPJNOVOField As String
        
        Private cODIGOPRODUTOField As String
        
        Private mAILUSUARIOField As String
        
        Private mENSAGEMAPROVADORField As String
        
        Private oBSERVACAOField As String
        
        Private sERIEITEMField As String
        
        '''<remarks/>
        Public Property CNPJANTERIOR() As String
            Get
                Return Me.cNPJANTERIORField
            End Get
            Set
                Me.cNPJANTERIORField = value
            End Set
        End Property
        
        '''<remarks/>
        Public Property CNPJNOVO() As String
            Get
                Return Me.cNPJNOVOField
            End Get
            Set
                Me.cNPJNOVOField = value
            End Set
        End Property
        
        '''<remarks/>
        Public Property CODIGOPRODUTO() As String
            Get
                Return Me.cODIGOPRODUTOField
            End Get
            Set
                Me.cODIGOPRODUTOField = value
            End Set
        End Property
        
        '''<remarks/>
        Public Property MAILUSUARIO() As String
            Get
                Return Me.mAILUSUARIOField
            End Get
            Set
                Me.mAILUSUARIOField = value
            End Set
        End Property
        
        '''<remarks/>
        Public Property MENSAGEMAPROVADOR() As String
            Get
                Return Me.mENSAGEMAPROVADORField
            End Get
            Set
                Me.mENSAGEMAPROVADORField = value
            End Set
        End Property
        
        '''<remarks/>
        Public Property OBSERVACAO() As String
            Get
                Return Me.oBSERVACAOField
            End Get
            Set
                Me.oBSERVACAOField = value
            End Set
        End Property
        
        '''<remarks/>
        Public Property SERIEITEM() As String
            Get
                Return Me.sERIEITEMField
            End Get
            Set
                Me.sERIEITEMField = value
            End Set
        End Property
    End Class
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.17626"),  _
     System.SerializableAttribute(),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code"),  _
     System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://192.168.44.42:8000/")>  _
    Partial Public Class RETORNOSTRUCT
        
        Private cHAVEField As String
        
        Private cODIGOField As Single
        
        Private mENSAGEMField As String
        
        Private sUCESSOField As Boolean
        
        '''<remarks/>
        Public Property CHAVE() As String
            Get
                Return Me.cHAVEField
            End Get
            Set
                Me.cHAVEField = value
            End Set
        End Property
        
        '''<remarks/>
        Public Property CODIGO() As Single
            Get
                Return Me.cODIGOField
            End Get
            Set
                Me.cODIGOField = value
            End Set
        End Property
        
        '''<remarks/>
        Public Property MENSAGEM() As String
            Get
                Return Me.mENSAGEMField
            End Get
            Set
                Me.mENSAGEMField = value
            End Set
        End Property
        
        '''<remarks/>
        Public Property SUCESSO() As Boolean
            Get
                Return Me.sUCESSOField
            End Get
            Set
                Me.sUCESSOField = value
            End Set
        End Property
    End Class
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.17626"),  _
     System.SerializableAttribute(),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code"),  _
     System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://192.168.44.42:8000/")>  _
    Partial Public Class ACESSORIOBASEINSTALADASTRUCT
        
        Private cODIGOField As String
        
        Private lOTEField As String
        
        Private sERIEField As String
        
        '''<remarks/>
        Public Property CODIGO() As String
            Get
                Return Me.cODIGOField
            End Get
            Set
                Me.cODIGOField = value
            End Set
        End Property
        
        '''<remarks/>
        Public Property LOTE() As String
            Get
                Return Me.lOTEField
            End Get
            Set
                Me.lOTEField = value
            End Set
        End Property
        
        '''<remarks/>
        Public Property SERIE() As String
            Get
                Return Me.sERIEField
            End Get
            Set
                Me.sERIEField = value
            End Set
        End Property
    End Class
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.17626"),  _
     System.SerializableAttribute(),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code"),  _
     System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://192.168.44.42:8000/")>  _
    Partial Public Class FICHABASEINSTALADASTRUCT
        
        Private aCESSORIOSField() As ACESSORIOBASEINSTALADASTRUCT
        
        Private cNPJCLIENTEField As String
        
        Private cODIGOPRODUTOField As String
        
        Private cODIGOTECNICOField As String
        
        Private cONTATOField As String
        
        Private eNDERECOField As String
        
        Private iTEMPEDIDOField As String
        
        Private lOCALINSTALACAOField As String
        
        Private lOTEField As String
        
        Private mAILUSUARIOField As String
        
        Private mENSAGEMAPROVADORField As String
        
        Private nOTAFISCALField As String
        
        Private pEDIDOField As String
        
        Private sERIEField As String
        
        Private sERIENOTAField As String
        
        Private sTATUSEQUIPAMENTOField As String
        
        Private tELEFONEField As String
        
        '''<remarks/>
        <System.Xml.Serialization.XmlArrayItemAttribute(IsNullable:=false)>  _
        Public Property ACESSORIOS() As ACESSORIOBASEINSTALADASTRUCT()
            Get
                Return Me.aCESSORIOSField
            End Get
            Set
                Me.aCESSORIOSField = value
            End Set
        End Property
        
        '''<remarks/>
        Public Property CNPJCLIENTE() As String
            Get
                Return Me.cNPJCLIENTEField
            End Get
            Set
                Me.cNPJCLIENTEField = value
            End Set
        End Property
        
        '''<remarks/>
        Public Property CODIGOPRODUTO() As String
            Get
                Return Me.cODIGOPRODUTOField
            End Get
            Set
                Me.cODIGOPRODUTOField = value
            End Set
        End Property
        
        '''<remarks/>
        Public Property CODIGOTECNICO() As String
            Get
                Return Me.cODIGOTECNICOField
            End Get
            Set
                Me.cODIGOTECNICOField = value
            End Set
        End Property
        
        '''<remarks/>
        Public Property CONTATO() As String
            Get
                Return Me.cONTATOField
            End Get
            Set
                Me.cONTATOField = value
            End Set
        End Property
        
        '''<remarks/>
        Public Property ENDERECO() As String
            Get
                Return Me.eNDERECOField
            End Get
            Set
                Me.eNDERECOField = value
            End Set
        End Property
        
        '''<remarks/>
        Public Property ITEMPEDIDO() As String
            Get
                Return Me.iTEMPEDIDOField
            End Get
            Set
                Me.iTEMPEDIDOField = value
            End Set
        End Property
        
        '''<remarks/>
        Public Property LOCALINSTALACAO() As String
            Get
                Return Me.lOCALINSTALACAOField
            End Get
            Set
                Me.lOCALINSTALACAOField = value
            End Set
        End Property
        
        '''<remarks/>
        Public Property LOTE() As String
            Get
                Return Me.lOTEField
            End Get
            Set
                Me.lOTEField = value
            End Set
        End Property
        
        '''<remarks/>
        Public Property MAILUSUARIO() As String
            Get
                Return Me.mAILUSUARIOField
            End Get
            Set
                Me.mAILUSUARIOField = value
            End Set
        End Property
        
        '''<remarks/>
        Public Property MENSAGEMAPROVADOR() As String
            Get
                Return Me.mENSAGEMAPROVADORField
            End Get
            Set
                Me.mENSAGEMAPROVADORField = value
            End Set
        End Property
        
        '''<remarks/>
        Public Property NOTAFISCAL() As String
            Get
                Return Me.nOTAFISCALField
            End Get
            Set
                Me.nOTAFISCALField = value
            End Set
        End Property
        
        '''<remarks/>
        Public Property PEDIDO() As String
            Get
                Return Me.pEDIDOField
            End Get
            Set
                Me.pEDIDOField = value
            End Set
        End Property
        
        '''<remarks/>
        Public Property SERIE() As String
            Get
                Return Me.sERIEField
            End Get
            Set
                Me.sERIEField = value
            End Set
        End Property
        
        '''<remarks/>
        Public Property SERIENOTA() As String
            Get
                Return Me.sERIENOTAField
            End Get
            Set
                Me.sERIENOTAField = value
            End Set
        End Property
        
        '''<remarks/>
        Public Property STATUSEQUIPAMENTO() As String
            Get
                Return Me.sTATUSEQUIPAMENTOField
            End Get
            Set
                Me.sTATUSEQUIPAMENTOField = value
            End Set
        End Property
        
        '''<remarks/>
        Public Property TELEFONE() As String
            Get
                Return Me.tELEFONEField
            End Get
            Set
                Me.tELEFONEField = value
            End Set
        End Property
    End Class
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17626")>  _
    Public Delegate Sub ALTERARCompletedEventHandler(ByVal sender As Object, ByVal e As ALTERARCompletedEventArgs)
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17626"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code")>  _
    Partial Public Class ALTERARCompletedEventArgs
        Inherits System.ComponentModel.AsyncCompletedEventArgs
        
        Private results() As Object
        
        Friend Sub New(ByVal results() As Object, ByVal exception As System.Exception, ByVal cancelled As Boolean, ByVal userState As Object)
            MyBase.New(exception, cancelled, userState)
            Me.results = results
        End Sub
        
        '''<remarks/>
        Public ReadOnly Property Result() As RETORNOSTRUCT
            Get
                Me.RaiseExceptionIfNecessary
                Return CType(Me.results(0),RETORNOSTRUCT)
            End Get
        End Property
    End Class
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17626")>  _
    Public Delegate Sub INCLUIRCompletedEventHandler(ByVal sender As Object, ByVal e As INCLUIRCompletedEventArgs)
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17626"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code")>  _
    Partial Public Class INCLUIRCompletedEventArgs
        Inherits System.ComponentModel.AsyncCompletedEventArgs
        
        Private results() As Object
        
        Friend Sub New(ByVal results() As Object, ByVal exception As System.Exception, ByVal cancelled As Boolean, ByVal userState As Object)
            MyBase.New(exception, cancelled, userState)
            Me.results = results
        End Sub
        
        '''<remarks/>
        Public ReadOnly Property Result() As RETORNOSTRUCT
            Get
                Me.RaiseExceptionIfNecessary
                Return CType(Me.results(0),RETORNOSTRUCT)
            End Get
        End Property
    End Class
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17626")>  _
    Public Delegate Sub TRANSFERIRCompletedEventHandler(ByVal sender As Object, ByVal e As TRANSFERIRCompletedEventArgs)
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17626"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code")>  _
    Partial Public Class TRANSFERIRCompletedEventArgs
        Inherits System.ComponentModel.AsyncCompletedEventArgs
        
        Private results() As Object
        
        Friend Sub New(ByVal results() As Object, ByVal exception As System.Exception, ByVal cancelled As Boolean, ByVal userState As Object)
            MyBase.New(exception, cancelled, userState)
            Me.results = results
        End Sub
        
        '''<remarks/>
        Public ReadOnly Property Result() As RETORNOSTRUCT
            Get
                Me.RaiseExceptionIfNecessary
                Return CType(Me.results(0),RETORNOSTRUCT)
            End Get
        End Property
    End Class
End Namespace
