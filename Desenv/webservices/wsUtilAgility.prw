#include "protheus.ch"
#include "tbiconn.ch"      
#INCLUDE "APWEBSRV.CH"
#INCLUDE "TOPCONN.CH"

User Function WSUTILAGILITY() 
/*
O:=u_login('REV1','REVENDA')
O:=u_login('AGT_13','020315')
O:=u_login('wsfalha','wsfalha') 
*/

O:=u_login('LIC1','LIC001')
sEmp:=left(o[4,2,6,1], 2)
sFil:=RIGHT(alltrim(o[4,2,6,1]), 2)

Token:=wsclassnew("TokenStruct")
Token:Conteudo:="LIC1"
//Token:Senha   := PswEncript((DTOS(DATE())+"LIC001"), 0)
Token:SessionId:=""
Token:Mensagem:=""
Token:EmpresaFilial = wsclassnew("EmpFilialStruct")
Token:EmpresaFilial:Empresa:=sEmp
Token:EmpresaFilial:Filial:=sFil
Token:Modulo:=""
n:=u_ValidaToken(Token, .F.)

/*
EMPRESAFILIAL O=[4,2,6,N]   //SE FOR IGUAL = '@@@@' ENTAO VALE PARA TODOS
*/                                                                                   
Return Nil

WSService WSUTILAGILITY DESCRIPTION "Funcoes Utilitarias do Microsiga"
	WSDATA Token                 As TknStruct
	WSDATA Retorno			     As String
	WSDATA NomeParametro		 As String
	WSMETHOD WebGetMV	DESCRIPTION "Executa a Funcao GetMV"
ENDWSSERVICE

WSSTRUCT TknStruct
	WSDATA Conteudo   As String
	WSDATA Senha      As String Optional
	WSDATA SessionId  As String Optional
	WSDATA Mensagem   As String Optional
	WSDATA EmpresaFilial  As EmpFilialStruct Optional
	WSDATA Modulo     As String Optional
ENDWSSTRUCT

WSSTRUCT RetStruct
	WSDATA Sucesso   As Boolean
	WSDATA Codigo    As Float
	WSDATA Mensagem  As String 
	WSDATA Chave     As String
ENDWSSTRUCT

WSSTRUCT EmpFilialStruct
	WSDATA Empresa As String Optional
	WSDATA Filial As String Optional
	WSDATA FilPar As String Optional
ENDWSSTRUCT


/*/
±±±±±±±±±±±±±±±±±±±±±±±G±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±
±±ÚÄÄÄÄÄÄÄÄÄÄÂÄÄÄÄÄÄÄÄÄÄÂÄÄÄÄÄÄÄÂÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÂÄÄÄÄÄÄÂÄÄÄÄÄÄÄÄÄÄÄ¿±±
±±³Fun‡„o    ³getMV ³Autor  ³ Marcelo Piazza       ³ Data ³05.07.2010 ³±±
±±ÃÄÄÄÄÄÄÄÄÄÄÅÄÄÄÄÄÄÄÄÄÄÁÄÄÄÄÄÄÄÁÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÁÄÄÄÄÄÄÁÄÄÄÄÄÄÄÄÄÄÄ´±±
±±³          ³Retorna o conteudo do parametro                              ³±±
±±ÃÄÄÄÄÄÄÄÄÄÄÅÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄ´±±
±±³Parametros³Token: token de segurançac                                   ³±±
±±³          ³Apontamento: estrutura do apontamento                        ³±±
±±ÃÄÄÄÄÄÄÄÄÄÄÅÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄ´±±
±±³Retorno   ³aRet : Estrutura de autenticação no Microsiga                ³±±
±±ÀÄÄÄÄÄÄÄÄÄÄÁÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÙ±±
±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±
ßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßß
/*/
WSMETHOD WebGetMV WSRECEIVE Token, NomeParametro WSSEND Retorno WSSERVICE WSUTILAGILITY
Local lRet:=.T.   
Local oVar:=Nil
	lRet:=u_ValidaToken(Token)
	//lRet:=u_SetEnv(.T.)
	if lRet
		 oVar:= GetMV(NomeParametro)
		 if valtype(oVar)=="L"
		 	::Retorno:=transform(oVar, "@!")
		 elseif valtype(oVar)=="N"
		 	::Retorno:=transform(oVar, "@!")
		 elseif valtype(oVar)=="D"
		 	::Retorno:=DTOS(oVar)
		 else
		 	::Retorno:=transform(oVar, "@!")
		 endif
	Endif   
	RpcClearEnv()
Return lRet
                                  	
