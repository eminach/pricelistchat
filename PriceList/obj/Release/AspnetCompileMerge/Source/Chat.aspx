<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Chat.aspx.cs" Inherits="PriceList.Chat" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%:Styles.Render("~/Content/chat") %>
    <div id="chatmain" class="container">
        <div class="row">
            <div><strong>Username:</strong></div>
        </div>
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
                                        <input id="txt-message" style="max-width: 650px;" type="text" class="form-control input-sm" autocomplete="off" placeholder="Type your message here..." />

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
        <link rel="stylesheet" href="//code.jquery.com/ui/1.11.3/themes/smoothness/jquery-ui.css">
        <script src="signalr/hubs"></script>
        <script src="Scripts/swag.min.js"></script>
        <script src="//code.jquery.com/ui/1.11.3/jquery-ui.js"></script>
        <script>Swag.registerHelpers(Handlebars);</script>
        <script type="text/javascript">
            $(document).on("click", ".reply", function (e) {
                var id = $(this).data("message-id");
                $('li[data-message-id=' + id + '] .amount').removeClass('hidden');
                $('li[data-message-id=' + id + '] .amount').addClass('animated bounceInRight');
            });

            $(function () {

                $('#txt-message').autocomplete({
                    source: function (request, response) {
                        $.ajax({
                            url: '/api/devices/ByKeyword/' + request.term,
                            type: 'GET',
                            cache: false,
                            data: request,
                            dataType: 'json',
                            success: function (json) {
                                // call autocomplete callback method with results  
                                response($.map(json, function (name, val) {
                                    return {
                                        label: name,
                                        value: val
                                    }
                                }));
                            },
                            error: function (XMLHttpRequest, textStatus, errorThrown) {
                                //alert('error - ' + textStatus);
                                console.log('error', textStatus, errorThrown);
                            }
                        });
                    },
                    minLength: 2,
                    select: function (event, ui) {
                        //  alert('you have selected ' + ui.item.label + ' ID: ' + ui.item.value);
                        $('#txt-message').val(ui.item.label);
                        $('#txt-message').data('selected-device', ui.item.value);
                        return false;
                    }
                })
            });
            
                $(function () {
                    function DrawMessage(msg) {
                        var messageTemplate = $("#message-template").html();
                        var template = Handlebars.compile(messageTemplate);
                        var eachMessage = template(msg);
                        $('#chatlist').append($.parseHTML(eachMessage));
                    }
                    // $('.timeago').timeago();
                    //$('#chatlist .timeago').timeago();
                    // Declare a proxy to reference the hub.
                    var chat = $.connection.chatHub;

                    $(document).on('click', "#chatlist li .gobtn", function (e) {
                        var li = $(this).closest("li");
                        var id = li.data("message-id");
                        var amount = li.find(".amount-value").val();
                        chat.server.reply(id, amount);
                    });
                    // Create a function that the hub can call to broadcast messages.
                    chat.client.broadcastMessage = function (msg) {
                        DrawMessage(msg);
                    };
                    chat.client.activeUsersList = function (users) {
                        if (users.length != 0) {
                            $.each(users, function (key, item) {
                                $('#onlineUsers').append('<li>' + item.CompanyName + '</li>');
                            })
                        }
                    };
                    $('#txt-message').focus();
                    // Start the connection.
                    $.connection.hub.start().done(function () {
                        console.log('Now connected, connection ID=' + $.connection.hub.id);
                        $.getJSON('api/Messages', function (data) {
                            $.each(data, function (key, item) {
                                DrawMessage(item);
                            })
                        })
                    });
                    $('#btn-send').click(function () {
                        // Call the Send method on the hub. 
                        chat.server.send($('#txt-message').val, $('#txt-message').data('selected-device'));
                        // Clear text box and reset focus for next comment. 
                        $('#txt-message').val('').focus();
                    });



                });

        </script>




    </div>
</asp:Content>
