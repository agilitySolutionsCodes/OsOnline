#INCLUDE "APWEBSRV.CH"
#INCLUDE "PROTHEUS.CH"
/*Dummy*/
User Function WSUserAgility()      
oToken:=WSCLASSNEW("TokenStruct")    
oToken:Conteudo:="wsfalha"
oToken:Senha:="WSFALHA"
O:=WSUserAgility():Autenticar(oToken)

oToken:Conteudo:="AGT_13"
oToken:Senha:="020315"
O:=WSUserAgility():Autenticar(oToken)

//o2:=u_ValidaToken(o:RetAutenticacao:Usuario:Token)
Return Nil

WSService WSUserAgility DESCRIPTION "Serviços de segurança relacionados ao usuário Microsiga" 
WSDATA Token	   		AS TknStruct Optional
WSDATA RetAutenticacao  As ValidaStruct
WSMETHOD Autenticar		DESCRIPTION "Método de autenticação do usuário no Microsiga"
ENDWSSERVICE                   

WSSTRUCT ValidaStruct
	WSDATA Retorno			As RetStruct
	WSDATA Usuario			As UsuarioStruct Optional	
ENDWSSTRUCT

WSSTRUCT UsuarioStruct
	WSDATA UserId      AS String
	WSDATA UserName    AS String
	WSDATA Nome        AS String
	WSDATA Grupos      As Array Of String Optional                 
	WSDATA Filiais 	   As Array Of EmpFilialStruct Optional
	WSDATA Token	   AS TknStruct Optional
	WSDATA Regioes     AS String
ENDWSSTRUCT

/*/
±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±
±±ÚÄÄÄÄÄÄÄÄÄÄÂÄÄÄÄÄÄÄÄÄÄÂÄÄÄÄÄÄÄÂÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÂÄÄÄÄÄÄÂÄÄÄÄÄÄÄÄÄÄÄ¿±±
±±³Fun‡„o    ³Autenticar ³Autor  ³ Marcelo Piazza       ³ Data ³01.03.2010 ³±±
±±ÃÄÄÄÄÄÄÄÄÄÄÅÄÄÄÄÄÄÄÄÄÄÁÄÄÄÄÄÄÄÁÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÁÄÄÄÄÄÄÁÄÄÄÄÄÄÄÄÄÄÄ´±±
±±³          ³Método de autenticacao do usuário no Microsiga               ³±±
±±ÃÄÄÄÄÄÄÄÄÄÄÅÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄ´±±
±±³Parametros³Tone: username e senha  Microsiga                            ³±±
±±ÃÄÄÄÄÄÄÄÄÄÄÅÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄ´±±
±±³Retorno   ³aRet : Estrutura de autenticação no Microsiga                ³±±
±±ÀÄÄÄÄÄÄÄÄÄÄÁÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÙ±±
±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±
ßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßß
/*/
WSMETHOD Autenticar WSRECEIVE Token WSSEND RetAutenticacao WSSERVICE WSUserAgility
Local lRet:=.T.
Local aRet:={}
Local oRet:=WSClassNew("ValidaStruct")
Local oRetStru:=WSClassNew("RetStruct")
Local oRetUser:=WSClassNew("UsuarioStruct")
Local Token2:=Token

u_SetEnv(.T.)
aRet:=u_LoginAgt(Token:Conteudo, Token:Senha)
oRetStru:Sucesso:=aRet[1]
oRetStru:Codigo:=aRet[2]
oRetStru:Mensagem:=aRet[3]
oRetStru:Chave:=""
if oRetStru:Sucesso //autenticado com sucesso
	oRetUser:UserId:=aRet[4,1,1]
	oRetUser:UserName:=aRet[4,1,2]
	oRetUser:Nome:=aRet[4,1,4]
	oRetUser:Grupos:=aRet[4,1,10]
	oRetUser:Regioes:=aRet[5]
	Token:Senha:= PswEncript(dtos(date())+Token:Senha)
	Token:EmpresaFilial:=wsclassnew("EmpFilialStruct")
	if len(aRet[4,2,6])==1
		Token:EmpresaFilial:Empresa:=left(aRet[4,2,6,1], 2)
		Token:EmpresaFilial:Filial:=RIGHT(alltrim(aRet[4,2,6,1]), 2)
		Token:EmpresaFilial:FilPar:=alltrim(aRet[6])
	endif
	oRetUser:Token:=Token
	oRetStru:Chave:=oRetUser:UserId
