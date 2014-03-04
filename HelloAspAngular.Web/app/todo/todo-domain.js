var todoDomain = (function () {
    return {
        getTodosShouldNotBeArchived: function (todos) {
            var ret = [];
            angular.forEach(todos, function (todo) {
                if (!todo.IsDone) {
                    ret.push(todo);
                }
            });
            return ret;
        },
        getRemainingTodoCount: function (todos) {
            var count = 0;
            angular.forEach(todos, function (todo) {
                count += todo.IsDone ? 0 : 1;
            });
            return count;
        }
    };
})();
