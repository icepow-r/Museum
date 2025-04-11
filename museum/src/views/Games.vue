<template>
  <div>
    <div class="title-block">
      <h2><i class="bi bi-controller me-2"></i>Игры и интерактивные материалы</h2>
    </div>

    <div class="games-intro text-center mb-4 p-3 bg-light rounded">
      <p class="lead mb-0">Проверьте свои знания о ЛЭТИ и истории электроники в наших интерактивных играх</p>
    </div>

    <div v-if="!quizStarted && !matchGameStarted" class="row row-cols-1 row-cols-md-2 g-4 mb-5">
      <div class="col">
        <div class="game-card h-100">
          <div class="game-card-header">
            <div class="game-icon">
              <i class="bi bi-patch-question"></i>
            </div>
            <h3 class="game-title">Викторина о ЛЭТИ</h3>
          </div>
          <div class="game-card-body">
            <p class="game-description mb-4">Проверьте свои знания об истории Санкт-Петербургского электротехнического университета и его выдающихся личностях</p>
            <button class="btn btn-primary game-button" @click="startQuiz">
              <i class="bi bi-play-fill me-2"></i>Начать викторину
            </button>
          </div>
        </div>
      </div>
      <div class="col">
        <div class="game-card h-100">
          <div class="game-card-header">
            <div class="game-icon">
              <i class="bi bi-grid-3x3-gap"></i>
            </div>
            <h3 class="game-title">Игра "Мемо"</h3>
          </div>
          <div class="game-card-body">
            <p class="game-description mb-4">Соревновательная игра на память и время. Сопоставьте фотографии выдающихся личностей с их именами как можно быстрее!</p>
            <button class="btn btn-primary game-button" @click="startMatchGame">
              <i class="bi bi-play-fill me-2"></i>Начать игру
            </button>
          </div>
        </div>
      </div>
    </div>

    <div v-if="quizStarted" class="quiz-container">
      <div class="quiz-header mb-4">
        <h3 class="quiz-title">Викторина о ЛЭТИ</h3>
        <div class="quiz-progress">
          <div class="progress">
            <div class="progress-bar" role="progressbar" :style="{ width: `${progressPercentage}%` }" 
                 :aria-valuenow="currentQuestion + 1" aria-valuemin="0" :aria-valuemax="questions.length">
              {{ currentQuestion + 1 }} / {{ questions.length }}
            </div>
          </div>
        </div>
      </div>
      
      <div v-if="!quizFinished" class="quiz-question-container">
        <div class="quiz-question">
          <h4>{{ questions[currentQuestion].question }}</h4>
        </div>
        <div class="quiz-options">
          <div class="form-check quiz-option" v-for="(option, index) in questions[currentQuestion].options" :key="index">
            <input class="form-check-input" type="radio" :name="'quiz-option'" :id="`option-${index}`" 
                   :value="index" v-model="selectedOption"
                   :disabled="optionSelected">
            <label class="form-check-label" :for="`option-${index}`" 
                   :class="getOptionClass(index)">
              {{ option }}
            </label>
          </div>
        </div>
        <div class="quiz-actions">
          <button v-if="!optionSelected" class="btn btn-primary" @click="checkAnswer" :disabled="selectedOption === null">
            Проверить ответ
          </button>
          <button v-else class="btn btn-primary" @click="nextQuestion">
            {{ currentQuestion < questions.length - 1 ? 'Следующий вопрос' : 'Завершить викторину' }}
          </button>
        </div>
        <div v-if="optionSelected" class="quiz-feedback mt-3" :class="{'correct': isCorrect, 'incorrect': !isCorrect}">
          <div v-if="isCorrect" class="d-flex align-items-center">
            <i class="bi bi-check-circle-fill me-2"></i>
            <span>Правильно!</span>
          </div>
          <div v-else class="d-flex align-items-center">
            <i class="bi bi-x-circle-fill me-2"></i>
            <span>Неправильно! Правильный ответ: {{ questions[currentQuestion].options[questions[currentQuestion].correctAnswer] }}</span>
          </div>
        </div>
      </div>
      
      <div v-else class="quiz-results">
        <div class="quiz-results-header">
          <i class="bi bi-trophy-fill quiz-trophy" v-if="score >= questions.length * 0.7"></i>
          <i class="bi bi-emoji-smile-fill quiz-smile" v-else-if="score >= questions.length * 0.4"></i>
          <i class="bi bi-book-fill quiz-book" v-else></i>
          <h4>Викторина завершена!</h4>
        </div>
        <div class="quiz-score">
          <p>Ваш результат: <span class="score-value">{{ score }}</span> из <span>{{ questions.length }}</span></p>
        </div>
        <div class="quiz-result-message">
          <p v-if="score >= questions.length * 0.7">Отличный результат! Вы хорошо знаете историю ЛЭТИ!</p>
          <p v-else-if="score >= questions.length * 0.4">Неплохой результат, но есть куда расти. Посетите наш музей, чтобы узнать больше!</p>
          <p v-else>Советуем больше узнать об истории ЛЭТИ. Посетите наш музей!</p>
        </div>
        <div class="quiz-actions mt-4">
          <button class="btn btn-primary me-3" @click="restartQuiz">
            <i class="bi bi-arrow-repeat me-2"></i>Пройти еще раз
          </button>
          <button class="btn btn-outline-secondary" @click="exitQuiz">
            <i class="bi bi-box-arrow-left me-2"></i>Вернуться к играм
          </button>
        </div>
      </div>
    </div>

    <div v-if="matchGameStarted" class="match-game-container">
      <div class="match-game-header mb-4">
        <h3 class="match-game-title">Игра "Match" - Выдающиеся личности ЛЭТИ</h3>
        <div class="match-game-info d-flex justify-content-between align-items-center bg-light p-3 rounded">
          <div class="match-timer">
            <i class="bi bi-clock me-2"></i>
            <span class="timer-value">{{ formattedTime }}</span>
          </div>
          <div class="match-progress">
            <span>{{ matchedCards.length / 2 }} / {{ personalities.length }}</span>
          </div>
        </div>
      </div>
      
      <div v-if="!matchGameFinished" class="match-game-board">
        <div class="row row-cols-2 row-cols-sm-3 row-cols-md-4 g-3">
          <div v-for="card in matchCards" :key="card.id" class="col">
            <div 
              class="match-card" 
              :class="{ 'flipped': card.isFlipped, 'matched': card.isMatched }"
              @click="flipCard(card)"
            >
              <div class="match-card-front">
                <i class="bi bi-question-lg"></i>
              </div>
              <div class="match-card-back">
                <div v-if="card.type === 'image'" class="match-card-image">
                  <img :src="card.content" :alt="card.personId">
                </div>
                <div v-else class="match-card-name">
                  {{ card.content }}
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
      
      <div v-if="matchGameFinished" class="match-game-results">
        <div class="results-header">
          <i class="bi bi-trophy-fill game-trophy"></i>
          <h4>Поздравляем!</h4>
        </div>
        <div class="results-body">
          <p class="results-message">Вы успешно завершили игру за <span class="time-result">{{ formattedTime }}</span></p>
          
          <div v-if="showScoreInput" class="score-input-container mt-4 mb-4">
            <label class="form-label">Введите ваше имя для сохранения результата:</label>
            <div class="input-group mb-3">
              <input type="text" class="form-control" v-model="playerName" placeholder="Ваше имя">
              <button class="btn btn-primary" @click="submitScore">Сохранить</button>
            </div>
          </div>
          
          <div v-if="scoreSubmitted || !showScoreInput" class="leaderboard mt-4">
            <h5>Таблица рекордов</h5>
            <div class="table-responsive">
              <table class="table table-striped">
                <thead>
                  <tr>
                    <th>#</th>
                    <th>Имя</th>
                    <th>Время</th>
                  </tr>
                </thead>
                <tbody>
                  <tr v-for="(player, index) in sortedLeaderboard" :key="index" 
                      :class="{'table-primary': player.name === playerName}">
                    <td>{{ index + 1 }}</td>
                    <td>{{ player.name }}</td>
                    <td>{{ Math.floor(player.time / 60) }}:{{ (player.time % 60).toString().padStart(2, '0') }}</td>
                  </tr>
                </tbody>
              </table>
            </div>
          </div>
        </div>
        <div class="results-actions mt-4">
          <button class="btn btn-primary me-3" @click="restartMatchGame">
            <i class="bi bi-arrow-repeat me-2"></i>Сыграть еще раз
          </button>
          <button class="btn btn-outline-secondary" @click="exitMatchGame">
            <i class="bi bi-box-arrow-left me-2"></i>Вернуться к играм
          </button>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import peopleData from '@/assets/people.json'

