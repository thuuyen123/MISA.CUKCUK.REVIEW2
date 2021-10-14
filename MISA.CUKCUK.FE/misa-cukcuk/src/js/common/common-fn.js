

export const CommonFn = {
    /**
     * Xóa dấu trong chữ
     * CreateBy: TTUyen(05/10/2021)
     */

    removeAccents(str) {
        str = str.replace(/à|á|ạ|ả|ã|â|ầ|ấ|ậ|ẩ|ẫ|ă|ằ|ắ|ặ|ẳ|ẵ/g, "a");
        str = str.replace(/è|é|ẹ|ẻ|ẽ|ê|ề|ế|ệ|ể|ễ/g, "e");
        str = str.replace(/ì|í|ị|ỉ|ĩ/g, "i");
        str = str.replace(/ò|ó|ọ|ỏ|õ|ô|ồ|ố|ộ|ổ|ỗ|ơ|ờ|ớ|ợ|ở|ỡ/g, "o");
        str = str.replace(/ù|ú|ụ|ủ|ũ|ư|ừ|ứ|ự|ử|ữ/g, "u");
        str = str.replace(/ỳ|ý|ỵ|ỷ|ỹ/g, "y");
        str = str.replace(/đ/g, "d");
        str = str.replace(/À|Á|Ạ|Ả|Ã|Â|Ầ|Ấ|Ậ|Ẩ|Ẫ|Ă|Ằ|Ắ|Ặ|Ẳ|Ẵ/g, "A");
        str = str.replace(/È|É|Ẹ|Ẻ|Ẽ|Ê|Ề|Ế|Ệ|Ể|Ễ/g, "E");
        str = str.replace(/Ì|Í|Ị|Ỉ|Ĩ/g, "I");
        str = str.replace(/Ò|Ó|Ọ|Ỏ|Õ|Ô|Ồ|Ố|Ộ|Ổ|Ỗ|Ơ|Ờ|Ớ|Ợ|Ở|Ỡ/g, "O");
        str = str.replace(/Ù|Ú|Ụ|Ủ|Ũ|Ư|Ừ|Ứ|Ự|Ử|Ữ/g, "U");
        str = str.replace(/Ỳ|Ý|Ỵ|Ỷ|Ỹ/g, "Y");
        str = str.replace(/Đ/g, "D");
        str = str.replace(/\u0300|\u0301|\u0303|\u0309|\u0323/g, ""); // ̀ ́ ̃ ̉ ̣  huyền, sắc, ngã, hỏi, nặng
        str = str.replace(/\u02C6|\u0306|\u031B/g, ""); // ˆ ̆ ̛  Â, Ê, Ă, Ơ, Ư
        return str;
    },

    /**
     * Hàm format số tiền thành dạng 1.000.000
     * Created By: Ngọc 28/90/2021
     */
    formatAutoMoney(myinput) {
        myinput += "";
        if (myinput != null) {
            myinput.replaceAll(".", "");
            let onlynumber = "";
            for (let i = 0; i < myinput.length; i++) {
                if (!isNaN(parseInt(myinput[i], 10))) {
                    onlynumber += myinput[i];
                }
            }
            return Number(onlynumber).toLocaleString("vi");
        }
        return 0;
    },
    /**
    * Hàm chuyển số dạng 1.000.000 về dạng 1000000 để lưu dữ liệu
    *  CreateBy TTUyen(1/ 8 / 2021)
    */
    reFormatMoney(str) {
        str += "";
        if (str != null) {
            str.replaceAll(".", "");
            let onlynumber = "";
            for (let i = 0; i < str.length; i++) {
                if (!isNaN(parseInt(str[i], 10))) {
                    onlynumber += str[i];
                }
            }
            return onlynumber;
        }
        return 0;
    },

}