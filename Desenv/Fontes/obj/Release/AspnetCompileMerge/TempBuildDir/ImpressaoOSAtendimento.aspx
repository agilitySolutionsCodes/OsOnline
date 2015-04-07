<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPrint.Master" CodeBehind="ImpressaoOSAtendimento.aspx.vb" Inherits="OrdemServico.ImpressaoOSAtendimento" %>

<%@ Register Src="componentes/controles/Mensagem.ascx" TagName="Mensagem" TagPrefix="uc6" %>
<%@ Register Src="componentes/controles/AutoHideButton.ascx" TagName="AutoHideButton" TagPrefix="uc7" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" language="JavaScript">
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
        <td>  <br/>
            <asp:Label ID="lblTitEmpresa" runat="server" Text="Intermed Equip. Med. Hosp. Ltda " cssClass="label" /> <br />
            <asp:Label ID="lblTitTitulo" runat="server" text="Lista de Atendimentos da OS nº:" />&nbsp;
            <asp:Label ID="lblNumero" runat="server" Text="" /> 
        </td>
        <td  dir="rtl" class="style2">
            <asp:Image ID="imgLogoIntermed" runat="server" Height="52px" 
                       ImageUrl="~/App_Themes/padrao/Logo.gif" /> &nbsp;
        </td>
    </tr >

    <tr>
        <td colspan="2"><hr /><br/>
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
        </td>
    </tr>
</table>

<asp:Literal ID="litConteudo" runat="server"></asp:Literal>

<br /><br />
 <table border="0" width="100%" style="color: #000000"> 

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
<br />
<asp:Panel ID="pnlBotoes" runat="server" width="100%"> 
    <table id="tbBotoes" width="100%">
        <caption>
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
