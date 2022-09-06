<template>
    <div>
        <div class="router__view fix__view">
            <div class="router__content">
                <div class="content__area__content">
                    <div>
                        <div class="layout__dictionary">
                            <div class="title__distance header__list">
                                <div class="header__di">
                                    <div class="title__di"> Nhân viên</div>
                                    <div class="header__button flex">
                                        <div class="add__button ms__dropdown">
                                            <button class="btn btn__add" @click="openDialog()">Thêm mới nhân
                                                viên</button>
                                        </div>
                                    </div>

                                </div>
                            </div>

                            <div id="layout__dictionary__body" class="body">
                                <div class="grid__list__data">
                                    <div class="grid__button__list flex headerEmp">

                                        <div class="flex__grow ">
                                            <div class="perform__many flex" @click="openDeleteMany">
                                                Thực hiện hàng loạt
                                                <button class="perform__many__btn icon icon__24"></button>
                                                <div class="perform__many__btns"
                                                    v-if="listIdEmployeeDelete.length !== 0 && isShowDelete">
                                                    <button class="delete__many" @click="deleteMultiple()">Xóa</button>
                                                    <button class="unchecked" @click="uncheckedItem()">Bỏ chọn</button>

                                                </div>
                                            </div>


                                        </div>

                                        <div class="Search__main flex">
                                            <InputContent />
                                        </div>
                                        <div class="ultility__buttons flex">
                                            <TooltipTable :isInline="true" tooltipContent="Lấy lại dữ liệu"
                                                :show="true">
                                                <div class="refresh__button list__button" @click="reloadData()">
                                                    <div class="icon icon__24 mi__refresh"></div>
                                                </div>
                                            </TooltipTable>

                                            <TooltipTable :isInline="true" tooltipContent="Xuất ra Excel" :show="true">
                                                <div class="icon icon__24 mi_excel" @click="openSelectFolderExport()">
                                                </div>
                                            </TooltipTable>

                                        </div>
                                    </div>
                                    <div class="grid flex bg__white he__100">
                                        <div class="grid__model__control">
                                            <div class="ms__grid__viewer table__scroll he__100">
                                                <!-- data__table -->
                                                <TableData :listData="listEmployee" />
                                                <!-- footer__table -->
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <!--   phân trang    -->

                                <div class="page flex space__between">
                                    <div class="number__page">
                                        Tổng số: <span style="font-family: MISA fonts Bold ;"> {{ allNumber + " " }}
                                        </span> bản ghi
                                    </div>
                                    <div class="filter flex space__between">

                                        <!-- lọc theo số bản ghi 1 trang -->
                                        <ComboboxPaging :listOption="listNumberPage"
                                            v-on:changePage="updatePage($event)" />

                                        <div class="page__ft flex space__between">

                                            <!--                                                                                                       
                                            Description: xử lý phần trang.
                                            Author: TVTam 
                                            Last Edited:  16/07/2022 
                                            -->
                                            <!-- Giới hạn điểm đầu và chuyển trang trước đó-->
                                            <button class=" page__btn btn__back" @click="backPage()"
                                                :disabled="chagePage === 1">Trước</button>
                                            <button class="page__btn" v-for="(item, index) in paging" :key="index"
                                                :class="{ pageSelect: index + 1 === pageSelect }"
                                                @click="selectNumberPage(index + 1, item)">{{ item }}</button>

                                            <!-- Giới hạn điểm cuối và chuyển các trang tiếp -->
                                            <button class="page__btn btn__next" @click="nextPage()"
                                                :disabled="chagePage > allNumber / numberPage - 4">Sau</button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                </div>

            </div>

        </div>
        <DialogEmployee v-if="showDialog" @CloseDialog="closeDialog(event)" @LoadingOpen="loadingOpen()"
            @LoadingClose="loadingClose()" @OpenDialogContinue="openDialogContinue()" :employeeType="employeeUpDate"
            @OpenDialogContinueUpdate="openDialogContinueUpdate()" :typeDialog="typeDialog"
            @ContinueAdd="continueAdd" />
        <TheLoading v-show="loading" />
        <TheMesage :typeMessage="typeMessage" :titleMessage="titleMessage" v-if="showMessage"
            :idEmployyee="idEmployeeDelete" @LoadingDataDelete="loadingDataDelete()" @ClosethisMg="closethisMg()"
            :listIdEmployyee="listIdEmployeeDelete" @ClosethiDl="closethiDl"
            @CloseThisDialogAndSave="closeThisDialogAndSave()" />
    </div>
