<template>
    <div class="input" @keydown.enter="sendDataSearch()">
        <input id="txt__Pb" type="text" class="input__input" placeholder="Tìm theo mã ,tên nhân viên" v-model="txtSreach">
        <button class="input__button icon icon__16" id="btn__Phong__ban" @click="sendDataSearch()">
        </button>
    </div>
</template>

<script>
export default {
    data() {
        return {
            /**txt của input */
            txtSreach:""
        }
    },
    methods: {
         /**
     * Author: TVTam
     * Last Edited: 24/07/2022 
     * gửi từ khóa tìm kiếm lên tra     
     */
        sendDataSearch(){
            this.emitter.emit("TxtChange",this.txtSreach)
        }
    },    

}
</script>
<style scoped>
@import url(../../style/css/base/Input/body__input.css);    
    
</style>