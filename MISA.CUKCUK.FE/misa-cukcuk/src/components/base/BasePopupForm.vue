<template>
  <div class="m-popup-form">
    <div class="pop-form-container" :type="type">
      <div class="pop-form-header">
        <div class="title-form">{{ nameForm }}</div>
        <div class="tool-form">
          <div class="tool-sprites x-tool-close" @click="closeOnChange"></div>
        </div>
      </div>
      <div class="pop-form-content">
        <div class="pop-form-detail" v-if="type == 'unit'">
          <BaseLabel title="" label="Đơn vị tính">
            <BaseInput
              ref="inputUnitName"
              id="txtUnitName"
              type="text"
              fieldType="UnitName"
              displayName="Đơn vị tính"
              value=""
              placeholder=""
              :required="true"
              tabindex="14"
              :index="3"
              v-model="unitDetail.UnitName"
              @error="arrayError"
            />
            <!-- @error="changeError" -->
            <div class="x-form-error" v-if="isErrorForm[3] == true">
              <div class="sprite icon-form-invalid"></div>
            </div>
          </BaseLabel>

          <BaseLabel label="Diễn giải">
            <div class="field-textarea">
              <textarea
                ref="inputDescription"
                id="txtDescription"
                type="textare"
                fieldType="description"
                displayName="Diễn giải"
                value=""
                name=""
                cols="30"
                rows="10"
                tabindex="15"
                v-model="unitDetail.Description"
              ></textarea>
            </div>
          </BaseLabel>
        </div>
        <div
          class="pop-form-detail"
          title="Trường này không được để trống"
          v-if="type == 'stock'"
        >
          <BaseLabel title="" :required="true" label="Mã kho">
            <BaseInput
              ref="inputStockCode"
              id="txtStockName"
              type="text"
              fieldType="StockCode"
              displayName="Mã kho"
              value=""
              placeholder=""
              :required="true"
              tabindex="14"
              :index="4"
              @error="arrayError"
              v-model="stockDetail.StockCode"
            />
            <div
              class="x-form-error"
              title="Trường này không được để trống"
              v-if="isErrorForm[4] == true"
            >
              <div class="sprite icon-form-invalid"></div>
            </div>
          </BaseLabel>
          <BaseLabel title="" :required="true" label="Tên kho">
            <BaseInput
              ref="inputStockName"
              id="txtStockName"
              type="text"
              fieldType="StockName"
              displayName="Tên kho"
              v-model="stockDetail.StockName"
              placeholder=""
              :required="true"
              tabindex="15"
              :index="5"
              @error="arrayError"
            />
            <div
              class="x-form-error"
              title="Trường này không được để trống"
              v-if="isErrorForm[5] == true"
            >
              <div class="sprite icon-form-invalid"></div>
            </div>
          </BaseLabel>
          <BaseLabel label="Diễn giải">
            <div class="field-textarea" tabindex="10">
              <textarea
                ref="inputDescription"
                id="txtDescription"
                type="textarea"
                fieldType="description"
                displayName="Diễn giải"
                name=""
                cols="30"
                rows="10"
                tabindex="16"
                v-model="stockDetail.Description"
              ></textarea>
            </div>
          </BaseLabel>
        </div>
      </div>

      <div class="pop-form-footer">
        <div class="btn-footer-help">
          <BaseButton type="default" iconClass="sprite icon-btnHelp">
            Giúp
          </BaseButton>
        </div>
        <div class="btn-footer-form">
          <BaseButton
            type="default"
            iconClass="sprite icon-btnSave"
            @btn-click="btnSaveForm"
          >
            Cất
          </BaseButton>
          <BaseButton type="default" iconClass="sprite icon-btnCancel-red">
            Hủy bỏ
          </BaseButton>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
import BaseButton from "./BaseButton.vue";
import BaseLabel from "./BaseLabel.vue";
import BaseInput from "./BaseInput.vue";
import { MESSAGE } from "../../js/common/const";
import { STATUS_CODE } from "../../js/common/enums";
import axios from "axios";
import { CONFIG } from "../../js/common/config";

