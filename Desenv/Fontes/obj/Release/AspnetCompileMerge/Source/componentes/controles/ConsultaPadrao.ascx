<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ConsultaPadrao.ascx.vb"
    Inherits="OrdemServico.ConsultaPadrao" %>

<%@ Register Src="Mensagem.ascx" TagName="Mensagem" TagPrefix="uc1" %>
<%@ Register Src="AutoHideButton.ascx" TagName="AutoHideButton" TagPrefix="uc2" %>

<asp:Panel ID="pnlConsultaPadrao" runat="server">

            <asp:Panel ID="pnlParametros" runat="server"> &nbsp;
                <asp:DropDownList ID="drpTipoPesquisa" runat="server" CssClass="ParameterStyle" 
                        AutoPostBack="True" /> &nbsp;
                <asp:TextBox ID="txtParametroPesquisa" runat="server" CssClass="ParameterStyle" /> &nbsp;
                <uc2:AutoHideButton ID="wucAutoHideButton" runat="server" Text="Pesquisar" CssButton="button" />

            </asp:Panel> 

            <uc1:Mensagem ID="oMensagem" runat="server" />
    
                <asp:GridView ID="grdPesquisar" runat="server" AllowPaging="True" AllowSorting="True"
                                AutoGenerateSelectButton="True" PageSize="20" CssClass="GridViewStyle">
                    <HeaderStyle CssClass="Resultado HeaderStyle" />
                    <AlternatingRowStyle CssClass="Resultado AlternatingRowStyle" />
                    <RowStyle CssClass="Resultado RowStyle" />
                </asp:GridView>            

</asp:Panel>
