<template>
  <div class="x-toolbar paging-toolbar">
    <div class="paging-toolbar-setting">
      <div class="p-paging-index">
        <div class="p-index-left">
          <BaseButton
            type="secondary"
            title="Trang đầu"
            iconClass="icon-page-first"
            @btn-click="changPageOnClick('page-first')"
          />
          <BaseButton
            type="secondary"
            title="Trang trước"
            iconClass="icon-page-prev"
            @btn-click="changPageOnClick('page-prev')"
          />
        </div>
        <div class="p-index-center">
          <span>Trang</span>
          <input
            type="text"
            id="txtPageIndex"
            v-model="pageIndex"
            @enter="changePageIndex($event.target.value)"
            @blur="changePageIndex($event.target.value)"
          />
          <span>trên {{ totalPages }}</span>
        </div>
        <div class="p-index-right">
          <BaseButton
            type="secondary"
            title="Trang sau"
            iconClass="icon-page-next"
            @btn-click="changPageOnClick('page-next')"
          />
          <BaseButton
            type="secondary"
            title="Trang cuối"
            iconClass="icon-page-last"
            @btn-click="changPageOnClick('page-last')"
          />
        </div>
        <div class="p-load-paging">
          <BaseButton
            type="secondary"
            title="Tải lại"
            iconClass="icon-btnLoadPaging"
            @btn-click="reloadDataTable"
          />
        </div>
        <div class="p-paging-size">
          <BaseCombobox
            itemId="PageSize"
            itemName="PageSizeCode"
            fieldType="pageSize"
            type="pageSizeList"
            id="txtPageSize"
            ref="inputPageSize"
            value=""
            displayName="PageSize"
            styleList="bottom: 23px !important;"
            @inputCombo="changePageSize"
          />
        </div>
      </div>
    </div>
    <div class="p-paging-display-records">
      Hiển thị {{ startRecord }} - {{ endRecord }} trên {{ totalRecord }} kết
      quả
    </div>
  </div>
</template>

<script>
import BaseCombobox from "../../components/base/BaseCombobox.vue";
import BaseButton from "../../components/base/BaseButton.vue";
export default {
  name: "BasePaging",
  components: {
    BaseButton,
    BaseCombobox,
  },
  props: {
    totalRecord: Number,

    totalPages: Number,

    pageIndex: Number,

    pageSize: Number,
  },

  data() {
    return {
      startRecord: 1,

      endRecord: 100,

      // pageSize: 100,

      // pageIndex: 1,

      statusPage: 0,

      pageIndexOld: 1,
    };
  },
  created() {
    this.pageIndexOld = this.pageIndex;
  },
  watch: {
    totalRecord(value) {
      if (value < this.pageSize) {
        this.endRecord = value;
      }
      if (value == 0) {
        this.statusPage = 0;
      }
      this.updatePage();
    },
    pageIndex(){
      this.updatePage();
    },
    pageSize(){
      this.updatePage();
    }
  },
  methods: {
    /**
     * Hàm bấm các lựa chọn ở trang
     */
    changPageOnClick(statusPage) {
      let me = this;
      switch (statusPage) {
        case "page-first":
          if (me.pageIndex != 1) {
            me.pageIndex = 1;
            if (this.totalRecord < this.pageSize) {
              me.startRecord = 1;
              me.endRecord = me.totalRecord;
            } else {
              me.startRecord = me.pageSize * (me.pageIndex - 1) + 1;
              me.endRecord = me.pageSize * me.pageIndex;
            }
            me.$emit("changePageIndex", me.pageIndex);
          }

          break;
        case "page-prev":
          if (me.pageIndex > 1) {
            me.pageIndex--;
            if (this.totalRecord < this.pageSize) {
              me.startRecord = 1;
              me.endRecord = me.totalRecord;
            } else {
              me.startRecord = me.pageSize * (me.pageIndex - 1) + 1;
              me.endRecord = me.pageSize * me.pageIndex;
            }
            me.$emit("changePageIndex", me.pageIndex);
          }

          break;
        case "page-next":
          if (me.pageIndex < me.totalPages) {
            me.pageIndex++;
            if (me.pageIndex == me.totalPages) {
              me.startRecord = me.pageSize * (me.totalPages - 1);
              me.endRecord = me.totalRecord;
            } else {
              me.startRecord = me.pageSize * (me.pageIndex - 1) + 1;
              me.endRecord = me.pageSize * me.pageIndex;
            }
            me.$emit("changePageIndex", me.pageIndex);
          }
          break;
        case "page-last":
          if (me.pageIndexOld != me.totalPages) {
            me.pageIndex = me.totalPages;
            me.startRecord = me.pageSize * (me.totalPages - 1);
            me.endRecord = me.totalRecord;
            me.$emit("changePageIndex", me.pageIndex);
          }

          break;
        default:
          me.pageIndex = statusPage;
          if (me.pageIndex == me.totalPages) {
            me.startRecord = me.pageSize * (me.totalPages - 1);
            me.endRecord = me.totalRecord;
            me.$emit("changePageIndex", me.pageIndex);
          } else {
            me.startRecord = me.pageSize * (me.pageIndex - 1) + 1;
            me.endRecord = me.pageSize * me.pageIndex;
            me.$emit("changePageIndex", me.pageIndex);
          }
          break;
      }
    },

    //Thay dổi số trang
    changePageIndex(value) {
      let me = this;
      value = parseInt(value);
      if (
        value == null ||
        value == "" ||
        value < 1 ||
        value > me.totalPages ||
        value == me.pageIndexOld
      ) {
        me.pageIndex = me.pageIndexOld;
      } else {
        me.$emit("changePageIndex", value);
        me.updatePage();
        me.pageIndexOld = me.pageIndex;
      }
    },
    /**
     * Thay đổi số bản ghi trong một trang
     */
    changePageSize(pageSize) {
      this.pageSize = pageSize;
      this.$emit("changePageSize", pageSize);
      this.updatePage();
    },

    /**
     * Cập nhật trang
     */
    updatePage() {
      let me = this;
      if (me.totalRecord < me.pageSize) {
        me.startRecord = 1;
        me.endRecord = me.totalRecord;
      } else if (me.pageIndex == me.totalPages) {
        me.startRecord = me.pageSize * (me.totalPages - 1);
        me.endRecord = me.totalRecord;
      } else {
        me.startRecord = me.pageSize * (me.pageIndex - 1) + 1;
        me.endRecord = me.pageSize * me.pageIndex;
      }
    },
    // tải lại trang theo page
    reloadDataTable() {
      this.$emit("btnReloadDataTable");
    },
  },
};
</script>

<style scoped>
@import "../../css/base/paging.css";
</style>