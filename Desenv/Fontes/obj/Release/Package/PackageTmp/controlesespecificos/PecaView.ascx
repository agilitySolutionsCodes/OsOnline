<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="PecaView.ascx.vb" Inherits="OrdemServico.PecaView" %>

<asp:Panel ID="pnlProdutoSerie" runat="server" CssClass="ProdutoSeriePanel" ScrollBars="Auto">
    <asp:TextBox ID="txtSerie" runat="server" MaxLength="20" AutoPostBack="True"  /> &nbsp;
    <asp:Button ID="btnBusca" runat="server" CssClass="Lupa" ToolTip="Pesquisar" /> &nbsp;
    <asp:Label ID="lblDescricao" runat="server" CssClass="label2"  />
    <asp:Label ID="lblFound" runat="server" Visible="False" Text="False" />
    <asp:Image ID="imgNotFound" runat="server" ImageUrl="~/imagens/severidade.png" Visible="False" ToolTip="Não encontrado" />
    <asp:Label ID="lblCodigo" runat="server" Visible="false" />
</asp:Panel>
