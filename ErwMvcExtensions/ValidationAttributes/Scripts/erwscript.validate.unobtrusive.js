﻿(function($) {

    $.validator.addMethod("notequalsto", function(value, element, params) {

        if (this.optional(element)) return true;

        var splitPattern = /\W/;
        var propertiesToCompare = params.propertiesToCompare.split(splitPattern);
        var propertiesToCompareDisplayNames = params.propertiesToCompareDisplayNames.split(splitPattern);
        var propertiesDisplayNames = [];

        for (var i = 0; i < propertiesToCompare.length; i++) {
            if ($('#' + propertiesToCompare[i]).val() === value) {
                propertiesDisplayNames.push(propertiesToCompareDisplayNames[i]);
            }
        }

        if (propertiesDisplayNames.length > 0) return false;

        return true;
    });

    $.validator.unobtrusive.adapters.add("notequalsto", ["propertiestocompare", "propertiestocomparedisplaynames", "errormessage"], function(options) {
        options.rules['notequalsto'] = {
            propertiesToCompare: options.params.propertiestocompare,
            propertiesToCompareDisplayNames: options.params.propertiestocomparedisplaynames
        };
        options.messages['notequalsto'] = options.message;
    });

    $.validator.addMethod("equalstomultiple", function(value, element, params) {
        if (this.optional(element)) return true;

        var splitPattern = /\W/;
        var propertiesToCompare = params.propertiesToCompare.split(splitPattern);
        var propertiesToCompareDisplayNames = params.propertiesToCompareDisplayNames.split(splitPattern);
        var propertiesDisplayNames = [];

        for (var i = 0; i < propertiesToCompare.length; i++) {
            if ($('#' + propertiesToCompare[i]).val() !== value) {
                propertiesDisplayNames.push(propertiesToCompareDisplayNames[i]);
            }
        }

        if (propertiesDisplayNames.length > 0) return false;

        return true;
    });

    $.validator.unobtrusive.adapters.add("equalstomultiple", ["propertiestocompare", "propertiestocomparedisplaynames", "errormessage"], function(options) {
        options.rules['equalstomultiple'] = {
            propertiesToCompare: options.params.propertiestocompare,
            propertiesToCompareDisplayNames: options.params.propertiestocomparedisplaynames
        };
        options.messages['equalstomultiple'] = options.message;
    });

    $.validator.addMethod("httppostedfilebaseextensions", function(value, element, params) {
        if (this.optional(element)) return true;

        var splitPattern = ',';
        var validExtensions = params.validExtensions.split(splitPattern);

        for (var i = 0; i < element.files.length; i++) {
            var isValid = false;
            for (var j = 0; j < validExtensions.length; j++) {
                if (element.files[i].name.endsWith(validExtensions[j])) {
                    isValid = true;
                }
            }
            if (!isValid) {
                return false;
            }
        }

        return true;
    });

    $.validator.unobtrusive.adapters.add("httppostedfilebaseextensions", ["validextensions"], function(options) {
        options.rules['httppostedfilebaseextensions'] = {
            validExtensions: options.params.validextensions
        };
        options.messages['httppostedfilebaseextensions'] = options.message;
    });

    $.validator.addMethod("httppostedfilebasecount", function(value, element, params) {

        if (this.optional(element)) return true;

        if (element.files.length < params.minCount || element.files.length > params.maxCount) {
            return false;
        }

        return true;
    });

    $.validator.unobtrusive.adapters.add("httppostedfilebasecount", ["mincount", "maxcount"], function(options) {
        options.rules['httppostedfilebasecount'] = {
            minCount: options.params.mincount,
            maxCount: options.params.maxcount
        };
        options.messages['httppostedfilebasecount'] = options.message;
    });

    $.validator.addMethod("httppostedfilebasesize", function(value, element, params) {

        if (this.optional(element)) return true;

        let multiplier = 1;

        switch (params.sizeUnit) {
            case "B":
                break;
            case "Kb":
                multiplier = 1024;
                break;
            case "Mb":
                multiplier = 1048576;
                break;
            case "Gb":
                multiplier = 1073741824;
                break;
            default:
        }

        for (var i = 0; i < element.files.length; i++) {
            if (element.files[i].size < params.minSize * multiplier || element.files[i].size > params.maxSize * multiplier) {
                return false;
            }
        }

        return true;
    });

    $.validator.unobtrusive.adapters.add("httppostedfilebasesize", ["minsize", "maxsize", "sizeunit"], function(options) {
        options.rules['httppostedfilebasesize'] = {
            minSize: options.params.minsize,
            maxSize: options.params.maxsize,
            sizeUnit: options.params.sizeunit
        };
        options.messages['httppostedfilebasesize'] = options.message;
    });


    $.validator.addMethod("remoteandserver", function(value, element, params) {

        if (this.optional(element)) return true;

        let actionUrl = window.location.protocol + "//" + window.location.host + params.url;
        let httpMethod = $(element).data('val-remoteandserver-method');
        let validationResult = true;

        $.ajax({
            url: actionUrl,
            type: httpMethod,
            data: { KeyWord: value },
            success: function(data) {
                if (!data) {
                    validationResult = false;
                }
            },
            async: false
        });

        return validationResult;
    });

    $.validator.unobtrusive.adapters.add("remoteandserver", ["url", "method"], function(options) {
        options.rules['remoteandserver'] = {
            url: options.params.url,
            method: options.params.method
        };
        options.messages['remoteandserver'] = options.message;
    });

    $.validator.addMethod("requiredif", function(value, element, params) {
        let validationResult = true;
        let propertyToCheck = $('#' + params.propertyToCheck);
        let propertyToCheckVal = $(propertyToCheck).prop('type').toUpperCase() === 'CHECKBOX' ?
            $(propertyToCheck).prop('checked') : $(propertyToCheck).val();

        switch (params.compareMethod) {
            case "EqualsTo":
                if (propertyToCheckVal !== undefined && propertyToCheckVal !== null && String(propertyToCheckVal).toLowerCase() === String(params.propertyToCheckValue).toLowerCase()) {
                    if (value === undefined || value === null || value === "") {
                        validationResult = false;
                    }
                }
                break;
            case "NotEqualsTo":
                if (propertyToCheckVal === undefined || propertyToCheckVal === null || String(propertyToCheckVal).toLowerCase() !== String(params.propertyToCheckValue).toLowerCase()) {
                    if (value === undefined || value === null || value === "") {
                        validationResult = false;
                    }
                }
                break;
            default:
        }

        return validationResult;
    });

    $.validator.unobtrusive.adapters.add("requiredif", ["propertytocheck", "propertytocheckvalue", "comparemethod"], function(options) {
        options.rules['requiredif'] = {
            propertyToCheck: options.params.propertytocheck,
            propertyToCheckValue: options.params.propertytocheckvalue,
            compareMethod: options.params.comparemethod
        };
        options.messages['requiredif'] = options.message;
    });

    $.validator.addMethod("availableif", function(value, element, params) {
        console.log('availableif');

        let validationResult = true;
        let propertyToCheckVal = $('#' + params.propertyToCheck).val();

        switch (params.compareMethod) {
            case "EqualsTo":
                if (value !== undefined && value !== null && value !== "") {
                    if (propertyToCheckVal !== undefined && propertyToCheckVal !== null && propertyToCheckVal === params.propertyToCheckValue) {
                        validationResult = false;
                    }                
                }
                break;
            case "NotEqualsTo":
                if (value !== undefined && value !== null && value !== "") {
                    if (propertyToCheckVal !== undefined || propertyToCheckVal !== null || propertyToCheckVal !== params.propertyToCheckValue) {
                        validationResult = false;
                    }
                }               
                break;
            default:
        }

        return validationResult;
    });

    $.validator.unobtrusive.adapters.add("availableif", ["propertytocheck", "propertytocheckvalue", "comparemethod"], function(options) {
        options.rules['availableif'] = {
            propertyToCheck: options.params.propertytocheck,
            propertyToCheckValue: options.params.propertytocheckvalue,
            compareMethod: options.params.comparemethod
        };
        options.messages['availableif'] = options.message;

        let propertyToCheck = $('#' + options.params.propertytocheck);       
        let props = $("[data-val-availableif-propertytocheck=" + options.params.propertytocheck + "]");

        switch (options.params.comparemethod) {
            case "EqualsTo":
                $(props).hide();
                break;
            case "NotEqualsTo":
                $(props).show();
                break;
            default:
        }

        $(propertyToCheck).change(function() {
            let props = $("[data-val-availableif-propertytocheck=" + options.params.propertytocheck + "]");
            let propertyToCheckVal = $(propertyToCheck).prop('type').toUpperCase() === 'CHECKBOX' ?
                $(propertyToCheck).prop('checked') : $(propertyToCheck).val();

            if (String(propertyToCheckVal).toLowerCase() === options.params.propertytocheckvalue.toLowerCase()) {
                $(props).show();
            }
            else {
                $(props).hide();
            }
        });
    });

}(jQuery));