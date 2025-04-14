<template>
  <div>
    <div class="title-block">
      <h2><i class="bi bi-clock-history me-2"></i>История развития ЛЭТИ</h2>
    </div>
    
    <div class="timeline">
      <div class="container">
        <div class="row">
          <div class="col-12">
            <div class="horizontal-timeline-container mb-4">
              <div class="timeline-navigation">
                <button 
                  class="timeline-nav-btn prev-btn" 
                  @click="scrollTimeline('left')" 
                  :disabled="scrollPosition <= 0"
                >
                  <i class="bi bi-chevron-left"></i>
                </button>
                <button 
                  class="timeline-nav-btn next-btn" 
                  @click="scrollTimeline('right')" 
                  :disabled="scrollPosition >= maxScrollPosition"
                >
                  <i class="bi bi-chevron-right"></i>
                </button>
              </div>
              
              <div class="horizontal-timeline-wrapper" ref="timelineWrapper" @scroll="handleScroll">
                <div class="horizontal-timeline">
                  <div class="timeline-track">
                    <div class="timeline-line"></div>
                    <div 
                      v-for="(period, index) in timePeriods" 
                      :key="index" 
                      class="timeline-period" 
                      @click="openPeriodModal(index)"
                    >
                      <div class="timeline-period-node">
                        <div class="timeline-dot"></div>
                        <div class="timeline-period-year">{{ period.range }}</div>
                      </div>
                      <div class="timeline-period-title">{{ period.title }}</div>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>

    <transition name="fade">
      <div class="modal-overlay" v-if="showPeriodModal" @click.self="closePeriodModal">
        <div class="modal-container" :class="{ 'slide-right': showEventDetails }">
          
          <div class="modal-content events-list" :class="{ 'slide-left': showEventDetails }">
            <div class="modal-header">
              <h3 class="modal-title">{{ selectedPeriod ? selectedPeriod.range + ': ' + selectedPeriod.title : '' }}</h3>
              <button class="modal-close-btn" @click="closePeriodModal">✕</button>
            </div>
            <div class="modal-body">
              <div 
                v-for="(event, index) in periodEvents" 
                :key="event.year" 
                class="event-card"
                @click="openEventDetails(event)"
              >
                <div class="event-year">{{ event.year }}</div>
                <div class="event-content">
                  <h4 class="event-title">{{ event.title }}</h4>
                  <p class="event-description">{{ event.description.substring(0, 100) }}{{ event.description.length > 100 ? '...' : '' }}</p>
                </div>
                <div class="event-arrow">
                  <i class="bi bi-chevron-right"></i>
                </div>
              </div>
            </div>
          </div>
          
          <div class="modal-content event-details" :class="{ 'active': showEventDetails }">
            <div class="modal-header">
              <button class="modal-back-btn" @click="closeEventDetails">
                <i class="bi bi-arrow-left"></i> Назад
              </button>
              <button class="modal-close-btn" @click="closePeriodModal">✕</button>
            </div>
            <div class="modal-body" v-if="selectedEvent">
              <h3 class="event-detail-title">{{ selectedEvent.year }}: {{ selectedEvent.title }}</h3>
              <div class="row">
                <div class="col-12">
                  <div class="event-detail-description" v-if="selectedEvent.descriptionHtml" v-html="selectedEvent.descriptionHtml"></div>
                  <div class="event-detail-description" v-else>{{ selectedEvent.description }}</div>
                </div>
              </div>
            </div>
          </div>
          
        </div>
      </div>
    </transition>
  </div>
</template>

<script>
import historyData from '@/assets/history.json';

