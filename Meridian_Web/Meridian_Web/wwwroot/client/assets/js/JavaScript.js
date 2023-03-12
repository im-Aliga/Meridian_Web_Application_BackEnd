//clock
function clock() {
    let now = new Date();
    let h = now.getHours();
    let m = now.getMinutes();
    let s = now.getSeconds();

    $('#js-clock').html(h + ":" + m + ":" + s);
}

setInterval(clock, 1000);

let walk = 390;
let exercise = 13;
let stand = 3;


// counter animation function
function counter(el, data) {
    //delay animation for clock to load
    el.prop('counter', 0).delay(1000).animate({
        counter: data
    }, {
        duration: 700,
        easing: 'swing',
        step: function () {
            $(this).html(Math.ceil(this.counter));
        },
        complete: function () {
            $(this).html(data);
        }
    });
}
//counter for walk, exercise and stand
counter($('#js-walk'), walk);
counter($('#js-exercise'), exercise);
counter($('#js-stand'), stand);



