﻿(function (app) {
    app.controller('productCategoryListController', productCategoryListController);

    productCategoryListController.$inject = ['$scope', 'apiService'];
    function productCategoryListController($scope, apiService) {
        $scope.productCategories = [];
        $scope.page = 0;
        $scope.pageCount = 0;
        $scope.keyword = '';
        $scope.getProductCategories = getProductCategories;

        $scope.search = search;

        function search() {
            getProductCategories();
        };

        function getProductCategories(page) {
            page = page || 0;
            var config = {
                params: {
                    keyword: $scope.keyword,
                    page: page,
                    pageSize:2
                }
            }
            apiService.get('/api/productcategory/getall', config, function (result) {
                $scope.productCategories = result.data.Items;
                $scope.page = result.data.Page;
                $scope.pagesCount = result.data.TotalPages;
                $scope.totalCount = result.data.TotalCount;
            }, function () {
                console.log('Load productCategory failed.');
            });
        };

        $scope.getProductCategories();
    };
})(angular.module('lrocreshop.product_categories'));