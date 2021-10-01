<template>
  <div class="main-content">
    <div class="content-title">
      <div class="content-title-text">Nguyên vật liệu</div>
      <div class="content-title-button">
        <BaseButton type="default" iconClass="icon-feedback">
          Phản hồi
        </BaseButton>
      </div>
    </div>
    <div class="content-body">
      <div id="formMaterialList" class="m-panel-list-body">
        <div class="m-toolbar grid-toolbar">
          <BaseButton
            @btn-click="changeStatusForm"
            type="control"
            iconClass="sprite icon-btnAddition"
          >
            Thêm
          </BaseButton>
          <BaseButton type="control" iconClass="sprite icon-btnDuplicate">
            Nhân bản
          </BaseButton>
          <BaseButton type="control" iconClass="icon-btnEdit"> Sửa </BaseButton>
          <BaseButton
            type="control"
            iconClass="sprite icon-btnDelete"
            @btn-click="btnShowDelete"
          >
            Xóa
          </BaseButton>
          <BaseButton type="control" iconClass="sprite icon-refresh">
            Nạp
          </BaseButton>
        </div>
        <div class="m-panel main-gridpanel">
          <BaseTable ref="tableMaterial" :tableData="tableMaterial" />
        </div>
          <BasePaging />
      </div>
    </div>
    <MaterialDetail :status="recordStatus" />
    <BasePopup
      v-if="!isHidePopupDelete"
      btn1="Có"
      btn2="Không"
      type="confirm-del"
      icon="icon-question"
      @close="btnCancleDelete"
      @confirm="btnConfirmDelete"
    >
      {{ contentTextDel }}
    </BasePopup>
  </div>
</template>
<script>
import BaseButton from "../../components/base/BaseButton.vue";
import BaseTable from "../../components/base/BaseTable.vue";
import BasePopup from "../../components/base/BasePopup.vue";
import BasePaging from "../../components/base/BasePaging.vue";
import { FORM_STATE } from "../../js/common/enums";
import { MESSAGE } from "../../js/common/const";
import { MATERIAL_TEXT } from "../../js/common/enums";
import MaterialDetail from "./MaterialDetail.vue";

