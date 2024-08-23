(function ($) {

    'use strict';

    var datatableInit = function () {
        var $table = $('#datatable');

        // insert the expand/collapse column
        var th = document.createElement('th');
        var td = document.createElement('td');
        td.innerHTML = '<i data-toggle class="far fa-plus-square text-primary h5 m-0" style="cursor: pointer;"></i>';
        td.className = "text-center";

        // initialize
        var datatable = $table.dataTable({

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
                bSortable: true,
                aTargets: [0]
            }],
            aaSorting: [
                [1, 'dsc']
            ]
        });

        $('#datatable_wrapper').find('.btn-secondary').removeClass('btn-secondary').addClass('btn-outline-danger').addClass('mb-3');
        $('#datatable_filter').find('.pull-right').removeClass('pull-right').addClass('form-control-sm').addClass('mb-3');

    };

    $(function () {
        datatableInit();
    });

}).apply(this, [jQuery]);