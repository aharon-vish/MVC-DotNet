angular.module('main')
    .controller('MainController', ['$scope', '$http', '$modal', '$log', 'appService', function
        ($scope, $http, $modal, $log, appService) {
        
        $scope.items = [];
        $scope.animationsEnabled = true;
        $scope.nameGiftCard = null;      
        $scope.person = {
            name: "Ari Lerner"
        };
        //var modalInstance = null;
        // jquery ui autocomplete 
        $(function () {
            $("#txtSearch").autocomplete({
                source: "Home/autocomplete",
                minLength: 1
            });
        });

        $scope.openModal = function () {                   
            var cardfullname = $("#txtSearch").val();           
            appService.getGiftCardParm(cardfullname).then(function (d) {
               
                $scope.nameGiftCard = d.data;
              
                $scope.modalInstance = $modal.open({
                    templateUrl: 'buyAction.html',
                    controller: 'ModalInstanceCtrl',
                    backdrop: 'static',
                    keyboard: false,
                    resolve: {
                        items: function () {
                            return $scope.items;
                        },
                        nameGiftCard: function () {
                            return $scope.nameGiftCard;
                        }
                    }
                });
          
            }, function (error) {
                alert("No Gift Card Was Found")
            });        
        };
       
    }]);
angular.module('main').controller('ModalInstanceCtrl', function ($scope,$filter,$http, $modalInstance, items, nameGiftCard) {
    $scope.nameGiftCard = nameGiftCard;
    $scope.items = items;   
    $scope.selected = {
        item: $scope.items[0]
    };



    $scope.cancel = function () {
        $modalInstance.dismiss('cancel');
    };

    $scope.items = [
      { id: 1, skuNumber: '1111', itemDescription: 'Men’s UA Performance Chino – Straight Leg', price: 234, size: 'xl' },
      { id: 2, skuNumber: '2222', itemDescription: 'Men’s UA ColdGear® Infrared Fleece Zip Hoodie', price: 234, size: 'xl' },
      { id: 3, skuNumber: '3333', itemDescription: 'Men’s UA ColdGear® Infrared Fleece ¼ Zip', price: 112, size: 'xl' },
      { id: 4, skuNumber: '4444', itemDescription: 'Men’s UA ColdGear® Armour Printed Compression Crew', price: 90, size: 'xl' },
      { id: 5, skuNumber: '5555', itemDescription: 'Men’s UA Lockertag T-Shirt', price: 50, size: 'xl' },
      { id: 6, skuNumber: '6666', itemDescription: 'UA ColdGear® Infrared Survivor Hybrid Full Zip Vest', price: 500, size: 'xl' },
      { id: 7, skuNumber: '7777', itemDescription: 'Women’s UA Stripe Logo Long Sleeve', price: 230, size: 'xl' },
      { id: 8, skuNumber: '8888', itemDescription: 'Women’s UA Armour High Bra', price: 120, size: 'xl' },
      { id: 9, skuNumber: '9999', itemDescription: 'Women’s UA Pure Stretch Cheeky', price: 80, size: 'xl' },
      { id: 10, skuNumber: '1212', itemDescription: 'Women’s UA ColdGear® Infrared Avondale Parka', price: 99, size: 'xl' }];

    $scope.listItems = [];

    $scope.deleteItem = function (index) {
        $scope.nameGiftCard.Credit = $scope.nameGiftCard.Credit + $scope.listItems[index].price;
        $scope.listItems.splice(index, 1);
      
    }
    $scope.addItem = function (selectedItem) {
        $scope.nameGiftCard.Credit = $scope.nameGiftCard.Credit - selectedItem.price;
        if (nameGiftCard.Credit <0) {
            alert("Note!!!\nNo balance remaining on the card\nThe customer must add" +" "+(-1*nameGiftCard.Credit)+"$");
        }
        $scope.listItems.push(selectedItem);
        $scope.selectedItem = null;
      

    };

    $scope.ok = function () {
        $scope.ddMMyyyy = $filter('date')(new Date(), 'yyyy-MM-dd HH:mm:ss');
        $scope.Receipt = 

            { PurchaseAmount: $scope.nameGiftCard.Credit,
             DatePurchas: $scope.ddMMyyyy,
             GiftCardID: $scope.nameGiftCard.GiftCardID ,
             StoreID: $scope.nameGiftCard.StoreID
            }
        ;
      
       
      
        if (!$scope.listItems.length == 0) {
           

            $http({
                method: 'POST',
                url: '/Home/cardImplement',
                data: $scope.Receipt
            }).success(function (data, status, headers, config) {
                if (data.success === true) {
                    $scope.showForm = false;
                    alert("successfully upodate");
                    $modalInstance.close();                                     
                }
                else {
                    alert('Unexpected Error');
                    location.reload();
                }
            }).error(function (data, status, headers, config) {
                $scope.errors = [];
               
            });
        }
        else {
            alert('I am not in');
            $modalInstance.close();
        }
    };
});