else
	oRetUser:UserId := ""
	oRetUser:UserName := ""
	oRetUser:Nome := ""
	oRetUser:Regioes := ""
endif
oRet:Retorno:=oRetStru
oRet:Usuario:=oRetUser
::RetAutenticacao:=oRet
RpcClearEnv()
Return(lRet)

/* ===============================================================================
WSDL Location    http://localhost:8000/WSUSERAGILITY.apw?WSDL
Gerado em        09/19/11 13:41:29
Observações      Código-Fonte gerado por ADVPL WSDL Client 1.110217
                 Alterações neste arquivo podem causar funcionamento incorreto
                 e serão perdidas caso o código-fonte seja gerado novamente.
=============================================================================== */

/* -------------------------------------------------------------------------------
WSDL Service WSWSUSERAGILITY
------------------------------------------------------------------------------- */

WSCLIENT WSWSUSERAGILITY

	WSMETHOD NEW
	WSMETHOD INIT
	WSMETHOD RESET
	WSMETHOD CLONE
	WSMETHOD AUTENTICAR

	WSDATA   _URL                      AS String
	WSDATA   _HEADOUT                  AS Array of String
	WSDATA   oWSTOKEN                  AS WSUSERAGILITY_TKNSTRUCT
	WSDATA   oWSAUTENTICARRESULT       AS WSUSERAGILITY_VALIDASTRUCT

	// Estruturas mantidas por compatibilidade - NÃO USAR
	WSDATA   oWSTOKENSTRUCT            AS WSUSERAGILITY_TKNSTRUCT

ENDWSCLIENT

WSMETHOD NEW WSCLIENT WSWSUSERAGILITY
::Init()
If !FindFunction("XMLCHILDEX")
	UserException("O Código-Fonte Client atual requer os executáveis do Protheus Build [7.00.101202A-20110330] ou superior. Atualize o Protheus ou gere o Código-Fonte novamente utilizando o Build atual.")
EndIf
Return Self

WSMETHOD INIT WSCLIENT WSWSUSERAGILITY
	::oWSTOKEN           := WSUSERAGILITY_TKNSTRUCT():New()
	::oWSAUTENTICARRESULT := WSUSERAGILITY_VALIDASTRUCT():New()

	// Estruturas mantidas por compatibilidade - NÃO USAR
	::oWSTOKENSTRUCT     := ::oWSTOKEN
Return

WSMETHOD RESET WSCLIENT WSWSUSERAGILITY
	::oWSTOKEN           := NIL 
	::oWSAUTENTICARRESULT := NIL 

	// Estruturas mantidas por compatibilidade - NÃO USAR
	::oWSTOKENSTRUCT     := NIL
	::Init()
Return

WSMETHOD CLONE WSCLIENT WSWSUSERAGILITY
Local oClone := WSWSUSERAGILITY():New()
	oClone:_URL          := ::_URL 
	oClone:oWSTOKEN      :=  IIF(::oWSTOKEN = NIL , NIL ,::oWSTOKEN:Clone() )
	oClone:oWSAUTENTICARRESULT :=  IIF(::oWSAUTENTICARRESULT = NIL , NIL ,::oWSAUTENTICARRESULT:Clone() )

	// Estruturas mantidas por compatibilidade - NÃO USAR
	oClone:oWSTOKENSTRUCT := oClone:oWSTOKEN
Return oClone

// WSDL Method AUTENTICAR of Service WSWSUSERAGILITY

