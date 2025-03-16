<!-- src/App.vue -->
<template>
  <div id="app">
    <the-navigation />
    <router-view />
    <the-footer />
  </div>
</template>

<script>
import TheNavigation from '@/components/layout/TheNavigation.vue';
import TheFooter from '@/components/layout/TheFooter.vue';

export default {
  name: 'App',
  components: {
    TheNavigation,
    TheFooter
  }
}
</script>

<style>
@import 'bootstrap/dist/css/bootstrap.min.css';
@import 'bootstrap-icons/font/bootstrap-icons.css';

:root {
  --bs-primary: #0055a4;
  --bs-primary-rgb: 0, 85, 164;
}

.btn-primary {
  background-color: var(--bs-primary);
  border-color: var(--bs-primary);
}

.btn-primary:hover {
  background-color: #004080;
  border-color: #004080;
}

.btn-outline-primary {
  color: var(--bs-primary);
  border-color: var(--bs-primary);
}

.btn-outline-primary:hover {
  background-color: var(--bs-primary);
  border-color: var(--bs-primary);
}

.text-primary {
  color: var(--bs-primary) !important;
}

body {
  font-family: 'Roboto', sans-serif;
}
</style>