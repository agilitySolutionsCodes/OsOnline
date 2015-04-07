<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/SubMaster.Master" CodeBehind="AlteracaoBaseInstalada.aspx.vb" Inherits="OrdemServico.AlteracaoBaseInstalada" %>

<%@ Register Src="componentes/controles/DateBox.ascx" TagName="DateBox" TagPrefix="uc1" %>
<%@ Register Src="componentes/controles/Mensagem.ascx" TagName="Mensagem" TagPrefix="uc2" %>
<%@ Register src="componentes/controles/PhoneBox.ascx" tagname="PhoneBox" tagprefix="uc4" %>
<%@ Register Src="componentes/controles/AutoHideButton.ascx" TagName="AutoHideButton" TagPrefix="uc7" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SubMaster" runat="server">
    <div id="BaseInstalada">
        <br />
        <h3><asp:Label Text="" ID="lblTitBaseInstalada" runat="server" /></h3>
        <br />

        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlItens" runat="server" CssClass="gridFundo">
                <asp:GridView ID="grdItens" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"
                              ShowFooter="True" RowStyle-HorizontalAlign="NotSet" Width="100%">
                    
                    <Columns>
                       <asp:TemplateField HeaderText="Nº Série" Visible="True">
                            <ItemTemplate>
                                <asp:Label Width="143px" ID="lblNumSerie" runat="server" Text='<%# Bind("SERIEEQUIPAMENTO") %>' />
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                                                
                        <asp:TemplateField HeaderText="Local">
                            <ItemTemplate>
                                <asp:TextBox ID="txtLocal" runat="server" MaxLength="50" Text='<%# Bind("LOCALINSTALACAO") %>' />
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="center" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Endereço">
                            <ItemTemplate>
                                <asp:TextBox ID="txtEndereco" runat="server" MaxLength="50" Text='<%# Bind("ENDERECO") %>' />
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="center" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Estado">
                            <ItemTemplate>
                                <asp:DropDownList ID="drpEstado" runat="server" DataTextField="Estado" DataValueField="Sigla" CssClass="drop" AutoPostBack="true" OnSelectedIndexChanged="CarregarCidade"  />
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="center" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Cidade">
                            <ItemTemplate>
                                <asp:DropDownList ID="drpCidade" runat="server" DataTextField="Cidade" DataValueField="CodCidade" CssClass="drop" />
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="center" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Telefone">
                            <ItemTemplate>
                                <uc4:PhoneBox ID="oPhoneBox" runat="server" Text='<%# Bind("TELEFONE") %>' />
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="center" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Contato">
                            <ItemTemplate>
                                <asp:TextBox ID="txtContato" runat="server" MaxLength="50" Text='<%# Bind("CONTATO") %>' />
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="center" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Dt. Instalação">
                            <ItemTemplate>
                                <uc1:DateBox ID="oDataInstalacao" runat="server" Text='<%# Bind("DATAINSTALACAO") %>'  />
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="center" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Técnico">
                            <ItemTemplate>
                                <asp:DropDownList ID="drpTecnico" runat="server" DataTextField="NomeTecnico" DataValueField="CodigoTecnico" CssClass="drop" />
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="center" />
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

        <asp:Panel runat="server" ID="pnlMensagm">
            <uc2:Mensagem ID="oMensagem" runat="server" /><br />
        </asp:Panel>

        <asp:Panel ID="pnlBotoes" runat="server"> <br />
             <table width="100%">
                <tr>
                    <td>
                        <uc7:AutoHideButton ID="btnConfirmar" runat="server" CssButton="button" Text="Confirmar" />
                    </td>
                    <td dir="rtl">
                        <uc7:AutoHideButton ID="btnVoltar" runat="server" CssButton="button" Text="Voltar" />
                    </td>
                </tr>
            </table>
        </asp:Panel> 
        
    </div>
</asp:Content>
