<template>
    <div class="father__table">
        <div class="table">
            <table class="e__table" cellspacing="0">
                <thead>
                    <tr>
                        <th class="first__column center" ref="checkBoxAll">
                            <input v-model="ischecktr" type="checkbox" @click="checkAlldata()">
                        </th>
                        <th>MÃ NHÂN VIÊN</th>
                        <th>TÊN NHÂN VIÊN</th>
                        <th>GIỚI TÍNH</th>
                        <th class="center">NGÀY SINH</th>
                        <th style="min-width: 100px">
                            <TooltipTable :isInline="true" tooltipContent="Số chứng minh nhân dân" :show="true">
                                <span>SỐ CMND</span>
                            </TooltipTable>
                        </th>
                        <th>CHỨC DANH</th>
                        <th>TÊN ĐƠN VỊ</th>
                        <th>SỐ TÀI KHOẢN</th>
                        <th>TÊN NGÂN HÀNG</th>
                        <th style="min-width: 200px">
                            <TooltipTable :isInline="true" tooltipContent="Chi nhánh tài khoản ngân hàng" :show="true">
                                <span>CHI NHÁNH TK NGÂN HÀNG</span>
                            </TooltipTable>
                        </th>
                        <th class="last__column center">
                            CHỨC NĂNG
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <ItemTable v-for="employee in listData" :key="employee.employeeId" :employeeItem="employee"
                        @dataSelectAll="dataSelectAll($dataId)" :listCheck="listId" :ischeckitemtr="ischecktr"
                        @dblclick="updateEmployee(employee.employeeId)" @changeIdContext="changeIdContext(data)" />
                </tbody>
            </table>
            <div class="no__data" v-if="show"></div>
            <div class="no__data__content" v-if="show">Không có dữ liệu</div>
        </div>
    </div>
</template>

<script>
import ItemTable from './ItemTable.vue';
import { getById } from "@/api/api.js";
import { createToaster } from "@meforma/vue-toaster";
const toaster = createToaster({ position: 'top' });
export default {
    props: {
        /** danh sách nhân viên hiển thị */
        listData: [],
    },
    components: { ItemTable }
    ,

    data() {
        return {
            /** chọn hàng */
            ischecktr: false,
            /** Danh sách id nhân viên */
            listId: [],

        }
    },
    mounted() {

        /**
        * Author: TVTam
        * Last Edited: 28/07/2022  
        * gửi thông tin nhân viên cần sửa lên form sửa
        */
        this.emitter.on("UpdateEployee", (data) => {
            this.updateEmployee(data);
        }),

            /**
            * Author: TVTam
            * Last Edited: 26/07/2022  
            *  bỏ chọn gán mảng lưu về rỗng 
       
            */
            this.emitter.on("uncheckItem", () => {
                this.listId = [];
                // chuyển mảng qua itemtable checkbox về false
                this.emitter.emit('ListSelectDelete', this.listId)
                // chuyển mảng qua EmployyMain đóng 
                this.emitter.emit("changeDataEmploy", this.listData)
            }),


            /**
            * Author: TVTam
            * Last Edited: 26/07/2022  
            * sửa lý thêm xóa nhân viên khi thực hiện chọn nhiều 
            */
            this.emitter.on("deleteOneSuccefull", (data) => {
                let a = [];
                this.listId.forEach(element => {
                    if (data !== element) {
                        a.push(element);
                    }
                });
                this.listId = a;
                // chuyển mảng qua itemtable checkbox về false
                this.emitter.emit('ListSelectDelete', this.listId)
                // chuyển mảng qua EmployyMain đóng 
                this.emitter.emit("changeDataEmploy", this.listData)
            }),
            // khi checkbox cập nhập lại mảng chứa
            this.emitter.on('DataSelectAll', data => {
                this.dataSelectAll(data)
                this.emitter.emit('ListSelectDelete', this.listId)
            });

    },
    methods: {

        /**
        * Author: TVTam
        * Last Edited: 26/07/2022  
        * Lấy thông tin nhân viên theo ID gửi lên form sửa nhân viên
        */
        async updateEmployee(id) {
            await getById('Employees', id
                , (res) => {
                    // Trường hợp thành công  gửi lên form sửa
                    this.emitter.emit('openDialogUpDate', res.data)


                }, (error) => {
                    // Trường hợp thất bại thì hiển thị toastMessage lỗi và ghi rõ lỗi xảy ra.
                    toaster.error(`Gửi thất bại: ${error}`);


                })
        },

        /**
        * Author: TVTam
        * Last Edited: 26/07/2022  
        * chọn nhiều : check giá trị checkbox đẩy vào mảng xóa
          */
        dataSelectAll(data) {

            if (data[1]) {
                let b = true;
                for (let index = 0; index < this.listId.length; index++) {
                    if (data.idEmployeedl === index) {
                        b = false;
                        break;
                    }
                }
                if (b) {
                    this.listId.push(data[0])
                }
            }
            else {
                let a = []
                this.listId.forEach(element => {
                    if (data[0] !== element) {
                        a.push(element)
                    }
                });

                this.listId = a;
            }

        },

        /**
        * Author: TVTam
        * Last Edited: 26/07/2022  
        * chọn tất cả bản ghi trên trang  
        */
        checkAlldata() {
            if (!this.ischecktr) {
                this.listId = [];
                this.listData.forEach(element => {
                    this.listId.push(element.employeeId)
                });

            }
            else {
                this.listId = [];
            }
            this.emitter.emit('ListSelectDelete', this.listId)
            // chuyển mảng qua EmployyMain đóng 
            this.emitter.emit("changeDataEmploy", this.listData)
        }

    },
    watch: {
        // độ dài của mảng chứa ID khi chọn nhiểu
        'listId.length'(newData) {
            if (newData === this.listData.length) {
                this.ischecktr = true;
            }
            else {
                this.ischecktr = false;
            }
        }
    }

}
</script>

<style scoped>
@import url(../../style/css/base/table/data__table.css);

input {
    width: 18px;
    height: 18px;
    border: 1px solid #b8b8b8;
    align-self: center;
    cursor: pointer;
    accent-color: #2ca01c;
}
</style>