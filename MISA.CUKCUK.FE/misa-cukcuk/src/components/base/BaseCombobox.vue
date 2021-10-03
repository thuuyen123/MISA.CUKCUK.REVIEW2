
<template>
  <div class="combobox" v-on-clickaway="away">
    <div
      class="combobox-header-wrapper"
      :class="{
        'input-border-red': !(isValid && isRequiredValid) && !focusing,
      }"
      :id="id"
      :title="tooltip"
      :tabindex="tabindex"
      :type="type"
    >
      <input
        type="text"
        class="combobox-value"
        v-model="currentName"
        @input="onInput($event.target.value)"
        @blur="onBlur($event.target.value)"
        :tabindex="tabindex"
        @keydown.down="moveDown"
        @keydown.up="moveUp"
        @keydown.tab="onTabOut"
        @keydown.enter="onItemSelect(currentFocus)"
      />

      <div class="form-control">
        <div
          class="combobox-open"
          @click="toggleItems"
          v-if="type == 'cbxHead'"
        >
          <div class="cbx-icon icon-default-small-arrow"></div>
        </div>
        <div class="combobox-open" @click="toggleItems" v-else>
          <div class="cbx-icon x-form-trigger-default"></div>
        </div>
        <div class="cbx-add" v-if="type == 'cbxform'" @click="btnShowFormAdd">
          <div class="cbx-icon sprite icon-form-add-trigger"></div>
        </div>
      </div>
    </div>
    <ul
      :class="['combobox-list', opened ? 'isshow' : 'isnone']"
      :style="styleList"
    >
      <!-- <li v-if="fieldType == 'stock'" class="combo-item combo-item-header">
        <div class="combo-item-text combo-col-1">Mã</div>
        <div class="combo-item-text combo-col-2">Tên</div>
      </li> -->
      <li
        v-for="(item, index) in itemList"
        :key="index"
        :class="[
          'combobox-item',
          item[itemId] == currentId ? 'active' : '',
          index == currentFocus ? 'picked' : '',
        ]"
        @click="clickItem(item[itemId], item[itemName])"
      >
        <!-- <div v-if="fieldType == 'stock'" class="combobox-item-text combo-col-1">
          {{ item[itemCode] }}
        </div> -->
        <div class="combobox-item-text combo-col-2">
          {{ item[itemName] }}
        </div>
      </li>
    </ul>
  </div>