</template>

<script>
import TableData from '@/components/Table/TableData.vue';
import ComboboxPaging from '@/components/Combobox/ComboboxPaging.vue';
import DialogEmployee from '@/components/Dialog/DialogEmployee.vue';
import InputContent from '@/components/Input/InputContent.vue';
import { getByFilter, get, getNewEmployeeCode } from "@/api/api.js";
import { createToaster } from "@meforma/vue-toaster";
const toaster = createToaster({ position: 'top' });


export default {

    components: {
        TableData,
        ComboboxPaging,
        DialogEmployee,
        InputContent,

    },
    data() {
        return {
            /** từ khóa tìm kiếm */
            txtSearch: "",
            /** danh sách nhân viên */
            listEmployee: [],
            /** kiểu form nhập liệu 1 : thêm mới  2 sửa  */
            typeDialog: "1",
            /** đóng mở form nhập liệu*/
            showDialog: false,
            /** đóng mở hộp thông báo*/
            showMessage: false,
            /**kiểu thông báo */
            typeMessage: 1,
            /** Nội dung thông báo */
            titleMessage: "",
            /**số trang */
            chagePage: 1,
            /**số bản ghi trên trang */
            numberPage: 10,
            /** tổng số trang */
            allPage: 1,
            /**đóng mở loading */
            loading: false,
            /**Tổng số bản ghi */
            allNumber: 0,
            allPageSize: 0,
            /** trang hiện tại được chọn */
            pageSelect: 1,
            /**Mảng phân trang */
            paging: [],
            /** id nhân viên */
            idEmployeeDelete: "",
            /** dtext combobox phân trang mặc định*/
            txtInputCobobox: "10 bản ghi trên 1 trang",
            /** danh sách tiêu chí số bản ghi trên trang */
            listNumberPage: [
                {
                    id: 1,
                    title: "10 bản ghi trên 1 trang"
                },
                {
                    id: 2,
                    title: "20 bản ghi trên 1 trang"
                },
                {
                    id: 3,
                    title: "30 bản ghi trên 1 trang"
                },
                {
                    id: 5,
                    title: "50 bản ghi trên 1 trang"
                },
                {
                    id: 10,
                    title: "100 bản ghi trên 1 trang"
                }
            ],
            /** Đối tượng nhân viến để thực hiện thêm / sửa  */
            employeeUpDate: {
                EmployeeId: "",
                EmployeeCode: "",
                FullName: "",
                Address: "",
                DateOfBirth: null,
                Gender: 0,
                employeeTitle: "",
                DepartmentName: "",
                departmentId: "",
                PersonalTaxCode: "",
                datePersonalTaxCode: null,
                AddressPersonalTaxCode: "",
                phoneNumber: "",
                CellPhoneNumber: "",
                Email: "",
                NumberBank: "",
                NameBank: "",
                addressBank: "",

            },
            /** Danh sách Id nhân viên */
            listIdEmployeeDelete: [],
            /**mở xóa thực hiện hàng loạt */
            isShowDelete: false,
        }
    },

    async created() {

        this.firstLoadPage(10, "");
    },

    methods: {

        /**
        * lấy dữ liệu và thực hiện phân trang ban đầu               
        * Author: TVTam
        * Last Edited: 27/07/2022  *  
         */
        async firstLoadPage(pageSize, txtSearch) {
            await this.loadDatabase(pageSize, 1, txtSearch)
            //kiểu tra số trang để tùy chỉnh hiển thị
            // nhỏ hơn 7 trang thì hiển thị tất cả
            if (this.allPageSize <= 7) {
                this.paging[this.allPageSize - 1] = this.allPageSize;

                for (let index = 0; index < this.paging.length; index++) {
                    this.paging[index] = (index + 1).toString();
                }
                this.paging = this.paging.slice(0, this.allPageSize)

            }
            // lớn hơn thì cập nhận mảng phân trang 
            else {
                //khởi tạo mảng page ban đầu
                this.paging = ["1", "2", "3", "4", "5", "...", this.allPageSize.toString()]
            }
        },
        /**
         * làm mới form nhập liệu cất và thêm             
        * Author: TVTam
        * Last Edited: 26/07/2022 
         */
        continueAdd() {
            this.showDialog = false;
            this.openDialog();
        },

        /**
         * khởi tạo đối tượng rỗng để thêm mới             
        * Author: TVTam
        * Last Edited: 23/07/2022 
        */

        async addNewEmployee() {
            await getNewEmployeeCode('Employees/NewEmployeeCode'
                , (response) => {
                    // Trường hợp thành công gán giá trị cho     EmployeeCode             
                    this.employeeUpDate.EmployeeCode = response.data

                }, () => {
                    // Trường hợp thất bại thì hiển thị toastMessage lỗi và và gán lại EmployeeCode = ""
                    toaster.error(` mã tự động bị lỗi`);
                    this.employeeUpDate.EmployeeCode = "";

                })
            // khởi tạo thuộc tính rông truyền xuống dialog add
            this.employeeUpDate.EmployeeId = "13ede5b6-19dd-563e-1980-de64d090a2c2";
            this.employeeUpDate.FullName = "";
            this.employeeUpDate.Address = "";
            this.employeeUpDate.employeeTitle = "";
            this.employeeUpDate.DateOfBirth = new Date();
            this.employeeUpDate.Gender = 0;
            this.employeeUpDate.DepartmentName = "";
            this.employeeUpDate.departmentId = "";
            this.employeeUpDate.PersonalTaxCode = "";
            this.employeeUpDate.phoneNumber = "";
            this.employeeUpDate.CellPhoneNumber = "";
            this.employeeUpDate.addressBank = "";
            this.employeeUpDate.NameBank = "";
            this.employeeUpDate.NumberBank = "";
            this.employeeUpDate.Email = null;
            this.employeeUpDate.datePersonalTaxCode = new Date();
            this.employeeUpDate.AddressPersonalTaxCode = "";
            this.showDialog = true;
        },

        // mở loading
        loadingOpen() {
            this.loading = true;
        },
        // đóng loading
        loadingClose() {
            this.loading = false;
        },

        /**
        * Prams: none
        * Description: Load data khi chuyen trang.
        * Author: TVTam
        * Last Edited: 19/07/2022
        */
        async loadDatabase(pageSize, pageNumber, employeeFilter) {
            this.loadingOpen();
            await getByFilter('Employees/Filter',
                {
                    txtSearch: employeeFilter,
                    pageNumber: pageNumber,
                    pageSize: pageSize,
                }
                , (response) => {
                    // Trường hợp thành công nhận về dữ liệu thì gán lại các thông tin vào bảng
                    if (response.request.status == 200) {
                        this.listEmployee = response.data.data;
                        this.allNumber = response.data.totalRecord;
                        this.allPageSize = response.data.totalPage;
                    }
                    else {
                        toaster.success(`Không có bản ghi `);
                        setTimeout(toaster.clear, 3000);

                    }
                    this.loadingClose();

                }, () => {
                    // Trường hợp thất bại thì hiển thị toastMessage lỗi 
                    toaster.error(`lấy dự liệu lỗi`);
                    this.loadingClose();

                })


        },
        /**
         * chuyển trang khi nhấn trang sau              
         * Author: TVTam
         * Last Edited: 27/07/2022  *  
         */

        async nextPage() {
            this.chagePage++;
            await this.loadDatabase(this.numberPage, this.chagePage, this.txtSearch)

        },

        /**
         * chuyển trang khi nhấn trang trước              
         * Author: TVTam
         * Last Edited: 27/07/2022  *  
         */

        async backPage() {
            this.chagePage--;
            await this.loadDatabase(this.numberPage, this.chagePage, this.txtSearch)
        },

        /**
         * Reload data               
         * Author: TVTam
         * Last Edited: 17/07/2022  *  
         */


        reloadData() {
            this.pageSelect = 1;
            this.updatePage(this.numberPage / 10)
        },
        /**
         * Description: Kiểm tra size trang và hiểu thị lên data table
         * Author: TVTam
         * Last Edited:  17/07/2022     
         */
        async updatePage(upateNumber) {
            this.chagePage = 1;
            this.numberPage = upateNumber * 10;
            await this.loadDatabase(this.numberPage, this.chagePage, this.txtSearch);
            if (this.allPageSize <= 7) {
                this.paging[this.allPageSize - 1] = this.allPageSize;

                for (let index = 0; index < this.paging.length; index++) {
                    this.paging[index] = (index + 1).toString();
                }
                this.paging = this.paging.slice(0, this.allPageSize)

            }
            else {

                this.paging = ["1", "2", "3", "4", "5", "...", this.allPageSize.toString()]
            }

        },

        /**
         * Description: Xử lý ở list button số trang
         * Author: TVTam
         * Last Edited: 20/07/2022    
         */
        async selectNumberPage(Numberp, item) {
            this.chagePage = parseInt(item);
            this.pageSelect = Numberp
            // trường hợp khi nhấn vào các số truyển trái phải khi nhấn vào các số

            if (item !== "...") {

                await this.loadDatabase(this.numberPage, this.chagePage, this.txtSearch);
                if (this.allPageSize >= 8) {
                    // chuyển page trái nhấn số bên phải dịch trái 3
                    if (this.pageSelect === 3) {
                        if (parseInt(this.paging[2]) - 5 < 0) {
                            this.paging = ["1", "2", "3", "4", "5", "...", (this.allPageSize).toString()];

                        }
                        //check điểm đầu 
                        else {
                            for (let index = 2; index < 5; index++) {

                                this.paging[index] = (parseInt(this.paging[index] - 2)).toString();

                            }
                            this.paging[this.paging.length - 2] = "..."
                            this.pageSelect = this.pageSelect + 2;

                        }
                    }
                    // chuyển page phải dịch phải 3
                    else if (this.pageSelect === 5) {
                        if (this.paging.length === 7) {

                            if (this.allPageSize - parseInt(this.paging[3]) < 5) {
                                this.paging = ["1", "...", (this.allPageSize - 4).toString(), (this.allPageSize - 3).toString(), (this.allPageSize - 2).toString(), (this.allPageSize - 1).toString(), (this.allPageSize).toString()];

                            }
                            // check điểm cuối
                            else {
                                for (let index = 2; index < 5; index++) {

                                    this.paging[index] = (parseInt(this.paging[index]) + 2).toString();

                                }
                                this.paging[1] = "..."
                                this.pageSelect = this.pageSelect - 2;

                            }
                        }

                    }
                }
            }
            // xử lý sự kiện khi nhấn vào "..." chuyển các page trái phải
            else {
                // chuyển phải
                if (this.pageSelect === this.paging.length - 1) {
                    if (this.paging.length < 7) {
                        if (this.allPageSize <= 8) {
                            this.paging = ["1", "...", "4", "5", "6", "7", "8"];
                        }
                        else {
                            this.paging = ["1", "...", "5", "6", "7", "...", this.allPageSize.toString()];

                        }

                    }
                    else {
                        this.paging[1] = "..."
                        if (this.allPageSize - parseInt(this.paging[4]) <= 3) {

                            this.paging = [1, "...", (this.allPageSize - 4).toString(), (this.allPageSize - 3).toString(), (this.allPageSize - 2).toString(), (this.allPageSize - 1).toString(), (this.allPageSize).toString()]

                        }
                        else {
                            for (let index = 2; index < 5; index++) {
                                let con = 0;
                                this.paging[index] = (parseInt(this.paging[index]) + 3 - con).toString()
                                con++;

                            }
                        }

                    }


                }

                //chuyển trái
                else {
                    if (parseInt(this.paging[2]) <= 6) {

                        this.paging = ["1", "2", "3", "4", "5", "...", (this.allPageSize).toString()];

                    }
                    //check điểm cuối hiển thị đầy đủ
                    else {
                        this.paging[5] = "..."

                        for (let index = 2; index < 5; index++) {
                            let con = 0;
                            this.paging[index] = (parseInt(this.paging[index]) - 3 + con).toString()
                            con++;

                        }

                    }


                }

            }

        },


        /**
         * Description: chuyển đổi trang thái update và add
         * Author: TVTam
         * Last Edited: 24/07/2022         
         */


        openDialog() {
            this.typeDialog = "1"
            this.addNewEmployee();


        },

        /**
         * Load data khi thêm mới              
         * Author: TVTam
         * Last Edited: 27/07/2022  *  
         */

        openDialogContinue() {
            this.showDialog = false;
            this.firstLoadPage(this.numberPage, this.txtSearch)

        },
        /**
         * loadData khi update               
         * Author: TVTam
         * Last Edited: 27/07/2022  *  
         */

        async openDialogContinueUpdate() {
            this.showDialog = false;
            await this.loadDatabase(this.numberPage, this.chagePage, this.txtSearch)
        },

        /**
         * Mở hộp thông báo khi nhấn tắt form nhập liệu              
         * Author: TVTam
         * Last Edited: 27/07/2022  *  
         */
        closeDialog() {
            this.typeMessage = 3,
                this.titleMessage = "Dữ liệu đã bị thay đổi. Bạn có muốn cất không?"
            this.showMessage = true
        },
        /**
         * Đóng thông báo             
         * Author: TVTam
         * Last Edited: 27/07/2022   
         */
        closethisMg() {
            this.showMessage = false

        },
        /** Đóng mở thực hiện hàng loạt */
        openDeleteMany() {
            this.isShowDelete = !this.isShowDelete;
        },
        /**
         * đóng thông báo và form nhập liệu              
         * Author: TVTam
         * Last Edited: 27/07/2022    
         */

        closethiDl() {
            this.closethisMg()
            this.showDialog = false;
        },
        /**
         * load data khi xoa              
         * Author: TVTam
         * Last Edited: 27/07/2022  *  
         */
        async loadingDataDelete() {
            this.idEmployeeDelete = ""
            this.closethisMg()
            await this.firstLoadPage(this.numberPage, this.txtSearch)
        },

        /**
         * gửi thông điệp cất sang dialogSave              
         * Author: TVTam
         * Last Edited: 27/07/2022  *  
         */

        closeThisDialogAndSave() {
            this.closethisMg()
            this.emitter.emit('BtnSaveMessage')
        },



        /**
         * hiển thị thông báo               
         * Author: TVTam
         * Last Edited: 27/07/2022  *  
         */
        errorMessageSave(dataTitle) {
            this.typeMessage = 1;
            this.titleMessage = dataTitle;
            this.showMessage = true

        },

        /**
         * data update truyền data từ table lên form thực hiện update               
         * Author: TVTam
         * Last Edited: 27/07/2022  *  
         */

        dataUpdateEmployee(data) {
            this.employeeUpDate.EmployeeId = data.employeeId;
            this.employeeUpDate.EmployeeCode = data.employeeCode;
            this.employeeUpDate.FullName = data.fullName;
            this.employeeUpDate.Address = data.address;
            this.employeeUpDate.Gender = data.gender;
            this.employeeUpDate.DateOfBirth = new Date(data.dateOfBirth);
            this.employeeUpDate.DepartmentName = data.departmentName;
            this.employeeUpDate.departmentId = data.departmentId;
            this.employeeUpDate.PersonalTaxCode = data.idenbtyNumber;
            this.employeeUpDate.phoneNumber = data.phoneNumber;
            this.employeeUpDate.Email = data.email;
            this.employeeUpDate.employeeTitle = data.employeeTitle
            this.employeeUpDate.PositionId = data.positionId;
            this.employeeUpDate.datePersonalTaxCode = data.idenbtyDate;
            this.employeeUpDate.AddressPersonalTaxCode = data.idenbtyplace;
            this.employeeUpDate.CellPhoneNumber = data.cellPhoneNumber;
            this.employeeUpDate.NumberBank = data.bankAccount;
            this.employeeUpDate.NameBank = data.bankName;
            this.employeeUpDate.addressBank = data.bankPlace;
        },

        /**
         * Description: xóa mảng chứa các item chọn
         * Author: TVTam
         * Last Edited: 20/07/2022
         */

        uncheckedItem() {
            this.emitter.emit("uncheckItem");
            this.isShowDelete = false;

        }
        ,
        /**
         * thực hiện xóa update title message và bật message để thực hiện xóa             
         * Author: TVTam
         * Last Edited: 27/07/2022    
         */

        deleteMultiple() {
            this.typeMessage = 2;
            this.titleMessage = `Bạn có muốn xóa danh sách ${this.listIdEmployeeDelete.length} nhân viên không ?`;
            this.showMessage = true;
            this.isShowDelete = false;
        },

        /**
         * Description: chọn thư mục export
         * Author: TVTam
         * Last Edited: 13/08/2022
         */

        async openSelectFolderExport() {
            await get('Employees/Export'
                , () => {

                    window.open('http://localhost:5074/api/v1/Employees/Export')
                    toaster.success(`Xuất khẩu thành công`);

                }, () => {
                    // Trường hợp thất bại thì hiển thị toastMessage lỗi và ghi rõ lỗi xảy ra.
                    toaster.error(`Có lỗi mục xuất khẩu`);
                })
        },
    },
    // hiển thị thông báo khi dữ liệu chưa chuẩn validate
    mounted() {
        this.emitter.on("deleteSuccefull", () => {
            this.uncheckedItem()
        })


        // list chon mhieu 
        this.emitter.on('ListSelectDelete', data => {
            this.listIdEmployeeDelete = data;


        })
        // erroo message

        this.emitter.on("TxtChange", (dataTxt) => {
            this.txtSearch = '';
            this.pageSelect = 1;
            this.txtSearch = dataTxt;
            this.updatePage(this.numberPage / 10)

        }),

            // kiểu tra kiểu message và hiển thị
            this.emitter.on("ErrorMessageSave", (data) => {
                this.errorMessageSave(data)
            }),

            // update Employee
            this.emitter.on("openDialogUpDate", (data) => {
                this.typeDialog = "2"
                this.dataUpdateEmployee(data)
                this.showDialog = true;

            }),

            // hien thi thong bao xoa
            this.emitter.on("DeleteMessage", (data) => {
                this.typeMessage = 2;
                this.titleMessage = `Bạn có muốn xóa nhân viên có mã <${data.employeeCode}>không ?`;
                this.showMessage = true;
                this.idEmployeeDelete = data.employeeId

            })
        /** thông tin nhân viên nhân bản xử lý gửi lên form nhập liêu */
        this.emitter.on("cloneEmployee", async (data) => {
            this.dataUpdateEmployee(data);
            await getNewEmployeeCode('Employees/NewEmployeeCode'
                , (response) => {
                    // Trường hợp thành công gán giá trị cho               
                    this.employeeUpDate.EmployeeCode = response.data

                }, () => {
                    // Trường hợp thất bại thì hiển thị toastMessage lỗi và và gán lại EmployeeCode = ""
                    toaster.error(` mã tự động bị lỗi`);
                    this.employeeUpDate.EmployeeCode = "";
                })
            this.typeDialog = "1"
            this.showDialog = true;

        })

    },
    watch: {
        /**
         * update trang về 1 khi chuyển pagesize          
         * Author: TVTam
         * Last Edited: 27/07/2022  *  
         */

        allPageSize() {
            this.pageSelect = 1;

        },
        /**
         * kiểm tra sự thay đổi của danh sách nhân viên             
        * Author: TVTam
        * Last Edited: 27/07/2022  *  
         */
        listEmployee() {
            this.emitter.emit("changeDataEmploy", this.listEmployee)
        },
        'listIdEmployeeDelete.length'(valueNext) {
            if (valueNext === 0) {
                this.isShowDelete = false;
            }
        }
    },
}
</script >

