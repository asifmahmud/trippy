//tripsController.js

(function () {
    'use strict';
    angular.module('app-trips').controller("tripsController", tripsController);

    function tripsController() {
        this.trips = [
            {
                name: "US Trip",
                date: new Date()
            },
            {
                name: "World Trip",
                date: new Date()
            }
        ];
    }
})();