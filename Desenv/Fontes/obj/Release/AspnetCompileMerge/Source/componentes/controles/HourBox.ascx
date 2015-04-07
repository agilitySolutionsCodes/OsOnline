<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="HourBox.ascx.vb" Inherits="OrdemServico.HourBox" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:TextBox ID="txtHour" runat="server" CssClass="textBox2" ValidationGroup="MKE" />

<cc1:MaskedEditExtender ID="txtHour_MaskedEditExtender" runat="server"
    TargetControlID="txtHour"
    Mask="99:99" 
    MessageValidatorTip="true"
    OnFocusCssClass="MaskedEditFocus"
    OnInvalidCssClass="MaskedEditError"
    MaskType="Time"
    AcceptAMPM="False"
    ErrorTooltipEnabled="True" />
        
<cc1:MaskedEditValidator ID="txtHour_MaskedEditValidator" runat="server"
    ControlExtender="txtHour_MaskedEditExtender"
    ControlToValidate="txtHour"
    IsValidEmpty="False"
    EmptyValueMessage=""
    InvalidValueMessage=""
    Display="Static"
    TooltipMessage=""
    EmptyValueBlurredText=""
    InvalidValueBlurredMessage=""
    ValidationGroup="MKE" />