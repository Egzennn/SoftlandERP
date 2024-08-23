(function ($) {
    $(document).ready(function () {
        var sekcjaSelect = $('#SpedycjaKonfekcjaForm_Sekcja');
        var logSelect = $('#SpedycjaKonfekcjaForm_AkronimLog');
            sekcjaSelect.change();
            logSelect.change();
    });

    $('#SpedycjaKonfekcjaForm_AkronimLog').change(function () {
        var akronimLogSelected = $("#SpedycjaKonfekcjaForm_AkronimLog").val();

        //$('#SpedycjaKonfekcjaForm_IloscZlecenia').val('0');
        //$('#SpedycjaKonfekcjaForm_Czas').val('00:00:00');

        $.getJSON('/Forms/SpedycjaKonfekcjaForm/GetTimeByLog', { log: akronimLogSelected }, function (result) {
            if (result != null) {
                var timeComponents = result.split(':');
                var hours = parseInt(timeComponents[0]);
                var minutes = parseInt(timeComponents[1]);
                var seconds = parseInt(timeComponents[2]);

                var iloscZleceniaField = $('#SpedycjaKonfekcjaForm_IloscZlecenia');
                var czasRbgField = $('#SpedycjaKonfekcjaForm_Czas');

                iloscZleceniaField.on('input', function () {
                    var iloscZlecenia = parseFloat(iloscZleceniaField.val());
                    if (!isNaN(iloscZlecenia)) {
                        var czasRbgMinutes = iloscZlecenia * ((hours * 60) + minutes + (seconds / 60));

                        // Convert back to time format
                        var calculatedHours = Math.floor(czasRbgMinutes / 60);
                        var calculatedMinutes = Math.floor(czasRbgMinutes % 60);
                        var calculatedSeconds = Math.round((czasRbgMinutes - Math.floor(czasRbgMinutes)) * 60);

                        var calculatedTime = `${calculatedHours}:${calculatedMinutes.toString().padStart(2, '0')}:${calculatedSeconds.toString().padStart(2, '0')}`;

                        czasRbgField.val(calculatedTime); // Update czasRbgField
                    }
                });

                // Trigger the input event on iloscZleceniaField to update czasRbgField initially
                iloscZleceniaField.trigger('input');
            }
        });


        $.getJSON('/Forms/SpedycjaKonfekcjaForm/GetMagByLog', { log: akronimLogSelected }, function (result) {
            if (result != null && result.length > 0) {
                result.sort(function (a, b) {
                    return a.akronim.localeCompare(b.akronim);
                });

                var tbody = $('#akronimMagTable tbody');
                tbody.empty();

                for (var i = 0; i < result.length; i++) {
                    var newRow = '<tr>';
                    newRow += '<td class="center">' + result[i].akronim + '</td>';
                    newRow += '<td class="center">' + result[i].czynnosc + '</td>';
                    newRow += '<td class="center">' + result[i].rodzajPrac + '</td>';
                    newRow += '<td class="center">' + result[i].opis + '</td>';
                    newRow += '</tr>';
                    tbody.append(newRow);
                }
            }
        });
    });

    $('#SpedycjaKonfekcjaForm_Sekcja').change(function () {
        var sekcjaSelected = $("#SpedycjaKonfekcjaForm_Sekcja").val();
        var towarSelect = $('#SpedycjaKonfekcjaForm_Towar');
        var towarSelected = $("#SpedycjaKonfekcjaForm_Towar").val();
        towarSelect.empty();


        if (towarSelected != null && towarSelected != '') {
            $.getJSON('/Forms/SpedycjaKonfekcjaForm/GetTowarBySekcja', { sekcja: sekcjaSelected }, function (towar) {
                if (towar != null && !jQuery.isEmptyObject(towar)) {

                    $.each(towar, function () {

                        var isSelected = false;
                        if (this.value == towarSelected) {
                            isSelected = true;
                        }

                        towarSelect.append($('<option/>', {
                            value: this.value,
                            text: this.text,
                            selected: isSelected
                        }));
                    });
                    $('#SpedycjaKonfekcjaForm_Towar').change();
                };
            });
        }
    });

    $('#SpedycjaKonfekcjaForm_Towar').change(function () {
        var towarSelected = $("#SpedycjaKonfekcjaForm_Towar").val();

        $.getJSON('/Forms/SpedycjaKonfekcjaForm/GetIloscByTowar', { towar: towarSelected }, function (result) {
            if (result != null && !jQuery.isEmptyObject(result)) {
                $('#SpedycjaKonfekcjaForm_IloscMagazynowa').val(result["iloscMagazynowa"]);
                $('#SpedycjaKonfekcjaForm_Suma_Sprzedazy').val(result["suma_Sprzedazy"]);
                $('#SpedycjaKonfekcjaForm_Min_Ilosc').val(result["min_Ilosc"]);
                $('#SpedycjaKonfekcjaForm_Max_Ilosc').val(result["max_Ilosc"]);
            };
        });
    })
}).apply(this, [jQuery]);
