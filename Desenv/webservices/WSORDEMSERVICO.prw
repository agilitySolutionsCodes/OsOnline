#INCLUDE "APWEBSRV.CH"
#INCLUDE "PROTHEUS.CH"
#INCLUDE "TOPCONN.CH"
#include "tbiconn.ch"
#define CRLF Chr(13)+Chr(10)

/*Dummy*/
User Function WSORDEMSERVICO()
/*
TECA300( cRotina, aDadosTMK, xAutoCab, xAutoItens, nOpcAuto )
ab1, ab2
TECA450( cRotina, xAutoCab, xAutoItens, xAutoApont, nOpcAuto )
ab6, ab7, ab8
TECA460(xAutoCab,xAutoItens,nOpcAuto)
ab9, aba
*/
fatd()
return

Static Function fatd()
otoken:=wsclassnews("tokenstruct")
otoken:conteudo:="AGT_13"
oficha:=wsclassnew("FichaAtendimentoStruct")
oitem:=wsclassnews("ItemAtendimentoStruct")
oordem:=wsclassnew("FichaOrdemServicoStruct")
oitordem:=wsclassnew("ItemOrdemServicoStruct")

oitordem:numeroitem:="01"
oitordem:CodigoOcorrencia:="000007"  
oordem:numeroordemservico="000005"

aItens:={}
aadd(aItens	, oitordem)
oordem:Itens:=aItens

oficha:OrdemServico:=oOrdem
oficha:CodigoTecnico:="00001"
oficha:Sequencia:="01"
oficha:DataInicio:=date()
oficha:HoraInicio:="00:00"
oficha:DataFim:=date()
oficha:HoraFim:="01:00"
oficha:Traslado:="01:00"
oficha:HorasFaturadas:="05:00"
oficha:CodigoSituacao:="1"
//oficha:Laudo:="laudo"
oficha:DefeitoConstatado:="defeito"
oficha:CausaProvavel:="causa"
oficha:ServicoExecutado:="servico ex"
oitem:CodigoProdutoNovo :="006.00001"
oitem:NumeroSerieNovo :="NS01B"
oitem:Quantidade :=1
//oitem:CodigoServico :="999999"
oitem:CodigoProdutoAnterior :=""
oitem:NumeroSerieAnterior :="NS01A"
oitem:Lote :=""
oitem:CodigoTecnico:="00001"

//	oitem:NumeroItem as String Optional

aItens:={}
aadd(aItens	, oitem)
oFicha:Itens:=aItens
//oRet:=wsordemservico():Incluir(otoken, oficha)
//oFicha:NumeroChamado:='00027042'
//oRet:=wsordemservico():Alterar(otoken, oficha)
//oFicha:NumeroOrdemServico:="002133"
//oficha:TipoOrdemServico:="01"
//oficha:Itens[1]:CodigoSituacao:="1"
oRet:=wsordemservico():Atender(otoken, oficha)
Return Nil



Static Function fIncOs()
otoken:=wsclassnews("tokenstruct")
otoken:conteudo:="AGT_13"
oficha:=wsclassnew("FichaOrdemServicoStruct")
oitem:=wsclassnews("ItemOrdemServicoStruct")
oficha:CNPJ:="05868278000280"
oficha:Contato:="Marcelo"
oficha:Telefone:="1126312510"
oficha:Tipo:="E"
oitem:CodigoSituacao:="1"
oitem:CodigoClassificacao:="001"
oitem:CodigoProduto :="132.00000"
oitem:NumeroSerie :="I5-2003-07-05260"
oitem:CodigoOcorrencia :="000007"
oitem:CodigoStatus :="A"
oItem:NumeroItem:="01"

oficha:TipoOrdemServico:="01"

aItens:={}
aadd(aItens	, oitem)
oFicha:Itens:=aItens
oRet:=wsordemservico():Incluir(otoken, oficha)
/*

oFicha:NumeroChamado:='00027042'
//oRet:=wsordemservico():Alterar(otoken, oficha)
oFicha:NumeroOrdemServico:="002133"

oficha:Itens[1]:CodigoSituacao:="1"
oRet:=wsordemservico():Alterar(otoken, oficha)
*/
Return Nil


WSService WSOrdemServico DESCRIPTION "Serviços de Relacionados a Ordem de Serviço
	WSDATA Token                 As TokenStruct
	WSDATA Retorno			     As RetornoStruct
	WSDATA NumeroOrdemServico	 AS String Optional
	WSDATA NumeroChamado         AS String Optional
	WSDATA ItemOrdemServico	     AS String Optional
	WSDATA FichaOrdemServico     As FichaOrdemServicoStruct  
	WSDATA FichaAtendimento      As FichaAtendimentoStruct
	WSMETHOD Incluir      		 DESCRIPTION "Método de geração" 
	WSMETHOD IncluirOS     		 DESCRIPTION "Método de geração" 
	WSMETHOD Alterar      		 DESCRIPTION "Método de alteração" 	
	WSMETHOD Atender      		 DESCRIPTION "Método de atualização" 
EndWSService

WSSTRUCT FichaOrdemServicoStruct   
	WSDATA NumeroChamado 		As String Optional
	WSDATA NumeroOrdemServico 	As String Optional	
	WSDATA NumeroOSparc			AS String Optional
	WSDATA CNPJ 				As String
	WSDATA CodigoCliente 		As String Optional
	WSDATA Contato 				As String
	WSDATA Telefone 			As String
	WSDATA Tipo 				As String	  
	WSDATA TipoOrdemServico 	As String Optional
	WSDATA ChamadoOS 			as String Optional //C=Chamado/O=Ordem de Servico
	WSDATA Itens 				As Array Of ItemOrdemServicoStruct
ENDWSSTRUCT

WSSTRUCT ItemOrdemServicoStruct
	WSDATA CodigoSituacao As String
	WSDATA CodigoClassificacao As String
	WSDATA CodigoProduto As String
	WSDATA NumeroSerie As String
	WSDATA CodigoOcorrencia As String
	WSDATA CodigoStatus As String
	WSDATA CodigoEtapa As String Optional
	WSDATA NumeroItem As String	Optional
	WSDATA Comentario As String Optional
	WSDATA D_E_L_E_T_ As String Optional
END WSSTRUCT           

WSSTRUCT FichaAtendimentoStruct   
//	WSDATA OrdemServico As FichaOrdemServicoStruct
	WSDATA OrdemServico As String Optional	
	WSDATA NumeroAtendimentoOS As String	
	WSDATA SequenciaAtendimentoOS As String	
	WSDATA NumeroSerieequip As String    
	WSDATA CodigoTecnico As String
	WSDATA Codigoocorrencia As String
	WSDATA Codigogarantia As String
	WSDATA Incluiitemos As String
	WSDATA DataInicio As Date
	WSDATA HoraInicio As String
	WSDATA DataFim As Date
	WSDATA HoraFim As String
	WSDATA Traslado As String
//	WSDATA HorasFaturadas As String
	WSDATA CodigoSituacao As String
	//WSDATA Laudo As String optional
	WSDATA DefeitoConstatado As String
	WSDATA CausaProvavel As String
	WSDATA ServicoExecutado As String
//	WSDATA CodigoUsuario As String
	WSDATA Itens As Array Of ItemAtendimentoStruct
	WSDATA Modoatendimento As String
	WSDATA BloqueioAtendimento as String Optional 
	WSDATA AprovadoAtendimento as String Optional
	WSDATA Aprovado2Atendimento as String Optional
	WSDATA CodigoDefeito as String
	WSDATA CodigoServico as String
	WSDATA VersaoSoftware as String Optional
	WSDATA VersaoAtual as String Optional
ENDWSSTRUCT

