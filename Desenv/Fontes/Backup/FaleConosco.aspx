<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FaleConosco.aspx.vb" Inherits="OrdemServico.FaleConosco" MasterPageFile="~/SubMaster.Master" %>


<%@ Register Src="componentes/controles/Mensagem.ascx" TagName="Mensagem" TagPrefix="uc6" %>
<%@ Register Src="componentes/controles/AutoHideButton.ascx" TagName="AutoHideButton" TagPrefix="uc7" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SubMaster" runat="server">
    <br /><br /> 
   
    &nbsp;&nbsp;<asp:Label ID="Label1" runat="server" CssClass="label" Text="Nome:" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:TextBox ID="txtNome" runat="server" Width="400px" />
    <br/><br/>
    &nbsp;&nbsp;<asp:Label ID="Label2" runat="server" CssClass="label" Text="E-mail:" />&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:TextBox ID="txtEmail" runat="server" Width="400px" />
    <br/><br/> 
     &nbsp;&nbsp;<asp:Label ID="lblAssunto" runat="server" CssClass="label" Text="Assunto:" />&nbsp;&nbsp;
    <asp:DropDownList ID="drpAssunto" runat="server" CssClass="drop">
        <asp:ListItem Text="Selecione" Value="" />
        <asp:ListItem Text="Regulatoria" Value="R" />
        <asp:ListItem Text="Pós-Vendas" Value="PV" />
        <asp:ListItem Text="Suporte Técnico" Value="S" />
    </asp:DropDownList>
    <br/><br/><br /> 

    &nbsp;&nbsp;<asp:Label ID="lblDescricao" runat="server" CssClass="label" Text="Descrição:" /> <br/>
    &nbsp;&nbsp;<asp:TextBox ID="txtDescricao" runat="server" TextMode="MultiLine" CssClass="textArea3" /> 
    <br/> <br/> <br /><br />
              
    <asp:Panel ID="pnlBotoes" runat="server"> 
        <table width="100%">
            <tr>
                <td>
                    &nbsp;&nbsp;<uc7:AutoHideButton ID="btnEnviar" runat="server" CssButton="button" Text="Enviar" />
                </td>
            </tr>
        </table>
    </asp:Panel> <br />

    <asp:Panel runat="server" ID="pnlMensagm">
        <uc6:Mensagem ID="oMensagem" runat="server" />
    </asp:Panel>

</asp:Content>
