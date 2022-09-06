<template>
  <div id="app">
    <div class="layout__main navbar__sticky footer__static">
      <TheSidebar/>

       <div id="content__area" class="content__area__default">
        <div id="content__overlay">
        </div>
        <div id="main__content" class="relative content__wrapper">
           
            <HeaderMenu />
            <TheContent/>
        </div>
    </div>
     

    </div>
  </div>
</template>

<script>
import TheSidebar from './layout/TheSidebar.vue'
import HeaderMenu from './layout/HeaderMenu.vue'
import TheContent from './layout/TheContent.vue'



export default {
  name: 'App',
  components: {
    TheSidebar,
    HeaderMenu,
    TheContent,
},
  data() {
    return {

      IscheckMenu:false,
   
    }
  },
  methods: {
    OpenMenuMini(data){
      this.IscheckMenu = data
    }
  },
}
</script>

<style>
@import url(./style/css/main.css);
</style>
