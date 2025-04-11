<template>
  <div v-if="game">
    <div class="title-block">
      <h2>{{ game.title }}</h2>
    </div>
    
    <div class="card mb-4">
      <div class="card-body">
        <div class="row">
          <div class="col-md-4 mb-3 mb-md-0">
            <img :src="game.image" class="img-fluid rounded" :alt="game.title">
          </div>
          <div class="col-md-8">
            <h4>{{ game.title }}</h4>
            <p class="mb-2">{{ game.description }}</p>
            <div class="d-flex mb-3">
              <span class="badge bg-secondary me-2">Сложность: {{ game.difficulty }}</span>
              <span class="badge bg-primary">{{ game.type }}</span>
            </div>
            
            <div v-if="game.id === 'quiz' && !quizStarted" class="mt-3">
              <button @click="startQuiz" class="btn auth-btn">Начать викторину</button>
            </div>
            
            <div v-if="game.id === 'quiz' && quizStarted" class="quiz-container">
              <div v-if="currentQuestionIndex < quizQuestions.length">
                <div class="card">
                  <div class="card-header">
                    <h5>Вопрос {{ currentQuestionIndex + 1 }} из {{ quizQuestions.length }}</h5>
                  </div>
                  <div class="card-body">
                    <p class="fs-5">{{ quizQuestions[currentQuestionIndex].question }}</p>
                    <div class="list-group mt-3">
                      <button
                        v-for="(option, index) in quizQuestions[currentQuestionIndex].options"
                        :key="index"
                        class="list-group-item list-group-item-action"
                        @click="selectAnswer(index)"
                        :class="{ 'active': selectedAnswerIndex === index }"
                      >
                        {{ option }}
                      </button>
                    </div>
                    <div class="d-flex justify-content-between mt-3">
                      <button 
                        @click="prevQuestion" 
                        class="btn btn-secondary"
                        :disabled="currentQuestionIndex === 0"
                      >
                        Предыдущий
                      </button>
                      <button 
                        @click="nextQuestion" 
                        class="btn auth-btn"
                        :disabled="selectedAnswerIndex === null"
                      >
                        {{ currentQuestionIndex === quizQuestions.length - 1 ? 'Завершить' : 'Следующий' }}
                      </button>
                    </div>
                  </div>
                </div>
              </div>
              <div v-else class="quiz-results">
                <div class="card">
                  <div class="card-header bg-success text-white">
                    <h5>Результаты викторины</h5>
                  </div>
                  <div class="card-body">
                    <h4>Вы ответили правильно на {{ correctAnswers }} из {{ quizQuestions.length }} вопросов</h4>
                    <div class="progress mb-3">
                      <div 
                        class="progress-bar" 
                        role="progressbar" 
                        :style="{ width: (correctAnswers / quizQuestions.length * 100) + '%' }"
                        :class="{ 
                          'bg-danger': correctAnswers / quizQuestions.length < 0.4,
                          'bg-warning': correctAnswers / quizQuestions.length >= 0.4 && correctAnswers / quizQuestions.length < 0.7,
                          'bg-success': correctAnswers / quizQuestions.length >= 0.7
                        }"
                      >
                        {{ Math.round(correctAnswers / quizQuestions.length * 100) }}%
                      </div>
                    </div>
                    <button @click="resetQuiz" class="btn auth-btn">Начать заново</button>
                  </div>
                </div>
              </div>
            </div>
            
            <div v-else-if="game.id !== 'quiz'" class="alert alert-info mt-3">
              Для запуска этой игры нажмите кнопку ниже.
              <div class="mt-2">
                <button class="btn auth-btn">Запустить {{ game.title }}</button>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
    
    <router-link to="/games" class="btn auth-btn">
      Назад к играм
    </router-link>
  </div>
  <div v-else class="alert alert-warning">
    Игра не найдена.
  </div>
</template>

