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
            vm.isBusy = true;
            vm.errorMessage = "";
            $http.post("/api/trips", vm.newTrip)
                .then(function (res) {
                    vm.trips.push(res.data);
                }, function (error) {
                    vm.errorMessage = "Failed to save data " + error;
                })
                .finally(function () {
                    vm.isBusy = false;
                    vm.newTrip = {};
                });
        };
    }
})();