﻿(function (app) {

    app.controller('productCategoryAddController', productCategoryAddController)

    productCategoryAddController.$inject = ['$scope', 'apiService', 'notificationService', '$state', 'commonService']

    function productCategoryAddController($scope, apiService, notificationService, $state, commonService) {
        $scope.productCategory = {
            CreatedDate: new Date(),
            Status:true
        }
        $scope.AddProductCategory = AddProductCategory;
        $scope.GetSeoTitle = GetSeoTitle;

        function GetSeoTitle() {
            $scope.productCategory.Alias = commonService.getSeoTitle($scope.productCategory.Name);
        }
        function AddProductCategory() {
            apiService.post('/api/productcategory/create', $scope.productCategory, function (result) {
                notificationService.displaySuccess(result.data.Name + ' đã được thêm mới.');
                $state.go('product_categories');

            }, function (error) {
                notificationService.displayError('Thêm mới không thành công');
            });
        };

        function loadParentCategories() {
            apiService.get('/api/productcategory/getallparents', null, function (result) {
                $scope.parentCategories = result.data;
            }, function (error) {
                console.log('Can not get list parent');
            });
        };
        loadParentCategories();
    }

})(angular.module('lrocreshop.product_categories'));