//tripsController.js

(function () {
    'use strict';
    angular.module('app-trips').controller("tripsController", tripsController);

    function tripsController($http) {
        var vm = this;
        vm.trips = [];
        vm.newTrip = {};
        vm.errorMessage = "";
        vm.isBusy = true;

        $http.get("/api/trips")
            .then(function (res) {
                angular.copy(res.data, vm.trips);
            }, function (error) {
                vm.errorMessage = "Failed to retrieve data: " + error;
            })
            .finally(function () {
                vm.isBusy = false;
            });

        vm.addTrip = function () {
            vm.trips.push({ name: vm.newTrip.name, created: new Date() });
            vm.newTrip = {};
        };
    }
})();