import {createWebHistory, createRouter} from "vue-router";
import TheHome from '@/page/Home/TheHome'
import EmployeeMain from '@/page/employee/EmployeeMain'

const routes = [
    {path:'/',redirect: '/dashboard'},
    {path:'/dashboard',component:TheHome},
    {path:'/cash', component: EmployeeMain},
]

const router = createRouter({
    history: createWebHistory(),
    routes, 
  })
  
export {router};