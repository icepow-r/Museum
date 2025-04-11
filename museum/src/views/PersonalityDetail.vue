<template>
  <div v-if="person">
    <div class="title-block">
      <h2>{{ person.name }}</h2>
    </div>
    
    <div class="card mb-4">
      <div class="row g-0">
        <div class="col-md-4">
          <div class="person-detail-image-container">
            <img :src="person.image" class="img-fluid" :alt="person.name">
          </div>
        </div>
        <div class="col-md-8">
          <div class="card-body">
            <h5 class="card-subtitle mb-2 text-muted">Годы жизни: {{ person.years }}</h5>
            <div class="person-description">
              <p v-for="(paragraph, index) in formattedDescription" :key="index" class="card-text fs-5">
                {{ paragraph }}
              </p>
            </div>
            <div class="mt-4 d-flex gap-2">
              <button v-if="person.proceedings && person.proceedings.length > 0" class="btn btn-outline-primary" @click="showProceedingsModal = true">
                <i class="bi bi-book me-2"></i>Труды
              </button>
              <button v-if="person.awards && person.awards.length > 0" class="btn btn-outline-success" @click="showAwardsModal = true">
                <i class="bi bi-trophy me-2"></i>Награды
              </button>
            </div>
          </div>
        </div>
      </div>
    </div>
    
    <div class="modal fade" :class="{ 'show d-block': showProceedingsModal }" tabindex="-1" role="dialog" aria-hidden="true">
      <div class="modal-dialog modal-dialog-centered modal-dialog-scrollable">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title">Труды</h5>
            <button type="button" class="btn-close" @click="showProceedingsModal = false"></button>
          </div>
          <div class="modal-body">
            <ul class="list-group list-group-flush">
              <li v-for="(item, index) in person.proceedings" :key="index" class="list-group-item">
                {{ item }}
              </li>
            </ul>
          </div>
          <div class="modal-footer">
            <button type="button" class="btn btn-secondary" @click="showProceedingsModal = false">Закрыть</button>
          </div>
        </div>
      </div>
    </div>
    <div v-if="showProceedingsModal" class="modal-backdrop fade show"></div>
    
    <div class="modal fade" :class="{ 'show d-block': showAwardsModal }" tabindex="-1" role="dialog" aria-hidden="true">
      <div class="modal-dialog modal-dialog-centered modal-dialog-scrollable">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title">Награды</h5>
            <button type="button" class="btn-close" @click="showAwardsModal = false"></button>
          </div>
          <div class="modal-body">
            <ul class="list-group list-group-flush">
              <li v-for="(item, index) in person.awards" :key="index" class="list-group-item">
                {{ item }}
              </li>
            </ul>
          </div>
          <div class="modal-footer">
            <button type="button" class="btn btn-secondary" @click="showAwardsModal = false">Закрыть</button>
          </div>
        </div>
      </div>
    </div>
    <div v-if="showAwardsModal" class="modal-backdrop fade show"></div>
    
    <router-link to="/personalities" class="btn auth-btn">
      Назад к списку
    </router-link>
  </div>
  <div v-else class="alert alert-warning">
    Информация о личности не найдена.
  </div>
</template>

<script>
import peopleData from '@/assets/people.json'

export default {
  name: 'PersonalityDetailPage',
  data() {
    return {
      showProceedingsModal: false,
      showAwardsModal: false,
      peopleData: peopleData
    }
  },
  computed: {
    person() {
      return this.peopleData.find(p => p.id === this.$route.params.id)
    },
    formattedDescription() {
      if (this.person && this.person.description) {
        return this.person.description.split('\n').filter(paragraph => paragraph.trim() !== '')
      }
      return []
    }
  }
}
</script>

<style scoped>
.modal {
  background-color: rgba(0, 0, 0, 0.5);
}

.modal-backdrop {
  z-index: 1040;
}

.modal.show {
  z-index: 1050;
}

.list-group-item {
  border-left: none;
  border-right: none;
  padding: 12px 0;
}

.person-detail-image-container {
  position: relative;
  overflow: hidden;
  padding-top: 140%;
  height: 0;
}

.person-detail-image-container img {
  position: absolute;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  object-fit: cover;
}
</style> 