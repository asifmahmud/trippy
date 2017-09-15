// editorController.js

(function () {
    'use strict';

    angular.module('app-trips').controller("editorController", editorController);

    function editorController($routeParams, $http) {
        var vm = this;
        vm.tripName = $routeParams.tripName;
        vm.stops = [];
        vm.errorMessage = "";
        vm.isBusy = true;

        $http.get("/api/trips/" + vm.tripName + "/stops")
            .then(function (res) {
                angular.copy(res.data, vm.stops);
            }, function (error) {
                vm.errorMessage = "Failed to get stops: " + error;
            })
            .finally(function () {
                vm.isBusy = false;
            });
            
    }
})();