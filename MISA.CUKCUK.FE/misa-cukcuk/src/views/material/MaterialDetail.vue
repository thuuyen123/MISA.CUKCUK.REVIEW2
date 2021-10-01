<template>
  <div class="dialog-detail" v-if="!status.isHide">
    <div class="m-model">
      <div class="dialog-container">
        <div class="dialog-form-header">
          <div class="title-dialog">Thêm Nguyên vật liệu</div>
          <div class="tool-form">
            <div class="tool-sprites icon-tool-zoom"></div>
            <div class="tool-sprites x-tool-close"></div>
          </div>
        </div>
        <div class="dialog-form-content">
          <div class="form-detail">
            <div class="form-block-1">
              <BaseLabel title="" :required="true" label="Tên">
                <BaseInput
                  ref="inputMaterialName"
                  id="txtMaterialName"
                  type="text"
                  fieldType="materialName"
                  displayName="Tên"
                  value=""
                  placeholder=""
                  :required="true"
                  v-model="materialDetail.MaterialName"
                  tabindex="1"
                />
                <div class="x-form-error" v-if="isError[0] == true">
                  <div class="sprite icon-form-invalid"></div>
                </div>
              </BaseLabel>
              <BaseLabel :required="true" label="ĐVT" title="Đơn vị tính">
                <div class="form-combo-error">
                  <BaseCombobox
                    type="cbxform"
                    ref="inputUnit"
                    id="txtUnit"
                    fieldType="unit"
                    displayName="Đơn vị tính"
                    value=""
                    itemId="UnitId"
                    itemName="UnitName"
                    :required="true"
                    tabindex="3"
                    @btnShowFormAdd="btnShowFormAdd"
                  />
                  <div class="x-form-error" v-if="isError[1] == true">
                    <div class="sprite icon-form-invalid"></div>
                  </div>
                </div>
              </BaseLabel>
              <BaseLabel label="Hạn sử dụng">
                <div class="form-input-combo">
                  <BaseInput
                    ref="inputExpiryDate"
                    id="txtExpiryDate"
                    type="text"
                    fieldType="expiryDate"
                    displayName="Hạn sử dụng"
                    value=""
                    placeholder=""
                    v-model="materialDetail.ExpiryDate"
                    tabindex="4"
                  />
                  <BaseCombobox
                    type="combo"
                    ref="inputExpriryType"
                    id="txtExpriryType"
                    fieldType="expriryType"
                    displayName="Kiểu hạn sử dụng"
                    value=""
                    itemId="ExpriryType"
                    itemName="ExpriryTypeName"
                    tabindex="5"
                  />
                </div>
              </BaseLabel>
            </div>
            <div class="form-block-2">
              <BaseLabel :required="true" label="Mã">
                <BaseInput
                  ref="inputMaterialCode"
                  id="txtMaterialCode"
                  type="text"
                  fieldType="materialCode"
                  displayName="Mã"
                  value=""
                  placeholder=""
                  :required="true"
                  v-model="materialDetail.MaterialCode"
                  tabindex="2"
                />
                <div class="x-form-error" v-if="isError[2] == true">
                  <div class="sprite icon-form-invalid"></div>
                </div>
              </BaseLabel>
              <BaseLabel label="Kho ngầm định">
                <BaseCombobox
                  type="cbxform"
                  ref="inputStock"
                  id="txtStock"
                  fieldType="stock"
                  displayName="Kho ngầm định"
                  value=""
                  itemId="StockId"
                  itemName="StockName"
                  tabindex="4"
                  @btnShowFormAdd="btnShowFormAdd"
                />
              </BaseLabel>
              <BaseLabel
                title="Số lượng tồn tối thiểu"
                label="SL tồn tối thiểu"
              >
                <BaseInput
                  ref="inputMinimumStock"
                  id="txtMinimumStock"
                  type="text"
                  fieldType="minimumStock"
                  displayName="Số lượng tồn tối thiểu"
                  value=""
                  placeholder=""
                  v-model="materialDetail.MinimumStock"
                  tabindex="6"
                />
              </BaseLabel>
            </div>
            <div class="form-block-3">
              <BaseLabel label="Mô tả">
                <textarea
                  ref="inputDescription"
                  id="txtDescription"
                  type="textare"
                  fieldType="description"
                  displayName="Mô tả"
                  value=""
                  v-model="materialDetail.Description"
                  tabindex="7"
                  name=""
                  cols="30"
                  rows="10"
                ></textarea>
              </BaseLabel>
            </div>
          </div>
          <div class="form-table">
            <div class="form-table-tabbar">
              <div class="box-tabbar">
                <span>Đơn vị chuyển đổi</span>
              </div>
            </div>
            <div class="form-table-body">
              <base-table ref="tableUnit" :tableData="tableUnitConvert" />
            </div>
            <div class="form-table-btn">
              <BaseButton
                type="default"
                @btn-click="btnAddTrTable"
                iconClass="sprite icon-btnAddition"
              >
                Thêm dòng
              </BaseButton>
              <BaseButton type="default" iconClass="sprite icon-btnDelete">
                Xóa dòng
              </BaseButton>
            </div>
          </div>
        </div>
        <div class="dialog-form-footer">
          <div class="btn-footer-help">
            <BaseButton type="default" iconClass="sprite icon-btnHelp">
              Giúp
            </BaseButton>
          </div>
          <div class="btn-footer-form">
            <BaseButton type="default" iconClass="sprite icon-btnSave">
              Cất
            </BaseButton>
            <BaseButton type="default" iconClass="sprite icon-btnSaveAdd">
              Cất và thêm
            </BaseButton>
            <BaseButton type="default" iconClass="sprite icon-btnCancel-red">
              Hủy bỏ
            </BaseButton>
          </div>
        </div>
      </div>
    </div>
    <BasePopupForm :isShow="isShowForm" :typeForm="fieldTypeForm" />
    <BasePopup
      v-if="saveChangesPopupShow"
      btn1="Có"
      btn2="Hủy"
      btn3="Không"
      @close="closeSaveChangesPopup"
      @confirm="confirmSaveChangesPopup"
      @cancel="cancelSaveChangesPopup"
      type="confirm-add"
      icon="icon-info"
    >
      {{ saveChangesPopupMessage }}
    </BasePopup>

    <BasePopup
      v-if="isNotifyInValidPopShow"
      btn1="Đóng"
      @confirm="onNotifyInValidPopShowConfirm"
      type="alert"
      icon="icon-notify"
    >
      {{ notifyInvalidMsg }}
    </BasePopup>

    <BasePopup
      v-if="isWarningServerPopShow"
      btn1="Đồng ý"
      @confirm="isWarningServerPopShow = false"
      type="warning"
      icon="icon-warning"
    >
      {{ warningServerMsg }}
    </BasePopup>
  </div>
