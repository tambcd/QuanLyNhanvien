<template>  
    <span :tooltip="tooltipText" :flow="position"><slot></slot></span>
 
</template>

<script>
export default {
  props: {
    /** nhãn tooltip */
    tooltipText: {
      type: String,
      default: "Tooltip txt",
    },
    /** vị trí tooltip */
    position: {
      default: "down",
      type: String,
    },
  },

}
</script>

<style  scoped>

@import url(@/style/css/base/tooltip/tooltip.css);

</style>