WSSTRUCT ItemAtendimentoStruct
	WSDATA CodigoProdutoNovo As String
	WSDATA NumeroSerieNovo As String 
	WSDATA Quantidade As Float
	WSDATA NumeroSeriepecas As String
	WSDATA Numerolote As String
	WSDATA NumeroItem as String Optional
	WSDATA D_E_L_E_T_ As String Optional
END WSSTRUCT  

/*/
±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±
±±ÚÄÄÄÄÄÄÄÄÄÄÂÄÄÄÄÄÄÄÄÄÄÂÄÄÄÄÄÄÄÂÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÂÄÄÄÄÄÄÂÄÄÄÄÄÄÄÄÄÄÄ¿±±
±±³Fun‡„o    ³Incluir           ³Autor  ³ Marcelo Piazza³ Data ³01.2011    ³±±
±±³Alterado  ³Juliana B. - Reestruturacao para grupo x serie   ³01.2012    ³±±
±±ÃÄÄÄÄÄÄÄÄÄÄÅÄÄÄÄÄÄÄÄÄÄÁÄÄÄÄÄÄÄÁÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÁÄÄÄÄÄÄÁÄÄÄÄÄÄÄÄÄÄÄ´±±
±±³          ³Método de geracao                                            ³±±
±±ÃÄÄÄÄÄÄÄÄÄÄÅÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄ´±±
±±³Parametros³Token: token de segurançac                                   ³±±
±±³          ³Apontamento: estrutura do apontamento                        ³±±
±±ÃÄÄÄÄÄÄÄÄÄÄÅÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄ´±±
±±³Retorno   ³aRet : Estrutura de retorno                                  ³±±
±±ÀÄÄÄÄÄÄÄÄÄÄÁÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÙ±±
±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±
ßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßß
/*/
WSMETHOD Incluir WSRECEIVE Token, FichaOrdemServico WSSEND Retorno WSSERVICE WSOrdemServico

Local oRetorno := WSClassNew("RetornoStruct")
Local aArea := {}
Local cCodGrp := "" 
Local cCodUsu := "" 
Local cRegLic := ""
//verifica se o usuario pode executar a operação e atualiza as variaveis do microsiga
if u_ValToken(Token)
	
	cCNPJ := FichaOrdemServico:CNPJ
	cCodCli := FichaOrdemServico:CodigoCliente
	cLojCli := AllTrim(GetAdvFVal("SA1","A1_LOJA",XFILIAL("SA1")+cCNPJ,3))
	cRegLic := AllTrim(GetAdvFVal("SA1","A1_REGLIC",XFILIAL("SA1")+cCNPJ,3))

	//Inicio tratativa Cliente X Licenciado
	cSQL:=" SELECT AI3_CODUSU,AI3_DESCRI, AI3_CGROUP FROM " + RETSQLTAB("AI3") + " WITH (NOLOCK)  WHERE " + RETSQLCOND("AI3") + " AND AI3_USRSIS = '"+__cUserId+"'"
	TCQUERY cSQL NEW ALIAS "TRB" 
	
	if !TRB->(EOF())
		cCodUsu:=TRB->AI3_CODUSU
		cCodGrp:=TRB->AI3_CGROUP //ALTERADO POR ANDERSON GUSMON EM 16/08/11 PARA TRATATIVA DE GRUPO DE USUARIO
	ENDIF
	TRB->(DBCLOSEAREA())
	IF len(ALLTRIM(cCodUsu))==0
		oRetorno:Sucesso:=.F.
		oRetorno:Codigo:=1
		oRetorno:Mensagem:="O usuario atual não está vinculado a um Licenciado."
		oRetorno:Chave:=""
	ELSE

		cNrCham:=GetSX8Num("AB1","AB1_NRCHAM")
		oItens:=FichaOrdemServico:Itens
		
		aCab:={}
		aItem:={}
		aItens:={}
		aadd(aCab, {'AB1_FILIAL',xfilial("AB1"),Nil})
		aadd(aCab, {'AB1_NRCHAM',cNrCham,Nil})
		aadd(aCab, {'AB1_EMISSA',dDatabase,Nil})
		aadd(aCab, {'AB1_CODCLI',cCodCli,Nil})
		aadd(aCab, {'AB1_LOJA',cLojCli,Nil})
		aadd(aCab, {'AB1_CONTAT',FichaOrdemServico:Contato,Nil})
		aadd(aCab, {'AB1_TEL',FichaOrdemServico:Telefone,Nil})
		aadd(aCab, {'AB1_ATEND',__cUserId,Nil}) 
		aadd(aCab, {'AB1_TIPO',FichaOrdemServico:Tipo,Nil})
		aadd(aCab, {'AB1_CGROUP',cCodGrp,Nil})                
		aadd(aCab, {'AB1_REGLIC',cRegLic,Nil})
		For i:=1 to len(oItens)
			aItem:={}
			oItem:=oItens[i]
			aadd(aItem, {'AB2_FILIAL',XFILIAL("AB2"),Nil})
			aadd(aItem, {'AB2_ITEM'  ,oItem:NumeroItem,Nil})
			aadd(aItem, {'AB2_TIPO'  ,oItem:CodigoSituacao,Nil})
			aadd(aItem, {'AB2_CLASSI',oItem:CodigoClassificacao,Nil})
			aadd(aItem, {'AB2_CODPRO',ALLTRIM(oItem:CodigoProduto),nil})
			aadd(aItem, {'AB2_NUMSER',oItem:NumeroSerie,Nil})
			aadd(aItem, {'AB2_CODPRB',oItem:CodigoOcorrencia,Nil})
			aadd(aItem, {'AB2_STATUS',oItem:CodigoStatus,Nil})
			aadd(aItem, {'AB2_NRCHAM',cNrCham,Nil})
			aadd(aItem, {'AB2_CODCLI',cCodCli,Nil})
			aadd(aItem, {'AB2_LOJA'  ,cLojCli,Nil})
			aadd(aItem, {'AB2_EMISSA',dDatabase,Nil})
			aadd(aItem, {'AB2_MEMO2'  ,oItem:Comentario,Nil})
			aadd(aItem, {'AB2_CGROUP',cCodGrp,Nil})
			aadd(aItens, aItem)
		Next
		lMsErroAuto := .F.
		//varinfo("ficha", fichaordemservico)
		msExecAuto({|x,y| TECA300('',,x,Y)}, aCab, aItens, 3)
		ConfirmSx8()
		If lMsErroAuto
			oRetorno:Sucesso:=.F.
			oRetorno:Codigo:=1
			oRetorno:Mensagem:=MemoRead(NOMEAUTOLOG())
			oRetorno:Chave:=""
			ferase(NOMEAUTOLOG())
		ELSE                     
			//CONSULTAR OS CRIADA E ALTERAR NUMERO DO TELEFONE E CONTATO 
			If oItens[1]:CodigoSituacao == "3"
				cSQL:=" SELECT LEFT(AB2_NUMOS,6) AB2_NUMOS FROM " + RETSQLTAB("AB2") + " WITH (NOLOCK)  WHERE " + RETSQLCOND("AB2") + " AND AB2_NRCHAM = '"+cNrCham+"'"
				TCQUERY cSQL NEW ALIAS "TAB" 
				If !TAB->(EOF())
			   		TCSQLEXEC("UPDATE " + RETSQLNAME("AB6") + " SET AB6_CONTAT = '" + FichaOrdemServico:Contato + "', AB6_TEL = '" + FichaOrdemServico:Telefone + "', AB6_REGLIC = '" + cRegLic + "', AB6_CGROUP = '" + cCodGrp + "', AB6_ATEND = '" + __cUserId + "', AB6_TIPO = '" + FichaOrdemServico:Tipo + "' WHERE AB6_FILIAL = '" + xFilial("AB6") + "' AND AB6_NUMOS = '"+ TAB->AB2_NUMOS +"' ")
				EndIf
				TAB->(DBCLOSEAREA())
			EndIf
			
			oRetorno:Sucesso:=.T.
			oRetorno:Codigo:=0
			oRetorno:Mensagem:="Número Chamado: "+cNrCham+" gerado com sucesso."
			oRetorno:Chave:=xfilial("AB1") + CNRCHAM
		ENDIF
		
	ENDIF