<style scoped>
.Search__main {
    align-items: center;
}

.headerEmp {
    height: 40px;
    align-content: center;
}

.he__100 {
    height: 100%;
}

.grid__list__data {
    height: calc(100% - 110px);
}

.page {
    height: 60px;
    margin: 40px 50px 0px 0px;
}

.page__btn {
    cursor: pointer;
    width: 30px;
}

.btn__next,
.btn__back {
    width: auto;
}

.page__btn:hover {
    background-color: #bbbbbb;
}

.pageSelect {
    border: 1px solid #bbbbbb;
    background-color: #e5f3ff;
}

.selectMutil {
    align-content: center;

}

.selectMutil div {
    height: 24px;
    margin: 10px 5px;

}

.selectMutil .btn__deleteall {

    color: #fff;
    padding: 0px 16px;
    background-color: red;
    border-radius: 4px;
    margin: 5px 10px 0px 10px;
    cursor: pointer;
    border: 1px solid transparent;
    height: 30px;
    width: auto;
}

.selectMutil .btn__deleteall:hover {
    background-color: rgba(255, 0, 0, 0.523);
}

.selectMutil .btn__backSelect {
    color: #fff;
    height: 30px;
    width: auto;
    margin: 5px 10px 0px 10px;
    cursor: pointer;
    border: 1px solid transparent;
    background-color: #016190;
    padding: 0px 16px;
}

