<template>
    <div class="combobox__dl" @keydown.enter="enterCombobox()" @keydown.down="downCombobox()" 
        @keydown.up="upCombobox()">
        <div class="comboboxdl">
            <TheTooltip :tooltipText="nameTooltip" position="up" v-if="errorCombobox">
                <input id="txt__Pb" type="text" class="comboboxdl__input input__eror" v-model="tilieInput"
                    @change="checkValidate()" autocomplete="off">
            </TheTooltip>

            <input v-else id="txt__Pb" type="text" class="comboboxdl__input " v-model="tilieInput"
                @change="checkValidate()" autocomplete="off">

            <button class="comboboxdl__button icon icon__16 dropdown__icon" tabindex="1000"
                @keydown.enter="enterCombobox()" :class="{ comboboxdl__button__up: isCheckbtnup }" id="btn__Phong__ban"
                @click="showCombobox()"> </button>
            <div class="comboboxdl__data" v-show="isCombobox">
                <div class="table__dateDr">
                    <table class="droptabledata" :class="{ select: check }">
                        <thead>
                            <tr>
                                <th id="codePs">Mã đơn vị</th>
                                <th id="namePs">Tên đơn vị</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr v-for="item in departmentsSearch" :key="item.departmentId"
                                @click="selectDeparment(item.departmentId, item.departmentName)"
                                :class="{ slect__combobox: isCheck == item.departmentId }">
                                <td>{{ item.departmentCode }}</td>
                                <td>{{ item.departmentName }}</td>
                            </tr>
                        </tbody>
                    </table>
                </div>

                <div class="foter__DataTable flex">
                    <div class="icon icon__24 btn__edip"></div>
                    <div class="title__edip">Cập nhật cơ cấu tổ chức</div>
                </div>
            </div>
        </div>
    </div>

</template>

<script>
import { createToaster } from "@meforma/vue-toaster";
const toaster = createToaster({ position: 'top' });
import { get } from "@/api/api.js";
import Resource from '@/Resource/Resource';


