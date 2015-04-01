$(document).on("click", ".reply", function (e) {
    var id = $(this).data("message-id");
    $('li[data-message-id=' + id + '] .amount').removeClass('hidden');
    $('li[data-message-id=' + id + '] .amount').addClass('animated bounceInRight');
});


$(function () {
    var chat = $.connection.chatHub;
    // Start the connection.
    $.connection.hub.start().done(function () {
        console.log('Now connected, connection ID=' + $.connection.hub.id);
        $.getJSON('api/Messages', function (data) {
            DrawMessages(data);
            //$.each(data, function (key, item) {
            //    DrawMessage(item);
            //})
        })
    });
    $("#txt-message").select2({
        ajax: {
            url: "/api/devices/ByKeyword/s/",
            dataType: 'json',
            delay: 250,
            data: function (params) {
                return {
                    q: params.term, // search term
                    page: params.page
                };
            },
            processResults: function (data) {
                return {
                    results: $.map(data, function (item) {
                        return {
                            text: item.Fullname,
                            id: item.ID
                        }
                    })
                };
            },
            cache: true
        },
        placeholder: "e.g S5 mini",
        allowClear: true,
        escapeMarkup: function (markup) { return markup; }, // let our custom formatter work
        minimumInputLength: 2,
        //formatResult: repoFormatResult, // omitted for brevity, see the source of this page
        //formatSelection: repoFormatSelection, // omitted for brevity, see the source of this page
        //dropdownCssClass: "bigdrop", // apply css that makes the dropdown taller
    });
    $(document).on('click', "#chatlist li .gobtn", function (e) {
        var li = $(this).closest("li");
        var id = li.data("message-id");
        var amount = li.find(".amount-value").val();
        chat.server.reply(id, amount);
    });
    $('#txt-message').on("select2:select", function (e) {
        $("#txt-message").data("selected-device", e.params.data.id);
        $("#txt-message").data("selected-device-name", e.params.data.text);
    });
    // $("#txt-message").select2('open');


    function DrawMessage(msg) {
        var messageTempl = $("#message-template").html();
        var tmpl = Handlebars.compile(messageTempl);
        var eachMessage = tmpl(msg);
        $('#chatlist').append($.parseHTML(eachMessage));
    }
    function DrawMessages(data) {
        var messageTemplate = $("#messages-template").html();
        var template = Handlebars.compile(messageTemplate);
        var eachMessage = template(data);
        $('#chatlist').append($.parseHTML(eachMessage));
    }

    // Create a function that the hub can call to broadcast messages.
    chat.client.broadcastMessage = function (msg) {
        DrawMessage(msg);
    };
    chat.client.broadcastReply = function (msgID, Amount) {
        $('li[data-message-id=' + msgID + '] .amount').removeClass('animated bounceInRight');
        $('li[data-message-id=' + msgID + '] .amount').addClass('animated bounceOutLeft');
        //$('li[data-message-id=' + msgID + '] .amount').addClass('hidden');
        var repliesList = $('ul[data-reply-msg-id=' + msgID + ']');
        if (repliesList.length) {
            var messageTemplate = $("#reply-template").html();
            var template = Handlebars.compile(messageTemplate);
            var eachMessage = template(Amount);
            repliesList.append(eachMessage);
        }
        else {
            var messageTemplate = $("#replyEmpty-template").html();
            var template = Handlebars.compile(messageTemplate);
            var eachMessage = template(Amount);
            $('li[data-message-id=' + msgID + '] #reply-container').append(eachMessage);
        }
    };
    chat.client.activeUsersList = function (users) {
        if (users.length != 0) {
            $.each(users, function (key, item) {
                $('#onlineUsers').append('<li>' + item.CompanyName + '</li>');
            })
        }
    };

    $('#btn-send').click(function () {
        var selectedText = $('#txt-message').data('selected-device-name');
        var selectedID = $('#txt-message').data('selected-device');
        chat.server.send(selectedText, selectedID);
        $("#txt-message").select2("val", "");
        $("#txt-message").select2('open');
    });



});