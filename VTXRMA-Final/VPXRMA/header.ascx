<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="header.ascx.cs" Inherits="VPXRMA.header" %>


<nav class="navbar navbar-expand-lg bg-white fixed-top">

    <a class="nav-link"><img src="assests/images/Logo.png" style="width: 120px;
    height: 40px" />-  </a>
   <%-- <h2><b>VTXRMA</b> </h2>--%>
    <button class="navbar-toggler" type="button" data-toggle="collapse"
        data-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
        <span class="navbar-toggler-icon"></span>
    </button>
    <div class="collapse navbar-collapse " id="navbarSupportedContent">
        <ul class="navbar-nav ml-auto navbar-right-top">
            <li class="nav-item dropdown nav-user">
                <a class="nav-link nav-user-img" href="#" id="navbarDropdownMenuLink2" data-toggle="dropdown"
                    aria-haspopup="true" aria-expanded="false">
                    <asp:Label ID="lblUserName" runat="server"></asp:Label>
                    <i class="fa fa-user"></i>
                </a>
                <div class="dropdown-menu dropdown-menu-right nav-user-dropdown" aria-labelledby="navbarDropdownMenuLink2">
                    <div class="nav-user-info">

                        <a class="dropdown-item" href="/Login.aspx"><i class="fas fa-power-off mr-2"></i>Logout</a>
                    </div>
                </div>
            </li>
        </ul>
    </div>
</nav>

<div class="nav-left-sidebar sidebar-dark">
    <div class="menu-list">
        <nav class="navbar navbar-expand-lg navbar-light">
            <a class="d-xl-none d-lg-none" href="#"></a>
            <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
                <span class="navbar-toggler-icon"></span>
            </button>
            <div class="collapse navbar-collapse" id="navbarNav">
                <ul class="navbar-nav flex-column mynav">
                    <li class="nav-item">
                        <a class="nav-link" href="#"></a>
                    </li>

                    <li class="nav-item">
                        <a class="nav-link" href="Dashboard.aspx"><i class="fas fa-fw fa-chart-pie"></i>Dashboard</a>
                    </li>

                     <% if(Session["Role"] != null && Session["Role"].ToString() == "Admin"){ %>
                    
                     

                    <li class="nav-item">
                        <a class="nav-link" href="#" data-toggle="collapse" aria-expanded="false" data-target="#submenu-2"
                            aria-controls="submenu-2"><i class="fa fa-fw fa-rocket"></i>Master</a>
                        <div id="submenu-2" class="collapse submenu" style="">
                            <ul class="nav flex-column">


                                <li class="nav-item">
                                    <a class="nav-link" href="/User.aspx">User</a>
                                </li>






                            </ul>
                        </div>
                    </li>
                     <%} %>

                    <li class="nav-item">
                        <a class="nav-link" href="/RMA_.aspx"><i class="fas fa-fw fa-chart-pie"></i>New RMA</a>

                    </li>

                    <li class="nav-item">
                        <a class="nav-link" href="/RMASearch.aspx"><i class="fas fa-fw fa-chart-pie"></i>Search Page</a>
                    </li>


                    <li class="nav-item">
                        <a class="nav-link" href="/login.aspx"><i class="fas fa-fw fa-chart-pie"></i>Logout</a>
                    </li>
                </ul>
            </div>
        </nav>
    </div>
</div>

