<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="PopupBoxYesNo.ascx.vb" Inherits="OrdemServico.PopupBoxYesNo" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<asp:Panel ID="pnlMensagem" runat="server" BackColor="White" Width="45%" BorderStyle="Ridge">

    <asp:Panel ID="pnlCabecalho" runat="server" HorizontalAlign="Center">
        <h3>
            <asp:Label ID="lblTitulo" runat="server" meta:resourcekey="lblTitulo" />
        </h3>
    </asp:Panel>
    
    <asp:Panel ID="pnlCorpo" runat="server" CssClass="PopupBox">
        
        <asp:Label ID="lblMensagem" runat="server" Text="Geração ROT" /> <br /> <br />
        <asp:HiddenField ID="txtSelecionadoSim" runat="server" />
        
    </asp:Panel>

    <asp:Panel ID="pnlRodape" runat="server" HorizontalAlign="Center"> <br />
        <asp:Button ID="btnSim" runat="server" Text="Sim" CssClass="Button"  />   
        <asp:Button ID="btnNao" runat="server" Text="Não" CssClass="Button" /> <br /> <br />
    </asp:Panel>

    <asp:Button runat="server" ID="HiddenForModal" Style="display: none" text="Ok" />
    
    <asp:ModalPopupExtender ID="HiddenForModal_ModalPopupExtender" runat="server" CancelControlID=""
        DynamicServicePath="" Enabled="True" OkControlID="" PopupControlID="pnlMensagem"
        TargetControlID="HiddenForModal" BackgroundCssClass="ModalPopup"></asp:ModalPopupExtender>

</asp:Panel>