/**/
User Function SetEnv(lWF, lUsaMaster)
Local cEmpWS:= GetSrvProfString( "EMPWS","",.T.) 
Local cFilWS:= GetSrvProfString( "FILWS","",.T.) 
Local cUsuWS:= GetSrvProfString( "USUWS","",.T.) 
Local cPswWS:= GetSrvProfString( "PSWWS","",.T.) 
Local lRet:=.T.
Default lWF:=.T.          
Default lUsaMaster:=.F.
RpcSetType(3)  
if lWF==.F.
	if lUsaMaster
		lRet:=RpcSetEnv(cEmpWS, cFilWS, cUsuWS, cPswWS)
	else
		lRet:=RpcSetEnv(cEmpWS, cFilWS)
	endif
else
	lRet:=WFPREPENV(cEmpWS, cFilWS)    
	//lRet:=RpcSetEnv(cEmpWS, cFilWS)
Endif
return lRet
       
/**/
User Function ValidaToken(oToken, lWF)
Local lRet:=.F.   
Local cModWS:=""
Local cEmpWS:= GetSrvProfString( "EMPWS","",.T.) 
Local cFilWS:= GetSrvProfString( "FILWS","",.T.) 
Local cUsuWS:= GetSrvProfString( "USUWS","",.T.) 
Local cPswWS:= GetSrvProfString( "PSWWS","",.T.) 
Local cUser := oToken:Conteudo
Local lUsaToken:=.F.
Default lWF:=.T.
if oToken:Senha!=Nil
	cUsuWS:= oToken:Conteudo 
    cPswWS:=PswEncript(oToken:Senha, 1) 
    lUsaToken:=.T.
endif 
if oToken:EmpresaFilial!=Nil
   	cEmpWS:=oToken:EmpresaFilial:Empresa
    cFilWS:=oToken:EmpresaFilial:Filial 
endif
if oToken:Modulo=Nil
    cModWS:=oToken:Modulo    
endif

if lUsaToken .And. left(cPswWS, 10) != DTOS(date())
	lRet:=.F.
else
	if lUsaToken
		cPswWS:=substr(cPswWS, 09) 
	endif
	RpcSetType(3)               	
	if lWF==.F.                     
		if cModWS==""
			lRet:=RpcSetEnv(cEmpWS, cFilWS, cUsuWS, cPswWS)
		else 
			lRet:=RpcSetEnv(cEmpWS, cFilWS, cUsuWS, cPswWS, cModWS)
		endif
	else
		lRet:=WFPREPENV(cEmpWS, cFilWS)
	Endif
	
	if lRet .AND. len(alltrim(cUsuario)) > 0
		if !lUsaToken
			cUserName:=PADR(cUser, 15)    
			cUsuario:=REPLACE(cUsuario, PADR(cUsuWS, 15), cUserName)
			PswOrder(2)
			if PswSeek(cUserName) 		
				__cUserID:=PswId()      
				lRet:=.T.
			else
				lRet:=.F.
			endif
		endif
	elseif lRet
		cUserName:=PADR(cUser, 15)		
		cUsuario:="******"+cUserName + "SSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSS"
		PswOrder(2)
		if PswSeek(cUserName) 		
			__cUserID:=PswId()      
			lRet:=.T.
		else
			lRet:=.F.
		endif
	else 
		lRet:=.F.
	endif  
endif
	/*
PswEncript(cStrEncrypt,nType) -> cStrEncrypt || cStrDecrypt
Str2Array(cSPFDet,lEncrypt) -> aSPFDet
Array2Str(aSPFDet,lEncrypt) -> cSPFDet
PswOrder(nPswOrder)
PswSeek(cPswKey) -> lFound
PswRet(nType) -> aPswDet
*/         

Return lRet

/*User Function TryUse(xContent, xVar)
If ValType(xContent) !="U"
	xVar:=xContent
endif
Return Nil
  */
/*
Usuario Existe?
Senha Confere?
*/
User Function LoginAgt(cUsu, cSen)
Local aRet:={}
Local nErro:=99                    
Local cDesc:="Erro não identificado."
Local lRet:=.F.
Local aUsuario:={}
PswOrder(2)
If PswSeek(cUsu) //usuario existe
	If  PswName(cSen)
		aUsuario:=PswRet()
		if len(aUsuario)==0
			nErro:=4
			cDesc:="Acesso negado."
		elseif aUsuario[1,17]
			nErro:=3
			cDesc:="Acesso negado."
		else  
			lRet:=.T. 
			nErro:=0
			cDesc:="Usuário localizado."			
		endif
	Else
		nErro:=2
		cDesc:="Senha inválida."
	Endif
