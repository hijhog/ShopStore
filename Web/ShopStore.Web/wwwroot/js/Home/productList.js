﻿function getProductList(categoryId) {
    $.ajax({
        url: 'Home/GetProductsByCategory',
        type: 'GET',
        data: 'categoryId=' + categoryId,
        success: function (response) {
            var root = $('#prodList');
            if (response.products.length != 0) {
                $(root).empty();
                response.products.forEach(function (item) {
                    var html = '<div class="card mx-2 mb-2" style="width: 16rem;">' +
                        '<img class="card-img-top cropped" src="data:image/jpeg;base64,' + item.image + '" />' +
                        '<div class="card-body">' +
                        '<h5 class="card-title">' + item.name + '</h5>' +
                        '<div class="box"><p class="card-text">' + item.description + '</p></div>' +
                        '<button class="btn btn-primary stretched-link" onclick="addProductToCart(\'' + item.id + '\')">Add To Cart</button></div></div>';
                    $(html).appendTo(root);
                });
            } else {
                $(root).empty();
            }
        },
        error: function () {

        }
    });
}

function addProductToCart(productId) {
    $.ajax({
        url: '/api/Cart/AddProductToCart',
        type: 'GET',
        data: 'productId=' + productId,
        success: function (response) {
            if (response.successed) {
                updateCart();
                toastr.success('Product added to cart');
            } else {
                toastr.error('Error adding product');
            }
        },
        error: function () {
            window.location.href = "Account/Login";
        }
    });
}

function updateCart() {
    $.ajax({
        url: '/api/Cart/GetProductCount',
        type: 'GET',
        success: function (response) {
            $('#prod_count').html(response.count);
        },
        error: function () {
            toastr.error('Failed to update the number of items in the cart');
        }
    })
}