window.onload = function () {
$('.selectpicker').selectpicker();
};
$("#txtThemes").autocomplete({
    source: '/Configure/GetGroups',
    minLength: 0
});
$("#txtThemes").focus(function (event) {
    $("#txtThemes").autocomplete("search", "");
})
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
    console.log(1)
    if (typeof $("#themesUl li.active").children().first().attr("id") != "undefined") {
        console.log(2)
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
function dragOverThemes(ev) {
    if (this.draggedId.startsWith("groupAnchor")) {
        document.getElementById(ev.target.id).parentElement.classList.add("drop-ok");
        ev.preventDefault();
    }
}
function dragOverGroups(ev) {
    if (this.draggedId.startsWith("newKeywordAnchor") || this.draggedId.startsWith("stopListAnchor")) {
        document.getElementById(ev.target.id).parentElement.classList.add("drop-ok");
        ev.preventDefault();
    }
}
function dragOverStopList(ev) {
    if (this.draggedId.startsWith("newKeywordAnchor")) {
        document.getElementById("stopListUl").classList.add("drop-ok");
        ev.preventDefault();
    }
}
function dragOverNewKeywords(ev) {
    if (this.draggedId.startsWith("stopListAnchor")) {
        document.getElementById("newKeywordsUl").classList.add("drop-ok");
        ev.preventDefault();
    }
}
function dragLeave(ev) {
    document.getElementById(ev.target.id).parentElement.classList.remove("drop-ok");
}
function dragLeaveStopList(ev) {
    document.getElementById("stopListUl").classList.remove("drop-ok");
}
function dragLeaveNewKeywords(ev) {
    document.getElementById("newKeywordsUl").classList.remove("drop-ok");
}
function allowDrop(ev) {
    var data = ev.dataTransfer.getData("text");
    console.log(ev.draggedId)
    ev.preventDefault();
}
function dropInThemes(ev, target) {
    var dropId = ev.target.id;
    var dragId = ev.dataTransfer.getData("text");
    if (dragId.startsWith("groupAnchor")) {
        ev.preventDefault();
        var queryString = "?group=" + $("#" + dragId).text() + "&theme=" + $("#" + dropId).text();
        $.ajax({
            type: "POST",
            url: "/NewKeywords/MoveGroup" + queryString,
            //data: JSON.stringify(dataString),
            //contentType: "application/json; charset=utf-8",
            datatype: "html",
            success: function (data) {
                var dropTrendId = dropId.substring(11);
                var dropThemeId = document.getElementById("theme" + dropTrendId);
                var newLi = document.createElement('li');
                newLi.innerHTML = '<a id="' + dragId + '" draggable="true" ondragstart="drag(event)" ondrop="dropInGroups(event, this)" ondragover="allowDrop(event)" data-toggle="tab" href="#">' + document.getElementById(dragId).innerHTML + '</a>';
                document.getElementById(dragId).parentElement.remove();
                dropThemeId.firstElementChild.appendChild(newLi);
                console.log(newLi);
            },
            error: function () {
                alert("Content load failed.");
            }
        });
    }
    document.getElementById(ev.target.id).parentElement.classList.remove("drop-ok");
}
function dropInGroups(ev, target) {
    console.log(1)
    var dropId = ev.target.id;
    var dragId = ev.dataTransfer.getData("text");
    var dropTrendId = dropId.substring(11);
    console.log(dragId)
    if (dragId.startsWith("newKeywordAnchor") || dragId.startsWith("stopListAnchor")) {
        console.log(2)
        ev.preventDefault();
        var queryString = "?id=" + dropTrendId + "&newKeyword=" + $("#" + dragId).text();
        console.log(queryString)
        $.ajax({
            type: "POST",
            url: "/NewKeywords/MoveNewKeyword" + queryString,
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
    if (dragId.startsWith("newKeywordAnchor")) {
        console.log(2);
        ev.preventDefault();
        var queryString = "?stopList=" + $("#" + dragId).text();
        $.ajax({
            type: "POST",
            url: "/NewKeywords/AddStopList" + queryString,
            //data: JSON.stringify(dataString),
            //contentType: "application/json; charset=utf-8",
            datatype: "html",
            success: function (data) {
                var dragDBId = dragId.substring(16);
                var newLi = document.createElement('li');
                newLi.innerHTML = '<a id="' + dragId + '" draggable="true" ondragstart="drag(event)" ondrop="dropInStopList(event, this)" ondragover="allowDrop(event)" data-toggle="tab" href="#">' + document.getElementById(dragId).innerHTML + '</a>';
                document.getElementById(dragId).parentElement.remove();
                document.getElementById("stopListUl").appendChild(newLi);
                console.log(newLi);
            },
            error: function () {
                alert("Content load failed.");
            }
        });
    }
    document.getElementById("stopListUl").classList.remove("drop-ok");
}
function dropInNewKeywords(ev, target) {
    console.log(1);
    var dropId = ev.target.id;
    var dragId = ev.dataTransfer.getData("text");
    console.log(dragId)
    if (dragId.startsWith("stopListAnchor")) {
        console.log(2);
        ev.preventDefault();
        var queryString = "?newKeyword=" + $("#" + dragId).text();
        $.ajax({
            type: "POST",
            url: "/NewKeywords/AddNewKeyword" + queryString,
            //data: JSON.stringify(dataString),
            //contentType: "application/json; charset=utf-8",
            datatype: "html",
            success: function (data) {
                var dragDBId = dragId.substring(14);
                var newLi = document.createElement('li');
                newLi.innerHTML = '<a id="' + dragId + '" draggable="true" ondragstart="drag(event)" ondrop="dropInNewKeywords(event, this)" ondragover="allowDrop(event)" data-toggle="tab" href="#">' + document.getElementById(dragId).innerHTML + '</a>';
                document.getElementById(dragId).parentElement.remove();
                document.getElementById("newKeywordsUl").appendChild(newLi);
                console.log(newLi);
            },
            error: function () {
                alert("Content load failed.");
            }
        });
    }
    document.getElementById("newKeywordsUl").classList.remove("drop-ok");
}