Else
	oRetorno:Sucesso:=.F.
	oRetorno:Codigo:=1
	oRetorno:Mensagem:="O usuário não possui permissão para esta operação."
	oRetorno:Chave:=""
endif
::Retorno:=oRetorno
RpcClearEnv()
Return .t.          


/*/
±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±
±±ÚÄÄÄÄÄÄÄÄÄÄÂÄÄÄÄÄÄÄÄÄÄÂÄÄÄÄÄÄÄÂÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÂÄÄÄÄÄÄÂÄÄÄÄÄÄÄÄÄÄÄ¿±±
±±³Fun‡„o    ³IncluirOS		           ³Autor  ³ Juliana³ Data ³05.11.2011 ³±±
±±ÃÄÄÄÄÄÄÄÄÄÄÅÄÄÄÄÄÄÄÄÄÄÁÄÄÄÄÄÄÄÁÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÁÄÄÄÄÄÄÁÄÄÄÄÄÄÄÄÄÄÄ´±±
±±³          ³Método de geracao de OS                                      ³±±
±±ÃÄÄÄÄÄÄÄÄÄÄÅÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄ´±±
±±³Parametros³Token: token de segurançac                                   ³±±
±±³          ³Apontamento: estrutura do apontamento                        ³±±
±±ÃÄÄÄÄÄÄÄÄÄÄÅÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄ´±±
±±³Retorno   ³aRet : Estrutura de retorno                                  ³±±
±±ÀÄÄÄÄÄÄÄÄÄÄÁÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÙ±±
±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±
ßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßß
/*/
WSMETHOD IncluirOS WSRECEIVE Token, FichaOrdemServico WSSEND Retorno WSSERVICE WSOrdemServico   


Local oRetorno:=WSClassNew("RetornoStruct")
Local aArea:={}
Local cCodGrp := "" 
Local cRegLic := "" 
Local cCodUsu := ""
//verifica se o usuario pode executar a operação e atualiza as variaveis do microsiga
if u_ValToken(Token)

	cCNPJ := FichaOrdemServico:CNPJ
	cCodCli := FichaOrdemServico:CodigoCliente
	cLojCli := AllTrim(GetAdvFVal("SA1","A1_LOJA",XFILIAL("SA1")+cCNPJ,3))
	cRegLic := AllTrim(GetAdvFVal("SA1","A1_REGLIC",XFILIAL("SA1")+cCNPJ,3))

	//Inicio tratativa Cliente X Licenciado
	cSQL:=" SELECT AI3_CODUSU,AI3_DESCRI, AI3_CGROUP FROM " + RETSQLTAB("AI3") + " WITH (NOLOCK)  WHERE " + RETSQLCOND("AI3") + " AND AI3_USRSIS = '"+__cUserId+"'"
	TCQUERY cSQL NEW ALIAS "TRB" 
	
	if !TRB->(EOF())
		cCodUsu:=TRB->AI3_CODUSU
		cCodGrp:=TRB->AI3_CGROUP //ALTERADO POR ANDERSON GUSMON EM 16/08/11 PARA TRATATIVA DE GRUPO DE USUARIO
	ENDIF
	TRB->(DBCLOSEAREA())
	IF len(ALLTRIM(cCodUsu))==0
		oRetorno:Sucesso:=.F.
		oRetorno:Codigo:=1
		oRetorno:Mensagem:="O usuario atual não está vinculado a um Licenciado."
		oRetorno:Chave:=""
	ELSE
	
		cNrOS:=GetSX8Num("AB6","AB6_NUMOS") 
		oItens:=FichaOrdemServico:Itens
		aCab:={}
		aItem:={}
		aItens:={}

		//DEPOIS GRAVAR OS NOVOS ITENS
		aadd(aCab, {'AB6_NUMOS',cNrOS,Nil})   
		aadd(aCab, {'AB6_CODCLI',cCodCli,Nil})
		aadd(aCab, {'AB6_LOJA',cLojCli,Nil})
		aadd(aCab, {'AB6_EMISSA',dDatabase,Nil})
		aadd(aCab, {'AB6_ATEND',__cUserId,Nil})
		aadd(aCab, {'AB6_CONPAG',"212",Nil})
			aadd(aCab, {'AB6_TIPO',FichaOrdemServico:Tipo,Nil})
		/*IF ALLTRIM(FichaOrdemServico:Tipo)='1'
			aadd(aCab, {'AB6_TIPO','I',Nil})
		ELSEIF ALLTRIM(FichaOrdemServico:Tipo)='2'                            
			aadd(aCab, {'AB6_TIPO','E',Nil})
		ENDIF*/ 
		aadd(aCab, {'AB6_OK',"",Nil})
		aadd(aCab, {'AB6_OSPARC',FichaOrdemServico:NumeroOSparc,Nil})
		aadd(aCab, {'AB6_TPOS',FichaOrdemServico:TipoOrdemServico,Nil})
		aadd(aCab, {'AB6_CGROUP',cCodGrp,Nil})
		aadd(aCab, {'AB6_CONTAT',FichaOrdemServico:Contato,Nil})
		aadd(aCab, {'AB6_TEL',FichaOrdemServico:Telefone,Nil})    
		aadd(aCab, {'AB6_REGLIC',cRegLic,Nil})

		For i:=1 to len(oItens)
			aItem:={}
			oItem:=oItens[i]         
			If AllTrim(oItem:D_E_L_E_T_) <> '*'
				aadd(aItem, {'AB7_NUMOS',cNrOS,Nil})
				aadd(aItem, {'AB7_ITEM',oItem:NumeroItem,Nil})
				aadd(aItem, {'AB7_TIPO',oItem:CodigoSituacao,Nil})  
				aadd(aItem, {'AB7_CODPRO',PADR(oItem:CodigoProduto,15),Nil})
				aadd(aItem, {'AB7_NUMSER',PADR(oItem:NumeroSerie,20),Nil})
				aadd(aItem, {'AB7_CODPRB',oItem:CodigoOcorrencia,Nil})  
				aadd(aItem, {'AB7_SERVIC', "000007",Nil})
				
			   	aadd(aItem, {'AB7_OSPARC',FichaOrdemServico:NumeroOSparc,Nil})
			   	aadd(aItem, {'AB7_ITPARC','',Nil})
				
				aadd(aItem, {'AB7_ETAPA', oItem:CodigoEtapa,Nil})
				
			   	aadd(aItem, {'AB7_CGROUP',cCodGrp,Nil})
   				aadd(aItem,{"AUTDELETA" ,"N",Nil}) // Incluir sempre no último elemento do array de cada item
				aadd(aItens, aItem)
			EndIf
		Next

		lMsErroAuto := .F. 
		MSExecAuto({|x,y,z| TECA450('',x,y, ,z)}, aCab, aItens, ,3)

		ConfirmSx8()
		If lMsErroAuto         
			oRetorno:Sucesso:=.F.
			oRetorno:Codigo:=1
			oRetorno:Mensagem:=MemoRead(NOMEAUTOLOG())
			oRetorno:Chave:=""
			ferase(NOMEAUTOLOG())
		ELSE
			oRetorno:Sucesso:=.T.
			oRetorno:Codigo:=0
			oRetorno:Mensagem:="Ordem de Servico: "+cNrOS+" gerado com sucesso."
			oRetorno:Chave:=xfilial("AB6") + cNrOS
		ENDIF
	
	ENDIF
