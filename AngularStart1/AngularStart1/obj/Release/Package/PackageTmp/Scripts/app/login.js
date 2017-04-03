var app = angular.module('Login', []);
app.controller('validateCtrl', ['$scope', '$http', function ($scope, $http) {
    
    //$scope.LoginData = {
    //    StoreName: '',
    //    Password: ''
    //};
    $scope.hasFocusStoreName = true;
    $scope.hasFocusPassword = true;


    $scope.Login = function ($location) {
        $http({
            method: 'POST',
            url: '/Home/UserLogin',
            data: $scope.LoginData
       }).success(function (data, status, headers, config) {
            $scope.message = '';
            $scope.errors = [];
            if (data.success === false) {             
                $scope.errors = "User store or password is incorrect ."
            }
            else {
                location.reload();
              
                //$scope.message = 'Saved Successfully';               
                //$scope.person = {};
            }
        }).error(function (data, status, headers, config) {
            $scope.errors = [];
            $scope.message = 'Unexpected Error';
        });
    }; 
}]);