WSMETHOD AUTENTICAR WSSEND oWSTOKEN WSRECEIVE oWSAUTENTICARRESULT WSCLIENT WSWSUSERAGILITY
Local cSoap := "" , oXmlRet

BEGIN WSMETHOD

cSoap += '<AUTENTICAR xmlns="http://localhost:8000/">'
cSoap += WSSoapValue("TOKEN", ::oWSTOKEN, oWSTOKEN , "TOKENSTRUCT", .F. , .F., 0 , NIL, .F.) 
cSoap += "</AUTENTICAR>"

oXmlRet := SvcSoapCall(	Self,cSoap,; 
	"http://localhost:8000/AUTENTICAR",; 
	"DOCUMENT","http://localhost:8000/",,"1.031217",; 
	"http://localhost:8000/WSUSERAGILITY.apw")

::Init()
::oWSAUTENTICARRESULT:SoapRecv( WSAdvValue( oXmlRet,"_AUTENTICARRESPONSE:_AUTENTICARRESULT","VALIDASTRUCT",NIL,NIL,NIL,NIL,NIL,NIL) )

END WSMETHOD

oXmlRet := NIL
Return .T.


// WSDL Data Structure VALIDASTRUCT

WSSTRUCT WSUSERAGILITY_VALIDASTRUCT
	WSDATA   oWSRETORNO                AS WSUSERAGILITY_RETSTRUCT
	WSDATA   oWSUSUARIO                AS WSUSERAGILITY_USUARIOSTRUCT OPTIONAL
	WSMETHOD NEW
	WSMETHOD INIT
	WSMETHOD CLONE
	WSMETHOD SOAPRECV
ENDWSSTRUCT

WSMETHOD NEW WSCLIENT WSUSERAGILITY_VALIDASTRUCT
	::Init()
Return Self

WSMETHOD INIT WSCLIENT WSUSERAGILITY_VALIDASTRUCT
Return

WSMETHOD CLONE WSCLIENT WSUSERAGILITY_VALIDASTRUCT
	Local oClone := WSUSERAGILITY_VALIDASTRUCT():NEW()
	oClone:oWSRETORNO           := IIF(::oWSRETORNO = NIL , NIL , ::oWSRETORNO:Clone() )
	oClone:oWSUSUARIO           := IIF(::oWSUSUARIO = NIL , NIL , ::oWSUSUARIO:Clone() )
Return oClone

WSMETHOD SOAPRECV WSSEND oResponse WSCLIENT WSUSERAGILITY_VALIDASTRUCT
	Local oNode1
	Local oNode2
	::Init()
	If oResponse = NIL ; Return ; Endif 
	oNode1 :=  WSAdvValue( oResponse,"_RETORNO","RETSTRUCT",NIL,"Property oWSRETORNO as s0:RETSTRUCT on SOAP Response not found.",NIL,"O",NIL,NIL) 
	If oNode1 != NIL
		::oWSRETORNO := WSUSERAGILITY_RETSTRUCT():New()
		::oWSRETORNO:SoapRecv(oNode1)
	EndIf
	oNode2 :=  WSAdvValue( oResponse,"_USUARIO","USUARIOSTRUCT",NIL,NIL,NIL,"O",NIL,NIL) 
	If oNode2 != NIL
		::oWSUSUARIO := WSUSERAGILITY_USUARIOSTRUCT():New()
		::oWSUSUARIO:SoapRecv(oNode2)
	EndIf
Return

// WSDL Data Structure RETSTRUCT

WSSTRUCT WSUSERAGILITY_RETSTRUCT
	WSDATA   cCHAVE                    AS string
	WSDATA   nCODIGO                   AS float
	WSDATA   cMENSAGEM                 AS string
	WSDATA   lSUCESSO                  AS boolean
	WSMETHOD NEW
	WSMETHOD INIT
	WSMETHOD CLONE
	WSMETHOD SOAPRECV
ENDWSSTRUCT

WSMETHOD NEW WSCLIENT WSUSERAGILITY_RETSTRUCT
	::Init()
Return Self

WSMETHOD INIT WSCLIENT WSUSERAGILITY_RETSTRUCT
Return

