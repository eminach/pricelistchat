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
        </div>


        <script src="Scripts/jquery.signalR-2.1.2.js"></script>
        <link rel="stylesheet" href="//code.jquery.com/ui/1.11.3/themes/smoothness/jquery-ui.css">
        <script src="signalr/hubs"></script>
        <script src="Scripts/jquery.timeago.js"></script>
        <script src="//code.jquery.com/ui/1.11.3/jquery-ui.js"></script>
        <script type="text/javascript">
            $(document).on("click", ".reply", function (e) {
                var id = $(this).data("message-id");
                $('li[data-message-id=' + id+'] .amount').removeClass('hidden');
                $('li[data-message-id=' + id + '] .amount').addClass('animated bounceInRight');
            })
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
        </script>
        <%-- <script type="text/javascript">
            $(document).ready(function () {
               
            });
        </script>--%>
        <script type="text/javascript">
 
            $(function () {
               
                // Declare a proxy to reference the hub. 
                function DrawMessage(msg) {                    
                    var eachmessage = '<li class="left clearfix"  data-message-id=' + msg.ID + '><span class="chat-img pull-left">' +
                          '<img src="http://placehold.it/50/55C1E7/fff&text=U" alt="User Avatar" class="img-circle" />' +
                            '</span>' +
                                '<div class="chat-body clearfix">' +
                                    '<div class="header"><strong class="primary-font">' +
                                          msg.User.FirstName +
                                          '</strong> <small class="pull-right text-muted">' +
                                          '<span class="glyphicon glyphicon-time"></span><abbr class="timeago">' + jQuery.timeago(msg.PostDate) + '</abbr></small></div>' +
                                           '<p>' + msg.AskedDevice.Fullname + '</p>' +
                                           '<p class="pull-right"><a href="#" class="reply pull-left" data-message-id=' + msg.ID + '><span class="glyphicon glyphicon-plus-sign" aria-hidden="true"></span>reply</a>' +
                                                 '<div data-message="" class="amount hidden pull-right col-sm-3 input-group input-group-sm pull-right">' +
                                                    '<span class="input-group-addon" id="sizing-addon3">AED</span>' +
                                                       '<input type="text" class="form-control" placeholder="0.00" aria-describedby="sizing-addon3">'+
                                                       ' <span class="input-group-btn">'+
                                                       '   <button class="gobtn btn btn-default" type="button">Go!</button></span></div>' +
                                           ' </p></div></li>';

                    $('#chatlist').append(eachmessage);
                }

                var chat = $.connection.chatHub;
                // Create a function that the hub can call to broadcast messages.
                chat.client.broadcastMessage = function (msg) {
                    DrawMessage(msg);
                };
                // Get the user name and store it to prepend to messages.
                $('#displayname').val('Emin');
                // Set initial focus to message input box.  
                $('#txt-message').focus();
                // Start the connection.
                $.connection.hub.start().done(function () {
                    $.getJSON('api/Messages', function (data) {
                        $.each(data, function (key, item) {
                            message = item.AskedDevice.Fullname;
                            DrawMessage(item);
                        })
                    })
                    console.log('Now connected, connection ID=' + $.connection.hub.id);
                    $('#btn-send').click(function () {
                        // Call the Send method on the hub. 
                        chat.server.send($('#txt-message').val, $('#txt-message').data('selected-device'));
                        // Clear text box and reset focus for next comment. 
                        $('#txt-message').val('').focus();
                    });

                    $('#txt-message').keydown(function (event) {
                        if (event.which == 13) {

                            event.preventDefault();
                            chat.server.send($('#txt-message').val, $('#txt-message').data('selected-device'));
                            // Clear text box and reset focus for next comment. 
                            $('#txt-message').val('').focus();
                            return false;
                        }
                    });
                    $('#chatlist li .gobtn').on('click', function () {
                        var id = $(this).data("message-id");
                        alert(id);
                    })
                });

                jQuery("abbr.timeago").timeago();
            });
            //Bu ne zibildir eee nese chatnan elaqelidi gozleyin 

          

        </script>




    </div>
</asp:Content>
