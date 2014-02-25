angular.module("todoApp", ["ngRoute", "todoCtrl"]).
    config(["$routeProvider", "$httpProvider", function ($routeProvider, $httpProvider) {
        $routeProvider.
            when("/", {
                templateUrl: "/Home/Todo",
                controller: "TodoCtrl"
            }).
            when("/about", {
                templateUrl: "/Home/About"
            }).
            otherwise({ redirectTo: "/" });

        $httpProvider.defaults.headers.common["X-Requested-With"] = 'XMLHttpRequest';
    }]);
