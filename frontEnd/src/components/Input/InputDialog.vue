<template>
    <div>
        <div class="title__pp">{{ txtInput }} <span v-if="typeInput == 1" style="color: red;">{{ " " }}
                *</span></div>
        <TheTooltip position="down" :tooltipText="txtInput + ' không được để trống.'"
            v-if="inputError && typeInput == 1">
            <div class="input__pp">
                <input :type="text" v-model="valueInput" class="input__eror" >
            </div>
        </TheTooltip>
        <div class="input__pp" v-else>
            <input :type="typeCheck" v-model="valueInput" />
        </div>
    </div>
</template>
 
<script>
export default {
    props: {
        /**nhãn input*/ 
        txtInput: String,
        
        /**kiểu của input : cho trống hay ko ? */
        typeInput: String,
        
        /**kiểu của input : date out text */
        typeCheck: {
            type: String,
            default: "text",
        },
       
       /** nội dung input truyển xuống khởi tạo*/       
        valueStartInput: {
            type: String,
            default: "",
        },
        
         /** trạng thái vaildate ban đầu*/  
        inputError: {
            typeof: Boolean,
            default: true
        },
        
    },
    data() {
        return {            
            /** txt input*/
            valueInput: "",
            /**kiểm tra validate */        
            checkErro: false,
            
        };
    },

    mounted() {   
        this.valueInput = this.valueStartInput;               
    },
   
    watch: {

        /**
        * validate input : check type validate thỏa mãn thì thực hiện validate
        * @param : dữ liệu của input            
        * Author: TVTam
        * Last Edited: 1/08/2022  *    
        */        
        valueInput(dataInput) {
            if (this.typeInput == 1) {
                // check trơngs
                if (dataInput === "") {
                    this.checkErro = true;
                }
                else {
                    this.checkErro = false;
                }
            }
            // gửi kết quả validate lên form thêm mới
            this.$emit("DataInput", dataInput, this.checkErro);

        },
    },
    components: { }
}
</script>

<style scoped>
.input__eror {
    border: 1px solid red;
}

.input__ok {
    border: 1px solid #babec5;

}
</style>