<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FaleConosco.aspx.vb" Inherits="OrdemServico.FaleConosco" MasterPageFile="~/SubMaster.Master" %>

<%@ Register Src="componentes/controles/Mensagem.ascx" TagName="Mensagem" TagPrefix="uc6" %>
<%@ Register Src="componentes/controles/AutoHideButton.ascx" TagName="AutoHideButton" TagPrefix="uc7" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SubMaster" runat="server">

    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.5.0/jquery.min.js"></script>
    <script type="text/javascript">
        $(function showDiv() {
            var moveLeft = 20;
            var moveDown = 10;

            $('a#trigger').hover(function (e) {
                $('div#pop-up').show();
                //.css('top', e.pageY + moveDown)
                //.css('left', e.pageX + moveLeft)
                //.appendTo('body');
            }, function () {
                $('div#pop-up').hide();
            });

            $('a#trigger').mousemove(function (e) {
                $("div#pop-up").css('top', e.pageY + moveDown).css('left', e.pageX + moveLeft);
            });
        });
    </script>
    <br />
    <br />
    &nbsp;&nbsp;<asp:Label ID="Label1" runat="server" CssClass="label" Text="Nome do solicitante:" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:TextBox ID="txtNome" runat="server" Width="400px" />
    <br />
    <br />
    &nbsp;&nbsp;<asp:Label ID="Label2" runat="server" CssClass="label" Text="E-mail do solicitante:" />&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:TextBox ID="txtEmail" runat="server" Width="400px" />
    <br />
    <br />
    &nbsp;&nbsp;<asp:Label ID="lblAssunto" runat="server" CssClass="label" Text="Assunto:" />&nbsp;&nbsp;
    <a href="#" id="trigger">
        <asp:DropDownList ID="drpAssunto" runat="server" CssClass="drop">
            <asp:ListItem Text="Selecione" Value="" />
            <asp:ListItem Text="Regulatoria" Value="R" />
            <asp:ListItem Text="Pós-Vendas" Value="PV" />
            <asp:ListItem Text="Suporte Técnico" Value="S" />
        </asp:DropDownList>
    </a>
    <br />
    <br />
    <br />
    <div id="pop-up" class="dvComentario">
        <h3 class="tituloHover">REGULATORIA:</h3>
        <i class="descricaoHover">Dúvidas sobre documentos disponibilizados na Os Online(Download de documentos)</i>
        <h3 class="tituloHover">PÓS-VENDAS:</h3>
        <i class="descricaoHover">Dúvidas relacionadas a OS Online (Ex: Abertura de chamado, OS, atendimento da OS)</i>
        <h3 class="tituloHover">SUPORTE TÉCNICO:</h3>
        <i class="descricaoHover">Dúvidas na aplicação do equipamento (Ex: Montagem, limpeza)</i>
        <h3 class="tituloHover"></h3>
        <i class="descricaoHover">Dúvidas técnicas dos equipamentos(Ex: Código de peças, dúvidas de reparo)</i>
        <h3 class="tituloHover"></h3>
        <i class="descricaoHover">Dúvidas na utilização dos documentos de Serviço(Ex: Manual de Serviço, Instruções, Registros)</i>
    </div>

    &nbsp;&nbsp;<asp:Label ID="lblDescricao" runat="server" CssClass="label" Text="Descrição:" />
    <br />
    &nbsp;&nbsp;<asp:TextBox ID="txtDescricao" runat="server" TextMode="MultiLine" CssClass="textArea3" />
    <br />
    <div id="containerAnexo" runat="server" class="dvContainer">
        <div class="anexo">
            <asp:FileUpload ID="UploadAnexoUm" CssClass="btnUpload" runat="server" />
        </div>
    </div>
    <br />
    <br />
    <br />
    <asp:Panel ID="pnlBotoes" runat="server">
        <table width="100%">
            <tr>
                <td>&nbsp;&nbsp;<uc7:AutoHideButton ID="btnEnviar" runat="server" CssButton="button" Text="Enviar" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <br />
    <asp:Panel runat="server" ID="pnlMensagm">
        <uc6:Mensagem ID="oMensagem" runat="server" />
    </asp:Panel>
</asp:Content>
