<template>
  <div class="grid-container">
    <div class="grid-header">
      <div class="colomn-header"></div>
    </div>
    <div class="grid-body">
      <div class="grip-item-container">
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
                <div class="filter-condition">
                  <div :class="['title-column', item.position]">
                    <span>{{ item.title }}</span>
                  </div>

                  <div
                    class="content-filter-input"
                    v-if="item.typeFilter == 'input'"
                  >
                    <div class="button-choice-operator">
                      <button class="m-btn">*</button>
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
                    v-if="item.typeFilter == 'combo'"
                  >
                    <BaseComboBox
                      type="tableList"
                      ref="inputProperties"
                      id="txtProperties"
                      fieldType="properties"
                      displayName="Tính chất"
                      value=""
                      itemId="PropertiesOfMaterialsId"
                      itemName="PropertiesOfMaterialsName"
                      v-if="item.fieldname =='PropertiesOfMaterials'"
                    />
                    <BaseComboBox
                      type="tableList"
                      ref="inputStopUsing"
                      id="txtStopUsing"
                      fieldType="StopUsing"
                      displayName="Ngưng sử dụng"
                      value=""
                      itemId="StopUsing"
                      itemName="StopUsingName"
                      v-if="item.fieldname =='StopUsing'"
                    />
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
                <div
                  class="text-value-column"
                  v-if="column.fieldname != 'StopUsing'"
                >
                  <span>{{ record[column.fieldname] }}</span>
                </div>
                <div
                  style="left: 16px; min-width: 16px; text-align: center"
                  v-else
                >
                  <input type="checkbox" class="" :id="'checkbox-' + index" />
                </div>
              </td>
            </tr>
          </tbody>
        </table>
        <div class="no-data" v-if="tableData.data.length <= 0">
          <div class="no-data-img"></div>
          <div class="no-data-text">Không có dữ liệu</div>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
// import { FORM_STATE } from "../../js/common/enums";
import BaseComboBox from "../../components/base/BaseCombobox.vue";
export default {
  name: "BaseTable",
  components: {
    BaseComboBox,
  },

  props: {
    tableData: {
      data: {
        type: Array,
      },
      columns: {
        type: Array,
      },
    },
  },
  data() {
    return {
      checkAll: false,

      top: 0,
    };
  },

  methods: {
    /**
     * Xử lý khi click hiển thị tính năng mở rộng
     *CreateBy: TTUyen (02/09/2021)
     */
    btnShowOption(index) {
      console.log(this.tableData.data[index]);
      console.log(this.$refs);
      this.top = this.$refs[`myButton${index}`][0].getBoundingClientRect().top;
      console.log(this.top);
      this.$emit("show-option-click", this.tableData.data[index], this.top);
    },
    /**
     * Hàm xử lý khi bấm sửa
     * CreateBy: TTUyen (02/09/2021)
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
.background-color-white {
  background-color: #fff;
  z-index: 0 !important;
}
</style>