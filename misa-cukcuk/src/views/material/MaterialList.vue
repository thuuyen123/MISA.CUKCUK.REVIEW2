<template>
  <div class="main-content">
    <div class="content-title">
      <div class="content-title-text">Nguyên vật liệu</div>
      <div class="content-title-button">
        <base-button type="default" iconClass="icon-feedback">
          Phản hồi
        </base-button>
      </div>
    </div>
    <div class="content-body">
      <div id="formMaterialList" class="m-panel-list-body">
        <div class="m-toolbar grid-toolbar">
          <base-button type="control" iconClass="sprite icon-btnAddition">
            Thêm
          </base-button>
          <base-button type="control" iconClass="sprite icon-btnDuplicate">
            Nhân bản
          </base-button>
          <base-button type="control" iconClass="icon-btnEdit">
            Sửa
          </base-button>
          <base-button type="control" iconClass="sprite icon-btnDelete">
            Xóa
          </base-button>
          <base-button type="control" iconClass="sprite icon-refresh">
            Nạp
          </base-button>
        </div>
        <div class="m-panel main-gridpanel">
          <base-table ref="table" :tableData="table" />
        </div>
        <div class="x-toolbar paging-toolbar">
          <div class="paging-toolbar-setting">
            <div class="p-paging-index"></div>
            <div class="p-load-paging">
              <!-- <base-button type="secondary" iconClass="icon-btnLoadPaging" /> -->
            </div>
            <div class="p-paging-size"></div>
          </div>
          <div class="p-paging-display-records"></div>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
import BaseButton from "../../components/base/BaseButton.vue";
import BaseTable from "../../components/base/BaseTable.vue";
export default {
  name: "MaterialList",
  components: {
    BaseButton,
    BaseTable,
  },
  data() {
    return {
      table: {
        columns: [
          {
            id: "MaterialCodeHead",
            style: "min-width: 100px;z-index: 5; max-width: 150px !important;",
            fieldname: "MaterialCode",
            title: "Mã nhân viên",
            typeFilter: "input",
            idType: "txtMaterialCode",
          },
          {
            id: "MaterialNameHead",
            style:
              "min-width: 120px; max-width:170px !important; z-index: 5;",
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
        ],
      },
    };
  },
};
</script>
<style scoped>
@import "../../css/view/material/material-list.css";
</style>