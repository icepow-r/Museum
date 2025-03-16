<!-- src/components/home/NotablePersons.vue -->
<template>
    <section class="py-5">
      <h2 class="mb-4 text-primary">Выдающиеся личности</h2>
      <div class="position-relative">
        <button class="btn btn-primary position-absolute top-50 start-0 translate-middle-y z-index-1" @click="prevPerson">
          <i class="bi bi-arrow-left"></i>
        </button>
        <div class="persons-container overflow-hidden">
          <div class="persons-slider d-flex" :style="{ transform: `translateX(-${personIndex * 100}%)` }">
            <div v-for="(person, index) in notablePersons" :key="index" class="person-card w-100">
              <div class="card border-0 shadow">
                <div class="row g-0">
                  <div class="col-md-4">
                    <img :src="`/api/placeholder/300/400`" :alt="person.name" class="img-fluid rounded-start">
                  </div>
                  <div class="col-md-8">
                    <div class="card-body">
                      <h3 class="card-title text-primary">{{ person.name }}</h3>
                      <p class="card-text text-muted">{{ person.years }}</p>
                      <p class="card-text">{{ person.description }}</p>
                      <button class="btn btn-outline-primary btn-sm">Подробнее</button>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
        <button class="btn btn-primary position-absolute top-50 end-0 translate-middle-y z-index-1" @click="nextPerson">
          <i class="bi bi-arrow-right"></i>
        </button>
        <div class="text-center mt-4">
          <div class="pagination-dots">
            <span 
              v-for="(_, index) in notablePersons" 
              :key="index" 
              :class="['pagination-dot', {'active': index === personIndex}]"
              @click="personIndex = index"
            ></span>
          </div>
        </div>
      </div>
    </section>
  </template>
  
  <script>
  export default {
    name: 'NotablePersons',
    data() {
      return {
        personIndex: 0,
        notablePersons: [
          {
            name: 'Александр Степанович Попов',
            years: '1859-1906',
            description: 'Русский физик и электротехник, изобретатель радио. Профессор и первый выборный директор электротехнического института.'
          },
          {
            name: 'Жорес Иванович Алфёров',
            years: '1930-2019',
            description: 'Советский и российский физик, лауреат Нобелевской премии по физике 2000 года за разработку полупроводниковых гетероструктур.'
          },
          {
            name: 'Владимир Иванович Сифоров',
            years: '1904-1993',
            description: 'Советский радиотехник, академик АН СССР, создатель научной школы статистической радиотехники.'
          },
          {
            name: 'Акселерод Дмитрий Соломонович',
            years: '1898-1954',
            description: 'Советский физик и радиотехник, один из основоположников электровакуумной промышленности СССР.'
          }
        ]
      }
    },
    methods: {
      prevPerson() {
        if (this.personIndex > 0) {
          this.personIndex--;
        } else {
          this.personIndex = this.notablePersons.length - 1;
        }
      },
      nextPerson() {
        if (this.personIndex < this.notablePersons.length - 1) {
          this.personIndex++;
        } else {
          this.personIndex = 0;
        }
      }
    }
  }
  </script>
  
  <style scoped>
  .persons-container {
    overflow: hidden;
    margin: 0 50px;
  }
  
  .persons-slider {
    transition: transform 0.5s ease;
  }
  
  .person-card {
    flex: 0 0 100%;
    padding: 15px;
  }
  
  .pagination-dots {
    display: flex;
    justify-content: center;
    gap: 8px;
    margin-top: 15px;
  }
  
  .pagination-dot {
    display: inline-block;
    width: 12px;
    height: 12px;
    border-radius: 50%;
    background-color: #d0d0d0;
    cursor: pointer;
    transition: background-color 0.3s;
  }
  
  .pagination-dot.active {
    background-color: var(--bs-primary);
  }
  
  .z-index-1 {
    z-index: 1;
  }
  </style>