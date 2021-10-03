<template>
  <div class="dropdown dropdown-filter" @click="toggleDropdown">
    <div class="dropdown-text">{{ selectedText }}</div>
    <div class="box-icon">
      <div class="sprite icon-arrow-down" :class="{ rotate: isRotate }"></div>
    </div>
    <div
      class="dropdown-data"
      :class="{ 'dropdown-hidden': isClose }"
      @click="changePageSize"
    >
      <div
        class="dropdown-item"
        v-for="(item, index) in items"
        :key="item.value"
        @click="selectItem(index)"
        :value="item.value"
        :class="{ 'dropdown-selected': activeItem(index) }"
      >
        <div class="dropdown-item-text">
          {{ item.text }}
        </div>
      </div>
    </div>
  </div>
</template>

<script>
// import { eventBus } from "../../main.js";
export default {
  name: "BaseDropdown",
  props: {
    pageSize: Number,
  },

  data() {
    return {
      isClose: true,
      selectedText: "",
      selectedValue: "",
      items: [
        { text: "10 bản ghi trên 1 trang", value: 10 },
        { text: "20 bản ghi trên 1 trang", value: 20 },
        { text: "30 bản ghi trên 1 trang", value: 30 },
        { text: "50 bản ghi trên 1 trang", value: 50 },
        { text: "100 bản ghi trên 1 trang", value: 100 },
      ],
      currentIndex: 0,
      isRotate: false,

      indexChange: -1,
    };
  },
  async created() {
    this.selectItem(0);
  },
  watch: {
    pageSize(valuePage) {
      this.items.forEach((item,index) => {
        if(item.value ==  valuePage){
          this.selectItem(index);
        }
      });
    },
  },
  methods: {
    /**
     * Hàm bắt sự kiện click gửi value sang cho bên employeeDetail
     * CreatedBy: TTUyen(03/09/2021)
     */
    changePageSize() {
      this.$emit("changePageSize", this.selectedValue);
    },

    /*
     * Hàm xử lý việc toggle dropdown
     * CreatedBy: TTUyen(03/09/2021)
     */
    toggleDropdown() {
      this.isClose = !this.isClose;
      this.isRotate = !this.isRotate;
    },

    /**
     * Hàm select item
     * CreatedBy: TTUyen(03/09/2021)
     */
    selectItem(index) {
      this.selectedText = this.items[index].text;
      this.selectedValue = this.items[index].value;
      this.currentIndex = index;
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
        this.isClose = true;
        this.isRotate = false;
      }
    },
  },

  mounted() {
    document.addEventListener("click", this.close);
  },
};
</script>

<style scoped>
@import url(../../css/base/dropdown.css);
</style>