<template>
    <button class="btn__base" :class="{btn__dark:btnType == 1,btn__color:btnType == 2}">
        {{btnName}}
    </button>
</template>

<script>
export default {
 props:{
    /**Nhãn của button */
        btnName:String,
        btnType:String
    },
}
</script>

<style scoped>
.btn__base{
    padding: 0 16px;
    border-radius: 4px ;
    font-size: 13px;
    height: 34px;
    border: none;    
    cursor: pointer;
}
.btn__dark{
    border: 1px solid #111;
    background-color: #fff;
}

.btn__dark:focus{
    background-color: #b8bcc3;
}
.btn__dark:hover{
    background-color: #b8bcc3;
}
.btn__color:focus{
    background-color: #019160 ;
}
.btn__color:hover{
    background-color: #019160 ;
}
.btn__color{
    color: #fff;
    background-color: #2ca01c;
}

.btn__color:focus{
    background-color: #019160 ;
}
.btn__color:hover{
    background-color: #019160 ;
}

</style>