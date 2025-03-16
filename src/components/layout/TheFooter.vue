<!-- src/components/layout/TheFooter.vue -->
<template>
    <footer class="bg-dark text-white py-4 mt-5">
      <div class="container">
        <div class="row">
          <div class="col-md-4">
            <h5>Контакты</h5>
            <p>Адрес: Санкт-Петербург, ул. Профессора Попова, д. 5</p>
            <p>Телефон: +7 (812) 123-45-67</p>
            <p>Email: info@leti.ru</p>
          </div>
          <div class="col-md-4">
            <h5>Ссылки</h5>
            <ul class="list-unstyled">
              <li><router-link to="/" class="text-white">Главная</router-link></li>
              <li><router-link to="/about" class="text-white">История</router-link></li>
              <li><router-link to="/persons" class="text-white">Выдающиеся личности</router-link></li>
              <li><router-link to="/contact" class="text-white">Контакты</router-link></li>
            </ul>
          </div>
          <div class="col-md-4">
            <h5>Связаться с нами</h5>
            <div class="social-icons">
              <a href="#" class="text-white me-3"><i class="bi bi-facebook"></i></a>
              <a href="#" class="text-white me-3"><i class="bi bi-twitter"></i></a>
              <a href="#" class="text-white me-3"><i class="bi bi-instagram"></i></a>
              <a href="#" class="text-white"><i class="bi bi-youtube"></i></a>
            </div>
          </div>
        </div>
        <hr>
        <div class="text-center">
          <p>&copy; 2025 Санкт-Петербургский государственный электротехнический университет «ЛЭТИ». Все права защищены.</p>
        </div>
      </div>
    </footer>
  </template>
  
  <script>
  export default {
    name: 'TheFooter'
  }
  </script>
  
  <style scoped>
  .social-icons a {
    font-size: 1.5rem;
    transition: opacity 0.3s;
  }
  
  .social-icons a:hover {
    opacity: 0.7;
  }
  </style>