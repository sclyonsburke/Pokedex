$().ready(function () {
    $(".detailsLink").click(function (e) {
        if (!e.target.classList.contains("favorite") && !e.target.classList.contains("unfavorite")) {
            var id = $(this).attr("id");
            window.location.href = "/Home/DexDetails/" + id;
        }
    });
    var audioUrl = $("#pokemonCry").attr("audio");
    audio = new Audio(audioUrl);
    $("#pokemonCry").click(function () {
        audio.play();
    });
    refreshFavorite();
});
function refreshFavorite() {
    $(".favorite").click(function () {
        var id = $(this).attr("pid");
        $.post("/Home/ChangeFavorite", { id: id, favorite: false });
        $(this).addClass("unfavorite");
        $(this).removeClass("favorite");
        refreshFavorite();
    });
    $(".unfavorite").click(function () {
        var id = $(this).attr("pid");
        $.post("/Home/ChangeFavorite", { id: id, favorite: true });
        $(this).addClass("favorite");
        $(this).removeClass("unfavorite");
        refreshFavorite();
    });
}
//# sourceMappingURL=DexDetails.js.map