<script>
import peopleData from '@/assets/people.json';

export default {
  data() {
    return {
      username: '',
      password: '',
      token: undefined,
      routeTitles: {
        'personalities': 'Выдающиеся личности',
        'personality': 'Личность',
        'exhibits': 'Экспонаты',
        'history': 'История развития ЛЭТИ',
        'traditions': 'Традиции и символы',
        'games': 'Игры',
        'contacts': 'Контакты',
        'employee': 'Панель управления'
      }
    }
  },
  mounted() {
    if (localStorage.token) {
      this.token = localStorage.token;
    }
  },
  computed: {
    getPageTitles() {
      const baseRoute = this.$route.path?.split('/').slice(1) || []
      let routeTuples = Array(baseRoute.length).fill({ "path": "", "displayName": "" })

      for (let i = 0; i < baseRoute.length; i++) {
        let newPath
        if (i == 0) {
          newPath = "/" + baseRoute[i]
        } else {
          newPath = routeTuples[i - 1].path + "/" + baseRoute[i]
        }

        if (baseRoute[i - 1] === 'personalities' && i > 0) {
          const personId = baseRoute[i]

          try {
            const person = peopleData.find(p => p.id === personId)

            if (person) {
              const nameParts = person.name.split(' ')
              const lastName = nameParts.length > 1 ? nameParts[2] : person.name

              routeTuples[i] = { "path": newPath, "displayName": lastName }
              continue
            }
          } catch (error) {
            console.error('Ошибка при получении данных о личности:', error)
          }
        }

        if (baseRoute[i - 1] === 'exhibits' && i > 0) {
          routeTuples[i] = { "path": newPath, "displayName": "Экспонат" }
          continue
        }

        routeTuples[i] = { "path": newPath, "displayName": this.routeTitles[baseRoute[i]] || 'Раздел' }
      }

      return routeTuples
    }
  },
  methods: {
    async login(e) {
      e.preventDefault();
      const response_raw = await fetch("http://" + import.meta.env.VITE_API_LINK + "/api/Auth/login", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({
          username: this.username,
          password: this.password,
        }),
      });
      const response = await response_raw.json();
      this.username = ''
      this.password = ''
      if (response.token) {
        localStorage.token = response.token;
        this.token = response.token;
      };
      window.location.reload()
    },
    logout() {
      localStorage.removeItem('token');
      this.token = undefined;
      window.location.reload();
    },
    showForgotPasswordMessage() {
      alert('Для восстановления пароля свяжитесь с администратором сайта');
    }
  }
}
</script>

