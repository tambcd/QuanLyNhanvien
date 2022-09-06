<template>
    <div class="dialog__model" @keyup.esc="closeDialog()" @keydown.ctrl.shift.s.prevent="addDataContinue()"  @keydown.ctrl.1.prevent="fisrtForcus()">
        <div class="dialog__content" @keydown.ctrl.s.prevent="addDataClose()">
            <div class="header__popup">
                <div class="ms__popup__title"> 
                    <div class="header__title flex">
                        <div class="title">Thông tin nhân viên</div>
                        <!-- list check box -->
                        <CheckboxDialog v-for="item in listCheckbox " :key="item.id" :idCheckbox="item.id"
                            :titleCheckbox="item.title" />
                    </div>
                </div>
                <div class="header__btn">
                    <div class="icon icon__24 btn__help"> </div>
                    <TooltipTable :isInline="true" tooltipContent="Đóng (ESC)" :show="true">
                        <div class="icon icon__24 btn__closepp" @click="closeDialog()"></div>
                    </TooltipTable>
                </div>
            </div>
            <div class="main__popup">
                <div class="content__popup">
                    <div class="header__content flex">
                        <div class="header__content__left">
                            <span class="mg__b__5">
                                <div class="flex row__input">
                                    <div class="row__input__left " ref="EmployeeCode">
                                        <InputDialog txtInput="Mã" typeInput="1" :inputError="checkError.id"
                                            :valueStartInput="employeeType.EmployeeCode"
                                            @DataInput="(dataInput, BaseValidate) => { this.newEmployee.employeeCode = dataInput; this.checkError.id = BaseValidate }" />

                                    </div>
                                    <div class="row__input__right" ref="FullName">

                                        <InputDialog txtInput="Tên" typeInput="1" :inputError="checkError.name"
                                            :valueStartInput="employeeType.FullName"
                                            @DataInput="(dataInput, BaseValidate) => { this.newEmployee.fullName = dataInput; this.checkError.name = BaseValidate }" />

                                    </div>
                                </div>
                            </span>
                            <span>
                                <div class="flex row__input">
                                    <div class="combobox__position" ref="department">
                                        <div class=" title__pp title__combobox">Đơn vị<span style="color: red;">
                                                *</span></div>
                                        <ComboboxDialog :valueStartInput="employeeType.departmentId" typeCombobox="1"
                                            :nameTooltip="typeValidateCombobox" urlRequest="Departments"
                                            :errorCombobox="checkError.DepartmentCode" />
                                    </div>
                                </div>
                            </span>

                            <span>
                                <div class="flex row__input">
                                    <div class="txt__depm">
                                        <InputDialog txtInput="Chức danh" typeInput="0"
                                            :valueStartInput="employeeType.employeeTitle"
                                            @DataInput="(dataInput, BaseValidate) => { this.newEmployee.employeeTitle = dataInput; this.checkError.DepartmentName = BaseValidate }" />

                                    </div>
                                </div>
                            </span>
                        </div>
                        <div class="header__content__right">
                            <span class="mg__b__5">
                                <div class="flex row__input">
                                    <div class="row__input__left">
                                        <div class="title__pp">Ngày sinh </div>
                                        <div class="flex input__date" ref="dob">
                                            <Datepicker v-model="newEmployee.dateOfBirth" maxlength="10"
                                                placeholder="DD/MM/YYYY" ref="calendar" class="date__Input"
                                                format="dd/MM/yyyy" textInput type='date' hideInputIcon
                                                :clearable="false" utc language="de" />
                                            <button class="icon icon__24 icon__date" @click="openDateInput()"
                                                tabindex="-1"></button>
                                        </div>
                                    </div>
                                    <div class="row__input__right" style="margin-left: 10px;">
                                        <div class="title__pp title__code">Giới tính </div>
                                        <div class=" input__pp input__name flex">

                                            <RadiobtnDialog v-for="(itemRadio, index) in  listRadiobtn"
                                                :key="itemRadio.id" :valueRadio="index" @click="checkGT(itemRadio.id)"
                                                :idRadio="itemRadio.id" :namerd="itemRadio.name"
                                                :isCheck="this.employeeType.Gender == itemRadio.id"
                                                :titleRadio="itemRadio.title" />
                                        </div>
                                    </div>
                                </div>
                            </span>
                            <span>
                                <div class="flex row__input">
                                    <div class="input__CCCD">
                                        <TheTooltip position="down" tooltipText="Số chứng minh nhân đân">
                                            <InputDialog txtInput="Số CMND" typeInput="0" ref="PersonalTaxCode"
                                                :valueStartInput="employeeType.PersonalTaxCode"
                                                @DataInput="(dataInput, BaseValidate) => { this.newEmployee.idenbtyNumber = dataInput; this.checkError.PersonalTaxCode = BaseValidate }" />
                                        </TheTooltip>
                                    </div>
                                    <div class="date__CCCD">
                                        <div class="title__pp">Ngày cấp </div>
                                        <div class="flex input__date" ref="date__CCCD">
                                            <Datepicker v-model="newEmployee.idenbtyDate" maxlength="10"
                                                placeholder="DD/MM/YYYY" class="date__Input" format="dd/MM/yyyy"
                                                textInput type='date' hideInputIcon :clearable="false" utc
                                                style="height: 32px;" language="de" />
                                            <button class="icon icon__24 icon__date" tabindex="-1"></button>
                                        </div>

                                    </div>
                                </div>
                            </span>
                            <span>
                                <div class="flex row__input">
                                    <div class="txt__depm">
                                        <InputDialog txtInput="Nơi cấp" typeInput="0"
                                            :valueStartInput="employeeType.AddressPersonalTaxCode"
                                            @DataInput="(dataInput, BaseValidate) => { this.newEmployee.idenbtyplace = dataInput; this.checkError.AddressPersonalTaxCode = BaseValidate }" />

                                    </div>
                                </div>
                            </span>
                        </div>

                    </div>
                    <div class="content__center">
                        <span>
                            <div class="flex row__input">
                                <div class="txt__depm">

                                    <InputDialog txtInput="Địa chỉ" typeInput="0"
                                        :valueStartInput="employeeType.Address"
                                        @DataInput="(dataInput, BaseValidate) => { this.newEmployee.address = dataInput; this.checkError.Address = BaseValidate }" />

                                </div>
                            </div>
                        </span>
                    </div>

                    <div class="content__bottom">
                        <div class="top flex">
                            <div class="input__Cellphone">
                                <TheTooltip position="down" tooltipText="Điện thoại di động">
                                    <InputDialog txtInput="DT di động" typeInput="0"
                                        :valueStartInput="employeeType.phoneNumber"
                                        @DataInput="(dataInput, BaseValidate) => { this.newEmployee.phoneNumber = dataInput; this.checkError.CellPhoneNumber = BaseValidate }" />
                                </TheTooltip>
                            </div>

                            <div class="input__phone">
                                <TheTooltip position="down" tooltipText="Điện thoại cố định">
                                    <InputDialog txtInput="DT cố định" typeInput="0"
                                        :valueStartInput="employeeType.CellPhoneNumber"
                                        @DataInput="(dataInput, BaseValidate) => { this.newEmployee.cellPhoneNumber = dataInput; this.checkError.phoneNumber = BaseValidate }" />
                                </TheTooltip>
                            </div>
                            <div class="input__Email" ref="Email">
                                <InputDialog txtInput="Email" typeInput="0" :valueStartInput="employeeType.Email"
                                    @DataInput="(dataInput, BaseValidate) => { this.newEmployee.email = dataInput; this.checkError.Email = BaseValidate }" />
                            </div>
                        </div>
                        <div class="bottom flex">
                            <div class="input__Numberbank">
                                <InputDialog txtInput="Tài khoản ngân hàng" typeInput="0"
                                    :valueStartInput="employeeType.NumberBank"
                                    @DataInput="(dataInput, BaseValidate) => { this.newEmployee.bankAccount = dataInput; this.checkError.Email = BaseValidate }" />

                            </div>
                            <div class="input__Namebank">
                                <InputDialog txtInput="Tên ngân hàng" typeInput="0"
                                    :valueStartInput="employeeType.NameBank"
                                    @DataInput="(dataInput, BaseValidate) => { this.newEmployee.bankName = dataInput; this.checkError.NameBank = BaseValidate }" />

                            </div>
                            <div class="input__addressbank">
                                <InputDialog txtInput="Chi nhánh" typeInput="0"
                                    :valueStartInput="employeeType.addressBank"
                                    @DataInput="(dataInput, BaseValidate) => { this.newEmployee.bankPlace = dataInput; this.checkError.addressBank = BaseValidate }" />
                            </div>
                        </div>
                    </div>


                </div>

            </div>
            <div style="padding:10px 12px 0px 32px; margin-right: 22px;">
                <hr />
            </div>
            <div class="button__popup">
                <div class="btn__save">
                    <TooltipTable :isInline="true" tooltipContent="Cất (Ctrl + S)" :show="true">
                        <TheButton btnName="Cất" class="btn__add" @click="addDataClose()" btnType="1" />
                    </TooltipTable>

                    <TooltipTable :isInline="true" tooltipContent="Cất và thêm (Ctrl + Shift + S)" :show="true">
                        <TheButton btnName="Cất và Thêm" class="contine" @click="addDataContinue()" btnType="2" />
                    </TooltipTable>
                </div>
                <div class="btn__huy">
                    <TheButton btnName="Hủy" @click="closeDialog()" btnType="1" />
                </div>

            </div>
        </div>
    </div>
