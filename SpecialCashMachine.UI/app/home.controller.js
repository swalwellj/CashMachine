(function () {
    'use strict';

    var app = angular.module('cashMachine');
    app.controller('homeController', ['$scope', '$http', function ($scope, $http) {


        $scope.change;
        $scope.amount;
        $scope.showResults = false;
        $scope.errorMessage;
        $scope.busyPromise;

        $scope.algorithm = 0;

   


        $scope.getChange = function () {
            $scope.showResults = false;
            $scope.errorMessage = undefined;
            var config = {
                params: {
                    amount: this.amount,
                    algorithm: this.algorithm
                    
                }
            }

          $scope.busyPromise =  $http.get("/api/CashMachine/", config)
                .then(function(response) {

                    $scope.change = JSON.parse(response.data);

                    if ($scope.change.TranscationStatus === 0) {
                        $scope.showResults = true;
                    } else {
                        $scope.errorMessage = $scope.change.ErrorMessage;
                    }
                   

                }, function errorCallback(response) {
                   $scope.errorMessage = response.data.error;
                });
           
           
        }
    }]);

}());