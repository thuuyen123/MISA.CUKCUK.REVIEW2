<template>
  <div class="m-form">
    <div class="form-container">
      <div class="form-header">Thêm mới vị tính</div>
      <div class="form-content">

      </div>
      <div class="pop-option">
        <div>
          <BaseButton
            type="control"
            id="pop-3"
            @btn-click="cancelPopup"
          >
            
          </BaseButton>
          <BaseButton
            type="control"
            @btn-click="confirmPopup"
            id="pop-1"
          >
           
          </BaseButton>
        </div>
        <BaseButton
          type="control"
          @btn-click="closePopup"
          id="pop-2"
        >
          
        </BaseButton>
      </div>
    </div>
  </div>
</template>
<script>
import BaseButton from "../base/BaseButton.vue";
export default {
  components: {
    BaseButton,
  },
  props: {
    open: {
      type: Boolean,
      default: false,
    },
    icon: String,
  },
  methods: {
    /**
     * Đóng popup
     * CreateBy: TTUyen (30/8/2021)
     */
    closePopup() {
      this.$emit("close");
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
  },
};
</script>
<style lang="scss" scoped>
@import "../../css/base/popup.css";

</style>