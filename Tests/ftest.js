function x2(x) {
    return 2 * x;
}

(function (a, b) { with({}) if(a == b) console.log("Weak parameters") })(x2(2), x2(3));

console.log(Math.max(...[1,2,3,4,5,6,7,8,9,0]))

if (false) {
    function func(a, b, ...rest) {
        console.log(JSON.stringify(rest));
        console.log(JSON.stringify(arguments));
        return arguments.length;
    }

    var test = {};
    test[Symbol.iterator] = function* () {
        for (var i = 0; i < 5; i++)
            yield i;
    }

    console.log(func(...test));
}