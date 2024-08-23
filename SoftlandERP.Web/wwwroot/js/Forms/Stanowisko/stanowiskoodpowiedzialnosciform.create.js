(function ($) {

    $(document).ready(function () {
        //var sekcjaSelect = $('#RodzajPracy');
        //    sekcjaSelect.change();
        $('#AkronimWyk').trigger('change');
        $('#Symbol').trigger('change');
        $('#KluczPrzydzialu').trigger('change');
    });

    $('#Symbol').change(function () {
        var symbolSelected = $("#Symbol").val();
        var nazwaOdpowiedzialnosciTextbox = $("#NazwaOdpowiedzialnosci");

        if (symbolSelected != null && symbolSelected != '') {
            $.getJSON('/Forms/StanowiskoOdpowiedzialnosciForm/GetNazwaBySymbol', { symbol: symbolSelected }, function (symbol) {
                if (symbol != null && !jQuery.isEmptyObject(symbol)) {
                    nazwaOdpowiedzialnosciTextbox.val(symbol);
                    nazwaOdpowiedzialnosciTextbox.attr('title', symbol);
                };
            });
        }
    });

    $('#KluczPrzydzialu').change(function () {
        var kluczPrzydzialuSelected = $("#KluczPrzydzialu").val();
        var dzialField = $("#Dzial");
        console.log(kluczPrzydzialuSelected);

        if (kluczPrzydzialuSelected === 'Lokalizacja') {
            dzialField.val(null);
            dzialField.prop('disabled', true);
        } else {
            dzialField.prop('disabled', false);
        }
    });


    $('#AkronimWyk').change(function () {
        var acronymSelected = $("#AkronimWyk").val();
        var imieNazwiskoTextbox = $("#ImieNazwisko");
        if (acronymSelected != null && acronymSelected != '') {
            $.getJSON('/Forms/StanowiskoOdpowiedzialnosciForm/GetFirstLastNameByAcronym', { acronym: acronymSelected }, function (acronym) {
                if (acronym != null && !jQuery.isEmptyObject(acronym)) {
                    imieNazwiskoTextbox.val(acronym);
                    imieNazwiskoTextbox.attr('title', acronym);
                };
            });
        }
    });

}).apply(this, [jQuery]);
