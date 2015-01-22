<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="BaseInstaladaView.ascx.vb" Inherits="OrdemServico.BaseInstaladaView" %>

<%@ Register Src="../componentes/controles/DetalheConsultaPadrao.ascx" TagName="DetalheConsultaPadrao" TagPrefix="uc1" %>

<asp:Label ID="lblFound" runat="server" Visible="False" Text="False" />

<asp:UpdatePanel ID="updCabecalho" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true" RenderMode="Inline">
    
    <ContentTemplate>
        
        <asp:Panel ID="pnlEdicao" runat="server" Visible="true" CssClass="inlineButton">
            <asp:TextBox ID="txtSerie" runat="server" Columns="15" MaxLength="20" 
                AutoPostBack="True" />
            <asp:Button ID="btnBusca" runat="server" CssClass="Lupa" ToolTip="Pesquisar" />
            <asp:Image ID="imgNotFound" runat="server" ImageUrl="~/imagens/severidade.png" Visible="False" ToolTip="Não encontrado" />
            <asp:Label ID="lblNome2" runat="server" CssClass="label2" />
            <asp:Label ID="lblGarantia" runat="server" CssClass="label2" />
        </asp:Panel>

        <asp:Panel ID="pnlVisualizacao" runat="server" Visible="false" CssClass="inlineButton"> 
            <asp:Label ID="lblSerie" runat="server" /> -
            <asp:Label ID="lblNome" runat="server" />
            <asp:Label ID="lblCodigo" runat="server" />
            <asp:Label ID="lblGarantia2" runat="server" />
        </asp:Panel>
        
        <uc1:DetalheConsultaPadrao ID="oDetalhe" runat="server" Procedure="OrdemServico.ctlBaseInstalada" />
    
    </ContentTemplate>

</asp:UpdatePanel>