<template>
  <div>
    <div class="title-block mb-4">
      <h2><i class="bi bi-buildings me-2"></i>Добро пожаловать в виртуальный музей ЛЭТИ</h2>
    </div>

    <div class="home-intro p-4 mb-5 bg-light rounded-3 text-center">
      <h3 class="fw-light mb-3">Исследуйте историю старейшего электротехнического университета России</h3>
      <p class="lead mb-0">Выберите интересующий раздел и погрузитесь в увлекательное путешествие по истории ЛЭТИ</p>
    </div>
    
    <div class="row row-cols-1 row-cols-md-2 row-cols-lg-3 g-4">
      <div class="col">
        <router-link to="/history" class="card-link">
          <div class="menu-card">
            <div class="icon-circle">
              <i class="bi bi-book"></i>
            </div>
            <h3>История развития ЛЭТИ</h3>
            <p class="menu-card-description">Хронология основных событий</p>
          </div>
        </router-link>
      </div>
      
      <div class="col">
        <router-link to="/personalities" class="card-link">
          <div class="menu-card">
            <div class="icon-circle">
              <i class="bi bi-people"></i>
            </div>
            <h3>Выдающиеся личности</h3>
            <p class="menu-card-description">Ученые и исследователи</p>
          </div>
        </router-link>
      </div>
      
      <div class="col">
        <router-link to="/exhibits" class="card-link">
          <div class="menu-card">
            <div class="icon-circle">
              <i class="bi bi-archive"></i>
            </div>
            <h3>Экспонаты</h3>
            <p class="menu-card-description">Коллекция музея</p>
          </div>
        </router-link>
      </div>
      
      <div class="col">
        <router-link to="/traditions" class="card-link">
          <div class="menu-card">
            <div class="icon-circle">
              <i class="bi bi-flag"></i>
            </div>
            <h3>Традиции и символы</h3>
            <p class="menu-card-description">Культура университета</p>
          </div>
        </router-link>
      </div>
      
      <div class="col">
        <router-link to="/games" class="card-link">
          <div class="menu-card">
            <div class="icon-circle">
              <i class="bi bi-controller"></i>
            </div>
            <h3>Игры</h3>
            <p class="menu-card-description">Интерактивные развлечения</p>
          </div>
        </router-link>
      </div>
      
      <div class="col">
        <router-link to="/contacts" class="card-link">
          <div class="menu-card">
            <div class="icon-circle">
              <i class="bi bi-envelope"></i>
            </div>
            <h3>Контакты</h3>
            <p class="menu-card-description">Как связаться с нами</p>
          </div>
        </router-link>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: 'HomePage',
  data() {
	  return {
		token: undefined,
	  }
  },
  mounted() {
	if (localStorage.token) {
	  this.token = localStorage.token;
	}
  }
}
</script>

<style scoped>
.home-intro {
  background-color: rgba(255, 255, 255, 0.7) !important;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.05);
}

.icon-circle {
  display: flex;
  justify-content: center;
  align-items: center;
  width: 80px;
  height: 80px;
  background-color: rgba(57, 94, 171, 0.1);
  border-radius: 50%;
  margin-bottom: 20px;
  transition: all 0.3s ease;
}

.icon-circle i {
  font-size: 2.5rem;
  color: #395eab;
  margin-bottom: 0;
}

.menu-card:hover .icon-circle {
  background-color: rgba(255, 255, 255, 0.2);
}

.menu-card:hover .icon-circle i {
  color: white;
}

.menu-card-description {
  margin-top: 8px;
  font-size: 0.9rem;
  opacity: 0.8;
  margin-bottom: 0;
}

.menu-card:hover .menu-card-description {
  opacity: 1;
  color: rgba(255, 255, 255, 0.9);
}

@media (max-width: 992px) {
  .menu-card {
    padding: 20px 16px;
  }
  
  .icon-circle {
    width: 70px;
    height: 70px;
    margin-bottom: 16px;
  }
  
  .icon-circle i {
    font-size: 2rem;
  }
}

@media (max-width: 768px) {
  .home-intro {
    padding: 16px !important;
  }
  
  .home-intro h3 {
    font-size: 1.3rem;
  }
  
  .home-intro .lead {
    font-size: 1rem;
  }
}

@media (max-width: 576px) {
  .home-intro h3 {
    font-size: 1.1rem;
  }
  
  .home-intro .lead {
    font-size: 0.9rem;
  }
  
  .icon-circle {
    width: 60px;
    height: 60px;
    margin-bottom: 12px;
  }
  
  .icon-circle i {
    font-size: 1.7rem;
  }
  
  .menu-card-description {
    font-size: 0.8rem;
  }
}
</style> 
