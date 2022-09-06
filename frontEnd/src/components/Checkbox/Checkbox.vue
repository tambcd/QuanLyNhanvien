<template>
    <input type="checkbox" class="checkbox" @click="checkboxEvent" :value="modelValue"  />
</template>
<script>
export default {
     props: {
    modelValue: String,//Dữ liệu radio binding đến component cha
  },
    
  methods: {
     checkboxEvent() {
      this.$emit("onCheckBox");
    },
  },
}
</script>
<style scoped>
.checkbox {
    width: 18px;
    height: 18px;
    border: 1px solid #b8b8b8;
    align-self: center;
    cursor: pointer;
    accent-color: #2ca01c;
}
</style>