</template>

<script>
import ComboboxDialog from '../Combobox/comboboxDialog.vue'
import CheckboxDialog from '../Checkbox/CheckboxDialog.vue'
import RadiobtnDialog from '../Radio/RadiobtnDialog.vue'
import { createToaster } from "@meforma/vue-toaster";
import { post, put } from "@/api/api.js";
import { Resource } from '@/Resource/Resource'
const toaster = createToaster({ position: 'top' });
import InputDialog from "../Input/InputDialog.vue"
import TheTooltip from '../tooltip/TheTooltip.vue';
import TheButton from '../Button/TheButton.vue';
export default {
    props: {
        /**đối tượng nhân viên chuyển xuống để thực hiện thêm sửa */
        employeeType: Object,
        /**trạng thái sửa hay thêm của form */
        typeDialog: String
    },
    data() {
        return {
            /**Vị trí forcus */
            isforcus: "",
            /**nội dung thông báo  */
            titleMessage: "",
            checkSave: 1,
            /**
             Khiểu nhãn của toolTip và thông báo vaildate combobox
             */
            typeValidateCombobox: Resource.VN_DepartmentEmpty,
            /**các trường validate */
            checkError: {
                id: false,
                name: false,
                DateOfBirth: false,
                Gender: false,
                Address: false,
                DepartmentCode: false,
                PersonalTaxCode: false,
                datePersonalTaxCode: false,
                AddressPersonalTaxCode: false,
                phoneNumber: false,
                cellPhoneNumber: false,
                Email: false,
                NumberBank: false,
                NameBank: false,
                addressBank: false,

            },

            /** đối tượng lưu data post */
            newEmployee: {

                employeeId: "",
                employeeCode: "",
                fullName: "",
                address: "",
                dateOfBirth: new Date(),
                gender: 0,
                DepartmentName: "",
                departmentId: "",
                idenbtyNumber: "",
                employeeTitle: "",
                idenbtyDate: new Date(),
                idenbtyplace: "",
                phoneNumber: "",
                cellPhoneNumber: "",
                email: "",
                bankAccount: "",
                bankName: "",
                bankPlace: "",

            },
            /** data khách hàng hoặc nhà cung cấp */
            listCheckbox: [
                {
                    id: 1,
                    title: "Là khách hàng"
                },
                {
                    id: 2,
                    title: "Là nhà cung cấp"
                },

            ],
            /**data giới tính */
            listRadiobtn: [
                {
                    id: 0,
                    title: "Nam",
                    name: "gr"
                },
                {
                    id: 1,
                    title: "Nữ",
                    name: "gr"
                },
                {
                    id: 2,
                    title: "Khác",
                    name: "gr"
                },
            ]
        };
    },

    methods: {
        /**
         * chọn và ô nhập liệu đầu
         * @created : tvtam 24/07/2022
         */
        fisrtForcus(){
            this.$refs["EmployeeCode"].querySelector("input").focus();
        },

        /**
        * chọn giới tính khi chọn radio button 
        * @param : giá trị của radio
        * Author: TVTam
        * Last Edited: 24/07/2022    
        */
        checkGT(id) {
            this.newEmployee.Gender = id
        },

        /**
       * Mở message trước khi đóng tắt form nhân viên
       * Author: TVTam
       * Last Edited: 24/07/2022    
       */
        closeDialog() {
            this.$emit("CloseDialog", false);
        },



        /**
         * Thêm mới nhân viên
         * @param : thông tin nhân viên được thêm mới
         * Last Edited: 24/07/2022    
         * Author: TVTam
         */

        async addNewData() {
            var me = this;
            await post(`Employees`,
                this.newEmployee
                , () => {
                    // Trường hợp thành công toast thồn báo 
                    toaster.success(Resource.VN_AddSuccess),
                        setTimeout(toaster.clear, 3000);
                    // đóng form loading
                    me.$emit("LoadingClose");
                    // cất và thêm
                    if (this.checkSave === 1) {
                        me.$emit("OpenDialogContinue");
                    }
                    // cất
                    else {
                        me.$emit("ContinueAdd");
                    }

                }, (error) => {
                    // Trường hợp thất bại thì hiển thị toastMessage lỗi và ghi rõ lỗi xảy ra.
                    toaster.error(`${error.response.data.devMsg}: ${error.response.data.erros}`);
                    this.checkError.id = true;
                    this.fisrtForcus()

                    // đóng loading
                    me.$emit("LoadingClose");


                })


        },
        /** Validate  */
        validateEmail(email) {
            var re = /\S+@\S+\.\S+/;
            return re.test(email);
        },

        /**
        * mở carender chọn ngày tháng năm 
        * Author: TVTam
        * Last Edited: 25/07/2022    
        */
        openDateInput() {
            this.$refs['calendar'].openMenu();
        },

        /**
       * sửa nhân viên
       * @param : nhân viên cần sửa
       * Author: TVTam
       * Last Edited: 24/07/2022    
       */
        async updateData() {
            
            var me = this;
            this.newEmployee.dateOfBirth = this.validateHours(this.newEmployee.dateOfBirth)
            this.newEmployee.datePersonalTaxCode = this.validateHours(this.newEmployee.datePersonalTaxCode)

            await put(`Employees`, this.newEmployee
                , () => {
                    // Trường hợp thành công nhận về dữ liệu thì toast thông báo 
                    toaster.success(Resource.VN_UpdateSuccess),
                        setTimeout(toaster.clear, 3000);
                    // loading dữ liệu và đóng
                    me.$emit("LoadingClose");
                    //cất
                    if (this.checkSave === 1) {
                        me.$emit("OpenDialogContinue");
                    }
                    //cất và thêm
                    else {
                        me.$emit("ContinueAdd");
                    }

                }, (error) => {
                    // Trường hợp thất bại thì hiển thị toastMessage lỗi và ghi rõ lỗi xảy ra.
                    toaster.error(`${error.response.data.devMsg}: ${error.response.data.erros}`);
                    this.fisrtForcus()
                    // đóng form loading
                    me.$emit("LoadingClose");


                })



        },
        /** Validate ngày không được lớn hơn ngày hiện tại 
         * @param : giá trị date
         * Last Edited: 24/07/2022            
        */
        validateDay(dateData) {
            if (Number(new Date(dateData) - new Date()) > 0) {
                return true;
            }
            return false;
        },
        /**Cập nhập giờ */
        validateHours(dateData) {
            let value = new Date(dateData);
            value.setHours(23, 59, 59, 59);
            return value
        },
        /**Thực hiên validate đầu vào
         * Created: txTam 24/07/2022  
         * 
         */
        validateData() {
            if (this.newEmployee.employeeCode === "") {
                this.isforcus = "EmployeeCode"
                // this.titleMessage = "Mã không được để trống."
                this.titleMessage = this.Resource.VN_EmployeeCodeEmpty;
                this.checkError.id = true;
                if (this.newEmployee.fullName === "") {
                    this.checkError.name = true;
                }
                if (this.newEmployee.departmentId === "") {
                    this.checkError.DepartmentCode = true;
                }

            }
            // tên trống
            else if (this.newEmployee.fullName === "") {
                this.isforcus = "FullName"
                this.titleMessage = Resource.VN_FullNameValidate
                this.checkError.name = true;
                if (this.newEmployee.departmentId === "") {
                    this.checkError.DepartmentCode = true
                }
            }
            // đơn vị trống
            else if (this.newEmployee.departmentId === "") {
                this.isforcus = "department"
                this.titleMessage = this.typeValidateCombobox
                this.checkError.DepartmentCode = true
            }
            else if (this.checkError.DateOfBirth) {
                this.titleMessage = "Ngày sinh " + Resource.VN_DateValidate;
                this.checkError.DateOfBirth = true
                this.isforcus = "dob"
            }
            else if (this.checkError.datePersonalTaxCode) {
                this.titleMessage = "Ngày cấp " + Resource.VN_DateValidate
                this.checkError.datePersonalTaxCode = true
                this.isforcus = "date__CCCD"
            }
            else if (!this.validateEmail(this.newEmployee.email)) {
                this.titleMessage = Resource.VN_EmailValidate
                this.checkError.Email = true
                this.isforcus = "Email"
            }


        },
        /**thực hiện đẩy thêm sửa */
        postData() {
            var me = this;
            me.$emit("LoadingOpen");
            try {
                // trong trường hợp thêm
                if (me.typeDialog === "1") {
                    me.addNewData();
                }
                // trong trường hợp update
                else {
                    me.updateData();
                }

            } catch (error) {
                //toast message lỗi và đóng loading data
                toaster.error(`${error.response.data.devMsg}: ${error.response.data.erros}`);
                console.log(error);
                me.$emit("LoadingClose");

            }
        },
        /**
        * cất/sửa nhân viên
        * Author: TVTam
        * Last Edited: 24/07/2022    
        */
        addData() {
            this.checkError.DateOfBirth = this.validateDay(this.newEmployee.dateOfBirth);
            this.checkError.datePersonalTaxCode = this.validateDay(this.newEmployee.idenbtyDate);

            if (this.newEmployee.email !== "") {

                if (this.newEmployee.employeeCode !=="" && this.newEmployee.fullName !=="" && this.newEmployee.departmentId !== "" && this.validateEmail(this.newEmployee.email) && !this.checkError.DateOfBirth && !this.checkError.datePersonalTaxCode) {
                    this.postData()

                }
                // nếu cất ko thành công thực hiện hiển thị message cảnh báo dữ liệu trống
                else {
                    // validate
                    this.validateData()
                    // mở hộp thông báo lỗi

                    this.emitter.emit("ErrorMessageSave", this.titleMessage);

                }
            } 
            else {
                if (this.newEmployee.employeeCode !== "" && this.newEmployee.fullName !== "" && this.newEmployee.departmentId !== "" && !this.checkError.DateOfBirth && !this.checkError.datePersonalTaxCode) {
                    this.postData()

                }
                // nếu cất ko thành công thực hiện hiển thị message cảnh báo dữ liệu trống
                else {
                    this.validateData();
                    // mở hộp thông báo lỗi
                    this.emitter.emit("ErrorMessageSave", this.titleMessage);
                }
            }
        },
        /**
         * cất và đóng form nhập liệu       
         * Author: TVTam
         * Last Edited: 25/07/2022  
         */
        addDataClose() {
            this.checkSave = 1;
            this.addData();
        },
        /**
        * cất và làm mới form nhập liệu       
        * Author: TVTam
        * Last Edited: 25/07/2022  
        */
        addDataContinue() {
            this.checkSave = 2;
            this.addData();
        },


    },
    components: { ComboboxDialog, CheckboxDialog, RadiobtnDialog, InputDialog, TheTooltip, TheButton }
    ,
    mounted() {
        /**
        * cập nhập ngày sinh ,ngày cấp CMND ,giới tính vào form
        * Author: TVTam
        * Last Edited: 25/07/2022           
        */
        this.newEmployee.employeeId = this.employeeType.EmployeeId;
        this.newEmployee.dateOfBirth = this.employeeType.DateOfBirth;
        this.newEmployee.idenbtyDate = this.employeeType.datePersonalTaxCode;
        this.newEmployee.gender = this.employeeType.Gender;

        /**
        * thực hiên forcus ô nhập liệu đầu tiên               
        * Author: TVTam
        * Last Edited: 25/07/2022  
        */
        this.fisrtForcus()

        /**
        * thực hiên forcus ô nhập liệu lỗi đầu tiên               
        * Author: TVTam
        * Last Edited: 25/07/2022  
        */
        this.emitter.on('foucusInputError', () => {
            if (this.isforcus !== "") {
                this.$refs[this.isforcus].querySelector("input").focus();
                this.isforcus = ""
            }
        });

        /**
        * lấy dữ liệu combobox Deparment và check validate             
        * Author: TVTam
        * Last Edited: 25/07/2022  
        */
        this.emitter.on('DeparmentData', (data) => {
            this.newEmployee.departmentId = data.idDepartment;
            this.checkError.DepartmentCode = data.validateDepartment;
            if (data.validateDepartment) {
                if (data.txtInput.trim() === "") {

                    this.typeValidateCombobox = Resource.VN_DepartmentEmpty;
                }
                else {
                    this.typeValidateCombobox = Resource.VN_DepartmentValidate;
                }

            }



        });
        /**
        * lấy tín hiệu bên EmployeeMain thực hiện cất bên TheMessage Gửi           
        * Author: TVTam
        * Last Edited: 25/07/2022  
        */
        this.emitter.on("BtnSaveMessage", () => {
            this.addDataClose()
        })
    },

}
</script>

<style scoped>
@import url(../../style/css/base/dialog/dialog.css);

.icon__date {
    background-position: -128px -305px;
    background-color: transparent;
    border: 0px solid transparent;
    cursor: pointer;
    position: absolute;
    right: 0;
}

.input__date {

    position: relative;
}

.dp__input {
    height: 32px;
}
</style>