export default {
  name: 'HistoryPage',
  data() {
    return {
      scrollPosition: 0,
      maxScrollPosition: 0,
      showPeriodModal: false,
      showEventDetails: false,
      selectedPeriodIndex: null,
      selectedEvent: null,
      contentHeight: 0,
      timePeriods: historyData.map(period => ({
        range: period.years,
        title: period.title,
        events: period.events.map(event => event.year)
      })),
      historyEvents: []
    }
  },
  computed: {
    selectedPeriod() {
      return this.selectedPeriodIndex !== null ? this.timePeriods[this.selectedPeriodIndex] : null;
    },
    periodEvents() {
      if (!this.selectedPeriod) return [];
      
      return this.historyEvents.filter(event => 
        this.selectedPeriod.events.includes(event.year)
      );
    }
  },
  methods: {
    scrollTimeline(direction) {
      const scrollAmount = 200;
      const currentScroll = this.$refs.timelineWrapper.scrollLeft;
      
      if (direction === 'left') {
        this.$refs.timelineWrapper.scrollTo({
          left: Math.max(0, currentScroll - scrollAmount),
          behavior: 'smooth'
        });
      } else {
        this.$refs.timelineWrapper.scrollTo({
          left: currentScroll + scrollAmount,
          behavior: 'smooth'
        });
      }
    },
    handleScroll(event) {
      this.scrollPosition = event.target.scrollLeft;
      this.maxScrollPosition = event.target.scrollWidth - event.target.clientWidth;
    },
    updateMaxScrollPosition() {
      if (this.$refs.timelineWrapper) {
        this.maxScrollPosition = this.$refs.timelineWrapper.scrollWidth - this.$refs.timelineWrapper.clientWidth;
      }
    },
    openPeriodModal(index) {
      this.selectedPeriodIndex = index;
      this.showPeriodModal = true;
      this.showEventDetails = false;
      
      document.body.style.overflow = 'hidden';
      
      this.$nextTick(() => {
        this.adjustEventsListSize();
      });
    },
    closePeriodModal() {
      this.showPeriodModal = false;
      this.showEventDetails = false;
      this.selectedEvent = null;
      this.selectedPeriodIndex = null;
      
      document.body.style.overflow = '';
    },
    openEventDetails(event) {
      this.selectedEvent = event;
      this.showEventDetails = true;
      
      this.$nextTick(() => {
        this.adjustModalSize();
      });
    },
    closeEventDetails() {
      this.showEventDetails = false;
      this.selectedEvent = null;
      
      this.$nextTick(() => {
        this.adjustEventsListSize();
      });
    },
    loadHistoryData() {
      this.historyEvents = [];
      historyData.forEach(period => {
        period.events.forEach(event => {
          this.historyEvents.push({
            year: event.year,
            title: event.title,
            description: event.description,
            shortDescription: event.shortDescription
          });
        });
      });
    },
    adjustEventsListSize() {
      const eventsContainer = document.querySelector('.events-list .modal-body');
      if (!eventsContainer) return;
      
      const eventCards = eventsContainer.querySelectorAll('.event-card');
      if (eventCards.length === 0) return;
      
      let totalHeight = 0;
      eventCards.forEach(card => {
        totalHeight += card.offsetHeight + 15;
      });
      
      const headerHeight = 60;
      const padding = 70;
      
  
      const optimalHeight = Math.min(totalHeight + headerHeight + padding, window.innerHeight * 0.8);
      const minHeight = window.innerHeight * 0.45;
      
      let modalHeight = Math.max(optimalHeight, minHeight);
      
      const modalContainer = document.querySelector('.modal-container');
      if (modalContainer) {
        modalContainer.style.height = `${modalHeight}px`;
      }
    },
    adjustModalSize() {
      const detailsContainer = document.querySelector('.event-detail-description');
      if (!detailsContainer) return;
      
      const contentHeight = detailsContainer.scrollHeight;
      const headerHeight = 60;
      const padding = 50;

      const optimalHeight = contentHeight + headerHeight + padding;
      const windowHeight = window.innerHeight;
      
      let modalHeight = Math.max(Math.min(optimalHeight, windowHeight * 0.75), windowHeight * 0.45);
      
      const modalContainer = document.querySelector('.modal-container');
      if (modalContainer) {
        modalContainer.style.height = `${modalHeight}px`;
      }
    }
  },
  mounted() {
    this.loadHistoryData();
    
    this.$nextTick(() => {
      this.updateMaxScrollPosition();
      window.addEventListener('resize', this.updateMaxScrollPosition);
      window.addEventListener('resize', this.adjustModalSize);
      
      const timelineWrapper = this.$refs.timelineWrapper;
      let isDown = false;
      let startX;
      let scrollLeft;
      
      timelineWrapper.addEventListener('mousedown', (e) => {
        isDown = true;
        timelineWrapper.style.cursor = 'grabbing';
        startX = e.pageX - timelineWrapper.offsetLeft;
        scrollLeft = timelineWrapper.scrollLeft;
      });
      
      timelineWrapper.addEventListener('mouseleave', () => {
        isDown = false;
        timelineWrapper.style.cursor = 'grab';
      });
      
      timelineWrapper.addEventListener('mouseup', () => {
        isDown = false;
        timelineWrapper.style.cursor = 'grab';
      });
      
      timelineWrapper.addEventListener('mousemove', (e) => {
        if (!isDown) return;
        e.preventDefault();
        const x = e.pageX - timelineWrapper.offsetLeft;
        const walk = (x - startX) * 2;
        timelineWrapper.scrollLeft = scrollLeft - walk;
      });

      window.addEventListener('keydown', (e) => {
        if (e.key === 'Escape' && this.showPeriodModal) {
          this.closePeriodModal();
        }
      });
    });
  },
  beforeUnmount() {
    window.removeEventListener('resize', this.updateMaxScrollPosition);
    window.removeEventListener('resize', this.adjustModalSize);
    window.removeEventListener('keydown', this.handleKeydown);
    document.body.style.overflow = '';
  }
}
</script>

