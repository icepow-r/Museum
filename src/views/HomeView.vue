<!-- src/views/HomeView.vue -->
<template>
    <div>
      <the-header />
      <div class="container mt-5">
        <historical-info id="about" />
        <timeline-slider id="timeline" />
        <notable-persons id="notable-persons" />
      </div>
    </div>
  </template>
  
  <script>
  import TheHeader from '@/components/layout/TheHeader.vue';
  import HistoricalInfo from '@/components/home/HistoricalInfo.vue';
  import TimelineSlider from '@/components/home/TimelineSlider.vue';
  import NotablePersons from '@/components/home/NotablePersons.vue';
  
  export default {
    name: 'HomeView',
    components: {
      TheHeader,
      HistoricalInfo,
      TimelineSlider,
      NotablePersons
    }
  }
  </script>