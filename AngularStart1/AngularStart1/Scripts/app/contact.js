angular.module('main')
    .controller('ContactController', ['$scope', '$location', '$http', function ($scope, $location, $http) {
        $scope.messageFromStoreUser = {};
        $scope.showForm = true;
        $scope.send = function (user) {
            $scope.messageFromStoreUser = angular.copy(user);
           
            $http({
                method: 'POST',
                url: '/Home/Contact',
                data: $scope.messageFromStoreUser
            }).success(function (data, status, headers, config) {
                if (data.success === true) {
                    $scope.showForm = false;
                    alert("Your message has been sent successfully");
                    $location.path('/Home');
                }
                else {
                    alert('Unexpected Error');
                    location.reload();
                }
            }).error(function (data, status, headers, config) {
                $scope.errors = [];
                alert('Unexpected Error');
            });
        };

    }]);