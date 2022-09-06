<template>
    <div class="combobox__ft">
        <div class="combobox__vt">
            <div class="combobox">
                <div class="combobox__data" v-show="iShowCombobox" >
                    <div class="combobox_item" v-for="item in listOption" :key="item.id"
                        :class="{ selecter: isCheckSelect === item.id }"
                        @click="selectNumberPage(item.id,item.title)"> {{ item.title }}                        
                    </div>
                </div>
                <input id="txt__Vt" type="text" class="combobox__input" v-model="txtInputCobobox">
                <button class="icon icon__24 combobox__button" id="btn__vi__tri" :class="{comboboxdl__button__up:isCheckBtnup}"
                    @click="openSelectPage()" > </button> 
            </div>
        </div>
    </div>
</template>

<script>
    // import ref from 'vue'

export default {
    props: {
        listOption: [],

    },
    data() {
        return {
            /** biến kiểu tra đóng mở combobox */
            iShowCombobox: false,
            /** xoay icon */
            isCheckBtnup:false,
            /**txt input trong combobox */
            txtInputCobobox: "10 bản ghi trên 1 trang",
            /**chỉ định item nào được chọn */
            isCheckSelect:1,

        }
    },
    methods: {

        /**
        * thay đổi giá trị của combobox page và gửi dữ liệu về EmployeeMain            
        * Author: TVTam
        * Last Edited: 1/08/2022  *    
        */
        selectNumberPage(id,title){
            this.iShowCombobox = !this.iShowCombobox;
            this.txtInputCobobox = title;
            this.isCheckSelect = id;  
            this.$emit('changePage',id)
            
        },
        /**
        * mở hộp chọn số bản ghi trên trang            
        * Author: TVTam
        * Last Edited: 1/08/2022  *    
        */
        
        openSelectPage(){
            this.iShowCombobox = !this.iShowCombobox;
            this.isCheckBtnup = !this.isCheckBtnup;
        }
    },   


}
</script>

<style scoped>
@import url(../../style/css/base/combobox/combobox.css);
</style>