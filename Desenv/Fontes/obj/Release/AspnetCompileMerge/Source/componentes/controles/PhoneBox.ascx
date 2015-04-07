<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="PhoneBox.ascx.vb" Inherits="OrdemServico.PhoneBox" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:TextBox ID="txtPhone" runat="server" Width="130px" Height="16px" ValidationGroup="MKE" />

<cc1:MaskedEditExtender ID="txtPhone_MaskedEditExtender" runat="server"
    TargetControlID="txtPhone" 
    Mask="(99) 9999-9999"
    MessageValidatorTip="true"
    OnFocusCssClass="MaskedEditFocus"
    OnInvalidCssClass="MaskedEditError"
    MaskType="Number"
    AcceptAMPM="False"
    ErrorTooltipEnabled="True" />
        
<cc1:MaskedEditValidator ID="txtTelefone_MaskedEditValidator" runat="server"
    ControlExtender="txtPhone_MaskedEditExtender"
    ControlToValidate="txtPhone"
    IsValidEmpty="False"
    EmptyValueMessage=""
    InvalidValueMessage=""
    Display="Static"
    TooltipMessage=""
    EmptyValueBlurredText=""
    InvalidValueBlurredMessage=""
    ValidationGroup="MKE" />
