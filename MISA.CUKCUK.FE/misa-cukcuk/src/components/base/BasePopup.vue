<template>
  <div class="m-popup">
    <div class="popup-container">
      <div class="pop-header">CUKCUK - Quản lý nhà hàng</div>
      <div class="pop-content">
        <div class="pop-icon-container">
          <div class="pop-icon sprite" :class="icon"></div>
        </div>
        <div class="pop-text">
          <p>
            <slot></slot>
          </p>
        </div>
      </div>
      <div class="pop-option">
        <div>
          <BaseButton
            type="control"
            id="pop-3"
            :tabindex="2"
            @btn-click="cancelPopup"
          >
            {{ $attrs.btn3 }}
          </BaseButton>
          <BaseButton
            type="control"
            @btn-click="confirmPopup"
            id="pop-1"
            :tabindex="3"
          >
            {{ $attrs.btn1 }}
          </BaseButton>
        </div>
        <BaseButton
          type="control"
          @btn-click="closePopup"
          id="pop-2"
          :tabindex="1"
        >
          {{ $attrs.btn2 }}
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
      this.$emit("btnClose");
    },

    /**
     * Hủy popup
     * CreateBy: TTUyen (30/8/2021)
     */
    cancelPopup() {
      this.$emit("btnCancel");
    },

    /**
     * Xử lý bấm xác nhận
     * CreateBy: TTUyen (30/8/2021)
     */
    confirmPopup() {
      this.$emit("btnConfirm");
    },
  },
};
</script>
<style lang="scss" scoped>
@import "../../css/base/popup.css";
.m-popup[type="alert"] {
  .pop-option {
    justify-content: center;
    #pop-2,
    #pop-3 {
      display: none;
    }
  }
}
.m-popup[type="warning"] {
  .pop-option {
    justify-content: flex-end;
  }

  #pop-2,
  #pop-3 {
    display: none;
  }
}

.m-popup[type="confirm-del"] {
  #pop-3 {
    display: none;
  }
}
</style>