</template>
<script>
// import axios from "axios";
import { directive as onClickaway } from "vue-clickaway";
import { MESSAGE } from "../../js/common/const";
import axios from "axios";
import { CONFIG } from "../../js/common/config";
export default {
  directives: {
    onClickaway: onClickaway,
  },
  name: "BaseCombobox",
  props: {
    type: String,
    id: String,
    itemId: String,
    itemName: String,
    itemCode: String,
    selectedId: String,
    tabindex: String,
    value: String,
    required: {
      type: Boolean,
      default: false,
    },
    displayName: {
      type: String,
      default: "",
    },
    fieldType: String,
    styleList: String,
  },
  created() {
    this.loadDataCombobox();
    setTimeout(() => {
      this.initChoice();
      this.filteredList("");
    }, 150);
  },
  data() {
    return {
      itemList: [],

      currentId: "-1",

      currentName: "",

      opened: false,

      tooltip: "",

      currentFocus: -1,

      //input được focus
      focusing: false,

      // Kiểm tra đã hợp lệ chưa
      isValid: true,

      // Kiểm tra required không rỗng
      isRequiredValid: true,

      dataCombobox: [],

      tempName: "",
    };
  },
  watch: {
    /**
     * Xử lý khi thay đổi lựa chọn
     *  CreateBy: TTUyen (30/8/2021)
     */
    selectedId: function () {
      this.initChoice();
    },
  },
  methods: {
    btnShowFormAdd() {
      this.$emit("btnShowFormAdd", this.fieldType);
    },
    /**
     * Đóng combobox khi click ra ngoài
     * CreateBy: TTUyen (30/8/2021)
     */
    away: function () {
      this.opened = false;
      this.currentName = this.tempName;
    },

    /**
     * Hàm đóng list combobx
     * CreateBy: TTUyen (30/8/2021)
     */
    toggleItems() {
      this.opened = !this.opened;
    },
    /**
     * Lọc dữ liệu
     * CreateBy: TTUyen (08/9/2021)
     */
    filteredList(searchText) {
      this.loadDataCombobox();
      console.log(this.dataCombobox);
      if (searchText == null || searchText == "") {
        this.itemList = this.dataCombobox;
      } else {
        this.itemList = this.dataCombobox.filter((data) =>
          data[this.itemName].toLowerCase().includes(searchText.toLowerCase())
        );
      }
    },

    /**
     *  Lấy dữ liệu theo để hiện thị
     * CreateBy: TTUyen (30/8/2021)
     */
    initChoice() {
      try {
        let me = this;
        this.focusing = true;
        me.loadDataCombobox();
        if (
          me.fieldType != "language" &&
          me.fieldType != "branch" &&
          me.fieldType != "pageSize"
        ) {
          if ((me.selectedId + "").length > 0) {
            me.dataCombobox.forEach((item) => {
              if (me.selectedId == item[me.itemId]) {
                me.currentName = item[me.itemName];
                me.currentId = item[me.itemId];
                me.tempName = me.currentName;
              }
            });
            this.isValid = true;
            this.isRequiredValid = true;
          } else {
            me.currentId = "-1";
            me.currentName = "";
            me.tempName = me.currentName;
          }
        } else {
          if (me.fieldType != "pageSize") {
            me.currentId = me.dataCombobox[0][me.itemId];
            me.currentName = me.dataCombobox[0][me.itemName];
            me.tempName = me.currentName;
          } else {
            me.currentId = me.dataCombobox[2][me.itemId];
            me.currentName = me.dataCombobox[2][me.itemName];
            me.tempName = me.currentName;
          }
        }
      } catch (error) {
        console.log(error);
      }
    },
    async loadDataCombobox() {
      let me = this;
      switch (me.fieldType) {
        case "branch":
          me.dataCombobox = [
            { BranchId: "CCC112", BranchName: "---Chuỗi cửa hàng----" },
            { BranchId: "KH1233", BranchName: "Kho tổng" },
            { BranchId: "ADR422", BranchName: "Ánh dương restaurant" },
            { BranchId: "CN8122", BranchName: "CN8" },
            { BranchId: "NHBHS122", BranchName: "Nhà hàng buffer Hải sản" },
          ];
          break;
        case "language":
          me.dataCombobox = [
            { LanguageId: "TV001", LanguageName: "Tiếng Việt" },
            { LanguageId: "TA002", LanguageName: "Tiếng Anh" },
            { LanguageId: "TP003", LanguageName: "Tiếng Pháp" },
            { LanguageId: "TT004", LanguageName: "Tiếng Trung" },
          ];
          break;
        case "properties":
          me.dataCombobox = [
            {
              PropertiesOfMaterialId: "NVL122",
              PropertiesOfMaterialName: "Nguyên vật liệu",
            },
            {
              PropertiesOfMaterialId: "DOC222",
              PropertiesOfMaterialName: "Đồ uống đóng chai",
            },
            {
              PropertiesOfMaterialId: "MHK212",
              PropertiesOfMaterialName: "Mặt hàng khác",
            },
          ];
          break;
        case "stopUsing":
          me.dataCombobox = [
            {
              StopUsing: 0,
              StopUsingName: "Không",
            },
            {
              StopUsing: 1,
              StopUsingName: "Có",
            },
          ];
          break;
        case "pageSize":
          me.dataCombobox = [
            {
              PageSizeCode: "25",
              PageSize: 25,
            },
            {
              PageSizeCode: "50",
              PageSize: 50,
            },
            {
              PageSizeCode: "100",
              PageSize: 100,
            },
          ];
          break;
        case "exprityType":
          me.dataCombobox = [
            {
              ExprityType: 0,
              ExprityTypeName: "Ngày",
            },
            {
              ExprityType: 1,
              ExprityTypeName: "Tháng",
            },
            {
              ExprityType: 2,
              ExprityTypeName: "Năm",
            },
          ];
          break;
        case "unit":
          try {
            await axios
              .get(`${CONFIG.MY_URL}Units`)
              .then((res) => {
                me.dataCombobox = res.data.Data;
              })
              .catch((error) => {
                console.log(error);
              });
          } catch (error) {
            console.log(error);
          }

          console.log(me.dataCombobox);
          break;
        case "stock":
          try {
            await axios
              .get(`${CONFIG.MY_URL}Stocks`)
              .then((res) => {
                me.dataCombobox = res.data.Data;
              })
              .catch((error) => {
                console.log(error);
              });
          } catch (error) {
            console.log(error);
          }
          break;
        default:
          break;
      }
    },
    /**
     * Xử lý khi lick và vào các item
     * CreateBy: TTUyen (30/8/2021)
     */
    clickItem(itemValue, itemName) {
      this.currentId = itemValue;
      this.currentName = itemName;
      this.tempName = this.currentName;
      this.opened = false;

      this.$emit("inputCombo", itemValue);
      this.$emit("comboboxOnSelect");
      this.isValid = true;
      this.isRequiredValid = true;
    },

    /**
     * Hàm tab khỏi ô input
     *CreateBy: TTUyen (08/9/2021)
     */
    onTabOut() {
      this.opened = false;
    },

    /**
     * Hàm xử lý khi blur input
     * CreateBy: TTUyen (30/8/2021)
     */
    onBlur(value) {
      let me = this;
      me.focusing = false;
      me.validateInput(value);
    },
    /**
     * Hàm xử lý khi nhập input
     * CreateBy: TTUyen (30/8/2021)
     */
    onInput(value) {
      let me = this;
      me.focusing = true;
      me.opened = true;
      if (this.fieldType != "pageSize") {
        me.$emit("inputCombo", value);
      }
      me.validateInput(value);
      me.filteredList(value);
    },
    /**
     * Hàm định dạng giá trị trong input
     * CreateBy: TTUyen (30/8/2021)
     */
    validateInput(value) {
      try {
        let me = this;
        //Kiểm tra nhập các trường bắt buộc
        if (!me.required && value == "") {
          me.isValid = true;
          me.isRequiredValid = true;
          return;
        }

        //Rỗng và bắt buộc
        if (value == "" && me.required) {
          me.isRequiredValid = false;
          me.tooltip = MESSAGE.CANT_BE_NULL.format(me.displayName);
          return;
        } else {
          me.isRequiredValid = true;
        }
        if (me.isRequiredValid && me.isValid) {
          me.tooltip = "";
        }
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * Chọn item khi click enter
     * CreateBy: TTUyen (08/9/2021)
     */
    onItemSelect(index) {
      if (index < 0) {
        this.currentId = -1;
        this.currentFocus = -1;
        this.tempName = "";
        return;
      }

      if (this.itemList.length > 0) {
        let itemId = this.itemList[index][this.itemId];
        console.log(itemId);
        let itemName = this.itemList[index][this.itemName];

        this.clickItem(itemId, itemName);
      }
      this.opened = false;
    },

    /**
     * Xử lý khi bàn phím đi lên
     * CreateBy: TTUyen (08/9/2021)
     */
    moveUp(event) {
      event.preventDefault();
      if (this.currentFocus > 0 && this.opened) {
        this.currentFocus--;
      }
      if (this.currentFocus < 0) {
        this.opened = false;
      }
    },

    /**
     * Xử lý khi nhấn bàn phím đi xuống
     * CreateBy: TTUyen (08/9/2021)
     */
    moveDown(event) {
      event.preventDefault();
      this.opened = true;
      if (this.currentFocus < this.itemList.length - 1 && this.opened) {
        this.currentFocus++;
      }
    },
    /**
     * Hàm reset lại các thuộc tính khi thoát khỏi form
     * CreateBy: TTUyen (30/8/2021)
     */
    reset() {
      this.isValid = true;
      this.isRequiredValid = true;
    },
  },
};
</script>
<style scoped>
@import "../../css/base/combobox.css";
</style>