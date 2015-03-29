<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Chat.aspx.cs" Inherits="PriceList.Chat" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%:Styles.Render("~/Content/chat") %>
    <div id="chatmain" class="container">
        <div>&nbsp;</div>
        <div class="col-md-10">
            <div id="chatpanel">
                <div class="row">
                    <div class="col-md-12">
                        <div class="panel panel-primary">
                            <div class="panel-heading" id="accordion">
                                <span class="glyphicon glyphicon-comment"></span>Chat
                                <div class="btn-group pull-right">
                                    <a type="button" class="btn btn-default btn-xs" data-toggle="collapse" data-parent="#accordion" href="#collapseOne">
                                        <span class="glyphicon glyphicon-chevron-down"></span>
                                    </a>
                                </div>
                            </div>
                            <div class="panel-collapse expand" id="collapseOne">
                                <div class="panel-body">
                                    <ul id="chatlist" class="chat">
                                    </ul>
                                </div>
                                <div class="panel-footer">
                                    <div class="input-group">                                        
                                        <select id="txt-message" style="max-width: 650px;" type="text" class="form-control input-sm">

                                        </select>
                                        <span class="input-group-btn">
                                            <input class="btn btn-warning btn-sm" type="button" value="Send" id="btn-send" />
                                        </span>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>


        </div>
        <div class="col-md-2">
            List of users:
            <ul id="onlineUsers"></ul>
        </div>
        <script id="message-template" type="text/x-handlebars-template">
            <li class="message left clearfix" data-message-id="{{ID}}">
                <div class="row">
                    <div class="col-md-10">
                        <span class="chat-img pull-left">
                            <img src="http://placehold.it/50/55C1E7/fff&text=U" alt="User Avatar" class="img-circle" /></span>
                        <div class="chat-body clearfix">
                            <div class="header">
                                <strong class="primary-font">{{User.FirstName}}</strong>
                                <small class="pull-right text-muted"><span class="glyphicon glyphicon-time"></span>
                                    <span class="timeago">{{timeago PostDate}}</span></small>
                            </div>
                            <div>{{AskedDevice.Fullname}} </div>
                            <div class="pull-right">
                                <a href="#" class="reply pull-left" data-message-id='{{ID}}'>
                                    <span class="glyphicon glyphicon-plus-sign" aria-hidden="true"></span>reply</a>
                                <div data-message="" class="amount hidden pull-right col-sm-3 input-group input-group-sm pull-right">
                                    <span class="input-group-addon" id="sizing-addon3">AED</span>
                                    <input type="text" class="form-control amount-value" placeholder="0.00" aria-describedby="sizing-addon3">
                                    <span class="input-group-btn">
                                        <button class="gobtn btn btn-default" type="button">Go!</button></span>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-2">
                        <div class="reply-box">
                            <span>Replies:</span>
                            <ul>
                                {{#each Replies}}
                                    <li>{{Amount}} AED</li>
                                {{/each}}
                            </ul>
                        </div>
                    </div>
                </div>
            </li>
        </script>

        <script src="Scripts/jquery.signalR-2.1.2.js"></script>
        <script src="signalr/hubs"></script>
        <script src="Scripts/swag.min.js"></script>
        <script>Swag.registerHelpers(Handlebars);</script>
        <script src="//cdnjs.cloudflare.com/ajax/libs/select2/4.0.0-rc.2/js/select2.js"></script>
        <script src="Scripts/Chat.js"></script>



    </div>
</asp:Content>