WSMETHOD CLONE WSCLIENT WSUSERAGILITY_RETSTRUCT
	Local oClone := WSUSERAGILITY_RETSTRUCT():NEW()
	oClone:cCHAVE               := ::cCHAVE
	oClone:nCODIGO              := ::nCODIGO
	oClone:cMENSAGEM            := ::cMENSAGEM
	oClone:lSUCESSO             := ::lSUCESSO
Return oClone

WSMETHOD SOAPRECV WSSEND oResponse WSCLIENT WSUSERAGILITY_RETSTRUCT
	::Init()
	If oResponse = NIL ; Return ; Endif 
	::cCHAVE             :=  WSAdvValue( oResponse,"_CHAVE","string",NIL,"Property cCHAVE as s:string on SOAP Response not found.",NIL,"S",NIL,NIL) 
	::nCODIGO            :=  WSAdvValue( oResponse,"_CODIGO","float",NIL,"Property nCODIGO as s:float on SOAP Response not found.",NIL,"N",NIL,NIL) 
	::cMENSAGEM          :=  WSAdvValue( oResponse,"_MENSAGEM","string",NIL,"Property cMENSAGEM as s:string on SOAP Response not found.",NIL,"S",NIL,NIL) 
	::lSUCESSO           :=  WSAdvValue( oResponse,"_SUCESSO","boolean",NIL,"Property lSUCESSO as s:boolean on SOAP Response not found.",NIL,"L",NIL,NIL) 
Return

// WSDL Data Structure USUARIOSTRUCT

WSSTRUCT WSUSERAGILITY_USUARIOSTRUCT
	WSDATA   oWSFILIAIS                AS WSUSERAGILITY_ARRAYOFEmpFilialStruct OPTIONAL
	WSDATA   oWSGRUPOS                 AS WSUSERAGILITY_ARRAYOFSTRING OPTIONAL
	WSDATA   cNOME                     AS string
	WSDATA   cREGIOES                  AS string
	WSDATA   oWSTOKEN                  AS WSUSERAGILITY_TKNSTRUCT OPTIONAL
	WSDATA   cUSERID                   AS string
	WSDATA   cUSERNAME                 AS string
	WSMETHOD NEW
	WSMETHOD INIT
	WSMETHOD CLONE
	WSMETHOD SOAPRECV
ENDWSSTRUCT

WSMETHOD NEW WSCLIENT WSUSERAGILITY_USUARIOSTRUCT
	::Init()
Return Self

WSMETHOD INIT WSCLIENT WSUSERAGILITY_USUARIOSTRUCT
Return

WSMETHOD CLONE WSCLIENT WSUSERAGILITY_USUARIOSTRUCT
	Local oClone := WSUSERAGILITY_USUARIOSTRUCT():NEW()
	oClone:oWSFILIAIS           := IIF(::oWSFILIAIS = NIL , NIL , ::oWSFILIAIS:Clone() )
	oClone:oWSGRUPOS            := IIF(::oWSGRUPOS = NIL , NIL , ::oWSGRUPOS:Clone() )
	oClone:cNOME                := ::cNOME
	oClone:cREGIOES             := ::cREGIOES
	oClone:oWSTOKEN             := IIF(::oWSTOKEN = NIL , NIL , ::oWSTOKEN:Clone() )
	oClone:cUSERID              := ::cUSERID
	oClone:cUSERNAME            := ::cUSERNAME
Return oClone

