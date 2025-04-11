<template>
  <div v-if="exhibit" ref="exhibitDetail">
    <div class="title-block">
      <h2>{{ exhibit.title }}</h2>
    </div>
    
    <div class="card mb-4">
      <div class="row g-0">
        <div class="col-md-5">
          <div class="exhibit-detail-image-container">
            <img :src="exhibit.image" class="img-fluid" :alt="exhibit.title">
          </div>
        </div>
        <div class="col-md-7">
          <div class="card-body">
            <h3 class="card-title">{{ exhibit.title }}</h3>
            <div class="exhibit-description">
              <p v-for="(paragraph, index) in formattedDescription" :key="index" class="card-text fs-5">
                {{ paragraph }}
              </p>
            </div>
          </div>
        </div>
      </div>
    </div>
    
    <router-link to="/exhibits" class="btn auth-btn">
      Назад к экспонатам
    </router-link>
  </div>
  <div v-else class="alert alert-warning">
    Информация об экспонате не найдена.
  </div>
</template>

<script>
export default {
  name: 'ExhibitDetailPage',
  data() {
    return {
      exhibits: [
        {
          id: 'radio-receiver',
          title: 'Первый радиоприемник А.С. Попова',
          shortDescription: 'Копия первого в мире радиоприемника, созданного А.С. Поповым в 1895 году.',
          image: '/src/assets/images/exhibits/radio-receiver.jpg',
          fullDescription: 'Первый в мире радиоприемник был создан русским ученым Александром Степановичем Поповым в 1895 году. Этот прибор смог принять и зарегистрировать радиосигналы. Изначально он использовался для обнаружения гроз, но вскоре был адаптирован для передачи информации. 7 мая 1895 года Попов продемонстрировал свое изобретение на заседании Русского физико-химического общества. Этот день считается днем рождения радио.'
        },
        {
          id: 'first-computer',
          title: 'Компьютер "ЛЭТИ-1"',
          shortDescription: 'Один из первых компьютеров, разработанных в ЛЭТИ в 1960-х годах.',
          image: '/src/assets/images/exhibits/computer.jpg',
          fullDescription: 'Компьютер "ЛЭТИ-1" был разработан в Ленинградском электротехническом институте в 1960-х годах. Он представлял собой одну из первых отечественных ЭВМ, созданных на полупроводниковых элементах. Компьютер использовался для научных исследований и обучения студентов основам программирования и вычислительной техники. "ЛЭТИ-1" занимал целую комнату и имел оперативную память объемом всего несколько килобайт, но по тем временам это было передовое техническое устройство.'
        },
        {
          id: 'vacuum-tube',
          title: 'Коллекция радиоламп',
          shortDescription: 'Коллекция радиоламп разных периодов, используемых в радиотехнических устройствах.',
          image: '/src/assets/images/exhibits/vacuum-tube.jpg',
          fullDescription: 'Коллекция радиоламп музея ЛЭТИ включает образцы различных периодов развития радиотехники - от первых диодов и триодов начала XX века до сложных многоэлектродных ламп середины XX века. Радиолампы были основным активным элементом радиотехнических устройств до появления полупроводниковых транзисторов. Коллекция демонстрирует эволюцию технологии производства радиоламп, изменение их конструкции и характеристик. Среди экспонатов есть уникальные образцы, созданные в лабораториях ЛЭТИ.'
        },
        {
          id: 'oscilloscope',
          title: 'Первые осциллографы',
          shortDescription: 'Коллекция ранних осциллографов, использованных в ЛЭТИ для научных исследований.',
          image: '/src/assets/images/exhibits/oscilloscope.jpg',
          fullDescription: 'В коллекции музея ЛЭТИ представлены образцы первых осциллографов, использовавшихся в лабораториях института с 1930-х годов. Осциллографы - это приборы для визуального наблюдения и измерения электрических сигналов. Они сыграли важную роль в развитии радиотехники, электроники и систем связи. Представленные в музее образцы демонстрируют эволюцию этих измерительных приборов от простых механических устройств до сложных электронных систем.'
        },
        {
          id: 'telegraph',
          title: 'Телеграфный аппарат Морзе',
          shortDescription: 'Оригинальный телеграфный аппарат системы Морзе, использовавшийся для обучения студентов.',
          image: '/src/assets/images/exhibits/telegraph.jpg',
          fullDescription: 'Телеграфный аппарат системы Морзе, представленный в музее ЛЭТИ, использовался для обучения студентов в конце XIX - начале XX века. Аппарат позволял передавать сообщения с помощью кода Морзе - системы кодирования букв и цифр с помощью комбинаций коротких и длинных сигналов. Телеграфия была первым видом электрической связи на расстоянии и предшественницей современных телекоммуникаций. Этот экспонат иллюстрирует важный этап в развитии систем связи и подготовке специалистов в ЛЭТИ.'
        },
        {
          id: 'semiconductor',
          title: 'Первые полупроводниковые приборы',
          shortDescription: 'Коллекция ранних полупроводниковых устройств, созданных в ЛЭТИ.',
          image: '/src/assets/images/exhibits/semiconductor.jpg',
          fullDescription: 'В музее ЛЭТИ представлена коллекция первых полупроводниковых приборов, разработанных и созданных в лабораториях института в 1950-1960-х годах. Эта коллекция включает первые диоды, транзисторы и интегральные схемы, которые ознаменовали новую эру в развитии электроники. Полупроводниковые приборы пришли на смену радиолампам, что позволило значительно уменьшить размеры электронных устройств, снизить их энергопотребление и повысить надежность. Многие из представленных образцов являются прототипами, которые в дальнейшем послужили основой для промышленного производства.'
        }
      ]
    }
  },
  computed: {
    exhibit() {
      return this.exhibits.find(e => e.id === this.$route.params.id)
    },
    formattedDescription() {
      if (this.exhibit && this.exhibit.fullDescription) {
        const paragraphs = this.exhibit.fullDescription
          .split(/\n|-\s/)
          .filter(paragraph => paragraph.trim() !== '');
        return paragraphs;
      }
      return [];
    }
  }
}
</script>

