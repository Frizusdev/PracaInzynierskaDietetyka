var dishdata = [];
const a = new Date();
const options = { weekday: 'long', year: 'numeric', month: 'long', day: 'numeric' };
var rest;

firstset();

$('#kcalchange').on("keyup", function (e) {
    if (e.keyCode === 13) {
        $.post({
            url: 'ChangePersonData',
            data: { email: $("#name").attr("data-value"), datatype: "kcal", data: $('#kcalchange').val() },
            success: function (response) {
                alert("Dane Zmienone");
                $("#kcal").text($('#kcalchange').val());
            },
            error: function (responce) { alert(responce.data) }
        });
    }
});

$('#protchange').on("keyup", function (e) {
    if (e.keyCode === 13) {
        $.post({
            url: 'ChangePersonData',
            data: { email: $("#name").attr("data-value"), datatype: "prot", data: $('#protchange').val() },
            success: function (response) {
                alert("Dane Zmienone");
                $("#prot").text($('#protchange').val());
            },
            error: function (responce) { alert(responce.data) }
        });
    }
});

$('#fatchange').on("keyup", function (e) {
    if (e.keyCode === 13) {
        $.post({
            url: 'ChangePersonData',
            data: { email: $("#name").attr("data-value"), datatype: "fat", data: $('#fatchange').val() },
            success: function (response) {
                alert("Dane Zmienone");
                $("#fat").text($('#fatchange').val());
            },
            error: function (responce) { alert(responce.data) }
        });
    }
});

$('#carbchange').on("keyup", function (e) {
    if (e.keyCode === 13) {
        $.post({
            url: 'ChangePersonData',
            data: { email: $("#name").attr("data-value"), datatype: "carb", data: $('#carbchange').val() },
            success: function (response) {
                alert("Dane Zmienone");
                $("#carb").text($('#carbchange').val());
            },
            error: function (responce) { alert(responce.data) }
        });
    }
});

$(".Userscontainer ul li").each(function (e) {
    $(this).on("click", function (e) {
        if ($(e.target).is("button")) {
            $('#name').attr('data-value', $("button", this).attr("data-email"));
            $.get({
                url: 'UserDataDetails',
                data: { email: $("button", this).attr("data-email") },
                success: function (response) {

                    $("#name").text(response.first_Name + " " + response.last_Name);
                    $("#sex").text(response.sex);
                    $("#age").text(response.age);
                    $("#height").text(response.height);
                    $("#weight").text(response.weight);
                    $("#wish_weight").text(response.wish_Weight);
                    $("#extras").text(response.extras);
                    $("#kcal").text(Math.round(response.kcal));
                    $("#prot").text(Math.round(response.protein));
                    $("#fat").text(Math.round(response.fat));
                    $("#carb").text(Math.round(response.carbon));
                    $("#AboutUserView").show("slow");
                },
                error: function (responce) { alert(responce.data) }
            });
        }
        else {
            $(".name").attr("data-value", $(this).attr("data-value"));
            GetWorkout();
            setUser($(this).attr("data-value"));
            $(".Userscontainer").hide("slow");
            $("#nav-icon1").removeClass('open');
        }
    });
});

$(document).on("click", "#addwork", function () {
    $("#aboutexercise").show("slow");
    $("#backaboutexercise").on("click", function () {
        $("#aboutexercise").hide("slow");
    });
});

$(document).on("input", "#exercisename", function () {
    var exercises = [];
    if ($(this).val().length >= 3) {
        $.ajax({
            url: 'GetExercises',
            data: { data: $(this).val() },
            success: function (response) {
                for (let exer of response) {
                    exercises.push(exer);
                }
                if (exercises.length > 0) {
                    $("#exercises").show("slow");
                    $("#exercises ul").empty();
                    for (var i = 0; i < exercises.length; i++) {
                        $("#exercises ul").append('<div data-value="' + exercises[i].id + '"><p>' + exercises[i].name + '</p></div>');
                    }
                }
            },
            error: function (responce) { alert(responce.data) }
        });
    }
    else {
        $("#exercises ul").empty();
    }
});

$(document).on("click", ".exercises ul div", function () {
    $(".exercises ul div").css("background-color", "");
    $(this).css("background-color", "grey");
    $("#addexercise").attr("data-id", $(this).attr("data-value"));
});

$(document).on("click", "#addexercise" , function () {
    if ($("#times").val() > 0 && $("#reps").val() > 0 && $("#brake").val() != null && $("#addexercise").attr("data-id") != null) {
        $.post({
            url: 'AddExercise',
            data: { email: $(".name").attr("data-value"), date: curropened.toISOString().slice(0, 10), exercise_id: $("#addexercise").attr("data-id"), reps: $("#reps").val(), times: $("#times").val(), pause: $("#brake").val()},
            success: function (response) {
                $("#partialworkout").html(response);
            },
            error: function (responce) { alert(responce.data) }
        });
    }
});

