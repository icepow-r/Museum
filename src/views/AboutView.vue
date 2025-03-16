<!-- src/views/AboutView.vue -->
<template>
    <div>
      <div class="page-header bg-primary text-white py-5">
        <div class="container">
          <h1 class="display-4">История ЛЭТИ</h1>
          <p class="lead">Ознакомьтесь с богатой историей старейшего электротехнического университета Европы</p>
        </div>
      </div>
      
      <div class="container py-5">
        <div class="row mb-5">
          <div class="col-md-8">
            <h2 class="mb-4">Основание и первые годы</h2>
            <p>Санкт-Петербургский государственный электротехнический университет «ЛЭТИ» был основан в 1886 году как Техническое училище почтово-телеграфного ведомства. Инициатором создания был выдающийся ученый Александр Степанович Попов, изобретатель радио.</p>
            
            <p>В 1891 году училище было преобразовано в Электротехнический институт. Это было первое в Европе высшее учебное заведение электротехнического профиля.</p>
            
            <h2 class="mb-4 mt-5">Вклад в науку и технику</h2>
            <p>Именно в стенах ЛЭТИ А.С. Попов провел свои знаменитые эксперименты, которые привели к изобретению радио. 7 мая 1895 года он продемонстрировал первый в мире радиоприемник, что стало началом новой эры в развитии техники связи.</p>
          </div>
          <div class="col-md-4">
            <div class="card border-0 shadow-sm">
              <div class="card-body">
                <h5 class="card-title text-primary">Основные даты</h5>
                <ul class="timeline-list">
                  <li><span class="fw-bold">1886</span> - Основание Технического училища</li>
                  <li><span class="fw-bold">1891</span> - Преобразование в Электротехнический институт</li>
                  <li><span class="fw-bold">1899</span> - Демонстрация радиоприемника</li>
                  <li><span class="fw-bold">1905</span> - Присвоение имени Александра III</li>
                  <li><span class="fw-bold">1921</span> - Переименование в ЛЭТИ</li>
                  <li><span class="fw-bold">1992</span> - Современное название СПбГЭТУ «ЛЭТИ»</li>
                </ul>
              </div>
            </div>
          </div>
        </div>
        
        <!-- Здесь будет подробное содержание истории университета -->
        <div class="placeholder-content text-center p-5 bg-light rounded">
          <i class="bi bi-journals fs-1 text-primary"></i>
          <p class="mt-3">Здесь будет размещено полное содержание об истории университета ЛЭТИ</p>
        </div>
      </div>
    </div>
  </template>
  
  <script>
  export default {
    name: 'AboutView'
  }
  </script>
  
  <style scoped>
  .page-header {
    margin-top: 56px;
  }
  
  .timeline-list {
    list-style: none;
    padding-left: 0;
  }
  
  .timeline-list li {
    position: relative;
    padding-bottom: 15px;
    padding-left: 25px;
  }
  
  .timeline-list li:before {
    content: "";
    position: absolute;
    left: 0;
    top: 8px;
    width: 10px;
    height: 10px;
    border-radius: 50%;
    background: var(--bs-primary);
  }
  
  .timeline-list li:after {
    content: "";
    position: absolute;
    left: 4px;
    top: 18px;
    height: calc(100% - 13px);
    width: 2px;
    background: var(--bs-primary);
  }
  
  .timeline-list li:last-child:after {
    display: none;
  }
  </style>