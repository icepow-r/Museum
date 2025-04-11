<template>
  <div>
    <div class="title-block">
      <h2><i class="bi bi-flag-fill me-2"></i>Традиции и символы ЛЭТИ</h2>
    </div>

    <div class="traditions-intro text-center mb-4 p-3 bg-light rounded">
      <p class="lead mb-0">Познакомьтесь с уникальными традициями, символикой и ритуалами, формирующими идентичность Электротехнического университета на протяжении его богатой истории</p>
    </div>

    <div class="row g-4">
      <div class="col-12 mb-3">
        <div class="section-card" id="symbols">
          <div class="section-header">
            <i class="bi bi-shield-fill"></i>
            <h3>Символы ЛЭТИ</h3>
          </div>
          <div class="section-body">
            <div class="row row-cols-1 row-cols-md-2 g-4">
              <div class="col">
                <div class="symbol-card">
                  <div class="symbol-image">
                    <div class="placeholder-image d-flex justify-content-center align-items-center bg-light">
                      <img src="@\assets\flag.png" alt="FLAG"> 
                    </div>
                  </div>
                  <div class="symbol-content">
                    <h4>Флаг университета</h4>
                    <p>Флаг университета представляет собой синее полотнище с изображением герба в центре. Синий цвет символизирует преданность науке и стремление к знаниям. Флаг поднимается во время важных университетских событий и мероприятий.</p>
                  </div>
                </div>
              </div>
              <div class="col">
                <div class="symbol-card">
                  <div class="symbol-image">
                    <div class="placeholder-image d-flex justify-content-center align-items-center bg-light">
                      <i class="bi bi-music-note-beamed fs-1 text-primary"></i>
                    </div>
                  </div>
                  <div class="symbol-content">
                    <h4>Гимн СПбГЭТУ «ЛЭТИ»</h4>
                    <p>Гимн университета исполняется на всех значимых мероприятиях, включая день первокурсника и выпускные церемонии. Слова гимна отражают историю университета, его ценности и устремления. Мелодия была создана специально для ЛЭТИ и стала важной частью университетских традиций.</p>
                    <div class="d-flex mt-2">
                      <button class="btn btn-sm btn-outline-primary me-2" @click="playHymn">
                        <i class="bi" :class="isHymnPlaying ? 'bi-pause-circle' : 'bi-play-circle'"></i>
                        {{ isHymnPlaying ? 'Приостановить' : 'Прослушать гимн' }}
                      </button>
                      <button class="btn btn-sm btn-outline-secondary" @click="showHymnText">
                        <i class="bi bi-file-text me-1"></i>Текст
                      </button>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>

      <div class="col-12">
        <div class="section-card" id="quotes">
          <div class="section-header">
            <i class="bi bi-chat-quote-fill"></i>
            <h3>Известные высказывания о ЛЭТИ</h3>
          </div>
          <div class="section-body">
            <div class="quotes-container">
              <div class="quote-card">
                <div class="quote-text">
                  <i class="bi bi-quote"></i>
                  <p>Сегодня ЛЭТИ является одним из ведущих вузов России, входит в престижные международные рейтинги. Мы осознаем и берем на себя ответственность за кадровое обеспечение и научно-технологическое развитие региона и профильных отраслей. Университет является лидером в разработке инновационных технологий и готовит инженеров нового поколения для цифровой экономики России.</p>
                </div>
                <div class="quote-author">
                
                  <div>
                    <strong>Виктор Шелудько, ректор СПбГЭТУ "ЛЭТИ"</strong>
                  </div>
                </div>
              </div>
              <div class="quote-card">
                <div class="quote-text">
                  <i class="bi bi-quote"></i>
                  <p>Кто-то считает, что у ЛЭТИ есть свой ангел-хранитель. Возможно. Но известная русская поговорка учит на Бога надеяться, но самим не плошать. А коллектив нашего университета очень даже «не плошал», нам всем есть чем гордиться. И это – оправдавшиеся ожидания, а, точнее, стопроцентная уверенность в могучем потенциале нашего вуза!</p>
                </div>
                <div class="quote-author">
                  <div>
                    <strong>Владимир Кутузов, профессор</strong>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>

    <div class="modal fade" id="hymnModal" tabindex="-1" aria-labelledby="hymnModalLabel" aria-hidden="true">
      <div class="modal-dialog">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title" id="hymnModalLabel">Текст гимна ЛЭТИ</h5>
            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Закрыть"></button>
          </div>
          <div class="modal-body">
            <pre class="hymn-text">{{ hymnText }}</pre>
          </div>
          <div class="modal-footer">
            <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Закрыть</button>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import hymn from '@/assets/LETI GIMN.mp3';
