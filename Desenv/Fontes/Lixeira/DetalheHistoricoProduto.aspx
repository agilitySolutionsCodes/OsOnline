<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/SubMaster.Master" CodeBehind="DetalheHistoricoProduto.aspx.vb" Inherits="OrdemServico.DetalheHistoricoProduto" %>

<%@ Register Src="controlesespecificos/ProdutoView.ascx" TagName="ProdutoView" TagPrefix="uc3" %>
<%@ Register Src="componentes/controles/NumberBox.ascx" TagName="NumberBox" TagPrefix="uc4" %>
<%@ Register Src="componentes/controles/DateBox.ascx" TagName="DateBox" TagPrefix="uc5" %>
<%@ Register Src="componentes/controles/Mensagem.ascx" TagName="Mensagem" TagPrefix="uc6" %>
<%@ Register Src="componentes/controles/AutoHideButton.ascx" TagName="AutoHideButton" TagPrefix="uc7" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SubMaster" runat="server">
    
    <br />
    <asp:Panel ID="pnlCabecalho" runat="server"> <br />
        
        <asp:Label ID="lblTitSerie" runat="server" Text="Série:" CssClass="label" /> &nbsp;
        <asp:Label ID="lblSerie" runat="server" /> &nbsp; 

        <asp:Label ID="lblTitProduto" runat="server" Text="Produto:" CssClass="label" /> &nbsp;
        <asp:Label ID="lblProduto" runat="server" CssClass="label2" /> <br/> <br/>
        
        <asp:Label ID="Label2" runat="server" Text="ORDEM DE SERVIÇO" CssClass="label" /> <br/>

        <asp:Label ID="lblTitNumeroOs" runat="server" Text="Número:" CssClass="label" /> &nbsp;
        <asp:Label ID="lblNumeroOs" runat="server" /> <br/>  

        <asp:Label ID="lblTitItem" runat="server" Text="Item:" CssClass="label" /> &nbsp;
        <asp:Label ID="lblItem" runat="server" /> <br/>

        <asp:Label ID="lblTitCNPJ" runat="server" Text="CNPJ:" CssClass="label" /> &nbsp;
        <asp:Label ID="lblCNPJ" runat="server" /> <br/>  
                       
        <asp:Label ID="lblTitCliente" runat="server" Text="Cliente:" CssClass="label" /> &nbsp;
        <asp:Label ID="lblCliente" runat="server" CssClass="label2" /> <br/>
                       
        <asp:Label ID="lblTitSituacao" runat="server" Text="Situação:" CssClass="label" /> &nbsp;
        <asp:Label ID="lblSituacao" runat="server" /> <br/> 
                       
        <asp:Label ID="lblTitOcorrencia" runat="server" Text="Ocorrência:" CssClass="label" /> &nbsp;
        <asp:Label ID="lblOcorrencia" runat="server" CssClass="label2" /> <br/>  
                       
        <asp:Label ID="lblTitServico" runat="server" Text="Serviço:" CssClass="label" /> &nbsp;
        <asp:Label ID="lblServico" runat="server" /> <br/>  
                        
        <asp:Label ID="lblTitEtapa" runat="server" Text="Etapa:" CssClass="label" /> &nbsp;
        <asp:Label ID="lblEtapa" runat="server" CssClass="label2" /> <br/> 
                       
        <asp:Label ID="lblTitDataInclusao" runat="server" Text="Data Inclusão:" CssClass="label" /> &nbsp;
        <asp:Label ID="lblDataInclusão" runat="server" /> <br/>  
                       
        <asp:Label ID="lblTitHoraInclusao" runat="server" Text="Hora Inclusão:" CssClass="label" /> &nbsp;
        <asp:Label ID="lblHoraInclusao" runat="server" /> <br/> <br/>

        <asp:Label ID="Label1" runat="server" Text="CHAMADO TÉCNICO" CssClass="label" /> <br/>

        <asp:Label ID="lblTitNumeroChamado" runat="server" Text="Número:" CssClass="label" /> &nbsp;
        <asp:Label ID="lblNumeroChamado" runat="server" /> <br/> 

        <asp:Label ID="lblTitNumeroOrcamento" runat="server" Text="Nº Orçamento:" CssClass="label" /> &nbsp;
        <asp:Label ID="lblNumeroOrcamento" runat="server" /> <br/>

        <asp:Label ID="lblTitTipo" runat="server" Text="Tipo:" CssClass="label" /> &nbsp;
        <asp:Label ID="lblTipo" runat="server" /> <br/>

        <asp:Label ID="lblTitClassificacao" runat="server" Text="Classificação:" CssClass="label" /> &nbsp;
        <asp:Label ID="lblClassificacao" runat="server" CssClass="label2" /> <br/>

        <asp:Label ID="lblTitStatus" runat="server" Text="Status:" CssClass="label" /> &nbsp;
        <asp:Label ID="lblStatus" runat="server" /> &nbsp; 

    </asp:Panel>

    <asp:Panel runat="server" ID="pnlMensagm">
        <uc6:Mensagem ID="oMensagem" runat="server" />
    </asp:Panel>

</asp:Content>
