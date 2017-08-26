// site.js

(function () {
    $('#username').text("Asif Mahmud");

    var menuItems = $(".menu li a");
    menuItems.on("click", function () {
        alert("Hello");
    });
})();