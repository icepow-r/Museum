<!-- src/components/home/TimelineSlider.vue -->
<template>
    <section class="py-5">
      <h2 class="mb-4 text-primary">Историческая хронология</h2>
      <div class="position-relative">
        <button class="btn btn-primary position-absolute top-50 start-0 translate-middle-y" @click="scrollTimelineLeft">
          <i class="bi bi-arrow-left"></i>
        </button>
        <div class="timeline-container" ref="timelineContainer">
          <div class="timeline-scroll d-flex">
            <div v-for="(event, index) in timelineEvents" :key="index" class="timeline-item p-3 text-center">
              <div class="timeline-year fw-bold fs-4 text-primary">{{ event.year }}</div>
              <div class="timeline-content bg-light p-3 rounded shadow-sm">
                <h5>{{ event.title }}</h5>
                <p>{{ event.description }}</p>
              </div>
            </div>
          </div>
        </div>
        <button class="btn btn-primary position-absolute top-50 end-0 translate-middle-y" @click="scrollTimelineRight">
          <i class="bi bi-arrow-right"></i>
        </button>
      </div>
    </section>
  </template>
  
  <script>
  export default {
    name: 'TimelineSlider',
    data() {
      return {
        timelineEvents: [
          { year: '1886', title: 'Основание', description: 'Создание Технического училища почтово-телеграфного ведомства' },
          { year: '1891', title: 'Электротехнический институт', description: 'Преобразование в Электротехнический институт' },
          { year: '1899', title: 'Изобретение радио', description: 'А.С. Попов демонстрирует первый в мире радиоприемник' },
          { year: '1905', title: 'Присвоение имени', description: 'Институту присвоено имя императора Александра III' },
          { year: '1921', title: 'Советское время', description: 'Переименование в Ленинградский электротехнический институт' },
          { year: '1941-1945', title: 'Война', description: 'Институт во время Великой Отечественной войны' },
          { year: '1992', title: 'Современность', description: 'Переименование в Санкт-Петербургский государственный электротехнический университет' },
          { year: '2015', title: 'Новые достижения', description: 'Включение в программу повышения конкурентоспособности российских вузов' }
        ]
      }
    },
    methods: {
      scrollTimelineLeft() {
        if (this.$refs.timelineContainer) {
          this.$refs.timelineContainer.scrollBy({ left: -300, behavior: 'smooth' });
        }
      },
      scrollTimelineRight() {
        if (this.$refs.timelineContainer) {
          this.$refs.timelineContainer.scrollBy({ left: 300, behavior: 'smooth' });
        }
      }
    }
  }
  </script>
  
  <style scoped>
  .timeline-container {
    overflow-x: auto;
    scrollbar-width: none;
    -ms-overflow-style: none;
    margin: 0 50px;
  }
  
  .timeline-container::-webkit-scrollbar {
    display: none;
  }
  
  .timeline-scroll {
    padding: 20px 0;
  }
  
  .timeline-item {
    min-width: 250px;
    margin-right: 20px;
  }
  
  .timeline-year {
    position: relative;
    margin-bottom: 15px;
  }
  
  .timeline-year:after {
    content: "";
    position: absolute;
    width: 2px;
    height: 15px;
    background-color: var(--bs-primary);
    bottom: -15px;
    left: 50%;
    transform: translateX(-50%);
  }
  </style>