<template>
  <div class="container py-4">
    <header class="d-flex flex-column flex-md-row justify-content-between align-items-md-center mb-4">
      <div class="header-content mb-3 mb-md-0">
        <router-link to="/" class="header-logo">
          <h1 class="display-5 fw-bold">Музей ЛЭТИ</h1>
        </router-link>
        <p class="lead mb-0 small-text">История и наследие Санкт-Петербургского электротехнического университета</p>
      </div>
      <div class="auth-buttons-wrapper d-flex flex-column flex-md-row align-items-stretch align-items-md-center">
        <router-link v-if="token != undefined" style="text-decoration: none; color: white" to="/employee" class="auth-button-link">
          <button style="margin-right: 1em" class="btn auth-btn">
            Панель управления
          </button>
        </router-link>
        <button v-if="token == undefined" class="btn auth-btn d-flex align-items-center" data-bs-toggle="modal"
          data-bs-target="#authModal">
          <i class="bi bi-person-fill me-2"></i>
          Авторизация
        </button>
        <button v-else class="btn auth-btn d-flex align-items-center auth-button-logout" @click="logout">
          <i class="bi bi-box-arrow-right me-2"></i>
          Выход
        </button>
      </div>
    </header>

    <nav v-if="$route.path !== '/'" class="breadcrumb-container" aria-label="breadcrumb">
      <ol class="breadcrumb m-0">
        <li class="breadcrumb-item">
          <router-link to="/"><i class="bi bi-house-door me-1"></i>Главная</router-link>
        </li>
        <li v-for="obj in getPageTitles" class="breadcrumb-item" aria-current="page">
          <router-link :to="obj.path">{{ obj.displayName }}</router-link>
        </li>
      </ol>
    </nav>

    <main>
      <router-view ref="routerView" />
    </main>

    <footer class="pt-4 mt-5 border-top text-center">
      <div class="row">
        <div class="col-md-6 mb-3 mb-md-0 text-md-start">
          <h5>Музей ЛЭТИ</h5>
          <p class="text-muted small mb-0">&copy; {{ new Date().getFullYear() }} Санкт-Петербургский электротехнический
            университет</p>
        </div>
      </div>
    </footer>
  </div>

  <div class="modal fade" id="authModal" tabindex="-1" aria-labelledby="authModalLabel" aria-hidden="true">
    <div class="modal-dialog modal-dialog-centered">
      <div class="modal-content">
        <div class="modal-header">
          <h5 class="modal-title" id="authModalLabel">
            <i class="bi bi-person-fill me-2"></i>Авторизация
          </h5>
          <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
        </div>
        <div class="modal-body">
          <form>
            <div class="mb-3">
              <label for="username" class="form-label">Логин</label>
              <div class="input-group">
                <span class="input-group-text"><i class="bi bi-person"></i></span>
                <input type="text" class="form-control" id="username" v-model="username" placeholder="Введите логин">
              </div>
            </div>
            <div class="mb-3">
              <label for="password" class="form-label">Пароль</label>
              <div class="input-group">
                <span class="input-group-text"><i class="bi bi-key"></i></span>
                <input type="password" class="form-control" id="password" v-model="password"
                  placeholder="Введите пароль">
              </div>
            </div>
            <div class="mb-3 form-check">
              <input type="checkbox" class="form-check-input" id="rememberMe">
              <label class="form-check-label" for="rememberMe">Запомнить меня</label>
            </div>
          </form>
          <div class="text-center mt-4 mb-2">
            <a href="#" class="small text-muted" @click.prevent="showForgotPasswordMessage">Забыли пароль?</a>
          </div>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Отмена</button>
          <button type="button" class="btn auth-btn d-flex align-items-center" @click="login">
            <i class="bi bi-box-arrow-in-right me-2"></i>Войти
          </button>
        </div>
      </div>
    </div>
  </div>
</template>

<style scoped>
.header-content {
  flex: 1;
}

.header-logo {
  text-decoration: none;
  color: inherit;
  display: inline-block;
}

.header-logo:hover h1 {
  color: #395eab;
}

.header-logo h1 {
  transition: color 0.3s ease;
}

.social-link {
  display: inline-flex;
  justify-content: center;
  align-items: center;
  width: 36px;
  height: 36px;
  color: #6c757d;
  background-color: #f8f9fa;
  border-radius: 50%;
  transition: all 0.3s ease;
}

.social-link:hover {
  color: white;
  background-color: #395eab;
  transform: translateY(-3px);
}

footer {
  color: #6c757d;
}

footer h5 {
  color: #395eab;
  font-weight: 600;
}

@media (max-width: 768px) {
  .header-content {
    width: 100%;
    text-align: center;
  }

  .auth-buttons-wrapper {
    width: 100%;
    margin-top: 1rem;
  }

  .auth-buttons-wrapper .auth-button-link,
  .auth-buttons-wrapper .btn.auth-btn {
    width: 100%;
    margin-right: 0 !important;
    margin-bottom: 0.5rem;
    justify-content: center;
  }
  
  .auth-buttons-wrapper .auth-button-link button {
      width: 100%;
      margin-right: 0 !important;
  }
  
  .auth-buttons-wrapper > *:last-child {
      margin-bottom: 0;
  }

  .display-5 {
    font-size: 1.8rem;
  }

  .small-text {
    font-size: 0.9rem;
  }
}

@media (max-width: 576px) {
  .display-5 {
    font-size: 1.5rem;
  }

  .small-text {
    font-size: 0.8rem;
  }

  .auth-btn {
    padding: 8px 16px;
    font-size: 0.9rem;
  }
}
</style>
