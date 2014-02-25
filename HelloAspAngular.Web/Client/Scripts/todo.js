angular.module("todoCtrl", []).
    controller("TodoCtrl", ["$scope", "$http", function ($scope, $http) {
        // プロパティ
        $scope.todoLists = null;
        $scope.activeTodoList = null;
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
            var oldTodos = $scope.activeTodoList.Todos;
            $scope.activeTodoList.Todos = [];
            angular.forEach(oldTodos, function (todo) {
                if (!todo.IsDone) {
                    $scope.activeTodoList.Todos.push(todo);
                }
            });

            var url = "/api/todolists/" + $scope.activeTodoList.Id + "/archive";
            $http.put(url, null, getHttpConfig()).success(function (data, status, headers) {
                $scope.activeTodoList.etag = headers("ETag");
            });
        };

        $scope.remaining = function () {
            var count = 0;
            angular.forEach($scope.activeTodoList.Todos, function (todo) {
                count += todo.IsDone ? 0 : 1;
            });
            return count;
        };

        $scope.isActive = function (todoList) {
            return todoList.Id === $scope.activeTodoList.Id;
        };

        $scope.activate = function (todoListId) {
            $http.get("/api/todolists/" + todoListId).success(function (data, status, headers) {
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
