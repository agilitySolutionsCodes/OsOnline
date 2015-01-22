<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="AcessorioView.ascx.vb" Inherits="OrdemServico.AcessorioView" %>

<asp:Panel ID="pnlAcessorio" runat="server" Visible="true">
    <asp:TextBox ID="txtCodigo" runat="server" MaxLength="20" AutoPostBack="True"  /> &nbsp;
    <asp:Button ID="btnBusca" runat="server" CssClass="Lupa" ToolTip="Pesquisar" /> &nbsp;
    <asp:Label ID="lblDescricao" runat="server" CssClass="label2"  />
</asp:Panel>