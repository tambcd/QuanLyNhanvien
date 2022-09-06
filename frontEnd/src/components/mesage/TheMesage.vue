<template>
    <div class="messing" ref="Close__error">
        <div class="messing__content">
            <div class="messing__body">
                <div class="content flex">
                    <div class="icon__message">
                        <div class=" icon icon__48 "
                            :class="{ icon__error: typeMessage == 1, icon__warning: typeMessage == 2, icon__mes: typeMessage == 3 }">
                        </div>
                    </div>
                    <div class="message__title">
                        <span>{{ titleMessage }}</span>

                        <!-- <br /> -->
                    </div>
                </div>
                <div class="messing__line"></div>

                <!-- btn errror -->
                <div class="footer__error flex" v-if="typeMessage === 1">
                    <TheButton btnName="Đóng" class="btnError__close" @click="closeThisMessage()" btnType="2" />
                </div>

                <!-- btn warning -->
                <div class="footer__warning flex" v-if="typeMessage === 2">
                    <TheButton btnName="Có" class="btnWarn__Yes" @click="deleteEmployee()" btnType="2" />
                    <TheButton btnName="Không" class="btnWarn__close" @click="closeThisMessage()" btnType="1" />
                </div>

                <!-- btn mesage -->
                <div class="footer__mesage flex" v-if="typeMessage === 3">
                    <div class="footer__right flex">
                        <TheButton btnName="Có" class="btnWarn__Yes" @click="closeThisDialogAndSave()" btnType="2" />
                        <TheButton btnName="Không" class="btnWarn__close" @click="closeThisDialog()" btnType="1" />

                    </div>
                    <div class="footer__left">
                        <TheButton btnName="hủy" class="btnWarn__close" @click="closeThisMessage()" btnType="1" />
                    </div>


                </div>

            </div>

        </div>
    </div>
</template>

<script>
import { deleteEmployee, deleteMultiEmployee } from "@/api/api.js";
import {Resource} from '@/Resource/Resource'

import { createToaster } from "@meforma/vue-toaster";
const toaster = createToaster({ position: 'top' });

export default {
    props: {
        /** kiểmt của thông báo */
        typeMessage: Number,
        /**nội dung thông báo */
        titleMessage: String,
        /**khóa chính nhân viên */
        idEmployyee: {
            typeof: String,
            default: ""
        },
        /**mảng khóa chính nhân viên */
        listIdEmployyee: {
            typeof: Array,
            default: []
        }
    },
    data() {

    },
    mounted() {
        /**
        * check forcus khi mở thông báo              
        * Author: TVTam
        * Last Edited: 27/07/2022  *    
         */
        this.$refs["Close__error"].querySelector("button").focus();

    },
    methods: {
        /**
         * đóng hộp thông báo                
         * Author: TVTam
         * Last Edited: 27/07/2022  *  
         */
        closeThisMessage() {
            this.$emit("ClosethisMg");
            this.emitter.emit("foucusInputError");
        },
        /**
        * Author: TVTam
        * Last Edited: 27/07/2022  *  
        * đóng form nhập liệu nhân viên              
         */
        closeThisDialog() {
            this.$emit("ClosethiDl");
        },
        /**
        * đóng hộp thông báo và thực hiện cất dữ liêu              
        * Author: TVTam
        * Last Edited: 27/07/2022  *  
         */
        closeThisDialogAndSave() {
            this.$emit("CloseThisDialogAndSave");
        },

        /**
        * thực hiện xóa và xóa nhiều              
        * Author: TVTam
        * Last Edited: 27/07/2022  *  
         */
        async deleteEmployee() {
            // check nếu nhân nut xóa 1 thì data của idEmployee đc cập nhập
            if (this.idEmployyee !== "") {
                await deleteEmployee('Employees', this.idEmployyee
                    , () => {
                        // Trường hợp thành công nhận về dữ liệu thì gán lại các thông tin vào bảng
                        toaster.error(Resource.VN_DeleteSuccess),
                            setTimeout(toaster.clear, 2000);
                        // check chon nhieu
                        this.emitter.emit("deleteOneSuccefull", this.idEmployyee)
                        this.$emit("LoadingDataDelete");


                    }, (error) => {
                        // Trường hợp thất bại thì hiển thị toastMessage lỗi và ghi rõ lỗi xảy ra.
                        toaster.error(Resource.VN_DeleteFailure);
                        console.log(error);


                    })

            }
            // xóa nhiều thì mảng xóa đc cập nhập data
            else {
                let a = await deleteMultiEmployee('Employees', this.listIdEmployyee
                    , (error) => {
                        // Trường hợp thất bại thì hiển thị toastMessage lỗi và ghi rõ lỗi xảy ra.
                        toaster.error(Resource.VN_DeleteFailure);
                        console.log(error);

                    });
                if (a) {
                    toaster.error(Resource.VN_DeleteSuccess);
                    // chuyền thông báó xóa thành công để clear mảng xóa nhiều
                    this.emitter.emit("deleteSuccefull")
                    this.$emit("LoadingDataDelete");
                }

            }
        }
    },

}
</script>

<style scoped>
@import url(@/style/css/base/mesage/message.css);
</style>