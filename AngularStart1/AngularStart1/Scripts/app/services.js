// Define you service here. Services can be added to same module as 'main' or a seperate module can be created.

var angularStartServices = angular.module('angularStart.services', ['ngResource']);     //Define the services module

angularStartServices.factory('TestService', '$http', [function ($http) {

}]);

appRoot.service('appService', function ($http) {
    this.getGiftCardParm = function (cardfullname) {
     return $http({
            url: '/Home/GiftCardParm',
            method: "GET",
            params: { param1: cardfullname }
        });

    }
    this.getannualReport = function () {
        return $http({
            url: '/Home/annualReport',
            method: "GET"
        });

    }
    this.getmonthlyReport = function () {
        return $http({
            url: '/Home/monthlyReport',
            method: "GET"
        });

    }
});