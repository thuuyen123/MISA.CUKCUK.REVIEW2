<template>
  <div class="x-toolbar paging-toolbar">
    <div class="paging-toolbar-setting">
      <div class="p-paging-index">
        <div class="p-index-left">
          <BaseButton
            type="secondary"
            title="Trang đầu"
            iconClass="icon-page-first"
          />
          <BaseButton
            type="secondary"
            title="Trang trước"
            iconClass="icon-page-prev"
          />
        </div>
        <div class="p-index-center">
          <span>Trang</span>
          <input type="text" id="txtPageIndex" />
          <span>trên 2</span>
        </div>
        <div class="p-index-right">
          <BaseButton
            type="secondary"
            title="Trang sau"
            iconClass="icon-page-next"
          />
          <BaseButton
            type="secondary"
            title="Trang cuối"
            iconClass="icon-page-last"
          />
        </div>
        <div class="p-load-paging">
          <BaseButton
            type="secondary"
            title="Tải lại"
            iconClass="icon-btnLoadPaging"
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
          />
        </div>
      </div>
    </div>
    <div class="p-paging-display-records">Hiển thị 1 - 45 kết quả</div>
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
};
</script>

<style scoped>

</style>