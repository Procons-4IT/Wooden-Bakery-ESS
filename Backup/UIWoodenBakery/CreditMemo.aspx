<%@ Page Title="Credit Memo" Language="vb" AutoEventWireup="false" MasterPageFile="~/UIWB.Master"
    CodeBehind="CreditMemo.aspx.vb" Inherits="UIWoodenBakery.CreditMemo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function AllowNumbers(el) {
            var ex = /^[0-9.]+$/;
            if (ex.test(el.value) == false) {
                el.value = el.value.substring(0, el.value.length - 1);
            }
        }
        function alphanumerichypen(el) {
            var ex = /^[A-Za-z0-9_-]+$/;
            if (ex.test(el.value) == false) {
                el.value = el.value.substring(0, el.value.length - 1);
            }
        }

        function checkDec(el) {
            el.value = el.value.replace(/^[ 0]+/, '');
            var ex = /^\d*\.?\d{0,2}$/;
            if (ex.test(el.value) == false) {
                el.value = el.value.substring(0, el.value.length - 1);
            }
        }

        function RemoveZero(el) {
            el.value = el.value.replace(/^[ 0]+/, '');
        }

        function popupdisplay(option, uniqueid, tripno) {
            if (uniqueid.length > 0) {
                var uniid = document.getElementById("<%=txtpopunique.ClientID%>").value;
                var tno = document.getElementById("<%=txtpoptno.ClientID%>").value;
                var opt = document.getElementById("<%=txthidoption.ClientID%>").value;
                uniid = ""; tno = ""; opt = "";
                if (uniid != uniqueid && tno != tripno && opt != option) {
                    document.getElementById("<%=txtpopunique.ClientID%>").value = uniqueid;
                    document.getElementById("<%=txtpoptno.ClientID%>").value = tripno;
                    document.getElementById("<%=txthidoption.ClientID%>").value = option;
                    document.getElementById("<%=Btncallpop.ClientID%>").onclick();
                }
            }
        }
    </script>
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
                    <td>
                        <asp:TextBox ID="txtHEmpID" runat="server" Width="93px" Style="display: none"></asp:TextBox>
                        <asp:TextBox ID="txtpopunique" runat="server" Style="display: none"></asp:TextBox>
                        <asp:TextBox ID="txtpoptno" runat="server" Style="display: none"></asp:TextBox>
                        <asp:TextBox ID="txthidoption" runat="server" Style="display: none"></asp:TextBox>
                        <asp:TextBox ID="txttname" runat="server" Style="display: none"></asp:TextBox>
                        <input id="Btncallpop" runat="server" onserverclick="Btncallpop_ServerClick" style="display: none"
                            type="button" value="button" />
                    </td>
                </tr>
                <tr>
                    <td height="30" align="left" colspan="2" valign="bottom" background="images/h_bg.png"
                        style="border-bottom: 1px dotted; border-color: #f45501; background-repeat: repeat-x">
                        <div>
                            &nbsp;
                            <asp:Label ID="Label3" runat="server" Text="Credit Memo" CssClass="subheader" Style="float: left;
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
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:ImageButton ID="btnnew" runat="server" ImageUrl="~/images/Add.jpg" ToolTip="Add new record" />
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    </asp:Panel>
                                    <asp:Label ID="Label2" runat="server" Text="" Style="color: Red;"></asp:Label>
                                    <asp:Panel ID="panelview" runat="server" Width="100%">
                                        <table width="99%" border="0" cellspacing="0" cellpadding="4" class="main_content">
                                            <tr>
                                                <td valign="top">
                                                    <ajx:TabContainer ID="TabContainer2" runat="server" ActiveTabIndex="0" CssClass="ajax__tab_yuitabview-theme"
                                                        Width="100%">
                                                        <ajx:TabPanel ID="TabPanel3" runat="server" HeaderText="Credit Memo Draft">
                                                            <ContentTemplate>
                                                                <table width="100%" border="0" cellspacing="0" cellpadding="3" class="main_content">
                                                                    <tr>
                                                                        <td>
                                                                            <asp:GridView ID="grdSalesDraft" runat="server" CellPadding="4" AllowPaging="True"
                                                                                ShowHeaderWhenEmpty="true" CssClass="mGrid" HeaderStyle-CssClass="GridBG" PagerStyle-CssClass="pgr"
                                                                                AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="false" Width="100%" PageSize="15">
                                                                                <Columns>
                                                                                    <asp:TemplateField HeaderText="Document Code">
                                                                                        <ItemTemplate>
                                                                                            <div align="center">
                                                                                                <asp:LinkButton ID="lbtDdocnum" runat="server" Text='<%#Bind("DocEntry") %>' OnClick="lbtndocnum_Click"> <%----%>
                                                                                                </asp:LinkButton>
                                                                                            </div>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Customer Code">
                                                                                        <ItemTemplate>
                                                                                            <div align="left">
                                                                                                <asp:Label ID="lblDCardCode" runat="server" Text='<%#Bind("CardCode")%>'></asp:Label>
                                                                                            </div>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Customer Name">
                                                                                        <ItemTemplate>
                                                                                            <div align="left">
                                                                                                <asp:Label ID="lblDCardName" runat="server" Text='<%#Bind("CardName")%>'></asp:Label>
                                                                                            </div>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Posting Date">
                                                                                        <ItemTemplate>
                                                                                            <div align="left">
                                                                                                &nbsp;<asp:Label ID="lblDPostDate" runat="server" Text='<%#Bind("DocDate")%>'></asp:Label>
                                                                                            </div>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Delivery Date">
                                                                                        <ItemTemplate>
                                                                                            <div align="left">
                                                                                                &nbsp;<asp:Label ID="lblDDelidate" runat="server" Text='<%#Bind("DocDueDate")%>'></asp:Label>
                                                                                            </div>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Document Date">
                                                                                        <ItemTemplate>
                                                                                            <div align="left">
                                                                                                &nbsp;<asp:Label ID="lblDTaxDate" runat="server" Text='<%#Bind("TaxDate")%>'></asp:Label>
                                                                                            </div>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Document Status">
                                                                                        <ItemTemplate>
                                                                                            <div align="left">
                                                                                                &nbsp;<asp:Label ID="lblDstatus" runat="server" Text='<%#Bind("DocStatus")%>'></asp:Label>
                                                                                            </div>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                </Columns>
                                                                                <HeaderStyle HorizontalAlign="Center" Height="25px" BackColor="#CCCCCC" />
                                                                            </asp:GridView>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                                <table width="100%" border="0" cellspacing="0" cellpadding="3" class="main_content">
                                                                    <tr>
                                                                        <td>
                                                                            <a class="txtbox" style="text-decoration: underline; font-weight: bold;">Contents</a>
                                                                            <br />
                                                                            <asp:GridView ID="grdDrfLines" runat="server" CellPadding="4" AllowPaging="True"
                                                                                ShowFooter="true" ShowHeaderWhenEmpty="true" CssClass="mGrid" HeaderStyle-CssClass="GridBG"
                                                                                PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="false"
                                                                                Width="100%" PageSize="20">
                                                                                <Columns>
                                                                                    <asp:TemplateField HeaderText="Document Code" Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <div align="left">
                                                                                                <asp:Label ID="lbtnDLdocnum" runat="server" Text='<%#Bind("DocEntry") %>'> 
                                                                                                </asp:Label>
                                                                                            </div>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Item Code">
                                                                                        <ItemTemplate>
                                                                                            <div align="left">
                                                                                                <asp:Label ID="lblDLItemCode" runat="server" Text='<%#Bind("ItemCode")%>'></asp:Label>
                                                                                            </div>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Item Name">
                                                                                        <ItemTemplate>
                                                                                            <div align="left">
                                                                                                <asp:Label ID="lblDLItemName" runat="server" Text='<%#Bind("Dscription")%>'></asp:Label>
                                                                                            </div>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="UoM">
                                                                                        <ItemTemplate>
                                                                                            <div align="left">
                                                                                                &#160;<asp:Label ID="lblDLUoM" runat="server" Text='<%#Bind("UomCode")%>'></asp:Label></div>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Quantity">
                                                                                        <ItemTemplate>
                                                                                            <div align="right">
                                                                                                &nbsp;<asp:Label ID="lblDLQty" runat="server" Text='<%#Bind("Quantity")%>'></asp:Label>
                                                                                            </div>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Price">
                                                                                        <ItemTemplate>
                                                                                            <div align="right">
                                                                                                &nbsp;<asp:Label ID="lblDLPrice" runat="server" Text='<%#Bind("Price")%>'></asp:Label>
                                                                                            </div>
                                                                                        </ItemTemplate>
                                                                                        <FooterTemplate>
                                                                                            <div align="right">
                                                                                                <asp:Label ID="lblDCur" runat="server" Text="Total :"></asp:Label>&nbsp;</div>
                                                                                        </FooterTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Total">
                                                                                        <ItemTemplate>
                                                                                            <div align="right">
                                                                                                &nbsp;<asp:Label ID="lblDLTotal" runat="server" Text='<%#Bind("LineTotal")%>'></asp:Label>
                                                                                            </div>
                                                                                        </ItemTemplate>
                                                                                        <FooterTemplate>
                                                                                            <div align="right">
                                                                                                <asp:Label ID="lblDCurTotal" runat="server"></asp:Label>&nbsp;</div>
                                                                                        </FooterTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Line Status">
                                                                                        <ItemTemplate>
                                                                                            <div align="left">
                                                                                                &nbsp;<asp:Label ID="lblDLstatus" runat="server" Text='<%#Bind("LineStatus")%>'></asp:Label>
                                                                                            </div>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                </Columns>
                                                                                <HeaderStyle HorizontalAlign="Center" Height="25px" BackColor="#CCCCCC" />
                                                                                <FooterStyle Height="25px" BackColor="#CCCCCC" Font-Bold="true" />
                                                                            </asp:GridView>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ContentTemplate>
                                                        </ajx:TabPanel>
                                                        <ajx:TabPanel ID="TabPanel1" runat="server" HeaderText="Credit Memo Document">
                                                            <ContentTemplate>
                                                                <table width="100%" border="0" cellspacing="0" cellpadding="3" class="main_content">
                                                                    <tr>
                                                                        <td>
                                                                            <asp:GridView ID="grdSales" runat="server" CellPadding="4" AllowPaging="True" ShowHeaderWhenEmpty="true"
                                                                                CssClass="mGrid" HeaderStyle-CssClass="GridBG" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                                                                AutoGenerateColumns="false" Width="100%" PageSize="15">
                                                                                <Columns>
                                                                                    <asp:TemplateField HeaderText="Document Code">
                                                                                        <ItemTemplate>
                                                                                            <div align="center">
                                                                                                <asp:LinkButton ID="lbtnSdocnum" runat="server" Text='<%#Bind("DocEntry") %>' OnClick="lbtnSdocnum_Click"> <%----%>
                                                                                                </asp:LinkButton>
                                                                                            </div>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Customer Code">
                                                                                        <ItemTemplate>
                                                                                            <div align="left">
                                                                                                <asp:Label ID="lblCardCode" runat="server" Text='<%#Bind("CardCode")%>'></asp:Label>
                                                                                            </div>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Customer Name">
                                                                                        <ItemTemplate>
                                                                                            <div align="left">
                                                                                                <asp:Label ID="lblCardName" runat="server" Text='<%#Bind("CardName")%>'></asp:Label>
                                                                                            </div>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Posting Date">
                                                                                        <ItemTemplate>
                                                                                            <div align="left">
                                                                                                &nbsp;<asp:Label ID="lblDPostDate" runat="server" Text='<%#Bind("DocDate")%>'></asp:Label>
                                                                                            </div>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Delivery Date">
                                                                                        <ItemTemplate>
                                                                                            <div align="left">
                                                                                                &nbsp;<asp:Label ID="lblDelidate" runat="server" Text='<%#Bind("DocDueDate")%>'></asp:Label>
                                                                                            </div>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Document Date">
                                                                                        <ItemTemplate>
                                                                                            <div align="left">
                                                                                                &nbsp;<asp:Label ID="lblTaxDate" runat="server" Text='<%#Bind("TaxDate")%>'></asp:Label>
                                                                                            </div>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Document Status">
                                                                                        <ItemTemplate>
                                                                                            <div align="left">
                                                                                                &nbsp;<asp:Label ID="lblstatus" runat="server" Text='<%#Bind("DocStatus")%>'></asp:Label>
                                                                                            </div>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                </Columns>
                                                                                <HeaderStyle HorizontalAlign="Center" Height="25px" BackColor="#CCCCCC" />
                                                                            </asp:GridView>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                                <table width="100%" border="0" cellspacing="0" cellpadding="3" class="main_content">
                                                                    <tr>
                                                                        <td>
                                                                            <a class="txtbox" style="text-decoration: underline; font-weight: bold;">Contents</a>
                                                                            <br />
                                                                            <asp:GridView ID="grdSalesLine" runat="server" CellPadding="4" AllowPaging="True"
                                                                                ShowHeaderWhenEmpty="true" CssClass="mGrid" HeaderStyle-CssClass="GridBG" PagerStyle-CssClass="pgr"
                                                                                AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="false" Width="100%" PageSize="20"
                                                                                ShowFooter="true">
                                                                                <Columns>
                                                                                    <asp:TemplateField HeaderText="Document Code" Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <div align="center">
                                                                                                <asp:Label ID="lbtnLdocnum" runat="server" Text='<%#Bind("DocEntry") %>'> <%--OnClick="lbtndocnum_Click"--%>
                                                                                                </asp:Label>
                                                                                            </div>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Item Code">
                                                                                        <ItemTemplate>
                                                                                            <div align="left">
                                                                                                <asp:Label ID="lblLItemCode" runat="server" Text='<%#Bind("ItemCode")%>'></asp:Label>
                                                                                            </div>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Item Name">
                                                                                        <ItemTemplate>
                                                                                            <div align="left">
                                                                                                <asp:Label ID="lblLItemName" runat="server" Text='<%#Bind("Dscription")%>'></asp:Label>
                                                                                            </div>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="UoM">
                                                                                        <ItemTemplate>
                                                                                            <div align="left">
                                                                                                &#160;<asp:Label ID="lblLUoM" runat="server" Text='<%#Bind("UomCode")%>'></asp:Label></div>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Quantity">
                                                                                        <ItemTemplate>
                                                                                            <div align="right">
                                                                                                &nbsp;<asp:Label ID="lblLQty" runat="server" Text='<%#Bind("Quantity")%>'></asp:Label>
                                                                                            </div>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Price">
                                                                                        <ItemTemplate>
                                                                                            <div align="right">
                                                                                                &nbsp;<asp:Label ID="lblLPrice" runat="server" Text='<%#Bind("Price")%>'></asp:Label>
                                                                                            </div>
                                                                                        </ItemTemplate>
                                                                                        <FooterTemplate>
                                                                                            <div align="right">
                                                                                                <asp:Label ID="lblSCur" runat="server" Text="Total :"></asp:Label>&nbsp;</div>
                                                                                        </FooterTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Total">
                                                                                        <ItemTemplate>
                                                                                            <div align="right">
                                                                                                &nbsp;<asp:Label ID="lblLTotal" runat="server" Text='<%#Bind("LineTotal")%>'></asp:Label>
                                                                                            </div>
                                                                                        </ItemTemplate>
                                                                                        <FooterTemplate>
                                                                                            <div align="right">
                                                                                                <asp:Label ID="lblSCurTotal" runat="server"></asp:Label>&nbsp;</div>
                                                                                        </FooterTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Line Status">
                                                                                        <ItemTemplate>
                                                                                            <div align="left">
                                                                                                &nbsp;<asp:Label ID="lblLstatus" runat="server" Text='<%#Bind("LineStatus")%>'></asp:Label>
                                                                                            </div>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                </Columns>
                                                                                <HeaderStyle HorizontalAlign="Center" Height="25px" BackColor="#CCCCCC" />
                                                                                <FooterStyle Height="25px" BackColor="#CCCCCC" Font-Bold="true" />
                                                                            </asp:GridView>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ContentTemplate>
                                                        </ajx:TabPanel>
                                                    </ajx:TabContainer>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                    <asp:Panel ID="PanelNewRequest" runat="server" Width="100%">
                                        <div id="Div1" runat="server" class="DivCorner" style="border: solid 2px LightSteelBlue;
                                            width: 100%;">
                                            <table width="99%" border="0" cellspacing="0" cellpadding="4" class="main_content">
                                                <tr>
                                                    <td width="7%" style="margin-left: 20px;">
                                                        CustomerCode
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblcardCode" CssClass="txtbox" Width="150px" runat="server"></asp:Label>
                                                    </td>
                                                    <td width="5%">
                                                    </td>
                                                    <td width="15%">
                                                        Customer Name
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblCardName" CssClass="txtbox" runat="server" Width="150px"></asp:Label>
                                                        <asp:TextBox ID="txtdocdate" CssClass="txtbox" runat="server" Visible="false"></asp:TextBox>
                                                        <ajx:CalendarExtender ID="CalendarExtender3" Animated="true" Format="dd/MM/yyyy"
                                                            runat="server" TargetControlID="txtdocdate" CssClass="cal_Theme1">
                                                        </ajx:CalendarExtender>
                                                        <asp:Label ID="lblSalesUom" CssClass="txtbox" runat="server" Visible="false"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="10%">
                                                        Posting Date
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtpostDate" CssClass="txtbox" runat="server"></asp:TextBox>
                                                        <ajx:CalendarExtender ID="CalendarExtender2" Animated="true" Format="dd/MM/yyyy"
                                                            runat="server" TargetControlID="txtpostDate" CssClass="cal_Theme1">
                                                        </ajx:CalendarExtender>
                                                    </td>
                                                    <td width="5%">
                                                    </td>
                                                    <td width="10%">
                                                        Delivery Date
                                                    </td>
                                                    <td class="style1">
                                                        <asp:TextBox ID="txtdelDate" CssClass="txtbox" runat="server"></asp:TextBox>
                                                        <ajx:CalendarExtender ID="CalendarExtender1" Animated="true" Format="dd/MM/yyyy"
                                                            runat="server" TargetControlID="txtdelDate" CssClass="cal_Theme1">
                                                        </ajx:CalendarExtender>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="10%">
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td width="5%">
                                                    </td>
                                                    <td width="10%">
                                                        Status
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblDocStatus" CssClass="txtbox" runat="server" Width="150px" Text="Open"></asp:Label>
                                                        <asp:DropDownList ID="ddlconPerson" CssClass="txtbox1" Width="150px" runat="server"
                                                            Visible="false">
                                                        </asp:DropDownList>
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
                                                            <ajx:TabPanel ID="TabPanel2" runat="server" HeaderText="General">
                                                                <ContentTemplate>
                                                                    <panel>
                                                                <div>
                                                                 <asp:Button ID="btnImport" CssClass="btn" runat="server" Text="Import from BP catalogue number" />
                                                               
                                                                </div>
                                                                </panel>
                                                                    <table width="100%" border="0" cellspacing="0" cellpadding="3" class="main_content">
                                                                        <tr>
                                                                            <td>
                                                                                ItemCode
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtItemCode" CssClass="txtbox" runat="server" Enabled="false"></asp:TextBox>
                                                                                <asp:ImageButton ID="btnItem1" runat="server" Text="Find" ImageUrl="~/images/search.jpg" />
                                                                                <ajx:ModalPopupExtender ID="popemployee" runat="server" DropShadow="true" PopupControlID="Panelpopemployee"
                                                                                    TargetControlID="btnItem1" CancelControlID="btnclstech1" BackgroundCssClass="modalBackground">
                                                                                </ajx:ModalPopupExtender>
                                                                                <asp:TextBox ID="txtItemName" CssClass="txtbox" Width="150px" runat="server" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td>
                                                                                Return UoM
                                                                            </td>
                                                                            <td>
                                                                                <asp:DropDownList ID="ddlUoM" CssClass="txtbox1" Width="100px" runat="server" Enabled="false">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                            <td>
                                                                                Quantity
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtQty" CssClass="txtbox" runat="server" onkeypress="AllowNumbers(this);checkDec(this);RemoveZero(this);"
                                                                                    onkeyup="AllowNumbers(this);checkDec(this);RemoveZero(this);"></asp:TextBox>
                                                                            </td>
                                                                            <td>
                                                                                Price
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtPrice" CssClass="txtbox" runat="server" Enabled="false" onkeypress="AllowNumbers(this);checkDec(this);RemoveZero(this);"
                                                                                    onkeyup="AllowNumbers(this);checkDec(this);RemoveZero(this);"></asp:TextBox>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Button ID="btnSubmit" CssClass="btn" runat="server" Text="Add" />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="8">
                                                                                <asp:GridView ID="grdSalesItem" runat="server" CellPadding="4" RowStyle-CssClass="mousecursor"
                                                                                    CssClass="mGrid" HeaderStyle-CssClass="GridBG" ShowHeaderWhenEmpty="true" EmptyDataText="No records Found"
                                                                                    PagerStyle-CssClass="pgr" AutoGenerateDeleteButton="true" AlternatingRowStyle-CssClass="alt"
                                                                                    ShowFooter="true" AutoGenerateColumns="false" Width="100%">
                                                                                    <Columns>
                                                                                        <asp:TemplateField HeaderText="DocEntry" Visible="false">
                                                                                            <ItemTemplate>
                                                                                                <div align="left">
                                                                                                    &nbsp;
                                                                                                    <asp:Label ID="lblDocNo" runat="server" Text='<%#Bind("U_DocEntry")%>'></asp:Label>
                                                                                                </div>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="ItemCode">
                                                                                            <ItemTemplate>
                                                                                                <div align="left">
                                                                                                    &nbsp;
                                                                                                    <asp:Label ID="lblItCode" runat="server" Text='<%#Bind("U_ItemCode")%>'></asp:Label>
                                                                                                </div>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="ItemName">
                                                                                            <ItemTemplate>
                                                                                                <div align="left">
                                                                                                    &nbsp;
                                                                                                    <asp:Label ID="lblItName" runat="server" Text='<%#Bind("U_ItemName")%>'></asp:Label>
                                                                                                </div>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Foreign Name">
                                                                                            <ItemTemplate>
                                                                                                <div align="left">
                                                                                                    &#160;&nbsp;<asp:Label ID="lblFrgName" runat="server" Text='<%#Bind("U_frgName")%>'></asp:Label></div>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Quantity">
                                                                                            <ItemTemplate>
                                                                                                <div align="right" width="100px">
                                                                                                    &#160;&nbsp;<asp:TextBox ID="lblQty" runat="server" Text='<%#Bind("U_Quantity")%>'
                                                                                                        AutoPostBack="true" OnTextChanged="lblQty_TextChanged" onkeypress="AllowNumbers(this);checkDec(this);RemoveZero(this);"
                                                                                                        onkeyup="AllowNumbers(this);checkDec(this);RemoveZero(this);"></asp:TextBox></div>
                                                                                            </ItemTemplate>
                                                                                            <FooterTemplate>
                                                                                                <div align="right">
                                                                                                    <asp:Label ID="lblRCur" runat="server" Text="Total :"></asp:Label>&nbsp;</div>
                                                                                            </FooterTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Price">
                                                                                            <ItemTemplate>
                                                                                                <div align="right">
                                                                                                    &#160;&nbsp;<asp:Label ID="lblPrice" runat="server" Text='<%#Bind("U_Price")%>'></asp:Label></div>
                                                                                            </ItemTemplate>
                                                                                            <FooterTemplate>
                                                                                                <div align="right">
                                                                                                    <asp:Label ID="lblRCurTotal" runat="server"></asp:Label>&nbsp;</div>
                                                                                            </FooterTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="UoM">
                                                                                            <ItemTemplate>
                                                                                                <div align="left">
                                                                                                    &#160;&nbsp;<asp:Label ID="lblUom" runat="server" Text='<%#Bind("U_UoMName")%>'></asp:Label></div>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                    </Columns>
                                                                                    <HeaderStyle BackColor="Gray" HorizontalAlign="Center" ForeColor="White" Height="25px" />
                                                                                    <RowStyle HorizontalAlign="Center" />
                                                                                    <AlternatingRowStyle HorizontalAlign="Center" CssClass="mousecursor" />
                                                                                    <FooterStyle Height="25px" BackColor="#CCCCCC" Font-Bold="true" />
                                                                                </asp:GridView>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </ContentTemplate>
                                                            </ajx:TabPanel>
                                                            <%-- <ajx:TabPanel ID="TabPanel4" runat="server" HeaderText="Content">
                                                            <ContentTemplate>
                                                                <table width="100%" border="0" cellspacing="0" cellpadding="3" class="main_content">
                                                                    <tr>
                                                                        <td valign="top">
                                                                            <asp:TextBox ID="txtContent" runat="server" CssClass="txtbox" Width="350px" Height="150px"
                                                                                TextMode="MultiLine"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ContentTemplate>
                                                        </ajx:TabPanel>--%>
                                                        </ajx:TabContainer>
                                                    </td>
                                                    <tr>
                                                        <td>
                                                            <br />
                                                            <asp:Button ID="btnAdd" CssClass="btn" Width="85px" runat="server" Text="Save" ValidationGroup="Test"
                                                                OnClientClick="return Confirmation();" />
                                                            <asp:Button ID="btncancel" CssClass="btn" Width="85px" runat="server" Text="Cancel" />
                                                        </td>
                                                    </tr>
                                            </table>
                                        </asp:Panel>
                                    </asp:Panel>
                                    <asp:Panel ID="Panelpopemployee" runat="server" BackColor="White" Style="display: none;
                                        padding: 10px; width: 500px;">
                                        <div>
                                            <span class="sideheading" style="color: Green;">ItemS Details</span> <span style="float: right;">
                                                <asp:Button ID="btnclstech1" runat="server" CssClass="btn" Width="30px" Text="X" /></span>
                                        </div>
                                        <div>
                                            <span>
                                                <asp:Label ID="lblitem" runat="server" Text="Item Code :"></asp:Label></span>
                                            <asp:TextBox ID="txtsearchItem" runat="server" Width="150px"></asp:TextBox>
                                            <ajx:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="txtsearchItem"
                                                MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1" CompletionInterval="100"
                                                ServiceMethod="GetItems">
                                            </ajx:AutoCompleteExtender>
                                            <br />
                                            <span>
                                                <asp:Label ID="lblitemNa" runat="server" Text="Item Name :"></asp:Label></span>
                                            <asp:TextBox ID="txtsearchItemNa" runat="server" Width="150px"></asp:TextBox>
                                            <ajx:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" TargetControlID="txtsearchItemNa"
                                                MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1" CompletionInterval="100"
                                                ServiceMethod="GetItemsName">
                                            </ajx:AutoCompleteExtender>
                                            <asp:Button ID="btngoItem" runat="server" Text="Go" CssClass="button" Width="50px" />
                                            <asp:LinkButton ID="lbtpopnview" runat="server">View All</asp:LinkButton>
                                            <br />
                                        </div>
                                        <br />
                                        <br />
                                        <asp:Panel ID="Panel4" runat="server" Height="200px" ScrollBars="Vertical">
                                            <asp:GridView ID="grdItems" runat="server" CellPadding="4" RowStyle-CssClass="mousecursor"
                                                ShowHeaderWhenEmpty="true" EmptyDataText="No records Found" CssClass="mGrid"
                                                HeaderStyle-CssClass="GridBG" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                                AutoGenerateColumns="false" Width="100%">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="ItemCode">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblpItemCode" runat="server" Text='<%#Bind("ItemCode")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Description">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPItemName" runat="server" Text='<%#Bind("ItemName")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <HeaderStyle BackColor="Gray" HorizontalAlign="Center" ForeColor="White" Height="25px" />
                                                <RowStyle HorizontalAlign="Center" />
                                                <AlternatingRowStyle HorizontalAlign="Center" CssClass="mousecursor" />
                                            </asp:GridView>
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
