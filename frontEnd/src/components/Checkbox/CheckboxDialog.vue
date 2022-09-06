<template>
    <div class="option option__kh">
        <input type="checkbox" name="" :id="idCheckbox">
        <span>{{titleCheckbox}}</span>
    </div>
</template>

<script>
export default {

    props:{
        /** id checkbox*/
        idCheckbox:Number, 
        /**nhãn của check box */
        titleCheckbox:String 
    }

}
</script>

<style>
</style>