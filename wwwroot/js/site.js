
(function () {
    var $sidebarAndContainer = $("#sidebar, #container");
    var $iconToggle = $("#toggleSidebar i.fa");

    $("#toggleSidebar").on("click", function () {
        $sidebarAndContainer.toggleClass("show-sidebar");
    });
})();