export default {
  name: 'GamesPage',
  data() {
    return {
      quizStarted: false,
      quizFinished: false,
      currentQuestion: 0,
      selectedOption: null,
      optionSelected: false,
      isCorrect: false,
      score: 0,
      questions: [
        {
          question: 'В каком году был основан ЛЭТИ?',
          options: ['1886', '1891', '1899', '1905'],
          correctAnswer: 0
        },
        {
          question: 'Кто был первым директором ЭТИ (сейчас ЛЭТИ)?',
          options: ['А.С. Попов', 'Н.Г. Писаревский', 'Д.И. Менделеев', 'И.И. Боргман'],
          correctAnswer: 1
        },
        {
          question: 'Кто из этих ученых изобрел радио и работал в ЛЭТИ?',
          options: ['М.В. Ломоносов', 'А.С. Попов', 'И.П. Павлов', 'Н.И. Лобачевский'],
          correctAnswer: 1
        },
        {
          question: 'За что Ж.И. Алферов, связанный с ЛЭТИ, получил Нобелевскую премию?',
          options: ['За открытие графена', 'За разработку лазеров', 'За изобретение транзистора', 'За разработку полупроводниковых гетероструктур'],
          correctAnswer: 3
        },
        {
          question: 'Как назывался ЛЭТИ в период с 1918 по 1924 год?',
          options: ['Петроградский электротехнический институт', 'Ленинградский электротехнический институт', 'Петроградский политехнический институт', 'Электротехнический институт императора Александра III'],
          correctAnswer: 0
        }
      ],
      
      matchGameStarted: false,
      matchGameFinished: false,
      matchCards: [],
      flippedCards: [],
      matchedCards: [],
      gameTime: 0,
      gameTimerId: null,
      playerName: '',
      showScoreInput: false,
      scoreSubmitted: false,
      leaderboard: [],
      personalities: []
    }
  },
  created() {
    this.personalities = this.getRandomSix();
  },

  computed: {
    progressPercentage() {
      return ((this.currentQuestion + 1) / this.questions.length) * 100
    },
    formattedTime() {
      const minutes = Math.floor(this.gameTime / 60);
      const seconds = this.gameTime % 60;
      return `${minutes < 10 ? '0' + minutes : minutes}:${seconds < 10 ? '0' + seconds : seconds}`;
    },
    sortedLeaderboard() {
      return [...this.leaderboard].sort((a, b) => a.time - b.time).slice(0, 10);
    }
  },
  methods: {
      getRandomSix() {
      const shuffled = [...peopleData];
      for (let i = shuffled.length - 1; i > 0; i--) {
          const j = Math.floor(Math.random() * (i + 1));
          [shuffled[i], shuffled[j]] = [shuffled[j], shuffled[i]];
      }
      return shuffled.slice(0, 6);
    },
    startQuiz() {
      this.quizStarted = true
      this.quizFinished = false
      this.currentQuestion = 0
      this.score = 0
      this.selectedOption = null
      this.optionSelected = false
    },
    checkAnswer() {
      this.optionSelected = true
      this.isCorrect = this.selectedOption === this.questions[this.currentQuestion].correctAnswer
      if (this.isCorrect) {
        this.score++
      }
    },
    nextQuestion() {
      if (this.currentQuestion < this.questions.length - 1) {
        this.currentQuestion++
        this.selectedOption = null
        this.optionSelected = false
      } else {
        this.quizFinished = true
      }
    },
    getOptionClass(index) {
      if (!this.optionSelected) return ''
      
      if (index === this.questions[this.currentQuestion].correctAnswer) {
        return 'correct-option'
      } else if (index === this.selectedOption) {
        return this.isCorrect ? 'correct-option' : 'incorrect-option'
      }
      
      return ''
    },
    restartQuiz() {
      this.startQuiz()
    },
    exitQuiz() {
      this.quizStarted = false
      this.quizFinished = false
    },
    
    startMatchGame() {
      this.matchGameStarted = true;
      this.matchGameFinished = false;
      this.flippedCards = [];
      this.matchedCards = [];
      this.gameTime = 0;
      this.playerName = '';
      this.showScoreInput = false;
      this.scoreSubmitted = false;
      
      const cards = [];
      this.personalities.forEach(person => {
        cards.push({
          id: `img-${person.id}`,
          type: 'image',
          content: person.image,
          personId: person.id,
          isFlipped: false,
          isMatched: false
        });
        
        cards.push({
          id: `name-${person.id}`,
          type: 'name',
          content: person.name,
          personId: person.id,
          isFlipped: false,
          isMatched: false
        });
      });
      
      this.matchCards = this.shuffleArray([...cards]);
      
      this.startTimer();
    },
    
    flipCard(card) {
      if (this.flippedCards.length >= 2 || card.isMatched) return;
      
      if (this.flippedCards.some(c => c.id === card.id)) return;
      
      card.isFlipped = true;
      this.flippedCards.push(card);
      
      if (this.flippedCards.length === 2) {
        setTimeout(() => {
          this.checkMatch();
        }, 1000);
      }
    },
    
    checkMatch() {
      const [card1, card2] = this.flippedCards;
      
      if (card1.personId === card2.personId && card1.type !== card2.type) {
        card1.isMatched = true;
        card2.isMatched = true;
        this.matchedCards.push(card1, card2);
      } else {
        card1.isFlipped = false;
        card2.isFlipped = false;
        this.gameTime += 5;
      }
      
      this.flippedCards = [];
      
      if (this.matchedCards.length === this.matchCards.length) {
        this.finishGame();
      }
    },
    
    startTimer() {
      this.gameTimerId = setInterval(() => {
        this.gameTime++;
      }, 1000);
    },
    
    finishGame() {
      clearInterval(this.gameTimerId);
      this.matchGameFinished = true;
      this.showScoreInput = true;
    },
    
    async submitScore() {
      if (!this.playerName.trim()) return;
      
	  const response_scores_raw = await fetch("http://" + import.meta.env.VITE_API_LINK + "/api/Scores/game/1", {
		method: "GET",
		headers: {
			"accept": "application/json",
			"Content-Type": "application/json",
		},
	  });
	  const response_scores = await response_scores_raw.json()
	  this.leaderboard = []
	  for (let i = 0; i < response_scores.length; i++) {
		  this.leaderboard.push({name: response_scores[i].playerName, time: response_scores[i].scoreValue})
	  }
      
	  const response_post_raw = await fetch("http://" + import.meta.env.VITE_API_LINK + "/api/Scores", {
		method: "POST",
		headers: {
			"accept": "application/json",
			"Content-Type": "application/json",
		},
		body: JSON.stringify({
			gameId: 1,
			playerName: this.playerName,
			scoreValue: this.gameTime,
		})
	  });

      this.leaderboard.push({
        name: this.playerName,
        time: this.gameTime
      });

	  const response_post = await response_post_raw.json()
	  console.log(response_post)

      this.scoreSubmitted = true;
      this.showScoreInput = false;
		
    },
    
    restartMatchGame() {
      this.startMatchGame();
    },
    
    exitMatchGame() {
      this.matchGameStarted = false;
      this.matchGameFinished = false;
      clearInterval(this.gameTimerId);
    },
    
    shuffleArray(array) {
      for (let i = array.length - 1; i > 0; i--) {
        const j = Math.floor(Math.random() * (i + 1));
        [array[i], array[j]] = [array[j], array[i]];
      }
      return array;
    }
  }
}
</script>

