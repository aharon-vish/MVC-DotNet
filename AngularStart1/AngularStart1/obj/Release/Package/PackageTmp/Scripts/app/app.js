// Main configuration file. Sets up AngularJS module and routes and any other config objects

var appRoot = angular.module('main', ['ngRoute', 'ngGrid', 'ngResource', 'angularStart.services', 'angularStart.directives', 'googlechart', 'ui.bootstrap']);     //Define the main module

appRoot
    .config(['$routeProvider', function ($routeProvider) {
        //Setup routes to load partial templates from server. TemplateUrl is the location for the server view (Razor .cshtml view)
        $routeProvider            
            .when('/home', { templateUrl: '/Home.html', controller: 'MainController' })
            .when('/Contact', { templateUrl: '/Contact.html', controller: 'ContactController' })
            .when('/report', { templateUrl: '/report.html', controller: 'reportController' })
            .when('/demo', { templateUrl: '/buyAction.html', controller: 'MainController' })
            .when('/LogOut', { templateUrl: '/Home.html', controller: 'LogOutController' })
            .when('/angular', { templateUrl: '/home/angular' })
            .otherwise({ redirectTo: '/home' });
    }])
    .controller('RootController', ['$scope', '$route', '$routeParams', '$location', function ($scope, $route, $routeParams, $location) {
        $scope.$on('$routeChangeSuccess', function (e, current, previous) {
            $scope.activeViewPath = $location.path();
        });
    }]);