export default {
    props: {
        valueStartInput: {
            typeof: String,
            default: ""
        },
        errorCombobox: {
            typeof: Boolean,
            default: false
        },
        // Kiểu combobox
        typeCombobox: {
            typeof: String,
            default: "1"
        },
        // Kiểu combobox
        urlRequest: {
            typeof: String,
            default: "Departments"
        },
        nameTooltip: String
    },
    data() {
        return {
            /**tất cả phòng ban */
            departments: [],
            /**danh sách phòng ban lọc */
            departmentsSearch: [],
            /**is ẩn hiện combobox */
            isCombobox: false,
            /**is icon btn */
            isCheckbtnup: false,
            /**kiểm tra phòng ban khi chọn */
            isCheck: "",
            /** txt của input */
            tilieInput: "",
            /**check vakidate combobox */
            checkError: false,
            /**Id phòng ban được chọn */
            idDeparment: "",

        };
    },
    async created() {
        /**
        * Author: TVTam
        * Last Edited: 26/07/2022  
        * Lấy dữ liệu phòng ban           
        */

        await get(this.urlRequest
            , (response) => {
                // Trường hợp thành công nhận về dữ liệu thì gán lại vào mảng Departments
                this.departments = response.data;
                //thực hiện search
                this.departmentsSearch = response.data;
                response.data.forEach(element => {
                    if (this.valueStartInput === element.departmentId) {
                        this.tilieInput = element.departmentName;
                        this.idDeparment = element.departmentId;
                        // gửi data và checkvalida qua dialog
                        this.emitter.emit("DeparmentData", { txtInput: this.tilieInput, idDepartment: this.idDeparment, validateDepartment: this.checkError });
                    }
                });
            }, () => {
                // Trường hợp thất bại thì hiển thị toastMessage lỗi và ghi rõ lỗi xảy ra.
                toaster.error(Resource.VN_Departments);
            })

    },
    methods: {
        /**
         * mở drop menu                  
         * Author: TVTam
         * Last Edited: 23/07/2022  
         */

        showCombobox() {
            this.isCombobox = !this.isCombobox;
            this.isCheckbtnup = !this.isCheckbtnup;
        },

        /**
         * gán giá trị lên input combobox  
         * @param : id phòng ban 
         * @param : tên phòng ban
         *          
         * Created: TVTam 26/07/2022  
         */
        selectDeparment(id, name) {
            this.showCombobox()
            this.departmentsSearch = this.departments
            this.isCheck = id,
                this.tilieInput = name;
            this.checkError = false;
            this.idDeparment = id;
            this.emitter.emit("DeparmentData", { txtInput: this.tilieInput, idDepartment: this.idDeparment, validateDepartment: this.checkError });
        },


        /**
        * kiểm tra valide đơn vị   
        * Author: TVTam
        * Last Edited: 26/07/2022  
        */
        checkValidate() {
            if (this.tilieInput.trim() === "") {
                this.checkError = true;
                this.idDeparment = "";
                this.emitter.emit("DeparmentData", { txtInput: this.tilieInput, idDepartment: this.idDeparment, validateDepartment: this.checkError });
            }
            else {
                for (let index = 0; index < this.departments.length; index++) {
                    if (this.tilieInput === index.departmentName) {
                        this.checkError = false;
                        this.idDeparment = index.departmentId;
                        break;
                    }
                    else {
                        this.checkError = true;
                        this.idDeparment = "";
                    }
                }
                this.emitter.emit("DeparmentData", { txtInput: this.tilieInput, idDepartment: this.idDeparment, validateDepartment: this.checkError });
            }

        },
        /**
        * sự kiện mở  đóng bằng keydown enter            
        * Author: TVTam
        * Last Edited: 1/08/2022  *    
        */
        enterCombobox() {
            this.showCombobox();
            this.departmentsSearch = this.departments
            this.departments.forEach(element => {
                if (element.departmentId == this.isCheck) {
                    this.tilieInput = element.departmentName;
                    this.idDeparment = element.departmentId;
                }
            });
            this.checkError = false;

        },

        /**
        * sự kiện mở  đóng bằng keydown down            
        * Author: TVTam
        * Last Edited: 1/08/2022  *    
        */
        downCombobox() {
            if (this.isCheck == "") {
                this.isCheck = this.departments[0].departmentId;
            }
            else {

                for (let index = 0; index < this.departments.length; index++) {
                    if (this.isCheck == this.departments[index].departmentId) {
                        if (index === this.departments.length - 1) {
                            this.isCheck = this.departments[0].departmentId;
                            break;
                        }
                        else {
                            this.isCheck = this.departments[index + 1].departmentId;
                            break;
                        }
                    }
                }
            }
        },
        /**
        * sự kiện mở  đóng bằng keydown up            
        * Author: TVTam
        * Last Edited: 1/08/2022  *    
        */
        upCombobox() {

            if (this.isCheck == "") {
                this.isCheck = this.departments[this.departments.length - 1].departmentId;
            }
            else {
                for (let index = 0; index < this.departments.length; index++) {
                    if (this.isCheck == this.departments[index].departmentId) {
                        if (index === 0) {
                            this.isCheck = this.departments[this.departments.length - 1].departmentId;
                            break;
                        }
                        else {
                            this.isCheck = this.departments[index - 1].departmentId;
                            break;
                        }
                    }
                }
            }
        }
    },
    mounted() {
        this.isCheck = this.valueStartInput
    },
    components: {},
    watch: {
        /**
        * tim kiếm trên combobox            
        * Author: TVTam
        * Last Edited: 1/08/2022     
        */
        tilieInput(txtSearch) {
            this.isCombobox = true;
            this.isCheckbtnup = true;
            let checkSearch = true;
            let a = []
            this.departments.forEach(element => {
                if (element.departmentName.toLowerCase().indexOf(txtSearch.toLowerCase()) !== -1) {
                    a.push(element);
                }
                if (element.departmentName === txtSearch) {
                    this.departmentsSearch = this.departments;
                    checkSearch = false;
                }
            });
            if (checkSearch) {
                if (a.length !== 0) {
                    this.departmentsSearch = a;
                }
                else {
                    this.departmentsSearch = this.departments
                }
            }
            else {
                this.isCombobox = false;
                this.isCheckbtnup = false;
            }
        }

    }
}
</script>

<style scoped>
@import url(../../style/css/base/combobox/comboboxDialog.css);

.input__eror {
    border: 1px solid red;
}

.e__table th:last-child,
td:last-child {
    outline: 0px dotted #c7c7c7;
}

.e__table th:first-child,
td:first-child {
    outline: 0px dotted #c7c7c7;
}
</style>