<style scoped>
.games-intro {
  background-color: rgba(255, 255, 255, 0.7) !important;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.05);
  border-radius: 8px;
}

.game-card {
  background-color: white;
  border-radius: 12px;
  overflow: hidden;
  box-shadow: 0 5px 15px rgba(0, 0, 0, 0.08);
  transition: all 0.3s ease;
  border: none;
}

.game-card:hover {
  transform: translateY(-10px);
  box-shadow: 0 15px 30px rgba(0, 0, 0, 0.12);
}

.game-card-header {
  background-color: #395eab;
  color: white;
  padding: 20px;
  position: relative;
}

.coming-soon .game-card-header {
  background-color: #6c757d;
}

.game-icon {
  background-color: white;
  color: #395eab;
  width: 60px;
  height: 60px;
  border-radius: 50%;
  display: flex;
  align-items: center;
  justify-content: center;
  font-size: 28px;
  margin-bottom: 15px;
  box-shadow: 0 5px 15px rgba(0, 0, 0, 0.1);
}

.coming-soon .game-icon {
  color: #6c757d;
}

.game-title {
  font-size: 22px;
  margin: 0;
  font-weight: 600;
}

.coming-soon-badge {
  position: absolute;
  top: 15px;
  right: 15px;
  font-weight: normal;
}