Else
	oRetorno:Sucesso:=.F.
	oRetorno:Codigo:=1
	oRetorno:Mensagem:="O usuário não possui permissão para esta operação."
	oRetorno:Chave:=""
endif
::Retorno:=oRetorno
RpcClearEnv()
Return .t.



/*/
±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±
±±ÚÄÄÄÄÄÄÄÄÄÄÂÄÄÄÄÄÄÄÄÄÄÂÄÄÄÄÄÄÄÂÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÂÄÄÄÄÄÄÂÄÄÄÄÄÄÄÄÄÄÄ¿±±
±±³Fun‡„o    ³Alterar             ³Autor  ³ Marcelo Piazza³ Data ³03.2011  ³±±
±±ÃÄÄÄÄÄÄÄÄÄÄÅÄÄÄÄÄÄÄÄÄÄÁÄÄÄÄÄÄÄÁÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÁÄÄÄÄÄÄÁÄÄÄÄÄÄÄÄÄÄÄ´±±
±±³          ³Método de alteracao                                          ³±±
±±ÃÄÄÄÄÄÄÄÄÄÄÅÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄ´±±
±±³Parametros³Token: token de segurançac                                   ³±±
±±³          ³Apontamento: estrutura do apontamento                        ³±±
±±ÃÄÄÄÄÄÄÄÄÄÄÅÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄ´±±
±±³Retorno   ³aRet : Estrutura de retorno                                  ³±±
±±ÃÄÄÄÄÄÄÄÄÄÄÅÄÄÄÄÄÄÄÄÄÄÂÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄ´±±
±±³AUTOR     ³DATA      ³ALTERACAO										   ³±±
±±ÃÄÄÄÄÄÄÄÄÄÄÅÄÄÄÄÄÄÄÄÄÄÁÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄ´±±
±±³Juliana   ³26.10.2011|Alterada gravacao de OS para gravar dados de chamado³±±
±±ÀÄÄÄÄÄÄÄÄÄÄÁÄÄÄÄÄÄÄÄÄÄÁÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÙ±±
±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±
ßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßß
/*/
WSMETHOD Alterar WSRECEIVE Token, FichaOrdemServico WSSEND Retorno WSSERVICE WSOrdemServico
Local oRetorno:=WSClassNew("RetornoStruct")
Local aArea:={}
Local cCodGrp := "" 
Local cRegLic := ""

