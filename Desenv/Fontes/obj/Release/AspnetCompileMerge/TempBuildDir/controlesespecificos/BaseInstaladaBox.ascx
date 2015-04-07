<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="BaseInstaladaBox.ascx.vb" Inherits="OrdemServico.BaseInstaladaBox" %>

<asp:Panel ID="pnlBaseInstaladaBox" runat="server" CssClass="inlineButton">
    <asp:Label ID="lblSerie" runat="server" /> -
    <asp:Label ID="lblNome" runat="server" />
    <asp:Label ID="lblGarantia" runat="server" />
</asp:Panel>
<asp:Label ID="lblCodigo" runat="server" Visible="false"/>
