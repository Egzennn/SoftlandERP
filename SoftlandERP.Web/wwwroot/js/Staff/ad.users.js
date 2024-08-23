(function ($) {
    var datatableInit = function () {
        $('#adusers-datatable').dataTable({
            buttons: [
                {
                    text: '<i class="fa-solid fa-plus"></i> Dodawanie użytkownika',
                    action: function () {
                        $.LoadingOverlay("show");
                        window.location.href = '/Staff/AD/CreateUser';
                        $.LoadingOverlay("hide");
                    }
                },
                {
                    text: '<i class="fa-solid fa-table"></i> Excel',
                    action: function () {
                        $.LoadingOverlay("show");
                        window.location.href = '/Staff/AD/ExportExcel';
                        $.LoadingOverlay("hide");
                    }
                },
                {
                    text: '<i class="fa-solid fa-table-list"></i> Dodatkowe formularze',
                    action: function () {
                        $.LoadingOverlay("show");
                        $.ajax({
                            url: "/Staff/AD/Forms",
                            success: function (response) {
                                $.LoadingOverlay("hide");
                                $("#modal-content").html(response);
                                $('#formsTree').jstree({
                                    'core': { 'themes': { 'responsive': false } },
                                    'types': { 'default': { 'icon': 'fas fa-folder' }, 'file': { 'icon': 'fas fa-file' } },
                                    'plugins': ['types']
                                }).on("activate_node.jstree", function (e, data) { window.location.href = data.node.a_attr.href; });
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
                searchPlaceholder: "Wyszukaj...",
                emptyTable: "Brak danych"
            },
            dom: "<'row'<'col col'B><'col col'f>>" + "<'row'<'col'tr>>" + "<'row'<'col col'i><'col col'p>>",
        });

        $('#adusers-datatable_wrapper').find('.btn-secondary').removeClass('btn-secondary').addClass('btn-outline-danger').addClass('mb-3');
        $('#adusers-datatable_filter').find('.pull-right').removeClass('pull-right').addClass('form-control-sm').addClass('mb-3');
    };

    $(function () {
        datatableInit();
    });
}).apply(this, [jQuery]);

(function ($) {
    $('#adusers-datatable').on("click", '#show-button', function () {
        $.LoadingOverlay("show");
        var id = $(this).data("id");
        window.location.href = '/Staff/AD/CreateUser/' + id + '?option=show';
        $.LoadingOverlay("hide");
    });

    $('#adusers-datatable').on("click", '#modify-button', function () {
        $.LoadingOverlay("show");
        var id = $(this).data("id");
        window.location.href = '/Staff/AD/CreateUser/' + id;
        $.LoadingOverlay("hide");
    });

    $('#adusers-datatable').on("click", '#password-button', function () {
        $.LoadingOverlay("show");
        var id = $(this).data("id");
        $.ajax({
            url: "/Staff/AD/ResetPassword",
            data: { id: id },
            success: function (response) {
                $.LoadingOverlay("hide");
                $("#modal-content").html(response);
                $("#modal").modal("show");
            }
        });
    });

    $('#adusers-datatable').on("click", '#status-button', function () {
        $.LoadingOverlay("show");
        var id = $(this).data("id");
        $.ajax({
            url: "/Staff/AD/ChangeStatus",
            data: { id: id },
            success: function (response) {
                $.LoadingOverlay("hide");
                $("#modal-content").html(response);
                $("#modal").modal("show");
            }
        });
    });
}).apply(this, [jQuery]);