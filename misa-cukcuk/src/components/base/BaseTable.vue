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
                      <div class="button-choice-operator">
                        <button
                          @click="btnClickToggleMenu(index)"
                          class="m-btn"
                        >
                          *
                        </button>
                      </div>
                      <div class="x-form-input">
                        <input
                          class="form-input-item"
                          type="text"
                          :id="item.idType"
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
                        itemId="PropertiesOfMaterialsId"
                        itemName="PropertiesOfMaterialsName"
                        v-if="item.fieldname == 'PropertiesOfMaterials'"
                      />
                      <BaseCombobox
                        type="tableList"
                        ref="inputStopUsing"
                        id="txtStopUsing"
                        fieldType="stopUsing"
                        displayName="Ngưng sử dụng"
                        value=""
                        itemId="StopUsing"
                        itemName="StopUsingName"
                        v-if="item.fieldname == 'StopUsing'"
                      />
                    </div>
                  </div>

                  <div
                    class="content-menu-filter"
                    style="display: none"
                    :ref="'filter' + index"
                  >
                    <ul class="context-menu-list">
                      <li class="context-menu-item active-menu">
                        <span>* : Chứa</span>
                      </li>
                      <li class="context-menu-item">
                        <span>= : Bằng</span>
                      </li>
                      <li class="context-menu-item">
                        <span>+ : Bắt đầu bằng</span>
                      </li>
                      <li class="context-menu-item">
                        <span>- : Kết thúc bằng</span>
                      </li>
                      <li class="context-menu-item">
                        <span>! : Không chứa</span>
                      </li>
                    </ul>
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
            >
              <td
                v-for="(column, idx) in tableData.columns"
                :key="'td' + index + '' + idx"
                :id="'td' + index + '' + idx"
                :style="column.style"
                :class="column.subClass"
                :title="record[column.fieldname]"
              >
                <div>
                  <div
                    class="cbx-value-column"
                    v-if="
                      column.fieldname == 'UnitConvert' ||
                      column.fieldname == 'ConvertRateOperate' ||
                      column.fieldname == 'ConvertRate'">
                    <BaseCombobox
                      v-if="column.fieldname == 'UnitConvert'"
                      type="cbxform"
                      ref="inputUnit"
                      id="txtUnit"
                      fieldType="Unit"
                      displayName="Đơn vị tính"
                      value=""
                      itemId="UnitId"
                      itemName="UnitName"
                      tabindex=""
                    />
                   
                    <BaseInput
                      v-if="column.fieldname=='ConvertRate'"
                      ref="inputConvertRate"
                      id="txtConvertRate"
                      type="text"
                      fieldType="ConvertRate"
                      displayName="Tỷ lệ chuyển đổi"
                      value=""
                      placeholder=""
                      tabindex=""
                    />
                     <BaseCombobox
                      v-if="column.fieldname == 'ConvertRateOperate'"
                      type="combo"
                      ref="inputConvertRateOperate"
                      id="txtConvertRateOperate"
                      fieldType="ConvertRateOperate"
                      displayName="Phép tính"
                      value=""
                      itemId="MaterialUnitConvertId"
                      itemName="ConvertRateOperate"
                      tabindex=""
                      
                    />
                  </div>

                  <div
                    class="text-value-column"
                    v-else-if="column.fieldname != 'StopUsing'"
                  >
                    <span>{{ record[column.fieldname] }}</span>
                  </div>
                  <div
                    style="left: 16px; min-width: 16px; text-align: center"
                    v-else
                  >
                    <!-- <input type="checkbox" class="" /> -->
                    <div title="" class="check-stop x-item-disabled">
                      <img class="x-grid-checkcolunm" src="" alt="" />
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
// import { directive as onClickaway } from "vue-clickaway";

import BaseInput from "./BaseInput.vue";
import BaseCombobox from "./BaseCombobox.vue";
export default {
  // directives: {
  //   onClickaway: onClickaway,
  // },
  name: "BaseTable",
  components: {
    BaseInput,
    BaseCombobox,
  },

  props: {
    tableData: {
      data: {
        type: Array,
      },
      columns: {
        type: Array,
      },
      indexOpen: -1,
    },
  },
  data() {
    return {
      checkAll: false,

      top: 0,

      openMenu: false,
    };
  },

  methods: {
    // away: function () {
    //   alert(1);
    //   this.openMenu = false;
    //   this.$refs[`filter${this.indexOpen}`][0].style.display = "none";
    // },
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
        this.$refs[`filter${index}`][0].style.display = "block";
        this.openMenu = true;
      } else {
        this.openMenu = false;
        this.$refs[`filter${index}`][0].style.display = "none";
      }
    },
    /**
     * Hàm xử lý khi bấm sửa
     * CreateBy: TTUyen (24/09/2021)
     */
    // btnEditOnClick(index) {
    //   this.$emit(
    //     "edit-btn-click",
    //     false,
    //     FORM_STATE.EDIT,
    //     this.tableData.data[index]
    //   );
    // },
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