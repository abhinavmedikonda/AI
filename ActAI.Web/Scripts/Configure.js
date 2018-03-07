//window.onload = function () {
//$('.selectpicker').selectpicker();
//};
$("#themesUl").on("click", "li", function () {
    console.log("li.theme");
    if (!this.classList.contains("active")) {
        console.log("inactive");
        $("div#groups li").attr('class', 'groupLi');
        $("div#keywords div.tab-content div").attr('class', 'tab-pane');
    }
});
$("#groups").on("click", "li", function () {
    console.log("li.group");
    if (!this.classList.contains("active")) {
        $("div#keywords div.tab-content div").attr('class', 'tab-pane');
    }
});
$("#addTheme").click(function () {
    $.ajax({
        type: "get",
        url: "/Configure/_AddTheme",
        //data: dataString,
        contentType: "application/json; charset=utf-8",
        datatype: "html",
        success: function (data) {
            $('#modalContent').html(data);
            console.log(document.getElementById("txtAddTheme"));
            document.getElementById("txtAddTheme").click();
            $("#txtAddTheme").select();
        },
        error: function () {
            alert("Content load failed.");
            $('#overlayForm').modal('hide');
        }
    });
    $("#txtAddTheme").focus();
});
$("#addGroup").click(function (event) {
    console.log(1);
    if (typeof $("#themesUl li.active").children().first().attr("id") !== "undefined") {
        console.log(2);
        $.ajax({
            type: "get",
            url: "/Configure/_AddGroup",
            //data: dataString,
            contentType: "application/json; charset=utf-8",
            datatype: "html",
            success: function (data) {
                $('#modalContent').html(data);
                document.getElementById("txtAddGroup").focus();
                $("#txtAddGroup").select();
            },
            error: function () {
                alert("Content load failed.");
                $('#overlayForm').modal('hide');
            }
        });
    }
    else {
        alert("Select the Theme.");
        event.preventDefault();
        event.stopImmediatePropagation();
    }
});
$("#addKeyword").click(function (event) {
    if (typeof $(".groupsUl li.active").children().first().attr("id") !== "undefined") {
        $.ajax({
            type: "get",
            url: "/Configure/_AddPhrase",
            //data: dataString,
            contentType: "application/json; charset=utf-8",
            datatype: "html",
            success: function (data) {
                console.log(data);
                $('#modalContent').html(data);
                document.getElementById("txtAddPhrase").select();
                $("#txtAddPhrase").select();
            },
            error: function () {
                alert("Content load failed.");
                $('#overlayForm').modal('hide');
            }
        });
    }
    else {
        alert("Select the Group.");
        event.preventDefault();
        event.stopImmediatePropagation();
    }
});
$("#txtThemes").autocomplete({
    source: '/Configure/GetGroups',
    minLength: 0
});
$("#txtThemes").focus(function (event) {
    $("#txtThemes").autocomplete("search", "");
});
function drag(ev) {
    ev.dataTransfer.setData("text", ev.target.id);
    this.draggedId = ev.target.id;
}
function allowDrop(ev) {
    var data = ev.dataTransfer.getData("text");
    console.log(ev.targegroupAnchor234t.id);
    ev.preventDefault();
}
function dragLeave(ev) {
    document.getElementById(ev.target.id).parentElement.classList.remove("drop-ok");
}