.selectMutil .NumberTitile {
    font-size: 13px;
}

.mi_excel {
    background-position: -705px -200px;
}

.perform__many {
    width: 190px;
    border: solid black 2px;
    border-radius: 40px;
    background-color: inherit;
    color: black;
    text-align: center;
    font-weight: 600;
    height: 36px;
    justify-content: center;
    align-items: center;
    margin: 5px 0 0 18px;
    cursor: pointer;
    position: relative;

}

.perform__many:hover {
    background-color: #d2d3d6;
}

.perform__many .perform__many__btn {
    background-position: -551px -357px;
    background-color: transparent;
    border: 1px solid transparent;
    padding-left: 5px;
}

.perform__many .perform__many__btns {
    left: 283px;
    top: 148px;
    position: fixed;
    width: 100px;
    border: 1px solid;
    z-index: 10;
}

.perform__many .perform__many__btns button {
    width: 100%;
    height: 32px;
    background-color: #fff;
    border: 1px solid transparent;
    cursor: pointer;
}

.perform__many .perform__many__btns button:hover {
    background-color: #bbbbbb;
}

.ultility__buttons .mi_excel {
    margin: 0 30px 0 10px;
}

.ultility__buttons .mi__refresh {
    margin: 0 0 0 10px;
}
</style>