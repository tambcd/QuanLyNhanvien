<template>
    <div class="tooltip" :class="[isError? 'toop__tip__error' : '',{'inline__tootip':isInline}]" @mouseover="event">
        <div class="tooltip__text" v-show="show&&tdShow">{{tooltipContent}}</div>
        <slot></slot>
    </div>
</template>
<script>
export default {
    props:{
           tooltipContent:String,//Nội dung tooltip
           show: {
          type: Boolean,
          default: false
           },//Ân hiện tooltip

           isInline: {
          type: Boolean,
          default: false
           },//Ân hiện tooltip

           isDisplay: {
          type: Boolean,
          default: false
           },//Ân hiện tooltip
    },
    data(){
        return{
             isError:false,//Tooltip dùng cho l
             tdShow:true
        }
    },   
    methods:{
        /**
         * sự kiên ẩn hện tooltip
         * created : tvTam 1/8/2022
         */
        event(){
        let hide=this.$el.children[1]
          if(!(hide.clientHeight<hide.scrollHeight) && hide.classList.contains('hide__td'))
          {
              this.tdShow=false
        
          }
    }
}
}
</script>
<style scoped>

.tooltip {
    position: relative;
    display: block;
    font-size: 13px;
}

.tooltip__text {
    display: none;
    background-color: #555;
    color: #fff;
    text-align: center;
    padding: 2px 10px;
    border-radius: 3px;
    position: absolute;
    z-index: 20;
    top: 130%;
    opacity: 0;
    transition: opacity 0.3s;
    width: max-content;
    left: 50%;
    transform: translate(-50%, 0);
}

.toop__tip__error .tooltip__text::after {
    content: "";
    position: absolute;
    bottom: 98%;
    left: 50%;
    margin-left: -5px;
    border-width: 5px;
    border-style: solid;
    border-color: transparent transparent #ff7777 transparent;
}

.tooltip:hover .tooltip__text {
    display: block;
    opacity: 1;
}

.toop__tip__error .tooltip__text {
    background-color: #ff7777;
}

.inline__tootip {
    display: inline !important;
}
</style>