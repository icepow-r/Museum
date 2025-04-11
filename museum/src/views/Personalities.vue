<template>
  <div>
    <div class="title-block">
      <h2><i class="bi bi-people-fill me-2"></i>Выдающиеся личности ЛЭТИ</h2>
    </div>
    
    <div class="intro-text text-center mb-4 p-3 bg-light rounded">
      <p class="lead mb-0">Знаменитые ученые и исследователи, чьи имена тесно связаны с историей ЛЭТИ и развитием науки</p>
    </div>

    <div class="filter-container mb-4">
      <div class="d-flex justify-content-between align-items-center flex-wrap">
        <div class="search-group">
          <div class="input-group">
            <span class="input-group-text"><i class="bi bi-search"></i></span>
            <input type="text" class="form-control" v-model="searchQuery" placeholder="Поиск личностей...">
          </div>
        </div>
      </div>
      
      <div class="tags-container mt-3" v-if="tags.length > 0">
        <span class="me-2">Теги:</span>
        <div class="tags-list">
          <button 
            v-for="tag in tags" 
            :key="tag" 
            @click="toggleTagFilter(tag)"
            :class="['tag-badge', {'active': selectedTags.includes(tag)}]"
          >
            {{ tag }}
          </button>
        </div>
      </div>
    </div>
    
    <div v-if="filteredPersonalities.length === 0" class="no-results text-center p-5">
      <i class="bi bi-search fs-1 text-muted"></i>
      <p class="mt-3">Личности не найдены. Пожалуйста, измените параметры поиска.</p>
    </div>
    
    <div v-else class="row row-cols-1 row-cols-md-2 row-cols-lg-3 g-4">
      <div v-for="person in filteredPersonalities" :key="person.id" class="col">
        <router-link :to="{ name: 'personality', params: { id: person.id }}" class="card-link">
          <div class="card person-card h-100">
            <div class="person-image-container">
              <img :src="person.image" class="card-img-top" :alt="person.name">
              <div class="overlay">
                <span>Подробнее</span>
              </div>
            </div>
            <div class="card-body text-center">
              <h5 class="card-title">{{ person.name }}</h5>
              <p class="card-text years-text">{{ person.years }}</p>
              <div class="person-categories mb-2">
                <span 
                  v-for="tag in person.tags" 
                  :key="tag" 
                  class="badge bg-secondary me-1"
                  @click.prevent="toggleTagFilter(tag)"
                >
                  {{ tag }}
                </span>
              </div>
              <p class="card-text short-description">{{ person.shortBio || truncateText(person.description) }}</p>
            </div>
          </div>
        </router-link>
      </div>
    </div>
  </div>
</template>

<script>
import peopleData from '@/assets/people.json'

export default {
  name: 'PersonalitiesPage',
  data() {
    return {
      searchQuery: '',
      selectedTags: [],
      personalities: peopleData
    }
  },
  computed: {
    tags() {
      const allTags = this.personalities.flatMap(person => person.tags || []);
      return [...new Set(allTags)];
    },
    filteredPersonalities() {
      return this.personalities.filter(person => {
        const matchesTags = this.selectedTags.length ? 
          this.selectedTags.every(tag => person.tags && person.tags.includes(tag)) : 
          true;
        const matchesSearch = this.searchQuery ? 
          person.name.toLowerCase().includes(this.searchQuery.toLowerCase()) || 
          person.description.toLowerCase().includes(this.searchQuery.toLowerCase()) ||
          (person.shortBio && person.shortBio.toLowerCase().includes(this.searchQuery.toLowerCase())) :
          true;
          
        return matchesTags && matchesSearch;
      });
    }
  },
  methods: {
    truncateText(text) {
      if (text.length <= 100) return text
      return text.substring(0, 100) + '...'
    },
    toggleTagFilter(tag) {
      const index = this.selectedTags.indexOf(tag);
      if (index === -1) {
        this.selectedTags.push(tag);
      } else {
        this.selectedTags.splice(index, 1);
      }
    }
  }
}
</script>

<style scoped>
.intro-text {
  background-color: rgba(255, 255, 255, 0.7) !important;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.05);
  border-radius: 8px;
}

.filter-container {
  background-color: #f8f9fa;
  padding: 16px;
  border-radius: 8px;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.03);
}

.filter-group, .search-group {
  margin-bottom: 10px;
}

.search-group {
  flex-grow: 1;
  max-width: 300px;
}

.tags-container {
  display: flex;
  align-items: center;
  flex-wrap: wrap;
}

.tags-list {
  display: flex;
  flex-wrap: wrap;
  gap: 5px;
}

.tag-badge {
  background-color: #e9ecef;
  color: #495057;
  border: none;
  padding: 4px 12px;
  border-radius: 20px;
  font-size: 0.8rem;
  cursor: pointer;
  transition: all 0.2s ease;
}

.tag-badge:hover {
  background-color: #dee2e6;
}

.tag-badge.active {
  background-color: #395eab;
  color: white;
}

.person-card {
  transition: all 0.3s ease;
  overflow: hidden;
}

.person-card:hover {
  transform: translateY(-10px);
  box-shadow: 0 10px 20px rgba(0, 0, 0, 0.1);
}

.person-image-container {
  position: relative;
  overflow: hidden;
  padding-top: 133.33%;
  height: 0;
}

.person-image-container img {
  width: 100%;
  height: 100%;
  object-fit: cover;
  object-position: center top;
  transition: transform 0.5s ease;
  position: absolute;
  top: 0;
  left: 0;
}

.person-card:hover .person-image-container img {
  transform: scale(1.1);
}

.overlay {
  position: absolute;
  bottom: 0;
  left: 0;
  right: 0;
  background-color: rgba(57, 94, 171, 0.85);
  overflow: hidden;
  width: 100%;
  height: 0;
  transition: .5s ease;
  display: flex;
  align-items: center;
  justify-content: center;
}

.person-card:hover .overlay {
  height: 100%;
}

.overlay span {
  color: white;
  font-size: 18px;
  font-weight: 500;
  position: relative;
  padding: 10px 20px;
  border: 2px solid white;
  border-radius: 30px;
}

.years-text {
  color: #6c757d;
  font-weight: 500;
  font-size: 0.9rem;
  margin-bottom: 10px;
}

.person-categories {
  margin-bottom: 10px;
}

.short-description {
  font-size: 0.9rem;
  color: #555;
  line-height: 1.5;
}

.card-title {
  font-weight: 600;
  color: #395eab;
}

.badge {
  font-weight: 500;
  font-size: 0.75rem;
}

.badge.bg-secondary {
  cursor: pointer;
  transition: transform 0.2s ease;
}

.badge.bg-secondary:hover {
  transform: translateY(-2px);
  box-shadow: 0 2px 3px rgba(0, 0, 0, 0.1);
}

.no-results {
  background-color: #f8f9fa;
  border-radius: 8px;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.03);
}

.no-results i {
  color: #dee2e6;
}

@media (max-width: 767px) {
  .filter-container {
    padding: 12px;
  }
  
  .search-group {
    max-width: 100%;
  }
}
</style> 