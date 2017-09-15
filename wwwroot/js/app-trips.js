// app-trips.js

(function () {
    'use strict';

    angular.module("app-trips", ['simpleControls', 'ngRoute'])
        .config(function ($routeProvider) {

            $routeProvider
                .when("/", {
                    controller: "tripsController",
                    controllerAs: "vm",
                    templateUrl: "/views/tripsView.html"
                })
                .when("/editor/:tripName", {
                    controller: "editorController",
                    controllerAs: "vm",
                    templateUrl: "/views/editorView.html"
                });

            $routeProvider.otherwise({ redirectTo: "/" });
        });
})();
