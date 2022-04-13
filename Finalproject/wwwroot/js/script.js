$(document).ready(function () {
   

    $('.nav-link').click(function () {
        console.log("Clicked");
        $('.nav-link.active').removeClass('active');
        $('nav-link').addClass('active');
    });

    // $('.dropendd').click(function() {
    //     console.log("Clicked");
    //     $('.submenu-1.active').removeClass('active');
    //     $(".submenu-1").addClass('active');
    // });
    window.addEventListener("scroll", function () {
        var nav = document.querySelector("nav");
        nav.classList.toggle("fixed-top", window.scrollY > 58);
    })


    window.addEventListener("scroll", function () {
        var nav = document.getElementsByClassName("top_button")[0];
        nav.classList.toggle("fixedd-top", window.scrollY > 20);



    })
    setTimeout(function () {
        $(".loader-wrapper").remove();
    }, 1000);

    const deadline = '2022-05-06';

    function getTimeRemaining(endtime) {
        const t = Date.parse(endtime) - Date.parse(new Date()),
            days = Math.floor(t / (1000 * 60 * 60 * 24)),
            hours = Math.floor((t / (1000 * 60 * 60) % 24)),
            minutes = Math.floor((t / 1000 / 60) % 60),
            seconds = Math.floor((t / 1000) % 60);

        return {
            'total': t,
            'days': days,
            'hours': hours,
            'minutes': minutes,
            'seconds': seconds
        };
    }

    function getZero(num) {
        if (num >= 0 && num < 10) {
            return `0${num}`;
        } else {
            return num;
        }
    }

    function setClock(selector, endtime) {
        const timer = document.querySelector(selector),
            days = timer.querySelector('#days'),
            hours = timer.querySelector('#hours'),
            minutes = timer.querySelector('#minutes'),
            seconds = timer.querySelector('#seconds'),
            timeInterval = setInterval(updateClock, 1000);

        updateClock();

        function updateClock() {
            const t = getTimeRemaining(endtime);

            days.innerHTML = getZero(t.days);
            hours.innerHTML = getZero(t.hours);
            minutes.innerHTML = getZero(t.minutes);
            seconds.innerHTML = getZero(t.seconds);

            if (t.total <= 0) {
                clearInterval(timeInterval);
            }
        }
    }

    setClock('.timer', deadline);











});

