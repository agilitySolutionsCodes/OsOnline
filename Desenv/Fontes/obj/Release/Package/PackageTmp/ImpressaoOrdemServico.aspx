<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPrint.Master" CodeBehind="ImpressaoOrdemServico.aspx.vb" Inherits="OrdemServico.ImpressaoOrdemServico" %>

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
    <style type="text/css">
        .style1
        {
            width: 407px;
        }
        .style2
        {
            width: 4px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<table border="0" width="100%" style="color: #000000">
    
    <tr>
   
        <td colspan="4">  <br/>
            <asp:Label ID="lblTitEmpresa" runat="server" Text="Intermed Equip. Med. Hosp. Ltda - " cssClass="label" /> &nbsp;
            <asp:Label ID="lblTitTitulo" runat="server" text="Ordem de Serviço" /> &nbsp;
            <asp:Label ID="lblTitNumero" runat="server" Text="Número: " CssClass="label" />
            <asp:Label ID="lblNumero" runat="server" /> <br/> <br/> 
        </td>
        <td  dir="rtl" class="style2">
            <asp:Image ID="imgLogoIntermed" runat="server" Height="52px" 
                       ImageUrl="~/App_Themes/padrao/Logo.jpg" /> &nbsp;
        </td>
    </tr >

    <tr>
   
        <td colspan="5"> 
        ___________________________________________________________________________________________________
                <br /> <br />
            
            <asp:Label ID="lblTitCliente" runat="server" Text="Cliente: " CssClass="label" />
            <asp:Label ID="lblCliente" runat="server" cssClass="label2" /> <br/> <br/>
            
            <asp:Label ID="lblTitDataEmissao" runat="server" Text="Emissão: " CssClass="label" />
            <asp:Label ID="lblDataEmissao" runat="server" /> <br/> <br/>
            
            <asp:Label ID="lblTitAtendente" runat="server" Text="Atendente: " CssClass="label" />
            <asp:Label ID="lblAtendente" runat="server" /> <br/> <br/>

            <!-- <asp:Label ID="lblTitCondicaoPagamento" runat="server" Text="Cond. Pagamento: " CssClass="label" />
            <asp:Label ID="lblCondicaoPagamento" runat="server" CssClass="label2" /> <br/> <br/>

            <asp:Label ID="lblTitDesconto" runat="server" Text="Desconto(s): " CssClass="label" />
            <asp:Label ID="lblDesconto" runat="server" /> <br/> <br/> -->
        ___________________________________________________________________________________________________
                </td>
    </tr>

    <tr>
        <td colspan="5"> <br/> 
            <asp:GridView ID="grdItens" runat="server" AutoGenerateColumns="False" EnableModelValidation="True" ShowFooter="True" 
                                BorderStyle="None" GridLines="Vertical" Width="100%" >
                <Columns>
                    <asp:TemplateField HeaderText="Item">
                        <ItemTemplate>
                            <asp:Label ID="lblItem" runat="server" Text='<%# Bind("ItemOS") %>' 
                                       CssClass="label2">
                            </asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Situacao" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblSituacaoOS" runat="server" Text='<%# Bind("DescricaoSituacaoOS") %>' 
                                       CssClass="label2">
                            </asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Produto">
                        <ItemTemplate>
                            <asp:Label ID="lblProduto" runat="server" Text='<%# Bind("DescricaoProduto") %>' 
                                       CssClass="label2">
                            </asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Série">
                        <ItemTemplate>
                            <asp:Label ID="lblSerie" runat="server" Text='<%# Bind("NumeroSerieProduto") %>' 
                                       CssClass="label2">
                            </asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Ocorrência">
                        <ItemTemplate>
                            <asp:Label ID="lblProblema" runat="server" Text='<%# Bind("DescricaoOcorrencia") %>' 
                                       CssClass="label2">
                            </asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle HorizontalAlign="NotSet" />
                <PagerStyle BorderColor="#003300" />
                <RowStyle HorizontalAlign="Left" />
            </asp:GridView>
        ___________________________________________________________________________________________________
                </td>
    </tr>
    <!--
    <tr>
        <td colspan="5" dir="lft"> <br/>

            <asp:Label ID="lblTitTotal" runat="server" CssClass="label" Text="Valor Total: " /> &nbsp;
            <asp:Label ID="lblTotal" runat="server" CssClass="label2" Text="" /> <br/> <br/> 
        ___________________________________________________________________________________________________
                </td>
    </tr>
    -->
    <tr>
        <td colspan="5"> <br/>

            <asp:Label ID="lblObservação" runat="server" CssClass="label2" Text="Obs: A Intermed Equipamento Médico Hospitalar Ltda realizou a prestação de serviço conforme RS - Registro(s) de Serviço(s) em anexo à este documento." /> <br/> <br/> 
        ___________________________________________________________________________________________________ 
        <br/> <br/> <br/> <br/> <br/> <br/> <br/> <br/> <br/> <br/> <br/> <br/> <br/> 
                </td> 
    </tr>

    
    
    <tr> 
        <td>
            <asp:Label ID="lblTitAssCli" runat="server"  Text="________________________"/> <br/>
            <asp:Label ID="lblAssCli" runat="server" CssClass="label" Text="Cliente:" />        
        </td>
        <td>
        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
        </td>
        <td>
            <asp:Label ID="lblAprovador" runat="server" Text="________________________"/> <br/>
            <asp:Label ID="lblTitAprovador" runat="server" CssClass="label" Text="Aprovador:" />
        </td>
        <td>
        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
        </td>
        <td>
            <asp:Label ID="Label1" runat="server"  Text="________________________"/> <br/>
            <asp:Label ID="Label2" runat="server" CssClass="label" Text="Técnico:" />        
        </td>
    </tr>

</table> 

    <asp:Panel ID="pnlBotoes" runat="server" > 
        <table id="tbBotoes" class="style3" Width="100%">
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
        </table> <br/>
    </asp:Panel>
    <br/>

    <asp:Panel runat="server" ID="pnlMensagm">
        <uc6:Mensagem ID="oMensagem" runat="server" />
    </asp:Panel>

</asp:Content>