if u_ValToken(Token)
	cNumOS:=FichaOrdemServico:NumeroOrdemServico
	cCNPJ:=FichaOrdemServico:CNPJ
	cCodCli:=FichaOrdemServico:CodigoCliente
	cLojCli:=AllTrim(GetAdvFVal("SA1","A1_LOJA",XFILIAL("SA1")+cCNPJ,3))
	cRegLic := AllTrim(GetAdvFVal("SA1","A1_REGLIC",XFILIAL("SA1")+cCNPJ,3))
	oItens := FichaOrdemServico:Itens
	
	aCab:={}
	aItem:={}
	aItens:={}

	//Inicio tratativa Cliente X Licenciado
	cSQL:=" SELECT AI3_REGIAO, AI3_CODUSU,AI3_DESCRI, AI3_CGROUP FROM " + RETSQLTAB("AI3") + " WITH (NOLOCK)  WHERE " + RETSQLCOND("AI3") + " AND AI3_FILIAL= " +XFILIAL("AI3") + " AND AI3_USRSIS = '"+__cUserId+"'"
	cCodUsu:=""
	TCQUERY cSQL NEW ALIAS "TRB" 
	
	REGLICCLI:=AI3_REGIAO
	if !TRB->(EOF())
		cCodUsu:=TRB->AI3_CODUSU
		cCodGrp:=TRB->AI3_CGROUP //ALTERADO POR ANDERSON GUSMON EM 16/08/11 PARA TRATATIVA DE GRUPO DE USUARIO
	ENDIF
	TRB->(DBCLOSEAREA())
	IF len(ALLTRIM(cCodUsu))==0
		oRetorno:Sucesso:=.F.
		oRetorno:Codigo:=1
		oRetorno:Mensagem:="O usuario atual não está vinculado a um Licenciado."
		oRetorno:Chave:=""
	ENDIF
	
	/*
	eh chamado ou OS?
	*/
	
	
	IF FichaOrdemServico:ChamadoOS == "CT" //C=Chamado tecnico;O=Ordem Servico
		lEhChamado:=.T.
	ELSE
		lEhChamado:=.F.
	ENDIF
	if lEhChamado
		aadd(aCab, {'AB1_FILIAL',xfilial("AB1"),Nil})
		aadd(aCab, {'AB1_NRCHAM',FichaOrdemServico:NumeroChamado,Nil})
		aadd(aCab, {'AB1_CODCLI',cCodCli,Nil})
		aadd(aCab, {'AB1_LOJA',cLojCli,Nil})
		aadd(aCab, {'AB1_CONTAT',FichaOrdemServico:Contato,Nil})
		aadd(aCab, {'AB1_TEL',FichaOrdemServico:Telefone,Nil})
		aadd(aCab, {'AB1_ATEND',__cUserId,Nil}) 
		aadd(aCab, {'AB1_TIPO',FichaOrdemServico:Tipo,Nil})
	   /*	IF ALLTRIM(FichaOrdemServico:Tipo)='1'
			aadd(aCab, {'AB1_TIPO','I',Nil})
		ELSEIF ALLTRIM(FichaOrdemServico:Tipo)='2'                            
			aadd(aCab, {'AB1_TIPO','E',Nil})
		ENDIF   */
		aadd(aCab, {'AB1_CGROUP',cCodGrp,Nil})

		For i:=1 to len(oItens)
			aItem:={}
			oItem:=oItens[i]
			aadd(aItem, {'AB2_FILIAL',XFILIAL("AB2"),Nil})  
			aadd(aItem, {'AB2_ITEM',oItem:NumeroItem,Nil})
			aadd(aItem, {'AB2_TIPO'  ,oItem:CodigoSituacao,Nil})
			aadd(aItem, {'AB2_CLASSI',oItem:CodigoClassificacao,Nil})
			aadd(aItem, {'AB2_CODPRO',PADR(oItem:CodigoProduto,15),Nil})
			aadd(aItem, {'AB2_NUMSER',PADR(oItem:NumeroSerie,20),Nil})
			aadd(aItem, {'AB2_CODPRB',oItem:CodigoOcorrencia,Nil})
			aadd(aItem, {'AB2_STATUS',oItem:CodigoStatus,Nil})
			aadd(aItem, {'AB2_NRCHAM',FichaOrdemServico:NumeroChamado,Nil})
			aadd(aItem, {'AB2_CODCLI',cCodCli,Nil})
			aadd(aItem, {'AB2_LOJA'  ,cLojCli,Nil})
			aadd(aItem, {'AB2_MEMO2' ,oItem:Comentario,Nil})
			aadd(aItem, {'AB2_CGROUP',cCodGRP,Nil})
			aadd(aItens, aItem)
		Next                                       
		lMsErroAuto := .F.
		//msExecAuto({|x,y| TECA300('',,x,Y)}, aCab, aItens, 4)
		MSExecAuto({|x,y,z| TECA300('','A',aCab,aItens,4)}, aCab, aItens,4)
		
		If lMsErroAuto
			oRetorno:Sucesso:=.F.
			oRetorno:Codigo:=1
			oRetorno:Mensagem:=MemoRead(NOMEAUTOLOG())
			oRetorno:Chave:=""
			ferase(NOMEAUTOLOG())
		ELSE
			//CONSULTAR OS CRIADA E ALTERAR NUMERO DO TELEFONE E CONTATO E USUARIO
			If oItens[1]:CodigoSituacao == "3"
				cSQL:=" SELECT LEFT(AB2_NUMOS,6) AB2_NUMOS FROM " + RETSQLTAB("AB2") + " WITH (NOLOCK)  WHERE " + RETSQLCOND("AB2") + " AND AB2_NRCHAM = '"+FichaOrdemServico:NumeroChamado+"'"
				TCQUERY cSQL NEW ALIAS "TAB" 
				If !TAB->(EOF())  
			   		TCSQLEXEC("UPDATE " + RETSQLNAME("AB6") + " SET AB6_CONTAT = '" + FichaOrdemServico:Contato + "', AB6_TEL = '" + FichaOrdemServico:Telefone + "', AB6_REGLIC = '" + cRegLic + "', AB6_CGROUP = '" + cCodGrp + "', AB6_ATEND = '" + __cUserId + "', AB6_TIPO = '" + FichaOrdemServico:Tipo + "' WHERE AB6_FILIAL = '" + xFilial("AB6") + "' AND AB6_NUMOS = '"+ TAB->AB2_NUMOS +"' ")
				EndIf
				TAB->(DBCLOSEAREA())
			EndIf
		
			For i:=1 to len(oItens)
			  aItem:={}
			  oItem:=oItens[i]
			  IF ALLTRIM(oItem:D_E_L_E_T_)=='*'
				 TCSQLEXEC("UPDATE " + RETSQLNAME("AB2") + " SET D_E_L_E_T_ = '*' WHERE AB2_FILIAL = '" + XFILIAL("AB2") + "' AND AB2_NUMSER = '"+ PADR(oItem:NumeroSerie,20) +"' AND AB2_NRCHAM = '"+ FichaOrdemServico:NumeroChamado +"' AND AB2_ITEM = '"+ oItem:NumeroItem +"' ")
			  Endif
			Next  
			oRetorno:Sucesso:=.T.
			oRetorno:Codigo:=0
			oRetorno:Mensagem:="Chamado "+FichaOrdemServico:NumeroChamado+" alterado com sucesso."
			oRetorno:Chave:=xfilial("AB1") + FichaOrdemServico:NumeroChamado
		ENDIF
	Else        
	
		//PRIMEIRO DELETAR OS ITENS DAS TABELAS AB2 E AB7
		For i:=1 to len(oItens)
			aItem:={}
		   	oItem:=oItens[i]
		    If ALLTRIM(oItem:D_E_L_E_T_)=='*'
				TCSQLEXEC("UPDATE " + RETSQLNAME("AB7") + " SET D_E_L_E_T_ = '*' WHERE AB7_FILIAL = '" + XFILIAL("AB7") + "' AND AB7_NUMSER = '"+ ALLTRIM(PADR(oItem:NumeroSerie,20)) +"' AND AB7_NUMOS = '"+ SUBSTR(FichaOrdemServico:NumeroOrdemServico+oItem:NumeroItem,1,6) +"' AND AB7_ITEM = '"+ oItem:NumeroItem +"' ")
		   	EndIf         
		   	If !Empty(FichaOrdemServico:NumeroChamado) .And. AllTrim(oItem:D_E_L_E_T_) == '*'
		 		TCSQLEXEC("UPDATE " + RETSQLNAME("AB2") + " SET D_E_L_E_T_ = '*' WHERE AB2_FILIAL = '" + XFILIAL("AB2") + "' AND AB2_NUMSER = '"+ PADR(oItem:NumeroSerie,20) +"' AND AB2_NRCHAM = '"+ FichaOrdemServico:NumeroChamado +"' AND AB2_ITEM = '"+ oItem:NumeroItem +"' ")
			Endif
        Next
               
		//DEPOIS GRAVAR OS NOVOS ITENS
		aadd(aCab, {'AB6_FILIAL',xfilial("AB6"),Nil})
		aadd(aCab, {'AB6_NUMOS',FichaOrdemServico:NumeroOrdemServico,Nil})
		aadd(aCab, {'AB6_OSPARC',FichaOrdemServico:NumeroOSparc,Nil})
		aadd(aCab, {'AB6_TIPO',FichaOrdemServico:Tipo,Nil})
		/*IF ALLTRIM(FichaOrdemServico:Tipo)='1'
			aadd(aCab, {'AB6_TIPO','I',Nil})
		ELSEIF ALLTRIM(FichaOrdemServico:Tipo)='2'                            
			aadd(aCab, {'AB6_TIPO','E',Nil})
		ENDIF*/
		aadd(aCab, {'AB6_CODCLI',cCodCli,Nil})
		aadd(aCab, {'AB6_LOJA',cLojCli,Nil})
		aadd(aCab, {'AB6_CONPAG',"212",Nil})
		aadd(aCab, {'AB6_TPOS',FichaOrdemServico:TipoOrdemServico,Nil})
		aadd(aCab, {'AB6_ATEND',__cUserId,Nil})
		aadd(aCab, {'AB6_CGROUP',cCodGrp,Nil})
		aadd(aCab, {'AB6_CONTAT',FichaOrdemServico:Contato,Nil})
		aadd(aCab, {'AB6_TEL',FichaOrdemServico:Telefone,Nil})    

		
		For i:=1 to len(oItens)
			aItem:={}
			oItem:=oItens[i]         
			If AllTrim(oItem:D_E_L_E_T_) <> '*'
				aadd(aItem, {'AB7_FILIAL',xfilial("AB7"),Nil})
				aadd(aItem, {'AB7_NUMOS',FichaOrdemServico:NumeroOrdemServico+oItem:NumeroItem,Nil})
				aadd(aItem, {'AB7_OSPARC',FichaOrdemServico:NumeroOSparc,Nil})
				aadd(aItem, {'AB7_ITPARC','',Nil})
				aadd(aItem, {'AB7_ITEM',oItem:NumeroItem,Nil})
				aadd(aItem, {'AB7_CODCLI',cCodCli,Nil})
				aadd(aItem, {'AB7_LOJA',cLojCli,Nil})
				aadd(aItem, {'AB7_CODPRO',PADR(oItem:CodigoProduto,15),Nil})
				aadd(aItem, {'AB7_NUMSER',PADR(oItem:NumeroSerie,20),Nil})
				aadd(aItem, {'AB7_CODPRB',oItem:CodigoOcorrencia,Nil})
				aadd(aItem, {'AB7_ETAPA', oItem:CodigoEtapa,Nil})
				aadd(aItem, {'AB7_SERVIC', "000007",Nil})
				aadd(aItem, {'AB7_CGROUP',cCodGrp,Nil})
	
				aadd(aItens, aItem)
			EndIf
		Next

		lMsErroAuto := .F.
		MSExecAuto({|x,y,z| TECA450('',aCab,aItens, ,4)}, aCab, aItens, ,4)

		If lMsErroAuto  

			oRetorno:Sucesso:=.F.
			oRetorno:Codigo:=1
			oRetorno:Mensagem:=MemoRead(NOMEAUTOLOG())
			oRetorno:Chave:=""
			ferase(NOMEAUTOLOG())
		ELSE

			//ALTERAR DADOS DE CHAMADO DENTRO DA OS
			If !Empty(FichaOrdemServico:NumeroChamado) 
				//ALTERAR NOME DO CLIENTE E TELEFONE
				cChave := xFILIAL("AB1")+FichaOrdemServico:NumeroChamado
	        	DBSELECTAREA("AB1")
	        	AB1->(DbGoTop())
				AB1->(DbSetOrder(1))
				AB1->(DbSeek(cChave))		
				If AB1->(!EOF())
					RecLock("AB1",.F.)
					AB1->AB1_CONTAT	:= FichaOrdemServico:Contato     
				   	AB1->AB1_TEL	:= FichaOrdemServico:Telefone
					AB1->(MsUnlock())  	       
				EndIf                                                 
				
				//ALTERAR CLASSIFICACAO
				For i:=1 to len(oItens)
					TCSQLEXEC("UPDATE " + RETSQLNAME("AB2") + " SET AB2_CLASSI = '" + oItens[i]:CodigoClassificacao + "' WHERE AB2_FILIAL = '" + xFilial("AB2") + "' AND AB2_NUMSER = '"+ PADR(oItens[i]:NumeroSerie,20) +"' AND AB2_NRCHAM = '"+ FichaOrdemServico:NumeroChamado +"' AND AB2_ITEM = '"+ oItens[i]:NumeroItem +"' ")
				Next
            EndIf
			
	        IFATENDIDO := .F.
			dbSelectArea("AB7")
			AB7->(DBSETORDER(1))
			AB7->(DBGOTOP())
			AB7->(dbSeek(XFILIAL("AB7")+FichaOrdemServico:NumeroOrdemServico))
			WHILE AB7->(!EOF()) .AND. FichaOrdemServico:NumeroOrdemServico == AB7->AB7_NUMOS
				cSQL:=""
				cSQL:=" SELECT MAX(R_E_C_N_O_) AS RECNO,AB9_TIPO FROM " + RETSQLTAB("AB9") + " WITH (NOLOCK)  "
				cSQL+="WHERE '"+XFILIAL("AB7")+"'=AB9_FILIAL AND D_E_L_E_T_='' AND AB9_NUMOS='"+(ALLTRIM(AB7->AB7_NUMOS)+ALLTRIM(AB7->AB7_ITEM))+"' "
				cSQL+="GROUP BY AB9_TIPO "

				STATUSITEMOS:=""
				TCQUERY cSQL NEW ALIAS "TRB"
				if !TRB->(EOF())
					STATUSITEMOS:=TRB->AB9_TIPO
				ENDIF
				TRB->(DBCLOSEAREA())
				RecLock("AB7",.F.)
				IF STATUSITEMOS == '1'//ENCERRADO
					AB7->AB7_TIPO:="4" //ATENDIDO
				ELSEIF STATUSITEMOS == '2'
					AB7->AB7_TIPO:="3" //EM ATENDIMENTO
				ENDIF 
				AB7->(MSUNLOCK())          
				
				cSQL:=""
				cSQL:=" SELECT AB7_TIPO FROM " + RETSQLTAB("AB7") + " WITH (NOLOCK)  "
				cSQL+="WHERE '"+XFILIAL("AB7")+"'=AB7_FILIAL AND D_E_L_E_T_='' AND AB7_NUMOS='"+(ALLTRIM(AB7->AB7_NUMOS))+"' AND AB7_TIPO<>'4' "
				cSQL+="GROUP BY AB7_TIPO "

			   //	TCQUERY cSQL NEW ALIAS "TRB"
				
			/*	IF 	EMPTY(TRB->AB7_TIPO)
			        IFATENDIDO := .T.
			 	ELSE
			 	    IFATENDIDO := .F.
				ENDIF       
				TRB->(DBCLOSEAREA())
			   	IF IFATENDIDO = .T.
					dbSelectArea("AB6")
					AB6->(DBSETORDER(1))
					AB6->(DBGOTOP())
					AB6->(dbSeek(XFILIAL("AB6")+FichaOrdemServico:NumeroOrdemServico))
					RecLock("AB6",.F.)
					AB6->AB6_STATUS:="B"
			   		AB6->(MSUNLOCK())			
				ENDIF  
				             */
				AB7->(DBSKIP())
				
			ENDDO  

			oRetorno:Sucesso:=.T.
			oRetorno:Codigo:=0
			oRetorno:Mensagem:="Ordem de Serviço "+FichaOrdemServico:NumeroOrdemServico+" alterada com sucesso."
			oRetorno:Chave:=xfilial("AB6") + cNumOS
			
			//TROCA BAIXADO POR ENCERRADO
			CCHAVE:=XFILIAL("AB6")+FichaOrdemServico:NumeroOrdemServico
        	DBSELECTAREA("AB6")
        	AB6->(DBGOTOP())
			AB6->(DBSETORDER(1))//
			AB6->(DBSEEK(CCHAVE))		
			IF AB6->AB6_STATUS='B'
			RecLock("AB6",.F.)
				AB6->AB6_STATUS:="E" //TROCA BAIXADO POR ENCERRADO
			AB6->(MSUNLOCK())  	       
			ENDIF

		ENDIF
		
	ENDIF

