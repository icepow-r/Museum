<template>
  <div>
    <div class="title-block">
      <h2><i class="bi bi-telephone-fill me-2"></i>Контактная информация</h2>
    </div>

    <div class="contacts-intro text-center mb-4 p-3 bg-light rounded">
      <p class="lead mb-0">Свяжитесь с нами для получения дополнительной информации о музее и организации экскурсий</p>
    </div>

    <div class="row g-4">
      <div class="col-md-6">
        <div class="contact-info-card">
          <div class="contact-info-header">
            <i class="bi bi-info-circle-fill"></i>
            <h3>Информация о музее</h3>
          </div>
          <div class="contact-info-body">
            <div class="contact-item">
              <i class="bi bi-geo-alt-fill"></i>
              <div>
                <h5>Адрес</h5>
                <p>г. Санкт-Петербург, ул. Профессора Попова, 5, корпус Д</p>
              </div>
            </div>
            <div class="contact-item">
              <i class="bi bi-clock-fill"></i>
              <div>
                <h5>Часы работы</h5>
                <p>Пн-Пт: 11:00 - 16:00<br>Сб-Вс: выходной</p>
              </div>
            </div>
            <div class="contact-item">
              <i class="bi bi-telephone-fill"></i>
              <div>
                <h5>Телефон</h5>
                <p> +7 (812) 234-59-00 </p>
              </div>
            </div>
            <div class="contact-item">
              <i class="bi bi-envelope-fill"></i>
              <div>
                <h5>Электронная почта</h5>
                <p><a href="mailto:museyLETI@mail.ru">museyLETI@mail.ru</a></p>
                <p><a href="mailto:radioemc@yandex.ru">radioemc@yandex.ru</a></p>
              </div>
            </div>
            <div class="contact-item">
              <i class="bi bi-globe"></i>
              <div>
                <h5>Веб-сайт</h5>
                <p><a href="https://etu.ru/ru/muzej/" target="_blank">etu.ru/ru/muzej/</a></p>
              </div>
            </div>
            
            <div class="social-media mt-4">
              <h5>Социальные сети</h5>
              <div class="social-icons">
                <a href="https://vk.com/museyleti" class="social-icon" title="ВКонтакте">
                  <img src="..\assets\vk-brands.svg" alt="VK"> 
                </a>
                <a href="https://t.me/museumleti" class="social-icon" title="Telegram">
                  <i class="bi bi-telegram"></i>
                </a>
              </div>
            </div>
          </div>
        </div>
      </div>
      
      <div class="col-md-6">
        <div class="contact-form-card">
          <div class="contact-form-header">
            <i class="bi bi-chat-dots-fill"></i>
            <h3>Обратная связь</h3>
          </div>
          <div class="contact-form-body">
            <p class="mb-4">Заполните форму, чтобы получить дополнительную информацию или записаться на экскурсию:</p>
            <form @submit.prevent="submitForm">
              <div class="mb-3">
                <label for="name" class="form-label">Имя и фамилия <span class="text-danger">*</span></label>
                <div class="input-group">
                  <span class="input-group-text"><i class="bi bi-person-fill"></i></span>
                  <input type="text" class="form-control" id="name" v-model="formData.name" required 
                         placeholder="Введите ваше имя и фамилию">
                </div>
              </div>
              <div class="mb-3">
                <label for="email" class="form-label">Электронная почта <span class="text-danger">*</span></label>
                <div class="input-group">
                  <span class="input-group-text"><i class="bi bi-envelope-fill"></i></span>
                  <input type="email" class="form-control" id="email" v-model="formData.email" required
                         placeholder="example@mail.ru">
                </div>
              </div>
              <div class="mb-3">
                <label for="phone" class="form-label">Телефон</label>
                <div class="input-group">
                  <span class="input-group-text"><i class="bi bi-telephone-fill"></i></span>
                  <input type="tel" class="form-control" id="phone" v-model="formData.phone"
                         placeholder="+7 (XXX) XXX-XX-XX">
                </div>
              </div>
              <div class="mb-3">
                <label for="subject" class="form-label">Тема обращения <span class="text-danger">*</span></label>
                <div class="input-group">
                  <span class="input-group-text"><i class="bi bi-bookmark-fill"></i></span>
                  <select class="form-select" id="subject" v-model="formData.subject" required>
                    <option value="" disabled selected>Выберите тему</option>
                    <option value="Экскурсия">Экскурсия</option>
                    <option value="Вопрос">Вопрос</option>
                    <option value="Сотрудничество">Сотрудничество</option>
                    <option value="Другое">Другое</option>
                  </select>
                </div>
              </div>
              <div class="mb-3">
                <label for="message" class="form-label">Сообщение <span class="text-danger">*</span></label>
                <textarea class="form-control" id="message" rows="4" v-model="formData.message" required
                          placeholder="Введите ваше сообщение"></textarea>
              </div>
              <div class="mb-3 form-check">
                <input type="checkbox" class="form-check-input" id="agreement" v-model="formData.agreement" required>
                <label class="form-check-label" for="agreement">Я согласен на обработку моих персональных данных</label>
              </div>
              <div class="text-end">
                <button type="submit" class="btn btn-primary submit-button" 
                        :disabled="!isFormValid || isSubmitting">
                  <span v-if="isSubmitting">
                    <span class="spinner-border spinner-border-sm me-2" role="status" aria-hidden="true"></span>
                    Отправка...
                  </span>
                  <span v-else>
                    <i class="bi bi-send-fill me-2"></i>Отправить сообщение
                  </span>
                </button>
              </div>
            </form>
          </div>
        </div>
      </div>
    </div>
    
    <div class="mt-5">
      <div class="map-card">
        <div class="map-header">
          <i class="bi bi-map-fill"></i>
          <h3>Как нас найти</h3>
        </div>
        <div class="map-body">
          <div class="map-container">
               <iframe src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d1996.546978300558!2d30.320805303303768!3d59.97283785840227!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x46963159039c2179%3A0x13140cb02ad789b2!2z0JzRg9C30LXQuSDQuNGB0YLQvtGA0LjQuCDQodCf0LHQk9Ct0KLQoyDCq9Cb0K3QotCYwrs!5e0!3m2!1sru!2sru!4v1742913369909!5m2!1sru!2sru"
               width="100%" height="350" style="border:0;" allowfullscreen="" loading="lazy"></iframe>           
          </div>
          <div class="map-info mt-3">
            <h5>Как добраться до музея:</h5>
            <div class="row">
              <div class="col-md-6">
                <div class="transport-info">
                  <i class="bi bi-subway"></i>
                  <div>
                    <p><strong>Метро:</strong> Петроградская (15 минут пешком)</p>
                  </div>
                </div>
              </div>
              <div class="col-md-6">
                <div class="transport-info">
                  <i class="bi bi-bus-front"></i>
                  <div>
                    <p><strong>Автобусы:</strong> 128, 185, 249, 275 (остановка "Улица Профессора Попова")</p>
                    <p><strong>Троллейбусы:</strong> 31 (остановка "Улица Профессора Попова")</p>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: 'ContactsPage',
  data() {
    return {
      formData: {
        name: '',
        email: '',
        phone: '',
        subject: '',
        message: '',
        agreement: false
      },
      isSubmitting: false,
      submitSuccess: false,
      submitError: false
    }
  },
  computed: {
    isFormValid() {
      return this.formData.name && 
             this.formData.email && 
             this.formData.subject && 
             this.formData.message && 
             this.formData.agreement
    }
  },
  methods: {
    submitForm() {
      if (this.isFormValid) {
        this.isSubmitting = true
        
        setTimeout(() => {
          this.isSubmitting = false
          this.submitSuccess = true
          
          this.formData = {
            name: '',
            email: '',
            phone: '',
            subject: '',
            message: '',
            agreement: false
          }
          
          alert('Спасибо! Ваше сообщение успешно отправлено. Мы свяжемся с вами в ближайшее время.')
        }, 1500)
      }
    }
  }
}
</script>

