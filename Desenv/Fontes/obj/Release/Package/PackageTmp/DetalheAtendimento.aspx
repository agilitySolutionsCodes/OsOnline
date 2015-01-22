<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/SubMaster.Master" CodeBehind="DetalheAtendimento.aspx.vb" Inherits="OrdemServico.DetalheAtendimento" %>

<%@ Register Src="~/controlesespecificos/ProdutoView.ascx" TagName="ProdutoView" TagPrefix="uc3" %>
<%@ Register Src="~/componentes/controles/NumberBox.ascx" TagName="NumberBox" TagPrefix="uc4" %>
<%@ Register Src="~/componentes/controles/DateBox.ascx" TagName="DateBox" TagPrefix="uc5" %>
<%@ Register Src="~/componentes/controles/Mensagem.ascx" TagName="Mensagem" TagPrefix="uc6" %>
<%@ Register Src="~/componentes/controles/AutoHideButton.ascx" TagName="AutoHideButton" TagPrefix="uc7" %>
<%@ Register Src="~/componentes/controles/HourBox.ascx" TagName="HourBox" TagPrefix="uc11" %>
<%@ Register Src="~/componentes/controles/PopupBox.ascx" TagName="PopupBox" TagPrefix="uc12" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SubMaster" runat="server">
    <br />
    <asp:UpdatePanel ID="updCabecalho" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Panel ID="pnlCabecalho" runat="server">
                <br />
                <asp:HiddenField ID="txtModoAtendimento" runat="server" />
                <asp:HiddenField ID="hdnAlteracaoItem" runat="server" />
                <asp:HiddenField ID="hdnTpAtendimento" runat="server" />
                <asp:HiddenField ID="hdnDtEmissaoOS" runat="server" />
                <asp:Label ID="lblTitNumero" runat="server" Text="Número:" CssClass="label" />
                <asp:Label ID="lblNumero" runat="server" Text="000000" />&nbsp; 
        <asp:Label ID="lblTitSequencia" runat="server" Text="Sequência:" CssClass="label" />
                <asp:Label ID="lblSequencia" runat="server" Text="00" />&nbsp;
        <asp:Label ID="lblTitSerie" runat="server" Text="Nº Série Equipamento:" CssClass="label" />
                <asp:Label ID="lblSerie" runat="server" Text="000000" />&nbsp;
        <asp:Label ID="lblTitGarantia" runat="server" Text="Garantia até:" CssClass="label" />
                <asp:Label ID="lblDtGarantia" runat="server" Text="" />
                <asp:HiddenField ID="txtPossuiGarantia" runat="server" Value="N" />
                <br />
                <br />
                <asp:Label ID="lblTitVersaoSoft" runat="server" Text="Versão Original do Software:" CssClass="label" />&nbsp;
        <asp:Label ID="lblVersaoSoft" runat="server" Text="" />
                <asp:Label ID="lblMsgAtualizarVersao" runat="server" Text="- Atualizar versão de software" CssClass="semGarantia" Visible="false" />&nbsp;&nbsp;
        <asp:Label ID="lblTitVersaoAtual" runat="server" Text="Versão atual do Software:" CssClass="label" Visible="false" />&nbsp;
        <asp:Label ID="lblVersaoAtual" runat="server" Text="" Visible="false" />
                <br />
                <br />
                <asp:Label ID="lblTitTecnico" runat="server" CssClass="label" Text="Técnico:" />&nbsp;
        <asp:DropDownList ID="drpTecnico" runat="server" DataTextField="NomeTecnico" DataValueField="CodigoTecnico" CssClass="drop" />
                &nbsp;&nbsp;
        <asp:Label ID="lblTitOcorrencia" runat="server" CssClass="label" Text="Ocorrência:" />&nbsp;
        <asp:DropDownList ID="drpOcorrencia" runat="server" DataTextField="DescricaoOcorrencia" DataValueField="CodigoOcorrencia" CssClass="drop" />
                <br />
                <br />
                <asp:Label ID="lblTitDataInicio" runat="server" CssClass="label" Text="Data Início:" />&nbsp; 
                <uc5:DateBox ID="oDataInicio" runat="server" Text="" />
                &nbsp; 
                <asp:Label ID="lblTitHoraInicio" runat="server" CssClass="label" Text="Hora Início:" />&nbsp; 
                <uc11:HourBox ID="oHoraInicio" runat="server" Text="" />
                &nbsp; 
        <asp:Label ID="lblTitDataTermino" runat="server" CssClass="label" Text="Data Termino:" />&nbsp; 
        <uc5:DateBox ID="oDataTermino" runat="server" Text="" />
                &nbsp; 

        <asp:Label ID="lblTitHoraTermino" runat="server" CssClass="label" Text="Hora Termino:" />
                &nbsp; 
        <uc11:HourBox ID="oHoraTermino" runat="server" Text="" />
                &nbsp;  &nbsp;
                <br />
                <br />

                <asp:Label ID="lblTitTranslado" runat="server" CssClass="label" Text="Translado:" Visible="false" />
                <asp:TextBox ID="txtTranslado" runat="server" CssClass="textBox" Visible="false" />

                <asp:Label ID="lblTitStatus" runat="server" CssClass="label" Text="Status:" />
                <asp:DropDownList ID="drpStatus" runat="server" DataTextField="DescricaoStatus"
                    DataValueField="CodigoStatus" CssClass="drop">
                    <asp:ListItem Value="1">Encerrado</asp:ListItem>
                    <asp:ListItem Value="2" Selected="True">Em Aberto</asp:ListItem>
                </asp:DropDownList>
                &nbsp;
        
        <asp:Label ID="lblTitHorasFaturadas" runat="server" CssClass="label" Text="Horas Faturadas:" Visible="false" />
                <uc11:HourBox ID="oHorasFaturadas" runat="server" Text="" Visible="false" />
                &nbsp;

        <asp:Label ID="lblTitIncluirItemOS" runat="server" CssClass="label" Text="Incluir Item OS:" />
                &nbsp;
        <asp:DropDownList ID="drpIncluirItemOS" runat="server"
            DataTextField="DescricaoGarantia" DataValueField="CodigoGarantia"
            CssClass="drop">
            <asp:ListItem Value="0">Nenhum</asp:ListItem>
            <asp:ListItem Value="2">Não</asp:ListItem>
            <asp:ListItem Value="1">Sim</asp:ListItem>
        </asp:DropDownList>
                <br />
                <br />

                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <br />
                        <asp:Panel ID="pnlEtapas" runat="server" CssClass="gridFundo">
                            <asp:GridView ID="grdEtapas" runat="server" AutoGenerateColumns="False" Width="100%" CssClass="GridViewStyle" 
                                ShowFooter="True" EnableModelValidation="True" HeaderStyle-HorizontalAlign="NotSet">
                                <Columns>
                                    <asp:CommandField InsertText="Incluir" NewText="Novo" ShowCancelButton="True" ShowDeleteButton="true"
                                        ShowEditButton="true" ShowInsertButton="True" ShowSelectButton="False" ButtonType="Image"
                                        CancelText="Cancelar" CancelImageUrl="~/imagens/desfazer.png" EditImageUrl="~/imagens/Editar.png"
                                        DeleteImageUrl="~/imagens/Excluir.png" InsertImageUrl="~/imagens/Novo.png" NewImageUrl="~/imagens/Novo.png"
                                        UpdateImageUrl="~/imagens/confirmar.png" HeaderText="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" />

                                    <asp:TemplateField HeaderText="Item" Visible="True">
                                        <ItemTemplate>
                                            <asp:Label ID="lblItemEtapa" runat="server" Text='<%# Bind("Item") %>' />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Etapa">
                                        <EditItemTemplate>
                                            <asp:DropDownList ID="drpEtapa" runat="server" DataTextField="DescricaoEtapa"
                                                DataValueField="CodigoEtapa" CssClass="drop2">
                                            </asp:DropDownList>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:DropDownList ID="drpEtapa" runat="server" DataTextField="DescricaoEtapa"
                                                DataValueField="CodigoEtapa" CssClass="drop2" Enabled="false">
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField  HeaderText="Data Início">
                                        <EditItemTemplate>
                                            <uc5:DateBox ID="oDtInicio" runat="server" Text='<%# Bind("DataInicio")%>' />
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <uc5:DateBox ID="oDtInicio" runat="server" Text='<%# Bind("DataInicio")%>' Enabled="false" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Hora Início">
                                        <EditItemTemplate>
                                            <uc11:HourBox ID="oHrInicio" runat="server" Text='<%# Bind("HoraInicio")%>' />
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <uc11:HourBox ID="oHrInicio" runat="server" Text='<%# Bind("HoraInicio")%>' Enabled="false" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Data Término">
                                        <EditItemTemplate>
                                            <uc5:DateBox ID="oDtFim" runat="server" Text='<%# Bind("DataFim")%>' />
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <uc5:DateBox ID="oDtFim" runat="server" Text='<%# Bind("DataFim")%>' Enabled="false" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Hora Término">
                                        <EditItemTemplate>
                                            <uc11:HourBox ID="oHrFim" runat="server" Text='<%# Bind("HoraFim")%>' />
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <uc11:HourBox ID="oHrFim" runat="server" Text='<%# Bind("HoraFim")%>' Enabled="false" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="D_E_L_E_T_" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblD_E_L_E_T_" runat="server" Text='<%# Bind("D_E_L_E_T_") %>' />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                </Columns>
                                <HeaderStyle CssClass="HeaderStyle" />
                                <FooterStyle CssClass="HeaderStyle" />
                                <EditRowStyle CssClass="AltRowStyle" />
                                <RowStyle CssClass="RowStyle2" />
                                <AlternatingRowStyle CssClass="AltRowStyle" />

                            </asp:GridView>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <br />
                <br />
                <asp:Panel ID="pnlAprovacaoOS" runat="server" Visible="false">
                    <asp:Label ID="lblTitROT" runat="server" CssClass="label" Text="Aprovação OS:" />&nbsp;
            <asp:DropDownList ID="drpROTAprovado" runat="server" DataTextField="DescricaoStatus"
                DataValueField="CodigoStatus" CssClass="drop" Enabled="false">
                <asp:ListItem Value="" Selected="True">-</asp:ListItem>
                <asp:ListItem Value="1">Aprovado</asp:ListItem>
                <asp:ListItem Value="2">Reprovado</asp:ListItem>
                <asp:ListItem Value="3">Aguardando Análise</asp:ListItem>
            </asp:DropDownList>
                    &nbsp;&nbsp;

            <asp:Label ID="lblAprovacao2" runat="server" CssClass="label" Text="Aprovação OS 2:" Visible="false" />&nbsp;
            <asp:DropDownList ID="drpAprovacao2" runat="server" DataTextField="DescricaoStatus"
                DataValueField="CodigoStatus" CssClass="drop" Enabled="false" Visible="false">
                <asp:ListItem Value="" Selected="True">-</asp:ListItem>
                <asp:ListItem Value="1">Aprovado</asp:ListItem>
                <asp:ListItem Value="2">Reprovado</asp:ListItem>
                <asp:ListItem Value="3">Aguardando Análise</asp:ListItem>
            </asp:DropDownList>
                    <br />
                    <br />
                </asp:Panel>

                <asp:Panel ID="pnlEnviaAnalise" runat="server">
                    <asp:CheckBox ID="chkEnviaAnalise" runat="server" Text="Solicitar análise de troca de peças" /><br />
                    <br />
                    <br />
                </asp:Panel>

                <asp:Label ID="lblDefeito" runat="server" CssClass="label" Text="Defeito:" />&nbsp;
        <asp:DropDownList ID="drpDefeito" runat="server" DataTextField="descricao" DataValueField="codigo" />&nbsp;&nbsp;&nbsp;

        <asp:Label ID="lblServico" runat="server" CssClass="label" Text="Serviço:" />&nbsp;
        <asp:DropDownList ID="drpServico" runat="server" DataTextField="descricao" DataValueField="codigo" /><br />
                <br />

                <asp:Panel ID="pnlDescricaoROT" runat="server" Visible="false">
                    <asp:Label ID="lblTitRelROT" runat="server" CssClass="label" Text="Análise OS:" />
                    <br />
                    <asp:TextBox ID="txtROT" runat="server" CssClass="textArea2" TextMode="MultiLine" Enabled="false" />
                    <br />
                    <br />
                </asp:Panel>

                <asp:Panel ID="pnlDescricaoOS2" runat="server" Visible="false">
                    <asp:Label ID="lblAnaliseOS2" runat="server" CssClass="label" Text="Análise OS 2:" />
                    <br />
                    <asp:TextBox ID="txtAnaliseOS2" runat="server" CssClass="textArea2" TextMode="MultiLine" Enabled="false" />
                    <br />
                    <br />
                </asp:Panel>

                <asp:Label ID="lblTitDefeito" runat="server" CssClass="label" Text="Defeito:" />
                <br />
                <asp:TextBox ID="txtDefeito" runat="server" CssClass="textArea2" TextMode="MultiLine" />
                <br />
                <br />

                <asp:Label ID="lblTitCausa" runat="server" CssClass="label" Text="Causa:" />
                <br />
                <asp:TextBox ID="txtCausa" runat="server" CssClass="textArea2" TextMode="MultiLine" />
                <br />
                <br />

                <asp:Label ID="lblTitServExecutado" runat="server" CssClass="label" Text="Serv. Executado:" />
                <br />
                <asp:TextBox ID="txtServExecutado" runat="server" CssClass="textArea2" TextMode="MultiLine" />

            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">

        <ContentTemplate>
            <br />

            <asp:Panel ID="pnlItens" runat="server" CssClass="gridFundo">

                <asp:GridView ID="grdItens" runat="server" AutoGenerateColumns="False" Width="100%" CssClass="GridViewStyle"
                    ShowFooter="True" EnableModelValidation="True" HeaderStyle-HorizontalAlign="NotSet">

                    <Columns>

                        <asp:CommandField InsertText="Incluir" NewText="Novo" ShowCancelButton="True" ShowDeleteButton="true"
                            ShowEditButton="true" ShowInsertButton="True" ShowSelectButton="False" ButtonType="Image"
                            CancelText="Cancelar" CancelImageUrl="~/imagens/desfazer.png" EditImageUrl="~/imagens/editar.png"
                            DeleteImageUrl="~/imagens/excluir.png" InsertImageUrl="~/imagens/Novo.png" NewImageUrl="~/imagens/novo.png"
                            UpdateImageUrl="~/imagens/confirmar.png"
                            HeaderText="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" />

                        <asp:TemplateField HeaderText="Item" Visible="True">
                            <ItemTemplate>
                                <asp:Label ID="lblItem" runat="server" Text='<%# Bind("Item") %>' />
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Peça">
                            <ItemTemplate>
                                <uc3:ProdutoView ID="oProdutoView" runat="server" Codigo='<%# Bind("CodigoItem") %>'
                                    HabilitarEdicao="false" Nome='<%# Bind("DescricaoItem") %>' OnSelecionarOnClick="oProduto_SelecionarOnClick" />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <uc3:ProdutoView ID="oProdutoView" runat="server" Codigo='<%# Bind("CodigoItem") %>'
                                    HabilitarEdicao="true" Nome='<%# Bind("DescricaoItem") %>' OnSelecionarOnClick="oProduto_SelecionarOnClick" />
                            </EditItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Quantidade">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtQuantidade" runat="server" Text='<%# Bind("Quantidade") %>'
                                    CssClass="drop2" Enabled="true">
                                </asp:TextBox>
                            </EditItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemTemplate>
                                <asp:TextBox ID="txtQuantidade" runat="server" Text='<%# Bind("Quantidade") %>'
                                    CssClass="drop2" Enabled="FALSE">
                                </asp:TextBox>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Nr. Série">
                            <ItemTemplate>
                                <asp:TextBox ID="txtSeriePeca" runat="server" Text='<%# Bind("NumeroSeriePeca") %>'
                                    CssClass="drop2" Enabled="false">
                                </asp:TextBox>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                            <EditItemTemplate>
                                <asp:TextBox ID="txtSeriePeca" runat="server" Text='<%# Bind("NumeroSeriePeca") %>'
                                    CssClass="drop2" Enabled="true">
                                </asp:TextBox>
                            </EditItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Lote">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtLote" MaxLength="10" runat="server" Text='<%# Bind("NumeroLote") %>'
                                    CssClass="drop2" Enabled="true">
                                </asp:TextBox>
                            </EditItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemTemplate>
                                <asp:TextBox ID="txtLote" runat="server" Text='<%# Bind("NumeroLote") %>'
                                    CssClass="drop2" Enabled="false">
                                </asp:TextBox>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="D_E_L_E_T_" Visible="False">
                            <ItemTemplate>
                                <asp:Label ID="lblD_E_L_E_T_" runat="server" Text='<%# Bind("D_E_L_E_T_") %>' />
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Registro" Visible="False">
                            <ItemTemplate>
                                <asp:Label ID="lblRegistro" runat="server" Text='<%# Bind("NumeroOS") %>' />
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>

                    </Columns>

                    <HeaderStyle CssClass="HeaderStyle" />
                    <FooterStyle CssClass="HeaderStyle" />
                    <EditRowStyle CssClass="EditRowStyle" />
                    <AlternatingRowStyle CssClass="AltRowStyle2" />
                    <RowStyle CssClass="RowStyle2" />

                </asp:GridView>

            </asp:Panel>

        </ContentTemplate>

    </asp:UpdatePanel>

    <asp:Panel ID="pnlBotoes" runat="server" Width="100%">
        <br />
        <table width="100%">
            <tr>
                <td>
                    <uc7:AutoHideButton ID="btnIncluir" runat="server" CssButton="button" Text="Incluir" />
                    <uc7:AutoHideButton ID="btnAlterar" runat="server" CssButton="button" Text="Alterar" />
                    <uc7:AutoHideButton ID="btnAlterarComPedido" runat="server" Visible="false" CssButton="button" Text="Alterar" />
                    <uc7:AutoHideButton ID="btnImprimir" runat="server" CssButton="button" Text="Imprimir" />
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
    <uc12:PopupBox ID="oPopupBox" runat="server" />
</asp:Content>
