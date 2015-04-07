<%@ Page Title="" EnableEventValidation="false" Language="vb" AutoEventWireup="false"
    MasterPageFile="~/SubMaster.Master" CodeBehind="DownloadVersao.aspx.vb" Inherits="OrdemServico.DownloadVersao" %>

<%@ Register Src="componentes/controles/Mensagem.ascx" TagName="Mensagem" TagPrefix="uc4" %>
<%@ Register Src="componentes/controles/AutoHideButton.ascx" TagName="AutoHideButton"
    TagPrefix="uc7" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SubMaster" runat="server">
    <br />
    <table class="tabVerde">
        <tr>
            <td>
                <table border="0" width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td align="right" width="40%">
                            <asp:Image ID="Image2" ImageUrl="imagens/arq_baixado.png" runat="server" />&nbsp;Já
                            foi baixado
                        </td>
                        <td width="20%">
                            &nbsp;
                        </td>
                        <td align="left" width="40%">
                            <asp:Image ID="Image1" ImageUrl="imagens/download.png" runat="server" />&nbsp;Liberado
                            para baixar
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:UpdatePanel ID="updMensagem" runat="server" UpdateMode="Conditional" Visible="true">
        <ContentTemplate>
            <asp:Panel runat="server" ID="pnlMensagem">
                <br />
                <uc4:Mensagem ID="oMensagem" runat="server" />
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
    <br />
    <br />
    <asp:Panel ID="pnlItens" runat="server" CssClass="gridFundo">
        <asp:GridView ID="grdItens" runat="server" AutoGenerateColumns="False" Width="100%"
            CssClass="GridViewStyle" AllowPaging="True" AllowSorting="True" PageSize="100">
            <Columns>
                <asp:TemplateField HeaderText="Tipo Doc.">
                    <ItemTemplate>
                        <asp:Label ID="lblTpDoc" runat="server" Text='<%# Bind("TIPO") %>' />
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Cod. Documento">
                    <ItemTemplate>
                        <asp:Label ID="lblDocto" runat="server" Text='<%# Bind("DOCUMENTO") %>' />
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Revisão">
                    <ItemTemplate>
                        <asp:Label ID="lblRevisao" runat="server" Text='<%# Bind("REVISAO") %>' />
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Título">
                    <ItemTemplate>
                        <asp:Label ID="lblTitulo" runat="server" Text='<%# Bind("TITULO") %>' />
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Dt. Disp.">
                    <ItemTemplate>
                        <asp:Label ID="lblData" runat="server" Text='<%# Bind("DTDISPONIVEL")  %>' />
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Arquivo">
                    <ItemTemplate>
                        <asp:ImageButton ID="imgDownload" ImageUrl='<%# "imagens/" & EVAL("IMAGEM").toString %>'
                            OnClick="DownloadArquivo" CommandArgument='<%# Bind("CAMINHO_DOC_REV") %>' runat="server"
                            Enabled='<%# Bind("HABILITAIMG") %>' />
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
</asp:Content>
