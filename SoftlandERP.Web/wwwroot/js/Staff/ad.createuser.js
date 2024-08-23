(function ($) {
    $(document).ready(function () {
        if (document.getElementById("ADUser_AccountExpirationDateCheck").checked) {
            document.getElementById("ADUser_AccountExpirationDate").type = "date";
        }
        else {
            document.getElementById("ADUser_AccountExpirationDate").type = "text";
        }
    });

    $("#ADUser_AccountExpirationDateCheck").change(function () {
        if (this.checked) {
            document.getElementById("ADUser_AccountExpirationDate").type = "date";
        }
        else {
            document.getElementById("ADUser_AccountExpirationDate").type = "text";
        }
    });
}).apply(this, [jQuery]);

(function ($) {
    $("#ADUser_PasswordNeverExpires").change(function () {
        if (this.checked) {
            document.getElementById("ADUser_UserMustChangePassword").checked = false;
        }
    });

    $("#ADUser_UserMustChangePassword").change(function () {
        if (this.checked) {
            document.getElementById("ADUser_PasswordNeverExpires").checked = false;
            document.getElementById("ADUser_UserCannotChangePassword").checked = false;
        }
    });

    $("#ADUser_UserCannotChangePassword").change(function () {
        if (this.checked) {
            document.getElementById("ADUser_UserMustChangePassword").checked = false;
        }
    });
}).apply(this, [jQuery]);

(function ($) {
    $(document).ready(function () {
        var countrySelect = $('#ADUser_Address_Country');
        if (countrySelect.prop('disabled') == false) {
            countrySelect.change();
        }
    });

    $('#ADUser_Address_Country').change(function () {
        var countrySelected = $("#ADUser_Address_Country").val();
        var citiesSelect = $('#ADUser_Address_City');
        var citySelected = $("#ADUser_Address_City").val();
        citiesSelect.empty();

        if (countrySelected != null && countrySelected != '') {
            $.getJSON('/Administration/UserAddress/GetCitiesByCountry', { country: countrySelected }, function (cities) {
                if (cities != null && !jQuery.isEmptyObject(cities)) {
                    $.each(cities, function () {

                        var isSelected = false;
                        if (this.value == citySelected) {
                            isSelected = true;
                        }

                        citiesSelect.append($('<option/>', {
                            value: this.value,
                            text: this.text,
                            selected: isSelected
                        }));
                    });
                    $('#ADUser_Address_City').change();
                    citiesSelect.prop('disabled', false);
                    citiesSelect.attr('readonly', false);
                };
            });
        }
    });

    $('#ADUser_Address_City').change(function () {
        var citySelected = $("#ADUser_Address_City").val();
        var streetsSelect = $('#ADUser_Address_Street');
        var streetSelected = $("#ADUser_Address_Street").val();
        streetsSelect.empty();

        if (citySelected != null && citySelected != '') {
            $.getJSON('/Administration/Address/GetStreetsByCity', { city: citySelected }, function (streets) {
                if (streets != null && !jQuery.isEmptyObject(streets)) {
                    $.each(streets, function () {

                        var isSelected = false;
                        if (this.value == streetSelected) {
                            isSelected = true;
                        }

                        streetsSelect.append($('<option/>', {
                            value: this.value,
                            text: this.text,
                            selected: isSelected
                        }));
                    });
                };
            });
        }
        streetsSelect.prop('disabled', false);
        streetsSelect.attr('readonly', false); 
    });
}).apply(this, [jQuery]);

(function ($) {
    $('#optima-fill').click(function () {

        $.LoadingOverlay("show");
        var userLogin = $("#ADUser_Login").val();
        $.getJSON('/Staff/AD/GetInformationFromOptima', { login: userLogin }, function (info) {
            if (info != null && !jQuery.isEmptyObject(info)) {
                if (info[0]) $("#ADUser_Company").val(info[0]);
                if (info[1]) $("#ADUser_Department").val(info[1]);
                if (info[2]) $("#ADUser_JobTitle").val(info[2]);
            };
        });
        $.LoadingOverlay("hide");
    });
}).apply(this, [jQuery]);