WSMETHOD SOAPRECV WSSEND oResponse WSCLIENT WSUSERAGILITY_USUARIOSTRUCT
	Local oNode1
	Local oNode2
	Local oNode5
	::Init()
	If oResponse = NIL ; Return ; Endif 
	oNode1 :=  WSAdvValue( oResponse,"_FILIAIS","ARRAYOFEmpFilialStruct",NIL,NIL,NIL,"O",NIL,NIL) 
	If oNode1 != NIL
		::oWSFILIAIS := WSUSERAGILITY_ARRAYOFEmpFilialStruct():New()
		::oWSFILIAIS:SoapRecv(oNode1)
	EndIf
	oNode2 :=  WSAdvValue( oResponse,"_GRUPOS","ARRAYOFSTRING",NIL,NIL,NIL,"O",NIL,NIL) 
	If oNode2 != NIL
		::oWSGRUPOS := WSUSERAGILITY_ARRAYOFSTRING():New()
		::oWSGRUPOS:SoapRecv(oNode2)
	EndIf
	::cNOME              :=  WSAdvValue( oResponse,"_NOME","string",NIL,"Property cNOME as s:string on SOAP Response not found.",NIL,"S",NIL,NIL) 
	::cREGIOES           :=  WSAdvValue( oResponse,"_REGIOES","string",NIL,"Property cREGIOES as s:string on SOAP Response not found.",NIL,"S",NIL,NIL) 
	oNode5 :=  WSAdvValue( oResponse,"_TOKEN","TOKENSTRUCT",NIL,NIL,NIL,"O",NIL,NIL) 
	If oNode5 != NIL
		::oWSTOKEN := WSUSERAGILITY_TKNSTRUCT():New()
		::oWSTOKEN:SoapRecv(oNode5)
	EndIf
	::cUSERID            :=  WSAdvValue( oResponse,"_USERID","string",NIL,"Property cUSERID as s:string on SOAP Response not found.",NIL,"S",NIL,NIL) 
	::cUSERNAME          :=  WSAdvValue( oResponse,"_USERNAME","string",NIL,"Property cUSERNAME as s:string on SOAP Response not found.",NIL,"S",NIL,NIL) 
Return

// WSDL Data Structure ARRAYOFEmpFilialStruct

WSSTRUCT WSUSERAGILITY_ARRAYOFEmpFilialStruct
	WSDATA   oWSEmpFilialStruct    AS WSUSERAGILITY_EmpFilialStruct OPTIONAL
	WSMETHOD NEW
	WSMETHOD INIT
	WSMETHOD CLONE
	WSMETHOD SOAPRECV
ENDWSSTRUCT

WSMETHOD NEW WSCLIENT WSUSERAGILITY_ARRAYOFEmpFilialStruct
	::Init()
Return Self

WSMETHOD INIT WSCLIENT WSUSERAGILITY_ARRAYOFEmpFilialStruct
	::oWSEmpFilialStruct := {} // Array Of  WSUSERAGILITY_EmpFilialStruct():New()
Return

WSMETHOD CLONE WSCLIENT WSUSERAGILITY_ARRAYOFEmpFilialStruct
	Local oClone := WSUSERAGILITY_ARRAYOFEmpFilialStruct():NEW()
	oClone:oWSEmpFilialStruct := NIL
	If ::oWSEmpFilialStruct <> NIL 
		oClone:oWSEmpFilialStruct := {}
		aEval( ::oWSEmpFilialStruct , { |x| aadd( oClone:oWSEmpFilialStruct , x:Clone() ) } )
	Endif 
Return oClone

WSMETHOD SOAPRECV WSSEND oResponse WSCLIENT WSUSERAGILITY_ARRAYOFEmpFilialStruct
	Local nRElem1, oNodes1, nTElem1
	::Init()
	If oResponse = NIL ; Return ; Endif 
	oNodes1 :=  WSAdvValue( oResponse,"_EmpFilialStruct","EmpFilialStruct",{},NIL,.T.,"O",NIL,NIL) 
	nTElem1 := len(oNodes1)
	For nRElem1 := 1 to nTElem1 
		If !WSIsNilNode( oNodes1[nRElem1] )
			aadd(::oWSEmpFilialStruct , WSUSERAGILITY_EmpFilialStruct():New() )
			::oWSEmpFilialStruct[len(::oWSEmpFilialStruct)]:SoapRecv(oNodes1[nRElem1])
		Endif
	Next
Return

// WSDL Data Structure ARRAYOFSTRING

