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
                    <div class="license__title">G??i Ultimate</div>
                    <div class="license__sub">
                        "
                        H???n d??ng :
                        "
                        <span>12/12/2022</span>
                    </div>
                    <div class="license__about">Gia h???n ng??y !</div>
                    <div class="license__about">Th??ng tin g??i !</div>

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
            /** ????ng m??? menu mini */
            ischeck: false,
            /** d??nh d???u khi ch???n */
            itemSelect: 1,
            ischeckMini: false,
            /**danh s??ch menu */
            OptionSidebars: [
                {
                    ItemID: 1,
                    ItemTxt: "T???ng quan",
                    ItemBg: "sidebar__dashboard",
                    path: '/'
                },
                {
                    ItemID: 2,
                    ItemTxt: "Ti???n m???t",
                    ItemBg: "sidebar__cash",
                    path: '/cash'
                },
                {
                    ItemID: 3,
                    ItemTxt: "Ti???n g???i",
                    ItemBg: "sidebar__bank",
                    path: '/'
                },
                {
                    ItemID: 4,
                    ItemTxt: "Mua h??ng",
                    ItemBg: "sidebar__pu",
                    path: '/'
                },
                {
                    ItemID: 5,
                    ItemTxt: "Qu???n l?? h??a ????n",
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
                    ItemTxt: "C??ng c??? d???ng c???",
                    ItemBg: "sidebar__tools",
                    path: '/'
                },
                {
                    ItemID: 8,
                    ItemTxt: "T??i s???n c??? ?????nh",
                    ItemBg: "sidebar__assets",
                    path: '/'
                },
                {
                    ItemID: 9,
                    ItemTxt: "Thu???",
                    ItemBg: "sidebar__tax",
                    path: '/'
                },
                {
                    ItemID: 10,
                    ItemTxt: "Gi?? th??nh",
                    ItemBg: "sidebar__price",
                    path: '/'
                },
                {
                    ItemID: 11,
                    ItemTxt: "T???ng h???p",
                    ItemBg: "sidebar__general",
                    path: '/'
                },
                {
                    ItemID: 12,
                    ItemTxt: "Ng??n s??ch",
                    ItemBg: "sidebar__budget",
                    path: '/'
                },
                // {
                //     ItemID: 13,
                //     ItemTxt: "B??o c??o",
                //     ItemBg: "sidebar__report",
                //     path: '/'

                // },
                // {
                //     ItemID: 14,
                //     ItemTxt: "Ph??n t??ch t??i ch??nh",
                //     ItemBg: "sidebar__finance",
                //     path: '/'

                // },
            ]

        }

    },
    mounted() {
        /**
         * Description: nh???n d??? li???u t??? HeaderMenu m??? mini menu 
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
        * m??? menu l???n              
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