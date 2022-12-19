let type: string = "";
var favorite: boolean = false;
var search: string = "";
var offset: number = 0;
var maxList: number = 0;
var currentTab: string = "all";
var audio = new Audio;
var listView: boolean = false;
type CallbackFunction = (arg: any, ...args: any[]) => void;

$().ready(function ()
{
    refreshList();
    $("#listDisplay").hide();
    $("#gridButton").prop("disabled", true);

    $("#typeList").change(function () {
        type = $(this)?.val()?.toString()!;
        callListUpdate();
    })

    $(".listTab").click(function () {
        var id:string = $(this)?.attr("id")!;
        if (id == currentTab) {
            return;
        }
        currentTab = id;
        offset = 0;
        if (id == "all") {
            favorite = false;
            $("#favorites").removeClass("selected");
        }
        else {
            favorite = true;
            $("#all").removeClass("selected")
        }
        $(this).addClass("selected");
        callListUpdate();
    })

    $("#search").change(function () {
        search = $(this)?.val()?.toString()!;
        callListUpdate();
    })

    $("#prev").click(function () {
        offset = Math.max(0, offset - 20);
        callListUpdate();
    });

    $("#next").click(function () {
        offset += 20;
        callListUpdate();
    });

    $("#gridButton").click(function () {
        $("#gridDisplay").show();
        $("#listDisplay").hide();
        $("#gridButton").prop("disabled", true);
        $("#listButton").prop("disabled", false);
        listView = false;
    });

    $("#listButton").click(function () {
        $("#listDisplay").show();
        $("#gridDisplay").hide();
        $("#gridButton").prop("disabled", false);
        $("#listButton").prop("disabled", true);
        listView = true;
    });
})

function refreshList() {
    maxList = $("#Count").val() as number;
    $("#prev").prop("disabled", (offset == 0));
    $("#next").prop("disabled", (maxList <= (offset + 20)));

    if (listView) {
        $("#listDisplay").show();
        $("#gridDisplay").hide();
    }

    $(".listLink").click(function (e) {
        if (!e.target.classList.contains("favorite") && !e.target.classList.contains("unfavorite")) {
            var id = $(this).attr("id");
            window.location.href = "/Home/DexDetails/" + id;
        }
    });

    $(".favorite").click(function () {
        var id: string = $(this)?.attr("pid")!;
        $(".favorite[pid=" + id + "]").each(function () {
            $(this).addClass("unfavorite");
            $(this).removeClass("favorite");
        });
        favoriteSwitch(id, false);
    });

    $(".unfavorite").click(function () {
        var id: string = $(this)?.attr("pid")!;
        $(".unfavorite[pid=" + id + "]").each(function () {
            $(this).addClass("favorite");
            $(this).removeClass("unfavorite");
        });
        favoriteSwitch(id, true);
    })
}

function callListUpdate() {
    callController("GET", "RefreshList", JSON.stringify({ offset: offset, search: search, favorite: favorite, type: type }), refreshListCallback)
}

function callController(type: string, method: string, params: string, callback: CallbackFunction) {
    var request = $.ajax({
        type: type,
        url: "/Home/" + method,
        data: params,
        contentType: 'json'
    });
    request.done(function (response) {
        callback(response);
    });
}

function refreshListCallback(response: any) {
    $("#listView").html(response);
    refreshList();
}

function favoriteSwitch(id: string, fav: boolean) {
    if (favorite) {
        callController("GET", "ChangeFavorite", JSON.stringify({ id: id, favorite: fav }), callListUpdate)
    }
    else {
        $.post("/Home/ChangeFavorite", { id: id, favorite: fav });
        refreshList();
    }
}