var today = new Date();
var curropened = new Date(today);
var cnt = 0;
var UserDataDetails = [];

$("#AboutUserViewBackbtn").on("click", function () {
    $("#AboutUserView").hide("slow");
});

$('#addnewpersonbtn').on("click", function () {
    if ($("#addnewpersoninput").val() != null) {
        $.post({
            url: 'AddNewPersonToDietetyk',
            data: { email: $("#addnewpersoninput").val() },
            success: function (response) { location.reload() },
            error: function (responce) { alert(responce.data) }
        });
    }
    else {
        alert("Email is empty");
    }
});

$(document).ready(function () {
    //$('#month').attr("data-id", $('#users li').first().attr('data-value'));
    $(".Userscontainer").hide();
    $('#nav-icon1').click(function () {
        $(this).toggleClass('open');
        if ($(this).hasClass('open')) {
            $(".Userscontainer").show("slow");
        }
        else {
            $(".Userscontainer").hide("slow");
        }
    });
});

function setUser(id) {
    $(".name").attr("data-value", id);
};

function firstset() {
    let today = new Date();
    for (var i = 1; i <= 7; i++) {
        rest = new Date(a.setDate(a.getDate() - a.getDay() + i));
        $(".days", "#date").each(function () {
            if ($(this).attr("data-id") == i) {
                $(this).attr("data-date", rest.toISOString().slice(0, 10));
                $("p", this).text(rest.toISOString().slice(8, 10));
                let str1 = rest.toLocaleDateString(navigator.language, options).slice(0, 3);
                str1 = str1.charAt(0).toUpperCase() + str1.slice(1);
                $("span", this).text(str1);
                if (today.toISOString() == rest.toISOString()) {
                    $(this).addClass("choose");
                }
            }
        });
    }
    $("#month").text(rest.toLocaleString('default', { month: 'long' }));
}

function GetWorkout() {
    if ($(".name") == null) {
        $.get({
            url: 'GetWorkoutGrid',
            data: { date: curropened.toISOString().slice(0, 10), email: "" },
            success: function (response) {
                $('.partial').html(response);
            },
            error: function (responce) { alert(responce.data) }
        });
    }
    else {
        $.get({
            url: 'GetWorkoutGrid',
            data: { date: curropened.toISOString().slice(0, 10), email: $(".name").attr("data-value") },
            success: function (response) {
                $('.partial').html(response);
            },
            error: function (responce) { alert(responce.data) }
        });
    }
}
//

var datestart = { x: undefined, y: undefined }, dateend = { x: undefined, y: undefined };
$("#date").on('mousedown touchstart', function (event) {
    datestart = { x: event.clientX, y: event.clientY };
});

$("#date").on('mouseup touchend', function (event) {
    dateend = { x: event.clientX, y: event.clientY };
    swipemousedate();
});

function swipemousedate() {
    if ((Math.abs(`${datestart.x}`) - Math.abs(`${dateend.x}`)) < 0) {
        back();
    }
    else if ((Math.abs(`${datestart.x}`) - Math.abs(`${dateend.x}`)) > 0) {
        forward();
    }
};

function back() {
    rest = new Date(a.setDate(a.getDate() - a.getDay() - 12));
    for (var i = 1; i <= 7; i++) {
        rest = new Date(a.setDate(a.getDate() - a.getDay() + i));
        $(".days", "#date").each(function () {
            if ($(this).attr("data-id") == i) {
                $(this).attr("data-date", rest.toISOString().slice(0, 10));
                $("p", this).text(rest.toISOString().slice(8, 10));
            }
        });
        $("#month").text(rest.toLocaleString('default', { month: 'long' }));
    }
};

function forward() {
    for (var i = 1; i <= 7; i++) {
        rest = new Date(a.setDate(a.getDate() - a.getDay() + i));
        $(".days", "#date").each(function () {
            if ($(this).attr("data-id") == i) {
                $(this).attr("data-date", rest.toISOString().slice(0, 10));
                $("p", this).text(rest.toISOString().slice(8, 10));
            }
        });
        $("#month").text(rest.toLocaleString('default', { month: 'long' }));
    }
};

$("#date div").on('click', function () {
    $("#date div").removeClass("choose");
    $(this).addClass("choose");
    curropened = new Date($(this).attr("data-date"));
    $("#month").attr("data-curropened", $(this).attr("data-date"));
    GetWorkout();
});


//swipes
let start = { x: undefined, y: undefined }, end = { x: undefined, y: undefined };
$("#partialworkout").on('mousedown', function (event) {
    start = { x: event.clientX, y: event.clientY };
});