<style scoped>
.contacts-intro {
  background-color: rgba(255, 255, 255, 0.7) !important;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.05);
  border-radius: 8px;
}

.contact-info-card, .contact-form-card, .map-card {
  background-color: white;
  border-radius: 12px;
  overflow: hidden;
  box-shadow: 0 5px 15px rgba(0, 0, 0, 0.08);
  height: 100%;
  transition: all 0.3s ease;
}

.contact-info-card:hover, .contact-form-card:hover, .map-card:hover {
  box-shadow: 0 8px 25px rgba(0, 0, 0, 0.12);
  transform: translateY(-5px);
}

.contact-info-header, .contact-form-header, .map-header {
  background-color: #395eab;
  color: white;
  padding: 20px;
  display: flex;
  align-items: center;
}

.contact-info-header i, .contact-form-header i, .map-header i {
  font-size: 24px;
  margin-right: 15px;
}

.contact-info-header h3, .contact-form-header h3, .map-header h3 {
  margin: 0;
  font-size: 22px;
  font-weight: 600;
}

.contact-info-body, .contact-form-body, .map-body {
  padding: 20px;
}

.contact-item {
  display: flex;
  margin-bottom: 20px;
  align-items: flex-start;
}

.contact-item i {
  font-size: 20px;
  color: #395eab;
  margin-right: 15px;
  margin-top: 3px;
}