.game-card-body {
  padding: 20px;
}

.game-description {
  color: #555;
  font-size: 16px;
  line-height: 1.6;
}

.game-button {
  border-radius: 30px;
  padding: 10px 25px;
  font-weight: 500;
  transition: all 0.3s ease;
}

.game-button:hover {
  transform: scale(1.05);
}

.quiz-container {
  background-color: white;
  border-radius: 12px;
  padding: 25px;
  box-shadow: 0 5px 15px rgba(0, 0, 0, 0.08);
}

.quiz-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  flex-wrap: wrap;
}

.quiz-title {
  color: #395eab;
  margin: 0;
  font-size: 22px;
  font-weight: 600;
}

.quiz-progress {
  width: 200px;
}

.progress {
  height: 10px;
  background-color: #e9ecef;
}

.progress-bar {
  background-color: #395eab;
  transition: width 0.3s ease;
}

.quiz-question-container {
  background-color: #f8f9fa;
  padding: 20px;
  border-radius: 8px;
  margin-bottom: 20px;
}

.quiz-question h4 {
  color: #212529;
  margin-bottom: 20px;
  font-weight: 600;
}

.quiz-options {
  margin-bottom: 25px;
}

.quiz-option {
  margin-bottom: 15px;
  padding: 10px 15px;
  border-radius: 8px;
  transition: all 0.2s ease;
  cursor: pointer;
  background-color: white;
  border: 1px solid #dee2e6;
}

