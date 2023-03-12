

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


