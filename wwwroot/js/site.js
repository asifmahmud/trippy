
(function () {
    var $sidebarAndContainer = $("#sidebar, #container");
    var $iconToggle = $("#toggleSidebar i.fa");

    $("#toggleSidebar").on("click", function () {
        $sidebarAndContainer.toggleClass("hide-sidebar");
        /*
        if ($sidebarAndContainer.hasClass("hide-sidebar")) {
            $iconToggle.removeClass("fa-angle-left");
            $iconToggle.addClass("fa-angle-right");
        }
        else {
            $iconToggle.removeClass("fa-angle-right");
            $iconToggle.addClass("fa-angle-left");
        }
        */
    });
})();