.contact-item h5 {
  margin: 0 0 5px 0;
  font-size: 16px;
  font-weight: 600;
  color: #343a40;
}

.contact-item p {
  margin: 0;
  color: #495057;
}

.contact-item a {
  color: #395eab;
  text-decoration: none;
  transition: color 0.3s ease;
}

.contact-item a:hover {
  color: #2a4494;
  text-decoration: underline;
}

.social-media h5 {
  margin-bottom: 15px;
  font-size: 16px;
  font-weight: 600;
  color: #343a40;
}

.social-icons {
  display: flex;
  gap: 10px;
}

.social-icon {
  display: flex;
  align-items: center;
  justify-content: center;
  width: 40px;
  height: 40px;
  border-radius: 50%;
  background-color: #395eab;
  color: white;
  font-size: 20px;
  transition: all 0.3s ease;
}

.social-icon:hover {
  background-color: #2a4494;
  transform: translateY(-3px);
}

.form-label {
  font-weight: 500;
  color: #343a40;
}

.input-group-text {
  background-color: #395eab;
  color: white;
  border: none;
}

.form-control, .form-select {
  padding: 10px 15px;
  border: 1px solid #ced4da;
  border-radius: 0 6px 6px 0;
}

.form-control:focus, .form-select:focus {
  border-color: #395eab;
  box-shadow: 0 0 0 0.25rem rgba(57, 94, 171, 0.25);
}

textarea.form-control {
  border-radius: 6px;
}

.form-check-input:checked {
  background-color: #395eab;
  border-color: #395eab;
}

.submit-button {
  padding: 10px 20px;
  border-radius: 30px;
  font-weight: 500;
  background-color: #395eab;
  border-color: #395eab;
  transition: all 0.3s ease;
}

.submit-button:hover:not(:disabled) {
  background-color: #2a4494;
  border-color: #2a4494;
  transform: translateY(-3px);
}

.map-container {
  border-radius: 8px;
  overflow: hidden;
}

.map-info {
  color: #495057;
}

.map-info h5 {
  font-size: 18px;
  font-weight: 600;
  color: #343a40;
  margin-bottom: 15px;
}

.transport-info {
  display: flex;
  align-items: center;
  margin-bottom: 10px;
}

.transport-info i {
  font-size: 20px;
  color: #395eab;
  margin-right: 15px;
}

.transport-info p {
  margin: 0;
}
</style> 