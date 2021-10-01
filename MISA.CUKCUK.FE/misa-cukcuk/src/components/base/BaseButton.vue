<template>
  <button class="m-btn" :class="subClass" @click="btnClick">
    <div :class="['m-btn-icon', iconClass]"></div>
    <div class="m-btn-text" v-if="$slots.default"><slot></slot></div>
  </button>
</template>

<script>
export default {
  name: "BaseButton",
  props: {
    subClass: String,
    iconClass: String,
  },
  methods: {
    /**
     * Bẩm click button
     * CreateBy: TTUyen (28/7/2021)
     */
    btnClick() {
      this.$emit("btn-click");
    },
  },
};
</script>
<style scoped>
@import "../../css/base/button.css";
</style>