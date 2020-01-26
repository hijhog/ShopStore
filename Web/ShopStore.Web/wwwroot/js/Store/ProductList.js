$(document).ready(function () {
    table = $('#table').DataTable(
        {
            ajax: {
                url: "/Store/GetStoreProducts?storeId="+storeId,
                type: "GET",
                dataType: "json"
            },
            columns: [
                { "data": "productId" },
                { "data": "name" },
                { "data": "description" },
                { "data": "category" },
                { "data": "price" },
                { "data": "productCount" },
                {
                    "data": null,
                    defaultContent: "<img onclick='openProduct(this)' class='custom_button' src='../../img/plus.png' height='16' width='16'/>"
                },
                {
                    "data": null,
                    defaultContent: "<img onclick='removeProduct(this)' class='custom_button' src='../../img/bin.png' height='16' width='16'/>"
                }
            ],
            columnDefs: [
                { "orderable": false, targets: [6, 7] }
            ]
        }
    );
});

function addProduct(data) {
    $.ajax({
        url: '/Store/AddProduct',
        type: 'POST',
        data: 'storeId=' + storeId + '&productId=' + data.productId + '&productCount=' + $('#ProductCount').val(),
        success: function (result) {
            if (result.successed) {
                toastr.success('The product was added to this store');
                $('#table').DataTable().ajax.reload();
                $('#exampleModalCenter').modal('hide');
            } else {
                toastr.error(result.description);
            }
        },
        error: function () {
            toastr.error('Error');
        }
    });
}

function openProduct(self) {
    window.product = table.row($(self).parents('tr')).data();
    $('#exampleModalCenter').modal('show');

    $('#prod_name').html(product.name);
    $('#prod_desc').html(product.description);
    $('#prod_category').html(product.category);
    $('#prod_price').html('$' + product.price);
};

function removeProduct(self) {
    var data = table.row($(self).parents('tr')).data();
    $.ajax({
        url: '/Store/RemoveProduct',
        type: 'GET',
        data: 'storeId=' + storeId + '&prodId=' + data.productId,
        success: function (result) {
            if (result.successed) {
                toastr.success('The product was deleted from this store');
                $('#table').DataTable().ajax.reload();
                $('#exampleModalCenter').modal('hide');
            } else {
                toastr.error(result.description);
            }
        },
        error: function () {
            toastr.error('Error');
        }
    });
}
