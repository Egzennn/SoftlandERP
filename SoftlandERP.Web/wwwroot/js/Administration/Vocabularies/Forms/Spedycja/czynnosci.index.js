(function ($) {
    var datatableInit = function () {
        var $table = $('#datatable');
        var table = $table.dataTable({
            buttons: [
                {
                    text: '<i class="fa-solid fa-user-plus"></i> Dodanie wpisu',
                    attr: {
                        id: "addDataTableButton"
                    },
                    action: function () {
                        $.LoadingOverlay("show");
                        $.ajax({
                            url: "/Administration/FormsSpedycjaCzynnosci/Create",
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
                                window.location.href = '/Administration/FormsSpedycjaCzynnosci/ExportExcel';
                            }
                        },
                        {
                            extend: 'excel',
                            text: 'Aktualny widok',
                        }
                    ]
                }
            ],
            lengthChange: false,
            bInfo: false,
            columnDefs: [
                {
                    orderable: false,
                    targets: [-1]
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
                sInfo: "Pokazujesz od _START_ do _END_ z _TOTAL_ rekordów"
            },
            dom: "<'row'<'col-sm-12 col-md-6'B><'col-sm-12 col-md-6'f>>" + "<'row'<'col-sm-12 col-md-6'l>>" + "<'row'<'table-responsive't>>" + "<'row'<'col-sm-12 col-md-5'i><'col-sm-12 col-md-7'p>>",

            aoColumnDefs: [{
                bSortable: false,
                aTargets: [0]
            }]
        });

        $('#datatable_wrapper').find('.btn-secondary').removeClass('btn-secondary').addClass('btn-outline-danger').addClass('mb-3');
        $('#datatable_filter').find('.pull-right').removeClass('pull-right').addClass('form-control-sm').addClass('mb-3');

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
            url: "/Administration/FormsSpedycjaCzynnosci/Create",
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
            url: "/Administration/FormsSpedycjaCzynnosci/Delete",
            data: { id: id },
            success: function (response) {
                $.LoadingOverlay("hide");
                $("#modal-content").html(response);
                $("#modal").modal("show");
            }
        });
    });

}).apply(this, [jQuery]);