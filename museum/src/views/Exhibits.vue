<template>
  <div>
    <div class="title-block">
      <h2><i class="bi bi-collection-fill me-2"></i>Экспонаты музея</h2>
    </div>

    <div class="exhibits-intro text-center mb-4 p-3 bg-light rounded">
      <p class="lead mb-0">Уникальная коллекция экспонатов, отражающих историю развития электротехники и ЛЭТИ</p>
    </div>

    <div class="filter-container mb-4">
      <div class="d-flex justify-content-left align-items-center flex-wrap">
        <div class="filter-group">
        </div>
        <div class="search-group">
          <div class="input-group">
            <span class="input-group-text"><i class="bi bi-search"></i></span>
            <input type="text" class="form-control" v-model="searchQuery" placeholder="Поиск экспонатов...">
          </div>
        </div>
      </div>
    </div>

    <div v-if="filteredExhibits.length === 0" class="no-results text-center p-5">
      <i class="bi bi-search fs-1 text-muted"></i>
      <p class="mt-3">Экспонаты не найдены. Пожалуйста, измените параметры поиска.</p>
    </div>

    <div v-else class="row row-cols-1 row-cols-md-2 row-cols-lg-3 g-4">
      <div v-for="exhibit in filteredExhibits" :key="exhibit.exhibitId" class="col">
        <div class="card exhibit-card h-100" @click="showExhibitDetails(exhibit)">
          <div class="exhibit-image-container">
            <img :src="exhibit.images[0].imagePath" class="card-img-top" :alt="exhibit.images[0].altText">
            <div class="overlay">
              <span>Подробнее</span>
            </div>
          </div>
          <div class="card-body">
            <h5 class="card-title">{{ exhibit.title }}</h5>
          </div>
        </div>
      </div>
    </div>

    <div class="modal fade" id="exhibitModal" tabindex="-1" aria-labelledby="exhibitModalLabel" aria-hidden="true">
      <div class="modal-dialog modal-lg">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title" id="exhibitModalLabel">{{ selectedExhibit?.title }}</h5>
            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
          </div>
          <div class="modal-body" v-if="selectedExhibit">
            <div class="row">
              <div class="col-md-6">
				  <div class="exhibit-modal-image-container">
				    <img :src="selectedExhibit.images[this.currentModalImage].imagePath" class="img-fluid rounded" :alt="selectedExhibit.title">
				  </div>
          <span v-if="selectedExhibit.images.length > 1">
					  <div class="image-navigation-buttons">
					    <button class="btn btn-outline-primary" id="prevbutton" @click="prevImage"><i class="bi bi-arrow-left"></i></button>
					    <button class="btn btn-outline-primary" id="nextbutton" @click="nextImage"><i class="bi bi-arrow-right"></i></button>
					  </div>
				  </span>
              </div>
              <div class="col-md-6">
                <div class="exhibit-details">
                  <p class="exhibit-category mb-2">
                    <span class="badge bg-primary">{{ selectedExhibit.category }}</span>
                  </p>
                  <p class="exhibit-description">{{ selectedExhibit.description }}</p>
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
import { Modal } from 'bootstrap'

