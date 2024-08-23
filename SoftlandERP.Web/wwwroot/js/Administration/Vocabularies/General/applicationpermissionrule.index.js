(function ($) {
    var datatableInit = function () {
        $('#datatable').dataTable({
            buttons: [
                {
                    text: '<i class="fa-solid fa-plus"></i> Dodawanie uprawnień',
                    action: function () {
                        $.LoadingOverlay("show");
                        $.ajax({
                            url: "/Administration/GeneralApplicationPermissionsRule/Create",
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
            }],
        });

        $('#datatable_wrapper').find('.btn-secondary').removeClass('btn-secondary').addClass('btn-outline-danger').addClass('mb-3');
        $('#datatable_filter').find('.pull-right').removeClass('pull-right').addClass('form-control-sm').addClass('mb-3');
    };

    $(function () {
        datatableInit();
    });
}).apply(this, [jQuery]);

(function ($) {
    $('#datatable').on("click", '#delete-button', function () {
        $.LoadingOverlay("show");
        var id = $(this).data("id");
        $.ajax({
            url: "/Administration/GeneralApplicationPermissionsRule/Delete",
            data: { id: id },
            success: function (response) {
                $.LoadingOverlay("hide");
                $("#modal-content").html(response);
                $("#modal").modal("show");
            }
        });
    });
}).apply(this, [jQuery]);