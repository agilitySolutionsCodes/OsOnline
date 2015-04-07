<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="PopupBox.ascx.vb" Inherits="OrdemServico.PopupBox" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Panel ID="pnlMensagem" runat="server" DefaultButton="btnOk" BackColor="White"
            Width="45%" BorderStyle="Ridge">

    <asp:Panel ID="pnlCabecalho" runat="server" HorizontalAlign="Center">
        <h3>
            <asp:Label ID="lblTitulo" runat="server" meta:resourcekey="lblTitulo" />
        </h3>
    </asp:Panel>
    
    <asp:Panel ID="pnlCorpo" runat="server" CssClass="PopupBox">
        
        <asp:Label ID="lblMensagem" runat="server" Text="Defina o modelo de impressão." /> <br /> <br />
        
        <asp:Label ID="Label1" runat="server" Visible="false" />
        <asp:RadioButton ID="rdbOpcao1" runat="server" GroupName="Grupo1" Text="Aprovador/Técnico  <br />"/>
        <asp:Label ID="lblValor1" runat="server" Visible="false" />

        <asp:Label ID="Label2" runat="server" Visible="false" />
        <asp:RadioButton ID="rdbOpcao2" runat="server" GroupName="Grupo1" Text="Cliente/Aprovador/Técnico"/>
        <asp:Label ID="lblValor2" runat="server" Visible="false" />
    
    </asp:Panel>

    <asp:Panel ID="pnlRodape" runat="server" HorizontalAlign="Center"> <br />
        <asp:Button ID="btnOk" runat="server" CssClass="Button" Text="Ok"/> <br /> <br />
    </asp:Panel>
    
    <asp:Button runat="server" ID="HiddenForModal" Style="display: none" text="Ok" />
    
    <asp:ModalPopupExtender ID="HiddenForModal_ModalPopupExtender" runat="server" CancelControlID=""
        DynamicServicePath="" Enabled="True" OkControlID="" PopupControlID="pnlMensagem"
        TargetControlID="HiddenForModal" BackgroundCssClass="ModalPopup"></asp:ModalPopupExtender>

</asp:Panel>
