<template>
    <tr :class="{row__selected: ischeckItem }">
        <td class="c__txt__center first__column" style="width:50px;"><input type="checkbox" :checked="ischeckItem"
                @change="sendCheckEmp(ischeckItem)" v-model="ischeckItem"></td>
        <td style="min-width:150px;">{{ employeeItem.employeeCode }}</td>
        <td style="min-width:150px; width:150px;">{{ employeeItem.fullName }}</td>
        <td style="min-width:100px; width:100px;">{{ employeeItem.genderName || "" }}</td>
        <td class="center" style="min-width:120px;width:120px;">{{ validateDoB(employeeItem.dateOfBirth) }}</td>
        <td style="width:150px;min-width:150px;">{{ employeeItem.idenbtyNumber }}</td>
        <td style="width:150px;min-width:150px;">{{ employeeItem.employeeTitle }}</td>
        <td style="min-width:150px;">{{ employeeItem.departmentName }} </td>
        <td style="min-width:150px;width:150px;">{{ employeeItem.bankAccount }}</td>
        <td style="min-width:150px;">{{ employeeItem.bankName }}</td>
        <td style="min-width:200px;">{{ employeeItem.bankPlace }}</td>
        <td class="center last__column" id="btnGroup" style="min-width: 150px">
            <div class="table__drop"> <button class="table__drop__btn_update"
                    @click="updateEployee(employeeItem.employeeId)">Sửa</button>
                <GroupBtnControl :refSelect="employeeItem.employeeId" :Employee="employeeItem" />
            </div>
        </td>
    </tr>
</template>

<script>
import GroupBtnControl from '../Button/GroupBtnControl.vue'
export default {
    props: {
        /** danh sách nhân viên */
        employeeItem: {},        
        ischeckitemtr: Boolean,
        /** mảng mã nhân viên chọn */
        listCheck: []

    },
    data() {
        return {
            ischeckItem: false,
            idEmployee: "",
            dataAll: ['', false]
        };
    },
    mounted() {

        /**
        * trả về số phần tử trong mảng xóa            
        * Author: TVTam
        * Last Edited: 26/07/2022  
        */
        this.emitter.on("ListSelectDelete", (data) => {
            this.checkSelect(data)
        })
        this.checkSelectCheckbox()

    },

    methods: {

        /**
        * dịnh dạng dữ liệu date        
       * Author: TVTam
       * Last Edited: 26/07/2022  
       */
        validateDoB(Dob) {
            let dob = new Date(Dob);
            let dd = dob.getDate();
            let mm = dob.getMonth() + 1;
            let yyyy = dob.getFullYear();

            if (dob.getDate() < 10) {
                dd = "0" + dd;
            }
            if (dob.getMonth() + 1 < 10) {
                mm = "0" + mm;
            }
            return dd + "/" + mm + "/" + yyyy;

        },

        /**
       * Nhân id của nhân viên gửi lên form nhập liệu để hiểu thi thồn tin nhân viên cần sửa       
       * Author: TVTam
       * Last Edited: 26/07/2022  
       */
        updateEployee(Id) {
            this.emitter.emit("UpdateEployee", Id);
        },

        /**
        * kiểm tra item đc chọn nhiều tích checkbox   
        * Author: TVTam
        * Last Edited: 26/07/2022  
        */

        checkSelect(data) {
            if (data.length === 0) {
                this.ischeckItem = false;
            }
            data.forEach(element => {
                if (element === this.employeeItem.employeeId) {
                    this.ischeckItem = true

                }

            });
        },
        // check select item
        checkSelectCheckbox() {
            this.checkSelect(this.listCheck);

        },
        /**
        * chọn và bỏ chọn nhân viên trên bản bằng checkbox 
        * @param : giá trị của checkbox : True or false
        * Author: TVTam
        * Last Edited: 26/07/2022  
        */
        sendCheckEmp(data) {

            if (data === true) {
                this.dataAll[1] = true
            }
            else {
                this.dataAll[1] = false
            }
            // gắn id kèm giá trị của checkbox để kiểm tra trong mảng đã đc chọn trước đó
            this.dataAll[0] = this.employeeItem.employeeId
            // gửi dư liệu lên kiểm tra
            this.emitter.emit('DataSelectAll', this.dataAll)

        }



    },
    components: { GroupBtnControl },

}
</script>


<style scoped>
input {
    width: 18px;
    height: 18px;
    border: 1px solid #b8b8b8;
    align-self: center;
    cursor: pointer;
    accent-color: #2ca01c;
}

.table__drop__btn_update {
    color: #0075c0;
    cursor: pointer;
    font-family: MISA fonts Bold;
    background: transparent;
    border: 1px solid transparent;
    margin-right: 5px;
}
.row__selected{
     background:  #e5f2fd;
}
.row__selected .first__column{
     background:  #e5f2fd;
}
.row__selected .last__column{
     background:  #e5f2fd;
}
</style>