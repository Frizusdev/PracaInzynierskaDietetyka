$("#addDish").click(function () {
	$.ajax({
		url: 'CheckIfDishNameAlreadyExist',
		method: 'get',
		data: {
			name: $('#dishname').val()
		}
	}).done(function (result) {
		if (result) {
			$('#disherror').show("slow");
		} else {
			$('#disherror').hide("slow");
			$('#dishform').submit();
		}
	}).fail(function () {
		console.log("error dish form");
	});
});

$("#addExercise").on("click", function () {
	$.ajax({
		url: 'CheckIfExerciseNameAlreadyExist',
		method: 'get',
		data: {
			name: $('#exercisename').val()
		}
	}).done(function (result) {
		alert(result);
		if (result) {
			$('#exerciseerror').show("slow");
		} else {
			$('#exerciseerror').hide("slow");
			$('#exerciseform').submit();
		}
	}).fail(function () {
		console.log("error exercise form");
	});
});

$(".textarea").each(function () {
	this.setAttribute("style", "height:" + (this.scrollHeight) + "px;overflow-y:hidden;");
}).on("input", function () {
	this.style.height = 0;
	this.style.height = (this.scrollHeight) + "px";
});


$("#showdishgrid").on("click", function () {
	if ($(".workgrid").is(":visible")) {
		$(".workgrid").hide("slow");
		$(".dishgrid").show("slow");
	}
});

$("#showexercisegrid").on("click", function () {
	if ($(".dishgrid").is(":visible")) {
		$(".dishgrid").hide("slow");
		$(".workgrid").show("slow");
	}
});

$(document).ready(function () {
	$(".workgrid").hide();
});
