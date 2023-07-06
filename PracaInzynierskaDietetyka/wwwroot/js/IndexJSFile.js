var dishdata = [];
const a = new Date();
const options = { weekday: 'long', year: 'numeric', month: 'long', day: 'numeric' };
var rest;

firstset();

var today = new Date();
var curropened = new Date(today);
var cnt = 0;

$('#kcalchange').on("keyup", function (e) {
    if (e.keyCode === 13) {
        $.post({
            url: 'Home/ChangePersonData',
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
            url: 'Home/ChangePersonData',
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
            url: 'Home/ChangePersonData',
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
            url: 'Home/ChangePersonData',
            data: { email: $("#name").attr("data-value"), datatype: "carb", data: $('#carbchange').val() },
            success: function (response) {
                alert("Dane Zmienone");
                $("#carb").text($('#carbchange').val());
            },
            error: function (responce) { alert(responce.data) }
        });
    }
});

function countmacro() {
    let protein = 0, kcal = 0, fat = 0, carbon = 0;
    let wholeprotein = 0, wholekcal = 0, wholefat = 0, wholecarbon = 0;
    $(".grid").each(function () {
        $(".gridbody", this).each(function () {
            kcal += parseFloat($("span", this).attr("data-kcal"));
            protein += parseFloat($("span", this).attr("data-protein"));
            fat += parseFloat($("span", this).attr("data-fat"));
            carbon += parseFloat($("span", this).attr("data-carbon"));
        });
        $()
        $("#sum", this).text("Kcal: " + kcal + " | P: " + protein + " | F: " + fat + " | C: " + carbon);
        wholekcal += kcal;
        wholeprotein += protein;
        wholefat += fat;
        wholecarbon += carbon;
        protein = 0, kcal = 0, fat = 0, carbon = 0;
    });
    $("#kcalnow").text(wholekcal);
    $("#prot").text(wholeprotein);
    $("#fatt").text(wholefat);
    $("#carb").text(wholecarbon);


    var x = 0;
    x = (parseFloat(wholekcal) / parseFloat($("#kcalwant").text())) * 100;
    if (x > 100) {
        $("#kcalbar").css("width", "100%");
        $("#kcalbar").css("background-color", "red");
    }
    else {
        $("#kcalbar").css("width", x + "%");
        $("#kcalbar").css("background-color", "green");
    }

    x = (parseFloat(wholeprotein) / parseFloat($("#protwant").text())) * 100;
    if (x > 100) {
        $("#protbar").css("width", "100%");
        $("#protbar").css("background-color", "red");
    }
    else {
        $("#protbar").css("width", x + "%");
        $("#protbar").css("background-color", "green");
    }

    x = (parseFloat(wholefat) / parseFloat($("#fattwant").text())) * 100;
    if (x > 100) {
        $("#fattbar").css("width", "100%");
        $("#fattbar").css("background-color", "red");
    }
    else {
        $("#fattbar").css("width", x + "%");
        $("#fattbar").css("background-color", "green");
    }

    x = (parseFloat(wholecarbon) / parseFloat($("#carbwant").text())) * 100;
    if (x > 100) {
        $("#carbbar").css("width", "100%");
        $("#carbbar").css("background-color", "red");
    }
    else {
        $("#carbbar").css("width", x + "%");
        $("#carbbar").css("background-color", "green");
    }
}

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

function getDiet() {
    if ($("#userchosen") == null) {
        $.get({
            url: 'Home/getDiet',
            data: { date: curropened.toISOString().slice(0, 10) },
            success: function (response) {
                $('#partial').html(response);
                countmacro();
                $(".gridbody").hide();
            },
            error: function (responce) { alert(responce.data) }
        });
    }
    else {
        $.get({
            url: 'Home/getUserDiet',
            data: { date: curropened.toISOString().slice(0, 10), email: $("#userchosen").attr("data-choose") },
            success: function (response) {
                $('#partial').html(response);
                countmacro();
                $(".gridbody").hide();
            },
            error: function (responce) { alert(responce.data) }
        });
    }
}

