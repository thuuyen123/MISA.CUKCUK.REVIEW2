<template>
  <div class="dialog-detail">
    <div class="m-model">
      <div class="dialog-container">
        <div class="dialog-form-header">
          <div class="title-dialog">Thêm Nguyên vật liệu</div>
          <div class="tool-form">
            <div class="tool-sprites icon-tool-zoom"></div>
            <div
              class="tool-sprites x-tool-close"
              @click="btnCloseOnClick()"
            ></div>
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
                  :index="0"
                  @error="arrayError"
                  @handleNewCode="handleNewCode"
                />
                <div
                  class="x-form-error"
                  :class="{ 'hide-form-error': !(isError[0] == true) }"
                  title="Tên không được phép để trống"
                >
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
                    :index="1"
                    :data="dataUnit"
                    @error="arrayError"
                    tabindex="3"
                    styleList="max-height: 266px; overflow: auto;"
                    @btnShowFormAdd="btnShowFormAdd"
                    :selectedId="materialDetail.UnitId"
                    @changeValueCombo="updateValueCombobox"
                  />
                  <div
                    class="x-form-error"
                    :class="{ 'hide-form-error': !(isError[1] == true) }"
                    title="Đơn vị tính không được phép để trống"
                  >
                    <div class="sprite icon-form-invalid"></div>
                  </div>
                </div>
              </BaseLabel>
              <BaseLabel label="Hạn sử dụng">
                <div class="form-input-combo">
                  <BaseInput
                    ref="inputExprityDate"
                    id="txtExprityDate"
                    type="num"
                    fieldType="exprityDate"
                    displayName="Hạn sử dụng"
                    value=""
                    placeholder=""
                    tabindex="4"
                    :required="false"
                    v-model="materialDetail.ExprityDate"
                  />
                  <BaseCombobox
                    type="combo"
                    ref="inputExprityType"
                    id="txtExprityType"
                    fieldType="exprityType"
                    displayName="Kiểu hạn sử dụng"
                    value=""
                    itemId="ExprityType"
                    itemName="ExprityTypeName"
                    tabindex="5"
                    :required="false"
                    :selectedId="materialDetail.ExprityType"
                    v-model="materialDetail.ExprityType"
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
                  :index="2"
                  @error="arrayError"
                />
                <div
                  class="x-form-error"
                  :class="{ 'hide-form-error': !(isError[2] == true) }"
                  title="Mã không được phép để trống"
                >
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
                  itemCode="StockCode"
                  tabindex="4"
                  @btnShowFormAdd="btnShowFormAdd"
                  :required="false"
                  :selectedId="materialDetail.StockId"
                  v-model="materialDetail.StockId"
                  :data="dataStock"
                />
              </BaseLabel>
              <BaseLabel
                title="Số lượng tồn tối thiểu"
                label="SL tồn tối thiểu"
              >
                <!-- <BaseInput
                  ref="inputMinimumStock"
                  id="txtMinimumStock"
                  type="text"
                  fieldType="minimumStock"
                  displayName="Số lượng tồn tối thiểu"
                  value=""
                  placeholder=""
                  v-model="materialDetail.MinimumStock"
                  tabindex="6"
                  :required="false"
                /> -->
                <div
                  id="txtMinimumStock"
                  title=""
                  tabindex="6"
                  class="field-input"
                  type="text"
                  placeholder=""
                  data-v-0c456455=""
                >
                  <!-- <input
                    data-v-d3863baa=""
                    type="text"
                    placeholder=""
                    class="form-input-item"
                  /> -->
                  <money
                    ref="inputMinimumStock"
                    id="txtMinimumStock"
                    type="num"
                    fieldType="minimumStock"
                    displayName="Số lượng tồn tối thiểu"
                    value=""
                    placeholder=""
                    v-model="materialDetail.MinimumStock"
                    class="form-input-item"
                    style="text-align: right"
                    v-bind="money"
                  />
                </div>
              </BaseLabel>
            </div>
            <div class="form-block-3">
              <BaseLabel label="Mô tả">
                <div class="field-textarea" tabindex="7">
                  <textarea
                    ref="inputDescription"
                    id="txtDescription"
                    type="textare"
                    fieldType="description"
                    displayName="Mô tả"
                    value=""
                    v-model="materialDetail.Description"
                    name=""
                    cols="30"
                    rows="10"
                    :required="false"
                  ></textarea>
                </div>
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
              <BaseTable
                ref="tableUnit"
                :tableData="tableUnitConvert"
                :isChecked="isChecked"
                @btnShowFormAdd="btnShowFormAdd"
                @unitChange="tableUnitConvertOnChange($event)"
                @rateChange="tableConvertRateOperateChange($event)"
              />
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
            <BaseButton
              type="default"
              iconClass="sprite icon-btnSave"
              @btn-click="btnSaveForm"
            >
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
    <BasePopupForm
      v-if="isShowForm"
      :type="fieldTypeForm"
      ref="formPop"
      @close="closePopForm"
      @errorForm="arrayError"
      :isErrorForm="isError"
    />
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
      btn1="Đồng ý  "
      @confirm="onNotifyInValidPopShowConfirm"
      type="alert"
      icon="icon-notify"
    >
      {{ notifyInvalidMsg }}
    </BasePopup>

    <BasePopup
      v-if="isWarningServerPopShow"
      btn1="Đồng ý"
      @confirm="onWarningServerPopShowConfirm"
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
import { MESSAGE } from "../../js/common/const";
import { FORM_STATE, STATUS_CODE, ENTITY_STATE } from "../../js/common/enums";
import axios from "axios";
import { CONFIG } from "../../js/common/config";
import { Money } from "v-money";
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
    Money,
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

      countRows: 0,

      materialDetail: {
        // MaterialCode: "",
        // MaterialName: "",
        // MinimumStock: null,
        // ExprityDate: null,
        // ExprityType: null,
        // StopFollowing: null,
        // MaterialGroupName: "",
        // PropertiesOfMaterialName: "",
        // Description: null,
        // StockId: "",
        // UnitId: "",
        // UnitConvertId: "",
        // ConvertRate: 0,
        // ConvertRateOperate: "*",
        // Note: "",
        // ParentId: "",
      },
      tableUnitConvert: {
        columns: [
          {
            id: "NumberHead",
            style:
              "min-width: 10px; max-width: 20px !important; text-align: center; ",
            fieldname: "Number",
            title: "STT",
            typeFilter: "span",
            idType: "spNumber",
          },
          {
            id: "UnitConvertHead",
            style: "min-width: 90px; max-width:100px !important; ",
            fieldname: "UnitConvertId",
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
            id: "NoteHead",
            style: "min-width: 60px; max-width: 100px !important;",
            fieldname: "Note",
            title: "Mô tả",
            typeFilter: "span",
            idType: "spNote",
          },
        ],
        data: [],
        name: "tbUnitConvert",
      },

      originalConvert: [],

      isChecked: [],

      fieldTypeForm: "unit",

      originalMaterial: {},

      money: {
        decimal: ",",
        thousands: ".",
        precision: 2,
        masked: false,
      },

      nameForm: "Thêm Nguyên vật liệu",

      isError: [false, false, false, false, false],

      dataStock: [],

      dataUnit: [],

      dataRecord: {
        MaterialDetails: [],
      },

      dataUnitConvert: [
        {
          Number: 1,
          UnitConvertId: "",
          ConvertRate: "",
          ConvertRateOperate: "",
          Note: "",
        },
      ],

      invalidRef: [],
    };
  },

  created() {
    this.loadUnit();
    this.loadStock();
    this.load();
    if (this.status) {
      const status = this.status;
      if (!status.isHide) {
        this.resetInput();
        switch (status.formMode) {
          case FORM_STATE.ADD:
            this.materialDetail.ExprityDate = 0;
            break;
          case FORM_STATE.EDIT:
            this.nameForm = "Sửa Nguyên vật liệu";
            this.editForm();
            break;
          case FORM_STATE.CLONE:
            this.nameForm;
            this.cloneForm();
            break;
          default:
            break;
        }
      }
      this.originalMaterial = { ...this.dataRecord };
    }
  },

  mounted() {
    this.focusFirstControl();
  },

  methods: {
    /**
     * Cập nhật giá trị của các NVL chung
     * CreateBy: TTUyen(05/10/2021)
     */

    updateValueCombobox(value, field) {
      this.$set(this.materialDetail, field, value);
    },

    /**
     * Cập nhật giá trị đơn vị chuyển đổi thay đổi trong bảng
     * CreateBy: TTUyen(05/10/2021)
     */

    tableUnitConvertOnChange({ index, unit }) {
      console.log(index);
      console.log(unit);
      const unitConvert = this.tableUnitConvert.data;
      unitConvert[index].UnitConvertId = unit;
      if (
        this.materialDetail.UnitId != undefined ||
        this.materialDetail.UnitId != null
      ) {
        if (this.materialDetail.UnitId == unit) {
          this.showPopupInvalidFields(MESSAGE.DUBLICATE_UNIT);
        } else {
          let unitName = this.getUnitNameByUnitId(this.materialDetail.UnitId);
          if (unit) {
            let unitConvertName = this.getUnitNameByUnitId(unit);
            setTimeout(() => {
              const note =
                "1 " +
                unitConvertName +
                " = " +
                unitConvert[index].ConvertRate +
                ",00 * " +
                unitName;
              this.$set(unitConvert[index], "Note", note);
              console.log(note);
            }, 1500);
          }
        }
        this.$set(this.tableUnitConvert, "data", unitConvert);
      }
      // this.$set(this.tableUnitConvert,)
    },

    /**
     * Hiển thị popup thông báo trường không hợp lệ
     *   CreateBy: TTUyen(01/09/2021)
     */
    showPopupInvalidFields(message) {
      this.notifyInvalidMsg = message;
      this.isNotifyInValidPopShow = true;
    },
    /**
     * Hiển thị popup thông báo xác nhận
     *  CreateBy: TTUyen(01/09/2021)
     */
    onNotifyInValidPopShowConfirm() {
      this.isNotifyInValidPopShow = false;
      this.invalidRef[0].$el.querySelector("input").focus();
    },
    /**
     * Lấy tên đơn vị theo id đơn vị tính
     */

    getUnitNameByUnitId(unitId) {
      let unitName;
      this.dataUnit.forEach((item) => {
        if (item.UnitId == unitId) {
          unitName = item.UnitName;
        }
      });
      return unitName;
    },
    /**
     * Cập nhật giá trị phép tính thay đổi trong bảng
     * CreateBy: TTUyen(05/10/2021)
     */

    tableConvertRateOperateChange({ index, rate }) {
      const rateConvert = this.tableUnitConvert.data;
      rateConvert[index].ConvertRateOperate = rate;
      this.$set(this.tableUnitConvert, "data", rateConvert);
    },

    /**
     * Lấy dữ liệu NVL chi tiết
     * CreateBy: TTUyen(05/10/2021)
     */

    load() {
      if (this.status.data.MaterialId) {
        this.loadMaterialDetail(this.status.data.MaterialId);
      }
    },
    /**
     * Thực hiện focus khi mở form
     * CreateBy: TTUyen(05/10/2021)
     */

    focusFirstControl() {
      if (this.$refs.inputMaterialName) {
        this.$refs.inputMaterialName.focus();
      }
    },

    /**
     * Nút đóng form
     * CreateBy: TTUyen(05/10/2021)
     */

    btnCloseOnClick() {
      this.$emit("close");
    },

    /**
     * Hiển thị form sửa
     *  CreateBy: TTUyen(02/10/2021)

     */

    editForm() {
      let me = this;
      if (me.status.data) {
        me.materialDetail = me.status.data;
      }
    },

    /**
     * Hiển thị form nhân bản
     *  CreateBy: TTUyen(02/10/2021)

     */

    async cloneForm() {
      let me = this;
      me.materialDetail = JSON.parse(JSON.stringify(me.status.data));
      let res = await me.getNewCode(me.materialDetail.MaterialName);
      let newCode = res.data.Data;
      me.materialDetail.MaterialCode = newCode;
    },

    /**
     * Lấy mã mới từ API
     *  CreateBy: TTUyen(02/10/2021)

     */

    async getNewCode(name) {
      try {
        let res = await axios.get(
          CONFIG.MY_URL + "Materials/NewMaterialCode?materialName=" + name
        );
        return res;
      } catch (e) {
        this.$toast.error(MESSAGE.EXCEPTION_MSG, {
          position: "bottom-right",
          timeout: 2000,
        });
      }
    },

    /**
     * Thực hiện lưu dữ liệu
     * CreateBy: TTUyen(01/09/2021)
     */

    btnSaveForm() {
      this.invalidRef = [];
      Object.values(this.$refs).forEach((el) => {
        if (typeof el.validateInput === "function") {
          let valid = el.validateInput(el.value);
          if (el.value == null || el.value == "") {
            valid = el.validateInput(el.$el.querySelector("input").value);
          }
          if (valid === false) {
            this.invalidRef.push(el);
          }
        }
      });

      //Kiểm tra những input không hợp lệ

      if (this.invalidRef.length > 0) {
        if (typeof this.invalidRef[0].focus === "function") {
          this.invalidRef[0].focus();
        }
        return false;
      } else {
        return this.sendDetails();
      }
    },

    handleDetail() {
      this.dataRecord = JSON.parse(JSON.stringify(this.materialDetail));
      console.log(this.dataRecord);
      // let materialDetails = [];
       let detail = {};
      for (let index = 0; index < this.tableUnitConvert.data.length; index++) {
        //   let item = Object.assign(
        //     this.tableUnitConvert.data[index],
        //     this.dataRecord
        //   );
        //   if (item != null) {
        //     this.$set(materialDetails, index, item);
        //   }
        // }
        // this.dataRecord.MaterialDetails = materialDetails;

       
        detail[index] = this.tableUnitConvert.data[index];
        // thêm vào mảng dataRecord.ProductDetails
        if (
          this.dataRecord.MaterialDetails &&
          this.dataRecord.MaterialDetails.length
        ) {
          this.$set(this.dataRecord, "MaterialDetails", [
            ...this.dataRecord.MaterialDetails,
            detail,
          ]);
        } else {
          this.$set(this.dataRecord, "MaterialDetails", [detail]);
        }
      }

      console.log(this.dataRecord);
    },
    /**
     * Hàm thực hiện lưu dữ liệu vào database
     * CreateBy: TTUyen(01/09/2021)
     */

    async sendDetails() {
      this.handleDetail();
      let material = this.dataRecord;
      try {
        switch (this.status.formMode) {
          case FORM_STATE.ADD:
            await axios
              .post(CONFIG.MY_URL + "Materials/", material)
              .then((res) => {
                if (res.status != STATUS_CODE.NO_CONTENT) {
                  this.$toast.success(MESSAGE.ADD_MSG_SUCCESS, {
                    position: "bottom-right",
                    timeout: 2000,
                  });
                  this.$emit("changeState", true);
                }
              })
              .catch((error) => {
                var msgError = String(error.response.data.Data.userMsg);
                msgError = msgError.split("[").join("");
                this.showPopupWarningServer(msgError);
              });
            break;
          case FORM_STATE.CLONE:
            await axios
              .post(CONFIG.MY_URL + "Materials/", material)
              .then((res) => {
                console.log(res.data);
                if (res.status != STATUS_CODE.NO_CONTENT) {
                  this.$toast.success(MESSAGE.CLONE_MSG_SUCCESS, {
                    position: "bottom-right",
                    timeout: 2000,
                  });
                  this.$emit("changeState", true);
                }
              })
              .catch((res) => {
                this.showPopupWarningServer(res.response.data.Data.userMsg[0]);
              });
            break;
          case FORM_STATE.EDIT:
            this.addEntityState(
              this.originalConvert,
              this.tableUnitConvert.data
            );
            await axios
              .put(
                CONFIG.MY_URL + "Materials/" + this.dataRecord.MaterialId,
                this.materialDetail
              )
              .then((res) => {
                console.log(res.data);
                if (res.status == STATUS_CODE.SUCCESS) {
                  this.$toast.success(MESSAGE.EDIT_MSG_SUCCESS, {
                    position: "bottom-right",
                    timeout: 2000,
                  });
                  this.$emit("changeState", true);
                }
              })
              .catch((error) => {
                var msgError = String(error.response.data.Data.userMsg);
                msgError = msgError.split("[").join("");
                this.showPopupWarningServer(msgError);
              });
            break;
          default:
            break;
        }
      } catch (err) {
        console.log(err);
        this.$toast.error(MESSAGE.EXCEPTION_MSG, {
          position: "bottom-right",
          timeout: 2000,
        });
      }
      this.$emit("reloadData");
    },

    /**
     * Hiển thị popup thông báo khi có lỗi từ server
     *  CreateBy: TTUyen(01/09/2021)
     */

    showPopupWarningServer(message) {
      this.warningServerMsg = message;
      this.isWarningServerPopShow = true;
    },

    /**
     * Hiển thị popup thông báo xác nhận
     *  CreateBy: TTUyen(01/09/2021)
     */

    onWarningServerPopShowConfirm() {
      this.isWarningServerPopShow = false;
      this.invalidRef[0].$el.querySelector("input").focus();
    },
    /**
     * Nhận xử lý sinh mã code mới
     * CreateBy: TTUyen(05/10/2021)
     */

    async handleNewCode(value) {
      let me = this;
      if (value) {
        value = this.removeAccents(value);
        let res = await me.getNewCode(value);
        let newCode = res.data.Data;
        me.materialDetail.MaterialCode = newCode;
      }
    },

    /**
     * Xóa dấu trong chữ
     * CreateBy: TTUyen(05/10/2021)
     */

    removeAccents(str) {
      return str
        .normalize("NFD")
        .replace(/[\u0300-\u036f]/g, "")
        .replace(/đ/g, "d")
        .replace(/Đ/g, "D");
    },
    /**
     * Hiển thị popup form thêm mới
     * CreateBy: TTUyen(05/10/2021)
     */

    btnShowFormAdd(field) {
      this.fieldTypeForm = field;
      this.isShowForm = true;
    },

    /**
     * Thêm dòng vào bảng đơn vị chuyển đổi
     * CreateBy: TTUyen(05/10/2021)
     */

    btnAddTrTable() {
      this.countRows = this.countRows + 1;
      let newRow = {
        Number: this.countRows,
        UnitConvert: "",
        ConvertRate: "1,00",
        ConvertRateOperate: "*",
        Note: "",
      };
      this.tableUnitConvert.data.push(newRow);
      this.isChecked = new Array(this.tableUnitConvert.data.length).fill(false);
    },

    /**
     * Hiển thị lỗi chưa validate dữ liệu
     * CreateBy: TTUyen(05/10/2021)
     */

    arrayError(isShow, value) {
      if (isShow == true) {
        this.$set(this.isError, value, true);
      } else {
        this.$set(this.isError, value, false);
      }
    },
    /**
     * Hàm reset lại form khi thoát khỏi form
     * CreateBy: TTUyen(03/10/2021)
     */
    resetInput() {
      Object.entries(this.materialDetail).forEach(([key]) => {
        this.materialDetail[key] = "";
      });
    },

    /**
     * Lấy dữ liệu kho ngầm định
     * CreateBy: TTUyen(05/10/2021)
     */

    async loadStock() {
      try {
        await axios
          .get(`${CONFIG.MY_URL}Stocks`)
          .then((res) => {
            this.dataStock = res.data.Data;
          })
          .catch((error) => {
            console.log(error);
          });
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * Lấy dữ liệu cho đơn vị tính
     * CreateBy: TTUyen(05/10/2021)
     */

    async loadUnit() {
      try {
        await axios
          .get(`${CONFIG.MY_URL}Units`)
          .then((res) => {
            this.dataUnit = res.data.Data;
          })
          .catch((error) => {
            console.log(error);
          });
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * Lấy nguyên vật liệu chi tiết
     * CreateBy: TTUyen(05/10/2021)
     */

    async loadMaterialDetail(materialId) {
      try {
        await axios
          .get(`${CONFIG.MY_URL}Materials/Detail/${materialId}`)
          .then((res) => {
            this.dataRecord = res.data.Data;
            this.tableUnitConvert.data = [...this.dataRecord.MaterialDetails];
            this.originalConvert = [...this.dataRecord.MaterialDetails];
          })
          .catch((error) => {
            console.log(error);
          });
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * Hàm đánh dấu các hàng xóa chi tiết để biết đâu là hàng cần sửa,xóa, thêm
     * Created By: TTUyen(06/10/2021)
     */
    addEntityState(oldArr, newArr) {
      for (let newObj of newArr) {
        if (oldArr.indexOf(newObj) >= 0) {
          newObj.EntityState = ENTITY_STATE.EDIT;
        } else {
          newObj.EntityState = ENTITY_STATE.ADD;
        }
      }

      for (let oldObj of oldArr) {
        if (newArr.indexOf(oldObj) < 0) {
          oldObj.FlagMode = ENTITY_STATE.DELETE;
          newArr.push(oldObj);
        }
      }
    },
    /**
     * Đóng form nhập
     */
    closePopForm() {
      this.isShowForm = false;
    },
  },
};
</script>
<style lang="scss" scoped>
@import "../../css/view/material/material-detail.css";
.hide-form {
  display: none;
}
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
      align-items: normal;
      .field-textarea {
        flex: 1;
      }
    }
  }
}
.hide-form-error {
  display: none;
}
.form-input-item[type="num"] {
  text-align: left;
}
</style>