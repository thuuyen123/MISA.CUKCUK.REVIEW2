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
          <BaseButton
            @btn-click="changeStatusForm(false, 'clone', dataMaterial)"
            type="control"
            iconClass="sprite icon-btnDuplicate"
          >
            Nhân bản
          </BaseButton>
          <BaseButton
            @btn-click="changeStatusForm(false, 'edit', dataMaterial)"
            type="control"
            iconClass="icon-btnEdit"
          >
            Sửa
          </BaseButton>
          <BaseButton
            type="control"
            iconClass="sprite icon-btnDelete"
            @btn-click="btnShowDelete"
          >
            Xóa
          </BaseButton>
          <BaseButton
            type="control"
            iconClass="sprite icon-refresh"
            @btn-click="reloadData"
          >
            Nạp
          </BaseButton>
        </div>
        <div class="m-panel main-gridpanel">
          <BaseTable
            ref="tableMaterial"
            :tableData="tableMaterial"
            :isChecked="isChecked"
            @changeSelectedTr="changeSelectedTr"
            @btnEditOnDbClick="changeStatusForm"
          />
        </div>
        <BasePaging
          :totalRecord="totalRecord"
          :totalPages="totalPages"
          @changePageIndex="changePageIndex"
          @changePageSize="changePageSize"
          @btnReloadDataTable="btnReloadDataTable"
        />
      </div>
    </div>
    <MaterialDetail
      v-if="!recordStatus.isHide"
      :status="recordStatus"
      @close="detailOnClose()"
      @reloadData="reloadData"
      @changeState="changeStatusForm"
    />
    <BasePopup
      v-if="!isHidePopupDelete"
      btn1="Có"
      btn2="Không"
      type="confirm-del"
      icon="icon-question"
      @btnClose="btnCancelDel"
      @btnConfirm="btnConfirmDel"
    >
      {{ contentTextDel }}
    </BasePopup>
    <BaseLoading :showLoader="isShowLoader" />
  </div>
