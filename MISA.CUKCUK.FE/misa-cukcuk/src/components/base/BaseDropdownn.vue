<template>
  <!-- <div class="dropdown dropdown-filter" @click="toggleDropdown" style="style"> -->
  <div class="button-choice-operator">
    <button @click="btnClickToggleMenu" class="m-btn">{{ contextBtn }}</button>
    <div
      class="dropdown-data content-menu-filter"
      style="display: none"
      ref="dropdown"
    >
      <!-- :class="{ 'active-menu': activeItem(index) }" -->
      <div
        class="context-menu-item"
        v-for="(item, index) in items"
        :key="item.value"
        @click="selectItem(index)"
        :value="item.FilterTextId"
        :class="[
          'dropdown-item',
          currentIndex == index ? 'active-menu' : ''
        ]"
      >
        <div class="dropdown-item-text">
          {{ item.FilterTextName }}
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "BaseDropdown",
  props: {},
  watch: {},
  data() {
    return {
      selectedText: "",
      selectedValue: "",
      isOpen: false,
      items: [
        {
          FilterTextId: "1",
          FilterTextName: "* : Chứa",
        },

        {
          FilterTextId: "2",
          FilterTextName: "= : Bằng",
        },
        {
          FilterTextId: "3",
          FilterTextName: "+ : Bắt đầu bằng",
        },
        {
          FilterTextId: "4",
          FilterTextName: "- : Kết thúc bằng",
        },
        {
          FilterTextId: "5",
          FilterTextName: "! : Không chứa",
        },
      ],
      currentIndex: 0,

      indexChange: -1,

      contextBtn: "*",

    };
  },
  async created() {
    // this.selectItem(0);
  },
  methods: {

    /**
     * Hiển thị dropdown khi click vào btn
     */
    btnClickToggleMenu() {
      this.$emit("btnClickToggleMenu");
    },

  /**
   * Hiển thị danh sách dropdown 
   */
    displayBlock() {
      this.$refs.dropdown.style.display = "block";
    },

    /**
     * Ấn danh sách dropdown
     */
    displayNone() {
      this.$refs.dropdown.style.display = "none";
    },
    
    /**
     * Hàm select item
     * CreatedBy: TTUyen(03/09/2021)
     */
    selectItem(index) {
      this.selectedText = this.items[index].FilterTextId;
      this.selectedValue = this.items[index].FilterTextName;
      this.contextBtn = this.items[index].FilterTextName.charAt(0);
      this.currentIndex = index;
      this.$emit("updateFilterOption", this.selectedText);
      this.$emit("closeDropdown");
    },

    /**
     * Hàm check index của item
     * CreatedBy: TTUyen(03/09/2021)
     */
    activeItem(index) {
      return this.currentIndex == index ? true : false;
    },

    /**
     * Hàm trả về index của item khi so sánh value
     * CreatedBy: TTUyen(03/09/2021)
     */
    getIndexByValue() {
      return this.items.findIndex((element, index) => {
        if (element.value === this.value) {
          return index;
        }
      });
    },

    /**
     * Hàm check event khi click ra bên ngoài dropdown
     * CreateBy: TTUyen ( 03/09/2021)
     */
    close(e) {
      if (!this.$el.contains(e.target)) {
        this.$emit("closeDropdown");
      }
    },
  },

  mounted() {
    document.addEventListener("click", this.close);
  },
};
</script>

<style scoped>
.dropdown-hidden {
  display: none;
}
.content-filter .content-menu-filter {
  position: absolute;
  left: 5px;
  background-color: #fff;
  border: 1px solid #ccc;
  box-shadow: 0 0 5px 3px #ccc;
}

.context-menu-item {
  height: 22px;
  font-size: 13px;
  font-weight: normal;
  text-align: left;
  white-space: nowrap;
  text-decoration: none !important;
  padding: 0px 10px 0px 26px;
}
.context-menu-list {
  text-decoration: none !important;
  list-style: none;
  background-color: #fff;
  box-shadow: 0px 0px 2px 2px #ccc;
}

.context-menu-item.active-menu {
  background: url(../../assets/images/table-circle-blue16.png) no-repeat left
    center;
  background-position: 5px center;
}

.context-menu-item {
  height: 22px;
  font-size: 13px;
  font-weight: normal;
  text-align: left;
  white-space: nowrap;
  text-decoration: none !important;
  padding: 0px 10px 0px 26px;
  display: flex;
  align-items: center;
}

.context-menu-item:hover {
  background-color: #d7e9f4;
}

.context-menu-item .picked, .context-menu-item .picked:hover {
    background-color: #d7e9f4;
    color: #000000 !important;
}
</style>