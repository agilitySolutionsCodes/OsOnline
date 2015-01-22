<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/SubMaster.Master"
    CodeBehind="DetalheOrdemServico.aspx.vb" Inherits="OrdemServico.DetalheOrdemServico" %>

<%@ Register Src="controlesespecificos/ClienteView.ascx" TagName="ClienteView" TagPrefix="uc1" %>
<%@ Register Src="componentes/controles/PhoneBox.ascx" TagName="PhoneBox" TagPrefix="uc2" %>
<%@ Register Src="componentes/controles/NumberBox.ascx" TagName="NumberBox" TagPrefix="uc4" %>
<%@ Register Src="componentes/controles/DateBox.ascx" TagName="DateBox" TagPrefix="uc5" %>
<%@ Register Src="componentes/controles/Mensagem.ascx" TagName="Mensagem" TagPrefix="uc6" %>
<%@ Register Src="componentes/controles/AutoHideButton.ascx" TagName="AutoHideButton" TagPrefix="uc7" %>
<%@ Register Src="controlesespecificos/BaseInstaladaBox.ascx" TagName="BaseInstaladaBox" TagPrefix="uc9" %>
<%@ Register Src="controlesespecificos/BaseInstaladaView.ascx" TagName="BaseInstaladaView" TagPrefix="uc10" %>
<%@ Register Src="componentes/controles/PopupBox.ascx" TagName="PopupBox" TagPrefix="uc12" %>