</template>
<script>
import BaseButton from "../../components/base/BaseButton.vue";
import BaseTable from "../../components/base/BaseTable.vue";
import BasePopup from "../../components/base/BasePopup.vue";
import BasePaging from "../../components/base/BasePaging.vue";
import BaseLoading from "../../components/base/BaseLoading.vue";
import { FORM_STATE, STATUS_CODE } from "../../js/common/enums";
import { MESSAGE } from "../../js/common/const";
import { MATERIAL_TEXT } from "../../js/common/enums";
import MaterialDetail from "./MaterialDetail.vue";
import { eventBus } from "../../main.js";
import axios from "axios";
import { CONFIG } from "../../js/common/config";
export default {
  name: "MaterialList",
  components: {
    BaseButton,
    BaseTable,
    MaterialDetail,
    BasePopup,
    BasePaging,
    BaseLoading,
  },
  data() {
    return {
      tableMaterial: {
        columns: [
          {
            id: "MaterialCodeHead",
            style: "min-width: 100px; max-width: 150px !important;  ",
            fieldname: "MaterialCode",
            title: "Mã nguyên vật liệu",
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
            id: "PropertiesOfMaterialNameHead",
            style: "min-width: 100px; max-width: 150px !important",
            fieldname: "PropertiesOfMaterialName",
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
            id: "MaterialGroupNameHead",
            style: "min-width: 100px; max-width: 150px !important;",
            fieldname: "MaterialGroupName",
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
        data: [],
        name: "tbMaterial",
      },

      filterList: [
        {
          DataField: "MaterialCode",
          FilterOption: "1",
          FilterType: "FilterText",
          FilterValue: "",
          Enable: false,
        },
      ],
      recordStatus: {
        isHide: true,
        formMode: FORM_STATE.ADD,
        data: [],
      },

      isHideForm: true,

      isHidePopupDelete: true,

      contentTextDel: "",

      isShowLoader: false,

      totalRecord: 0,

      pageIndex: 1,

      pageSize: 100,

      totalPages: 1,

      isChecked: [],

      idItemSelected: "",

      nameMaterial: "",

      dataMaterial: [],
    };
  },

  created() {
    this.loadDataTable();
  },
  methods: {
    detailOnClose() {
      this.recordStatus.isHide = true;
    },

    /**
     * Hàm load dữ liệu phân trang
     */
    async loadDataTable() {
      let me = this;
      me.reloading();
      try {
        await axios
          .post(
            CONFIG.MY_URL +
              "Materials/Filter?pageSize=" +
              me.pageSize +
              "&pageIndex=" +
              me.pageIndex,
            me.filterList
          )
          .then((res) => {
            if (res.status != STATUS_CODE.NO_CONTENT) {
              me.tableMaterial.data = res.data.Data;
              me.totalPages = res.data.TotalPage;
              me.totalRecord = res.data.TotalRecord;
              me.isChecked = new Array(me.tableMaterial.data.length).fill(
                false
              );
              me.isChecked[0] = true;
              me.idItemSelected = me.tableMaterial.data[0].MaterialId;
              me.nameMaterial = me.tableMaterial.data[0].MaterialName;
              me.dataMaterial = me.tableMaterial.data[0];
            } else {
              me.tableMaterial.data = [];
              me.totalRecord = 0;
              me.totalPages = 1;
            }
          })
          .catch(() => {
            this.$toast.error(MESSAGE.EXCEPTION_MSG, {
              position: "bottom-right",
              timeout: 2000,
            });
          });

        me.closeReloading();
      } catch (error) {
        console.log(error);
      }
    },

    async loadMaterialDetail(materialId) {
      try {
        let res = await axios.get(
          `${CONFIG.MY_URL}Materials/Detail/${materialId}`
        );
        return res;
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * Hiển thị form dialog
     * CreateBy: TTUyen(02/10/2021)
     */
    changeStatusForm(isHide, formMode = FORM_STATE.ADD, data = {}) {
      this.recordStatus = {
        isHide: isHide,
        formMode: formMode,
        data: data,
      };
    },
    /**
     * Thay đổi item đã lựa chọn
     */
    changeSelectedTr(value, data) {
      this.isChecked = new Array(this.tableMaterial.data.length).fill(false);
      this.isChecked[value] = true;
      this.idItemSelected = data.MaterialId;
      this.nameMaterial = data.MaterialName;
      this.dataMaterial = data;
    },

    /**
     * Hàm hủy xóa bản ghi
     * CreateBy: TTUyen(02/10/2021)
     */
    btnCancelDel() {
      this.isHidePopupDelete = true;
    },

    /**
     * Hàm xóa dữ liệu trong database
     * CreateBy: TTUyen(02/10/2021)
     */
    async btnConfirmDel() {
      let me = this;
      try {
        await axios
          .delete(CONFIG.MY_URL + `Materials/${me.idItemSelected}`)
          .then((res) => {
            if (res.status == 200) {
              this.$toast.success(MESSAGE.DELETE_MSG_SUCCESS, {
                position: "bottom-right",
                timeout: 2000,
              });
            } else {
              this.$toast.error(MESSAGE.ERROR_DELETE_NOSUCCES, {
                position: "bottom-right",
                timeout: 2000,
              });
            }
          })
          .catch(() => {
            this.$toast.error(MESSAGE.EXCEPTION_MSG, {
              position: "bottom-right",
              timeout: 2000,
            });
          });
      } catch (error) {
        console.log(error);
      }
      me.reloadData();
      this.isHidePopupDelete = true;
    },
    /**
     * Hàm phân lại trang khi thay đổi vị trí trang
     * CreateBy: TTUyen(02/10/2021)
     */
    changePageIndex(pageIndex) {
      this.pageIndex = pageIndex;
      this.loadDataTable();
    },

    /**
     * Hàm phân lại trang khi thay đổi số bản ghi trong trang
     * CreateBy: TTUyen(02/10/2021)
     */
    changePageSize(pageSize) {
      this.pageSize = pageSize;
      this.loadDataTable();
    },
    /**
     * Hàm Ẩn/hiện Loader
     * CreateBy: TTUyen(02/10/2021)
     */
    reloading() {
      this.isShowLoader = true;
      setTimeout(() => {
        this.isShowLoader = false;
      }, 1500);
    },

    /**
     * Hàm tắt loader
     * CreateBy: TTUyen(02/10/2021)
     */
    closeReloading() {
      this.isHideLoader = true;
    },
    /**
     * Hàm hiển thị popup xác nhận xóa
     * CreateBy: TTUyen(02/10/2021)
     */
    btnShowDelete() {
      this.isHidePopupDelete = false;
      setTimeout(() => {
        this.contentTextDel = MESSAGE.MSG_DEL.format(
          MATERIAL_TEXT.DISPLAY_NAME,
          this.nameMaterial
        );
      }, 150);
    },

    /**
     * Hàm tải lại dữ liệu table qua paging
     */
    btnReloadDataTable() {
      this.loadDataTable();
    },
    /**
     * Tải lại dữ liệu
     * CreateBy: TTUyen (02/10/2021)
     */
    reloadData() {
      this.pageIndex = 1;
      this.loadDataTable();
    },

    onShowLoaderFull() {
      eventBus.$emit("showLoaderFull");
    },

    onHideLoaderFile() {
      eventBus.$emit("hideLoaderFull");
    },
  },
};
</script>
<style scoped>
@import "../../css/view/material/material-list.css";
@import "../../css/base/combobox.css";
</style>