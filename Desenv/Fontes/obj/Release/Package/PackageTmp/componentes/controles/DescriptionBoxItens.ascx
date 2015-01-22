<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="DescriptionBoxItens.ascx.vb" Inherits="OrdemServico.DescriptionBoxItens" %>

<%@ Register src="AutoHideButton.ascx" tagname="AutoHideButton" tagprefix="uc7" %>
<%@ Register Src="Mensagem.ascx" TagName="Mensagem" TagPrefix="uc4" %>

<asp:ImageButton ID="imgDescricao" runat="server" ImageUrl="~/imagens/Nota.png" ToolTip="" Width="18px" Height="18px" />

<asp:Panel ID="pnlDescricao" runat="server" CssClass="DescriptionBox" Visible="false">

    <asp:Panel ID="pnlTopBar" runat="server" CssClass="TopBar">
        <asp:ImageButton ID="imgFechar" runat="server" ImageAlign="Right" ImageUrl="~/imagens/fechar.jpg" 
                         ToolTip="Fechar e Salvar" /> &nbsp;
        <asp:Label ID="lblTopBar" runat="server" />
    </asp:Panel>

    <asp:HiddenField ID="lblNumAtendimento" runat="server" />
    <asp:GridView ID="grdItens" runat="server" AutoGenerateColumns="False"  width="440px" CssClass="GridViewStyle"
                              ShowFooter="false" EnableModelValidation="True" HeaderStyle-HorizontalAlign="NotSet">
                    
        <Columns>
            <asp:TemplateField HeaderText="Item" Visible="True">
                <ItemTemplate>
                    <asp:Label ID="lblItem" runat="server" Text='<%# Bind("Item") %>' />
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" />
            </asp:TemplateField>                        
 
                <asp:TemplateField HeaderText="Cod. Peça" Visible="True">
                <ItemTemplate>
                    <asp:Label ID="lblCodigo" runat="server" Text='<%# Bind("CodProduto") %>' />
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" />
            </asp:TemplateField> 

            <asp:TemplateField HeaderText="Descrição Peça" Visible="True">
                <ItemTemplate>
                    <asp:Label ID="lblDescricao" runat="server" Text='<%# Bind("Descricao") %>' />
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" />
            </asp:TemplateField> 

        </Columns>

        <HeaderStyle CssClass="HeaderStyle" />
        <FooterStyle CssClass="HeaderStyle" />
        <EditRowStyle CssClass="EditRowStyle" />
        <AlternatingRowStyle CssClass="AltRowStyle2" />
        <RowStyle CssClass="RowStyle2" />
                
    </asp:GridView> <br/>
    <uc4:Mensagem ID="oMensagem" runat="server" />
    <uc7:AutoHideButton ID="btnFechar" runat="server" Text="Fechar" CssButton="button" /> <br/>

</asp:Panel>