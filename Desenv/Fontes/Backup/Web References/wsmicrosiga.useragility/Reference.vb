﻿'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:4.0.30319.296
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
'This source code was auto-generated by Microsoft.VSDesigner, Version 4.0.30319.296.
'
Namespace wsmicrosiga.useragility
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code"),  _
     System.Web.Services.WebServiceBindingAttribute(Name:="WSUSERAGILITYSOAP", [Namespace]:="http://10.230.2.42:91/")>  _
    Partial Public Class WSUSERAGILITY
        Inherits System.Web.Services.Protocols.SoapHttpClientProtocol
        
        Private AUTENTICAROperationCompleted As System.Threading.SendOrPostCallback
        
        Private useDefaultCredentialsSetExplicitly As Boolean
        
        '''<remarks/>
        Public Sub New()
            MyBase.New
            Me.Url = Global.OrdemServico.My.MySettings.Default.OrdemServico_wsmicrosiga_useragility_WSUSERAGILITY
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
        Public Event AUTENTICARCompleted As AUTENTICARCompletedEventHandler
        
        '''<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://10.230.2.42:91/AUTENTICAR", RequestNamespace:="http://10.230.2.42:91/", ResponseElementName:="AUTENTICARRESPONSE", ResponseNamespace:="http://10.230.2.42:91/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Function AUTENTICAR(ByVal TOKEN As TKNSTRUCT) As <System.Xml.Serialization.XmlElementAttribute("AUTENTICARRESULT")> VALIDASTRUCT
            Dim results() As Object = Me.Invoke("AUTENTICAR", New Object() {TOKEN})
            Return CType(results(0),VALIDASTRUCT)
        End Function
        
        '''<remarks/>
        Public Overloads Sub AUTENTICARAsync(ByVal TOKEN As TKNSTRUCT)
            Me.AUTENTICARAsync(TOKEN, Nothing)
        End Sub
        
        '''<remarks/>
        Public Overloads Sub AUTENTICARAsync(ByVal TOKEN As TKNSTRUCT, ByVal userState As Object)
            If (Me.AUTENTICAROperationCompleted Is Nothing) Then
                Me.AUTENTICAROperationCompleted = AddressOf Me.OnAUTENTICAROperationCompleted
            End If
            Me.InvokeAsync("AUTENTICAR", New Object() {TOKEN}, Me.AUTENTICAROperationCompleted, userState)
        End Sub
        
        Private Sub OnAUTENTICAROperationCompleted(ByVal arg As Object)
            If (Not (Me.AUTENTICARCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg,System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent AUTENTICARCompleted(Me, New AUTENTICARCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
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
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.233"),  _
     System.SerializableAttribute(),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code"),  _
     System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://10.230.2.42:91/")>  _
    Partial Public Class TKNSTRUCT
        
        Private cONTEUDOField As String
        
        Private eMPRESAFILIALField As EMPFILIALSTRUCT
        
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
        Public Property EMPRESAFILIAL() As EMPFILIALSTRUCT
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
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.233"),  _
     System.SerializableAttribute(),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code"),  _
     System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://10.230.2.42:91/")>  _
    Partial Public Class EMPFILIALSTRUCT
        
        Private eMPRESAField As String
        
        Private fILIALField As String
        
        Private fILPARField As String
        
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
        
        '''<remarks/>
        Public Property FILPAR() As String
            Get
                Return Me.fILPARField
            End Get
            Set
                Me.fILPARField = value
            End Set
        End Property
    End Class
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.233"),  _
     System.SerializableAttribute(),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code"),  _
     System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://10.230.2.42:91/")>  _
    Partial Public Class USUARIOSTRUCT
        
        Private fILIAISField() As EMPFILIALSTRUCT
        
        Private gRUPOSField() As String
        
        Private gRUPOUSUARIOField As String
        
        Private nOMEField As String
        
        Private rEGIOESField As String
        
        Private tOKENField As TKNSTRUCT
        
        Private uSERIDField As String
        
        Private uSERNAMEField As String
        
        '''<remarks/>
        <System.Xml.Serialization.XmlArrayItemAttribute(IsNullable:=false)>  _
        Public Property FILIAIS() As EMPFILIALSTRUCT()
            Get
                Return Me.fILIAISField
            End Get
            Set
                Me.fILIAISField = value
            End Set
        End Property
        
        '''<remarks/>
        <System.Xml.Serialization.XmlArrayItemAttribute("STRING", IsNullable:=false)>  _
        Public Property GRUPOS() As String()
            Get
                Return Me.gRUPOSField
            End Get
            Set
                Me.gRUPOSField = value
            End Set
        End Property
        
        '''<remarks/>
        Public Property GRUPOUSUARIO() As String
            Get
                Return Me.gRUPOUSUARIOField
            End Get
            Set
                Me.gRUPOUSUARIOField = value
            End Set
        End Property
        
        '''<remarks/>
        Public Property NOME() As String
            Get
                Return Me.nOMEField
            End Get
            Set
                Me.nOMEField = value
            End Set
        End Property
        
        '''<remarks/>
        Public Property REGIOES() As String
            Get
                Return Me.rEGIOESField
            End Get
            Set
                Me.rEGIOESField = value
            End Set
        End Property
        
        '''<remarks/>
        Public Property TOKEN() As TKNSTRUCT
            Get
                Return Me.tOKENField
            End Get
            Set
                Me.tOKENField = value
            End Set
        End Property
        
        '''<remarks/>
        Public Property USERID() As String
            Get
                Return Me.uSERIDField
            End Get
            Set
                Me.uSERIDField = value
            End Set
        End Property
        
        '''<remarks/>
        Public Property USERNAME() As String
            Get
                Return Me.uSERNAMEField
            End Get
            Set
                Me.uSERNAMEField = value
            End Set
        End Property
    End Class
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.233"),  _
     System.SerializableAttribute(),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code"),  _
     System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://10.230.2.42:91/")>  _
    Partial Public Class RETSTRUCT
        
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
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.233"),  _
     System.SerializableAttribute(),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code"),  _
     System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://10.230.2.42:91/")>  _
    Partial Public Class VALIDASTRUCT
        
        Private rETORNOField As RETSTRUCT
        
        Private uSUARIOField As USUARIOSTRUCT
        
        '''<remarks/>
        Public Property RETORNO() As RETSTRUCT
            Get
                Return Me.rETORNOField
            End Get
            Set
                Me.rETORNOField = value
            End Set
        End Property
        
        '''<remarks/>
        Public Property USUARIO() As USUARIOSTRUCT
            Get
                Return Me.uSUARIOField
            End Get
            Set
                Me.uSUARIOField = value
            End Set
        End Property
    End Class
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")>  _
    Public Delegate Sub AUTENTICARCompletedEventHandler(ByVal sender As Object, ByVal e As AUTENTICARCompletedEventArgs)
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code")>  _
    Partial Public Class AUTENTICARCompletedEventArgs
        Inherits System.ComponentModel.AsyncCompletedEventArgs
        
        Private results() As Object
        
        Friend Sub New(ByVal results() As Object, ByVal exception As System.Exception, ByVal cancelled As Boolean, ByVal userState As Object)
            MyBase.New(exception, cancelled, userState)
            Me.results = results
        End Sub
        
        '''<remarks/>
        Public ReadOnly Property Result() As VALIDASTRUCT
            Get
                Me.RaiseExceptionIfNecessary
                Return CType(Me.results(0),VALIDASTRUCT)
            End Get
        End Property
    End Class
End Namespace