.quiz-option:hover {
  background-color: #e9ecef;
}

.quiz-option input {
  cursor: pointer;
}

.quiz-option label {
  cursor: pointer;
  width: 100%;
  margin-bottom: 0;
  padding-left: 5px;
}

.correct-option {
  background-color: #d4edda !important;
  color: #155724;
  border-color: #c3e6cb !important;
}

.incorrect-option {
  background-color: #f8d7da !important;
  color: #721c24;
  border-color: #f5c6cb !important;
}

.quiz-actions {
  display: flex;
  justify-content: flex-end;
}

.quiz-feedback {
  padding: 10px 15px;
  border-radius: 8px;
  font-weight: 500;
}

.quiz-feedback.correct {
  background-color: #d4edda;
  color: #155724;
}

.quiz-feedback.incorrect {
  background-color: #f8d7da;
  color: #721c24;
}

.quiz-feedback i {
  font-size: 20px;
}

.quiz-results {
  text-align: center;
  padding: 30px 20px;
}

.quiz-results-header {
  margin-bottom: 20px;
}

.quiz-trophy, .quiz-smile, .quiz-book {
  font-size: 50px;
  margin-bottom: 15px;
  display: block;
}

.quiz-trophy {
  color: #ffc107;
}

.quiz-smile {
  color: #28a745;
}