<style scoped>
.horizontal-timeline-container {
  position: relative;
  margin: 40px 0;
}

.timeline-navigation {
  position: absolute;
  top: 50%;
  width: calc(100% - 80px);
  left: 40px;
  transform: translateY(-50%);
  display: flex;
  justify-content: space-between;
  pointer-events: none;
  z-index: 10;
}

.timeline-nav-btn {
  width: 40px;
  height: 40px;
  border-radius: 50%;
  background-color: rgba(255, 255, 255, 0.9);
  color: #395eab;
  border: 1px solid #d0d9ea;
  display: flex;
  align-items: center;
  justify-content: center;
  cursor: pointer;
  transition: all 0.3s ease;
  pointer-events: auto;
  box-shadow: 0 3px 8px rgba(0, 0, 0, 0.15);
}

.timeline-nav-btn i {
  font-size: 1.2rem;
}

.timeline-nav-btn:hover:not(:disabled) {
  background-color: #395eab;
  color: white;
  border-color: #395eab;
  transform: scale(1.1);
}

.timeline-nav-btn:disabled {
  opacity: 0.4;
  cursor: not-allowed;
  background-color: #e9ecef;
}

.horizontal-timeline-wrapper {
  overflow-x: auto;
  -webkit-overflow-scrolling: touch;
  scrollbar-width: none;
  -ms-overflow-style: none;
  cursor: grab;
  padding: 0 40px;
}

.horizontal-timeline-wrapper::-webkit-scrollbar {
  display: none;
}

.horizontal-timeline {
  padding: 30px 0;
  min-width: 100%;
  display: inline-block;
}

.timeline-track {
  position: relative;
  display: flex;
  min-width: max-content;
  padding: 0 20px;
  align-items: center;
  min-height: 100px;
}

