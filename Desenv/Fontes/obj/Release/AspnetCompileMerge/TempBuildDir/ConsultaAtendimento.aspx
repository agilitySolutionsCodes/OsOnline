<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/SubMaster.Master" CodeBehind="ConsultaAtendimento.aspx.vb" Inherits="OrdemServico.ConsultaAtendimento" %>

<%@ Register Src="controlesespecificos/ClienteView.ascx" TagName="ClienteView" TagPrefix="uc1" %>
<%@ Register Src="componentes/controles/DateBox.ascx" TagName="DateBox" TagPrefix="uc3" %>
<%@ Register Src="componentes/controles/Mensagem.ascx" TagName="Mensagem" TagPrefix="uc4" %>
<%@ Register Src="controlesespecificos/ClienteBox.ascx" TagName="ClienteBox" TagPrefix="uc5" %>
<%@ Register Src="componentes/controles/AutoHideButton.ascx" TagName="AutoHideButton" TagPrefix="uc7" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SubMaster" runat="server">

    <asp:Panel ID="pnlPesquisa" runat="server" BorderStyle="Solid" BorderWidth="1px" BorderColor="#3B3B3B" Width="100%"> 
        &nbsp;
          <asp:Label ID="lblTitPesquisar" runat="server" Text="Pesquisar:" CssClass="label" />
        &nbsp;
             <asp:DropDownList ID="drpTipoPequisa" runat="server" AutoPostBack="True" />
        &nbsp; &nbsp; 
                <span id="spanParametros" runat="server">
                    <asp:TextBox ID="txtParametroPesquisa" runat="server" Visible="false" />
                    <uc3:DateBox ID="txtEmissao" runat="server" />
                    <uc1:ClienteView ID="txtCliente" runat="server" ExibirBusca="false" TipoBuscaSelecionado="CPF_CNPJ" />
                    <uc7:AutoHideButton ID="btnBuscar" runat="server" CssButton="button" Text="Buscar" />
                </span>
        <br />
    </asp:Panel>

    <asp:Panel ID="pnlResultado" runat="server" Visible="False">
        <br />

        <asp:Panel ID="pnlItens" runat="server" CssClass="gridFundo" Height="400px">

            <asp:GridView ID="grdItens" runat="server" AutoGenerateColumns="False"
                Width="100%" EnableModelValidation="True" CssClass="GridViewStyle"
                AllowPaging="True" AllowSorting="True" PageSize="100">

                <Columns>

                    <asp:TemplateField HeaderText="" SortExpression="Legenda">
                        <ItemTemplate>
                            <asp:Image ID="imgLegenda" runat="server" ImageUrl='<%# "imagens/" & EVAL("IMGLEGENDA").toString %>' />
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Status" SortExpression="DescricaoStatus">
                        <ItemTemplate>
                            <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("DescricaoStatus") %>' />
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>

                    <asp:HyperLinkField DataTextField="NumeroOS" HeaderText="Nr. OS" DataNavigateUrlFields="AtendimentoOS"
                        DataNavigateUrlFormatString="~/DetalheAtendimento.aspx?Numero={0}" Target="_parent" HeaderStyle-HorizontalAlign="Left"
                        SortExpression="NumeroOS" />

                    <asp:TemplateField HeaderText="Seq." SortExpression="Sequencia">
                        <ItemTemplate>
                            <asp:Label ID="lblSeq" runat="server" Text='<%# Bind("Sequencia") %>' />
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>

                    <asp:BoundField DataField="DataInicio" HeaderText="Data Inicio"
                        DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-HorizontalAlign="Left" SortExpression="DataInicio" />

                    <asp:BoundField DataField="HoraInicio" HeaderText="Hora Inicio"
                        HeaderStyle-HorizontalAlign="Left" SortExpression="HoraInicio" />

                    <asp:BoundField DataField="DataTermino" HeaderText="Data Termino"
                        DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-HorizontalAlign="Left" SortExpression="DataTermino" />

                    <asp:BoundField DataField="HoraTermino" HeaderText="Hora Termino"
                        HeaderStyle-HorizontalAlign="Left" SortExpression="HoraTermino" />

                    <asp:TemplateField HeaderText="Ocorrência" SortExpression="Ocorrencia">
                        <ItemTemplate>
                            <asp:Label ID="lblOcorrencia" runat="server" Text='<%# Bind("Ocorrencia") %>' />
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>

                </Columns>

                <HeaderStyle CssClass="HeaderStyle" />
                <EditRowStyle CssClass="EditRowStyle" />
                <AlternatingRowStyle CssClass="AltRowStyle" />
                <RowStyle CssClass="RowStyle" />
                <PagerStyle CssClass="PagerStyle" />

            </asp:GridView>
        </asp:Panel>
        <br />
        <br />
        <fieldset class="legenda">
            <legend class="tit_legenda">&nbsp;LEGENDA&nbsp;</legend>
            <p>
                <asp:Image ID="imgAz" runat="server" ImageUrl="imagens/legenda_azul.jpg" />&nbsp;&nbsp;&nbsp;Equipamento em garantia&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Image ID="ImgAm" runat="server" ImageUrl="imagens/legenda_amarela.jpg" />&nbsp;&nbsp;&nbsp;Atendimento em análise&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Image ID="imgV" runat="server" ImageUrl="imagens/legenda_vermelha.jpg" />&nbsp;&nbsp;&nbsp;Equipamento fora da garantia<br />
                <br />
                <asp:Image ID="Image1" runat="server" ImageUrl="imagens/legenda_verde.bmp" />&nbsp;&nbsp;&nbsp;Pedido gerado (Equipamento em garantia)&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Image ID="Image2" runat="server" ImageUrl="imagens/legenda_cinza.bmp" />&nbsp;&nbsp;&nbsp;Pedido gerado (Equipamento fora da garantia)
            </p>
        </fieldset>
    </asp:Panel>
    <br />


    <uc4:Mensagem ID="oMensagem" runat="server" />

</asp:Content>
