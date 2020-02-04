$(function () {
    $.ajax({
        url: 'Home/GetCategories',
        type: 'GET',
        success: function (response) {
            var root = $('#catList');
            var html = '<button type="button" class="list-group-item list-group-item-action" onclick=\'getProductList("00000000-0000-0000-0000-000000000000")\'>All</button>';
            $(html).appendTo(root);
            if (response.categories.length != 0) {
                response.categories.forEach(function (item) {
                    html = '<button type="button" class="list-group-item list-group-item-action" onclick=\'getProductList("' + item.id + '")\'>' + item.name + '</button>';
                    $(html).appendTo(root);
                });
            } else {
                $(root).empty();
            }
        },
        error: function () {

        }
    });
});