(function ($) {
    $(document).ready(function () {
        var grupaSelect = $('#Grupa');
        grupaSelect.change();
    });

    $('#Grupa').change(function () {
        var grupaSelected = $("#Grupa").val();
        var nazwaSelect = $('#Nazwa');
        var nazwaSelected = $("#Nazwa").val();
        nazwaSelect.empty();


        if (nazwaSelected != null && nazwaSelected != '') {
            $.getJSON('/Forms/StanowiskoOprogramowanieForm/GetNazwaByGrupa', { grupa: grupaSelected }, function (nazwa) {
                if (nazwa != null && !jQuery.isEmptyObject(nazwa)) {
                    $.each(nazwa, function () {

                        var isSelected = false;
                        if (this.value == nazwaSelected) {
                            isSelected = true;
                        }

                        nazwaSelect.append($('<option/>', {
                            value: this.value,
                            text: this.text,
                            selected: isSelected
                        }));
                    });
                    $('#Nazwa').change();
                };
            });
        }
    });


}).apply(this, [jQuery]);