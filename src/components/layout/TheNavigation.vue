<!-- src/components/layout/TheNavigation.vue -->
<template>
    <nav class="navbar navbar-expand-lg navbar-dark bg-primary fixed-top">
      <div class="container">
        <router-link class="navbar-brand" to="/">
          <img src="" alt="ЛЭТИ логотип" class="me-2">
          ЛЭТИ
        </router-link>
        <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNav">
          <span class="navbar-toggler-icon"></span>
        </button>
        <div class="collapse navbar-collapse" id="navbarNav">
          <ul class="navbar-nav ms-auto">
            <li class="nav-item">
              <router-link class="nav-link" to="/" active-class="active">Главная</router-link>
            </li>
            <li class="nav-item">
              <router-link class="nav-link" to="/about" active-class="active">История</router-link>
            </li>
            <li class="nav-item">
              <router-link class="nav-link" to="/persons" active-class="active">Выдающиеся личности</router-link>
            </li>
            <li class="nav-item">
              <router-link class="nav-link" to="/contact" active-class="active">Контакты</router-link>
            </li>
          </ul>
        </div>
      </div>
    </nav>
  </template>
  
  <script>
  export default {
    name: 'TheNavigation'
  }
  </script>
  
  <style scoped>
  .navbar {
    box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
  }
  </style>