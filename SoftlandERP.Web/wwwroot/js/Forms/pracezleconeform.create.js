(function ($) {

    $(document).ready(function () {
        var sekcjaSelect = $('#RodzajPracy');
            sekcjaSelect.change();
        $('#AkronimWyk').trigger('change');
    });

    $('#NazwaReceptury').change(function () {
        var recepturaSelected = $("#NazwaReceptury").val();

        //$('#SpedycjaKonfekcjaForm_IloscZlecenia').val('0');
        //$('#CzasWykonania').val('00:00:00');

        $.getJSON('/Forms/PraceZleconeForm/GetTimeByNR', { nr: recepturaSelected }, function (result) {
            if (result != null) {
                var timeComponents = result.split(':');
                var hours = parseInt(timeComponents[0]);
                var minutes = parseInt(timeComponents[1]);
                var seconds = parseInt(timeComponents[2]);

                var iloscZleceniaField = $('#IloscWykonana');
                var czasRbgField = $('#CzasWykonania');

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
    });

    $('#RodzajPracy').change(function () {
        var rpSelected = $("#RodzajPracy").val();
        var recepturaSelect = $('#NazwaReceptury');
        var recepturaSelected = $("#NazwaReceptury").val();
        recepturaSelect.empty();

        if (recepturaSelected != null && recepturaSelected != '') {
            $.getJSON('/Forms/PraceZleconeForm/GetNRByRP', { rp: rpSelected }, function (receptura) {
                if (receptura != null && !jQuery.isEmptyObject(receptura)) {

                    $.each(receptura, function () {

                        var isSelected = false;
                        if (this.value == recepturaSelected) {
                            isSelected = true;
                        }

                        recepturaSelect.append($('<option/>', {
                            value: this.value,
                            text: this.text,
                            selected: isSelected
                        }));
                    });
                    $('#NazwaReceptury').change();
                };
            });
        }
    });

    $('#AkronimWyk').change(function () {
        var acronymSelected = $("#AkronimWyk").val();
        var imieNazwiskoTextbox = $("#ImieNazwisko");

        if (acronymSelected != null && acronymSelected != '') {
            $.getJSON('/Forms/PraceZleconeForm/GetFirstLastNameByAcronym', { acronym: acronymSelected }, function (acronym) {
                if (acronym != null && !jQuery.isEmptyObject(acronym)) {
                    imieNazwiskoTextbox.val(acronym);
                    imieNazwiskoTextbox.attr('title', acronym);
                };
            });
        }
    });

    $('#IloscWykonana').change(function () {
        var czaswykonaniaSelected = $("#CzasWykonania").val();
        var recepturaSelected = $("#NazwaReceptury").val();

        if (czaswykonaniaSelected != null && czaswykonaniaSelected != '') {
            $.getJSON('/Forms/PraceZleconeForm/GetKwotaZleceniaByNR', { nr: recepturaSelected }, function (kwota) {
                console.log(kwota);

                // Przekszta³cenie czasu wykonania na liczbê godzin
                var czasWykonaniaAsTimeSpan = TimeSpanFromString(czaswykonaniaSelected);
                var czasWykonaniaWGodzinach = czasWykonaniaAsTimeSpan.totalHours;

                // Pomno¿enie przez stawkê godzinow¹
                var wynik = czasWykonaniaWGodzinach * kwota;
                var wynikZPrzecinkiem = wynik.toFixed(2).replace('.', ',');
                $("#WartoscPracyZleconej").val(wynikZPrzecinkiem);
                 console.log(wynik);
            });
        }
    });

    // Funkcja do konwersji formatu TimeSpan na obiekt TimeSpan
    function TimeSpanFromString(timeSpanString) {
        var parts = timeSpanString.split(':');
        var hours = parseInt(parts[0]);
        var minutes = parseInt(parts[1]);
        var seconds = parseInt(parts[2]);
        return {
            totalHours: hours + minutes / 60 + seconds / 3600
        };
    }





}).apply(this, [jQuery]);