<asp:Content ID="Content2" ContentPlaceHolderID="SubMaster" runat="server">
    <br />
    <asp:UpdatePanel ID="pnlUpdate" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Panel ID="pnlCabecalho" runat="server">
                <br />
                <asp:Label ID="lblTitNumero" runat="server" Text="Número: " CssClass="label" />
                <asp:Label ID="lblChamado" runat="server" Visible="false" />
                <asp:Label ID="lblOrdemServico" runat="server" />
                &nbsp;&nbsp; 
            <asp:Label ID="lblTitEmissao" runat="server" CssClass="label" Text=" Emissão: " />
                <asp:Label ID="lblEmissao" runat="server" Text="__/__/____" />&nbsp;&nbsp;
            <asp:Label ID="lblTitTipo" runat="server" CssClass="label" Text=" Tipo: " />&nbsp;
            <asp:DropDownList ID="drpTipo" runat="server">
                <asp:ListItem>Nenhum</asp:ListItem>
                <asp:ListItem Value="I">Interno</asp:ListItem>
                <asp:ListItem Value="E">Externo</asp:ListItem>
            </asp:DropDownList>
                <asp:Label ID="lblOSAntiga" runat="server" CssClass="label" Text="Nº OS Antiga:" Visible="false" />
                <asp:Label ID="txtOSAntiga" runat="server" CssClass="ClienteViewLabel" Visible="false" />
                <br />
                <br />
                <asp:Label ID="lblTitCliente" runat="server" CssClass="label" Text="CNPJ do Cliente: " />
                <span id="spanParametros" runat="server">
                    <uc1:ClienteView ID="txtCliente" runat="server" ExibirBusca="true" TipoBuscaSelecionado="CPF_CNPJ" />
                    <br />
                    <br />
                </span>
                <asp:Label ID="lblTitContato" runat="server" CssClass="label" Text="Contato: " />&nbsp;
                <asp:TextBox ID="txtContato" runat="server" CssClass="textBox" />&nbsp;&nbsp;
                <asp:Label ID="txtTitTelefone" runat="server" CssClass="label" Text="Telefone: " />&nbsp;
                <uc2:PhoneBox ID="oPhoneBox" runat="server" />
                &nbsp;&nbsp;
                <asp:Label ID="lblOSParceiro" runat="server" CssClass="label" Text="Nº OS Parceiro: " />&nbsp;
                <asp:TextBox ID="txtOSParceiro" runat="server" CssClass="textBox" MaxLength="8" />
                <br />
                <br />
                <hr />
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlItens" runat="server" CssClass="gridFundo">
                <asp:GridView ID="grdItens" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"
                    ShowFooter="True" EnableModelValidation="True" RowStyle-HorizontalAlign="NotSet" AllowPaging="False" AllowSorting="False" PageSize="100" Width="100%">
                    <Columns>
                        <asp:CommandField InsertText="Incluir" NewText="Novo" ShowCancelButton="True" ShowDeleteButton="true"
                            ShowEditButton="true" ShowInsertButton="True" ShowSelectButton="False" ButtonType="Image"
                            CancelText="Cancelar" CancelImageUrl="~/imagens/desfazer.png" EditImageUrl="~/imagens/editar.png"
                            DeleteImageUrl="~/imagens/excluir.png" InsertImageUrl="~/imagens/Novo.png" NewImageUrl="~/imagens/novo.png"
                            UpdateImageUrl="~/imagens/confirmar.png" HeaderText="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" />
                        <asp:TemplateField HeaderText="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;">
                            <EditItemTemplate>
                                <uc7:AutoHideButton ID="btnAtender" runat="server" CssButton="booyaosx" Text=""
                                    OnClick="btnAtender_Click" ToolTip="Atender"
                                    CommandArgument='<%# Bind("ProximoAtendimento") %>' Enabled="False" />
                                <uc7:AutoHideButton ID="btnPrint" runat="server" CssButton="print" Text=""
                                    OnClick="btnPrint_Click" ToolTip="Imprimir em branco"
                                    CommandArgument='<%# Bind("ProximoAtendimento") %>' Enabled="False" />
                            </EditItemTemplate>
                            <ItemTemplate>
                                <uc7:AutoHideButton ID="btnAtender" runat="server" CssButton="booyaosx" Text=""
                                    OnClick="btnAtender_Click" ToolTip="Atender"
                                    CommandArgument='<%# Bind("ProximoAtendimento") %>' />
                                <uc7:AutoHideButton ID="btnPrint" runat="server" CssButton="print" Text=""
                                    OnClick="btnPrint_Click" ToolTip="Imprimir em branco"
                                    CommandArgument='<%# Bind("ProximoAtendimento") %>' />
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Item" Visible="True">
                            <ItemTemplate>
                                <asp:Label ID="lblItemOs" runat="server" Text='<%# Bind("ItemOS") %>' />
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Situação">
                            <EditItemTemplate>
                                <asp:DropDownList ID="drpSituacao" runat="server" DataTextField="DescricaoSituacaoOS"
                                    DataValueField="CodigoSituacaoOs" CssClass="drop2">
                                </asp:DropDownList>
                            </EditItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemTemplate>
                                <asp:DropDownList ID="drpSituacao" runat="server" DataTextField="DescricaoSituacaoOS"
                                    DataValueField="CodigoSituacaoOS" CssClass="drop2" Enabled="false">
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Chamado" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblChamado" runat="server" Text='<%# Bind("NumeroChamado") %>' />
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Equipamento (Série)">
                            <ItemTemplate>
                                <uc9:BaseInstaladaBox ID="oBaseInstaladaBox" runat="server" Serie='<%# Bind("NumeroSerieProduto") %>'
                                    Nome='<%# Bind("DescricaoProduto") %>' Garantia='<%# Bind("DescrGarantia") %>' Codigo='<%# Bind("CodigoProduto") %>' />
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                            <EditItemTemplate>
                                <uc10:BaseInstaladaView ID="oBaseInstaladaView" runat="server" Serie='<%# Bind("NumeroSerieProduto") %>'
                                    Nome='<%# Bind("DescricaoProduto") %>' Garantia='<%# Bind("DescrGarantia") %>' Codigo='<%# Bind("CodigoProduto") %>' />
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Classificação">
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
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Ocorrência">
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
                        <asp:TemplateField HeaderText="" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblDtGarantia" runat="server" Text='<%# Bind("DATAGARANTIA") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblVersao" runat="server" Text='<%# Bind("VERSAO") %>' />
                            </ItemTemplate>
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
    <div id="containerAnexo" runat="server" class="dvContainer">
        <div class="anexo">
            <asp:FileUpload ID="UploadAnexoUm" CssClass="btnUpload" runat="server" />
            <asp:TextBox ID="TxtDescArquivoUm" CssClass="textBox" runat="server" MaxLength="99" />
        </div>
        <div class="anexo">
            <asp:FileUpload ID="UploadAnexoDois" CssClass="btnUpload" runat="server" />
            <asp:TextBox ID="TxtDescArquivoDois" CssClass="textBox" runat="server" MaxLength="99" />
        </div>
        <div class="anexo">
            <asp:FileUpload ID="UploadAnexoTres" CssClass="btnUpload" runat="server" />
            <asp:TextBox ID="TxtDescArquivoTres" CssClass="textBox" runat="server" MaxLength="99" />
        </div>
    </div>

    <div id="containerAnexados" runat="server" class="dvContainer">
        <div class="tituloAnexo">
            <asp:Label ID="LblTituloAnexo" Text="Documentos Anexados" runat="server" CssClass="lblAnexos" />
        </div>
        <asp:Literal ID="LtlConteudo" runat="server" />
    </div>
    <asp:Panel ID="pnlBotoes" runat="server">
        <br />
        <table width="100%">
            <tr>
                <td>
                    <uc7:AutoHideButton ID="btnNovo" runat="server" CssButton="button" Text="Incluir" />
                    <uc7:AutoHideButton ID="btnIncluir" runat="server" CssButton="button" Text="Confirmar" />
                    <uc7:AutoHideButton ID="btnAlterar" runat="server" CssButton="button" Text="Confirmar" />
                    <uc7:AutoHideButton ID="btnImprimir" runat="server" CssButton="button" Text="Imprimir" />
                    <uc7:AutoHideButton ID="btnImpimirAtend" runat="server" CssButton="buttonMaior" Text="Imprimir Atendimentos" Visible="false" />
                </td>
                <td dir="rtl">
                    <uc7:AutoHideButton ID="btnLimpar" runat="server" CssButton="button" Text="Limpar" Visible="false" />
                    <uc7:AutoHideButton ID="btnVoltar" runat="server" CssButton="button" Text="Voltar" />
                    <uc7:AutoHideButton ID="btnAnexar" runat="server" CssButton="button" Text="Anexar" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <br />
    <asp:Panel runat="server" ID="pnlMensagem">
        <uc6:Mensagem ID="oMensagem" runat="server" />
    </asp:Panel>
    <uc12:PopupBox ID="oPopupBox" runat="server" />
</asp:Content>