<style scoped>
.exhibit-detail-image-container {
  position: relative;
  overflow: hidden;
  padding-top: 75%;
  height: 0;
}

.exhibit-detail-image-container img {
  position: absolute;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  object-fit: cover;
}

.exhibit-description p {
  margin-bottom: 1rem;
  line-height: 1.6;
  text-align: justify;
}

.exhibit-description p:last-child {
  margin-bottom: 0;
}

.title-block {
  margin-bottom: 2rem;
  position: relative;
}

.title-block h2 {
  font-weight: 700;
  color: #395eab;
  margin-bottom: 0.5rem;
  position: relative;
  display: inline-block;
}

.title-block h2:after {
  content: '';
  position: absolute;
  bottom: -8px;
  left: 0;
  width: 60px;
  height: 3px;
  background-color: #395eab;
}

.card {
  border: none;
  border-radius: 10px;
  overflow: hidden;
  box-shadow: 0 5px 15px rgba(0, 0, 0, 0.08);
  transition: all 0.3s ease;
}

.card:hover {
  box-shadow: 0 8px 25px rgba(0, 0, 0, 0.12);
}

.card-title {
  color: #395eab;
  font-weight: 600;
  margin-bottom: 1.25rem;
}

.card-body {
  padding: 1.5rem;
}

.btn.auth-btn {
  background-color: #395eab;
  color: white;
  padding: 8px 20px;
  border: none;
  transition: all 0.3s ease;
}

.btn.auth-btn:hover {
  background-color: #2c4a8a;
}

@media (max-width: 768px) {
  .card {
    margin: 0 -12px;
    border-radius: 0;
  }
  
  .card:hover {
    box-shadow: 0 5px 15px rgba(0, 0, 0, 0.08);
    transform: none;
  }
  
  .card-body {
    padding: 1rem;
  }
}
</style> 