<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/SubMaster.Master" CodeBehind="ConsultaChamadoTecnico.aspx.vb" Inherits="OrdemServico.ConsultaChamadoTecnico" %>

<%@ Register Src="controlesespecificos/ClienteView.ascx" TagName="ClienteView" TagPrefix="uc1" %>
<%@ Register Src="componentes/controles/DateBox.ascx" TagName="DateBox" TagPrefix="uc3" %>
<%@ Register Src="componentes/controles/Mensagem.ascx" TagName="Mensagem" TagPrefix="uc4" %>
<%@ Register Src="controlesespecificos/ClienteBox.ascx" TagName="ClienteBox" TagPrefix="uc5" %>
<%@ Register Src="componentes/controles/AutoHideButton.ascx" TagName="AutoHideButton" TagPrefix="uc7" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SubMaster" runat="server">
    
    <asp:Panel ID="pnlPesquisa" runat="server" BorderStyle="Solid" BorderWidth="1px" BorderColor="#3B3B3B" Width="100%"> &nbsp;
        <asp:Label ID="lblTitPesquisar" runat="server" Text="Pesquisar:" CssClass="label" /> &nbsp;
        <asp:DropDownList ID="drpTipoPequisa" runat="server" AutoPostBack="True" /> &nbsp; &nbsp; 
        <span id="spanParametros" runat="server">
            <asp:TextBox ID="txtParametroPesquisa" runat="server" Visible="false" />
            <uc3:DateBox ID="txtEmissao" runat="server" />
            <uc1:ClienteView ID="txtCliente" runat="server" ExibirBusca="false" TipoBuscaSelecionado="CPF_CNpJ" />
            <uc7:AutoHideButton ID="btnBuscar" runat="server" CssButton="button" Text="Buscar" />
        </span> <br />
    </asp:Panel>
    
    <asp:Panel ID="pnlIncluir" runat="server">
        <uc7:AutoHideButton ID="btnIncluir" runat="server" CssButton="button" Text="Incluir" />
    </asp:Panel>
    
    <asp:Panel ID="pnlResultado" runat="server" Visible="False"> <br />
       
        <asp:Panel ID="pnlItens" runat="server" cssClass="gridFundo" Height="400px">
         
            <asp:GridView ID="grdItens" runat="server" AutoGenerateColumns="False"
                          Width="100%" EnableModelValidation="True"  CssClass="GridViewStyle"
                          AllowPaging="True" AllowSorting="True" PageSize="100">
          
                <Columns>

                    <asp:TemplateField HeaderText="Status" SortExpression="DescricaoStatusChamado">
                        <ItemTemplate>
                            <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("DescricaoStatusChamado") %>' />
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                                       
                    <asp:HyperLinkField DataTextField="NumeroChamado" HeaderText="Chamado" DataNavigateUrlFields="NumeroChamado"
                        DataNavigateUrlFormatString="~/DetalheChamadoTecnico.aspx?Numero={0}" Target="_parent" HeaderStyle-HorizontalAlign="Left" 
                         SortExpression="NumeroChamado" />

                    <asp:TemplateField HeaderText="Chamado Antigo" SortExpression="ChamadoAntigo" >
                        <ItemTemplate>
                            <asp:Label ID="lblChamadoAntigo" runat="server" Text='<%# Bind("ChamadoAntigo") %>' />
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                                                           
                    <asp:BoundField DataField="DataEmissao" HeaderText="Data Emissão" SortExpression="DataEmissao"
                        DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-HorizontalAlign="Left" />
                    
                    <asp:TemplateField HeaderText="Cliente" SortExpression="NomeCliente" >
                        <ItemTemplate>
                            <uc5:ClienteBox ID="oCliente" runat="server" Nome='<%# Bind("NomeCliente") %>' Codigo='<%# Bind("CodigoCliente") %>'
                                            Loja='<%# Bind("LojaCliente") %>' CPF_CNPJ='<%# Bind("CGCCLIENTE") %>' />
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
  
    </asp:Panel> <br/>

    <uc4:Mensagem ID="oMensagem" runat="server" />

</asp:Content>