<template>
  <div class="label-field-input">
    <label>
      {{ label }}
      <span v-if="required" style="color: red"> (*) </span>
    </label>
    <slot></slot>
    
  </div>
</template>

<script>
export default {
  name: "BaseLabel",
  components: {},
  props: {
    label: {
      type: String,
    },
    required: {
      type: Boolean,
    },
  },
};
</script>

<style scoped>
@import "../../css/base/label.css";
</style>