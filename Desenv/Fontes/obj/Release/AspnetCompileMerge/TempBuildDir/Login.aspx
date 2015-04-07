<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Login.aspx.vb" Inherits="OrdemServico.Login" MasterPageFile="~/MasterPage.Master" %>

<%@ MasterType VirtualPath="~/SubMaster.Master" %>
<%@ Register Src="componentes/controles/Mensagem.ascx" TagName="Mensagem" TagPrefix="uc1" %>

<asp:Content ID="Principal" ContentPlaceHolderID="cMaster" runat="Server">
    <asp:Label ID="lblTitulo" runat="server" Text="Login" CssClass="label" />
    <br />
    <br />
    <asp:Login ID="objLogin" runat="server" DisplayRememberMe="False" FailureText="Usuário e senha não conferem. Por favor, tente novamente."
        LoginButtonText="Confirmar" Orientation="Horizontal" PasswordLabelText="Senha:"
        PasswordRequiredErrorMessage="Por favor, preencha a senha." TextLayout="TextOnTop"
        TitleText="" UserNameLabelText="Usuário:" UserNameRequiredErrorMessage="Por favor, preencha o usuário.">
        <TextBoxStyle CssClass="txt" />
        <LoginButtonStyle CssClass="button" />
        <LabelStyle CssClass="label" />
    </asp:Login>
    <asp:ValidationSummary ID="oMsgErro" runat="server" HeaderText="Erros encontrados:"
        DisplayMode="BulletList"
        EnableClientScript="True" ValidationGroup="objLogin" />

    <uc1:Mensagem ID="oMensagem" runat="server" />
</asp:Content>
