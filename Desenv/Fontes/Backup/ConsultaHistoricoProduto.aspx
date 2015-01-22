<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/SubMaster.Master" CodeBehind="ConsultaHistoricoProduto.aspx.vb" Inherits="OrdemServico.ConsultaHistoricoProduto" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="controlesespecificos/ClienteView.ascx" TagName="ClienteView" TagPrefix="uc1" %>
<%@ Register Src="componentes/controles/Mensagem.ascx" TagName="Mensagem" TagPrefix="uc4" %>
<%@ Register Src="controlesespecificos/ClienteBox.ascx" TagName="ClienteBox" TagPrefix="uc5" %>
<%@ Register Src="componentes/controles/AutoHideButton.ascx" TagName="AutoHideButton" TagPrefix="uc7" %>
<%@ Register Src="componentes/controles/DescriptionBoxSmall.ascx" TagName="DescriptionBoxSmall" TagPrefix="uc8" %>
<%@ Register Src="componentes/controles/DescriptionBoxItens.ascx" TagName="DescriptionBoxItens" TagPrefix="uc9" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SubMaster" runat="server">
    
    <asp:Panel ID="pnlPesquisa" runat="server" BorderStyle="Solid" BorderWidth="1px" Width="100%"> &nbsp;
        <asp:Label ID="lblTitPesquisar" runat="server" Text="Pesquisar:" CssClass="label" /> &nbsp;
        <asp:DropDownList ID="drpTipoPequisa" runat="server" AutoPostBack="True" /> &nbsp; &nbsp; 
        <span id="spanParametros" runat="server">
            <asp:TextBox ID="txtParametroPesquisa" runat="server" Visible="true" />
            <uc7:AutoHideButton ID="btnBuscar" runat="server" CssButton="button" Text="Buscar" />
        </span>
    </asp:Panel> <br />
    <asp:HiddenField ID="hdnNumSerie" runat="server" />
            
    <asp:TabContainer ID="tcHistoricoProduto" runat="server">
       
        <asp:TabPanel ID="tpChamado" runat="server" HeaderText="Chamado Técnico" >
       
            <ContentTemplate>

              <asp:Panel ID="pnlResultado" runat="server">
        
        <asp:Panel ID="pnlItens" runat="server" cssClass="gridFundo" Height="400px" >
        
            <asp:GridView ID="grdChamado" runat="server" AutoGenerateColumns="False"
                          Width="100%" EnableModelValidation="True"  CssClass="GridViewStyle"
                          AllowPaging="True" AllowSorting="True" PageSize="10" >
                
                        <Columns>
                
                            <asp:TemplateField HeaderText="Status" SortExpression="DescricaoStatus" >
                                <ItemTemplate>
                                    <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("DescricaoStatus") %>' />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                
                            <asp:HyperLinkField DataTextField="NumeroChamado" HeaderText="Chamado" DataNavigateUrlFields="NumeroChamado"
                                DataNavigateUrlFormatString="~/DetalheChamadoTecnico.aspx?Numero={0}" Target="_parent" 
                                HeaderStyle-HorizontalAlign="Left" SortExpression="NumeroChamado" />

                            <asp:TemplateField HeaderText="Item" SortExpression="ItemChamado" >
                                <ItemTemplate>
                                    <asp:Label ID="lblItemChamado" runat="server" Text='<%# Bind("ItemChamado") %>' />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                                        
                            <asp:TemplateField HeaderText="Cliente" SortExpression="NomeCliente">
                                <ItemTemplate>
                                    <uc5:ClienteBox ID="oCliente" runat="server" Nome='<%# Bind("NomeCliente") %>' Codigo='<%# Bind("CodigoCliente") %>'
                                                    Loja='<%# Bind("LojaCliente") %>' CPF_CNPJ='<%# Bind("CgcCliente") %>' />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Tipo" SortExpression="DescricaoTipo" >
                                <ItemTemplate>
                                    <asp:Label ID="lblTipo" runat="server" Text='<%# Bind("DescricaoTipo") %>' />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
    
                            <asp:HyperLinkField DataTextField="NumeroOs" HeaderText="OS" DataNavigateUrlFields="NumeroOS" SortExpression="NumeroOs"
                                DataNavigateUrlFormatString="~/DetalheOrdemServico.aspx?Numero={0}" Target="_parent" HeaderStyle-HorizontalAlign="Left" />

                            <asp:TemplateField HeaderText="Item" SortExpression="ItemOS" >
                                <ItemTemplate>
                                    <asp:Label ID="lblItemOS" runat="server" Text='<%# Bind("ItemOS") %>' />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                                                                               
                            <asp:TemplateField HeaderText="Classificação" SortExpression="DescricaoClassificacao" >
                                <ItemTemplate>
                                    <asp:Label ID="lblClassificação" runat="server" Text='<%# Bind("DescricaoClassificacao") %>' />
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
             
                </asp:Panel>

            </ContentTemplate>

        </asp:TabPanel>
        
        <asp:TabPanel ID="tpOs" runat="server" HeaderText="Ordem de Serviço">

            <ContentTemplate>

              <asp:Panel ID="pnlResultadoOS" runat="server">
        
        <asp:Panel ID="Panel1" runat="server" cssClass="gridFundo" Height="400px" >
        
            <asp:GridView ID="grdOs" runat="server" AutoGenerateColumns="False"
                          Width="100%" EnableModelValidation="True"  CssClass="GridViewStyle"
                           AllowPaging="True" AllowSorting="True" PageSize="10" >

                        <Columns>

                            <asp:TemplateField HeaderText="Situação" SortExpression="DescricaoSituacao" >
                                <ItemTemplate>
                                    <asp:Label ID="lblSituacao" runat="server" Text='<%# Bind("DescricaoSituacao") %>' />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>

                            <asp:HyperLinkField DataTextField="NumeroOs" HeaderText="OS" DataNavigateUrlFields="NumeroOS"
                                DataNavigateUrlFormatString="~/DetalheOrdemServico.aspx?Numero={0}H" 
                                Target="_parent" ItemStyle-HorizontalAlign="NotSet" HeaderStyle-HorizontalAlign="Left" SortExpression="NumeroOs" />

                            <asp:TemplateField HeaderText="Item" SortExpression="ItemOs" >
                                <ItemTemplate>
                                    <asp:Label ID="lblItemOS" runat="server" Text='<%# Bind("ItemOS") %>' />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                                          
                            <asp:TemplateField HeaderText="Cliente" SortExpression="NomeCliente" >
                                <ItemTemplate>
                                    <uc5:ClienteBox ID="ClienteBox1" runat="server" Nome='<%# Bind("NomeCliente") %>' Codigo='<%# Bind("CodigoCliente") %>'
                                                    Loja='<%# Bind("LojaCliente") %>' CPF_CNPJ='<%# Bind("CgcCliente") %>' />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Ocorrência" SortExpression="DescricaoOcorrencia" >
                                <ItemTemplate>
                                    <asp:Label ID="lblOcorrencia" runat="server" Text='<%# Bind("DescricaoOcorrencia") %>' />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Serviço" SortExpression="DescricaoServico" >
                                <ItemTemplate>
                                    <asp:Label ID="lblServico" runat="server" Text='<%# Bind("DescricaoServico") %>' />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Etapa" SortExpression="DescricaoEtapa" >
                                <ItemTemplate>
                                    <asp:Label ID="lblEtapa" runat="server" Text='<%# Bind("DescricaoEtapa") %>' />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>

                            <asp:BoundField DataField="DataInclusao" DataFormatString="{0:d}" HeaderText="Data Inclusão" SortExpression="DataInclusao" >
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>

                            <asp:TemplateField HeaderText="Hora Inclusão" SortExpression="HoraInclusao" >
                                <ItemTemplate>
                                    <asp:Label ID="lblHora" runat="server" Text='<%# Bind("HoraInclusao") %>' />
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
             
                </asp:Panel>

            </ContentTemplate>

        </asp:TabPanel>

        <asp:TabPanel ID="tbAtendimento" runat="server" HeaderText="Atendimento OS">

            <ContentTemplate>

              <asp:Panel ID="Panel2" runat="server">
        
                <asp:Panel ID="Panel3" runat="server" cssClass="gridFundo" Height="400px" >
        
                    <asp:GridView ID="grdAtendimento" runat="server" AutoGenerateColumns="False"
                                  Width="100%" EnableModelValidation="True"  CssClass="GridViewStyle"
                                   AllowPaging="True" AllowSorting="True" PageSize="10" >

                        <Columns>
                            <asp:TemplateField HeaderText="Nr. OS" SortExpression="OS" >
                                <ItemTemplate>
                                    <asp:Label ID="lblNumOS" runat="server" Text='<%# Bind("NumOS") %>' />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Ocorrência" SortExpression="Ocorrencia" >
                                <ItemTemplate>
                                    <asp:Label ID="lblOcorrencia" runat="server" Text='<%# Bind("Ocorrencia") %>' />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Defeito" SortExpression="Defeito" >
                                <ItemTemplate>
                                    <asp:Label ID="lblDefeito" runat="server" Text='<%# Bind("Defeito") %>' />&nbsp;
                                    <uc8:DescriptionBoxSmall ID="oDescriptionBoxDefeito" runat="server" ToolTip="Defeito" Descricao='<%# Bind("DefeitoConstatado") %>' Enabled="True" />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                                          
                            <asp:TemplateField HeaderText="Causa" SortExpression="Causa" >
                                <ItemTemplate>
                                    <uc8:DescriptionBoxSmall ID="oDescriptionBoxCausa" runat="server" ToolTip="Causa" Descricao='<%# Bind("CausaProvavel") %>' Enabled="True" /> 
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Serviço Exec." SortExpression="Servico" >
                                <ItemTemplate>
                                    <asp:Label ID="lblServico" runat="server" Text='<%# Bind("Servico") %>' />&nbsp;
                                    <uc8:DescriptionBoxSmall ID="oDescriptionBoxServico" runat="server" ToolTip="Serviço" Descricao='<%# Bind("ServicoExecutado") %>' Enabled="True" />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Tot. Peças" SortExpression="TotalPecas" >
                                <ItemTemplate>
                                    <asp:Label ID="lblTotProdutos" runat="server" Text='<%# Bind("TotalProdutos") %>' />&nbsp;
                                    <uc9:DescriptionBoxItens ID="oDescriptionBoxAtend" runat="server" ToolTip="Atendimento" Descricao='<%# Bind("NumAtendimento") %>' Enabled="True" />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>

                            <asp:BoundField DataField="DataInclusao" DataFormatString="{0:d}" HeaderText="Data Incl." SortExpression="DataInclusao" >
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>

                        </Columns>

                        <HeaderStyle CssClass="HeaderStyle" />
                        <EditRowStyle CssClass="EditRowStyle" />
                        <AlternatingRowStyle CssClass="AltRowStyle" />
                        <RowStyle CssClass="RowStyle" />
                        <PagerStyle CssClass="PagerStyle" />

                    </asp:GridView>     

                    </asp:Panel> <br /><br />
                    <asp:Button ID="btnImprimir" Text="Imprimir" CssClass="button" runat="server" />

                </asp:Panel>

            </ContentTemplate>

        </asp:TabPanel>
    
    </asp:TabContainer>      

    <uc4:Mensagem ID="oMensagem" runat="server" />

</asp:Content>
