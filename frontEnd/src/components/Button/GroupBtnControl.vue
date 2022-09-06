<template>
    <div class="table__choose" @click="buttonEvent" :class="{ border: isActive }" :ref="refSelect">
        <teleport to="tbody" v-if="show && refSelect">
            <div class="list__item" :style="{ top: y + 'px', left: x + 'px' }">
                <div class="item" @click="cloneEmployee()">Nhân bản</div>
                <div class="item" @click="deleteEmpoyee()">Xóa</div>
                <div class="item">Ngừng sử dụng</div>
            </div>
        </teleport>
    </div>

</template>

<script>
export default {
    props: {
        /**
         * đối tượng nhân viên
         * */
        Employee: Object,
        /**ref lấy ra vị trí  */
        refSelect: String
    },
     created() {
    window.addEventListener("click", this.close);
  },
    data() {
        return {
            checkBoder:false,
            show: false,// Ân hiện mục item combobox
            x: 0,//Toa do x contactmenu trong table
            y: 0,//Toa do y contactmenu trong table
            isActive :false
        }
    },
    methods: {
        /**
         * Xóa một nhân viên 
         * created: TVTam (25/7/2022)
         */
        deleteEmpoyee() {
            //mở thông báo xóa            
            this.emitter.emit("DeleteMessage", this.Employee)
        },
        /**
         * Đóng mở contacmenu
         * created: TVTam (25/7/2022)
         */
        buttonEvent() {
            this.show = !this.show//
            this.isActive = true;            
            // gán lại tạo đội cho contacmenu 
            let rect = this.$refs[this.refSelect].getBoundingClientRect()
            this.x = rect.x - 85//Lay toa do x
            this.y = 25 + rect.y//Lay toa do y
        },
        /** Nhân bản nhân viên
         * createby: TVTam (25/7/2022)
         */
        cloneEmployee(){
            /** Gửi thông tin nhân viên lên */
            this.emitter.emit('cloneEmployee',this.Employee);
        },

        /**
         * Đóng khi forcus ra ngoài
         * createby: TVTam (25/7/2022)
         */
        close(e) {
            if (!this.$refs[this.refSelect].contains(e.target)) {
                this.show = false
                this.isActive = false;   
                
            }
        }
    },
    beforeUnmount() {
        /**Xóa sự kiên ẩn menu
         * createby: TVTam (25/7/2022)
         */
    window.removeEventListener("click", this.close);
  },

}
</script>

<style scoped>
.table__choose {
    width: 30px;
    height: 20px;
    margin-left: 5px;
    position: sticky;
    background: url(../../assets/img/Sprites.64af8f61.svg) no-repeat -890px -358px;
    z-index: 3;
}
.border{
    border: 1px solid #0075c0;
}
.list__item {
    margin-left: 5px;
    position: fixed;
    border: 1px solid #babec5;
    text-align: left;
    color: black;
    background-color: white;
    z-index: 30;
}

.item:hover {
    color: #2ca01c !important;
    background-color: rgb(187, 187, 187, 0.4) !important;
}

.item {
    padding: 0 10px;
    font-size: 13px;
    line-height: 32px;
    height: 32px;
    cursor: pointer;
}
</style>