<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="RMASearch.aspx.cs" Inherits="VPXRMA.RMASearch" EnableEventValidation="false"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .gridSize {
            max-width: 100%;
            width: 95%;
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
            $("[id*=grd_Search] td").hover(function () {
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
                <h4 class="auto-style1">RMA Search :</h4>
                <hr style="border-bottom: 1px thin; border-bottom-color: crimson; width: 100%; height: 5px" />
                <div class="MsgStyle">
                    <asp:Label ID="lblMsg" runat="server" ForeColor="Red" Text="" Visible="false" CssClass="MsgStyle"></asp:Label>
                </div>
                <div class="row">
                    <div class="form-group col-md-6">
                        <asp:Label ID="lblStatus" runat="server" Text="Status"></asp:Label>
                        <asp:DropDownList ID="ddlStatus" CssClass="form-control" runat="server"></asp:DropDownList>
                    </div>

                    <div class="form-group col-md-6">
                        <asp:Label ID="lblSrNo" runat="server" Text="Sr. No"></asp:Label>
                        <asp:TextBox ID="txtSrNo" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="form-group col-md-6">
                        <asp:Label ID="Label2" runat="server" Text="Model"></asp:Label>
                        <asp:TextBox ID="txtmodel" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>

                    <div class="form-group col-md-6">
                        <asp:Label ID="Label3" runat="server" Text="Entry Date"></asp:Label>
                        <asp:TextBox ID="txtentrydate" CssClass="form-control" runat="server" TextMode="Date"></asp:TextBox>
                    </div>
                </div>


                <div class="row">
                    <div class="form-group col-md-4">
                        <asp:Label ID="Label1" runat="server" Visible="false"></asp:Label>
                        <asp:Button ID="btn_Search" Text="Search" runat="server" CssClass="btn btn-primary" OnClick="btn_Search_Click"></asp:Button>

                    </div>
                </div>

                <div class="row">
                    <div class="form-group col-md-4">
                        <asp:Label ID="lblexport" runat="server" Visible="false"></asp:Label>
                        <asp:Button ID="txtexport" Text="Export TO Excel" runat="server" CssClass="btn btn-primary" OnClick="txtexport_Click"></asp:Button>

                    </div>
                </div>





                <asp:Panel runat="server" ID="Panelallchecked" Visible="false">
                    <div class="form-row">
                        <div class="form-group col-md-6">

                            <asp:Label ID="Label4" runat="server" Text="Status"></asp:Label>
                            <%--<asp:TextBox ID="txt_Status" CssClass="form-control" runat="server"></asp:TextBox>--%>
                            <asp:DropDownList ID="ddl_Status" CssClass="form-control" runat="server"></asp:DropDownList>
                        </div>

                        <div class="form-group col-md-6">
                            <asp:Label ID="Label5" runat="server" Text="Remarks"></asp:Label>
                            <asp:TextBox ID="txt_Remarks" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>

                    </div>
                    <div class="row">
                        <div class="form-group col-md-4">
                            <asp:Label ID="Label6" runat="server" Visible="false"></asp:Label>
                            <asp:Button ID="btnSave1" Text="Save" runat="server" CssClass="btn btn-primary" OnClick="btnSave1_Click"></asp:Button>

                        </div>
                    </div>

                </asp:Panel>






                <div class="row">
                    <div class="form-group col-md-4">
                        <asp:Label ID="lblallsearch" runat="server" Visible="false"></asp:Label>
                        <asp:Button ID="btn_Allcheck" Text="Check All" runat="server" CssClass="btn btn-primary" OnClick="btn_Allcheck_Click" Visible="false"></asp:Button>

                    </div>
                </div>


                <div class="row">
                    <div class="form-group col-md-12">
                        <asp:GridView ID="grd_Search" runat="server" AutoGenerateColumns="False" OnRowCommand="grd_Search_RowCommand"
                            OnPageIndexChanging="grd_Search_PageIndexChanging"
                            BackColor="White" BorderColor="#EEF2F5" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" CssClass="gridSize" ShowFooter="false" AllowPaging="True"
                            GridLines="None" Font-Bold="False" FooterStyle-Wrap="True"
                            PagerSettings-FirstPageText="First" PagerSettings-LastPageText="Last" PagerSettings-PageButtonCount="4"
                            PagerStyle-VerticalAlign="Bottom" PagerStyle-HorizontalAlign="Center" PageSize="20" HorizontalAlign="Left"
                            RowStyle-HorizontalAlign="Center" RowStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Height="50px">
                            <Columns>

                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="SelectAllCheckBox" runat="server" OnClientClick="return SelectAllCheckboxes(this);" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="MyCheckBox" runat="server" />
                                    </ItemTemplate>
                                     <HeaderStyle CssClass="gridheader"></HeaderStyle>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="ID">
                                    <ItemTemplate>
                                        <%#Eval("Id") %>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="gridheader"></HeaderStyle>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Brand">
                                    <ItemTemplate>
                                        <%#Eval("Brand") %>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="gridHeaderVirticalline"></HeaderStyle>

                                    <ItemStyle CssClass="gridvirticalline"></ItemStyle>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Model">
                                    <ItemTemplate>
                                        <%#Eval("Model") %>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="gridHeaderVirticalline"></HeaderStyle>

                                    <ItemStyle CssClass="gridvirticalline"></ItemStyle>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Sr. No">
                                    <ItemTemplate>
                                        <%#Eval("SrNo") %>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="gridHeaderVirticalline"></HeaderStyle>

                                    <ItemStyle CssClass="gridvirticalline"></ItemStyle>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="OC. No">
                                    <ItemTemplate>
                                        <%#Eval("OcNo") %>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="gridHeaderVirticalline"></HeaderStyle>

                                    <ItemStyle CssClass="gridvirticalline"></ItemStyle>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Panel Sr.No">
                                    <ItemTemplate>
                                        <%#Eval("PanelSrNo") %>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="gridHeaderVirticalline"></HeaderStyle>

                                    <ItemStyle CssClass="gridvirticalline"></ItemStyle>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Mfg. Date">
                                    <ItemTemplate>
                                        <%#Eval("MfgDate") %>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="gridHeaderVirticalline"></HeaderStyle>

                                    <ItemStyle CssClass="gridvirticalline"></ItemStyle>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Entry Date">
                                    <ItemTemplate>
                                        <%#Eval("EntryDate") %>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="gridHeaderVirticalline"></HeaderStyle>

                                    <ItemStyle CssClass="gridvirticalline"></ItemStyle>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Warranty">
                                    <ItemTemplate>
                                        <%#Eval("Warranty") %>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="gridHeaderVirticalline"></HeaderStyle>

                                    <ItemStyle CssClass="gridvirticalline"></ItemStyle>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Problem">
                                    <ItemTemplate>
                                        <%#Eval("Problem") %>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="gridHeaderVirticalline"></HeaderStyle>

                                    <ItemStyle CssClass="gridvirticalline"></ItemStyle>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Status">
                                    <ItemTemplate>
                                        <%#Eval("status") %>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="gridHeaderVirticalline"></HeaderStyle>

                                    <ItemStyle CssClass="gridvirticalline"></ItemStyle>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Front Image" HeaderStyle-Width="200px">
                                    <ItemTemplate>
                                        <asp:Image ID="Image1" runat="server" ImageUrl='<%# Eval("Pf_Image") %>' Height="80px" Width="100px" />
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="gridHeaderVirticalline"></HeaderStyle>

                                    <ItemStyle CssClass="gridvirticalline"></ItemStyle>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Back Image" HeaderStyle-Width="200px">
                                    <ItemTemplate>
                                        <asp:Image ID="Image2" runat="server" ImageUrl='<%# Eval("Pb_Image") %>' Height="80px" Width="100px" />
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="gridHeaderVirticalline"></HeaderStyle>

                                    <ItemStyle CssClass="gridvirticalline"></ItemStyle>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Defect Image" HeaderStyle-Width="200px">
                                    <ItemTemplate>
                                        <asp:Image ID="Image3" runat="server" ImageUrl='<%# Eval("Def_Image") %>' Height="80px" Width="100px" />
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="gridHeaderVirticalline"></HeaderStyle>

                                    <ItemStyle CssClass="gridvirticalline"></ItemStyle>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="TVSrNo_Image" HeaderStyle-Width="200px">
                                    <ItemTemplate>
                                        <asp:Image ID="Image4" runat="server" ImageUrl='<%# Eval("TVSrNo_Image") %>' Height="80px" Width="100px" />
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="gridHeaderVirticalline"></HeaderStyle>

                                    <ItemStyle CssClass="gridvirticalline"></ItemStyle>
                                </asp:TemplateField>



                                <%--                        <asp:BoundField DataField="Text" HeaderText="Name" />  
            <asp:ImageField DataImageUrlField="Value" ControlStyle-Height="75" ControlStyle-Width="75" HeaderText="Images" />  --%>


                                <%-- <asp:TemplateField HeaderText="Image" HeaderStyle-Width="200px">
                        <ItemTemplate>
                            <asp:Image ID="Image1" runat="server" ImageUrl='<%# Eval("Image") %>'
                                Height="80px" Width="100px" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:Image ID="img_user" runat="server" ImageUrl='<%# Eval("Image") %>'
                                Height="80px" Width="100px" /><br />
                            <asp:FileUpload ID="FileUpload1" runat="server" />
                        </EditItemTemplate>
                    </asp:TemplateField>--%>


                                <%--                             <asp:TemplateField HeaderText="Panel Front Image">
                                <ItemTemplate>
                                    <%#Eval("Mf Date") %>
                                </ItemTemplate>
                            </asp:TemplateField>

                               <asp:TemplateField HeaderText="Panel Back Image">
                                <ItemTemplate>
                                    <%#Eval("Mf Date") %>
                                </ItemTemplate>
                            </asp:TemplateField>

                               <asp:TemplateField HeaderText="Defect Image">
                                <ItemTemplate>
                                    <%#Eval("Mf Date") %>
                                </ItemTemplate>
                            </asp:TemplateField>

                               <asp:TemplateField HeaderText="TV SrNo Image">
                                <ItemTemplate>
                                    <%#Eval("Mf Date") %>
                                </ItemTemplate>
                            </asp:TemplateField>--%>



                                <asp:TemplateField HeaderText=" Action">
                                    <ItemTemplate>

                                        <asp:ImageButton ID="ImageButton1" ImageUrl="assests/images/edit.jpg" Height="30px" Width="30px" runat="server" Text="edit" ForeColor="black" CommandName="abc1" CommandArgument='<%#Eval("Id") %>' />
                                        <%-- <asp:ImageButton ID="btndelet"  ImageUrl="assests/images/Actions-edit-delete-icon.png" Height="30px" Width="30px" runat="server"   ForeColor="black" CommandName="abc"  CommandArgument='<%#Eval("Id") %>' />--%>
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
        
