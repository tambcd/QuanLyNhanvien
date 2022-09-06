<template>
    <div class="menu__container " :class="{ expand: !ischeckMini }">
        <!-- Side barHeader -->
        <div class="logo__container">
            <a href="#" :class="{ menu__container__option: !ischeckMini, menu__container__option__mini: ischeckMini }"
                @click="BigMenu()"> </a>
            <router-link to="/" class="logo">
                <img src="../assets/img/Logo_Module_TiengViet_White.66947422.svg" alt="" class="img__icon"
                    v-show="!ischeckMini">
            </router-link>
        </div>

        <!-- Sidebar Option -->

        <div class="menu__item__list scroller">
            <div class="con__ms__tooltip" v-for="item in OptionSidebars" :key="item.ItemID">
                <TooltipTable :isInline="true" :tooltipContent="item.ItemTxt" :show="ischeckMini">
                    <div class="tooltip__content">
                        <router-link :to=item.path class="menu__item router__link__active"
                            :class="{ select__item: itemSelect == item.ItemID }" @click="toItemSaideba(item.ItemID)">
                            <div class="menu__item__icon">
                                <div class="icon  icon__24 menu__icon" :class="item.ItemBg">
                                </div>
                            </div>
                            <div class="menu__item__title">{{ item.ItemTxt }}</div>
                        </router-link>
                    </div>
                </TooltipTable>

            </div>

        </div>


        <!-- Sidebar Footer  -->

        <div class="full__license" @click="ischeck = !ischeck" v-show="!ischeckMini">
            <div class="license__info icon__license__top license__info__visible license__info__lst">
                <div class="icon__license">
                    <div class="first__line">
                    </div>
                    <div class="second__line">
                    </div>
                </div>
            </div>
            <div v-if="ischeck">
                <div class="license__content license__lst">
                    <div class="license__title">Gói Ultimate</div>
                    <div class="license__sub">
                        "
                        Hạn dùng :
                        "
                        <span>12/12/2022</span>
                    </div>
                    <div class="license__about">Gia hạn ngày !</div>
                    <div class="license__about">Thông tin gói !</div>

                </div>
            </div>
        </div>
    </div>

</template>

<script>



export default {
    name: 'TheSidebar',
    props: {
        OpenMiniSb: Boolean
    },

    components: {
    },
    data() {
        return {
            /** đóng mở menu mini */
            ischeck: false,
            /** dánh dấu khi chọn */
            itemSelect: 1,
            ischeckMini: false,
            /**danh sách menu */
            OptionSidebars: [
                {
                    ItemID: 1,
                    ItemTxt: "Tổng quan",
                    ItemBg: "sidebar__dashboard",
                    path: '/'
                },
                {
                    ItemID: 2,
                    ItemTxt: "Tiền mặt",
                    ItemBg: "sidebar__cash",
                    path: '/cash'
                },
                {
                    ItemID: 3,
                    ItemTxt: "Tiền gửi",
                    ItemBg: "sidebar__bank",
                    path: '/'
                },
                {
                    ItemID: 4,
                    ItemTxt: "Mua hàng",
                    ItemBg: "sidebar__pu",
                    path: '/'
                },
                {
                    ItemID: 5,
                    ItemTxt: "Quản lý hóa đơn",
                    ItemBg: "sidebar__invoice",
                    path: '/'
                },
                {
                    ItemID: 6,
                    ItemTxt: "Kho",
                    ItemBg: "sidebar__store",
                    path: '/'
                },
                {
                    ItemID: 7,
                    ItemTxt: "Công cụ dụng cụ",
                    ItemBg: "sidebar__tools",
                    path: '/'
                },
                {
                    ItemID: 8,
                    ItemTxt: "Tài sản cố định",
                    ItemBg: "sidebar__assets",
                    path: '/'
                },
                {
                    ItemID: 9,
                    ItemTxt: "Thuế",
                    ItemBg: "sidebar__tax",
                    path: '/'
                },
                {
                    ItemID: 10,
                    ItemTxt: "Giá thành",
                    ItemBg: "sidebar__price",
                    path: '/'
                },
                {
                    ItemID: 11,
                    ItemTxt: "Tổng hợp",
                    ItemBg: "sidebar__general",
                    path: '/'
                },
                {
                    ItemID: 12,
                    ItemTxt: "Ngân sách",
                    ItemBg: "sidebar__budget",
                    path: '/'
                },
                // {
                //     ItemID: 13,
                //     ItemTxt: "Báo cáo",
                //     ItemBg: "sidebar__report",
                //     path: '/'

                // },
                // {
                //     ItemID: 14,
                //     ItemTxt: "Phân tích tài chính",
                //     ItemBg: "sidebar__finance",
                //     path: '/'

                // },
            ]

        }

    },
    mounted() {
        /**
         * Description: nhận dự liệu từ HeaderMenu mở mini menu 
         * Prams: none
         * Author: TVTam
         * Last Edited: 19/07/2022
         */
        this.emitter.on('OpenMenuMini', data => {
            this.ischeckMini = data
        })
    },


    methods: {
        toItemSaideba(idItem) {
            this.itemSelect = idItem;
        },
        /**
        * mở menu lớn              
        * Author: TVTam
        * Last Edited: 27/07/2022  *  
         */
        BigMenu() {
            this.ischeckMini = false;
            this.emitter.emit('OpenMenuBig', this.ischeckMini)

        }
    },
}
</script>

<style scoped>
@import url(../style/css/page/TheSidebar/menu.css);

.menu__item__mini {
    width: 52px;
}

.expand {
    width: auto;
}
</style>