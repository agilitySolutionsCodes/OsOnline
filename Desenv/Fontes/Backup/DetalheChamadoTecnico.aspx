<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/SubMaster.Master" CodeBehind="DetalheChamadoTecnico.aspx.vb" Inherits="OrdemServico.DetalheChamadoTecnico" ViewStateMode="Enabled" %>

<%@ Register Src="controlesespecificos/ClienteView.ascx" TagName="ClienteView" TagPrefix="uc1" %>
<%@ Register src="componentes/controles/PhoneBox.ascx" tagname="PhoneBox" tagprefix="uc2" %>
<%@ Register Src="componentes/controles/NumberBox.ascx" TagName="NumberBox" TagPrefix="uc4" %>
<%@ Register Src="componentes/controles/DateBox.ascx" TagName="DateBox" TagPrefix="uc5" %>
<%@ Register Src="componentes/controles/Mensagem.ascx" TagName="Mensagem" TagPrefix="uc6" %>
<%@ Register Src="componentes/controles/AutoHideButton.ascx" TagName="AutoHideButton" TagPrefix="uc7" %>
<%@ Register Src="controlesespecificos/BaseInstaladaBox.ascx" TagName="BaseInstaladaBox" TagPrefix="uc9" %>
<%@ Register Src="controlesespecificos/BaseInstaladaView.ascx" TagName="BaseInstaladaView" TagPrefix="uc10" %>
<%@ Register Src="componentes/controles/DescriptionBox.ascx" TagName="DescriptionBox" TagPrefix="uc12" %>