.timeline-line {
  position: absolute;
  top: 50%;
  left: 60px;
  right: 60px;
  height: 5px;
  background: linear-gradient(to right, #d0d9ea, #b3c5e5, #d0d9ea);
  border-radius: 3px;
  transform: translateY(-50%);
  z-index: 1;
}

.timeline-period {
  position: relative;
  z-index: 2;
  display: flex;
  flex-direction: column;
  align-items: center;
  padding: 0 50px;
  cursor: pointer;
  transition: transform 0.3s ease;
  min-width: 180px;
  text-align: center;
  justify-content: center;
  margin-top: 90px;
}

.timeline-period:hover {
  transform: translateY(-8px);
  align-items: center;
  position: relative;
  /* margin-bottom: 15px; */
}

.timeline-period-node {
  display: flex;
  flex-direction: column;
  align-items: center;
  position: relative;
  top: auto;
  margin-top: 0;
}

.timeline-dot {
  width: 24px;
  height: 24px;
  border-radius: 50%;
  background-color: white;
  border: 5px solid #395eab;
  transition: all 0.3s ease;
  box-shadow: 0 0 10px rgba(57, 94, 171, 0.3);
  position: static;
  transform: none;
  z-index: 3;
}

.timeline-period:hover .timeline-dot {
  background-color: rgba(255, 255, 255, 0.8);
  border: 1px solid #d0d9ea;
  margin-top: 0;
  transition: all 0.3s ease;
  display: inline-block;
}

.timeline-period-year {
  font-weight: 700;
  font-size: 1rem;
  color: #395eab;
  padding: 6px 15px;
  border-radius: 20px;
  background-color: rgba(255, 255, 255, 1);
  border: 1px solid #d0d9ea;
  margin-top: 10px;
  transition: all 0.3s ease;
  display: inline-block;
}

.timeline-period:hover .timeline-period-year {
  color: white;
  background-color: #395eab;
  border-color: #395eab;
  transform: scale(1.05);
  justify-content: center;
}

.timeline-period-title {
  font-size: 0.9rem;
  color: #555;
  font-weight: 500;
  max-width: 200px;
  line-height: 1.4;
  min-height: calc(0.9rem * 1.4 * 2);
  display: flex;
  align-items: center;
  justify-content: center;
}

.modal-overlay {
  position: fixed;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  background-color: rgba(0, 0, 0, 0.7);
  display: flex;
  justify-content: center;
  align-items: center;
  z-index: 1000;
  overflow: hidden;
}

.modal-container {
  position: relative;
  width: 90%;
  max-width: 800px;
  max-height: 90vh;
  background-color: white;
  border-radius: 10px;
  box-shadow: 0 10px 30px rgba(0, 0, 0, 0.2);
  overflow: hidden;
  transition: all 0.4s ease;
}

.modal-container.slide-right {
  height: 75vh;
  max-width: 900px;
  min-height: fit-content;
}

.modal-content {
  position: relative;
  display: flex;
  flex-direction: column;
  height: 100%;
  transition: transform 0.4s ease;
}

.modal-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: 15px 20px;
  border-bottom: 1px solid #eee;
  background-color: #f8f9fa;
}

.modal-title {
  margin: 0;
  font-size: 1.25rem;
  font-weight: 600;
  color: #1a2e64;
}

.modal-close-btn {
  background: none;
  border: none;
  font-size: 1.5rem;
  color: #666;
  cursor: pointer;
  transition: color 0.3s;
  display: flex;
  align-items: center;
  justify-content: center;
  width: 30px;
  height: 30px;
  border-radius: 50%;
}

.modal-close-btn:hover {
  color: #dc3545;
  background-color: rgba(0, 0, 0, 0.05);
}

.modal-back-btn {
  background: none;
  border: none;
  display: flex;
  align-items: center;
  color: #395eab;
  font-weight: 500;
  cursor: pointer;
  padding: 5px 10px;
  border-radius: 4px;
  transition: background-color 0.3s;
}

.modal-back-btn:hover {
  background-color: rgba(57, 94, 171, 0.1);
}

.modal-body {
  padding: 20px;
  overflow-y: auto;
  height: calc(66vh - 60px);
  transition: height 0.4s ease;
}

.event-details.active .modal-body {
  height: auto;
  max-height: calc(75vh - 60px);
}

.events-list {
  transition: transform 0.4s ease;
}

.event-card {
  display: flex;
  align-items: center;
  margin-bottom: 15px;
  padding: 15px;
  border-radius: 8px;
  background-color: #f8f9fa;
  cursor: pointer;
  transition: all 0.3s ease;
  box-shadow: 0 2px 5px rgba(0, 0, 0, 0.05);
}

.event-card:hover {
  background-color: #e9ecef;
  transform: translateY(-2px);
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
}

.event-year {
  min-width: 80px;
  padding: 5px 12px;
  background-color: #395eab;
  color: white;
  font-weight: 600;
  text-align: center;
  border-radius: 20px;
  margin-right: 15px;
}

.event-content {
  flex: 1;
}

