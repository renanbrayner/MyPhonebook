import { createRouter, createWebHistory } from 'vue-router'
import DefaultLayout from '../layouts/DefaultLayout.vue'
import HomeView from '../views/HomeView.vue'
import ContactView from '../views/ContactView.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: DefaultLayout,
      children: [
        {
          path: '',
          name: 'Home',
          component: HomeView,
        },
      ],
    },
    {
      path: '/contato',
      name: 'contato',
      component: DefaultLayout,
      children: [
        {
          path: '',
          name: 'Novo Contato',
          component: ContactView,
        },
        {
          path: ':id',
          name: 'Editar Contato',
          component: ContactView,
        },
      ],
    },
  ],
})

export default router
