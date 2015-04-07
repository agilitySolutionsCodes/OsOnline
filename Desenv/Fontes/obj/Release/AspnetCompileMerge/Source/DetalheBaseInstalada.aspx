<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/SubMaster.Master" CodeBehind="DetalheBaseInstalada.aspx.vb" Inherits="OrdemServico.DetalheBaseInstalada" %>

<%@ Register Src="controlesespecificos/ClienteView.ascx" TagName="ClienteView" TagPrefix="uc1" %>
<%@ Register Src="componentes/controles/NumberBox.ascx" TagName="NumberBox" TagPrefix="uc4" %>
<%@ Register Src="componentes/controles/DateBox.ascx" TagName="DateBox" TagPrefix="uc5" %>
<%@ Register Src="componentes/controles/Mensagem.ascx" TagName="Mensagem" TagPrefix="uc6" %>
<%@ Register Src="componentes/controles/AutoHideButton.ascx" TagName="AutoHideButton" TagPrefix="uc7" %>
<%@ Register Src="controlesespecificos/ProdutoView.ascx" TagName="ProdutoView" TagPrefix="uc3" %>
<%@ Register Src="controlesespecificos/ProdutoBox.ascx" TagName="ProdutoBox" TagPrefix="uc8" %>
<%@ Register Src="controlesespecificos/AcessorioView.ascx" TagName="AcessorioView" TagPrefix="uc9" %>
<%@ Register Src="controlesespecificos/BaseInstaladaBox.ascx" TagName="BaseInstaladaBox" TagPrefix="uc10" %>
<%@ Register Src="controlesespecificos/BaseInstaladaView.ascx" TagName="BaseInstaladaView" TagPrefix="uc11" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SubMaster" runat="server">

    <br />
    <asp:Panel ID="pnlCabecalho" runat="server">
        <br />

        <asp:Label ID="lblTitSerie" runat="server" CssClass="label" Text="Série: " />
        <asp:TextBox ID="txtSerie" runat="server" />
        &nbsp;
        
        <asp:Label ID="lblTitEquipamento" runat="server" CssClass="label" Text="Equipamento: " />
        <asp:Label ID="lblProduto" runat="server" /><br />
        <br />

        <asp:Label ID="lblTitLote" runat="server" CssClass="label" Text="Lote: " />
        <asp:TextBox ID="txtLote" runat="server" />
        &nbsp;
       
        <asp:Label ID="lblTitStatus" runat="server" CssClass="label" Text="Status:" />
        <asp:DropDownList ID="drpStatus" runat="server" DataTextField="DescricaoStatus" DataValueField="CodigoStatus" CssClass="drop" />
        <br />
        <br />

        <asp:Label ID="lblTitEndereco" runat="server" CssClass="label" Text="Endereço:" />
        <br />
        <asp:TextBox ID="txtEndereco" runat="server" TextMode="MultiLine" CssClass="textArea2" />
        <br />
        <br />

        <asp:Label ID="lblCidade" runat="server" CssClass="label" Text="Cidade:" />
        &nbsp;
        <asp:TextBox ID="txtCidade" runat="server" />
        &nbsp;&nbsp;&nbsp;&nbsp;

        <asp:Label ID="lblEstadoTit" runat="server" CssClass="label" Text="Estado:" />
        &nbsp;
        <asp:Label ID="lblEstado" runat="server" />
        <br />
        <br />

        <asp:Label ID="lblTitCliente" runat="server" CssClass="label" Text="CNPJ do Cliente:" />
        &nbsp;
        <span id="spanParametros" runat="server">
            <uc1:ClienteView ID="txtCliente" runat="server" ExibirBusca="false" TipoBuscaSelecionado="CPF_CNPJ" />
            <br />
        </span>
        <br />

        <asp:Label ID="lblTitTecnico" runat="server" CssClass="label" Text="Técnico:" />
        <asp:DropDownList ID="drpTecnico" runat="server" DataTextField="NomeTecnico" DataValueField="CodigoTecnico" CssClass="drop" />
        <br />
        <br />

        <asp:Label ID="lblTitNotaFiscal" runat="server" CssClass="label" Text="Nota Fiscal:" />
        &nbsp;
        <asp:TextBox ID="txtNotaFiscal" runat="server" />
        &nbsp;

        <asp:Label ID="lblTitSerieNota" runat="server" CssClass="label" Text="Série Nota:" />
        &nbsp;
        <asp:TextBox ID="txtSerieNota" runat="server" CssClass="textBox" />
        <br />
        <br />

        <asp:Label ID="lblTitPedido" runat="server" CssClass="label" Text="Pedido:" />
        &nbsp;
        <asp:TextBox ID="txtPedido" runat="server" />
        &nbsp;

        <asp:Label ID="lblTitItemPedido" runat="server" CssClass="label" Text="Item Pedido:" />
        &nbsp;
        <asp:TextBox ID="txtItemPedido" runat="server" CssClass="textBox" />
        <br />
        <br />

        <asp:Label ID="lblTitDataInstalacao" runat="server" CssClass="label" Text="Data da Instalação:" />
        &nbsp; 
        <uc5:DateBox ID="oDataInstalacao" runat="server" Text="__/__/____" />
        &nbsp;

        <asp:Label ID="lblTitDataVenda" runat="server" CssClass="label" Text="Data da Venda:" />
        &nbsp; 
        <uc5:DateBox ID="oDataVenda" runat="server" />
        &nbsp;

        <asp:Label ID="lblTitDataGarantia" runat="server" CssClass="label" Text="Data da Garantia:" />
        &nbsp; 
        <uc5:DateBox ID="oDataGarantia" runat="server" />
        <br />
        <br />

        <asp:Label ID="lblTitLocalInstalacao" runat="server" CssClass="label" Text="Local Instalação:" />
        <br />
        <asp:TextBox ID="txtLocalInstalacao" runat="server" TextMode="MultiLine" CssClass="textArea2" />
        <br />
        <br />

        <asp:Label ID="lblTitContato" runat="server" CssClass="label" Text="Contato:" />
        &nbsp;
        <asp:TextBox ID="txtContato" runat="server" CssClass="textBox" />
        &nbsp;

        <asp:Label ID="lblTitTelefone" runat="server" CssClass="label" Text="Telefone:" />
        &nbsp;
        <asp:TextBox ID="txtTelefone" runat="server" />
        <br />
        <br />

        <asp:Label ID="lblRevisaoFabrica" runat="server" CssClass="label" Text="Revisão de Fábrica:" />
        <asp:DropDownList ID="drpRevisaoFabrica" runat="server" DataTextField="DescricaoStatus"
            DataValueField="CodigoStatus" CssClass="drop">
            <asp:ListItem Value="0">Nenhum</asp:ListItem>
            <asp:ListItem Value="E">Executada</asp:ListItem>
            <asp:ListItem Value="C">Conforme</asp:ListItem>
            <asp:ListItem Value="R">Rejeitado</asp:ListItem>
            <asp:ListItem Value="B">BT007/10</asp:ListItem>
        </asp:DropDownList>

    </asp:Panel>
    <br />

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">

        <ContentTemplate>

            <asp:Panel ID="pnlItens" runat="server" CssClass="gridFundo">

                <asp:GridView ID="grdItens" runat="server" AutoGenerateColumns="False" Width="100%" Enabled="false" CssClass="GridViewStyle"
                    ShowFooter="True" EnableModelValidation="True" RowStyle-HorizontalAlign="NotSet">
                    <Columns>

                        <asp:TemplateField HeaderText="Acessório (Código)">
                            <ItemTemplate>
                                <uc9:AcessorioView ID="oAcessorio" runat="server" Codigo='<%# Bind("Acessorio") %>' SomenteLeitura="false" />
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Série">
                            <ItemTemplate>
                                <uc10:BaseInstaladaBox ID="oBaseInstaladaBox" runat="server" Serie='<%# Bind("Serie") %>' />
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Lote">
                            <ItemTemplate>
                                <asp:TextBox ID="txtLote" runat="server" CssClass="textBox2" ReadOnly="True" Text='<%# Bind("Lote") %>' />
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>

                    </Columns>

                    <HeaderStyle CssClass="HeaderStyle" />
                    <FooterStyle CssClass="HeaderStyle" />
                    <EditRowStyle CssClass="EditRowStyle" />
                    <AlternatingRowStyle CssClass="AltRowStyle" />
                    <RowStyle CssClass="RowStyle" />

                </asp:GridView>

            </asp:Panel>
            <br />

        </ContentTemplate>

    </asp:UpdatePanel>

    <asp:Panel runat="server" ID="pnlEmail" Visible="false">
        <asp:Label ID="lblTitEmail" runat="server" CssClass="label" Text="E-mail:" />
        &nbsp; 
        <asp:TextBox ID="txtEmail" runat="server" CssClass="textBox" />
        <br />
        <br />

        <asp:Label ID="lblTitMensagem" runat="server" CssClass="label" Text="Mensagem:" />
        <br />
        <asp:TextBox ID="txtMensagem" runat="server" TextMode="MultiLine" CssClass="textArea2" />
        <br />
    </asp:Panel>

    <asp:Panel ID="pnlBotoes" runat="server">
        <br />
        <table width="100%">
            <tr>
                <td>
                    <uc7:AutoHideButton ID="btnIncluir" runat="server" CssButton="button" Text="Confirmar" />
                    <uc7:AutoHideButton ID="btnAlterar" runat="server" CssButton="button" Text="Confirmar" />
                </td>
                <td dir="rtl">
                    <uc7:AutoHideButton ID="btnVoltar" runat="server" CssButton="button" Text="Voltar" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <br />

    <asp:Panel runat="server" ID="pnlMensagm">
        <uc6:Mensagem ID="oMensagem" runat="server" />
    </asp:Panel>

</asp:Content>
