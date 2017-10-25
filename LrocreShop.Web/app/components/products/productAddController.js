(function (app) {

    app.controller('productAddController', productAddController)

    productAddController.$inject = ['$scope', 'apiService', 'notificationService', '$state', 'commonService']

    function productAddController($scope, apiService, notificationService, $state, commonService) {
        $scope.product = {
            CreatedDate: new Date(),
            Status: true
        }
        $scope.moreImages = [];
        $scope.AddProduct = AddProduct;
        $scope.GetSeoTitle = GetSeoTitle;

        $scope.ckeditorOptions = {
            language: 'vi',
            height: '200px'
        }

        function GetSeoTitle() {
            $scope.product.Alias = commonService.getSeoTitle($scope.product.Name);
        }
        function AddProduct() {
            $scope.product.MoreImages = JSON.stringify($scope.moreImages);
            apiService.post('/api/product/create', $scope.product, function (result) {
                notificationService.displaySuccess(result.data.Name + ' đã được thêm mới.');
                $state.go('products');

            }, function (error) {
                notificationService.displayError('Thêm mới không thành công');
            });
        };

        function loadProductCategories() {
            apiService.get('/api/productcategory/getallparents', null, function (result) {
                $scope.productCategories = result.data;
            }, function (error) {
                console.log('Can not get list product categories');
            });
        };
        $scope.ChooseImage = function () {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {              
                $scope.$apply(function () {
                    $scope.product.Image = fileUrl
                });
            }
            finder.popup();
        }
        $scope.ChooseMoreImage = function () {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {                
                $scope.$apply(function () {
                    $scope.moreImages.push(fileUrl)
                });
            }
            finder.popup();
        }
        loadProductCategories();
    }

})(angular.module('lrocreshop.products'));