// site.js

(function () {
    var $sidebarAndContainer = $("#sidebar, #container");
    $("#toggleSidebar").on("click", function () {
        $sidebarAndContainer.toggleClass("hide-sidebar");
        if ($sidebarAndContainer.hasClass("hide-sidebar")) {
            $(this).text("Show Sidebar");
        }
        else {
            $(this).text("Hide Sidebar");
        }
    });
})();