export default {
  name: "MaterialList",
  components: {
    BaseButton,
    BaseTable,
    MaterialDetail,
    BasePopup,
    BasePaging,
  },
  data() {
    return {
      tableMaterial: {
        columns: [
          {
            id: "MaterialCodeHead",
            style: "min-width: 100px; max-width: 150px !important;  ",
            fieldname: "MaterialCode",
            title: "Mã nhân viên",
            typeFilter: "input",
            idType: "txtMaterialCode",
          },
          {
            id: "MaterialNameHead",
            style: "min-width: 120px; max-width:170px !important; ",
            fieldname: "MaterialName",
            title: "Họ và tên",
            subClass: "background-color-white",
            typeFilter: "input",
            idType: "txtMaterialName",
          },
          {
            id: "PropertiesOfMaterialsHead",
            style: "min-width: 100px; max-width: 150px !important",
            fieldname: "PropertiesOfMaterials",
            title: "Tính chất",
            typeFilter: "combo",
            idType: "cbxPropertiesOfMaterials",
          },
          {
            id: "UnitHead",
            style: "min-width: 60px;max-width: 100px !important;",
            fieldname: "UnitName",
            title: "Đơn vị tính",
            typeFilter: "input",
            idType: "txtUnitName",
          },
          {
            id: "MaterialGroupHead",
            style: "min-width: 100px; max-width: 150px !important;",
            fieldname: "MaterialGroup",
            title: "Nhóm nguyên vật liệu",
            typeFilter: "input",
            idType: "txtMaterialGroup",
          },
          {
            id: "NoteHead",
            style: "min-width: 100px; max-width:190px !important;",
            fieldname: "Note",
            title: "Ghi chú",
            idType: "txtNote",
            typeFilter: "input",
          },
          {
            id: "StopUsingHead",
            style: "min-width: 50px; max-width: 140px !important",
            fieldname: "StopUsing",
            title: "Ngừng theo dõi",
            typeFilter: "combo",
            idType: "cbxStopUsing",
          },
        ],
        data: [
          {
            MaterialCode: "COCACOLA",
            MaterialName: "Cooca côla",
            PropertiesOfMaterials: "Đồ uống",
            UnitName: "Lon",
            MaterialGroup: "Sản phẩm",
            Note: "Oke",
            StopUsing: 1,
          },
          {
            MaterialCode: "COCACOLA",
            MaterialName: "Cooca côla",
            PropertiesOfMaterials: "Đồ uống",
            UnitName: "Lon",
            MaterialGroup: "Sản phẩm",
            Note: "Oke",
            StopUsing: 0,
          },
          {
            MaterialCode: "COCACOLA",
            MaterialName: "Cooca côla",
            PropertiesOfMaterials: "Đồ uống",
            UnitName: "Lon",
            MaterialGroup: "Sản phẩm",
            Note: "Oke",
            StopUsing: 0,
          },
          {
            MaterialCode: "COCACOLA",
            MaterialName: "Cooca côla",
            PropertiesOfMaterials: "Đồ uống Đồ uống Đồ uống Đồ uống Đồ uống",
            UnitName: "Lon",
            MaterialGroup: "Sản phẩm",
            Note: "Oke",
            StopUsing: 0,
          },
          {
            MaterialCode: "COCACOLA",
            MaterialName: "Cooca côla",
            PropertiesOfMaterials: "Đồ uống",
            UnitName: "Lon",
            MaterialGroup: "Sản phẩm",
            Note: "Oke",
            StopUsing: 0,
          },
          {
            MaterialCode: "COCACOLA",
            MaterialName: "Cooca côla",
            PropertiesOfMaterials: "Đồ uống",
            UnitName: "Lon",
            MaterialGroup: "Sản phẩm",
            Note: "Oke",
            StopUsing: 0,
          },
          {
            MaterialCode: "COCACOLA",
            MaterialName: "Cooca côla",
            PropertiesOfMaterials: "Đồ uống",
            UnitName: "Lon",
            MaterialGroup: "Sản phẩm",
            Note: "Oke",
            StopUsing: 0,
          },
          {
            MaterialCode: "COCACOLA",
            MaterialName: "Cooca côla",
            PropertiesOfMaterials: "Đồ uống",
            UnitName: "Lon",
            MaterialGroup: "Sản phẩm",
            Note: "Oke",
            StopUsing: 0,
          },
          {
            MaterialCode: "COCACOLA",
            MaterialName: "Cooca côla",
            PropertiesOfMaterials: "Đồ uống",
            UnitName: "Lon",
            MaterialGroup: "Sản phẩm",
            Note: "Oke",
            StopUsing: 0,
          },
          {
            MaterialCode: "COCACOLA",
            MaterialName: "Cooca côla",
            PropertiesOfMaterials: "Đồ uống",
            UnitName: "Lon",
            MaterialGroup: "Sản phẩm",
            Note: "Oke",
            StopUsing: 0,
          },
        ],
        name: "tbMaterial",
      },

      recordStatus: {
        isHide: true,
        formMode: FORM_STATE.ADD,
        data: [],
      },

      isHideForm: true,

      isHidePopupDelete: true,

      contentTextDel: "",
    };
  },
  methods: {

    loadDataTable(){

    },
    /**
     * Hàm hiển thị popup xác nhận xóa
     * CreateBy: TTUyen(27/09/2021)
     */
    btnShowDelete() {
      this.isHidePopupDelete = false;
      setTimeout(() => {
        // var employeeCode = this.cloneData.EmployeeCode;
        this.contentTextDel = MESSAGE.DELETE_EMPLOYEE.format(
          MATERIAL_TEXT.DISPLAY_NAME,
          "Mã"
        );
      }, 150);
    },

    changeStatusForm(isHide, formMode = FORM_STATE.ADD, data = []) {
      this.recordStatus.isHide = false;
      setTimeout(() => {
        this.recordStatus = {
          isHide: isHide,
          formMode: formMode,
          data: data,
        };
        // this.reloadData();
      }, 150);
    },
  },
};
</script>
<style scoped>
@import "../../css/view/material/material-list.css";
@import "../../css/base/combobox.css";
</style>