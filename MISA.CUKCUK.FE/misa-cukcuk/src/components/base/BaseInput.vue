<template>
  <div class="field-input" :id="id" :title="tooltip" :tabindex="tabindex">
    <!-- !(isRequiredValid && isValid) && !focusing -->
    <input
      :class="{
        'input-border-red': !(isRequiredValid && isValid) && !focusing,
      }"
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
    value: {
      type: [String, Number],
    },
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
    index: Number,
    tabindex: String,
  },
  created() {
    this.setDefault();
  },
  watch: {},
  methods: {
    setDefault() {
      if (this.fieldType == "convertRate") {
        this.value = 0;
      }
    },
    focus() {
      if (this.$refs.input) {
        this.$refs.input.focus();
      }
    },
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
      me.sendNewCode(value);
    },

    /**
     * Hàm xử lý dữ liệu khi blur input
     * CreateBy: TTUyen (30/8/2021)
     */
    onBlur(value) {
      this.focusing = false;
      let me = this;
      me.validateInput(value);
      me.sendNewCode(value);
    },

    /**
     * Hàm định dạng dữ liệu
     * CreateBy: TTUyen (30/8/2021)
     */
    validateInput(value) {
      let me = this;
      let valid = true;
      me.focusing = false;
      if (me.required) {
        if (value === "" || value === undefined || value === null) {
          valid = false;
        }
      }
      if (valid) {
        me.isRequiredValid = true;
        me.$emit("error", false, me.index);
      } else {
        me.isRequiredValid = false;
        me.tooltip = MESSAGE.CANT_BE_NULL.format(me.displayName);
        me.$emit("error", true, me.index);
      }
      return valid;
    },

    /**
     * Gửi yc sinh mã mớ
     */
    sendNewCode(value) {
      if (this.fieldType == "materialName") {
        setTimeout(() => {
          this.$emit("handleNewCode", value);
        }, 500);
      }
    },
  },
};
</script>
<style scoped>
@import "../../css/base/input.css";
</style>