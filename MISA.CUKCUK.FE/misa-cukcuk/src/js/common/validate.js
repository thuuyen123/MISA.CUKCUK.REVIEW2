export const ValidatorMixin = {
    /**
    * Hàm dùng để kiểm tra phím có phải số hay không
    * @param {any} event
    * @returns true: hợp lệ | false: không hợp lệ
    */
    isNumberKey(event) {
        var charCode = (event.which) ? event.which : event.keyCode
        if ((charCode > 31 && (charCode < 48 || charCode > 57)) && !this.testNumberOnly(event.value))
            return false;
        return true;
    },

    /**
     * Hàm dùng để validate chỉ số
     * @param {String} string
     * @returns true: hợp lệ | false: không hợp lệ
     */
    testNumberOnly(string) {
        var numRegex = /^\d+$/;
        return numRegex.test(string);
    },

    /**
     * Lấy số từ string
     * @param {*} str 
     * @returns converted string
     */
    extractNumberFromString(str) {
        return str.replace(/[^0-9]/g, "");
    }
};