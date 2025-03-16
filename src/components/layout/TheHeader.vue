<!-- src/components/layout/TheHeader.vue -->
<template>
    <header class="hero-section text-white text-center">
      <div class="container">
        <h1 class="display-4 fw-bold">Санкт-Петербургский государственный электротехнический университет «ЛЭТИ»</h1>
        <p class="lead">Один из старейших электротехнических университетов России</p>
        <button class="btn btn-light btn-lg mt-3">Узнать больше</button>
      </div>
    </header>
  </template>
  
  <script>
  export default {
    name: 'TheHeader'
  }
  </script>
  
  <style scoped>
  .hero-section {
    background: linear-gradient(rgba(0, 51, 102, 0.8), rgba(0, 51, 102, 0.8)), url('/api/placeholder/1920/600');
    background-size: cover;
    background-position: center;
    height: 100vh;
    display: flex;
    align-items: center;
    padding-top: 76px;
  }
  </style>