export default {
  components: {
    BaseButton,
    BaseLabel,
    BaseInput,
  },
  props: {
    open: {
      type: Boolean,
      default: false,
    },
    icon: String,

    isShow: {
      type: Boolean,
      default: false,
    },
    type: {
      type: String,
      default: "1",
    },
    
    isConfirm: {
      type: Boolean,
      default: false,
    }
    //isErrorForm: Array,
  },
  mounted() {
    this.focusFirstControl();
  },
  watch: {
    type(value) {
      if (value == "unit") {
        this.nameForm = "Thêm Đơn vị tính";
      } else {
        this.nameForm = "Thêm Kho";
      }
    },
    isConfirm(value){
      alert(1);
      if(value == true){
        this.btnSaveForm();
        this.$emit("resetIsConfirm");
      }
      
    }
  },
  data() {
    return {
      nameForm: "Thêm Đơn vị tính",

      originalUnitForm: {
        UnitName: "",
        Description: "",
      },

      originalStockForm: {
        StockCode: "",
        StockName: "",
        Description: "",
      },

      unitDetail: { UnitName: "", Description: "" },

      stockDetail: { StockCode: "", StockName: "", Description: "" },

      invalidRef: [],

      isErrorForm: [],
    };
  },
  created() {},
  methods: {
    /**
     * Hàm thực hiện lưu dữ liệu
     */
    btnSaveForm() {
      this.invalidRef = [];
      Object.values(this.$refs).forEach((el) => {
        if (typeof el.validateInput === "function") {
          let valid = el.validateInput(el.value);
          if (el.value == null || el.value == "") {
            valid = el.validateInput(el.$el.querySelector("input").value);
          }
          if (valid === false) {
            this.invalidRef.push(el);
          }
        }
      });

      //Kiểm tra những input không hợp lệ

      if (this.invalidRef.length > 0) {
        if (typeof this.invalidRef[0].focus === "function") {
          this.invalidRef[0].focus();
        }
        return false;
      } else {
        return this.sendDetails();
      }
    },

    /**
     * Hàm thực hiện lưu dữ liệu vào database
     * CreateBy: TTUyen(01/09/2021)
     */

    async sendDetails() {
      try {
        switch (this.type) {
          case "unit":
            await axios
              .post(CONFIG.MY_URL + "Units/", this.unitDetail)
              .then((res) => {
                if (res.status != STATUS_CODE.NO_CONTENT) {
                  this.$toast.success(MESSAGE.ADD_MSG_SUCCESS, {
                    position: "bottom-right",
                    timeout: 2000,
                  });
                  this.$emit("reloadDataUnitAndStock");
                  this.$emit("closePopForm");
                }
              })
              .catch((res) => {
                this.$emit(
                  "showPopupWarningServer",
                  res.response.data.Data.userMsg[0]
                );
              });
            break;
          case "stock":
            await axios
              .post(CONFIG.MY_URL + "Stocks/", this.stockDetail)
              .then((res) => {
                console.log(res.data);
                if (res.status != STATUS_CODE.NO_CONTENT) {
                  this.$toast.success(MESSAGE.ADD_MSG_SUCCESS, {
                    position: "bottom-right",
                    timeout: 2000,
                  });
                  this.$emit("reloadDataUnitAndStock");
                  this.$emit("closePopForm");
                }
              })
              .catch((res) => {
                this.$emit(
                  "showPopupWarningServer",
                  res.response.data.Data.userMsg[0]
                );
              });
            break;
          default:
            break;
        }
      } catch (err) {
        console.log(err);
        this.$toast.error(MESSAGE.EXCEPTION_MSG, {
          position: "bottom-right",
          timeout: 2000,
        });
      }
      this.$emit("reloadDataUnitAndStock");
    },

    /**
     * Thực hiện focus khi mở form
     */
    focusFirstControl() {
      if (this.$refs.inputUnitName) {
        this.$refs.inputUnitName.focus();
      }
      if (this.$refs.inputStockCode) {
        this.$refs.inputStockCode.focus();
      }
    },

    checkChangeForm(original, present) {
      let isChange = false;
      Object.entries(original).forEach(([key]) => {
        if (original[key] !== present[key]) {
          isChange = true;
        }
      });
      if (isChange) {
        this.$emit("saveChangesPopupShow", "form");
      } else {
        this.closePopForm();
      }
    },
    /**
     * Kiểm tra thay đổi trc khi đóng form
     */
    closeOnChange() {
      if (this.type == "unit") {
        this.checkChangeForm(this.originalUnitForm, this.unitDetail);
      }
      if (this.type == "stock") {
        this.checkChangeForm(this.originalStockForm, this.stockDetail);
      }
    },
    /**
     * Đóng popup
     * CreateBy: TTUyen (30/8/2021)
     */
    closePopForm() {
      this.$emit("closePopForm");
      // this.$emit("errorForm", false, 3);
      // this.$emit("errorForm", false, 4);
      // this.$emit("errorForm", false, 5);
    },

    /**
     * Hủy popup
     * CreateBy: TTUyen (30/8/2021)
     */
    cancelPopup() {
      this.$emit("cancel");
    },

    /**
     * Xử lý bấm xác nhận
     * CreateBy: TTUyen (30/8/2021)
     */
    confirmPopup() {
      this.$emit("confirm");
    },

    // /**
    //  * Hàm gửi emit hiển thị cảnh báo khi validate
    //  */
    // changeError(isShow, value) {
    //   this.$emit("errorForm", isShow, value);
    // },

    /**
     * Hiển thị lỗi chưa validate dữ liệu
     * CreateBy: TTUyen(05/10/2021)
     */

    arrayError(isShow, value) {
      if (isShow == true) {
        this.$set(this.isErrorForm, value, true);
      } else {
        this.$set(this.isErrorForm, value, false);
      }
    },
  },
};
</script>
<style lang="scss" scoped>
@import "../../css/base/popup-form.css";

.pop-form-container[type="unit"] {
  height: 170px;
}

.pop-form-container[type="stock"] {
  height: 200px;
}
</style>