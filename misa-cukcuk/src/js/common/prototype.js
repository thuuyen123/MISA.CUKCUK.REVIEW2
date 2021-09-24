/**
 * Hàm format string theo dạng "*{n}*"
 * @returns string đã được format
 * CreateBy: TTUyen (07/09/2021)
 */
String.prototype.format = function () {
    var a = this;
    for (var k in arguments) {
        a = a.replace(new RegExp("\\{" + k + "\\}", 'g'), arguments[k]);
    }
    return a
}