<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="TestPage.aspx.vb" Inherits="OrdemServico.TestPage" %>

<%@ Register src="controlesespecificos/ClienteView.ascx" tagname="ClienteView" tagprefix="uc1" %>

<%@ Register src="controlesespecificos/BaseInstaladaView.ascx" tagname="BaseInstaladaView" tagprefix="uc2" %>

<%@ Register src="controlesespecificos/ProdutoView.ascx" tagname="ProdutoView" tagprefix="uc3" %>

<%@ Register src="controlesespecificos/ProdutoBox.ascx" tagname="ProdutoBox" tagprefix="uc4" %>

<%@ Register src="controlesespecificos/ClienteBox.ascx" tagname="ClienteBox" tagprefix="uc5" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<%@ Register src="componentes/controles/PhoneBox.ascx" tagname="PhoneBox" tagprefix="uc6" %>

<%@ Register src="componentes/controles/DateBox.ascx" tagname="DateBox" tagprefix="uc7" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title></title>
</head>

<body>
    <form id="form1" runat="server">
        <div>
            
            <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
            </asp:ToolkitScriptManager>
            
        <asp:Label ID="lblTeste" runat="server" Text="teste" /> <br/> 
            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            <br/>

            <asp:Button ID="btnVai" runat="server" Text="Vai" />


    </form>
</body>

</html>
