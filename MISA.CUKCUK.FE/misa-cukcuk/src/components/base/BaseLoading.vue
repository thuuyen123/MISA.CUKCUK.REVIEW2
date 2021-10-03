<template>
  <div class="model-loader" :class="{'hide': !showLoader}">
    <div class="loader">
      <img src="../..//assets/images/loading.gif" alt="">
    </div>
  </div>
</template>

<script>
export default {
  name: "BaseLoading",

  props: {
    showLoader:{
      type: Boolean,
      default: false,
    },
  },

  methods: {},

  watch: {},
};
</script>

<style scoped>
@import "../../css/base/loader.css";
.model-loader.hide{
    display: none;
}
</style>