<script>
export default {
  name: 'GameDetailPage',
  data() {
    return {
      games: [
        {
          id: 'quiz',
          title: 'Викторина о ЛЭТИ',
          description: 'Проверьте свои знания об истории и выдающихся личностях ЛЭТИ в этой увлекательной викторине.',
          difficulty: 'Средняя',
          type: 'Викторина',
          image: '/src/assets/images/games/quiz.jpg'
        },
        {
          id: 'timeline',
          title: 'Хронология ЛЭТИ',
          description: 'Расположите важные события в истории ЛЭТИ в правильном хронологическом порядке.',
          difficulty: 'Сложная',
          type: 'Хронология',
          image: '/src/assets/images/games/timeline.jpg'
        },
        {
          id: 'puzzle',
          title: 'Пазл исторических фотографий',
          description: 'Соберите пазлы из исторических фотографий ЛЭТИ разной сложности.',
          difficulty: 'Разная',
          type: 'Пазл',
          image: '/src/assets/images/games/puzzle.jpg'
        },
        {
          id: 'memory',
          title: 'Игра на память',
          description: 'Найдите пары карточек с изображениями известных личностей и важных мест ЛЭТИ.',
          difficulty: 'Лёгкая',
          type: 'Карточная игра',
          image: '/src/assets/images/games/memory.jpg'
        },
        {
          id: 'crossword',
          title: 'Кроссворд ЛЭТИ',
          description: 'Разгадайте кроссворд, посвящённый истории, людям и достижениям ЛЭТИ.',
          difficulty: 'Средняя',
          type: 'Кроссворд',
          image: '/src/assets/images/games/crossword.jpg'
        },
        {
          id: 'tour',
          title: 'Виртуальный тур',
          description: 'Совершите виртуальную экскурсию по историческим местам ЛЭТИ и узнайте интересные факты.',
          difficulty: 'Легкая',
          type: 'Тур',
          image: '/src/assets/images/games/tour.jpg'
        }
      ],
      quizStarted: false,
      currentQuestionIndex: 0,
      selectedAnswerIndex: null,
      userAnswers: [],
      correctAnswers: 0,
      quizQuestions: [
        {
          question: 'В каком году был основан ЛЭТИ?',
          options: ['1886', '1891', '1899', '1910'],
          correctAnswer: 0
        },
        {
          question: 'Кто был первым директором ЭТИ?',
          options: ['А.С. Попов', 'Н.Г. Писаревский', 'М.А. Шателен', 'И.И. Боргман'],
          correctAnswer: 1
        },
        {
          question: 'Кто из этих учёных изобрёл радио?',
          options: ['Г. Маркони', 'Н. Тесла', 'А.С. Попов', 'Дж. Максвелл'],
          correctAnswer: 2
        },
        {
          question: 'За какое открытие Ж.И. Алфёров получил Нобелевскую премию?',
          options: [
            'За изобретение лазера', 
            'За разработку полупроводниковых гетероструктур', 
            'За открытие сверхпроводимости', 
            'За теорию электромагнитного поля'
          ],
          correctAnswer: 1
        },
        {
          question: 'Как назывался ЛЭТИ в 1920-е годы?',
          options: [
            'Электротехнический институт императора Александра III', 
            'Петроградский электротехнический институт', 
            'Ленинградский электротехнический институт им. В.И. Ульянова (Ленина)', 
            'Санкт-Петербургский государственный электротехнический университет'
          ],
          correctAnswer: 2
        }
      ]
    }
  },
  computed: {
    game() {
      return this.games.find(g => g.id === this.$route.params.id)
    }
  },
  methods: {
    startQuiz() {
      this.quizStarted = true
      this.currentQuestionIndex = 0
      this.selectedAnswerIndex = null
      this.userAnswers = []
      this.correctAnswers = 0
    },
    selectAnswer(index) {
      this.selectedAnswerIndex = index
    },
    nextQuestion() {
      this.userAnswers[this.currentQuestionIndex] = this.selectedAnswerIndex
      
      if (this.selectedAnswerIndex === this.quizQuestions[this.currentQuestionIndex].correctAnswer) {
        this.correctAnswers++
      }
      
      if (this.currentQuestionIndex < this.quizQuestions.length - 1) {
        this.currentQuestionIndex++
        this.selectedAnswerIndex = this.userAnswers[this.currentQuestionIndex] !== undefined 
          ? this.userAnswers[this.currentQuestionIndex] 
          : null
      } else {
        this.currentQuestionIndex++
      }
    },
    prevQuestion() {
      if (this.currentQuestionIndex > 0) {
        this.currentQuestionIndex--
        this.selectedAnswerIndex = this.userAnswers[this.currentQuestionIndex]
      }
    },
    resetQuiz() {
      this.startQuiz()
    }
  }
}
</script>

<style scoped>
.quiz-container {
  max-width: 700px;
}

.list-group-item.active {
  background-color: rgba(57, 94, 171, 0.9);
  border-color: rgba(57, 94, 171, 0.9);
}
</style> 