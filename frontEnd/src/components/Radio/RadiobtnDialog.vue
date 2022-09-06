<template>
    <label class="flex Radio__gt"> 
        <input  class="radio" type="radio" :checked ="isCheck" :name="namerd" :id="titleRadio" >{{titleRadio}}
        </label>
</template>

<script>
export default {

    props: {
        /**id checkbox */
        idRadio: Number, 
        /**nhãn của check box */
        titleRadio: String,         
        isCheck:Boolean,
        /**tên nhóm radio */
        namerd :String,
        /**Giá trị của radio */
        valueRadio:String,
    },
   
}
</script>

<style scoped>
@import url(../../style/css/base/Radiobtn/Radiobtn.css);
</style>