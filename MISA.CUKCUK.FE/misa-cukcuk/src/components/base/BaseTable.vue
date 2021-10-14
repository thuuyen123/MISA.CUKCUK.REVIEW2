<template>
  <div class="grid-container">
    <div class="grid-header">
      <div class="colomn-header"></div>
    </div>
    <div class="grid-body">
      <div class="grid-item-container" :class="tableData.name">
        <table class="content-table" border="0" cellspacing="0">
          <thead>
            <tr>
              <th
                v-for="(item, index) in tableData.columns"
                :key="item.id + index"
                :id="item.id"
                :style="item.style"
                :fieldname="item.fieldname"
                class="colunm-header-title"
              >
                <div class="content-filter">
                  <div class="filter-condition">
                    <div :class="['title-column', item.position]">
                      <span>{{ item.title }}</span>
                    </div>

                    <div
                      class="content-filter-input"
                      v-if="
                        item.typeFilter == 'input' &&
                        tableData.name == 'tbMaterial'
                      "
                    >
                      <BaseDropdown
                        @closeDropdown="closeDropdownMenuFilter(index)"
                        @updateFilterOption="updateFilterOption(index, $event)"
                        :ref="'filter' + index"
                        @btnClickToggleMenu="btnClickToggleMenu(index)"
                      />

                      <div class="x-form-input">
                        <input
                          class="form-input-item"
                          type="text"
                          :id="item.idType"
                          v-model="filterList[index]['FilterValue']"
                          @blur="updateFilterValue(index, $event.target.value)"
                        />
                      </div>
                    </div>

                    <div
                      class="content-filter-combo"
                      v-if="
                        item.typeFilter == 'combo' &&
                        tableData.name == 'tbMaterial'
                      "
                    >
                      <BaseCombobox
                        type="tableList"
                        ref="inputProperties"
                        id="txtProperties"
                        fieldType="properties"
                        displayName="Tính chất"
                        value=""
                        itemId="PropertiesOfMaterialId"
                        itemName="PropertiesOfMaterialName"
                        v-if="item.fieldname == 'PropertiesOfMaterialName'"
                        @changePropertyName="changePropertyName(index, $event)"
                      />
                      <BaseCombobox
                        type="tableList"
                        ref="inputStopFollowing"
                        id="txtStopFollowing"
                        fieldType="stopFollowing"
                        displayName="Ngưng sử dụng"
                        value=""
                        itemId="StopFollowing"
                        itemName="StopFollowingName"
                        v-if="item.fieldname == 'StopFollowing'"
                        selectedId="0"
                        @changeStopFollowing="changeValueCbxStop(index, $event)"
                      />
                    </div>
                  </div>
                </div>
              </th>
            </tr>
          </thead>
          <tbody ref="tableBody">
            <tr
              v-for="(record, index) in tableData.data"
              :key="'record' + index"
              :id="'record' + index"
              class="x-record-item"
              @click="onClickChecked(index)"
              @dblclick="btnEditOnClick(index)"
              :class="{ 'x-record-item-selected': isChecked[index] }"
            >
              <td
                v-for="(column, idx) in tableData.columns"
                :key="'td' + index + '' + idx"
                :id="'td' + index + '' + idx"
                :style="column.style"
                :class="column.subClass"
                :title="record[column.fieldname]"
              >
                <div v-if="tableData.name == 'tbUnitConvert'">
                  <div
                    v-if="column.fieldname == 'Number'"
                    class="text-value-column"
                  >
                    <span> {{ index + 1 }} </span>
                  </div>
                  <div
                    v-else-if="column.fieldname == 'UnitConvertId'"
                    class="cbx-value-column"
                  >
                    <BaseCombobox
                      type="cbxform"
                      :ref="refCbb(index)"
                      id="txtUnit"
                      fieldType="unitConvert"
                      displayName="Đơn vị tính"
                      itemId="UnitId"
                      itemName="UnitName"
                      :tabindex="`${index + 9}`"
                      styleList="max-height: 98px; overflow: auto;"
                      @btnShowFormAdd="btnShowFormAdd('unit')"
                      @inputCombo="unitConvertChange(index, $event)"
                      :data="dataUnit"
                      :value="record['UnitConvertId']"
                      :selectedId="record['UnitConvertId']"
                      :required="true"
                    />
                  </div>
                  <div v-else-if="column.fieldname == 'ConvertRateOperate'">
                    <BaseCombobox
                      type="combo"
                      ref="inputConvertRateOperate"
                      id="txtConvertRateOperate"
                      fieldType="convertRateOperate"
                      displayName="Phép tính"
                      value=""
                      :selectedId="record[column.fieldname]"
                      itemId="ConvertRateOperate"
                      itemName="ConvertRateOperateName"
                      v-model="record['ConvertRateOperate']"
                      @inputCombo="convertRateOperateChange(index, $event)"
                      :tabindex="`${index + 10}`"
                    />
                  </div>

                  <div v-else-if="column.fieldname == 'ConvertRate'">
                    <BaseInput
                      v-if="column.fieldname == 'ConvertRate'"
                      ref="inputConvertRate"
                      id="txtConvertRate"
                      type="number"
                      fieldType="convertRate"
                      displayName="Tỷ lệ chuyển đổi"
                      v-model="record.ConvertRate"
                      :tabindex="`${index + 10}`"
                      @changeOnInput="changeConvertRate(index, $event)"
                      @input="changeInput(index, $event)"
                    />
                    <!-- <div
                      title="Tỷ lệ chuyển đổi"
                      tabindex=""
                      class="field-input"
                    >
                      <money
                        ref="inputConvertRate"
                        type="text"
                        class="form-input-item"
                        style="text-align: right"
                        v-bind="money"
                        v-model="record.ConvertRate"
                        :tabindex="`${index + 10}`"
                      ></money>
                    </div> -->
                  </div>

                  <div v-else class="text-value-column">
                    <span> {{ record[column.fieldname] }} </span>
                  </div>
                </div>
                <div v-else>
                  <div
                    class="text-value-column"
                    v-if="column.fieldname != 'StopFollowing'"
                  >
                    <span>{{ record[column.fieldname] }}</span>
                  </div>
                  <div
                    style="left: 16px; min-width: 16px; text-align: center"
                    v-else
                  >
                    <div title="" class="check-stop x-item-disabled">
                      <img
                        v-if="record.StopFollowing == 0"
                        class="x-grid-checkcolunm"
                        src=""
                        alt=""
                      />
                      <img v-else class="x-grid-checked-1" src="" alt="" />
                    </div>
                  </div>
                </div>
              </td>
            </tr>
          </tbody>
        </table>
        <div
          class="no-data"
          v-if="tableData.data.length <= 0 && tableData.name == 'tableMaterial'"
        >
          <div class="no-data-img"></div>
          <div class="no-data-text">Không có dữ liệu</div>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
