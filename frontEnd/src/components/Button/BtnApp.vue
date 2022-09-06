<template>
    <div class="header__button flex">
        <div class="add__button ms__dropdown">
            <button class="btn btn__add">{{btnName}}</button>

        </div>
    </div>
</template>

<script>
export default {
     props:{
        btnName:String
    },

}
</script>

<style >
</style>