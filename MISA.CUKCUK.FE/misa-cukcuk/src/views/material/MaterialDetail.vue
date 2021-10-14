<template>
  <div class="dialog-detail">
    <div class="m-model">
      <div class="dialog-container">
        <div class="dialog-form-header">
          <div class="title-dialog">{{ nameForm }}</div>
          <div class="tool-form">
            <div class="tool-sprites icon-tool-zoom"></div>
            <div class="tool-sprites x-tool-close" @click="closeOnChange"></div>
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
                  :required="true"
                  v-model="materialDetail.MaterialName"
                  tabindex="1"
                  :index="0"
                  @error="arrayError"
                  @handleNewCode="handleNewCode"
                  :maxLength="128"
                />
                <div
                  class="x-form-error"
                  :class="{ 'hide-form-error': !(isError[0] == true) }"
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
                    v-model="materialDetail.UnitId"
                    @changeValueCombo="updateValueCombobox"
                    :isClear="isClear"
                  />
                  <div
                    class="x-form-error"
                    :class="{ 'hide-form-error': !(isError[1] == true) }"
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
                    type="number"
                    fieldType="exprityDate"
                    displayName="Hạn sử dụng"
                    tabindex="5"
                    v-model="materialDetail.ExprityDate"
                    :maxLength="15"
                  />
                  <BaseCombobox
                    type="combo"
                    ref="inputExprityType"
                    id="txtExprityType"
                    fieldType="exprityType"
                    displayName="Kiểu hạn sử dụng"
                    itemId="ExprityType"
                    itemName="ExprityTypeName"
                    tabindex="6"
                    :selectedId="materialDetail.ExprityType"
                    v-model="materialDetail.ExprityType"
                    @changeValueCombo="updateValueCombobox"
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
                  :required="true"
                  v-model="materialDetail.MaterialCode"
                  tabindex="2"
                  :index="2"
                  @error="arrayError"
                  :maxLength="25"
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
                  itemId="StockId"
                  itemName="StockName"
                  itemCode="StockCode"
                  tabindex="4"
                  @btnShowFormAdd="btnShowFormAdd"
                  :selectedId="materialDetail.StockId"
                  v-model="materialDetail.StockId"
                  :data="dataStock"
                  @changeValueCombo="updateValueCombobox"
                  :isClear="isClear"
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
                  
                /> -->
                <div class="field-input">
                  <!-- <input
                    data-v-d3863baa=""
                    type="text"
                    placeholder=""
                    class="form-input-item"
                  /> -->
                  <money
                    ref="inputMinimumStock"
                    id="txtMinimumStock"
                    fieldType="minimumStock"
                    displayName="Số lượng tồn tối thiểu"
                    v-model="materialDetail.MinimumStock"
                    class="form-input-item"
                    style="text-align: right"
                    v-bind="money"
                    tabindex="7"
                    :maxLength="15"
                  />
                </div>
              </BaseLabel>
            </div>
            <div class="form-block-3">
              <BaseLabel label="Mô tả">
                <div class="field-textarea">
                  <textarea
                    ref="inputDescription"
                    id="txtDescription"
                    type="textare"
                    fieldType="description"
                    displayName="Mô tả"
                    v-model="materialDetail.Description"
                    cols="30"
                    rows="10"
                    tabindex="8"
                    :maxLength="255"
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
                @changeSelectedTr="changeSelectedTr"
                @changeConvertRate="changeConvertRate"
                @changeInput="changeInput('convertRate', $event)"
              />
            </div>
            <div class="form-table-btn">
              <BaseButton
                type="default"
                @btn-click="btnAddRowTable"
                iconClass="sprite icon-btnAddition"
                title="Ctrl + Insert"
              >
                Thêm dòng
              </BaseButton>
              <BaseButton
                type="default"
                iconClass="sprite icon-btnDelete"
                :indexChecked="indexChecked"
                @btn-click="btnDeleteRowTable"
                title="Ctrl + Delete"
              >
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
              title="Ctrl + S"
            >
              Cất
            </BaseButton>
            <BaseButton
              type="default"
              iconClass="sprite icon-btnSaveAdd"
              @btn-click="btnSaveAddForm"
              title="Ctrl + Shift + S"
            >
              Cất và thêm
            </BaseButton>
            <BaseButton
              type="default"
              iconClass="sprite icon-btnCancel-red"
              @btn-click="btnCloseOnClick"
              title="Ctrl + B"
            >
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
      @closePopForm="closePopForm"
      @reloadDataUnitAndStock="reloadDataUnitAndStock"
      @showPopupWarningServer="showPopupWarningServer"
      @saveChangesPopupShow="saveChangesPopupFormShow"
      :isConfirm="isConfirmForm"
      @resetIsConfirm="resetIsConfirm"
    />
    <BasePopup
      v-if="saveChangesPopupShow"
      btn1="Có"
      btn2="Không"
      btn3="Hủy bỏ"
      @btnClose="closeSaveChangesPopup"
      @btnConfirm="confirmSaveChangesPopup"
      @btnCancel="cancelSaveChangesPopup"
      type="confirm-add"
      icon="icon-question"
    >
      {{ saveChangesPopupMessage }}
    </BasePopup>

    <BasePopup
      v-if="isNotifyInValidPopShow"
      btn1="Đồng ý  "
      @btnConfirm="onNotifyInValidPopShowConfirm"
      type="alert"
      icon="icon-warning"
    >
      {{ notifyInvalidMsg }}
    </BasePopup>

    <BasePopup
      v-if="isWarningServerPopShow"
      btn1="Đồng ý"
      @btnConfirm="onWarningServerPopShowConfirm"
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
import { CommonFn } from "../../js/common/common-fn";
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
      // hiển thị form unit, stock
      isShowForm: false,

      // Xác nhận có khi form unit, stock thay đổi
      isConfirmForm: false,

      // tên của form thay đổi
      nameFormChange: "",

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

      rowTableSelected: -1,

      materialDetail: {},

      //Bảng đơn vị chuyển đổi
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

      //Lưu đơn vị chuyển đổi gốc
      originalConvert: [],

      isChecked: [],

      indexChecked: -1,

      fieldTypeForm: "unit",

      originalMaterial: {},

      money: {
        decimal: ",",
        thousands: ".",
        precision: 2,
        masked: false,
      },

      nameForm: "Thêm Nguyên vật liệu",

      isError: [false, false, false],

      dataStock: [],

      dataUnit: [],

      dataRecord: {},

      // Chứa phần tử chưa được đúng định dạng
      invalidRef: [],

      isClear: false,

      //sự kiên thao tác bằng tổ hợp phím
      pressCtrl: false,
      pressShift: false,
      pressKeyS: false,
      pressInsert: false,
      pressKeyB: false,
      pressDelete: false,

      focusRefString: "",
    };
  },
  watch: {},
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
            this.materialDetail.ExprityType = 0;
            break;
          case FORM_STATE.EDIT:
            this.nameForm = "Sửa Nguyên vật liệu";
            this.editForm();
            break;
          case FORM_STATE.CLONE:
            this.cloneForm();
            break;
          default:
            break;
        }
      }
    }

    
  },

  methods: {
    /**
     * Hiển thị xác nhận đóng form khi dữ liệu thay đổi
     * Đóng form khi dữ liệu không bị thay đổi
     */
    closeOnChange() {
      this.handleDetail();
      if (this.checkChangeForm(this.originalMaterial, this.dataRecord)) {
        this.saveChangesPopupFormShow();
      } else {
        this.btnCloseOnClick();
      }
    },

    /**
     * Kiểm tra xem form có thay đổi không
     */
    checkChangeForm(original, present) {
      let isChange = false;
      Object.entries(original).forEach(([key]) => {
        if (original[key] !== present[key]) {
          isChange = true;
        }
      });
      return isChange;
    },

    /**
     * Cập nhật mô tả trong bản từ việc thay đổi giá trị tỷ lệ chuyển đổi
     */
    changeConvertRate({ index, value }) {
      if (value) {
        value = CommonFn.formatAutoMoney(value);
        let unitConvert = this.tableUnitConvert.data;
        unitConvert[index].ConvertRate = value;
        // this.$set(this.materialDetail, "ConvertRate", value);
        let note = "";
        note = this.updateNoteConvert(
          index,
          this.materialDetail.UnitId,
          unitConvert[index].UnitConvertId,
          value,
          unitConvert[index].ConvertRateOperate
        );
        this.$set(unitConvert[index], "Note", note);
        this.$set(this.tableUnitConvert, "data", unitConvert);
      }
    },
    /**
     * Cập nhật mô tả đơn vị chuyển đổi trong bảng từ cbx đơn vị tính
     * CreateBy: TTUyen(05/10/2021)
     */
    updateValueCombobox(value, field) {
      this.$set(this.materialDetail, field, value);
      if (field == "UnitId") {
        this.tableUnitConvert.data.forEach((item) => {
          let note = "";
          note = this.updateNoteConvert(
            1,
            value,
            item.UnitConvertId,
            item.ConvertRate,
            item.ConvertRateOperate
          );
          item.Note = note;
        });
      }
    },

    /**
     * Hàm cập nhật note theo id đơn vị tính, id đơn vị chuyển đổi, phép tính, tỷ lệ chuyển đổi
     */
    updateNoteConvert(
      index,
      unitId,
      unitConvertId,
      convertRate,
      convertRateOperate
    ) {
      let unitMaterialName = "";
      let unitConvertName = "";
      let note = "";
      if (unitId != undefined && unitId != null && unitConvertId != undefined) {
        if (unitId == unitConvertId) {
          this.showPopupInvalidFields(MESSAGE.DUBLICATE_UNIT);
          this.focusRefString = `inputUnitConvert${index}`;
        } else {
          this.focusRefString = "";
          unitMaterialName = this.getUnitNameByUnitId(unitId);
          unitConvertName = this.getUnitNameByUnitId(unitConvertId);
          note =
            "1 " +
            unitConvertName +
            " = " +
            convertRate +
            ",00 " +
            convertRateOperate +
            " " +
            unitMaterialName;
        }
      }
      return note;
    },
    /**
     * Cập nhật giá trị đơn vị chuyển đổi thay đổi trong bảng
     * CreateBy: TTUyen(05/10/2021)
     */

    tableUnitConvertOnChange({ index, unit }) {
      let unitConvert = this.tableUnitConvert.data;
      unitConvert[index].UnitConvertId = unit;
      let note = this.updateNoteConvert(
        index,
        this.materialDetail.UnitId,
        unit,
        unitConvert[index].ConvertRate,
        unitConvert[index].ConvertRateOperate
      );
      this.$set(unitConvert[index], "Note", note);
      this.$set(this.tableUnitConvert, "data", unitConvert);
    },

    /**
     * Cập nhật giá trị tỷ lệ chuyển đổi dưới dạng format 1.000.000 khi nhập
     */
    changeInput(name, { index, value }) {
      if (name == "convertRate") {
        this.tableUnitConvert.data[index].ConvertRate =
          Number.parseInt(CommonFn.formatAutoMoney(value));
      }
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
      if (this.$refs.tableUnit) {
        if (this.focusRefString != "") {
          console.log(this.$refs.tableUnit.$refs[this.focusRefString][0]);
          setTimeout(() => {
            this.$refs.tableUnit.$refs[this.focusRefString][0].focus();
          }, 150);
        }
      }
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

      let note = "";
      note = this.updateNoteConvert(
        index,
        this.materialDetail.UnitId,
        rateConvert[index].UnitConvertId,
        rateConvert[index].ConvertRate,
        rate
      );
      rateConvert[index].Note = note;
      this.$set(this.tableUnitConvert, "data", rateConvert);
    },

    /**
     * Thay đổi item đã lựa chọn
     */
    changeSelectedTr(value, data) {
      console.log(value);
      this.isChecked = new Array(this.tableUnitConvert.data.length).fill(false);
      this.isChecked[value] = true;
      this.indexChecked = value;
      console.log(data);
    },

    /**
     * Xóa hàng trong bảng
     */
    btnDeleteRowTable() {
      if (this.indexChecked != -1) {
        this.tableUnitConvert.data.pop(this.indexChecked);
        this.indexChecked = 0;
      }
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
    async editForm() {
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

      axios
        .get(
          CONFIG.MY_URL +
            "Materials/NewMaterialCode?materialName=" +
            CommonFn.removeAccents(me.materialDetail.MaterialName)
        )
        .then((res) => {
          me.materialDetail.MaterialCode = res.data.Data;
        })
        .catch((error) => {
          console.log(error);
        });
    },

    /**
     * Lấy mã mới từ API
     *  CreateBy: TTUyen(02/10/2021)
     */
    getNewCode(name) {
      try {
        let res = axios.get(
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
    async save() {
      this.invalidRef = [];
      Object.values(this.$refs).forEach((el) => {
        if (el != undefined) {
          if (typeof el.validateInput === "function") {
            let valid = el.validateInput(el.value);
            if (el.value == null || el.value == "") {
              valid = el.validateInput(el.$el.querySelector("input").value);
            }
            if (valid === false) {
              this.invalidRef.push(el);
            }
          }
        }
      });

      //Kiểm tra những input không hợp lệ

      if (this.invalidRef.length > 0) {
        if (typeof this.invalidRef[0].focus === "function") {
          this.invalidRef[0].focus();
        }
        return Promise.resolve(false);
      } else {
        let a = await this.sendDetails();
        return a;
      }
    },

    /**
     * Hàm xử lý dữ liệu thành object để gửi dữ liệu cho server
     */
    handleDetail() {
      this.dataRecord = JSON.parse(JSON.stringify(this.materialDetail));
      this.addEntityState(this.originalConvert, this.tableUnitConvert.data);

      console.log(this.dataRecord);
      let materialDetails = [];
      //  let detail = {};
      for (let index = 0; index < this.tableUnitConvert.data.length; index++) {
        let item = {};
        if (this.status.formMode == FORM_STATE.ADD) {
          item = Object.assign(
            this.tableUnitConvert.data[index],
            this.dataRecord
          );
        } else {
          item = Object.assign(item, this.dataRecord);
          item = Object.assign(item, this.tableUnitConvert.data[index]);
        }
        item.ConvertRate = Number.parseInt(CommonFn.reFormatMoney(item.ConvertRate));
        if (item != null) {
          this.$set(materialDetails, index, item);
        }
      }
      this.dataRecord.MaterialDetails = materialDetails;
      this.dataRecord.ConvertRate = CommonFn.reFormatMoney(
        this.dataRecord.ConvertRate
      );

      console.log(this.dataRecord);
    },
    /**
     * Hàm thực hiện lưu dữ liệu vào database
     * CreateBy: TTUyen(01/09/2021)
     */
    sendDetails() {
      return new Promise((resolve) => {
        this.handleDetail();
        console.log(this.dataRecord);
        this.dataRecord.MinimumStock = Number.parseInt(
          this.dataRecord.MinimumStock
        );
        this.dataRecord.ConvertRate = 0;
        this.dataRecord.MaterialDetails.forEach((item) => {
          let responseTime = Number(new Date());
          item.MaterialCode = responseTime + "";
          item.MinimumStock = Number.parseInt(item.MinimumStock);
        });
        try {
          switch (this.status.formMode) {
            case FORM_STATE.ADD:
              axios
                .post(CONFIG.MY_URL + "Materials/", this.dataRecord)
                .then((res) => {
                  if (res.status != STATUS_CODE.NO_CONTENT) {
                    this.$toast.success(MESSAGE.ADD_MSG_SUCCESS, {
                      position: "bottom-right",
                      timeout: 2000,
                    });
                  }
                  resolve(true);
                })
                .catch((error) => {
                  var msgError = String(error.response.data.Data.userMsg);
                  this.showPopupWarningServer(msgError);
                  resolve(false);
                });
              break;
            case FORM_STATE.CLONE:
              axios
                .post(CONFIG.MY_URL + "Materials/", this.dataRecord)
                .then((res) => {
                  console.log(res.data);
                  if (res.status != STATUS_CODE.NO_CONTENT) {
                    this.$toast.success(MESSAGE.CLONE_MSG_SUCCESS, {
                      position: "bottom-right",
                      timeout: 2000,
                    });
                  }
                  resolve(true);
                })
                .catch((res) => {
                  this.showPopupWarningServer(
                    res.response.data.Data.userMsg[0]
                  );
                  resolve(false);
                });
              break;
            case FORM_STATE.EDIT:
              axios
                .put(
                  CONFIG.MY_URL + "Materials/" + this.dataRecord.MaterialId,
                  this.dataRecord
                )
                .then((res) => {
                  console.log(res.data);
                  if (res.status == STATUS_CODE.SUCCESS) {
                    this.$toast.success(MESSAGE.EDIT_MSG_SUCCESS, {
                      position: "bottom-right",
                      timeout: 2000,
                    });
                  }
                  resolve(true);
                })
                .catch((error) => {
                  var msgError = String(error.response.data.Data.userMsg);
                  // msgError = msgError.split("[").join("");
                  this.showPopupWarningServer(msgError);
                  resolve(false);
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
      });
    },
    /**
     * hàm xử lý  cất và hiện thị form thêm mới
     *  CreateBy: TTUyen(01/09/2021)
     */
    btnSaveAddForm() {
      this.save().then((res) => {
        if (res) {
          this.$emit("changeState", false, FORM_STATE.ADD);
          this.isClear = true;
          this.tableUnitConvert.data = [];
          this.materialDetail = {};
          this.dataRecord = {};
          this.originalMaterial = {};
          this.originalConvert = [];
          this.isChecked = [];
          this.invalidRef = [];
          this.materialDetail.ExprityDate = 0;
          this.materialDetail.ExprityType = 0;
          this.nameForm = "Thêm Nguyên vật liệu";
          this.$emit("reloadData");
        }
      });
    },

    /**
     * Hàm thực hiển lưu NVL
     */
    btnSaveForm() {
      this.save().then((res) => {
        if (res) {
          this.$emit("changeState", true);
          this.$emit("reloadData");
        }
      });
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
      this.$refs.inputMaterialCode.focus();
    },

    /**
     * Hiển thị popup xác nhận đóng
     */
    saveChangesPopupFormShow(value) {
      if (value) {
        this.nameFormChange = value;
      }
      this.saveChangesPopupShow = true;
      this.saveChangesPopupMessage = MESSAGE.FORM_CHANGED;
    },

    /**
     * Đóng popup, đóng form nhập (Không)
     *  CreateBy: TTUyen(01/09/2021)
     */
    closeSaveChangesPopup() {
      this.saveChangesPopupShow = false;
      if (this.nameFormChange == "form") {
        this.isShowForm = false;
      } else {
        this.$emit("changeState", true);
      }
      this.nameFormChange = "";
    },

    /**
     * Đóng popup vẫn hiển thị form nhập (Hủy bỏ)
     *  CreateBy: TTUyen(01/09/2021)
     */
    cancelSaveChangesPopup() {
      this.saveChangesPopupShow = false;
    },

    /**
     * Xác nhận có cất dữ liệu
     *  CreateBy: TTUyen(01/09/2021)
     */
    confirmSaveChangesPopup() {
      this.saveChangesPopupShow = false;
      if (this.nameFormChange == "form") {
        this.isConfirmForm = true;
        alert(2);
      } else {
        this.btnSaveForm();
      }
      this.nameFormChange = "";
    },

    resetIsConfirm() {
      this.isConfirmForm = false;
    },
    /**
     * Nhận xử lý sinh mã code mới
     * CreateBy: TTUyen(05/10/2021)
     */

    async handleNewCode(value) {
      if (
        value &&
        (this.materialDetail.MaterialCode == null ||
          this.materialDetail.MaterialCode == "")
      ) {
        value = CommonFn.removeAccents(value);
        axios
          .get(
            CONFIG.MY_URL + "Materials/NewMaterialCode?materialName=" + value
          )
          .then((res) => {
            const newCode = res.data.Data;
            this.$set(this.materialDetail, "MaterialCode", newCode);
          })
          .catch((error) => {
            console.log(error);
          });
      }
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

    btnAddRowTable() {
      this.countRows = this.countRows + 1;
      let newRow = {
        Number: this.countRows,
        UnitConvert: "",
        ConvertRate: "1",
        ConvertRateOperate: "*",
        Note: "",
      };
      this.tableUnitConvert.data.push(newRow);
      this.isChecked = new Array(this.tableUnitConvert.data.length).fill(false);
      this.indexChecked = this.tableUnitConvert.data.length;
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
            console.log(this.dataRecord);

            let tempConvert = [];
            for (
              let index = 0;
              index < res.data.Data.MaterialDetails.length;
              index++
            ) {
              let item = res.data.Data.MaterialDetails[index];
              this.$set(tempConvert, index, item);
            }
            this.originalConvert = tempConvert;
            this.tableUnitConvert.data = [...res.data.Data.MaterialDetails];

            this.isChecked = new Array(this.tableUnitConvert.data.length).fill(
              false
            );
            this.tableUnitConvert.data.forEach((item) => {
              item.ConvertRate = CommonFn.formatAutoMoney(item.ConvertRate);
            });
            this.originalMaterial = { ...this.dataRecord };
            console.log(this.dataRecord);
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
          oldObj.EntityState = ENTITY_STATE.DELETE;
          newArr.push(oldObj);
        }
      }
    },
    /**
     * Tải lại dữ liệu cho unit và stock
     */
    async reloadDataUnitAndStock() {
      await this.loadUnit();
      await this.loadStock();
    },
    /**
     * Đóng form nhập
     */
    closePopForm() {
      this.isShowForm = false;
    },
  },
  mounted() {
    this.focusFirstControl();
    let me = this;
    document.addEventListener("keydown", (e) => {
      switch (e.key) {
        case "Control":
          me.pressCtrl = true;
          break;
        case "s":
          me.pressKeyS = true;
          break;
        case "b":
          me.pressKeyB = true;
          break;
        case "Shift":
          me.pressShift = true;
          break;
        case "Delete":
          me.pressDelete = true;
          break;
        case "Insert":
          me.pressInsert = true;
          break;
      }
      if (me.pressCtrl && me.pressKeyS) {
        me.btnSaveForm();
        e.preventDefault();
      }
      if (me.pressShift && me.pressInsert) {
        me.btnSaveAddForm();
        e.preventDefault();
      }
      if (me.pressCtrl && me.pressInsert) {
        me.btnAddRowTable();
        e.preventDefault();
      }
      if (me.pressCtrl && me.pressDelete) {
        me.btnDeleteRowTable();
        e.preventDefault();
      }
      if (me.pressCtrl && me.pressKeyB) {
        me.btnCloseOnClick();
        e.preventDefault();
      }
    });

    document.addEventListener("keyup", (e) => {
      switch (e.key) {
        case "Control":
          me.pressCtrl = false;
          break;
        case "s":
          me.pressKeyS = false;
          break;
        case "b":
          me.pressKeyB = false;
          break;
        case "Shift":
          me.pressShift = false;
          break;
        case "Delete":
          me.pressDelete = false;
          break;
        case "Insert":
          me.pressInsert = false;
          break;
      }
    });
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