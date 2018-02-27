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
function dragOverThemes(ev) {
    console.log(this.draggedId);
    if (this.draggedId.startsWith("groupAnchor")) {
        document.getElementById(ev.target.id).parentElement.classList.add("drop-ok");
        ev.preventDefault();
    }
}
function dragOverGroups(ev) {
    console.log(this.draggedId);
    if (this.draggedId.startsWith("keywordAnchor")) {
        document.getElementById(ev.target.id).parentElement.classList.add("drop-ok");
        ev.preventDefault();
    }
}
function dragLeave(ev) {
    document.getElementById(ev.target.id).parentElement.classList.remove("drop-ok");
}
function dropInGroups(ev, target) {
    var dropId = ev.target.id;
    var dragId = ev.dataTransfer.getData("text");
    if (dragId.startsWith("keywordAnchor")) {
        ev.preventDefault();
        var queryString = "?phraseID=" + $("#" + dragId).attr("data-id") + "&groupID=" + $("#" + dropId).attr("data-id");
        $.ajax({
            type: "POST",
            url: "/Configure/MovePhrase" + queryString,
            //data: JSON.stringify(dataString),
            //contentType: "application/json; charset=utf-8",
            datatype: "html",
            success: function (data) {
                var dropTrendId = dropId.substring(11);
                console.log("dragId:" + dragId);
                console.log("dropId:" + dropId);
                console.log("dropTrendId:" + dropTrendId);
                var child = document.getElementById("group" + dropTrendId);
                var firstChildElement = child.firstElementChild;
                var dragLi = document.getElementById(dragId).parentElement.cloneNode(true);
                console.log(child);
                console.log(firstChildElement);
                console.log(dragLi);
                document.getElementById(dragId).parentElement.remove();
                firstChildElement.appendChild(dragLi);
            },
            error: function () {
                alert("Content load failed.");
            }
        });
    }
    document.getElementById(ev.target.id).parentElement.classList.remove("drop-ok");
}
function dropInThemes(ev, target) {
    console.log("dropInThemes");
    var dropId = ev.target.id;
    var dragId = ev.dataTransfer.getData("text");
    if (dragId.startsWith("groupAnchor")) {
        ev.preventDefault();
        console.log(dropId);
        var queryString = "?groupID=" + $("#" + dragId).attr("data-id") + "&themeID=" + $("#" + dropId).attr("data-id");
        $.ajax({
            type: "POST",
            url: "/Configure/MoveGroup" + queryString,
            //data: JSON.stringify(dataString),
            //contentType: "application/json; charset=utf-8",
            datatype: "html",
            success: function (data) {
                var dropTrendId = dropId.substring(11);
                var dragTrendId = dragId.substring(11);
                var dropThemeId = document.getElementById("theme" + dropTrendId);
                var dragLi = document.getElementById(dragId).parentElement.cloneNode(true);
                //var newLi = document.createElement('li');
                //newLi.innerHTML = '<a id="' + dragId + '" data-id="' + $("#" + dragId).attr("data-id") + '" draggable="true" ondragstart="drag(event)" ondrop="dropInGroups(event, this)" ondragover="allowDrop(event)" data-toggle="tab" href="#group' + dragTrendId + '">' + document.getElementById(dragId).innerHTML + '</a>';
                document.getElementById(dragId).parentElement.remove();
                dropThemeId.firstElementChild.appendChild(dragLi);
                console.log(newLi);
                document.getElementById("group" + dragTrendId).classList.remove("active");
            },
            error: function () {
                alert("Content load failed.");
            }
        });
    }
    document.getElementById(ev.target.id).parentElement.classList.remove("drop-ok");
}