<template>
  <div class="m-popup-form">
    <div class="pop-form-container" :type="type">
      <div class="pop-form-header">
        <div class="title-form">{{ nameForm }}</div>
        <div class="tool-form">
          <div class="tool-sprites x-tool-close" @click="closePopup"></div>
        </div>
      </div>
      <div class="pop-form-content">
        <div class="pop-form-detail" v-if="type == 'unit'">
          <BaseLabel title="" :required="true" label="Đơn vị tính">
            <BaseInput
              ref="inputUnitName"
              id="txtUnitName"
              type="text"
              fieldType="UnitName"
              displayName="Đơn vị tính"
              value=""
              placeholder=""
              :required="true"
              tabindex="8"
              :index="3"
              @error="changeError"
            />
            <div class="x-form-error" v-if="isErrorForm[3] == true">
              <div class="sprite icon-form-invalid"></div>
            </div>
          </BaseLabel>
          <BaseLabel label="Diễn giải">
            <div class="field-textarea" tabindex="9">
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
              tabindex="8"
              :index="4"
              @error="changeError"
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
              value=""
              placeholder=""
              :required="true"
              tabindex="9"
              :index="5"
              @error="changeError"
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
                value=""
                name=""
                cols="30"
                rows="10"
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
          <BaseButton type="default" iconClass="sprite icon-btnSave">
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
// import { eventBus } from "../../main.js";

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
    unitDetail: [
      {
        UnitName: "",
        Description: "",
      },
    ],
    isErrorForm: Array,
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
    // isErrorForm(value){
    //  for (let i = 0; i < value.length; i++) {
    //    if(value[i] == true) this.$set(this.isErrorForm, i, true);
    //    else{
    //      this.$set(this.isErrorForm, i, false);
    //    }
    //  }
    // }
  },
  data() {
    return {
      nameForm: "Thêm Đơn vị tính",
    };
  },
  created() {},
  methods: {
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
    /**
     * Đóng popup
     * CreateBy: TTUyen (30/8/2021)
     */
    closePopup() {
      this.$emit("close");
      this.$emit("errorForm", false, 3);
      this.$emit("errorForm", false, 4);
      this.$emit("errorForm", false, 5);
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

    changeError(isShow, value) {
      this.$emit("errorForm", isShow, value);
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