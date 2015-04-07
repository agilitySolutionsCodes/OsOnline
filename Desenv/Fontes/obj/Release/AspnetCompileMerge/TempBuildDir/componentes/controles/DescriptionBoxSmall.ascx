<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="DescriptionBoxSmall.ascx.vb" Inherits="OrdemServico.DescriptionBoxSmall" %>

<%@ Register src="AutoHideButton.ascx" tagname="AutoHideButton" tagprefix="uc7" %>

<asp:ImageButton ID="imgDescricao" runat="server" ImageUrl="~/imagens/Nota.png" ToolTip="" Width="18px" Height="18px" />

<asp:Panel ID="pnlDescricao" runat="server" CssClass="DescriptionBox" Visible="false">

    <asp:Panel ID="pnlTopBar" runat="server" CssClass="TopBar">
        <asp:ImageButton ID="imgFechar" runat="server" ImageAlign="Right" ImageUrl="~/imagens/fechar.jpg" 
                         ToolTip="Fechar e Salvar" /> &nbsp;
        <asp:Label ID="lblTopBar" runat="server" />
    </asp:Panel>

    <asp:TextBox ID="txtDescricao" runat="server" Height="220px" TextMode="MultiLine" Width="480px" /> <br/>
    <uc7:AutoHideButton ID="btnFechar" runat="server" Text="Fechar" CssButton="button" /> <br/>

</asp:Panel>
