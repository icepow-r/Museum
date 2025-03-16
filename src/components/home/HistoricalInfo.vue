<!-- src/components/home/HistoricalInfo.vue -->
<template>
    <section class="py-5">
      <div class="row">
        <div class="col-lg-6">
          <h2 class="mb-4 text-primary">Историческая справка</h2>
          <p>Санкт-Петербургский государственный электротехнический университет «ЛЭТИ» им. В.И. Ульянова (Ленина) – старейший электротехнический университет Европы, основанный в 1886 году.</p>
          <p>Университет был создан по инициативе выдающегося ученого, изобретателя радио А.С. Попова и первоначально назывался Техническим училищем почтово-телеграфного ведомства.</p>
          <p>Сегодня ЛЭТИ – современный научно-образовательный центр, один из лидеров в области электроники, электротехники, информационных технологий и биомедицинской инженерии.</p>
          <router-link to="/about" class="btn btn-outline-primary mt-3">Подробнее об истории</router-link>
        </div>
        <div class="col-lg-6">
          <img src="" alt="Исторический корпус ЛЭТИ" class="img-fluid rounded shadow">
        </div>
      </div>
    </section>
  </template>
  
  <script>
  export default {
    name: 'HistoricalInfo'
  }
  </script>