Else
	oRetorno:Sucesso:=.F.
	oRetorno:Codigo:=1
	oRetorno:Mensagem:="O usuário não possui permissão para esta operação."
	oRetorno:Chave:=""
endif
::Retorno:=oRetorno
RpcClearEnv()
Return .t.     

/*/
±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±
±±ÚÄÄÄÄÄÄÄÄÄÄÂÄÄÄÄÄÄÄÄÄÄÂÄÄÄÄÄÄÄÂÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÂÄÄÄÄÄÄÂÄÄÄÄÄÄÄÄÄÄÄ¿±±
±±³Fun‡„o    ³Atender             ³Autor  ³ Marcelo Piazza³ Data ³01.2011 ³±±
±±ÃÄÄÄÄÄÄÄÄÄÄÅÄÄÄÄÄÄÄÄÄÄÁÄÄÄÄÄÄÄÁÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÁÄÄÄÄÄÄÁÄÄÄÄÄÄÄÄÄÄÄ´±±
±±³          ³Método de geracao                                            ³±±
±±ÃÄÄÄÄÄÄÄÄÄÄÅÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄ´±±
±±³Parametros³Token: token de segurançac                                   ³±±
±±³          ³Apontamento: estrutura do apontamento                        ³±±
±±ÃÄÄÄÄÄÄÄÄÄÄÅÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄ´±±
±±³Retorno   ³aRet : Estrutura de retorno                                  ³±±
±±ÀÄÄÄÄÄÄÄÄÄÄÁÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÙ±±
±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±
ßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßß
/*/
WSMETHOD Atender WSRECEIVE Token, FichaAtendimento WSSEND Retorno WSSERVICE WSOrdemServico
Local oRetorno	:= WSClassNew("RetornoStruct")
Local cCodGrp 	:= ""
Local cCodUsu	:= ""
Local lInclui	:=.f.
Local lAltera	:=.f.                                                   
Local oItem	:= ""

DEFAULT FichaAtendimento:BloqueioAtendimento := "1" //LIBERADO

