function filterProducts() {
    $.ajax({
        url: 'Product/GetFilterProducts',
        type: 'POST',
        data: 'name=' + $('#prodName').val() + '&description=' + $('#prodDesc').val() + '&price=' + $('#prodPrice').val() + '&category=' + $('#prodCategory').val(),
        success: function (response) {
            if (response.data.length != 0) {
                var body = $('#table_body');
                body.empty();
                var html = '';
                response.data.forEach(function (item) {
                    var imageHtml = item.image != null ? '<img style="height: 60px; " src="data:image/jpeg;base64,' + item.image + '" />' : ' ';
                    html += '<tr><td>' + imageHtml +
                        '</td><td>' + item.name +
                        '</td><td>' + item.description +
                        '</td><td>$ ' + item.price.toFixed(2) +
                        '</td><td>' + item.category +
                        '</td><td><a href="Product/Edit?id=' + item.id + '">Edit</a>' +
                        '</td><td><a href="Product/Remove?id=' + item.id + '">Remove</a></td></tr>';
                });
                $(html).appendTo(body);
            } else {
                toastr.error("FUCK");
            }
        },
        error: function () {
            toastr.error("Error");
        }
    });
}