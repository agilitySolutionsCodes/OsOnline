<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPrint.Master" CodeBehind="ImpressaoAtendimento.aspx.vb" Inherits="OrdemServico.ImpressaoAtendimento" %>

<%@ Register Src="componentes/controles/Mensagem.ascx" TagName="Mensagem" TagPrefix="uc6" %>
<%@ Register Src="componentes/controles/AutoHideButton.ascx" TagName="AutoHideButton" TagPrefix="uc7" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language="JavaScript">
        function Imprimir() {
            document.getElementById("tbBotoes").setAttribute("style", "display:none");
            self.print();
            document.getElementById("tbBotoes").setAttribute("style", "display:inherit");
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<table border="0" width="100%" style="color: #000000">

    <tr>
        <td colspan="4">  <br/>
            <asp:Label ID="lblTitEmpresa" runat="server" Text="Intermed Equip. Med. Hosp. Ltda - " cssClass="label" /> &nbsp;
            <asp:Label ID="lblTitTitulo" runat="server" text="Ficha de Atendimento da OS" /> <br /> <br />
            <asp:Label ID="lblTitNumero" runat="server" Text="Número: " CssClass="label" />
            <asp:Label ID="lblNumero" runat="server" Text="&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;" /> &nbsp;
            <asp:Label ID="lblTitSequencia" runat="server" Text="Sequência: " CssClass="label" />
            <asp:Label ID="lblSequencia" runat="server" /> <br/> 
        </td>
        <td  dir="rtl" class="style2">
            <asp:Image ID="imgLogoIntermed" runat="server" Height="52px" 
                       ImageUrl="~/App_Themes/padrao/Logo.gif" /> &nbsp;
        </td>
    </tr >

    <tr>
        <td colspan="5"> 
        ___________________________________________________________________________________________________<br/> <br/>
            <asp:Label ID="lblTitCliente" runat="server" Text="Cliente: " CssClass="label" />
            <asp:Label ID="lblCliente" runat="server" cssClass="label2" /> <br/> <br/>
            
            <asp:Label ID="lblTitEndereco" runat="server" Text="Endereço: " CssClass="label" />
            <asp:Label ID="lblEndereco" runat="server" cssClass="label2" /> <br/> <br/>
            
            <asp:Label ID="lblTitContato" runat="server" Text="Contato: " CssClass="label" />
            <asp:Label ID="lblContato" runat="server" cssClass="label2" Text="&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;"/>  &nbsp;

            <asp:Label ID="lblTitTelefone" runat="server" Text="Telefone: " CssClass="label" />
            <asp:Label ID="lblTelefone" runat="server" /> <br/> <br/>
            
            <asp:Label ID="lblTitChamado" runat="server" Text="Chamado: " CssClass="label" />
            <asp:Label ID="lblChamado" runat="server" text="&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;"/> &nbsp;

            <asp:Label ID="lblTitDataChamado" runat="server" Text="Dt. Chamado: " CssClass="label" />
            <asp:Label ID="lblDataChamado" runat="server" Text="&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;"/> &nbsp;

            <asp:Label ID="lblTitHoraChamado" runat="server" Text="Hora: " CssClass="label" />
            <asp:Label ID="lblHoraChamado" runat="server" /> <br/> <br/>

            <asp:Label ID="lblTitObservacao" runat="server" Text="Observação: " CssClass="label" />
            <asp:Label ID="lblObservacao" runat="server" /> <br/> <br/>

            <asp:Label ID="lblTitProduto" runat="server" Text="Produto: " CssClass="label" />
            <asp:Label ID="lblProduto" runat="server" cssClass="label2" Text="&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;"/> &nbsp;
            <asp:Label ID="lblTitSerie" runat="server" Text="Nr. Série: " CssClass="label" />
            <asp:Label ID="lblSerie" runat="server" /> <br/> <br/>   

            <asp:Label ID="lblTitOcorrencia" runat="server" Text="Ocorrência: " CssClass="label" />
            <asp:Label ID="lblOcorrencia" runat="server" cssClass="label2" Text="&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;" /> &nbsp;
                <asp:Label ID="lblCodigoOcorrencia" runat="server" visible="false" />
            <asp:Label ID="lblTitGarantia" runat="server" Text="Garantia: " CssClass="label" />
            <asp:Label ID="lblGarantia" runat="server" cssClass="label2" />  <br/>
        ___________________________________________________________________________________________________</td>
    </tr>

    <tr>
        <td colspan="5"> <br/>
             
                <asp:Label ID="lblTitDataInicio" runat="server" CssClass="label" Text="Data Início: " />  
                <asp:Label ID="lblDataInicio" runat="server" Text=""/>&nbsp;&nbsp;
                <asp:Label ID="lblTitHoraInicio" runat="server" CssClass="label" Text="Hora Início: " />  
                <asp:Label ID="lblHoraInicio" runat="server" CssClass="textBox2" Text=""/>&nbsp;&nbsp; 
            
                <asp:Label ID="lblTitDataTermino" runat="server" CssClass="label" Text="Data Termino: " /> 
                <asp:Label ID="lblDataTermino" runat="server" Text="" />&nbsp;&nbsp;
                <asp:Label ID="lblTitHoraTermino" runat="server" CssClass="label" Text="Hora Termino: " /> 
                <asp:Label ID="lblHoraTermino" runat="server" CssClass="textBox2" /> <br/> <br/>
                ___________________________________________________________________________________________________
        </td>

    </tr>

    <tr>
        <td colspan="5"> <br/> 

            <asp:Label ID="lblTitDefeito" runat="server" CssClass="label" Text="Defeito:" /> 
            <asp:Label ID="lblDefeito" runat="server"  /> <br/> <br/>

            <asp:Label ID="lblTitCausa" runat="server" CssClass="label" Text="Causa:" /> 
            <asp:Label ID="lblCausa" runat="server" /> <br/> <br/>

            <asp:Label ID="lblTitServExecutado" runat="server" CssClass="label" Text="Serv. Executado:" />
            <asp:Label ID="lblServExecutado" runat="server" /> <br/> <br/> 
        ___________________________________________________________________________________________________</td>
    </tr>

    <tr>
        <td colspan="5"> <br/> 
            <asp:GridView ID="grdItens" runat="server" AutoGenerateColumns="False" EnableModelValidation="True" 
                          HeaderStyle-HorizontalAlign="NotSet" ShowFooter="True" 
                BorderStyle="None" GridLines="Vertical" Width="100%" >
                <Columns>
                    <asp:TemplateField HeaderText="Qtde.">
                        <ItemTemplate>
                            <asp:Label ID="lblQuantidade" runat="server" Text='<%# Bind("Quantidade") %>' 
                                       CssClass="label2">
                            </asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Código">
                        <ItemTemplate>
                            <asp:Label ID="lblCodigo" runat="server" Text='<%# Bind("CodigoItem") %>' 
                                       CssClass="label2">
                            </asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Descrição">
                        <ItemTemplate>
                            <asp:Label ID="lblProduto" runat="server" Text='<%# Bind("DescricaoItem") %>' 
                                       CssClass="label2">
                            </asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Nr. Série">
                        <ItemTemplate>
                            <asp:Label ID="lblSerie" runat="server" Text='<%# Bind("NumeroSeriePeca") %>' 
                                       CssClass="label2">
                            </asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Lote">
                        <ItemTemplate>
                            <asp:Label ID="lblLote" runat="server" Text='<%# Bind("NumeroLote") %>' 
                                       CssClass="label2">
                            </asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle HorizontalAlign="Left" />
                <PagerStyle BorderColor="#003300" />
                <RowStyle HorizontalAlign="Left" />
            </asp:GridView>
        ___________________________________________________________________________________________________ 
                <br/> <br/> <br/> <br/> <br/> 
            </td> 
    </tr>

    <tr> 
        <td>  
            <asp:Panel ID="pnlCliente" runat="server">
                <asp:Label ID="lblTitAssCli" runat="server"  Text="________________________"/> <br/>
                <asp:Label ID="lblAssCli" runat="server" CssClass="label" Text="Cliente:" />        
            </asp:Panel>
        </td>
        <td>    
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        </td>
        <td> 
            <asp:Label ID="lblAprovador" runat="server" Text="________________________"/> <br/>
            <asp:Label ID="lblTitAprovador" runat="server" CssClass="label" Text="Aprovador:" />
        </td>
        <td>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        </td>
        <td>
            <asp:Label ID="Label1" runat="server"  Text="________________________"/> <br/>
            <asp:Label ID="Label2" runat="server" CssClass="label" Text="Técnico:" />        
        </td>
    </tr>

</table>

    <asp:Panel ID="pnlBotoes" runat="server" width="100%"> 
        <table id="tbBotoes" width="100%">
            <caption>
                <br />
                <tr>
                    <td>
                        <input id="btnImprimir" type="button" value="Imprimir" onclick="Imprimir()" class="button"/>
                    </td>
                    <td dir="rtl">
                        <uc7:AutoHideButton ID="btnVoltar" runat="server" CssButton="button" Text="Voltar" />
                    </td>
                </tr>
            </caption>
        </table>
    </asp:Panel>

    <asp:Panel runat="server" ID="pnlMensagm">
        <uc6:Mensagem ID="oMensagem" runat="server" />
    </asp:Panel>

</asp:Content>
