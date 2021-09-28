<template>
  <div class="m-popup-form" :class="{ 'hide-form': !isShow }">
    <div class="pop-form-container">
      <div class="pop-form-header">
        <div class="title-form">{{ nameForm }}</div>
        <div class="tool-form">
          <div class="tool-sprites x-tool-close"></div>
        </div>
      </div>
      <div class="pop-form-content">
        <div class="pop-form-detail" v-if="typeForm == 'unit'">
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
              tabindex="1"
            />
            <div class="x-form-error" v-if="isError[0] == true">
              <div class="sprite icon-form-invalid"></div>
            </div>
          </BaseLabel>
          <BaseLabel label="Diễn giải">
            <textarea
              ref="inputDescription"
              id="txtDescription"
              type="textare"
              fieldType="description"
              displayName="Diễn giải"
              value=""
              tabindex="7"
              name=""
              cols="30"
              rows="10"
            ></textarea>
          </BaseLabel>
        </div>
        <div class="pop-form-detail" v-if="typeForm == 'stock'">
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
              tabindex=""
            />
            <div class="x-form-error" v-if="isError[0] == true">
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
              tabindex=""
            />
            <div class="x-form-error" v-if="isError[0] == true">
              <div class="sprite icon-form-invalid"></div>
            </div>
          </BaseLabel>
          <BaseLabel label="Diễn giải">
            <textarea
              ref="inputDescription"
              id="txtDescription"
              type="textare"
              fieldType="description"
              displayName="Diễn giải"
              value=""
              tabindex="7"
              name=""
              cols="30"
              rows="10"
            ></textarea>
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
    typeForm: {
      type: String,
      default: "1",
    },
    unitDetail: [
      {
        UnitName: "",
        Description: "",
      },
    ],
  },

  watch: {
    typeForm(value) {
      if (value == "unit") {
        this.nameForm = "Thêm Đơn vị tính";
      } else {
        this.nameForm = "Thêm Kho";
      }
    },
  },
  data() {
    return {
      isError: [false, false],
      nameForm: "Thêm Đơn vị tính",
    };
  },
  created() {
    // this.typeForm = "sds";
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
@import "../../css/base/popup-form.css";
.hide-form {
  display: none;
}
</style>