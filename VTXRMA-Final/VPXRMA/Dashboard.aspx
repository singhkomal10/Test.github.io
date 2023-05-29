<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="VPXRMA.Dashboard" %>




<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"> 
    <meta http-equiv="refresh" content="30" />
    <script src="https://code.highcharts.com/highcharts.js"></script>
    <script src="https://code.highcharts.com/modules/exporting.js"></script>
    <script src="https://code.highcharts.com/modules/export-data.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script>       
        $window.setInterval(function () {
            var dt = new Date();
            currentHours = dt.getHours();
            currentHours = ("0" + currentHours).slice(-2);
            currentMinuts = dt.getMinutes();
            currentMinuts = ("0" + currentMinuts).slice(-2);
            currentSeconds = dt.getSeconds();
            currentSeconds = ("0" + currentSeconds).slice(-2);
            //var time = dt.getHours() + ":" + dt.getMinutes() + ":" + dt.getSeconds();
            var time = currentHours + ":" + currentMinuts + ":" + currentSeconds;
            document.getElementById("time").innerHTML = dt.toLocaleDateString() + " , " + time;
        }, 1000);

        function fetchData(v) {



            PageMethods.fetchPopuData(v, onSuccess, onFailure);

            function onSuccess(val) {
                debugger;
                //alert(val);
                $("#divDetails").html(val);

                $('#myModal').modal('show');
            }

            function onFailure(val) {
                alert(val);
            }
        }

    </script>       
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" EnablePageMethods="true" runat="server"></asp:ScriptManager>
    <div class="dashboard-wrapper">
        <div class="dashboard-ecommerce">
            <div class="container-fluid dashboard-content">
                <div class="row">
                    <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12 col-12">
                        <div class="page-header">
                            <h2 class="pageheader-title"> Dashboard
                             <span class="fa-pull-right" id="time" style="font-size:18px;"></span></h2> 
                            <div class="page-breadcrumb">
                                <nav aria-label="breadcrumb">
                                    <ol class="breadcrumb">
                                        <li class="breadcrumb-item"><a href="#" class="breadcrumb-link">Dashboard</a></li>
                                        <li class="breadcrumb-item active" aria-current="page">Main Dashboard</li>
                                    </ol>
                                </nav>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="ecommerce-widget">
                     <h3 class="card-header" style="background-color:#ffcccc">Dashboard</h3>   
                    <div class="row">
                        <div class="col-xl-3 col-lg-6 col-md-6 col-sm-12 col-12">
                            <div class="card">
                                <div class="card-body">
                                    <h5 class="text-muted">Pending :</h5>
                                    <div class="metric-value d-inline-block">
                                        <h1 class="mb-1 text-primary" id="lblpending" runat="server" onclick="fetchData(1)"></h1>
                                            <%--<asp:LinkButton ID="lbtotalPending" runat="server" OnClick="lbtotalPending_Click"></asp:LinkButton></h1>--%>
                                    </div>
                                    <%--<div class="metric-label d-inline-block float-right text-success">
                                        <i class="fa fa-fw fa-caret-up"></i><span></span>
                                    </div>--%>
                                </div>
                                <div id="sparkline-1"></div>
                            </div>
                        </div>

                        <div class="col-xl-3 col-lg-6 col-md-6 col-sm-12 col-12">
                            <div class="card">
                                <div class="card-body">
                                    <h5 class="text-muted">Approved :</h5>
                                    <div class="metric-value d-inline-block">
                                        <h1 class="mb-1 text-primary" id="lblapproved" runat="server" onclick="fetchData(2)"></h1>
                                            <%--<asp:LinkButton ID="lbtotalApproved" runat="server" OnClick="lbtotalApproved_Click"></asp:LinkButton></h1>--%>
                                    </div>
                                    <div class="metric-label d-inline-block float-right text-danger">
                                        <span id="toPlan" runat="server"></span>
                                    </div>
                                </div>                                
                            </div>
                        </div>

                        <div class="col-xl-3 col-lg-6 col-md-6 col-sm-12 col-12">
                            <div class="card">
                                <div class="card-body">
                                    <h5 class="text-muted">Not Approved :</h5>
                                    <div class="metric-value d-inline-block">
                                        <h1 class="mb-1 text-primary" id="lblNotApproved" runat="server" onclick="fetchData(3)"></h1>
                                            <%--<asp:LinkButton ID="lbtotalNotApproved" runat="server" OnClick="lbtotalNotApproved_Click"></asp:LinkButton></h1>--%>
                                    </div>
                                    <div class="metric-label d-inline-block float-right text-danger">
                                        <span id="toProd" runat="server"></span>
                                    </div>
                                </div>
                                <div id="sparkline-3">
                                </div>
                            </div>
                        </div>

                        <div class="col-xl-3 col-lg-6 col-md-6 col-sm-12 col-12">
                            <div class="card">
                                <div class="card-body">
                                    <h5 class="text-muted">On Hold :</h5>
                                    <div class="metric-value d-inline-block">
                                        <h1 class="mb-1 text-primary" id="lblOnHold" runat="server" onclick="fetchData(4)"></h1>
                                            <%--<asp:LinkButton ID="lbtotalOnHold" runat="server" OnClick="lbtotalOnHold_Click"></asp:LinkButton></h1>--%>
                                    </div>
                                    <div class="metric-label d-inline-block float-right text-success">
                                        <i class="fa fa-fw fa-caret-up" id="statusIcone" runat="server"></i><span id="statusP" runat="server"></span>
                                    </div>
                                </div>
                                <div id="sparkline-4"></div>
                            </div>
                        </div>
                    </div>
                

                    <div class="row">
                        <div class="col-xs-12 col-sm-12 col-md-6 col-lg-6">                            
                            <div id="container1"></div>   
                        </div>
                        <div class="col-xs-12 col-sm-12 col-md-6 col-lg-6">
                             <div id="container2"></div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
     <div id="myModal" class="modal fade" role="dialog">
        <div class="modal-dialog modal-lg">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title">View Data</h4>
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                </div>
                <div class="modal-body">
                    <div id="divDetails" style="overflow-x: scroll;"></div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
                </div>
            </div>

        </div>
    </div>
</asp:Content>