import * as bootstrap from 'bootstrap';

export default {
  name: 'TraditionsPage',
  data() {
    return {
      isHymnPlaying: false,
      activeAccordion: null,
      audioPlayer: null,
      showHymnModal: false,
      hymnText: `Слова Владимира Малиновского, музыка Якова Дубравина

ЛЭТИ, ЛЭТИ! Ты - славный сын России,
Науки и культуры светлый храм,
Высоких знаний факел негасимый,
Дающий вдохновение трудам!

Припев:
Славься, наш магический,
Электротехнический
Первенец Отчизны,
Гордость нашей жизни!
Ты - обитель светлых муз, 
Славься, наш любимый вуз, 
Альма-матер!

Нам прошлое сегодня помогает, 
Наш путь всегда к вершинам устремлен, 
Дорогу в завтра юность пробивает 
Под звездами блистательных имен!

Припев

Прекрасен перекресток наших судеб, 
Мы здесь смогли призвание найти, 
Пусть наше братство нерушимо будет, 
Да здравствует наш общий дом ЛЭТИ!

Припев`
    }
  },
  methods: {
    playHymn() {
      if (this.isHymnPlaying) {
        this.audioPlayer.pause();
        this.isHymnPlaying = false;
        return;
      }
      
      if (this.audioPlayer) {
        this.audioPlayer.play();
        this.isHymnPlaying = true;
        return;
      }
      
      this.audioPlayer = new Audio(hymn);
      this.audioPlayer.play();
      this.isHymnPlaying = true;
      this.audioPlayer.addEventListener('ended', () => {
        this.isHymnPlaying = false;
      });
    },
    showHymnText() {
      this.showHymnModal = true;
      this.$nextTick(() => {
        const modal = new bootstrap.Modal(document.getElementById('hymnModal'));
        modal.show();
      });
    },
    backToTraditionsList() {
      if (this.activeAccordion) {
        const collapseElement = document.getElementById(this.activeAccordion);
        const bsCollapse = bootstrap.Collapse.getInstance(collapseElement);
        if (bsCollapse) {
          bsCollapse.hide();
        }
        this.activeAccordion = null;
      }
    },
    setActiveAccordion(id) {
      this.activeAccordion = id;
    }
  },
  mounted() {
    document.querySelectorAll('.accordion-collapse').forEach(collapse => {
      collapse.addEventListener('show.bs.collapse', () => {
        this.setActiveAccordion(collapse.id);
      });
      
      collapse.addEventListener('hidden.bs.collapse', () => {
        if (this.activeAccordion === collapse.id) {
          this.activeAccordion = null;
        }
      });
    });
  }
}
</script>

<style scoped>
.traditions-intro {
  background-color: rgba(255, 255, 255, 0.7) !important;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.05);
  border-radius: 8px;
}

.section-card {
  background-color: white;
  border-radius: 12px;
  overflow: hidden;
  box-shadow: 0 5px 15px rgba(0, 0, 0, 0.08);
  transition: all 0.3s ease;
  height: 100%;
}

.section-card:hover {
  box-shadow: 0 10px 25px rgba(0, 0, 0, 0.12);
  transform: translateY(-5px);
}

.section-header {
  background-color: #395eab;
  color: white;
  padding: 15px 20px;
  display: flex;
  align-items: center;
}

.section-header i {
  font-size: 24px;
  margin-right: 15px;
}

.section-header h3 {
  margin: 0;
  font-size: 20px;
  font-weight: 600;
}

.section-body {
  padding: 20px;
}

.symbol-card {
  border-radius: 10px;
  overflow: hidden;
  box-shadow: 0 4px 10px rgba(0, 0, 0, 0.05);
  transition: all 0.3s ease;
  height: 100%;
  border: 1px solid #e9ecef;
}