Else
	nErro:=1
	cDesc:="Usuário inexistente."
Endif
aadd(aRet, lRet)
aadd(aRet, nErro)
aadd(aRet, cDesc)
aadd(aRet, aUsuario)
aadd(aRet, "")
aadd(aRet, "")

If lRet
   
	cSQL:=" SELECT AI3_REGIAO, AI3_FILPAR FROM " + RETSQLTAB("AI3") + " WITH (NOLOCK)  WHERE " + RETSQLCOND("AI3") + " AND AI3_USRSIS = '"+alltrim(aRet[4,1,1] )+"'"
	aRegiao:=""
	TCQUERY cSQL NEW ALIAS "TRB" 
	if !TRB->(EOF())
		aRet[5] := TRB->AI3_REGIAO
		aRet[6] := TRB->AI3_FILPAR
	ENDIF
	TRB->(DBCLOSEAREA())
	
End If

Return aRet

/*/
±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±
±±ÚÄÄÄÄÄÄÄÄÄÄÂÄÄÄÄÄÄÄÄÄÄÂÄÄÄÄÄÄÄÂÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÂÄÄÄÄÄÄÂÄÄÄÄÄÄÄÄÄÄÄ¿±±
±±³Fun‡„o    ³ WEBMAIL             ³Autor  ³ Marcelo Piazza³ Data ³08.2010 ³±±
±±ÃÄÄÄÄÄÄÄÄÄÄÅÄÄÄÄÄÄÄÄÄÄÁÄÄÄÄÄÄÄÁÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÁÄÄÄÄÄÄÁÄÄÄÄÄÄÄÄÄÄÄ´±±
±±³          ³ Envio de email                                              ³±±
±±ÃÄÄÄÄÄÄÄÄÄÄÅÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄ´±±
±±³Parametros³                                                             ³±±
±±³          ³                                                             ³±±
±±ÀÄÄÄÄÄÄÄÄÄÄÁÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÙ±±
±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±
ßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßß
/*/
User Function WEBMAILAGILITY(cSubject, cFrom, cTo, cCC, cBody, cFromName)
Local lUsaSMTP:=.T.
Local cServer:=getmv("MV_RELSERV")//getmv("MV_WFSMTP")
Local cAcnt:=getmv("MV_WFMAIL")  //getmv("MV_RELACNT")
Local cPSW:=getmv("MV_WFPASSW")  //getmv("MV_RELPSW") 
Default cFromName:="Loja Virtual Intermed"
cFrom:=cFromName+"<"+alltrim(cAcnt)+">"
if lUsaSMTP

	connect smtp server cServer account cAcnt password cPSW result lok
	if lOk
		if ! mailauth( cAcnt, cPSW )
			get mail error cErrorMsg
			help("",1,"NAO AUTENTICOU!",,"ERRO: " + cErrorMsg,2,0)
			disconnect smtp server result lOk
			if !lOk
				get mail error cErrorMsg
				help("",1,"ERRO AO DESCONECTAR",,"ERRO: " + cErrorMsg, 2, 0)
			endif
			return .f.
		endif  
		if valType(cCC) = "A"
			cDest:=""
			if len(cCC)==1
				cDest:=cCC[1]
			else
				aEval( cCC , { |X| cDest:=cDest+alltrim(X)+";" } )										
			endif
			send mail from cFrom to cTo bcc  cDest  subject cSubject body cBody format text result lOk		
		elseif ! empty(cCC)
			send mail from cFrom to cTo bcc  cCC  subject cSubject body cBody format text result lOk
		else
			//send mail from cFrom to cTo bcc cBCC subject cSubject body cBody format text result lOk
			send mail from cFrom to cTo subject cSubject body cBody format text result lOk
		endif
		
		if ! lOk
			get mail error cErrorMsg
			help("", 1, "ERRO AO ENVIAR",,"ERRO: " + cErrorMsg, 2, 0)
		endif
	else
		get mail error cErrorMsg
		help("", 1, "NAO CONECTOU!",,"ERRO: " + cErrorMsg, 2, 0)
	endif
	
	disconnect smtp server
Else
	cFileName:="\LOJAVIRTUAL\HTML\TEMPLATE_CLIENTE.HTML"
	oProcess:=TWFProcess():New("000100") //, "Loja Virtual" )
	oProcess:cBody:=cBody
	oProcess:cSubject := cSubject
	oProcess:cTo      := cTo
	oProcess:cFromName:= cFromName
	o1:=oProcess:NewTask( cSubject, cFileName, .f. )
	oProcess:oHTML:cBuffer:=cBody
	o2:=oProcess:Start()
	o3:=WFSendMail( { cEmpAnt, cFilAnt } )
	o4:=oProcess:Finish()
