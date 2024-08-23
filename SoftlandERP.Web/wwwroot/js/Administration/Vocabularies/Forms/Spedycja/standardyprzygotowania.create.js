(function ($) {
    $(document).ready(function () {
        var logSelect = $('#SpedycjaStandardyPrzygotowania_AkronimLog');
        var magSelect = $('#SpedycjaStandardyPrzygotowania_AkronimMag');
        logSelect.change();
        magSelect.change();
    });

    $('#SpedycjaStandardyPrzygotowania_AkronimLog').change(function () {
        var logSelect = $("#SpedycjaStandardyPrzygotowania_AkronimLog").val();

        $.getJSON('/Administration/FormsSpedycjaStandardyPrzygotowania/GetLogByAkronim', { akronim: logSelect }, function (result) {
            if (result != null && result.length > 0) {

                var tbody = $('#akronimLogTable tbody');
                tbody.empty();

                for (var i = 0; i < result.length; i++) {
                    var newRow = '<tr>';
                    newRow += '<td class="center">' + result[i].czynnosc + '</td>';
                    newRow += '<td class="center">' + result[i].rodzajPrac + '</td>';
                    newRow += '<td class="center">' + result[i].opis + '</td>';
                    newRow += '</tr>';
                    tbody.append(newRow);
                }
            }
        });
    });

    $('#SpedycjaStandardyPrzygotowania_AkronimMag').change(function () {
        var magSelect = $("#SpedycjaStandardyPrzygotowania_AkronimMag").val();

        $.getJSON('/Administration/FormsSpedycjaStandardyPrzygotowania/GetMagByAkronim', { akronim: magSelect }, function (result) {
            if (result != null && result.length > 0) {

                var tbody = $('#akronimMagTable tbody');
                tbody.empty();

                for (var i = 0; i < result.length; i++) {
                    var newRow = '<tr>';
                    newRow += '<td class="center">' + result[i].czynnosc + '</td>';
                    newRow += '<td class="center">' + result[i].rodzajPrac + '</td>';
                    newRow += '<td class="center">' + result[i].opis + '</td>';
                    newRow += '</tr>';
                    tbody.append(newRow);
                }
            }
        });
    });


}).apply(this, [jQuery]);