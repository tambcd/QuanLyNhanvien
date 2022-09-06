<template>
  <div class="the__navbar bg__white">
        <div class="vx__navbar__wrapper">
            <div class="vx__navbar navbar__custom flex items__center">
                <div class="header__icon icon icon__24 mi__three__stripes pointer" v-show="!ischeckMiniMenu" @click="openMiniMenu()"></div>
                <div class="flex header__branch">
                    <div class="con__ms__tooltip">
                        <div class="msTooltipParent"></div>
                        <div class="tooltip__content">
                             <TooltipTable :isInline="true" tooltipContent="CÔNG TY Cổ phần MISA" :show="true">
                                <div class="header__branch__name">CÔNG TY Cổ phần MISA</div>
                            </TooltipTable>
                            
                        </div>
                    </div>
                    <a href="#" class="flex items__center">
                        <div class="header__icon icon icon__14 mi__chevron__right header__branch__icon pointer">
                        </div>
                    </a>
                </div>
                <div class="con__ms__tooltip flex">
                     <TooltipTable :isInline="true" tooltipContent="Thông báo" :show="true">
                                <div class="header__icon icon icon__24 mi__notify pointer"></div>
                            </TooltipTable>
                    <div class="msTooltipParent"></div>
                    <div class="tooltip__content">
                        <div class="header__current__db">                           
                            <div class="header__icon icon icon__24 avatar_user pointer"></div>
                            <div class="header__current__db__name">
                                Trần Văn Tám
                            </div>

                            <div class="icon icon__14 mi__chevron__right header__branch__icon pointer">
                            </div>

                        </div>
                    </div>
                </div>

            </div>
        </div>
    </div>
</template>

<script>
export default {
    data() {
        return {
            ischeckMiniMenu:false,
        }
    },
    methods: {

        /**
        * mở mini menu               
        * Author: TVTam
        * Last Edited: 27/07/2022  *  
        */
         openMiniMenu(){
               // Description:truyền dự liệu sang Saibar ẩn toggle
            this.ischeckMiniMenu = true
             this.emitter.emit('OpenMenuMini', this.ischeckMiniMenu )
        
        }
    },
    
    mounted() {
        // Description:Nhận dự liệu sang Saibar hiện toggle
         this.emitter.on('OpenMenuBig',data=>{
         this.ischeckMiniMenu = data
    })
    },

}
</script>

<style>
@import url(../style/css/page/TheMenuHeader/header__navbar.css);
.avatar_user{
    background: -415px -903px ;
}

</style>