$("#partialworkout").on('mouseup', function (event) {
    end = { x: event.clientX, y: event.clientY };
    swipemousepartial();
});

function swipemousepartial() {
    if ($("#addwork").attr("data-status") == "true") {
        if ((Math.abs(`${start.x}`) - Math.abs(`${end.x}`)) < 0) {
            curropened.setDate(curropened.getDate() - parseInt(1));
            var last = new Date($("#mon").attr("data-date"));
            GetWorkout();
            if (curropened < last) {
                back();
                $("#date div").removeClass("choose");
                $("#sun").addClass("choose");
            }
            else {
                var child = document.getElementById("date").querySelector('.choose').dataset.id;
                child -= 1;
                var before = document.getElementById("date").querySelector('div[data-id="' + child + '"]');
                $("#date div").removeClass("choose");
                before.classList.add("choose");
            }
        }
        else if ((Math.abs(`${start.x}`) - Math.abs(`${end.x}`)) > 0) {
            curropened.setDate(curropened.getDate() + parseInt(1));
            var last = new Date($("#sun").attr("data-date"));
            GetWorkout();
            if (curropened > last) {
                forward();
                $("#date div").removeClass("choose");
                $("#mon").addClass("choose");
            }
            else {
                var child = parseInt(document.getElementById("date").querySelector('.choose').dataset.id);
                child += 1;
                var before = document.getElementById("date").querySelector('div[data-id="' + child + '"]');
                $("#date div").removeClass("choose");
                before.classList.add("choose");
            }
        }
    }
};

//mobile
$("#partialworkout").on('touchstart', function (event) {
    var touchobj = e.changedTouches[0];
    start = { x: parseInt(touchobj.clientX), y: parseInt(touchobj.clientY) };
});

$("#partialworkout").on('touchend', function (event) {
    var touchobj = e.changedTouches[0];
    end = { x: parseInt(touchobj.clientX), y: parseInt(touchobj.clientY) };
    swipemousepartial();
});

document.getElementById("partialworkout").addEventListener('touchstart', handleTouchStart, false);
document.getElementById("partialworkout").addEventListener('touchmove', handleTouchMove, false);

var xDown = null;
var yDown = null;

function getTouches(evt) {
    return evt.touches ||
        evt.originalEvent.touches;
}

function handleTouchStart(evt) {
    const firstTouch = getTouches(evt)[0];
    xDown = firstTouch.clientX;
    yDown = firstTouch.clientY;
};

function handleTouchMove(evt) {
    if (!xDown || !yDown) {
        return;
    }

    var xUp = evt.touches[0].clientX;
    var yUp = evt.touches[0].clientY;

    var xDiff = xDown - xUp;
    var yDiff = yDown - yUp;

    if (Math.abs(xDiff) > Math.abs(yDiff)) {

        if ($("#addwork").attr("data-status") == "true") {
            if (xDiff > 0) {
                curropened.setDate(curropened.getDate() + parseInt(1));
                var last = new Date($("#sun").attr("data-date"));
                GetWorkout();
                if (curropened > last) {
                    forward();
                    $("#date div").removeClass("choose");
                    $("#mon").addClass("choose");
                }
                else {
                    var child = parseInt(document.getElementById("date").querySelector('.choose').dataset.id);
                    child += 1;
                    var before = document.getElementById("date").querySelector('div[data-id="' + child + '"]');
                    $("#date div").removeClass("choose");
                    before.classList.add("choose");
                }
            } else {
                curropened.setDate(curropened.getDate() - parseInt(1));
                var last = new Date($("#mon").attr("data-date"));
                GetWorkout();
                if (curropened < last) {
                    back();
                    $("#date div").removeClass("choose");
                    $("#sun").addClass("choose");
                }
                else {
                    var child = document.getElementById("date").querySelector('.choose').dataset.id;
                    child -= 1;
                    var before = document.getElementById("date").querySelector('div[data-id="' + child + '"]');
                    $("#date div").removeClass("choose");
                    before.classList.add("choose");
                }
            }
        }
    }

    xDown = null;
    yDown = null;
};

document.getElementById("date").addEventListener('touchstart', handleTouchStart, false);
document.getElementById("date").addEventListener('touchmove', handledateTouchMove, false);

function handledateTouchMove(evt) {

    if (!xDown || !yDown) {
        return;
    }

    var xUp = evt.touches[0].clientX;
    var yUp = evt.touches[0].clientY;

    var xDiff = xDown - xUp;
    var yDiff = yDown - yUp;

    if (Math.abs(xDiff) > Math.abs(yDiff)) {
        if (xDiff > 0) {
            forward();
        } else {
            back();
        }
    }

    xDown = null;
    yDown = null;
};