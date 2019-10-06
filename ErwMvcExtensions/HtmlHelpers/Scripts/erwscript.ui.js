(function($) {

    function setExpanders($) {
        let expanderElements = $("div[data-ui-type='expander']");

        for (let i = 0; i < expanderElements.length; i++) {
            let expanderTitles = $(expanderElements[i]).children("div[data-ui-type='expander-title']");

            $(expanderTitles).first().click(function() {
                $(this).parent("div[data-ui-type='expander']").toggleClass('expanded');
                $(this).find(".erw-arrow").first().toggleClass('active');
                $(this).parent("div[data-ui-type='expander']").children("div[data-ui-type='expander-content']").first().toggle();
            });
        }
    }

    setExpanders($);

    function setAutoСompletes($) {

        let autoCompleteElements = $("div[data-ui-type='autocomplete']");

        for (let i = 0; i < autoCompleteElements.length; i++) {

            let autoCompleteInput = $(autoCompleteElements[i])
                .children("div[data-ui-type='autocomplete-input']")
                .find("input[type='text']").first();

            $(autoCompleteInput).click(function() {

                let list = $(this).parents("div[data-ui-type='autocomplete']")
                    .find("div[data-ui-type='autocomplete-list']").first();

                let actionUrl = window.location.protocol + "//" + window.location.host + $(this).parents("div[data-ui-type='autocomplete']").data("ui-route");
                let httpMethod = $(this).parents("div[data-ui-type='autocomplete']").data("ui-method");

                $.ajax({
                    url: actionUrl,
                    type: httpMethod,
                    data: { comparePattern: $(this).val() },
                    success: function(data) {
                        $(list).empty();

                        if (data.list.length === 0) {
                            let element = $('<div></div>').text('No matches.');
                            $(list).append($(element));

                        }
                        else {
                            for (let j = 0; j < data.list.length; j++) {
                                let element = $("<div class='erw-autocomplete-list-item'></div>").text(data.list[j]);
                                $(element).click(function() {
                                    console.log('click');
                                    let input = $(this).parents("div[data-ui-type='autocomplete']")
                                        .children("div[data-ui-type='autocomplete-input']").first()
                                        .find("input[type='text']").first();

                                    let currentSelectedItem = $(this).parents("div[data-ui-type='autocomplete']")
                                        .find("div[data-ui-type='autocomplete-list']").first()
                                        .children(".selected").first();

                                    $(currentSelectedItem).removeClass("selected");
                                    $(this).addClass("selected");

                                    $(input).val($(this).html());
                                });
                                $(list).append($(element));
                            }
                        }

                        $(list).animate({
                            scrollTop: 0
                        }, 1);

                        $(list).show();
                    }
                });
            });

            $(autoCompleteInput).blur(function() {
                let autocompleteList = $(this).parents("div[data-ui-type='autocomplete']")
                    .children("div[data-ui-type='autocomplete-list']").first();

                if (!($(autocompleteList).is(':hover') > 0)) {
                    $(autocompleteList).empty();
                    $(autocompleteList).hide();
                }
                else {
                    $(autoCompleteInput).focus();
                }
            });

            $(autoCompleteInput).on('keyup', function(e) {
                if (e.keyCode < 8) {
                    return;
                }
                else if (e.keyCode > 8 && e.keyCode < 48 && e.keyCode !== 13 & e.keyCode !== 27) {
                    return;
                }
                else if (e.keyCode > 90 && e.keyCode < 186) {
                    return;
                }

                let autocompleteList = $(this).parents("div[data-ui-type='autocomplete']")
                    .find("div[data-ui-type='autocomplete-list']").first();

                if (e.keyCode === 13 || e.keyCode === 27) {
                    $(autocompleteList).empty();
                    $(autocompleteList).hide();
                    return;
                }

                let actionUrl = window.location.protocol + "//" + window.location.host + $(this).parents("div[data-ui-type='autocomplete']").data("ui-route");
                let httpMethod = $(this).parents("div[data-ui-type='autocomplete']").data("ui-method");

                $.ajax({
                    url: actionUrl,
                    type: httpMethod,
                    data: { comparePattern: $(this).val() },
                    success: function(data) {
                        $(autocompleteList).empty();

                        if (data.list.length === 0) {
                            let element = $('<div></div>').text('No matches.');
                            $(autocompleteList).append($(element));
                        }
                        else {
                            for (let j = 0; j < data.list.length; j++) {
                                let element = $("<div class='erw-autocomplete-list-item'></div>").text(data.list[j]);
                                $(element).click(function() {
                                    console.log('click');

                                    let input = $(this).parents("div[data-ui-type='autocomplete']")
                                        .children("div[data-ui-type='autocomplete-input']").first()
                                        .find("input[type='text']").first();

                                    let currentSelectedItem = $(this).parents("div[data-ui-type='autocomplete']")
                                        .find("div[data-ui-type='autocomplete-list']").first()
                                        .children(".selected").first();

                                    $(currentSelectedItem).removeClass("selected");
                                    $(this).addClass("selected");

                                    $(input).val($(this).html());
                                });
                                $(autocompleteList).append($(element));
                            }
                        }

                        $(autocompleteList).animate({
                            scrollTop: 0
                        }, 1);

                        $(autocompleteList).show();
                    }
                });
            });

            $(autoCompleteInput).on('keydown', function(e) {

                let autocompleteList = $(this).parents("div[data-ui-type='autocomplete']")
                    .find("div[data-ui-type='autocomplete-list']").first();

                let currentSelectedItem = $(autocompleteList).children(".selected").first();

                let firstItem = $(this).parents("div[data-ui-type='autocomplete']")
                    .find("div[data-ui-type='autocomplete-list']")
                    .children().first();

                let lastItem = $(this).parents("div[data-ui-type='autocomplete']")
                    .find("div[data-ui-type='autocomplete-list']")
                    .children().last();


                switch (e.keyCode) {
                    case 13:
                        $(this).blur();
                        e.stopPropagation();
                        $(autocompleteList).empty();
                        $(autocompleteList).hide();
                        break;
                    case 27:
                        $(this).blur();
                        $(autocompleteList).empty();
                        $(autocompleteList).hide();
                        break;
                    case 38:
                        if ($(currentSelectedItem).index() === $(firstItem).index()) {
                            break;
                        }
                        else {
                            $(currentSelectedItem).removeClass("selected");
                            $(currentSelectedItem).prev().addClass("selected");
                            $(this).val($(currentSelectedItem).prev().html());

                            let caretElement = $(this);
                            let caretPosition = $(this).val().length * 2;

                            setTimeout(function() {
                                $(caretElement).get(0).setSelectionRange(caretPosition, caretPosition);
                            }, 1);

                            let list = $(this).parents("div[data-ui-type='autocomplete']")
                                .find("div[data-ui-type='autocomplete-list']").first();

                            let elementIndex = $(currentSelectedItem).index();
                            let scrollHeight = $(currentSelectedItem).outerHeight() * elementIndex;
                            let listHeight = $(list).scrollTop() + $(currentSelectedItem).outerHeight();

                            if (scrollHeight < listHeight) {
                                $(list).animate({
                                    scrollTop: scrollHeight - $(currentSelectedItem).outerHeight()
                                }, 1);
                            }
                        }
                        break;
                    case 40:
                        if ($(currentSelectedItem).index() === $(lastItem).index()) {
                            break;
                        }
                        else {
                            if ($(currentSelectedItem).html() !== undefined) {

                                $(currentSelectedItem).removeClass("selected");
                                currentSelectedItem = $(currentSelectedItem).next();
                                $(currentSelectedItem).addClass("selected");
                                $(this).val($(currentSelectedItem).html());

                                let list = $(this).parents("div[data-ui-type='autocomplete']")
                                    .find("div[data-ui-type='autocomplete-list']").first();

                                let elementIndex = $(currentSelectedItem).index();
                                let scrollHeight = $(currentSelectedItem).outerHeight() * elementIndex;
                                let listHeight = $(list).scrollTop() + $(list).outerHeight() - $(currentSelectedItem).outerHeight();

                                if (scrollHeight > listHeight) {
                                    $(list).animate({
                                        scrollTop: $(list).scrollTop() + $(currentSelectedItem).outerHeight()
                                    }, 1);
                                }
                            }
                            else {
                                currentSelectedItem = firstItem;
                                $(currentSelectedItem).addClass("selected");
                                $(this).val($(currentSelectedItem).html());
                            }
                        }
                        break;
                    default:
                }
            });
        }
    }

    setAutoСompletes($);

}(jQuery));