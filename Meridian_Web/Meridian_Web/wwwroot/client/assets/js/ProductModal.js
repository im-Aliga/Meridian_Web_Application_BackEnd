﻿

$(document).on("click", ".show-product-modal", function (e) {
    console.log("Hello 1");

    e.preventDefault();

    
    var url = e.target.href;
    if (!url) {
        var closestLink = $(e.target).closest('a');
        url = closestLink.attr('href');
    }
    console.log(url)


    fetch(url)
        .then(response => response.text())
        .then(data => {
            $('.modal-content').html(data);
            console.log(data)
        })

    

    //$('#quick-look').modal('show');
})

let btns = document.querySelectorAll(".add-product-to-basket-btn")

btns.forEach(x => x.addEventListener("click", function (e) {
    console.log("Hello 2");

    var url = e.target.href;
    if (!url) {
        var closestLink = $(e.target).closest('a');
        url = closestLink.attr('href');
    }
    console.log(url)
    fetch(url)
        .then(response => response.text())
        .then(data => {
            console.log(data)
            $('.mini-cart').html(data);
        })
}))

$(document).on("click", ".remove-product-to-basket-btn", function (e) {
    console.log("Hello 1");

    e.preventDefault();
    var url = e.target.href;
    if (!url) {
        var closestLink = $(e.target).closest('a');
        url = close
    }
    console.log(url)

    fetch(url)
        .then(response => response.text())
        .then(data => {
            $('.mini-cart').html(data);
    console.log(data)
        })
})

$(document).on("click", ".add-product-to-basket-btn", function (e) {
  
    e.preventDefault();
    var url = e.target.href;
    if (!url) {
        var closestLink = $(e.target).closest('a');
        url = closestLink.attr('href');
    }
    console.log(url)
    fetch(url)
        .then(response => response.text())
        .then(data => {
            console.log(data)
            $('.mini-cart').html(data);
        })
})

$(document).on("click", ".add-product-to-wishlist-btn", function (e) {

    e.preventDefault();
    var url = e.target.href;
    if (!url) {
        var closestLink = $(e.target).closest('a');
        url = closestLink.attr('href');
    }
    console.log(url)
    fetch(url)
        .then(response => response.text())
        .then(data => {
            console.log(data)
            $('.wishlist-cart').html(data);
        })
})



$(document).on("click", ".plus-btn", function (e) {
    console.log("Hello 1");
    e.preventDefault();
    var url = e.target.href;
    if (!url) {
        var closestLink = $(e.target).closest('a');
        url = closestLink.attr('href');
    }
    console.log(url)
    fetch(url)
        .then(response => response.text())
        .then(data => {
            $('.cartPage').html(data);

            fetch(e.target.parentElement.nextElementSibling.href)
                .then(response => response.text())
                .then(data => {
                    $('.mini-cart').html(data);
                })
        })
})

$(document).on("click", ".minus-btn", function (e) {
    console.log("Hello 2");
    e.preventDefault()
    var url = e.target.href;
    if (!url) {
        var closestLink = $(e.target).closest('a');
        url = closestLink.attr('href');
    }

    fetch(url)
        .then(response => response.text())
        .then(data => {
            $('.cartPage').html(data);

            fetch(e.target.parentElement.nextElementSibling.href)
                .then(response => response.text())
                .then(data => {
                    $('.mini-cart').html(data);
                })
        })
})


//$(".searchproductPrice").change(function (e) {
//    e.preventDefault();

//    console.log("salam")

//    let minPrice = e.target;
///*    let MinPrice = parseInt(minPrice);*/
//    console.log(minPrice);

//    //let maxPrice = e.target.parentElement.firstElementChild.children[1].innerText.slice(1);
//    //let MaxPrice = parseInt(maxPrice);

//    //let aHref = document.querySelector(".shoppage-url").href;

//    //$.ajax(
//    //    {
//    //        url: aHref,

//    //        data: {
//    //            MinPrice: MinPrice,
//    //            MaxPrice: MaxPrice
//    //        },

//    //        success: function (response) {
//    //            $('.shopPageProduct').html(response);


//    //        },
//    //        error: function (err) {
//    //            $(".product-details-modal").html(err.responseText);

//    //        }

//    //    });


//});