// import { FORM_STATE } from "../../js/common/enums";

import BaseCombobox from "./BaseCombobox.vue";
import BaseInput from "./BaseInput.vue";
import BaseDropdown from "./BaseDropdownn.vue";
import { FORM_STATE } from "../../js/common/enums";
import axios from "axios";
import { CONFIG } from "../../js/common/config";
import { CommonFn } from "../../js/common/common-fn";
//  import { Money } from "v-money";
export default {
  name: "BaseTable",
  components: {
    BaseInput,
    BaseCombobox,
    BaseDropdown,
    // Money,
  },

  props: {
    tableData: {
      data: {
        type: Array,
      },
      columns: {
        type: Array,
      },
      name: String,
    },
    isChecked: Array,

    filterList: Array,

    isClear: Boolean,
  },
  created() {
    if (this.tableData.name == "tableMaterial") {
      this.isChecked[0] = true;
    }
    this.loadUnit();
  },
  mounted() {
    this.focus();
  },
  watch: {
    tableData(value) {
      if (value.data.ConvertRate) {
        value.data.ConvertRate = CommonFn.formatMoney(value.ConvertRate);
      }
    },
  },
  data() {
    return {
      top: 0,

      openMenu: false,

      dataUnit: [],

      indexOpen: -1,

      nameOption: [],
    };
  },

  methods: {
    changeInput(index, value) {
      this.$emit("changeInput", { index: index, value: value });
    },
    
    refCbb(index){
      return `inputUnitConvert${index}`;
    },

    /**
     * Hàm gửi emit DbClick để hiển thị form sửa
     */
    btnEditOnClick(value) {
      this.$emit(
        "btnEditOnDbClick",
        false,
        FORM_STATE.EDIT,
        this.tableData.data[value]
      );
    },
    /**
     * Xử lý khi click hiển thị tính năng mở rộng
     * CreateBy: TTUyen (24/09/2021)
     */
    btnShowOption(index) {
      console.log(this.tableData.data[index]);
      console.log(this.$refs);
      this.top = this.$refs[`myButton${index}`][0].getBoundingClientRect().top;
      console.log(this.top);
      this.$emit("show-option-click", this.tableData.data[index], this.top);
    },

    /**
     * Hàm mở menu lọc bằng điều kiện
     * CreateBy: TTUyen (24/09/2021)
     */
    btnClickToggleMenu(index) {
      this.indexOpen = index;
      if (!this.openMenu) {
        // this.$refs[`filter${index}`][0].style.display = "block";
        this.$refs[`filter${index}`][0].displayBlock();
        this.openMenu = true;
      } else {
        this.openMenu = false;
        // this.$refs[`filter${index}`][0].style.display = "none";
        this.$refs[`filter${index}`][0].displayNone();
      }
    },

    /**
     * Hiển thị form thêm đơn vị tính or kho ngầm định
     */
    btnShowFormAdd(value) {
      this.$emit("btnShowFormAdd", value);
    },

    /**
     * Lấy dữ liệu cho đơn vị tính
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
     * Thay đổi giá trị của cbx ngừng theo dõi
     */
    changeValueCbxStop(index, value) {
      this.$emit("changeValueFilter", index, value);
    },

    /**
     * Thay đổi giá trị của tính chất
     */
    changePropertyName(index, name) {
      this.$emit("changePropertyName", index, name);
    },
    /**
     * Cập nhật giá trị lọc
     */
    updateFilterValue(index, value) {
      this.$emit("updateFilterValue", index, value);
    },

    /**
     * Đóng menu dropdown lựa chọn lọc
     */
    closeDropdownMenuFilter(index) {
      this.openMenu = false;
      this.$refs[`filter${index}`][0].displayNone();
    },

    /**
     * Cập nhật lựa chọn lọc
     */
    updateFilterOption(index, option) {
      this.$emit("updateFilterOption", index, option);
    },

    changeConvertRate(index, value) {
      this.$emit("changeConvertRate", {index, value});
    },
    /**
     * Focus vào đơn vị tính
     */
    focus() {
      if (this.$refs.inputUnitConvert) {
        this.$refs.inputUnitConvert.focus();
      }
    },

    /**
     * Hàm gửi thay đổi của đơn vị tính sang bên detail
     */
    unitConvertChange(index, unit) {
      this.$emit("unitChange", { index, unit });
    },

    /**
     * Hàm gửi thay đổi tỷ lệ chuyển đổi sang detail
     */
    convertRateOperateChange(index, rate) {
      this.$emit("rateChange", { index, rate });
    },

    /**
     * Hàm chọn dòng trong bảng
     */
    onClickChecked(value) {
      this.$emit("changeSelectedTr", value, this.tableData.data[value]);
    },
  },
};
</script>
<style scoped>
@import "../../css/base/table.css";
@import "../../css/base/input.css";
@import "../../css/base/button.css";
@import "../../css/base/combobox.css";
.background-color-white {
  background-color: #fff;
  z-index: 0 !important;
}
</style>