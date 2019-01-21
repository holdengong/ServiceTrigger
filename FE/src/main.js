// The Vue build version to load with the `import` command
// (runtime-only or standalone) has been set in webpack.base.conf with an alias.
import Vue from 'vue'
import App from './App'
import VueRouter from 'vue-router'
import Home from './components/Home'
import About from './components/about/About'
import Login from './components/Login'
import Manage from './components/Manage'
import Menu from './components/Menu'
import Register from './components/Register'

//二级路由
import Contact from './components/about/Contact'
import Delivery from './components/about/Delivery'
import HistoryInfo from './components/about/HistoryInfo'
import OrderingGuide from './components/about/OrderingGuide'

// axios
import axios from 'axios'

import Element from 'element-ui'
import 'element-ui/lib/theme-chalk/index.css'
Vue.use(Element)


axios.defaults.headers.common['token']='111';
axios.defaults.headers.post["Content-Type"]='application/json';

Vue.use(VueRouter)

const routes = [
  {path:'/',component:Home},
  {path:'/menu',component:Menu},
  {path:'/about', redirect:'/about/historyInfo' ,component:About,children:[
    {path:'/about/contact',name:"contactLink",component:Contact},
    {path:'/about/delivery',name:"deliveryLink",component:Delivery},
    {path:'/about/historyInfo',name:"historyLink",component:HistoryInfo},
    {path:'/about/orderingGuide',name:"orderingGuideLink",component:OrderingGuide},
  ]},
  {path:'/login',component:Login},
  {path:'/manage',component:Manage},
  {path:'/register',component:Register},
  {path:'*',redirect:'/'}
]

const router = new VueRouter({
  routes,
  mode:'history'
});

// 全局守卫
router.beforeEach((to,from,next)=>{

  next();
  /* if(to.path=='/login' || to.path == '/register')
  {
    next();
  }
  else
  {
    alert("您还没有登录，请先登录!");
    next('/login');
  } */
});

// 后置钩子
router.afterEach((to,from)=>{
  
});

/* eslint-disable no-new */
new Vue({
  el: '#app',
  router,
  render:h=>h(App)
})