</template>
<script>
import BaseLabel from "../../components/base/BaseLabel.vue";
import BaseInput from "../../components/base/BaseInput.vue";
import BaseCombobox from "../../components/base/BaseCombobox.vue";
import BaseTable from "../../components/base/BaseTable.vue";
import BasePopupForm from "../../components/base/BasePopupForm.vue";
import BasePopup from "../../components/base/BasePopup.vue";
import BaseButton from "../../components/base/BaseButton.vue";
// import { MESSAGE } from "../../js/common/const";
// import { FORM_STATE, STATUS_CODE } from "../../js/common/enums";
export default {
  name: "MaterialDetail",
  components: {
    BaseLabel,
    BaseInput,
    BaseCombobox,
    BaseTable,
    BaseButton,
    BasePopup,
    BasePopupForm,
  },
  props: {
    status: {
      isHide: Boolean,
      formMode: String,
      data: [],
    },
  },
  data() {
    return {
      isShowForm: false,
      //hiển popup xác nhận
      saveChangesPopupShow: false,

      //thông báo có lỗi
      saveChangesPopupMessage: "",

      //hiện cảnh báo lỗi trường thông tin
      isNotifyInValidPopShow: false,

      //nội dung cảnh báo lỗi trường thông tin
      notifyInvalidMsg: "",

      //hiện thị thông báo lỗi từ server
      isWarningServerPopShow: false,

      //Nội dung thông báo lõi
      warningServerMsg: "",

      originalEmployeeRecord: {},

      countRows: 0,

      isError: [false, false, false],
      materialDetail: {
        MaterialCode: "",
        MaterialName: "",
      },
      tableUnitConvert: {
        columns: [
          {
            id: "NumberHead",
            style: "min-width: 10px; max-width: 20px !important;  ",
            fieldname: "Number",
            title: "STT",
            typeFilter: "span",
            idType: "spNumber",
          },
          {
            id: "UnitConvertHead",
            style: "min-width: 90px; max-width:100px !important; ",
            fieldname: "UnitConvert",
            title: "Đơn vị chuyển đổi",
            typeFilter: "comboAdd",
            idType: "cbxUnitConvert",
          },
          {
            id: "ConvertRateHead",
            style: "min-width: 50px; max-width: 80px !important",
            fieldname: "ConvertRate",
            title: "Tỷ lệ chuyển đổi",
            typeFilter: "input",
            idType: "txtConvertRate",
          },
          {
            id: "ConvertRateOperateHead",
            style: "min-width: 40px;max-width: 60px !important;",
            fieldname: "ConvertRateOperate",
            title: "Phép tính",
            typeFilter: "combo",
            idType: "cbxConvertRateOperate",
          },
          {
            id: "DescriptionHead",
            style: "min-width: 60px; max-width: 100px !important;",
            fieldname: "Description",
            title: "Mô tả",
            typeFilter: "span",
            idType: "spDescription",
          },
        ],
        data: [
          // {
          //   MaterialCode: "COCACOLA",
          //   MaterialName: "Cooca côla",
          //   PropertiesOfMaterials: "Đồ uống",
          //   UnitName: "Lon",
          //   MaterialGroup: "Sản phẩm",
          //   Note: "Oke",
          //   StopUsing: 1,
          // },
        ],
        name: "tbUnitConvert",
      },

      fieldTypeForm: "unit",
    };
  },
  methods: {
    btnShowFormAdd(field) {
      this.fieldTypeForm = field;
      this.isShowForm = true;
    },
    btnAddTrTable() {
      this.countRows = this.countRows + 1;
      let newRow = {
        Number: this.countRows,
        UnitConvert: "",
        ConvertRate: "1.0",
        ConvertRateOperate: "*",
        Description: "",
      };
      this.tableUnitConvert.data.push(newRow);
    },
  },
};
</script>
<style lang="scss" scoped>
@import "../../css/view/material/material-detail.css";
.dialog-form-content {
  .combobox-header-wrapper {
    border-radius: 0px !important;
  }
  .form-block-1 {
    > .label-field-input:nth-child(1) {
      flex-basis: 100%;
      .field-input {
        flex-grow: 1;
      }
    }
    > .label-field-input:nth-child(2) {
      flex-basis: 100%;
      .form-combo-error {
        display: flex;
        flex-basis: 70%;
        .combobox {
          flex-basis: 40%;
        }
        .x-form-error {
          flex-basis: 10%;
        }
      }
    }

    > .label-field-input:nth-child(3) {
      flex-basis: 100%;
      .form-input-combo {
        display: flex;
        flex-grow: 1;
        flex-basis: 70%;
        justify-content: flex-start;
        .field-input {
          flex-basis: 43%;
          padding-right: 8px;
        }
        .combobox {
          flex-basis: 40% !important;
        }
      }
    }
  }
  .form-block-2 {
    > .label-field-input:nth-child(1) {
      flex-basis: 100%;
      .field-input {
        flex-grow: 1;
      }
    }
    > .label-field-input:nth-child(2) {
      flex-basis: 100%;
      .combobox {
        flex-grow: 1;
      }
    }

    > .label-field-input:nth-child(3) {
      flex-basis: 100%;
      .field-input {
        flex-basis: 28%;
      }
    }
  }
  .form-block-3 {
    > .label-field-input:nth-child(1) {
      flex-basis: 100%;
      textarea {
        flex: 1;
      }
    }
  }
}
</style>