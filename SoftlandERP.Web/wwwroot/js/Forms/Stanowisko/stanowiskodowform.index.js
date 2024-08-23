(function ($) {

    'use strict';

    var datatableInit = function () {
        var $table = $('#datatable');

        // format function for row details
        var fnFormatDetails = function (datatable, tr) {
            var data = datatable.fnGetData(tr);

            return [
                '<table class="table table-striped mb-0">',
                '<tr class="b-top-0">',
                '<td style="width: 200px;"><label class="mb-0">Opis:</label></td>',
                '<td>' + data[6] + '</td>',
                '</tr>',
                '<tr>',
                '<td style="width: 200px;"><label class="mb-0">Zmodyfikowano:</label></td>',
                '<td>' + data[11] + ' </td>',
                '</tr>',
                '<tr>',
                '<td style="width: 200px;"><label class="mb-0">Zmodyfikowano przez:</label></td>',
                '<td>' + data[12] + '</td>',
                '</tr>',
                '<tr>',
                '<td style="width: 200px;"><label class="mb-0">Status:</label></td>',
                '<td>' + data[14] + '</td>',
                '</tr>',
                '</table>'

            ].join('');
        };

        // insert the expand/collapse column
        var th = document.createElement('th');
        var td = document.createElement('td');
        td.innerHTML = '<i data-toggle class="far fa-plus-square text-primary h5 m-0" style="cursor: pointer;"></i>';
        td.className = "text-center";

        $table
            .find('thead tr').each(function () {
                this.insertBefore(th, this.childNodes[0]);
            });

        $table
            .find('tbody tr').each(function () {
                this.insertBefore(td.cloneNode(true), this.childNodes[0]);
            });

        // initialize
        var datatable = $table.dataTable({
            buttons: [
                {
                    text: '<i class="fa-solid fa-user-plus"></i> Dodanie wpisu',
                    attr: {
                        id: "addDataTableButton"
                    },
                    action: function () {
                        $.LoadingOverlay("show");
                        $.ajax({
                            url: "/Forms/StanowiskoDOWForm/Create",
                            success: function (response) {
                                $.LoadingOverlay("hide");
                                $("#modal-content").html(response);
                                $("#modal").modal("show");
                            },
                            error: function () {
                                $.LoadingOverlay("hide");
                            }
                        });
                    }
                },
                {
                    extend: 'print',
                    text: '<i class="fa-solid fa-print"></i> Drukuj',
                },
                {
                    extend: 'collection',
                    text: '<i class="fa-solid fa-table"></i> Excel',
                    attr: {
                        id: "exportStanDataTableButton"
                    },
                    buttons: [
                        {
                            text: 'Cała baza',
                            action: function () {
                                window.location.href = '/Forms/StanowiskoDOWForm/ExportExcel';
                            }
                        },
                        {
                            extend: 'excel',
                            text: 'Aktualny widok',
                        }
                    ]
                }
            ],

            language: {
                paginate: {
                    previous: "<",
                    next: ">"
                },
                sSearch: "Wyszukaj:",
                emptyTable: "Brak danych",
                sLengthMenu: '_MENU_ rekordów na stronie',
                "sInfo": "Pokazujesz od _START_ do _END_ z _TOTAL_ rekordów",
            },
            dom: "<'row'<'col-sm-12 col-md-6'B><'col-sm-12 col-md-6'f>>" + "<'row'<'col-sm-12 col-md-6'l>>" + "<'row'<'table-responsive't>>" + "<'row'<'col-sm-12 col-md-5'i><'col-sm-12 col-md-7'p>>",

            aoColumnDefs: [{
                bSortable: false,
                aTargets: [0]
            }],
            aaSorting: [
                [9, 'dsc']
            ]
        });

        $('#datatable_wrapper').find('.btn-secondary').removeClass('btn-secondary').addClass('btn-outline-danger').addClass('mb-3');
        $('#datatable_filter').find('.pull-right').removeClass('pull-right').addClass('form-control-sm').addClass('mb-3');

        // add a listener
        $table.on('click', 'i[data-toggle]', function () {
            var $this = $(this),
                tr = $(this).closest('tr').get(0);

            if (datatable.fnIsOpen(tr)) {
                $this.removeClass('fa-minus-square').addClass('fa-plus-square');
                datatable.fnClose(tr);
            } else {
                $this.removeClass('fa-plus-square').addClass('fa-minus-square');
                datatable.fnOpen(tr, fnFormatDetails(datatable, tr), 'details');
            }
        });
    };

    $(function () {
        datatableInit();
    });

}).apply(this, [jQuery]);

(function ($) {
    $('#datatable').on("click", '#modify-button', function () {
        $.LoadingOverlay("show");
        var id = $(this).data("id");
        $.ajax({
            url: "/Forms/StanowiskoDOWForm/Create",
            data: { id: id },
            success: function (response) {
                $.LoadingOverlay("hide");
                $("#modal-content").html(response);
                $("#modal").modal("show");
            }
        });
    });
    $('#datatable').on("click", '#delete-button', function () {
        $.LoadingOverlay("show");
        var id = $(this).data("id");
        $.ajax({
            url: "/Forms/StanowiskoDOWForm/Delete",
            data: { id: id },
            success: function (response) {
                $.LoadingOverlay("hide");
                $("#modal-content").html(response);
                $("#modal").modal("show");
            }
        });
    });
    $('#datatable').on("click", '#clone-button', function () {
        $.LoadingOverlay("show");
        var id = $(this).data("id");
        $.ajax({
            url: "/Forms/StanowiskoDOWForm/Clone",
            data: { id: id },
            success: function (response) {
                $.LoadingOverlay("hide");
                $("#modal-content").html(response);
                $("#modal").modal("show");
            }
        });
    });

}).apply(this, [jQuery]);