.event-title {
  margin: 0 0 5px;
  font-size: 1.1rem;
  color: #1a2e64;
}

.event-description {
  margin: 0;
  color: #666;
  font-size: 0.9rem;
}

.event-arrow {
  margin-left: 10px;
  color: #395eab;
  font-size: 1.2rem;
  transition: transform 0.3s;
}

.event-card:hover .event-arrow {
  transform: translateX(3px);
}

.event-details {
  position: absolute;
  top: 0;
  left: 100%;
  width: 100%;
  height: 100%;
  background: white;
  transition: transform 0.4s ease;
}

.event-details.active {
  transform: translateX(-100%);
}

.slide-left {
  transform: translateX(-30%);
}

.slide-right {
  transform: translateX(0);
}

.event-detail-title {
  margin-bottom: 20px;
  color: #1a2e64;
  font-weight: 600;
  font-size: 1.4rem;
  border-bottom: 2px solid #395eab;
  padding-bottom: 10px;
}

.event-image-container {
  border-radius: 8px;
  overflow: hidden;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
}

.event-detail-description {
  line-height: 1.8;
  color: #333;
  font-size: 1.05rem;
  text-align: justify;
  margin-top: 15px;
  padding: 15px;
  background-color: #f9f9f9;
  border-radius: 8px;
  box-shadow: inset 0 0 5px rgba(0, 0, 0, 0.05);
  animation: fadeIn 0.5s ease;
}

@keyframes fadeIn {
  from { opacity: 0; transform: translateY(10px); }
  to { opacity: 1; transform: translateY(0); }
}

.fade-enter-active, .fade-leave-active {
  transition: opacity 0.3s ease;
}

.fade-enter-from, .fade-leave-to {
  opacity: 0;
}

@media (max-width: 768px) {
  .timeline-navigation {
    display: none;
  }

  .horizontal-timeline-wrapper {
    overflow-x: visible;
    cursor: default;
    padding: 0 15px;
  }

  .horizontal-timeline {
    padding: 0;
    display: block;
  }

  .timeline-track {
    flex-direction: column;
    align-items: flex-start;
    min-width: auto;
  }

  .timeline-line {
    top: 40px;
    bottom: 40px;
    left: 83px;
    width: 5px;
    height: auto;
    right: auto;
    background: linear-gradient(to bottom, #d0d9ea, #b3c5e5, #d0d9ea);
    transform: none;
  }

  .timeline-period {
    flex-direction: row;
    align-items: flex-start;
    padding: 25px 0;
    min-width: auto;
    width: 100%;
    text-align: left;
    justify-content: flex-start;
  }
   
  .timeline-period:hover {
    transform: none;
  }

  .timeline-period-node {
    margin-bottom: 0;
    margin-right: 20px;
    position: relative;
    height: 100%;
    padding-top: 5px;
  }
  
  .timeline-dot {
    position: relative;
    top: 0;
    left: 0;
    transform: none;
    margin: 0 auto;
    margin-bottom: 5px;
    width: 18px;
    height: 18px;
    border-width: 4px;
  }

  .timeline-period-year {
    margin-top: 0;
    margin-bottom: 8px;
    font-size: 0.9rem;
    padding: 4px 10px;
  }

  .timeline-period-title {
    min-height: 0;
    font-size: 0.85rem;
    align-items: flex-start;
    justify-content: flex-start;
    text-align: left;
    max-width: none;
  }

  .timeline-period-year {
    font-size: 0.8rem;
    padding: 4px 8px;
  }
  
  .modal-container {
    width: 95%;
  }
}

@media (max-width: 576px) {
  .horizontal-timeline-container {
    padding: 0 30px;
  }
  
  .timeline-period {
    padding: 0 25px;
    min-width: 120px;
  }
  
  .timeline-dot {
    width: 16px;
    height: 16px;
    border-width: 3px;
  }
  
  .timeline-period-title {
    font-size: 0.7rem;
  }
  
  .event-year {
    min-width: 60px;
    font-size: 0.8rem;
  }
  
  .event-title {
    font-size: 1rem;
  }
  
  .modal-title {
    font-size: 1.1rem;
  }
}
</style>