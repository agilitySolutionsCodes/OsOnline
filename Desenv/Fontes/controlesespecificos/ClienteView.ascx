<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ClienteView.ascx.vb" Inherits="OrdemServico.ClienteView" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../componentes/controles/DetalheConsultaPadrao.ascx" TagName="DetalheConsultaPadrao" TagPrefix="uc1" %>

<asp:Label ID="lblFound" runat="server" Visible="False" Text="False" />

<asp:UpdatePanel ID="updCabecalho" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true" RenderMode="Inline">

    <ContentTemplate>

        <asp:Panel ID="pnlEdicao" runat="server" CssClass="inlineButtonAuto">

                    <asp:TextBox ID="txtCodigo" runat="server" Columns="6" MaxLength="6" Visible="False" />
                    <asp:Label runat="server" ID="lblBarra" Text="/" />
                    <asp:TextBox ID="txtLoja" runat="server" Columns="2" MaxLength="2" Visible="False" />
                    <asp:TextBox ID="txtCnpj" runat="server" Width="170px" Height="16px" ValidationGroup="MKE" />
                    <cc1:MaskedEditExtender ID="txtCnpj_MaskedEditExtender" runat="server"
                        TargetControlID="txtCnpj" 
                        Mask="99,999,999/9999-99"
                        MessageValidatorTip="true"
                        OnFocusCssClass="MaskedEditFocus"
                        OnInvalidCssClass="MaskedEditError"
                        MaskType="None"
                        AcceptAMPM="False"
                        ErrorTooltipEnabled="True" />
                    <cc1:MaskedEditValidator ID="txtCnpj_MaskedEditValidator" runat="server"
                        ControlExtender="txtCnpj_MaskedEditExtender"
                        ControlToValidate="txtCnpj"
                        IsValidEmpty="False"
                        EmptyValueMessage=""
                        InvalidValueMessage=""
                        Display="Static"
                        TooltipMessage=""
                        EmptyValueBlurredText=""
                        InvalidValueBlurredMessage=""
                        ValidationGroup="MKE"/>
                    <asp:Button ID="btnBusca" runat="server" CssClass="Lupa" ToolTip="Pesquisar" Enabled="True"/>
                    <asp:Image ID="imgNotFound" runat="server" ImageUrl="~/imagens/severidade.png" Visible="False" ToolTip="Não encontrado" />
                    <asp:Label ID="lblNome" runat="server" Text="" CssClass="ClienteViewLabel"/>                

        </asp:Panel>

        <uc1:DetalheConsultaPadrao ID="oDetalhe" runat="server" Procedure="OrdemServico.ctlCliente" />               

    </ContentTemplate>

</asp:UpdatePanel>