<asp:Content ID="Content2" ContentPlaceHolderID="SubMaster" runat="server">
    <br />
    <asp:UpdatePanel ID="pnlUpdate" runat="server" UpdateMode="Conditional" > <ContentTemplate>
        <asp:Panel ID="pnlCabecalho" runat="server"> <br />
        
            <asp:Label ID="lblTitNumero" runat="server" Text="Número: " CssClass="label" />
            <asp:Label ID="lblNumero" runat="server" />  
            <asp:Label ID="lblTitEmissao" runat="server" CssClass="label" Text=" Emissão: " />
            <asp:Label ID="lblEmissao" runat="server" Text="__/__/____" />
            <asp:Label ID="lblTitTipo" runat="server" CssClass="label" Text=" Tipo: " />
            <asp:DropDownList ID="drpTipo" runat="server" > 
                <asp:ListItem>Nenhum</asp:ListItem>
                <asp:ListItem Value="I">Interno</asp:ListItem>
                <asp:ListItem Value="E">Externo</asp:ListItem>
            </asp:DropDownList>
            <asp:Label ID="lblChamadoAntigo" runat="server" CssClass="label" Text="Nº Chamado Antigo:" Visible="false" />
            <asp:Label ID="txtChamadoAntigo" runat="server" CssClass="ClienteViewLabel" Visible="false" /> 
            <br/> <br/>

            <asp:Label ID="lblTitCliente" runat="server" CssClass="label" Text="CNPJ do Cliente: " />
            <span id="spanParametros" runat="server">
                <uc1:ClienteView ID="txtCliente" runat="server" ExibirBusca="True" TipoBuscaSelecionado="CPF_CNPJ"/> <br/> <br/>
            </span> 
        
                <asp:Label ID="lblTitContato" runat="server" CssClass="label" Text="Contato: " />
                <asp:TextBox ID="txtContato" runat="server" CssClass="textBox" />
                <asp:Label ID="txtTitTelefone" runat="server" CssClass="label" Text="Telefone: " />
                <uc2:PhoneBox ID="oPhoneBox" runat="server" />
                <asp:HiddenField ID="txtNumOS" runat="server" />
            <br /> <br/> <hr />

        </asp:Panel>
    </ContentTemplate> </asp:UpdatePanel>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        
        <ContentTemplate>
            
            <asp:Panel ID="pnlItens" runat="server" CssClass="gridFundo">
                
                <asp:GridView ID="grdItens" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"
                              ShowFooter="True" EnableModelValidation="True" RowStyle-HorizontalAlign="NotSet" 
                              Width="100%" AllowPaging="False" AllowSorting="False" PageSize="100" >
                    
                    <Columns>
                        
                        <asp:CommandField InsertText="Incluir" NewText="Novo" ShowCancelButton="True" ShowDeleteButton="true"
                                          ShowEditButton="true" ShowInsertButton="True" ShowSelectButton="False" ButtonType="Image"
                                          CancelText="Cancelar" CancelImageUrl="~/imagens/desfazer.png" EditImageUrl="~/imagens/editar.png"
                                          DeleteImageUrl="~/imagens/excluir.png" InsertImageUrl="~/imagens/Novo.png" NewImageUrl="~/imagens/novo.png"
                                          UpdateImageUrl="~/imagens/confirmar.png" 
                        HeaderText="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" />
                        
                        <asp:TemplateField HeaderText="Item" Visible="True" SortExpression="ItemChamado" >
                            <ItemTemplate>
                                <asp:Label ID="lblItemChamado" runat="server" Text='<%# Bind("ItemChamado") %>' />
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left"  />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Situação" SortExpression="DescricaoSituacao" >
                            <EditItemTemplate>
                                <asp:DropDownList ID="drpSituacao" runat="server" DataTextField="DescricaoSituacao"
                                                  DataValueField="CodigoSituacao" CssClass="drop2">
                                </asp:DropDownList>
                            </EditItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemTemplate>
                                <asp:DropDownList ID="drpSituacao" runat="server" DataTextField="DescricaoSituacao"
                                                  DataValueField="CodigoSituacao" CssClass="drop2" Enabled="false">
                                </asp:DropDownList>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Equipamento (Série)" SortExpression="NumeroSerieProduto">
                            <ItemTemplate>
                                <uc9:BaseInstaladaBox ID="oBaseInstaladaBox" runat="server"  Serie='<%# Bind("NumeroSerieProduto") %>' 
                                                        Nome='<%# Bind("DescricaoProduto") %>' Garantia='<%# Bind("DescrGarantia") %>' Codigo='<%# Bind("CodigoProduto") %>' />
                                <uc12:DescriptionBox ID="oDescriptionBox" runat="server" 
                                                        Descricao='<%# Bind("Comentario") %>' Visible="False"/>                            
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                            <EditItemTemplate>
                                <uc10:BaseInstaladaView ID="oBaseInstaladaView" runat="server" Serie='<%# Bind("NumeroSerieProduto") %>' 
                                                            Nome='<%# Bind("DescricaoProduto") %>' Garantia='<%# Bind("DescrGarantia") %>' Codigo='<%# Bind("CodigoProduto") %>' />
                                <uc12:DescriptionBox ID="oDescriptionBox" runat="server" ToolTip="Comentário" 
                                                        Descricao='<%# Bind("Comentario") %>' Enabled="True" />                            
                            </EditItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Classificação" SortExpression="DescricaoClassificacao" >

                            <EditItemTemplate>
                                <asp:DropDownList ID="drpClassificacao" runat="server" DataTextField="DescricaoClassificacao"
                                                  DataValueField="CodigoClassificacao" CssClass="drop2">
                                </asp:DropDownList>
                            </EditItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemTemplate>
                                <asp:DropDownList ID="drpClassificacao" runat="server" DataTextField="DescricaoClassificacao"
                                                  DataValueField="CodigoClassificacao" CssClass="drop2" Enabled="false">
                                </asp:DropDownList>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Ocorrência" SortExpression="DescricaoOcorrencia" >
                            <EditItemTemplate>
                                <asp:DropDownList ID="drpOcorrencia" runat="server" DataTextField="DescricaoOcorrencia"
                                                  DataValueField="CodigoOcorrencia" CssClass="drop2">
                                </asp:DropDownList>
                            </EditItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemTemplate>
                                <asp:DropDownList ID="drpOcorrencia" runat="server" DataTextField="DescricaoOcorrencia"
                                                  DataValueField="CodigoOcorrencia" CssClass="drop2" Enabled="false">
                                </asp:DropDownList>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="D_E_L_E_T_" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblD_E_L_E_T_" runat="server" Text='<%# Bind("D_E_L_E_T_") %>' />
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Registro" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblRegistro" runat="server" Text='<%# Bind("NumeroChamado") %>' />
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>

                    </Columns>

                    <HeaderStyle CssClass="HeaderStyle" />
                    <FooterStyle CssClass="HeaderStyle" />
                    <EditRowStyle CssClass="EditRowStyle" />
                    <AlternatingRowStyle CssClass="AltRowStyle2" /> 
                    <RowStyle CssClass="RowStyle2" /> 
                    <PagerStyle CssClass="PagerStyle" />
                
                </asp:GridView>
            
            </asp:Panel>

        </ContentTemplate>

    </asp:UpdatePanel>
    <asp:Panel ID="pnlBotoes" runat="server" > <br />
        <table width="100%">
            <tr>
                <td>
                    <uc7:AutoHideButton ID="btnNovo" runat="server" CssButton="button" Text="Incluir" />
                    <uc7:AutoHideButton ID="btnIncluir" runat="server" CssButton="button" Text="Confirmar" />
                    <uc7:AutoHideButton ID="btnAlterar" runat="server" CssButton="button" Text="Confirmar" />
                </td>
                <td dir="rtl">
                    <uc7:AutoHideButton ID="btnLimpar" runat="server" CssButton="button" Text="Limpar" Visible="false" />
                    <uc7:AutoHideButton ID="btnVoltar" runat="server" CssButton="button" Text="Voltar" />
                </td>
            </tr>
        </table>
    </asp:Panel> <br />
    
    <asp:Panel runat="server" ID="pnlMensagem">
        <uc6:Mensagem ID="oMensagem" runat="server" />
    </asp:Panel>

</asp:Content>