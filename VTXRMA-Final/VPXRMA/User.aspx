<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="User.aspx.cs" Inherits="VPXRMA.User" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .gridSize {
            max-width: 100%;
            width: 70%;
        }

        td {
            cursor: default;
        }

        .hover_row {
            background-color: #A1DCF2;
        }
    </style>
    <script type="text/javascript">

         <%--function to show mouse hove on grid--%>
        $(function () {
            $("[id*=gv_CreateUser] td").hover(function () {
                $("td", $(this).closest("tr")).addClass("hover_row");
            }, function () {
                $("td", $(this).closest("tr")).removeClass("hover_row");
            });
        });


         <%--function to auto hide msg label--%>
        function HideLabel() {
            var seconds = 5;
            setTimeout(function () {
                document.getElementById("<%=lblMsg.ClientID %>").style.display = "none";
            }, seconds * 1000);
        };
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="dashboard-wrapper">
        <div class="dashboard-ecommerce">
            <div class="container-fluid dashboard-content">
                <h4 class="auto-style1">User Entry :</h4>
                <hr style="border-bottom: 1px thin; border-bottom-color: crimson; width: 100%; height: 5px" />
                <div class="MsgStyle">
                    <asp:Label ID="lblMsg" runat="server" ForeColor="Red" Text="" Visible="false" CssClass="MsgStyle"></asp:Label>
                </div>
                <div class="row">
                    <div class="form-group col-md-6">
                        <asp:Label ID="lbluserid" runat="server" Text="User Id"></asp:Label>
                        <asp:TextBox ID="txtUserId" CssClass="form-control" runat="server" ValidationGroup="*"></asp:TextBox>

                    </div>

                    <div class="form-group col-md-6">

                        <asp:Label ID="lblusername" runat="server" Text="User Name"></asp:Label>
                        <asp:TextBox ID="txtUserName" CssClass="form-control" runat="server"></asp:TextBox>

                    </div>
                </div>
                <div class="row">
                    <div class="form-group col-md-6">
                        <asp:Label ID="Label2" runat="server" Text="EmailId"></asp:Label>
                        <asp:TextBox ID="txtemail" CssClass="form-control" runat="server" ValidationGroup="ursergrp"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="entryemalid" ValidationGroup="ursergrp" Display="Dynamic"
                            ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"
                            ControlToValidate="txtemail"
                            runat="server" ForeColor="#ff0000"
                            ErrorMessage="* Enter a valid email address"></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="ursergrp"
                            ControlToValidate="txtemail"
                            ErrorMessage="EmailId is a required field."
                            ForeColor="Red">
                        </asp:RequiredFieldValidator>





                    </div>
                    <div class="form-group col-md-6">
                        <asp:Label ID="Label1" runat="server" Text="Mobile No"></asp:Label>
                        <asp:TextBox ID="txtmobileno" CssClass="form-control" runat="server"></asp:TextBox>


                    </div>
                </div>


                <div class="row">
                    <div class="form-group col-md-6">

                        <asp:Label ID="lbl_password" runat="server" Text="Password"></asp:Label>
                        <asp:TextBox ID="txtPassword" CssClass="form-control" runat="server" TextMode="Password"></asp:TextBox>

                    </div>


                    <div class="form-group col-md-6">
                        <asp:Label ID="lblRemarks" runat="server" Text="Remarks"></asp:Label>
                        <asp:TextBox ID="txtRemarks" CssClass="form-control" runat="server" TextMode="MultiLine" Height="40px"></asp:TextBox>


                    </div>
                </div>

                <div class="row">
                    <div class="form-group col-md-6">
                        User Type
                        <asp:DropDownList ID="ddl_Status" runat="server" CssClass="form-control" Enabled="true">
                            <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Admin" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Customer" Value="2"></asp:ListItem>
                            <asp:ListItem Text="Executive" Value="3"></asp:ListItem>
                        </asp:DropDownList>

                    </div>
                </div>

                <div class="row">
                    <div class="form-group col-md-6">
                        <asp:Button ID="btn_save" Text="Save" runat="server" CssClass="btn btn-primary" OnClick="btn_save_Click" ValidationGroup="ursergrp"></asp:Button>
                    </div>
                </div>

                <div class="row">
                    <div class="form-group col-md-12">
                        <asp:GridView ID="gv_CreateUser" runat="server" AutoGenerateColumns="False" OnRowCommand="gv_CreateUser_RowCommand"
                            BackColor="White" BorderColor="#EEF2F5" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" CssClass="gridSize" ShowFooter="false" AllowPaging="True"
                            GridLines="None" Font-Bold="False" FooterStyle-Wrap="True"
                            PagerSettings-FirstPageText="First" PagerSettings-LastPageText="Last" PagerSettings-PageButtonCount="4"
                            PagerStyle-VerticalAlign="Bottom" PagerStyle-HorizontalAlign="Center" PageSize="5" HorizontalAlign="Left"
                            RowStyle-HorizontalAlign="Center" RowStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Height="50px">
                            <Columns>


                                <asp:TemplateField HeaderText="User Id">
                                    <ItemTemplate>
                                        <%#Eval("UserId") %>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="gridheader"></HeaderStyle>
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="User Name">
                                    <ItemTemplate>
                                        <%#Eval("UserName") %>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="gridHeaderVirticalline"></HeaderStyle>

                                    <ItemStyle CssClass="gridvirticalline"></ItemStyle>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Password">
                                    <ItemTemplate>
                                        <%#Eval("Password") %>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="gridHeaderVirticalline"></HeaderStyle>

                                    <ItemStyle CssClass="gridvirticalline"></ItemStyle>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Email Id">
                                    <ItemTemplate>
                                        <%#Eval("EmailId") %>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="gridHeaderVirticalline"></HeaderStyle>

                                    <ItemStyle CssClass="gridvirticalline"></ItemStyle>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Mobile No">
                                    <ItemTemplate>
                                        <%#Eval("MobileNo") %>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="gridHeaderVirticalline"></HeaderStyle>

                                    <ItemStyle CssClass="gridvirticalline"></ItemStyle>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="User Type">
                                    <ItemTemplate>
                                        <%#Eval("UserType") %>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="gridHeaderVirticalline"></HeaderStyle>

                                    <ItemStyle CssClass="gridvirticalline"></ItemStyle>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Remarks">
                                    <ItemTemplate>
                                        <%#Eval("Remarks") %>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="gridHeaderVirticalline"></HeaderStyle>

                                    <ItemStyle CssClass="gridvirticalline"></ItemStyle>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Edit">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnedit" ImageUrl="/assests/images/Edit.jpg" Height="20px" Width="20px" runat="server" Text="edit" ForeColor="black" CommandName="abc1" CommandArgument='<%#Eval("Id") %>' />
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="gridHeaderVirticalline"></HeaderStyle>

                                    <ItemStyle CssClass="gridvirticalline"></ItemStyle>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText=" Delete">
                                    <ItemTemplate>

                                        <asp:ImageButton ID="btndelete" ImageUrl="/assests/images/Delete.jpg" Height="20px" Width="20px" runat="server" ForeColor="black" CommandName="abc" CommandArgument='<%#Eval("Id") %>' />
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="gridHeaderVirticalline"></HeaderStyle>

                                    <ItemStyle CssClass="gridvirticalline"></ItemStyle>
                                </asp:TemplateField>

                            </Columns>

                            <FooterStyle BackColor="#99CCCC" ForeColor="#003399" CssClass="gridfooter" BorderColor="Aqua" BorderStyle="Solid" BorderWidth="3px" />
                            <HeaderStyle Font-Bold="True" ForeColor="#003399" />
                            <PagerSettings Mode="NumericFirstLast" />
                            <PagerStyle BackColor="White" ForeColor="#003399" HorizontalAlign="Center" VerticalAlign="Bottom" CssClass="gridfooter" />
                            <RowStyle BackColor="White" ForeColor="#003399" BorderStyle="Solid" BorderWidth="3px" CssClass="gridrowstyle" />
                            <SelectedRowStyle BackColor="#ABC8F3" Font-Bold="True" ForeColor="#CCFF99" />
                            <SortedAscendingCellStyle BackColor="#EDF6F6" />
                            <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                            <SortedDescendingCellStyle BackColor="#D6DFDF" />
                            <SortedDescendingHeaderStyle BackColor="#002876" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>

    </div>
</asp:Content>

