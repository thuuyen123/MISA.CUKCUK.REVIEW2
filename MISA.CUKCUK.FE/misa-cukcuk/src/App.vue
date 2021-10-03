<template>
  <div id="app-container">
    <the-header />
    <div class="content-container">
      <the-menu />
      <the-content />
    </div>
    <BaseLoading :showLoader="isShowLoadFull" />
  </div>
</template>
<script>
import TheContent from "./components/layout/TheContent.vue";
import TheHeader from "./components/layout/TheHeader.vue";
import TheMenu from "./components/layout/TheMenu.vue";
import BaseLoading from "./components/base/BaseLoading.vue";
import { eventBus } from "./main";
export default {
  components: {
    TheContent,
    TheHeader,
    TheMenu,
    BaseLoading,
  },
  data() {
    return {
      isShowLoadFull: false,
    };
  },
  created() {
    eventBus.$on("showLoaderFull", () => {
      this.isShowLoadFull = true;
    });

    eventBus.$on("hideLoaderFull", () => {
      this.isShowLoadFull = true;
    });
  },
};
</script>
<style scoped>
@import "./css/main.css";
#app-container {
  position: absolute;
  top: 0;
  bottom: 0;
  right: 0;
  left: 0;
  display: flex;
  box-sizing: border-box;
  flex-wrap: wrap;
}
.content-container {
  display: flex;
  flex: 1;
  width: 100vw;
  position: relative;
  overflow: hidden;
  flex-basis: auto;
}
</style>