function InsertDish(temp) {
    if ($("#userchosen") == null) {
        $.post({
            url: 'Home/InsertDiet',
            data: { date: curropened.toISOString().slice(0, 10), dish_typeID: $("#dish_name").attr("data-dishtypeid"), dishID: $(temp).attr("data-dishid"), weight: $("#weight").val() },
            success: function (response) {
                $('#partial').html(response);
                countmacro();
            },
            error: function (responce) { alert(responce.data) }
        });
        $(".search").hide();
        $(".dishDetail").hide();
    }
    else {
        $.post({
            url: 'Home/InsertDietDietetyk',
            data: { date: curropened.toISOString().slice(0, 10), dish_typeID: $("#dish_name").attr("data-dishtypeid"), dishID: $(temp).attr("data-dishid"), weight: $("#weight").val(), email: $("#userchosen").attr("data-choose") },
            success: function (response) {
                $('#partial').html(response);
                countmacro();
            },
            error: function (responce) { alert(responce.data) }
        });
        $(".search").hide();
        $(".dishDetail").hide();
    }
}

function DeleteDish(obj) {
    if ($("#userchosen") == null) {
        if (confirm("Do u want to delete dish ?")) {
            $.post({
                url: 'Home/DeleteDish',
                data: { date: curropened.toISOString().slice(0, 10), dish_typeID: $(obj).attr("data-dishID"), dishID: $(obj).attr("data-dishID"), conID: $(obj).attr("data-conID") },
                success: function (response) {
                    $('#partial').html(response);
                    countmacro();
                },
                error: function (responce) { alert(responce.data) }
            });
        }
    }
    else {
        if (confirm("Do u want to delete dish ?")) {
            $.post({
                url: 'Home/DeleteDietDietetyk',
                data: { date: curropened.toISOString().slice(0, 10), dish_typeID: $(obj).attr("data-dishID"), dishID: $(obj).attr("data-dishID"), conID: $(obj).attr("data-conID"), email: $("#userchosen").attr("data-choose") },
                success: function (response) {
                    $('#partial').html(response);
                    countmacro();
                },
                error: function (responce) { alert(responce.data) }
            });
        }
    }
}

function dishDetails(i) {
    $(".dishDetail_list").empty();
    $(".dishDetail").show("slow");
    $(".dishDetail_list").append('<li><p>' + dishdata[i].name + '</p> <span> Kcal:' + dishdata[i].kcal + ' | </span><span>B:' + dishdata[i].protein + ' | </span> <span>T:' + dishdata[i].fat + ' | </span> <span>C:' + dishdata[i].carbon + ' | </span></li><input id="weight"/><button data-dishid="'+dishdata[i].id+'" onClick="InsertDish(this)">Add</button>');
}

function createlist(temp) {
    for (var i = 0; i < dishdata.length; i++) {
        $(temp).append('<li onclick="dishDetails('+i+')"><p>' + dishdata[i].name + '</p> <span> Kcal:' + dishdata[i].kcal + ' | </span><span>B:' + dishdata[i].protein + ' | </span> <span>T:' + dishdata[i].fat + ' | </span> <span>C:' + dishdata[i].carbon + ' | </span></li>');
    }
}

$(document).on("click", ".adder", function () {
    $("#adder").attr("data-status", "false");
    $(".search").show("slow");
    $("#dish_name").attr("data-dishtypeID", $(this).attr("data-dishtypeID"));
    $("#dish_name").val("");
    $(".dish_list").empty();
});

$(document).on("input", "#dish_name", function () {
    $(".dish_list").empty();
    if ($("#dish_name").val().length >= 3) {
        $.ajax({
            url: 'Home/getDishes',
            data: { name: $(this).val() },
            success: function (response) {
                dishdata = [];
                for (let dish of response) {
                    dishdata.push(dish);
                }
                createlist($(".dish_list"));
            },
            error: function (responce) { alert(responce.data) }
        });
    }
});

$(document).on("click", "#backbtnsearch", function () {
    $(".search").hide("slow");
    $("#adder").attr("data-status", "true");
});

$(document).on("click", ".dishDetailBackBtn", function () {
    $(".dishDetail").hide("slow");
    $("#adder").attr("data-status", "true");
});

