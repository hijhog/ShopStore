$(document).ready(function () {
    var bgColors = ["","bg-info", "bg-success", "bg-danger"];
    table = $('#table').DataTable(
        {
            ajax: {
                url: "Order/GetOrders",
                type: "GET",
                dataType: "json"
            },
            columns: [
                {
                    data: null,
                    render: function (data, type, row, meta) {
                        return meta.row + meta.settings._iDisplayStart + 1;
                    }
                },
                { data: "productName" },
                { data: "fullName" },
                { data: "quantity" },
                { data: "totalSum" },
                {
                    data: "statusText",
                    render: function (data, type, row, meta) {
                        return '<span class="rounded-lg p-1 text-white ' + bgColors[row.status] + '">' + data + '</span>';
                    }
                },
                {
                    data: null,
                    defaultContent: "<button class='btn btn-primary btn-sm' onclick='openModal(this)'>Change status</button>"
                },
                {
                    data: null,
                    defaultContent: "<button class='btn btn-primary btn-sm' onclick='removeOrder(this)'>Remove</button>"
                }
            ],
            columnDefs: [
                {
                    orderable: false,
                    searchable: false,
                    targets: 0
                },
                {
                    orderable: false,
                    searchable: false,
                    targets: [6, 7]
                }
            ]
        }
    );

    t.on('order.dt search.dt', function () {
        t.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
            cell.innerHTML = i + 1;
        });
    }).draw();
});

function openModal(self) {
    window.order = table.row($(self).parents('tr')).data();
    $('#staticBackdrop').modal('show');

    $('#prod_name').html(order.productName);
    $('#user_name').html(order.fullName);
    $('#quantity').html(order.quantity);
    $('#status').val(order.status);
}

function changeStatus(data) {
    $.ajax({
        url: 'Order/ChangeOrderStatus',
        type: 'POST',
        data: 'productId=' + data.productId + '&userId=' + data.userId + '&status=' + $('#status').val(),
        success: function (result) {
            if (result.successed) {
                toastr.success('Order status is changed');
                $('#table').DataTable().ajax.reload();
                $('#staticBackdrop').modal('hide');
            } else {
                toastr.error(result.description);
            }
        },
        error: function () {
            toastr.error('Error');
        }
    });
}

function removeOrder(self) {
    var data = table.row($(self).parents('tr')).data();
    $.ajax({
        url: 'Order/RemoveOrder',
        type: 'GET',
        data: 'productId=' + data.productId + '&userId=' + data.userId,
        success: function (result) {
            if (result.successed) {
                toastr.success('The order is deleted');
                $('#table').DataTable().ajax.reload();
                $('#staticBackdrop').modal('hide');
            } else {
                toastr.error(result.description);
            }
        },
        error: function () {
            toastr.error('Error');
        }
    });
}