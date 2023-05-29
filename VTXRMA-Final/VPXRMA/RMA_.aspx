<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="RMA_.aspx.cs" Inherits="VPXRMA.RMARequest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="assets/css/gridStyle.css" rel="stylesheet" />
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

         <%--function to auto hide msg label--%>
        function HideLabel() {
            var seconds = 5;
            setTimeout(function () {
                document.getElementById("<%=lblMsg.ClientID %>").style.display = "none";
            }, seconds * 1000);
        };

        <%--function to auto hide Save msg label--%>
        function HideLabel5() {
            var seconds = 5;
            setTimeout(function () {
             <%--   document.getElementById("<%=LableSave.ClientID %>").style.display = "none";--%>
            }, seconds * 1000);
        };
        <%--function to auto hide Upload msg label--%>
        function HideLabel1() {
            var seconds = 5;
            setTimeout(function () {
                document.getElementById("<%=LabelMessage1.ClientID %>").style.display = "none";
            }, seconds * 1000);
        };
        <%--function to auto hide Upload msg label--%>
        function HideLabel2() {
            var seconds = 5;
            setTimeout(function () {
                document.getElementById("<%=LabelMessage2.ClientID %>").style.display = "none";
            }, seconds * 1000);
        };
        <%--function to auto hide Upload msg label--%>
        function HideLabel3() {
            var seconds = 5;
            setTimeout(function () {
                document.getElementById("<%=LabelMessage3.ClientID %>").style.display = "none";
            }, seconds * 1000);
        };
        <%--function to auto hide Upload msg label--%>
        function HideLabel4() {
            var seconds = 5;
            setTimeout(function () {
                document.getElementById("<%=LabelMessage4.ClientID %>").style.display = "none";
            }, seconds * 1000);
        };
        <%--function to show mouse hove on grid--%>
        $(function () {
            $("[id*=grd_RMA] td").hover(function () {
                $("td", $(this).closest("tr")).addClass("hover_row");
            }, function () {
                $("td", $(this).closest("tr")).removeClass("hover_row");
            });
        });

        function ShowImagePreview(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('#<%=ImgPrv.ClientID%>').prop('src', e.target.result)
                        .width(240)
                        .height(150);
                };
                reader.readAsDataURL(input.files[0]);
            }
        }

        function ShowImagePreview2(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('#<%=ImgPrv2.ClientID%>').prop('src', e.target.result)
                        .width(240)
                        .height(150);
                };
                reader.readAsDataURL(input.files[0]);
            }
        }

        function ShowImagePreview3(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('#<%=ImgPrv3.ClientID%>').prop('src', e.target.result)
                        .width(240)
                        .height(150);
                };
                reader.readAsDataURL(input.files[0]);
            }
        }

        function ShowImagePreview4(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('#<%=ImgPrv4.ClientID%>').prop('src', e.target.result)
                        .width(240)
                        .height(150);
                };
                reader.readAsDataURL(input.files[0]);
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="dashboard-wrapper">
        <div class="dashboard-ecommerce">
            <div class="container-fluid dashboard-content">
                <h4 class="auto-style1">RMA Details:</h4>
                <hr style="border-bottom: 1px thin; border-bottom-color: crimson; width: 100%; height: 5px" />
                <div class="MsgStyle">
                    <asp:Label ID="lblMsg" runat="server" ForeColor="Red" Text="" Visible="false" CssClass="MsgStyle"></asp:Label>
                </div>
                <asp:Panel ID="Panel1" runat="server">

                    <div class="row">
                        <div class="form-group col-md-4">
                            <asp:Label ID="lbl_SrNO" runat="server" Text="Sr. No"></asp:Label>
                            <asp:TextBox ID="txt_SrNO" CssClass="form-control" runat="server" autocomplete="off"
                                OnTextChanged="txtSr_NO_TextChanged" AutoPostBack="True" ValidationGroup="srn"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvSrn" runat="server" ControlToValidate="txt_SrNO"
                                ErrorMessage="* Serial No. Required" ValidationGroup="srn" ForeColor="Red"></asp:RequiredFieldValidator>
                        </div>
                         <div class="form-group col-md-4">
                            <asp:Label ID="lbljobid" runat="server" Text="Job Id"></asp:Label>
                            <asp:TextBox ID="txtjobid" CssClass="form-control" runat="server" ></asp:TextBox>
                        </div>
                    </div>




                    <%--  false
                    <div class="row">
                        <div class="form-group col-md-12">
                            <asp:Button ID="btnNext" CssClass="form-control btn" runat="server" Visible="true" Text="Proceed" OnClick="btnNext_Click" BackColor="#0066CC" />
                        </div>


                    </div>--%>
                </asp:Panel>





                <asp:Panel ID="pnlBrand" CssClass="form-control" runat="server" Visible="True">
                    <div class="row">
                        <div class="form-group col-md-4">
                            <asp:Label ID="lbl_Brand" Text="Brand" runat="server"></asp:Label>
                            <%-- false --%>
                            <asp:TextBox ID="txtBrand" CssClass="form-control" runat="server" Enabled="true" ValidationGroup="srn"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvBrand" runat="server" ErrorMessage="* Brand Name Required" ForeColor="Red"
                                ControlToValidate="txtBrand" ValidationGroup="srn"></asp:RequiredFieldValidator>

                        </div>

                        <div class="form-group col-md-4">
                            <asp:Label ID="lbl_model" Text="Model" runat="server"></asp:Label>
                            <asp:TextBox ID="txtmodel" CssClass="form-control" runat="server" ValidationGroup="srn"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvModel" runat="server" ErrorMessage="* Model Name Required" ForeColor="Red"
                                ControlToValidate="txtmodel" ValidationGroup="srn"></asp:RequiredFieldValidator>

                        </div>

                        <div class="form-group col-md-4">
                            <asp:Label ID="lbl_PanelSr_No" Text="Panel Sr. No" runat="server"></asp:Label>
                            <asp:TextBox ID="txt_PanelSr_No" CssClass="form-control" runat="server"></asp:TextBox>

                        </div>
                    </div>


                    <div class="row">

                        <div class="form-group col-md-4">
                            <asp:Label ID="lbl_OC_No" Text="OC No" runat="server"></asp:Label>
                            <asp:TextBox ID="txtOC_No" CssClass="form-control" runat="server"></asp:TextBox>

                        </div>




                        <div class="form-group col-md-4">
                            <asp:Label ID="lbl_Board_Sr_No" Text="Board Sr. No" runat="server"></asp:Label>
                            <asp:TextBox ID="txt_Board_Sr_No" CssClass="form-control" runat="server"></asp:TextBox>

                        </div>


                        <div class="form-group col-md-4">
                            <asp:Label ID="lbl_Mf_Date" Text="Mfg. Date" runat="server"></asp:Label>
                            <asp:TextBox ID="txt_Mf_Date" CssClass="form-control" runat="server" TextMode="date" AutoPostBack="True"></asp:TextBox>

                        </div>
                    </div>
                </asp:Panel>
                <div class="row">
                    <div class="form-group col-md-12">
                    </div>

                </div>
                <asp:Panel runat="server" ID="PanelWarranty" Visible="true">
                    <div class="form-row">
                        <div class="form-group col-md-4">

                            <asp:Label ID="lbl_Warranty" runat="server" Text="Warranty "></asp:Label>
                            <asp:CheckBox ID="checkbox3" CssClass="form-control" runat="server" Text=" " OnCheckedChanged="checkbox3_CheckedChanged" AutoPostBack="true" />
                            &nbsp;&nbsp;
                    
                            <%--<asp:CheckBox ID="checkbox4" CssClass="form-control" Text="No" runat="server" OnCheckedChanged="checkbox4_CheckedChanged" Visible="false" />--%>
                            <asp:Label ID="lblwarrantycheck" runat="server" Text=" No" Visible="false"></asp:Label>

                        </div>


                        <div class="form-group col-md-4">
                            <asp:Label ID="lbl_Problem" runat="server" Text=" Defect Symptom"></asp:Label>

                            <asp:TextBox ID="txt_Problem" CssClass="form-control" runat="server" TextMode="MultiLine" Height="40px"></asp:TextBox>
                        </div>
                    </div>
                </asp:Panel>








                <div class="row">
                    <div class="form-group col-md-6">
                        <asp:Button ID="btn_save" Text="Save" runat="server" CssClass="btn btn-primary" OnClick="btn_save_Click" ValidationGroup="srn"></asp:Button>
                    </div>

                    <div class="form-group col-md-6 MsgStyle">

                        <asp:Label ID="LableSave" runat="server" ForeColor="Red" Text="" Visible="false" CssClass="MsgStyle"></asp:Label>
                    </div>
                </div>
                <%-- <div class="form-group col-md-6">
                        <asp:Button ID="btn_Edit" Text="Edit" runat="server" CssClass="btn btn-primary" OnClick="btn_Edit_Click"></asp:Button>
                    </div>--%>

                <div class="row">
                    <div class="form-group col-md-12">
                    </div>

                </div>


                <div class="row">
                    <div class="form-group col-md-12">
                        <asp:GridView ID="grd_RMA" runat="server" AutoGenerateColumns="False" OnRowCommand="grd_RMA_RowCommand"
                            OnPageIndexChanging="grd_RMA_PageIndexChanging"
                            BackColor="White" BorderColor="#EEF2F5" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" CssClass="gridSize" ShowFooter="false" AllowPaging="True"
                            GridLines="None" Font-Bold="False" FooterStyle-Wrap="True"
                            PagerSettings-FirstPageText="First" PagerSettings-LastPageText="Last" PagerSettings-PageButtonCount="4"
                            PagerStyle-VerticalAlign="Bottom" PagerStyle-HorizontalAlign="Center" PageSize="10" HorizontalAlign="Left"
                            RowStyle-HorizontalAlign="Center" RowStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Height="50px">
                            <Columns>
                                <asp:TemplateField HeaderText=" ID">
                                    <ItemTemplate>
                                        <%#Eval("id") %>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="gridheader"></HeaderStyle>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText=" Brand" ItemStyle-Wrap="true">
                                    <ItemTemplate>
                                        <%#Eval("Brand") %>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="gridHeaderVirticalline"></HeaderStyle>

                                    <ItemStyle CssClass="gridvirticalline"></ItemStyle>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText=" Model">
                                    <ItemTemplate>
                                        <%#Eval("Model") %>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="gridHeaderVirticalline"></HeaderStyle>

                                    <ItemStyle CssClass="gridvirticalline"></ItemStyle>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText=" Line No" ItemStyle-Wrap="true">
                                    <ItemTemplate>
                                        <%#Eval("Line_No") %>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="gridHeaderVirticalline"></HeaderStyle>

                                    <ItemStyle CssClass="gridvirticalline"></ItemStyle>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText=" Sr. No">
                                    <ItemTemplate>
                                        <%#Eval("Sr_No") %>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="gridHeaderVirticalline"></HeaderStyle>

                                    <ItemStyle CssClass="gridvirticalline"></ItemStyle>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText=" LineIn Date" ItemStyle-Wrap="true">
                                    <ItemTemplate>
                                        <%#Eval("Line_In_Date") %>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="gridHeaderVirticalline"></HeaderStyle>

                                    <ItemStyle CssClass="gridvirticalline"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText=" LineOut Date">
                                    <ItemTemplate>
                                        <%#Eval("Line_Out_Date") %>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="gridHeaderVirticalline"></HeaderStyle>

                                    <ItemStyle CssClass="gridvirticalline"></ItemStyle>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText=" Board Sr.No" ItemStyle-Wrap="true">
                                    <ItemTemplate>
                                        <%#Eval("brdsrno") %>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="gridHeaderVirticalline"></HeaderStyle>

                                    <ItemStyle CssClass="gridvirticalline"></ItemStyle>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="MI_In Date">
                                    <ItemTemplate>
                                        <%#Eval("MI_In_Date") %>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="gridHeaderVirticalline"></HeaderStyle>

                                    <ItemStyle CssClass="gridvirticalline"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="MI_In Date">
                                    <ItemTemplate>
                                        <%#Eval("MI_In_Date") %>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="gridHeaderVirticalline"></HeaderStyle>

                                    <ItemStyle CssClass="gridvirticalline"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="MI_Out Date">
                                    <ItemTemplate>
                                        <%#Eval("MI_Out_Date") %>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="gridHeaderVirticalline"></HeaderStyle>

                                    <ItemStyle CssClass="gridvirticalline"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="SMT_Out Date">
                                    <ItemTemplate>
                                        <%#Eval("SMT_Out_Date") %>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="gridHeaderVirticalline"></HeaderStyle>

                                    <ItemStyle CssClass="gridvirticalline"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText=" Panel Sr.No">
                                    <ItemTemplate>
                                        <%#Eval("pnlsrno") %>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="gridHeaderVirticalline"></HeaderStyle>

                                    <ItemStyle CssClass="gridvirticalline"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText=" User Date">
                                    <ItemTemplate>
                                        <%#Eval("UserDate") %>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="gridHeaderVirticalline"></HeaderStyle>

                                    <ItemStyle CssClass="gridvirticalline"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText=" MAC Id">
                                    <ItemTemplate>
                                        <%#Eval("Macid") %>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="gridHeaderVirticalline"></HeaderStyle>

                                    <ItemStyle CssClass="gridvirticalline"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText=" Action" ItemStyle-Width="4%" HeaderStyle-Width="4%" Visible="false">
                                    <ItemTemplate>

                                        <asp:ImageButton ID="btn_edit" ImageUrl="~/assests/images/select-button.png" Height="100%" Width="100%" runat="server" CssClass="fadein-img"
                                            Text="Select" ForeColor="black" ToolTip="Select" CommandName="cmd_Select" CommandArgument='<%#Eval("id") %>' ImageAlign="Middle" />

                                        <%--<asp:ImageButton ID="btn_delet" ImageUrl="~/assets/images/Delete_new.png" Height="23px" Width="25px" runat="server" CssClass="fadein-img"
                                            ToolTip="Delete" ForeColor="black" CommandName="cmd_Delete" CommandArgument='<%#Eval("id") %>' ImageAlign="Right" />--%>
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


                <asp:Panel runat="server" ID="btnpanel" Visible="false">
                    <div class="form-row">

                        <div class="form-group col-md-4">
                            <asp:Button runat="server" ID="btneditrecord" Text="Edit Record" CssClass="btn btn-primary" OnClick="btneditrecord_Click1" />&nbsp;
                            <asp:Button runat="server" ID="btnimage" Text="Show Image" CssClass="btn btn-primary" OnClick="btnimage_Click" />&nbsp;
                            <asp:Button runat="server" ID="btnstatus" Text="Show Status" CssClass="btn btn-primary" OnClick="btnstatus_Click" />

                        </div>
                        <div class="form-group col-md-4">
                            


                        </div>
                        <div class="form-group col-md-4">
                            
                        </div>
                    </div>
                </asp:Panel>


                <asp:Panel runat="server" ID="statusPanel" Visible="false">

                    <div class="row">
                        <div class="form-group col-md-4">
                            <asp:Label ID="lbl_Status" runat="server" Text="Status">  </asp:Label>

                            <asp:DropDownList ID="ddl_Status" runat="server" CssClass="form-control" Enabled="true"
                                OnSelectedIndexChanged="ddl_Status_SelectedIndexChanged" AutoPostBack="true">
                                


                            </asp:DropDownList>
                        </div>

                        <div class="form-group col-md-4">
                            <asp:Label ID="Label2" runat="server" Text="Remarks"></asp:Label>

                            <asp:TextBox ID="txtremark" CssClass="form-control" runat="server" TextMode="MultiLine" Height="40px"></asp:TextBox>
                        </div>

                        

                        <div class="form-group col-md-4">
                            <asp:Panel runat="server" ID="Paneldocket" Visible="false">
                                <asp:Label ID="Label1" runat="server" Text="Doket Number"></asp:Label>

                                <asp:TextBox ID="txtdocketnum" CssClass="form-control" runat="server"></asp:TextBox>

                                <asp:RequiredFieldValidator ID="rfvField" runat="server"
                                    ControlToValidate="txtdocketnum"
                                    ErrorMessage="Please enter a value for this field."
                                    InitialValue="">
                                </asp:RequiredFieldValidator>
                            </asp:Panel>
                        </div>
                    </div>
                    <div class="row">
                        <div class="form-group col-md-6">

                            <asp:Button runat="server" ID="btnstatusSave" Text="Save" Visible="true" CssClass="btn btn-primary" OnClick="btnstatusSave_Click" />
                            <div class="form-group col-md-6 MsgStyle">

                                <asp:Label ID="Label3" runat="server" ForeColor="Red" Text="" Visible="false" CssClass="MsgStyle"></asp:Label>

                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="form-group col-md-6">
                             <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnRowCommand="Grid12_RowCommand"
                            OnPageIndexChanging="grd_RMA_PageIndexChanging"
                            BackColor="White" BorderColor="#EEF2F5" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" CssClass="gridSize" ShowFooter="false" AllowPaging="True"
                            GridLines="None" Font-Bold="False" FooterStyle-Wrap="True"
                            PagerSettings-FirstPageText="First" PagerSettings-LastPageText="Last" PagerSettings-PageButtonCount="4"
                            PagerStyle-VerticalAlign="Bottom" PagerStyle-HorizontalAlign="Center" PageSize="10" HorizontalAlign="Left"
                            RowStyle-HorizontalAlign="Center" RowStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Height="50px">

                                <Columns>
                                     <asp:TemplateField HeaderText=" Status">
                                    <ItemTemplate>
                                        <%#Eval("Status") %>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="gridheader"></HeaderStyle>
                                </asp:TemplateField>



                                    <asp:TemplateField HeaderText="Remarks">
                                        <ItemTemplate>
                                            <%#Eval("Remarks") %>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="gridheader"></HeaderStyle>
                                    </asp:TemplateField>

                                     <asp:TemplateField HeaderText="Remarks Date">
                                        <ItemTemplate>
                                            <%#Eval("Remark_Date") %>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="gridheader"></HeaderStyle>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Docket Number">
                                        <ItemTemplate>
                                            <%#Eval("DocketNumber") %>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="gridheader"></HeaderStyle>
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

                </asp:Panel>

                <asp:Panel runat="server" ID="pnlImage" Visible="false">

                    <div class="form-row">
                        <div class="form-group col-md-6">
                            <asp:Label ID="lbl_PanelFrontImage" runat="server" Text="Panel Front Image :"></asp:Label>
                            &nbsp;
                              <asp:Image ID="ImgPrv" Height="150px" Width="240px" runat="server" /><br />
                            <br />
                            <asp:Panel runat="server" ID="pnlFu1" Visible="true">
                                <asp:FileUpload ID="flupImage" runat="server" onchange="ShowImagePreview(this);" />

                                &nbsp;&nbsp;&nbsp;
                            <asp:Button ID="UploadButton1" runat="server" Text="Upload" OnClick="Upload1" ValidationGroup="fu1" />
                                <br />
                                <br />
                                <asp:Label ID="LabelMessage1" runat="server"></asp:Label>
                                <asp:RegularExpressionValidator ID="flupValidator" runat="server" ForeColor="Red" ValidationGroup="fu1"
                                    ControlToValidate="flupImage" ErrorMessage="* Only .jpg, .png, .gif & .jpeg formats are allowed"
                                    ValidationExpression="(.+\.([Jj][Pp][Gg])|.+\.([Pp][Nn][Gg])|.+\.([Gg][Ii][Ff])|.+\.([Jj][Pp][Ee][Gg]))">

                                </asp:RegularExpressionValidator>
                            </asp:Panel>
                        </div>

                        <div class="form-group col-md-6">
                            <asp:Label ID="lbl_PanelBackImage" runat="server" Text="Panel Back Image:    "></asp:Label>
                            &nbsp;&nbsp;&nbsp;
                              <asp:Image ID="ImgPrv2" Height="150px" Width="240px" runat="server" /><br />
                            <br />
                            <asp:Panel runat="server" ID="pnlFu2" Visible="true">
                                <asp:FileUpload ID="flupImage2" runat="server" onchange="ShowImagePreview2(this);" />

                                &nbsp;&nbsp;&nbsp;
                            <asp:Button ID="UploadButton2" runat="server" Text="Upload" OnClick="Upload2" ValidationGroup="fu2" />
                                <br />
                                <br />
                                <asp:Label ID="LabelMessage2" runat="server"></asp:Label>
                                <asp:RegularExpressionValidator ID="flupValidator2" runat="server" ForeColor="Red" ValidationGroup="fu2"
                                    ControlToValidate="flupImage2" ErrorMessage="* Only .jpg, .png, .gif & .jpeg formats are allowed"
                                    ValidationExpression="(.+\.([Jj][Pp][Gg])|.+\.([Pp][Nn][Gg])|.+\.([Gg][Ii][Ff])|.+\.([Jj][Pp][Ee][Gg]))">
                                </asp:RegularExpressionValidator>
                            </asp:Panel>
                        </div>

                        <div class="form-group col-md-6">
                            <asp:Label ID="lbl_FeckImage" runat="server" Text=" Defect Image: "></asp:Label>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                              <asp:Image ID="ImgPrv3" Height="150px" Width="240px" runat="server" /><br />
                            <br />
                            <asp:Panel runat="server" ID="pnlFu3" Visible="true">
                                <asp:FileUpload ID="flupImage3" runat="server" onchange="ShowImagePreview3(this);" />

                                &nbsp;&nbsp;&nbsp;
                            <asp:Button ID="UploadButton3" runat="server" Text="Upload" OnClick="Upload3" ValidationGroup="fu3" /><br />
                                <br />
                                <asp:Label ID="LabelMessage3" runat="server"></asp:Label>
                                <asp:RegularExpressionValidator ID="flupValidator3" runat="server" ForeColor="Red" ValidationGroup="fu3"
                                    ControlToValidate="flupImage3" ErrorMessage="* Only .jpg, .png, .gif & .jpeg formats are allowed"
                                    ValidationExpression="(.+\.([Jj][Pp][Gg])|.+\.([Pp][Nn][Gg])|.+\.([Gg][Ii][Ff])|.+\.([Jj][Pp][Ee][Gg]))">
                                </asp:RegularExpressionValidator>
                            </asp:Panel>
                        </div>



                        <div class="form-group col-md-6">
                            <asp:Label ID="lbl_TV_SrNo_Image" runat="server" Text="TV Sr. No Image:    "></asp:Label>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                              <asp:Image ID="ImgPrv4" Height="150px" Width="240px" runat="server" /><br />
                            <br />
                            <asp:Panel runat="server" ID="pnlFu4" Visible="true">
                                <asp:FileUpload ID="flupImage4" runat="server" onchange="ShowImagePreview4(this);" />

                                &nbsp;&nbsp;&nbsp;
                            <asp:Button ID="UploadButton4" runat="server" Text="Upload" OnClick="Upload4" ValidationGroup="fu4" />
                                <br />
                                <br />
                                <asp:Label ID="LabelMessage4" runat="server"></asp:Label>
                                <asp:RegularExpressionValidator ID="flupValidator4" runat="server" ForeColor="Red" ValidationGroup="fu4"
                                    ControlToValidate="flupImage4" ErrorMessage="* Only .jpg, .png, .gif & .jpeg formats are allowed"
                                    ValidationExpression="(.+\.([Jj][Pp][Gg])|.+\.([Pp][Nn][Gg])|.+\.([Gg][Ii][Ff])|.+\.([Jj][Pp][Ee][Gg]))">
                                </asp:RegularExpressionValidator>
                            </asp:Panel>
                        </div>


                    </div>

                </asp:Panel>

            </div>
        </div>
    </div>


</asp:Content>
