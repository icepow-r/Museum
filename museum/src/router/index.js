import { createRouter, createWebHistory } from 'vue-router'
import Home from '../views/Home.vue'

const routes = [
  {
    path: '/',
    name: 'home',
    component: Home
  },
  {
    path: '/personalities',
    name: 'personalities',
    component: () => import('../views/Personalities.vue')
  },
  {
    path: '/personalities/:id',
    name: 'personality',
    component: () => import('../views/PersonalityDetail.vue')
  },
  {
    path: '/exhibits',
    name: 'exhibits',
    component: () => import('../views/Exhibits.vue')
  },
  {
    path: '/exhibits/:id',
    name: 'exhibit',
    component: () => import('../views/ExhibitDetail.vue')
  },
  {
    path: '/history',
    name: 'history',
    component: () => import('../views/History.vue')
  },
  {
    path: '/traditions',
    name: 'traditions',
    component: () => import('../views/Traditions.vue')
  },
  {
    path: '/games',
    name: 'games',
    component: () => import('../views/Games.vue')
  },
  {
    path: '/games/:id',
    name: 'game',
    component: () => import('../views/GameDetail.vue')
  },
  {
    path: '/contacts',
    name: 'contacts',
    component: () => import('../views/Contacts.vue')
  },
  {
    path: '/employee',
    name: 'employee',
    component: () => import('../views/EmployeePanel.vue')
  }
]

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes
})

export default router 
