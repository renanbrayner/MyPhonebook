import { createRouter, createWebHistory } from 'vue-router'
import DefaultLayout from '../layouts/DefaultLayout.vue'
import HomeView from '../views/HomeView.vue'
import ContactNewView from '../views/ContactNewView.vue'

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
      path: '/novo-contato',
      name: 'novo-contato',
      component: DefaultLayout,
      children: [
        {
          path: '',
          name: 'Novo Contato',
          component: ContactNewView,
        },
      ],
    },
  ],
})

export default router
