<!-- src/views/PersonsView.vue -->
<template>
    <div>
        <div class="page-header bg-primary text-white py-5">
            <div class="container">
                <h1 class="display-4">Выдающиеся личности</h1>
                <p class="lead">Учёные и исследователи, внесшие вклад в развитие ЛЭТИ и мировой науки</p>
            </div>
        </div>

        <div class="container py-5">
            <div class="row g-4">
                <div class="col-md-6 col-lg-4" v-for="person in persons" :key="person.id">
                    <div class="card h-100 shadow-sm hover-card">
                        <img :src="person.image" class="card-img-top" :alt="person.name">
                        <div class="card-body">
                            <h5 class="card-title text-primary">{{ person.name }}</h5>
                            <h6 class="card-subtitle mb-2 text-muted">{{ person.years }}</h6>
                            <p class="card-text">{{ person.description }}</p>
                        </div>
                        <div class="card-footer bg-transparent border-0">
                            <router-link :to="{ name: 'person-detail', params: { id: person.id } }"
                                class="btn btn-outline-primary">
                                Подробнее
                            </router-link>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
import { mapGetters } from 'vuex'

export default {
    name: 'PersonsView',
    computed: {
        ...mapGetters({
            persons: 'getAllPersons'
        })
    }
}
</script>

<style scoped>
.page-header {
    margin-top: 56px;
}

.hover-card {
    transition: transform 0.3s, box-shadow 0.3s;
}

.hover-card:hover {
    transform: translateY(-5px);
    box-shadow: 0 0.5rem 1rem rgba(0, 0, 0, 0.15) !important;
}
</style>