.quiz-book {
  color: #17a2b8;
}

.quiz-score {
  font-size: 18px;
  margin-bottom: 15px;
}

.score-value {
  font-weight: 700;
  color: #395eab;
  font-size: 24px;
}

.quiz-result-message {
  font-size: 16px;
  max-width: 450px;
  margin: 0 auto;
  color: #555;
}

.match-game-container {
  margin-bottom: 40px;
}

.match-game-title {
  font-weight: 600;
  color: #395eab;
  margin-bottom: 16px;
}

.match-game-info {
  display: flex;
  justify-content: space-between;
  align-items: center;
  border-radius: 8px;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.05);
}

.timer-value {
  font-weight: 600;
  color: #395eab;
  font-size: 1.1rem;
}

.match-game-board {
  margin-bottom: 30px;
}

.match-card {
  position: relative;
  height: 140px;
  cursor: pointer;
  perspective: 1000px;
  transition: transform 0.3s;
}

.match-card:hover {
  transform: translateY(-5px);
}

.match-card.flipped .match-card-front,
.match-card.matched .match-card-front {
  transform: rotateY(180deg);
}

.match-card.flipped .match-card-back,
.match-card.matched .match-card-back {
  transform: rotateY(0deg);
}

.match-card.matched {
  box-shadow: 0 0 15px rgba(40, 167, 69, 0.7);
  pointer-events: none;
}

.match-card-front,
.match-card-back {
  position: absolute;
  width: 100%;
  height: 100%;
  backface-visibility: hidden;
  transition: transform 0.6s;
  border-radius: 8px;
  overflow: hidden;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
}

.match-card-front {
  background-color: #395eab;
  color: white;
  display: flex;
  align-items: center;
  justify-content: center;
  font-size: 2rem;
}

.match-card-back {
  background-color: white;
  transform: rotateY(180deg);
  display: flex;
  align-items: center;
  justify-content: center;
  padding: 10px;
}

.match-card-image {
  width: 100%;
  height: 100%;
  display: flex;
  align-items: center;
  justify-content: center;
}

.match-card-image img {
  max-width: 100%;
  max-height: 100%;
  object-fit: cover;
}

.match-card-name {
  font-weight: 600;
  color: #333;
  font-size: 1rem;
  text-align: center;
}

.match-game-results {
  background-color: white;
  border-radius: 8px;
  padding: 30px;
  box-shadow: 0 4px 12px rgba(0, 0, 0, 0.1);
  text-align: center;
}

.results-header {
  margin-bottom: 24px;
}

.results-header i {
  font-size: 3rem;
  color: gold;
  margin-bottom: 16px;
}

.results-header h4 {
  font-weight: 600;
  color: #395eab;
  font-size: 1.5rem;
}

.results-message {
  font-size: 1.2rem;
  color: #555;
}

.time-result {
  font-weight: 600;
  color: #395eab;
}

.leaderboard h5 {
  margin-bottom: 16px;
  color: #395eab;
}

.leaderboard table {
  border-radius: 8px;
  overflow: hidden;
}

@media (max-width: 767px) {
  .match-card {
    height: 120px;
  }
  
  .match-card-name {
    font-size: 0.9rem;
  }
}

@media (max-width: 576px) {
  .match-card {
    height: 100px;
  }
  
  .match-card-name {
    font-size: 0.8rem;
  }
  
  .match-game-info {
    flex-direction: column;
    gap: 10px;
  }
}
</style> 