Endif
Return
/* ===============================================================================
WSDL Location    http://localhost:8000/WSUTILAGILITY.apw?WSDL
Gerado em        09/19/11 13:43:13
Observações      Código-Fonte gerado por ADVPL WSDL Client 1.110217
                 Alterações neste arquivo podem causar funcionamento incorreto
                 e serão perdidas caso o código-fonte seja gerado novamente.
=============================================================================== */

/* -------------------------------------------------------------------------------
WSDL Service WSWSUTILAGILITY
------------------------------------------------------------------------------- */

WSCLIENT WSWSUTILAGILITY

	WSMETHOD NEW
	WSMETHOD INIT
	WSMETHOD RESET
	WSMETHOD CLONE
	WSMETHOD WEBGETMV

	WSDATA   _URL                      AS String
	WSDATA   _HEADOUT                  AS Array of String
	WSDATA   oWSTOKEN                  AS WSUTILAGILITY_TKNSTRUCT //TOKEN
	WSDATA   cNOMEPARAMETRO            AS string
	WSDATA   cWEBGETMVRESULT           AS string

	// Estruturas mantidas por compatibilidade - NÃO USAR
	WSDATA   oWSTKNSTRUCT            AS WSUTILAGILITY_TKNSTRUCT

ENDWSCLIENT

WSMETHOD NEW WSCLIENT WSWSUTILAGILITY
::Init()
If !FindFunction("XMLCHILDEX")
	UserException("O Código-Fonte Client atual requer os executáveis do Protheus Build [7.00.101202A-20110330] ou superior. Atualize o Protheus ou gere o Código-Fonte novamente utilizando o Build atual.")
EndIf
Return Self

WSMETHOD INIT WSCLIENT WSWSUTILAGILITY
	::oWSTOKEN           := WSUTILAGILITY_TKNSTRUCT():New()

	// Estruturas mantidas por compatibilidade - NÃO USAR
	::oWSTKNSTRUCT     := ::oWSTOKEN
Return

WSMETHOD RESET WSCLIENT WSWSUTILAGILITY
	::oWSTOKEN           := NIL 
	::cNOMEPARAMETRO     := NIL 
	::cWEBGETMVRESULT    := NIL 

	// Estruturas mantidas por compatibilidade - NÃO USAR
	::oWSTKNSTRUCT     := NIL
	::Init()
Return

WSMETHOD CLONE WSCLIENT WSWSUTILAGILITY
Local oClone := WSWSUTILAGILITY():New()
	oClone:_URL          := ::_URL 
	oClone:oWSTOKEN      :=  IIF(::oWSTOKEN = NIL , NIL ,::oWSTOKEN:Clone() )
	oClone:cNOMEPARAMETRO := ::cNOMEPARAMETRO
	oClone:cWEBGETMVRESULT := ::cWEBGETMVRESULT

	// Estruturas mantidas por compatibilidade - NÃO USAR
	oClone:oWSTKNSTRUCT := oClone:oWSTOKEN
Return oClone

// WSDL Method WEBGETMV of Service WSWSUTILAGILITY

WSMETHOD WEBGETMV WSSEND oWSTOKEN,cNOMEPARAMETRO WSRECEIVE cWEBGETMVRESULT WSCLIENT WSWSUTILAGILITY
Local cSoap := "" , oXmlRet

BEGIN WSMETHOD

cSoap += '<WEBGETMV xmlns="http://localhost:8000/">'
cSoap += WSSoapValue("TOKEN", ::oWSTOKEN, oWSTOKEN , "TKNSTRUCT", .T. , .F., 0 , NIL, .F.) 
cSoap += WSSoapValue("NOMEPARAMETRO", ::cNOMEPARAMETRO, cNOMEPARAMETRO , "string", .T. , .F., 0 , NIL, .F.) 
cSoap += "</WEBGETMV>"

oXmlRet := SvcSoapCall(	Self,cSoap,; 
	"http://localhost:8000/WEBGETMV",; 
	"DOCUMENT","http://localhost:8000/",,"1.031217",; 
	"http://localhost:8000/WSUTILAGILITY.apw")

::Init()
::cWEBGETMVRESULT    :=  WSAdvValue( oXmlRet,"_WEBGETMVRESPONSE:_WEBGETMVRESULT:TEXT","string",NIL,NIL,NIL,NIL,NIL,NIL) 

