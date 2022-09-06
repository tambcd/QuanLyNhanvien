import { createApp } from 'vue'
import App from './App.vue'
import mitt from 'mitt';
import Toaster from "@meforma/vue-toaster";
import {requiredComponents} from './globalComponents'
import {router} from './router/router';
import '@vuepic/vue-datepicker/dist/main.css';



  const emitter = mitt();
  const app = createApp(App);

  // globalComponents
  registerComponents(app);
  
  // event bus
  app.config.globalProperties.emitter = emitter;
  app.use(router,Toaster).mount('#app');


  // nhung globalComponents

  function registerComponents(app) {
    for(let component of requiredComponents){
      app.component(component.componentName, component.component);
    }
}






  