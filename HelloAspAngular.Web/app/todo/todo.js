var mod = angular.module("todoCtrls", []);

mod.controller("TodoPageCtrl", ["$scope", "$http", function ($scope, $http) {
    // プロパティ
    $scope.todoLists = null;
    $scope.activeTodoList = null;

    // メソッド
    $scope.isActive = function (todoListId) {
        return todoListId === "archived" ?
            $scope.activeTodoList.Kind === 1 :
            todoListId === $scope.activeTodoList.Id;
    };

    $scope.activate = function (todoListId) {
        var url = todoListId === "archived" ?
            "/api/todolists/archived" :
            "/api/todolists/" + todoListId;
        $http.get(url).success(function (data, status, headers) {
            $scope.activeTodoList = data;
            $scope.activeTodoList.etag = headers("ETag");
        });
    };

    // 初期化
    $http.get("/api/todolists").success(function (data) {
        $scope.todoLists = data;
        $scope.activate($scope.todoLists[0].Id);
    });
}]);

mod.controller("TodoListCtrl", ["$scope", "$http", function ($scope, $http) {
    // プロパティ
    $scope.todoText = "";

    // メソッド
    function getHttpConfig() {
        return {
            headers: { 'If-Match': $scope.activeTodoList.etag }
        };
    }

    $scope.addTodo = function () {
        if (!$scope.todoText) {
            return;
        }

        var todo = {
            IsDone: false,
            Text: $scope.todoText
        };
        $scope.activeTodoList.Todos.push(todo);
        $scope.todoText = "";

        var url = "/api/todolists/" + $scope.activeTodoList.Id + "/todos";
        $http.post(url, todo, getHttpConfig()).success(function (data, status, headers) {
            todo.Id = data.Id;
            $scope.activeTodoList.etag = headers("ETag");
        });
    };

    $scope.updateTodo = function (todo) {
        var url = "/api/todolists/" + $scope.activeTodoList.Id + "/todos/" + todo.Id;
        $http.put(url, todo, getHttpConfig()).success(function (data, status, headers) {
            $scope.activeTodoList.etag = headers("ETag");
        });
    };

    $scope.archive = function () {
        $scope.activeTodoList.Todos = todoDomain.getTodosShouldNotBeArchived($scope.activeTodoList.Todos);

        var url = "/api/todolists/" + $scope.activeTodoList.Id + "/archive";
        $http.put(url, null, getHttpConfig()).success(function (data, status, headers) {
            $scope.activeTodoList.etag = headers("ETag");
        });
    };

    $scope.remaining = function () {
        return todoDomain.getRemainingTodoCount($scope.activeTodoList.Todos);
    };
}]);

mod.controller("ArchivedTodoListCtrl", ["$scope", "$http", function ($scope, $http) {
    // メソッド
    function getHttpConfig() {
        return {
            headers: { 'If-Match': $scope.activeTodoList.etag }
        };
    }

    $scope.clearTodos = function () {
        if ($scope.activeTodoList.Todos.length === 0) {
            return;
        }

        $scope.activeTodoList.Todos = [];

        var url = "/api/todolists/" + $scope.activeTodoList.Id + "/clear";
        $http.delete(url, getHttpConfig()).success(function (data, status, headers) {
            $scope.activeTodoList.etag = headers("ETag");
        });
    };
}]);