export default {
  name: 'ExhibitsPage',
  data() {
    return {
      searchQuery: '',
      selectedCategory: '',
      selectedExhibit: null,
      exhibitModal: null,
	  currentModalImage: 0,
      exhibits: []
    }
  },
  computed: {
    categories() {
      return [...new Set(this.exhibits.map(exhibit => exhibit.category))]
    },
    filteredExhibits() {
      return this.exhibits.filter(exhibit => {
        const matchesCategory = this.selectedCategory ? exhibit.category === this.selectedCategory : true
        const matchesSearch = this.searchQuery ? 
          exhibit.title.toLowerCase().includes(this.searchQuery.toLowerCase()) || 
          exhibit.description.toLowerCase().includes(this.searchQuery.toLowerCase()) :
          true
        return matchesCategory && matchesSearch
      })
    }
  },
  methods: {
    showExhibitDetails(exhibit) {
      this.selectedExhibit = exhibit
	  this.currentModalImage = 0
      this.exhibitModal.show()
    },
	nextImage() {
	  this.currentModalImage += 1 
	  this.currentModalImage = this.currentModalImage % this.selectedExhibit.images.length
	},
	prevImage() {
	  this.currentModalImage -= 1 
	  if (this.currentModalImage < 0) { this.currentModalImage = this.selectedExhibit.images.length - 1 }
	}
  },
  async mounted() {
    this.exhibitModal = new Modal(document.getElementById('exhibitModal'))
    
    try {
      const response = await fetch(`http://${import.meta.env.VITE_API_LINK}/api/Exhibits`, {
        method: "GET",
        headers: {
          "accept": "application/json",
        },
      });
      
      if (!response.ok) {
        throw new Error(`Ошибка HTTP: ${response.status}`);
      }
      
      const data = await response.json();
      
      const processedData = data.map(exhibit => {
        if (exhibit.images && exhibit.images.length > 0) {
          return {
            ...exhibit,
            images: exhibit.images.map(image => ({
              ...image,
              imagePath: `http://${import.meta.env.VITE_API_LINK}${image.imagePath}`
            }))
          };
        }
        return exhibit;
      });
      
      this.exhibits = processedData;
      console.log('Загруженные экспонаты:', this.exhibits);
    } catch (error) {
      console.error('Ошибка при загрузке экспонатов:', error);
      this.exhibits = [];
    }
  }
}
</script>

<style scoped>
.exhibits-intro {
  background-color: rgba(255, 255, 255, 0.7) !important;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.05);
  border-radius: 8px;
}

.filter-container {
  background-color: #f8f9fa;
  padding: 15px;
  border-radius: 8px;
  box-shadow: 0 2px 5px rgba(0, 0, 0, 0.05);
}

.filter-group, .search-group {
  margin: 5px 0;
}

.search-group {
  min-width: 250px;
}

.exhibit-card {
  transition: all 0.3s ease;
  border: none;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
  overflow: hidden;
  cursor: pointer;
}

.exhibit-card:hover {
  transform: translateY(-10px);
  box-shadow: 0 10px 20px rgba(0, 0, 0, 0.15);
}

.exhibit-image-container {
  position: relative;
  overflow: hidden;
  padding-top: 75%;
  height: 0;
}

.exhibit-image-container img {
  position: absolute;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  object-fit: cover;
  transition: transform 0.5s ease;
}

.exhibit-card:hover .exhibit-image-container img {
  transform: scale(1.1);
}

.exhibit-badge {
  position: absolute;
  top: 10px;
  right: 10px;
  background-color: rgba(57, 94, 171, 0.9);
  color: white;
  font-size: 0.8rem;
  padding: 5px 10px;
  border-radius: 20px;
  font-weight: 500;
}

.exhibit-year {
  font-size: 0.9rem;
  color: #6c757d;
  margin-bottom: 10px;
}

.card-title {
  color: #395eab;
  font-weight: 600;
  font-size: 2em;
  text-align: center;
}

.no-results {
  background-color: #f8f9fa;
  border-radius: 8px;
  border: 1px dashed #dee2e6;
}

.modal-content {
  border: none;
  box-shadow: 0 15px 30px rgba(0, 0, 0, 0.2);
}

.modal-header {
  background-color: #395eab;
  color: white;
}

.modal-header > h5 {
	font-weight: 900;
}

.btn-close {
  filter: brightness(0) invert(1);
}

.exhibit-details {
  padding: 10px;
}

.exhibit-category .badge {
  font-weight: normal;
  padding: 6px 12px;
}

.specs-title {
  border-bottom: 1px solid #dee2e6;
  padding-bottom: 8px;
  margin-bottom: 10px;
  color: #395eab;
}

.list-group-item {
  background-color: transparent;
  padding: 8px 0;
}

.image-navigation-buttons {
  display: flex;
  justify-content: space-between;
  margin-bottom: 15px;
}

button#nextbutton, button#prevbutton {
	width: 48%;
	padding: 8px 0;
}

button#nextbutton {
	border-radius: 0.25rem;
}

button#prevbutton {
	border-radius: 0.25rem;
}

.exhibit-modal-image-container {
  position: relative;
  overflow: hidden;
  padding-top: 75%;
  height: 0;
  margin-bottom: 15px;
}

.exhibit-modal-image-container img {
  position: absolute;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  object-fit: cover;
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

.exhibit-card:hover .overlay {
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
</style> 
