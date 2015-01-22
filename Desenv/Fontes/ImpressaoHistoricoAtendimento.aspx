<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPrint.Master" CodeBehind="ImpressaoHistoricoAtendimento.aspx.vb" Inherits="OrdemServico.ImpressaoHistoricoAtendimento" %>

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
                <asp:Label ID="lblTitEmpresa" runat="server" Text="Intermed Equip. Med. Hosp. Ltda " cssClass="label" /><br /> 
                <asp:Label ID="lblTitTitulo" runat="server" text="Lista de todos os atendimentos contendo o número de série: " />
                <asp:Label ID="lblSerie" runat="server" /> 
            </td>
            <td  dir="rtl" class="style2">
                <asp:Image ID="imgLogoIntermed" runat="server" Height="52px" 
                           ImageUrl="~/App_Themes/padrao/Logo.jpg" /> &nbsp;
            </td>
        </tr >
    </table>

    <asp:Literal ID="litConteudo" runat="server"></asp:Literal>

    <br /><br /><br />
    <asp:Panel ID="pnlBotoes" runat="server" width="100%"> 
        <table id="tbBotoes" width="100%">
            <caption>
                <tr>
                    <td >
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

