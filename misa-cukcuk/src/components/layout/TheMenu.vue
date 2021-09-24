<template>
  <div class="menu-container expand">
    <div class="menu-content">
      <router-link class="menu-item"  :class="{ 'menu-item-active': isActive }" to="/material">
        <div class="menu-item-icon icon-material"></div>
        <div class="menu-item-text">Nguyên vật liệu</div>
      </router-link>
    </div>
    
  </div>
</template>
<script>
export default {
  data() {
    return {
      isActive: {
        type: Boolean,
        default: false,
      },
    };
  },
  props: {},
  name: "TheMenu",
};
</script>
<style scoped>
@import "../../css/layout/menu.css";
.router-link-active.menu-item-active {
  background-color: #0199FB;
    color: #ffffff;
}
</style>