$(function () {
    $.ajax({
        url: 'api/Cart/GetCartProducts',
        type: 'GET',
        success: function (response) {
            if (response.cart.length != 0) {
                var body = $('#cart_table_body');
                var totalSum = 0;
                var html = '';
                response.cart.forEach(function (item) {
                    var imageHtml = item.productImage != null ? '<img style="height: 60px; " src="data:image/jpeg;base64,' + item.productImage + '" />' : ' ';
                    html += '<tr><td>' + imageHtml +
                        '</td><td>' + item.productName +
                        '</td><td>' + item.quantity +
                        '</td><td>$ ' + item.price +
                        '</td><td><span class="custom_button" onclick="removeProduct(this, \'' + item.productId + '\')">Remove From Cart</span></td></tr>';
                    totalSum += item.price;
                });
                $(html).appendTo(body);
                $('#cart_totalSum').html('$ ' + totalSum.toFixed(2));
            }
        },
        error: function () {
            toastr.error('Error');
        }
    });
});

function removeProduct(self, productId) {
    $.ajax({
        url: 'api/Cart/RemoveProduct',
        type: 'GET',
        data: 'productId=' + productId,
        success: function (response) {
            if (response.successed) {
                window.td = self;
                var row = self.parentNode.parentNode;
                row.remove();
                checkCart();
            } else {
                toastr.error(responce.description);
            }
        },
        error: function () {
            toastr.error('Error');
        }
    })
}

function checkCart() {
    var table = document.getElementById('table');
    var totalSum = 0;
    console.log(table);
    if (table.rows.length > 2) {
        for (var i = 1; i < table.rows.length - 1; i++) {
            var text = table.rows[i].cells[3].innerText;
            var price = parseFloat(text.split(' ')[1]);
            totalSum += price;
        }
    }
    $('#prod_count').html(table.rows.length - 2);
    $('#cart_totalSum').html(totalSum.toFixed(2) + ' $');
}