if u_ValToken(Token)
	
	aCab:={}
	aItem:={}
	aItens:={}
	            
		//Inicio tratativa Cliente X Licenciado
	cSQL:=" SELECT AI3_CODUSU,AI3_DESCRI, AI3_CGROUP FROM " + RETSQLTAB("AI3") + " WITH (NOLOCK)  WHERE " + RETSQLCOND("AI3") + " AND AI3_USRSIS = '"+__cUserId+"'"
	TCQUERY cSQL NEW ALIAS "TRB" 

	If !TRB->(EOF())
		cCodUsu:=TRB->AI3_CODUSU
		cCodGrp:=TRB->AI3_CGROUP //ALTERADO POR ANDERSON GUSMON EM 16/08/11 PARA TRATATIVA DE GRUPO DE USUARIO
	ENDIF
	TRB->(DBCLOSEAREA())
	IF len(ALLTRIM(cCodUsu))==0
		oRetorno:Sucesso:=.F.
		oRetorno:Codigo:=1
		oRetorno:Mensagem:="O usuario atual não está vinculado a um Licenciado."
		oRetorno:Chave:=""
	ENDIF 
	
	//"resgatar o numero da OS"
	//REVER AT460GRV        
	
	IF ALLTRIM(FichaAtendimento:Modoatendimento) == "I"
		lInclui:=.T.
	ELSEIF ALLTRIM(FichaAtendimento:Modoatendimento) == "A"
		lAltera:=.T.
	ENDIF
		
	IF lInclui
		SEQATENDOS:=""
		cQry := ""
		cQry := " SELECT MAX(AB9_SEQ) AS SEQ  FROM "+RetSqlName("AB9")+" "
		cQry += " WHERE AB9_FILIAL='"+XFILIAL("AB9")+"' AND AB9_NUMOS = '"+ALLTRIM(FichaAtendimento:NumeroatendimentoOS)+"' "
		cQry += " AND AB9_CODTEC = '"+padr(FichaAtendimento:CodigoTecnico,6)+"'  AND D_E_L_E_T_ = ' ' "
		cQry := ChangeQuery(cQry)
		If Select("TRD") > 0
			DbSelectArea("TRD")
			DbCloseArea()
		Endif
		TcQuery cQry New Alias "TRD"
		
		IF EMPTY(TRD->SEQ)
			SEQATENDOS:="01"
		ELSE
			SEQATENDOS:=padr(ALLTRIM(STR((VAL(TRD->SEQ))+1)),2)
		ENDIF		
		
		aadd(aCab, {'AB9_FILIAL',xfilial("AB9"),Nil})
		aadd(aCab, {'AB9_NUMOS',FichaAtendimento:NumeroatendimentoOS,Nil})
		aadd(aCab, {'AB9_OSPARC','',Nil})
		aadd(aCab, {'AB9_CODTEC',padr(FichaAtendimento:CodigoTecnico,6),Nil})
		aadd(aCab, {'AB9_SEQ',FichaAtendimento:SequenciaAtendimentoOS,Nil})
		aadd(aCab, {'AB9_DTCHEG',FichaAtendimento:DataInicio,Nil})
		aadd(aCab, {'AB9_HRCHEG',FichaAtendimento:HoraInicio,Nil})
		aadd(aCab, {'AB9_DTSAID',FichaAtendimento:DataFim,Nil})
		aadd(aCab, {'AB9_HRSAID',FichaAtendimento:HoraFim,Nil})
		aadd(aCab, {'AB9_DTINI',FichaAtendimento:DataInicio,Nil})
		aadd(aCab, {'AB9_HRINI',FichaAtendimento:HoraInicio,Nil})
		aadd(aCab, {'AB9_DTFIM',FichaAtendimento:DataFim,Nil})
		aadd(aCab, {'AB9_HRFIM',FichaAtendimento:HoraFim,Nil})
		aadd(aCab, {'AB9_CODPRB',FichaAtendimento:CodigoOcorrencia,Nil})
		IF ALLTRIM(FichaAtendimento:Codigogarantia)=="1"
			aadd(aCab, {'AB9_GARANT','S',Nil})
		ELSE
			aadd(aCab, {'AB9_GARANT','N',Nil})
		ENDIF
		aadd(aCab, {'AB9_TIPO',ALLTRIM(FichaAtendimento:CodigoSituacao),Nil})
		aadd(aCab, {'AB9_STARTAR',ALLTRIM(FichaAtendimento:CodigoSituacao),Nil})		
		aadd(aCab, {'AB9_INCOS',FichaAtendimento:Incluiitemos,Nil})
