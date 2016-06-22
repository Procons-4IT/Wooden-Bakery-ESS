﻿<%@ Page Title="Change Password" Language="vb" AutoEventWireup="false" MasterPageFile="~/UIWB.Master" CodeBehind="ChangePassword.aspx.vb" Inherits="UIWoodenBakery.ChangePassword" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdateProgress ID="UpdateProgress" runat="server">
        <ProgressTemplate>
           <asp:Image ID="Image1" ImageUrl="~/Images/waiting.gif" AlternateText="Processing"
                runat="server" />
        </ProgressTemplate>
    </asp:UpdateProgress>
    <ajx:ModalPopupExtender ID="modalPopup" runat="server" TargetControlID="UpdateProgress" PopupControlID="UpdateProgress" BackgroundCssClass="modalPopup" />

    <asp:UpdatePanel ID="Update" runat="server">
        <ContentTemplate>

            <table width="99%" border="0" cellspacing="0" cellpadding="4" class="main_content">
                <tr>

                    <td height="30" align="left" colspan="2" valign="bottom" background="images/h_bg.png" style="border-bottom: 1px dotted; border-color: #f45501; background-repeat: repeat-x">
                        <div>
                            &nbsp;
                            <asp:Label ID="Label3" runat="server" Text="Change Password" CssClass="subheader" Style="float: left;"></asp:Label>
                            <span>
                                <asp:Label ID="lblNewTrip" runat="server" Text="" Visible="false"></asp:Label></span>
                        </div>
                    </td>
                </tr>

                <tr>
                    <td>
                        <asp:Panel ID="panelhome" runat="server" Width="98%">
                            <asp:ImageButton ID="btnhome" runat="server" ImageUrl="images/Homeicon.jpg" PostBackUrl="~/Home.aspx"
                                ToolTip="Home" />
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                  
                        </asp:Panel>
                        <asp:Label ID="Label2" runat="server" Text="" Style="color: Red;"></asp:Label>

                    </td>
                </tr>
            </table>

            <asp:Panel ID="panelnew" runat="server" Width="98%">
                <div id="Div1" runat="server" class="DivCorner" style="border: solid 2px LightSteelBlue; width: 98%;">
                    <table width="99%" border="0" cellspacing="0" cellpadding="4" class="main_content">
                        <tr>
                            <td width="15%">Old Password <a style="color: Red;">*</a></td>
                            <td>
                                <asp:TextBox ID="txtoldpwd" CssClass="txtbox" Width="150px" runat="server" TextMode="Password"></asp:TextBox>
                            </td>
                            <td width="15%"></td>
                            <td width="15%"></td>
                            <td width="20%"></td>
                        </tr>
                        <tr>
                            <td width="15%">New Password <a style="color: Red;">*</a></td>
                            <td width="15%">
                                <asp:TextBox ID="txtnewpwd" CssClass="txtbox" Width="150px" runat="server" TextMode="Password"></asp:TextBox>
                            </td>
                            <td width="15%"></td>
                            <td width="15%"></td>
                            <td width="20%"></td>
                        </tr>
                        <tr>
                            <td width="15%">Confirm Password <a style="color: Red;">*</a></td>
                            <td width="15%">
                                <asp:TextBox ID="txtconfirmpwd" CssClass="txtbox" Width="150px" runat="server" TextMode="Password"></asp:TextBox>

                            </td>
                            <td width="15%">
                                <asp:CompareValidator ID="CompareValidator" runat="server" ControlToValidate="txtnewpwd" ControlToCompare="txtconfirmpwd" ErrorMessage="Confirm Password does not match!">  
                                </asp:CompareValidator>
                            </td>
                            <td width="15%"></td>
                            <td width="20%"></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:Button ID="btnsave" runat="server" Text="Save" CssClass="btn" Width="85px" />

                                <asp:Button ID="btnclose" CssClass="btn" Width="85px" runat="server" Text="Cancel" />

                            </td>
                        </tr>
                    </table>
                </div>
            </asp:Panel>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
