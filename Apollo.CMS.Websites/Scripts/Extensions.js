/* File Created: January 6, 2014 */
/*jshint ignore:start */

// String Format
String.prototype.format = function () {
    var args = arguments;
    return this.replace(/{(\d+)}/g, function (match, number) {
        return typeof args[number] != 'undefined' ? args[number] : match;
    });
};

// Array Remove - By John Resig (MIT Licensed)
Array.prototype.remove = function (from, to) {
    var rest = this.slice((to || from) + 1 || this.length);
    this.length = from < 0 ? this.length + from : from;
    return this.push.apply(this, rest);
};

// Array Remove All elements matched the criteria
Array.prototype.removeAll = function (criteria) {
    var length = this.length;

    for (var i = 0; i < length; i++) {
        var element = this[i];

        if (criteria(element)) {
            this.splice(i--, 1);
            length--;
        }
    }
};

/*jshint ignore:end */