$(document).on("click", ".delete", function () {
    DeleteDish($(this));
});

$(document).ready(function () {
    $("#month").attr("data-curropened", today.toISOString().slice(0, 10));
    $("#adder").attr("data-status", "true");
    $(".search").hide();
    $(".dishDetail").hide();
    $(".data").each(function () {
        if ($(this).is(":visible")) {
            $(this).hide();
        }
    });

    $(".search").each(function () {
        if ($(this).is(":visible")) {
            $(this).hide();
        }
    });
    countmacro();
    $(".gridbody", this).hide();

    // od tad
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

$(".Userscontainer ul li").each(function (e) {
    $(this).on("click", function (e) {
        if ($(e.target).is("button")) {
            $('#name').attr('data-value', $("button", this).attr("data-email"));
            $.get({
                url: 'Home/UserDataDetails',
                data: { email: $("button", this).attr("data-email") },
                success: function (response) {

                    $("#name").text(response.first_Name + " " + response.last_Name);
                    $("#sex").text(response.sex);
                    $("#age").text(response.age);
                    $("#height").text(response.height);
                    $("#person_weight").text(response.weight);
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
            if ($(e.target).is("li") || $(e.target).is("span")) {
                $.get({
                    url: 'Home/getUserDiet',
                    data: { date: curropened.toISOString().slice(0, 10), email: $(this).attr("data-value") },
                    success: function (response) {
                        $('#partial').html(response);
                        countmacro();
                        $(".gridbody").hide();
                    },
                    error: function (responce) { alert(responce.data) }
                });
                $('#nav-icon1').toggleClass('open');
                $(".Userscontainer").hide("slow");
            }
        }
    });
});

$("#AboutUserViewBackbtn").on("click", function () {
    $("#AboutUserView").hide("slow");
});


$('#addnewpersonbtn').on("click", function () {
    if ($("#addnewpersoninput").val() != null) {
        $.post({
            url: 'Home/AddNewPersonToDietetyk',
            data: { email: $("#addnewpersoninput").val() },
            success: function (response) { location.reload() },
            error: function (responce) { alert(responce.data) }
        });
    }
    else {
        alert("Email is empty");
    }
});
//do tad

$(document).on("click", ".grid", function (e) {
    if ($(e.target).is("button") || $(e.target).is("input")) {

    }
    else {
        $(this).each(function () {
            if ($(".gridbody", this).is(":visible")) {
                $(".gridbody", this).hide("slow");
            }
            else {
                $(".gridbody", this).show("slow");
            }
        });
    }
});

//old

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
    getDiet();
});


//swipes
let start = { x: undefined, y: undefined }, end = { x: undefined, y: undefined };
$("#partial").on('mousedown', function (event) {
    start = { x: event.clientX, y: event.clientY };
});

$("#partial").on('mouseup', function (event) {
    end = { x: event.clientX, y: event.clientY };
    swipemousepartial();
});

function swipemousepartial() {
    if ($("#adder").attr("data-status") == "true") {
        if ((Math.abs(`${start.x}`) - Math.abs(`${end.x}`)) < 0) {
            curropened.setDate(curropened.getDate() - parseInt(1));
            var last = new Date($("#mon").attr("data-date"));
            getDiet();
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
            getDiet();
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
$("#partial").on('touchstart', function (event) {
    var touchobj = e.changedTouches[0];
    start = { x: parseInt(touchobj.clientX), y: parseInt(touchobj.clientY) };
});

$("#partial").on('touchend', function (event) {
    var touchobj = e.changedTouches[0];
    end = { x: parseInt(touchobj.clientX), y: parseInt(touchobj.clientY) };
    swipemousepartial();
});

document.getElementById("partial").addEventListener('touchstart', handleTouchStart, false);
document.getElementById("partial").addEventListener('touchmove', handleTouchMove, false);

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

        if ($("#adder").attr("data-status") == "true") {
            if (xDiff > 0) {
                curropened.setDate(curropened.getDate() + parseInt(1));
                var last = new Date($("#sun").attr("data-date"));
                getDiet();
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
                getDiet();
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