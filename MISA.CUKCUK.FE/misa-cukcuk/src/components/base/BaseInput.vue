<template>
  <div
    class="field-input"
    :class="{
      'input-border-red': false,
    }"
    :id="id"
    :title="tooltip"
  >
    <!-- !(isRequiredValid && isValid) && !focusing -->
    <input
      ref="input"
      :value="value"
      @input="onInput($event.target.value)"
      @keyup.enter="inputSearchOnEnter"
      @blur="onBlur($event.target.value)"
      @focus="onInputFocus"
      v-bind="$attrs"
      class="form-input-item"
    />
  </div>
</template>
<script>
import { MESSAGE } from "../../js/common/const";
// import { CommonFn } from "../../js/common/common";
export default {
  name: "BaseInput",

  data() {
    return {
      //Nội dung title khi validate dữ liệu
      tooltip: "",

      //input được focus
      focusing: false,

      // Kiểm tra đã hợp lệ chưa
      isValid: true,

      // Kiểm tra required không rỗng
      isRequiredValid: true,
    };
  },
  props: {
    value: String, Number,
    id: String,
    required: {
      type: Boolean,
      default: false,
    },
    fieldType: {
      type: String,
    },
    displayName: {
      type: String,
      default: "",
    },
  },
  watch: {},
  methods: {
    /**
     * Hàm focus
     * CreateBy: TTUyen (30/8/2021)
     *
     */
    onInputFocus() {
      this.focusing = true;
    },
    /**
     * Hàm lọc theo input tìm kiếm
     * CreateBy: TTUyen (30/8/2021)
     */
    inputSearchOnEnter() {
      this.$emit("search");
    },
    /**
     * Hàm nhập input
     * CreateBy: TTUyen (30/8/2021)
     */
    onInput(value) {
      let me = this;
      me.$emit("input", value);
      me.validateInput(value);
    },

    /**
     * Hàm xử lý dữ liệu khi blur input
     * CreateBy: TTUyen (30/8/2021)
     */
    onBlur(value) {
      this.focusing = false;
      let me = this;
      me.validateInput(value);
    },

    /**
     * Hàm định dạng dữ liệu
     * CreateBy: TTUyen (30/8/2021)
     */
    validateInput(value) {
      let me = this;
      //Kiểm tra nhập các trường bắt buộc
      if (!me.required && value == "") {
        me.isValid = true;
        me.isRequiredValid = true;
        return;
      }

      //Rỗng và bắt buộc
      if (value == "" && me.required) {
        me.isRequiredValid = false;
        me.tooltip = MESSAGE.CANT_BE_NULL.format(me.displayName);
        return;
      } else {
        me.isRequiredValid = true;
      }

      // switch (me.fieldType) {
      //   case "email":
      //     me.isValid = CommonFn.isEmail(value);
      //     me.tooltip = me.isValid ? "" : MESSAGE.INVALID_EMAIL;
      //     break;
      // }
      if (me.isRequiredValid && me.isValid) {
        me.tooltip = "";
      }
    },
  },
};
</script>
<style scoped>
@import "../../css/base/input.css";
</style>