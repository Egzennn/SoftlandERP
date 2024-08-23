(function ($) {
    var datatableInit = function () {
        $('#view-datatable').dataTable({
            buttons: [
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
                                window.location.href = window.location.pathname + '/ExportExcel';
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

        $('#view-datatable_wrapper').find('.btn-secondary').removeClass('btn-secondary').addClass('btn-outline-danger').addClass('mb-3');
        $('#view-datatable_filter').find('.pull-right').removeClass('pull-right').addClass('form-control-sm').addClass('mb-3');
    };

    $(function () {
        datatableInit();
    });
}).apply(this, [jQuery]);