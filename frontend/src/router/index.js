import Vue from 'vue'
import Router from 'vue-router'

Vue.use(Router)

const router = new Router({
  mode: 'history',
  base: process.env.BASE_URL,
  routes: [
    {
      path: '/',
      name: 'Home',
      redirect: { name: 'Wells' },
      components: {
        main: () => import('../components/Layout.vue')
      },
      children: [
        { path: 'wells', name: 'Wells', components: { routeradmin: () => import('../components/Well/Index.vue') } },
        { path: 'well/create', name: 'CreateWell', components: { routeradmin: () => import('../components/Well/Create.vue') } },
        { path: 'well/edit/:id', name: 'EditWell', props: { routeradmin: true }, components: { routeradmin: () => import('../components/Well/Edit.vue') } },
        { path: 'fields', name: 'Fields', components: { routeradmin: () => import('../components/Field/Index.vue') } },
        { path: 'field/create', name: 'CreateField', components: { routeradmin: () => import('../components/Field/Create.vue') } },
        { path: 'field/edit/:id', name: 'EditField', props: { routeradmin: true }, components: { routeradmin: () => import('../components/Field/Edit.vue') } },
        { path: 'shops', name: 'Shops', components: { routeradmin: () => import('../components/Shop/Index.vue') } },
        { path: 'shop/create', name: 'CreateShop', components: { routeradmin: () => import('../components/Shop/Create.vue') } },
        { path: 'shop/edit/:id', name: 'EditShop', props: { routeradmin: true }, components: { routeradmin: () => import('../components/Shop/Edit.vue') } },
        { path: 'companies', name: 'Companies', components: { routeradmin: () => import('../components/Company/Index.vue') } },
        { path: 'company/create', name: 'CreateCompany', components: { routeradmin: () => import('../components/Company/Create.vue') } },
        { path: 'company/edit/:id', name: 'EditCompany', props: { routeradmin: true }, components: { routeradmin: () => import('../components/Company/Edit.vue') } }
      ]
    }
  ]
})

router.beforeEach(
  (to, from, next) => {
    next()
  }
)
export default router
