<%@ Page Title="Statement of Accounts" Language="vb" AutoEventWireup="false" MasterPageFile="~/UIWB.Master"
    CodeBehind="StofAccounts.aspx.vb" Inherits="UIWoodenBakery.StofAccounts" %>

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
    <ajx:ModalPopupExtender ID="modalPopup" runat="server" TargetControlID="UpdateProgress"
        PopupControlID="UpdateProgress" BackgroundCssClass="modalPopup" />
    <asp:UpdatePanel ID="Update" runat="server">
        <ContentTemplate>
            <table width="99%" border="0" cellspacing="0" cellpadding="4" class="main_content">
                <tr>
                    <td height="30" align="left" colspan="2" valign="bottom" background="images/h_bg.png"
                        style="border-bottom: 1px dotted; border-color: #f45501; background-repeat: repeat-x">
                        <div>
                            &nbsp;
                            <asp:Label ID="Label3" runat="server" Text="Sales Order" CssClass="subheader" Style="float: left;
                                margin-left: 10px;"></asp:Label>
                            <span>
                                <asp:Label ID="lblNewTrip" runat="server" Text="" Visible="false"></asp:Label></span>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <table width="99%" border="0" cellspacing="0" cellpadding="4" class="main_content">
                            <tr>
                                <td>
                                    <asp:Panel ID="panelhome" runat="server" Width="100%">
                                        <asp:ImageButton ID="btnhome" runat="server" ImageUrl="~/images/Homeicon.jpg" PostBackUrl="~/Home.aspx"
                                            ToolTip="Home" />
                                    </asp:Panel>
                                    <asp:Label ID="Label2" runat="server" Text="" Style="color: Red;"></asp:Label>
                                    <asp:Panel ID="PanelNewRequest" runat="server" Width="100%">
                                        <div id="Div1" runat="server" class="DivCorner" style="border: solid 2px LightSteelBlue;
                                            width: 100%;">
                                            <table width="99%" border="0" cellspacing="0" cellpadding="4" class="main_content">
                                                <tr>
                                                    <td >
                                                        CustomerCode
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblcardCode" CssClass="txtbox" Width="150px" runat="server"></asp:Label>
                                                    </td>
                                                    <td >
                                                        Customer Name
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblCardName" CssClass="txtbox" runat="server" Width="150px"></asp:Label>
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td >
                                                        From Date
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtpostDate" CssClass="txtbox" runat="server" ></asp:TextBox>
                                                        <ajx:CalendarExtender ID="CalendarExtender2" Animated="true" Format="dd/MM/yyyy"
                                                            runat="server" TargetControlID="txtpostDate" CssClass="cal_Theme1">
                                                        </ajx:CalendarExtender>
                                                    </td>
                                                    <td>
                                                        To Date
                                                    </td>
                                                    <td class="style1">
                                                        <asp:TextBox ID="txtdelDate" CssClass="txtbox" runat="server" ></asp:TextBox>
                                                        <ajx:CalendarExtender ID="CalendarExtender1" Animated="true" Format="dd/MM/yyyy"
                                                            runat="server" TargetControlID="txtdelDate" CssClass="cal_Theme1">
                                                        </ajx:CalendarExtender>
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btngo" CssClass="btn" Width="120px" runat="server" Text="Get Details" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <asp:Panel ID="Panel1" runat="server" Width="100%" BorderColor="LightSteelBlue" BorderWidth="2">
                                            <table width="100%" border="0" cellspacing="0" cellpadding="3" class="main_content">
                                                <tr>
                                                    <td valign="top">
                                                        <ajx:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" CssClass="ajax__tab_yuitabview-theme"
                                                            Width="100%">
                                                            <ajx:TabPanel ID="TabPanel2" runat="server" HeaderText="Accounts Statement">
                                                                <ContentTemplate>
                                                                    <table width="100%" border="0" cellspacing="0" cellpadding="3" class="main_content">
                                                                        <tr>
                                                                            <td>
                                                                                <div style="border: solid 0px #CCCCCC; width: auto; height: 25px; margin-top: 5px;">
                                                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="Label5" runat="server" Text="Opening Balance :"
                                                                                        Height="20px" Width="181px"></asp:Label>&nbsp;
                                                                                    <asp:Label ID="Label11" Text="" runat="server"></asp:Label>
                                                                                    <asp:TextBox ID="TextBox3" runat="server" Height="18px" Width="116px" Enabled="False"
                                                                                        Font-Names="Verdana" Font-Size="11pt"></asp:TextBox>&nbsp;&nbsp;&nbsp &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label
                                                                                            ID="Label6" runat="server" Text="Balance :" Height="20px" Width="91px"></asp:Label>&nbsp;
                                                                                    <asp:Label ID="Label12" Text="" runat="server"></asp:Label>
                                                                                    <asp:TextBox ID="TextBox4" runat="server" Height="18px" Width="116px" Enabled="False"
                                                                                        Font-Names="Verdana" Font-Size="11pt"></asp:TextBox> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                                         <asp:Button ID="btnexpexcel" CssClass="btn" Width="150px" runat="server" Text="Export to Excel" Visible="false" />
                                                                                </div>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:GridView ID="grdSalesItem" runat="server" CellPadding="4" RowStyle-CssClass="mousecursor"
                                                                                    CssClass="mGrid" HeaderStyle-CssClass="GridBG" ShowHeaderWhenEmpty="true" EmptyDataText="No records Found"
                                                                                    PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" AllowPaging="true"  PageSize="20"
                                                                                    AutoGenerateColumns="false" Width="100%">
                                                                                    <Columns>
                                                                                        <asp:TemplateField HeaderText="Date">
                                                                                            <ItemTemplate>
                                                                                                <div align="left">
                                                                                                    &#160;&nbsp;<asp:Label ID="lblDocNo" runat="server" Text='<%#Bind("Date")%>'></asp:Label></div>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Transaction">
                                                                                            <ItemTemplate>
                                                                                                <div align="left">
                                                                                                    &#160;&nbsp;<asp:Label ID="lblItCode" runat="server" Text='<%#Bind("Transaction")%>'></asp:Label></div>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Ref">
                                                                                            <ItemTemplate>
                                                                                                <div align="left">
                                                                                                    &#160;&nbsp;<asp:Label ID="lblItName" runat="server" Text='<%#Bind("Ref")%>'></asp:Label></div>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Memo">
                                                                                            <ItemTemplate>
                                                                                                <div align="left">
                                                                                                    &#160;&nbsp;<asp:Label ID="lblFrgName" runat="server" Text='<%#Bind("Memo")%>'></asp:Label></div>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Debit LC">
                                                                                            <ItemTemplate>
                                                                                                <div align="right">
                                                                                                    &#160;&nbsp;<asp:Label ID="lblQty" runat="server" Text='<%#Bind("Debit")%>'></asp:Label></div>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Credit LC">
                                                                                            <ItemTemplate>
                                                                                                <div align="right">
                                                                                                    &#160;&nbsp;<asp:Label ID="lblUom" runat="server" Text='<%#Bind("Credit")%>'></asp:Label></div>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                          <asp:TemplateField HeaderText="Foreign Currency">
                                                                                            <ItemTemplate>
                                                                                                <div align="right">
                                                                                                    &#160;&nbsp;<asp:Label ID="lblFCQty" runat="server" Text='<%#Bind("FCCurrency")%>'></asp:Label></div>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <%--  <asp:TemplateField HeaderText="Debit FC" Visible="false">
                                                                                            <ItemTemplate>
                                                                                                <div align="right">
                                                                                                    &#160;&nbsp;<asp:Label ID="lblFQty" runat="server" Text='<%#Bind("FCDebit")%>'></asp:Label></div>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Credit FC" Visible="false">
                                                                                            <ItemTemplate>
                                                                                                <div align="right">
                                                                                                    &#160;&nbsp;<asp:Label ID="lblFUom" runat="server" Text='<%#Bind("FCCredit")%>'></asp:Label></div>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>--%>
                                                                                    </Columns>
                                                                                    <HeaderStyle BackColor="Gray" HorizontalAlign="Center" ForeColor="White" Height="25px" />
                                                                                    <RowStyle HorizontalAlign="Center" />
                                                                                    <AlternatingRowStyle HorizontalAlign="Center" CssClass="mousecursor" />
                                                                                </asp:GridView>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </ContentTemplate>
                                                            </ajx:TabPanel>
                                                        </ajx:TabContainer>
                                                    </td>                                                
                                            </table>
                                        </asp:Panel>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>      
    </asp:UpdatePanel>
</asp:Content>