END WSMETHOD

oXmlRet := NIL
Return .T.


// WSDL Data Structure TKNSTRUCT

WSSTRUCT WSUTILAGILITY_TKNSTRUCT
	WSDATA   cCONTEUDO                 AS string
	WSDATA   oWSEMPRESAFILIAL          AS WSUTILAGILITY_EmpFilialStruct OPTIONAL
	WSDATA   cMENSAGEM                 AS string OPTIONAL
	WSDATA   cMODULO                   AS string OPTIONAL
	WSDATA   cSENHA                    AS string OPTIONAL
	WSDATA   cSESSIONID                AS string OPTIONAL
	WSMETHOD NEW
	WSMETHOD INIT
	WSMETHOD CLONE
	WSMETHOD SOAPSEND
ENDWSSTRUCT

WSMETHOD NEW WSCLIENT WSUTILAGILITY_TKNSTRUCT
	::Init()
Return Self

WSMETHOD INIT WSCLIENT WSUTILAGILITY_TKNSTRUCT
Return

WSMETHOD CLONE WSCLIENT WSUTILAGILITY_TKNSTRUCT
	Local oClone := WSUTILAGILITY_TKNSTRUCT():NEW()
	oClone:cCONTEUDO            := ::cCONTEUDO
	oClone:oWSEMPRESAFILIAL     := IIF(::oWSEMPRESAFILIAL = NIL , NIL , ::oWSEMPRESAFILIAL:Clone() )
	oClone:cMENSAGEM            := ::cMENSAGEM
	oClone:cMODULO              := ::cMODULO
	oClone:cSENHA               := ::cSENHA
	oClone:cSESSIONID           := ::cSESSIONID
Return oClone

WSMETHOD SOAPSEND WSCLIENT WSUTILAGILITY_TKNSTRUCT
	Local cSoap := ""
	cSoap += WSSoapValue("CONTEUDO", ::cCONTEUDO, ::cCONTEUDO , "string", .T. , .F., 0 , NIL, .F.) 
	cSoap += WSSoapValue("EMPRESAFILIAL", ::oWSEMPRESAFILIAL, ::oWSEMPRESAFILIAL , "EmpFilialStruct", .F. , .F., 0 , NIL, .F.) 
	cSoap += WSSoapValue("MENSAGEM", ::cMENSAGEM, ::cMENSAGEM , "string", .F. , .F., 0 , NIL, .F.) 
	cSoap += WSSoapValue("MODULO", ::cMODULO, ::cMODULO , "string", .F. , .F., 0 , NIL, .F.) 
	cSoap += WSSoapValue("SENHA", ::cSENHA, ::cSENHA , "string", .F. , .F., 0 , NIL, .F.) 
	cSoap += WSSoapValue("SESSIONID", ::cSESSIONID, ::cSESSIONID , "string", .F. , .F., 0 , NIL, .F.) 
Return cSoap

// WSDL Data Structure EmpFilialStruct

WSSTRUCT WSUTILAGILITY_EmpFilialStruct
	WSDATA   cEMPRESA                  AS string OPTIONAL
	WSDATA   cFILIAL                   AS string OPTIONAL
	WSDATA   cFILPAR                   AS string OPTIONAL
	WSMETHOD NEW
	WSMETHOD INIT
	WSMETHOD CLONE
	WSMETHOD SOAPSEND
ENDWSSTRUCT

WSMETHOD NEW WSCLIENT WSUTILAGILITY_EmpFilialStruct
	::Init()
Return Self

WSMETHOD INIT WSCLIENT WSUTILAGILITY_EmpFilialStruct
Return

WSMETHOD CLONE WSCLIENT WSUTILAGILITY_EmpFilialStruct
	Local oClone := WSUTILAGILITY_EmpFilialStruct():NEW()
	oClone:cEMPRESA             := ::cEMPRESA
	oClone:cFILIAL              := ::cFILIAL
	oClone:cFILPAR              := ::cFILPAR
Return oClone

WSMETHOD SOAPSEND WSCLIENT WSUTILAGILITY_EmpFilialStruct
	Local cSoap := ""
	cSoap += WSSoapValue("EMPRESA", ::cEMPRESA, ::cEMPRESA , "string", .F. , .F., 0 , NIL, .F.) 
	cSoap += WSSoapValue("FILIAL", ::cFILIAL, ::cFILIAL , "string", .F. , .F., 0 , NIL, .F.) 
	cSoap += WSSoapValue("FILPAR", ::cFILPAR, ::cFILPAR , "string", .F. , .F., 0 , NIL, .F.) 
Return cSoap