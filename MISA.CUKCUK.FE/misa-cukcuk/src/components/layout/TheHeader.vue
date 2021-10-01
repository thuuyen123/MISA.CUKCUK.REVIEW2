<template >
  <div class="header">
    <div class="header-logo">
      <div class="header-icon sprite icon-toggle"></div>
      <div class="header-app-name"></div>
    </div>
    <div class="header-info">
      <div class="header-branch">
        <base-combobox
          type="cbxHead"
          ref="inputBranch"
          id="txtBranch"
          fieldType="branch"
          displayName="Đơn vị"
          value=""
          placeholder=""
          itemId="BranchId"
          itemName="BranchName"
          tabindex=""
        />
      </div>
      <div class="header-info-right">
        <div class="header-text">R58.0</div>

        <div class="header-language">
          <base-combobox
            type="cbxHead"
            ref="inputLanguage"
            id="txtLanguage"
            fieldType="language"
            displayName="Đơn vị"
            value=""
            placeholder=""
            :required="true"
            itemId="LanguageId"
            itemName="LanguageName"
          />
        </div>
        <div class="header-announce">
          <div class="sprite icon-announce"></div>
        </div>
        <div class="header-support">
          <div class="sprite icon-phone-support"></div>
        </div>
        <div class="header-info-account">
          <div class="info-avatar icon-avatar"></div>
          <div class="account-name">Trịnh Thu Uyên</div>
          <div class="account-option icon-default-small-arrow"></div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import BaseCombobox from "../base/BaseCombobox.vue";
export default {
  name: "TheHeader",
  components: {
    BaseCombobox,
  },
};
</script>
<style scoped>
@import "../../css/layout/header.css";
</style>