//		aadd(aCab, {'AB9_MEMO2',FichaAtendimento:Laudo,Nil})
		aadd(aCab, {'AB9_DEFCON',FichaAtendimento:DefeitoConstatado,Nil})
		aadd(aCab, {'AB9_CPROV',FichaAtendimento:CausaProvavel,Nil})
		aadd(aCab, {'AB9_SRVEXE',FichaAtendimento:ServicoExecutado,Nil})
		aadd(aCab, {'AB9_CGROUP',cCodGrp,Nil})
		aadd(aCab, {'AB9_BLQATE',FichaAtendimento:BloqueioAtendimento,Nil}) 
		aadd(aCab, {'AB9_USER',__cUserId,Nil})  
		aadd(aCab, {'AB9_DEFEIT',FichaAtendimento:CodigoDefeito,Nil})
		aadd(aCab, {'AB9_SERVIC',FichaAtendimento:CodigoServico,Nil})
		//aadd(aCab, {'AB9_VERATU',FichaAtendimento:VersaoAtual,Nil})
		If !Empty(FichaAtendimento:VersaoSoftware) 
	   		aadd(aCab, {'AB9_VERSOF',FichaAtendimento:VersaoSoftware,Nil}) 
		EndIf

		FOR n:=1 to len(FichaAtendimento:Itens)
			aItem:={}
			oItem:=FichaAtendimento:Itens[n]
			
			IF !EMPTY(oItem:CodigoProdutoNovo)
				aadd(aItem, {'ABA_FILIAL',XFILIAL("ABA"),Nil})
				aadd(aItem, {'ABA_NUMOS',FichaAtendimento:NumeroAtendimentoOS,Nil})
				aadd(aItem, {'ABA_OSPARC','',Nil})
				aadd(aItem, {'ABA_SEQ'  ,FichaAtendimento:SequenciaAtendimentoOS,Nil})
				aadd(aItem, {'ABA_SUBOS','',Nil})
				aadd(aItem, {'ABA_ITEM',oItem:NumeroItem,Nil})
				aadd(aItem, {'ABA_LOCAL','01',Nil})
				aadd(aItem, {'ABA_GRVOS','N',Nil})
				aadd(aItem, {'ABA_CODTEC',padr(FichaAtendimento:CodigoTecnico,6),Nil})
				aadd(aItem, {'ABA_CODPRO',oItem:CodigoProdutoNovo,Nil})
				aadd(aItem, {'ABA_NUMSER',oItem:NumeroSeriepecas,Nil})
				aadd(aItem, {'ABA_QUANT' ,oItem:Quantidade,Nil})
				aadd(aItem, {'ABA_CODSER','999999',Nil})//serviço parceiro web criado novo
				aadd(aItem, {'ABA_LOTECT',ALLTRIM(oItem:Numerolote),Nil})
				aadd(aItem, {'ABA_CGROUP',cCodGrp,Nil}) 

				aadd(aItens, aItem)  
					
		   	ENDIF
		Next
	ELSEIF lAltera

		aadd(aCab, {'AB9_FILIAL',xfilial("AB9"),Nil})
		aadd(aCab, {'AB9_NUMOS',FichaAtendimento:NumeroatendimentoOS,Nil})
		aadd(aCab, {'AB9_OSPARC','',Nil})
		aadd(aCab, {'AB9_CODTEC',padr(FichaAtendimento:CodigoTecnico,6),Nil})
		aadd(aCab, {'AB9_SEQ',FichaAtendimento:SequenciaAtendimentoOS,Nil})
		aadd(aCab, {'AB9_DTCHEG',FichaAtendimento:DataInicio,Nil})
		aadd(aCab, {'AB9_HRCHEG',FichaAtendimento:HoraInicio,Nil})
		aadd(aCab, {'AB9_DTSAID',FichaAtendimento:DataFim,Nil})
		aadd(aCab, {'AB9_HRSAID',FichaAtendimento:HoraFim,Nil})
		aadd(aCab, {'AB9_DTINI',FichaAtendimento:DataInicio,Nil})
		aadd(aCab, {'AB9_HRINI',FichaAtendimento:HoraInicio,Nil})
		aadd(aCab, {'AB9_DTFIM',FichaAtendimento:DataFim,Nil})
		aadd(aCab, {'AB9_HRFIM',FichaAtendimento:HoraFim,Nil})
		aadd(aCab, {'AB9_CODPRB',FichaAtendimento:CodigoOcorrencia,Nil})
		IF ALLTRIM(FichaAtendimento:Codigogarantia)=="1"
			aadd(aCab, {'AB9_GARANT','S',Nil})
		ELSE
			aadd(aCab, {'AB9_GARANT','N',Nil})
		ENDIF
		aadd(aCab, {'AB9_TIPO',ALLTRIM(FichaAtendimento:CodigoSituacao),Nil})
		aadd(aCab, {'AB9_STARTAR',ALLTRIM(FichaAtendimento:CodigoSituacao),Nil})		
		aadd(aCab, {'AB9_INCOS',FichaAtendimento:Incluiitemos,Nil})
		//aadd(aCab, {'AB9_MEMO2',FichaAtendimento:Laudo,Nil})
		aadd(aCab, {'AB9_DEFCON',FichaAtendimento:DefeitoConstatado,Nil})
		aadd(aCab, {'AB9_CPROV',FichaAtendimento:CausaProvavel,Nil})
		aadd(aCab, {'AB9_SRVEXE',FichaAtendimento:ServicoExecutado,Nil})    
		aadd(aCab, {'AB9_CGROUP',cCodGrp,Nil})
		aadd(aCab, {'AB9_BLQATE',FichaAtendimento:BloqueioAtendimento,Nil}) 
		If !Empty(FichaAtendimento:AprovadoAtendimento) 
			aadd(aCab, {'AB9_APVROT',FichaAtendimento:AprovadoAtendimento,Nil}) 
		EndIf
		aadd(aCab, {'AB9_DEFEIT',FichaAtendimento:CodigoDefeito,Nil})
		aadd(aCab, {'AB9_SERVIC',FichaAtendimento:CodigoServico,Nil})
		//aadd(aCab, {'AB9_VERATU',FichaAtendimento:VersaoAtual,Nil}) 
		If !Empty(FichaAtendimento:VersaoSoftware) 
	   		aadd(aCab, {'AB9_VERSOF',FichaAtendimento:VersaoSoftware,Nil}) 
		EndIf  
		If !Empty(FichaAtendimento:Aprovado2Atendimento) 
			aadd(aCab, {'AB9_APROV2',FichaAtendimento:Aprovado2Atendimento,Nil}) 
		EndIf
	
		FOR n:=1 to len(FichaAtendimento:Itens)
			aItem:={}
			oItem:=FichaAtendimento:Itens[n]
			
			IF !EMPTY(oItem:CodigoProdutoNovo)
				aadd(aItem, {'ABA_FILIAL',XFILIAL("ABA"),Nil})
				aadd(aItem, {'ABA_NUMOS',FichaAtendimento:NumeroatendimentoOS,Nil})
				aadd(aItem, {'ABA_OSPARC','',Nil})
				aadd(aItem, {'ABA_SEQ'  ,FichaAtendimento:SequenciaAtendimentoOS,Nil})
				aadd(aItem, {'ABA_SUBOS','',Nil})
				aadd(aItem, {'ABA_ITEM',oItem:NumeroItem,Nil})
				aadd(aItem, {'ABA_LOCAL','01',Nil})
				aadd(aItem, {'ABA_GRVOS','N',Nil})
				aadd(aItem, {'ABA_CODTEC',padr(FichaAtendimento:CodigoTecnico,6),Nil})
				aadd(aItem, {'ABA_CODPRO',oItem:CodigoProdutoNovo,Nil})
				aadd(aItem, {'ABA_NUMSER',oItem:NumeroSeriepecas,Nil})
				aadd(aItem, {'ABA_QUANT' ,oItem:Quantidade,Nil})
				aadd(aItem, {'ABA_CODSER','999999',Nil})//serviço parceiro web criado novo
				aadd(aItem, {'ABA_LOTECT',ALLTRIM(oItem:Numerolote),Nil})
				aadd(aItem, {'ABA_NUMLOT',ALLTRIM(oItem:Numerolote),Nil})
				aadd(aItem, {'ABA_CGROUP',cCodGrp,Nil})

				IF ALLTRIM(oItem:D_E_L_E_T_)=='*'
					TCSQLEXEC("UPDATE " + RETSQLNAME("ABA") + " SET D_E_L_E_T_ = '*' WHERE ABA_FILIAL = '" + XFILIAL("ABA") + "' AND ABA_NUMOS = '"+ FichaAtendimento:NumeroAtendimentoOS +"' AND ABA_SEQ = '"+FichaAtendimento:SequenciaAtendimentoOS +"' AND ABA_ITEM = '"+oItem:NumeroItem +"' ")
				ELSE
					aadd(aItens, aItem)
				ENDIF
			ENDIF
		Next
		
	ENDIF

	lMsErroAuto := .F.
	If lInclui
		msExecAuto({|x,y| TECA460(x,Y, 3)}, aCab, aItens,3)
	ElseIf lAltera
		msExecAuto({|x,y| TECA460(x,Y, 4)}, aCab, aItens,4)
	EndIf
	If lMsErroAuto
		oRetorno:Sucesso:=.F.
		oRetorno:Codigo:=1
		oRetorno:Mensagem:=MemoRead(NOMEAUTOLOG())
		oRetorno:Chave:=""
		ferase(NOMEAUTOLOG())
	ELSE
		oRetorno:Sucesso:=.T.
		oRetorno:Codigo:=0
		oRetorno:Mensagem:="Atendimento " + FichaAtendimento:NumeroAtendimentoOS + FichaAtendimento:SequenciaAtendimentoOS + " gravado com sucesso."
		oRetorno:Chave:=xfilial("AB9") + FichaAtendimento:NumeroAtendimentoOS + FichaAtendimento:SequenciaAtendimentoOS //xfilial("AB9")                                                                          

	    	
		//TROCA BAIXADO POR ENCERRADO
		CCHAVE:=XFILIAL("AB6")+FichaAtendimento:NumeroatendimentoOS
        DBSELECTAREA("AB6")
		AB6->(DBSETORDER(1))//
		AB6->(DBSEEK(CCHAVE))		
		IF AB6->AB6_STATUS='B'
		RecLock("AB6",.F.)
			AB6->AB6_STATUS:="E" //TROCA BAIXADO POR ENCERRADO
		AB6->(MSUNLOCK())  
		       
		ENDIF
		
						//ATUALIZA NO ACESSORIO CRIADO O NUMERO DE LOTE  -- CRIAR INDICE 5 NA TABELA AA4
		IF !EMPTY(ALLTRIM(oItem:Numerolote))
			dbSelectArea("AA4")
			AA4->(DBSETORDER(5))
			AA4->(DBGOTOP())
			AA4->(dbSeek(XFILIAL("AA4")+FichaAtendimento:NumeroSerieequip))   
			WHILE AA4->(!EOF()) .AND. ALLTRIM(FichaAtendimento:NumeroSerieequip)==ALLTRIM(AA4->AA4_NUMSER)
			IF ALLTRIM(oItem:CodigoProdutoNovo)==ALLTRIM(AA4->AA4_PRODAC) 
				RecLock("AA4",.F.)
				AA4_LOTE:=ALLTRIM(oItem:Numerolote)
				AA4->(MSUNLOCK())
			ELSE  
			AA4->(DBSKIP())
			LOOP
			ENDIF	
			AA4->(DBSKIP())
			ENDDO  

	  	ENDIF  

		
	ENDIF
endif

::Retorno:=oRetorno
RpcClearEnv()
Return .T.
                                      