.symbol-card:hover {
  transform: translateY(-5px);
  box-shadow: 0 8px 15px rgba(0, 0, 0, 0.1);
}

.symbol-image {
  height: 180px;
  overflow: hidden;
}

.symbol-image img {
  width: 100%;
  height: 100%;
  object-fit: cover;
  transition: transform 0.5s ease;
}

.symbol-card:hover .symbol-image img {
  transform: scale(1.1);
}

.symbol-content {
  padding: 15px;
}

.symbol-content h4 {
  margin-top: 0;
  margin-bottom: 10px;
  font-size: 18px;
  font-weight: 600;
  color: #395eab;
}

.symbol-content p {
  font-size: 0.9rem;
  color: #555;
  margin-bottom: 10px;
}

.accordion-item {
  border: none;
  margin-bottom: 10px;
}

.accordion-button {
  padding: 15px;
  font-weight: 500;
  font-size: 16px;
  color: #343a40;
  background-color: #f8f9fa;
  border-radius: 6px !important;
  box-shadow: none !important;
}

.accordion-button:not(.collapsed) {
  color: #395eab;
  background-color: #e7f1ff;
}

.accordion-button:focus {
  box-shadow: none;
  border-color: rgba(57, 94, 171, 0.25);
}

.accordion-button::after {
  background-size: 16px;
  width: 16px;
  height: 16px;
}

.accordion-body {
  padding: 15px;
  background-color: #f8f9fa;
  border-radius: 0 0 6px 6px;
}

.tradition-item {
  font-size: 0.95rem;
  color: #555;
}

.tradition-item img {
  width: 100%;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
}

.ritual-list {
  display: flex;
  flex-direction: column;
  gap: 20px;
}

.ritual-item {
  display: flex;
  align-items: flex-start;
  margin-bottom: 24px;
  padding-bottom: 24px;
  border-bottom: 1px solid rgba(0, 0, 0, 0.05);
  position: relative;
  overflow: hidden;
}

.ritual-item:last-child {
  margin-bottom: 0;
  padding-bottom: 0;
  border-bottom: none;
}

.ritual-icon {
  flex-shrink: 0;
  margin-right: 16px;
  display: flex;
  align-items: center;
  justify-content: center;
  width: 60px;
}

.ritual-icon .placeholder-image {
  width: 50px;
  height: 50px;
  display: flex;
  align-items: center;
  justify-content: center;
}

.ritual-content {
  flex: 1;
}

.ritual-content h4 {
  font-size: 1.1rem;
  margin-bottom: 10px;
  color: #395eab;
  font-weight: 600;
}

.ritual-content p {
  font-size: 0.95rem;
  line-height: 1.5;
  color: #555;
}

.quotes-container {
  display: flex;
  flex-wrap: wrap;
  gap: 20px;
}

.quote-card {
  flex: 1 1 45%;
  min-width: 300px;
  background-color: #f8f9fa;
  border-radius: 10px;
  padding: 20px;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.05);
  transition: all 0.3s ease;
}

.quote-card:hover {
  background-color: #e7f1ff;
  transform: translateY(-5px);
}

.quote-text {
  position: relative;
  padding-left: 30px;
  margin-bottom: 15px;
}

.quote-text i {
  position: absolute;
  left: 0;
  top: 0;
  font-size: 24px;
  color: #395eab;
  opacity: 0.5;
}

.quote-text p {
  font-style: italic;
  color: #555;
  line-height: 1.6;
}

.quote-author {
  display: flex;
  align-items: center;
}

.quote-author-image {
  width: 45px;
  height: 45px;
  border-radius: 50%;
  object-fit: cover;
  margin-right: 15px;
}

.quote-author strong {
  display: block;
  color: #343a40;
}

.quote-author small {
  color: #6c757d;
}

@media (max-width: 768px) {
  .quotes-container {
    flex-direction: column;
  }
  
  .quote-card {
    flex: 1 1 100%;
  }
}

.placeholder-image {
  width: 100%;
  height: 180px;
  border-radius: 4px;
}

.rounded-circle.placeholder-image {
  width: 45px;
  height: 45px;
}

.hymn-text {
  white-space: pre-wrap;
  font-family: inherit;
  color: #333;
  background: none;
  border: none;
  padding: 0;
  font-size: 1rem;
  line-height: 1.5;
}
</style> 