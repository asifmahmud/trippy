// editorController.js

(function () {
    'use strict';

    angular.module('app-trips').controller("editorController", editorController);

    function editorController($routeParams, $http, $scope) {
        var vm = this;
        vm.tripName = $routeParams.tripName;
        vm.stops = [];
        vm.newStop = {};
        vm.errorMessage = "";
        vm.successMessage = "";
        vm.isBusy = true;

        var url = "/api/trips/" + vm.tripName + "/stops";

        $http.get(url)
            .then(function (res) {
                angular.copy(res.data, vm.stops);
            }, function (error) {
                vm.errorMessage = "Failed to get stops: " + error;
            })
            .finally(function () {
                vm.isBusy = false;
                _showMap();
            });

        vm.addStop = function () {
            vm.isBusy = true;
            vm.errorMessage = "";
            vm.successMessage = "";

            $http.post(url, vm.newStop)
                .then(function (res) {
                    vm.stops.push(res.data);
                    vm.successMessage = "Stop Successfully added. Please refresh page to see it on the map";
                }, function (error) {
                    vm.errorMessage = "Failed to save stop: " + error;
                })
                .finally(function () {
                    vm.isBusy = false;
                    vm.newStop = {};
                });
        };

        function _showMap() {
            var startingLocation = { lat: vm.stops[0].latitude, lng: vm.stops[0].longitude };

            var mapStops = _.map(vm.stops, function (data) {
                return {
                    lat: data.latitude,
                    lng: data.longitude,
                    info: data.name
                };
            });

            $scope.mapOptions = {
                zoom: 4,
                center: startingLocation,
                mapTypeId: 'terrain'
            };

            $scope.map = new google.maps.Map(document.getElementById('map'), $scope.mapOptions);

            $scope.tripStops = new google.maps.Polyline({
                path: mapStops,
                geodesic: false,
                strokeColor: '#FF0000',
                strokeOpacity: 0.5,
                strokeWeight: 4
            });



            //Adding a marker to each stop
            vm.stops.forEach(function (data) {
                var contentString = '<div> <p><b>Current Location: </b></p></div>' +
                    '<div><p>' + data.name + '</p></div>';

                $scope.infoWindow = new google.maps.InfoWindow({ content: contentString });

                $scope.marker = new google.maps.Marker({
                    position: { lat: data.latitude, lng: data.longitude },
                    map: $scope.map,
                    icon: {
                        path: google.maps.SymbolPath.CIRCLE,
                        scale: 4
                    },
                });

                $scope.marker.addListener('click', function () {
                    $scope.infoWindow.open($scope.map, $scope.marker);
                });

            });

            $scope.tripStops.setMap($scope.map);
        }
        

        

        
    }


})();