WSSTRUCT WSUSERAGILITY_ARRAYOFSTRING
	WSDATA   cSTRING                   AS string OPTIONAL
	WSMETHOD NEW
	WSMETHOD INIT
	WSMETHOD CLONE
	WSMETHOD SOAPRECV
ENDWSSTRUCT

WSMETHOD NEW WSCLIENT WSUSERAGILITY_ARRAYOFSTRING
	::Init()
Return Self

WSMETHOD INIT WSCLIENT WSUSERAGILITY_ARRAYOFSTRING
	::cSTRING              := {} // Array Of  ""
Return

WSMETHOD CLONE WSCLIENT WSUSERAGILITY_ARRAYOFSTRING
	Local oClone := WSUSERAGILITY_ARRAYOFSTRING():NEW()
	oClone:cSTRING              := IIf(::cSTRING <> NIL , aClone(::cSTRING) , NIL )
Return oClone

WSMETHOD SOAPRECV WSSEND oResponse WSCLIENT WSUSERAGILITY_ARRAYOFSTRING
	Local oNodes1 :=  WSAdvValue( oResponse,"_STRING","string",{},NIL,.T.,"S",NIL,"a") 
	::Init()
	If oResponse = NIL ; Return ; Endif 
	aEval(oNodes1 , { |x| aadd(::cSTRING ,  x:TEXT  ) } )
Return

// WSDL Data Structure TOKENSTRUCT

WSSTRUCT WSUSERAGILITY_TKNSTRUCT
	WSDATA   cCONTEUDO                 AS string
	WSDATA   oWSEMPRESAFILIAL          AS WSUSERAGILITY_EmpFilialStruct OPTIONAL
	WSDATA   cMENSAGEM                 AS string OPTIONAL
	WSDATA   cMODULO                   AS string OPTIONAL
	WSDATA   cSENHA                    AS string OPTIONAL
	WSDATA   cSESSIONID                AS string OPTIONAL
	WSMETHOD NEW
	WSMETHOD INIT
	WSMETHOD CLONE
	WSMETHOD SOAPSEND
	WSMETHOD SOAPRECV
ENDWSSTRUCT

WSMETHOD NEW WSCLIENT WSUSERAGILITY_TKNSTRUCT
	::Init()
Return Self

WSMETHOD INIT WSCLIENT WSUSERAGILITY_TKNSTRUCT
Return

WSMETHOD CLONE WSCLIENT WSUSERAGILITY_TKNSTRUCT
	Local oClone := WSUSERAGILITY_TKNSTRUCT():NEW()
	oClone:cCONTEUDO            := ::cCONTEUDO
	oClone:oWSEMPRESAFILIAL     := IIF(::oWSEMPRESAFILIAL = NIL , NIL , ::oWSEMPRESAFILIAL:Clone() )
	oClone:cMENSAGEM            := ::cMENSAGEM
	oClone:cMODULO              := ::cMODULO
	oClone:cSENHA               := ::cSENHA
	oClone:cSESSIONID           := ::cSESSIONID
Return oClone

WSMETHOD SOAPSEND WSCLIENT WSUSERAGILITY_TKNSTRUCT
	Local cSoap := ""
	cSoap += WSSoapValue("CONTEUDO", ::cCONTEUDO, ::cCONTEUDO , "string", .T. , .F., 0 , NIL, .F.) 
	cSoap += WSSoapValue("EMPRESAFILIAL", ::oWSEMPRESAFILIAL, ::oWSEMPRESAFILIAL , "EmpFilialStruct", .F. , .F., 0 , NIL, .F.) 
	cSoap += WSSoapValue("MENSAGEM", ::cMENSAGEM, ::cMENSAGEM , "string", .F. , .F., 0 , NIL, .F.) 
	cSoap += WSSoapValue("MODULO", ::cMODULO, ::cMODULO , "string", .F. , .F., 0 , NIL, .F.) 
	cSoap += WSSoapValue("SENHA", ::cSENHA, ::cSENHA , "string", .F. , .F., 0 , NIL, .F.) 
	cSoap += WSSoapValue("SESSIONID", ::cSESSIONID, ::cSESSIONID , "string", .F. , .F., 0 , NIL, .F.) 
