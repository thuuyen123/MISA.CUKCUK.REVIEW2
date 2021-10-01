import Vue from 'vue'
import VueRouter from 'vue-router'
import MaterialList from '../views/material/MaterialList.vue'

Vue.use(VueRouter)

const routes = [
  {
    path: '/',
    name: 'MaterialList',
    component: MaterialList
  },
  {
    path: '/material',
    name: 'MaterialList',
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: MaterialList
  }
]

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes
})

export default router
