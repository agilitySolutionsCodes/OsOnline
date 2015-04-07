<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="DescriptionBox.ascx.vb" Inherits="OrdemServico.DescriptionBox" %>

<%@ Register src="AutoHideButton.ascx" tagname="AutoHideButton" tagprefix="uc7" %>

<asp:ImageButton ID="imgDescricao" runat="server" ImageUrl="~/imagens/Nota.png" ToolTip="" Width="18px" Height="18px" />

<asp:Panel ID="pnlDescricao" runat="server" CssClass="DescriptionBox" Visible="false">

    <asp:Panel ID="pnlTopBar" runat="server" CssClass="TopBar">
        <asp:ImageButton ID="imgFechar" runat="server" ImageAlign="Right" ImageUrl="~/imagens/fechar.jpg" 
                         ToolTip="Fechar e Salvar" /> &nbsp;
        <asp:Label ID="lblTopBar" runat="server" />
    </asp:Panel>

    <asp:TextBox ID="txtDescricao" runat="server" Height="348px" TextMode="MultiLine" Width="800px" /> <br/>
    <uc7:AutoHideButton ID="btnConfirmar" runat="server" Text="Confirmar" CssButton="button" /> <br/>

</asp:Panel>
