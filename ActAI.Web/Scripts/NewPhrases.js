window.onload = function () {
$('.selectpicker').selectpicker();
};
$("#txtThemes").autocomplete({
    source: '/Configure/GetGroups',
    minLength: 0
});
$("#txtThemes").focus(function (event) {
    $("#txtThemes").autocomplete("search", "");
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
        },
        error: function () {
            alert("Content load failed.");
            $('#overlayForm').modal('hide');
        }
    });
});
$("#addGroup").click(function () {
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
function drag(ev) {
    ev.dataTransfer.setData("text", ev.target.id);
    this.draggedId = ev.target.id;
}
function dragOverStopList(ev) {
    if (this.draggedId.startsWith("newPhraseAnchor")) {
        document.getElementById("stopListUl").classList.add("drop-ok");
        ev.preventDefault();
    }
}
function dragOverNewPhrases(ev) {
    if (this.draggedId.startsWith("stopListAnchor")) {
        document.getElementById("newPhrasesUl").classList.add("drop-ok");
        ev.preventDefault();
    }
}
function dragLeave(ev) {
    document.getElementById(ev.target.id).parentElement.classList.remove("drop-ok");
}
function dragLeaveStopList(ev) {
    document.getElementById("stopListUl").classList.remove("drop-ok");
}
function dragLeaveNewPhrases(ev) {
    document.getElementById("newPhrasesUl").classList.remove("drop-ok");
}
function allowDrop(ev) {
    var data = ev.dataTransfer.getData("text");
    console.log(ev.draggedId);
    ev.preventDefault();
}
function dragOverGroups(ev) {
    if (this.draggedId.startsWith("newPhraseAnchor") || this.draggedId.startsWith("stopListAnchor")) {
        document.getElementById(ev.target.id).parentElement.classList.add("drop-ok");
        ev.preventDefault();
    }
}
function dropInGroups(ev, target) {
    console.log(1);
    var dropId = ev.target.id;
    var dragId = ev.dataTransfer.getData("text");
    var dropTrendId = dropId.substring(11);
    console.log(dragId);
    if (dragId.startsWith("newPhraseAnchor") || dragId.startsWith("stopListAnchor")) {
        console.log(2);
        ev.preventDefault();
        var queryString = "?groupID=" + $("#" + dropId).attr("data-id") + "&newPhraseID=" + $("#" + dragId).attr("data-id");
        console.log(queryString);
        $.ajax({
            type: "POST",
            url: "/NewPhrases/MoveToGroup" + queryString,
            //data: JSON.stringify(dataString),
            //contentType: "application/json; charset=utf-8",
            datatype: "html",
            success: function (data) {
                document.getElementById(dragId).parentElement.remove();
            },
            error: function () {
                alert("Content load failed.");
            }
        });
    }
    document.getElementById(ev.target.id).parentElement.classList.remove("drop-ok");
}
function dropInStopList(ev, target) {
    console.log(1);
    var dropId = ev.target.id;
    var dragId = ev.dataTransfer.getData("text");
    if (dragId.startsWith("newPhraseAnchor")) {
        console.log(2);
        ev.preventDefault();
        var ID = $("#" + dragId).attr("data-id");
        $.ajax({
            type: "POST",
            url: "/NewPhrases/MoveToStopList" + "?newPhraseID=" + ID,
            //data: JSON.stringify(dataString),
            //contentType: "application/json; charset=utf-8",
            datatype: "html",
            success: function (data) {
                var newLi = document.createElement('li');
                newLi.innerHTML = '<a id=stopListAnchor' + ID + ' data-id="' + ID + '" draggable="true" ondragstart="drag(event)" ondrop="dropInStopList(event, this)" ondragover="allowDrop(event)" data-toggle="tab" href="#">' + document.getElementById(dragId).innerHTML + '</a>';
                document.getElementById(dragId).parentElement.remove();
                document.getElementById("stopListUl").appendChild(newLi);
                console.log(newLi);
            },          error: function () {
                alert("Content load failed.");
            }
        });
    }
    document.getElementById("stopListUl").classList.remove("drop-ok");
}
function dropInNewPhrases(ev, target) {
    console.log(1);
    var dropId = ev.target.id;
    var dragId = ev.dataTransfer.getData("text");
    console.log(dragId);
    if (dragId.startsWith("stopListAnchor")) {
        console.log(2);
        ev.preventDefault();
        var ID = $("#" + dragId).attr("data-id");
        $.ajax({
            type: "POST",
            url: "/NewPhrases/MoveToNewPhrases" + "?newPhraseID=" + ID,
            //data: JSON.stringify(dataString),
            //contentType: "application/json; charset=utf-8",
            datatype: "html",
            success: function (data) {
                var newLi = document.createElement('li');
                newLi.innerHTML = '<a id=newPhraseAnchor' + ID + ' data-id="' + ID + '" draggable="true" ondragstart="drag(event)" ondrop="dropInNewPhrases(event, this)" ondragover="allowDrop(event)" data-toggle="tab" href="#">' + document.getElementById(dragId).innerHTML + '</a>';
                document.getElementById(dragId).parentElement.remove();
                document.getElementById("newPhrasesUl").appendChild(newLi);
                console.log(newLi);
            },
            error: function () {
                alert("Content load failed.");
            }
        });
    }
    document.getElementById("newPhrasesUl").classList.remove("drop-ok");
}