Return cSoap

WSMETHOD SOAPRECV WSSEND oResponse WSCLIENT WSUSERAGILITY_TKNSTRUCT
	Local oNode2
	::Init()
	If oResponse = NIL ; Return ; Endif 
	::cCONTEUDO          :=  WSAdvValue( oResponse,"_CONTEUDO","string",NIL,"Property cCONTEUDO as s:string on SOAP Response not found.",NIL,"S",NIL,NIL) 
	oNode2 :=  WSAdvValue( oResponse,"_EMPRESAFILIAL","EmpFilialStruct",NIL,NIL,NIL,"O",NIL,NIL) 
	If oNode2 != NIL
		::oWSEMPRESAFILIAL := WSUSERAGILITY_EmpFilialStruct():New()
		::oWSEMPRESAFILIAL:SoapRecv(oNode2)
	EndIf
	::cMENSAGEM          :=  WSAdvValue( oResponse,"_MENSAGEM","string",NIL,NIL,NIL,"S",NIL,NIL) 
	::cMODULO            :=  WSAdvValue( oResponse,"_MODULO","string",NIL,NIL,NIL,"S",NIL,NIL) 
	::cSENHA             :=  WSAdvValue( oResponse,"_SENHA","string",NIL,NIL,NIL,"S",NIL,NIL) 
	::cSESSIONID         :=  WSAdvValue( oResponse,"_SESSIONID","string",NIL,NIL,NIL,"S",NIL,NIL) 
Return

// WSDL Data Structure EmpFilialStruct

WSSTRUCT WSUSERAGILITY_EmpFilialStruct
	WSDATA   cEMPRESA                  AS string OPTIONAL
	WSDATA   cFILIAL                   AS string OPTIONAL
	WSDATA   cFILPAR                   AS string OPTIONAL
	WSMETHOD NEW
	WSMETHOD INIT
	WSMETHOD CLONE
	WSMETHOD SOAPSEND
	WSMETHOD SOAPRECV
ENDWSSTRUCT

WSMETHOD NEW WSCLIENT WSUSERAGILITY_EmpFilialStruct
	::Init()
Return Self

WSMETHOD INIT WSCLIENT WSUSERAGILITY_EmpFilialStruct
Return

WSMETHOD CLONE WSCLIENT WSUSERAGILITY_EmpFilialStruct
	Local oClone := WSUSERAGILITY_EmpFilialStruct():NEW()
	oClone:cEMPRESA             := ::cEMPRESA
	oClone:cFILIAL              := ::cFILIAL
	oClone:cFILPAR              := ::cFILPAR
Return oClone

WSMETHOD SOAPSEND WSCLIENT WSUSERAGILITY_EmpFilialStruct
	Local cSoap := ""
	cSoap += WSSoapValue("EMPRESA", ::cEMPRESA, ::cEMPRESA , "string", .F. , .F., 0 , NIL, .F.) 
	cSoap += WSSoapValue("FILIAL", ::cFILIAL, ::cFILIAL , "string", .F. , .F., 0 , NIL, .F.) 
	cSoap += WSSoapValue("FILPAR", ::cFILPAR, ::cFILPAR , "string", .F. , .F., 0 , NIL, .F.) 
Return cSoap

WSMETHOD SOAPRECV WSSEND oResponse WSCLIENT WSUSERAGILITY_EmpFilialStruct
	::Init()
	If oResponse = NIL ; Return ; Endif 
	::cEMPRESA           :=  WSAdvValue( oResponse,"_EMPRESA","string",NIL,NIL,NIL,"S",NIL,NIL) 
	::cFILIAL            :=  WSAdvValue( oResponse,"_FILIAL","string",NIL,NIL,NIL,"S",NIL,NIL) 
	::cFILPAR            :=  WSAdvValue( oResponse,"_FILPAR","string",NIL,NIL,NIL,"S",NIL,NIL) 
Return