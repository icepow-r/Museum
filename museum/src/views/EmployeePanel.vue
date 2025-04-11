<template>
	<div id="container-login" class="container-fluid" v-if="this.token">
		<div class="confirmation-modal" v-if="showConfirmation">
			<div class="confirmation-content">
				<h4>Подтверждение удаления</h4>
				<p>Вы уверены, что хотите удалить экспонат "{{ confirmationExhibitName }}"?</p>
				<div class="confirmation-buttons">
					<button class="btn bg-white border" @click="cancelDelete">Отмена</button>
					<button class="btn btn-danger" @click="confirmDelete">Удалить</button>
				</div>
			</div>
		</div>
		<div class="modal-backdrop" v-if="showConfirmation"></div>
		
		<div class="notification-toast" v-if="showNotification" :class="notificationType">
			<div class="notification-content">
				<p>{{ notificationMessage }}</p>
				<button class="close-notification" @click="closeNotification">×</button>
			</div>
		</div>
		
		<div class="row align-items-start g-2">
			<div id="employee-actions" class="col-3 sticky-top">
				<div class="p-3 border rounded bg-light">
					<div class="p-2" id="mode-picker">
						<input v-model="mode" type="radio" id="mode-picker-add" value="add"/>
						<label for="mode-picker-add">Добавить экспонат</label>
						<br/>
						<input v-model="mode" type="radio" id="mode-picker-edit" value="edit"/>
						<label for="mode-picker-edit">Редактировать экспонат</label>
					</div>
					<div v-if="this.mode == 'add'">
					<hr/>
					<form @submit.prevent="add_exhibit" enctype="multipart/form-data" id="exhibit_add" class="mb-3">
						<div class="mb-3">
							<label for="exhibit_add_name" class="form-label">Название</label>
							<input id="exhibit_add_name" v-model="exhibit_add_name" class="form-control"/>	
						</div>
						<div class="mb-3">
							<label for="exhibit_add_desc" class="form-label">Описание</label>
							<textarea id="exhibit_add_desc" v-model="exhibit_add_desc" class="form-control" rows="3"></textarea>	
						</div>
						<div class="mb-3">
							<label for="exhibit_add_collection" class="form-label">Коллекция</label>
							<input id="exhibit_add_collection" v-model="exhibit_add_collection" class="form-control"/>	
						</div>
						<div class="mb-3">
							<label for="exhibit_add_era" class="form-label">Эпоха</label>
							<input id="exhibit_add_era" v-model="exhibit_add_era" class="form-control"/>	
						</div>
						<div class="mb-3">
							<label for="exhibit_add_images" class="form-label">Изображения</label>
							<input id="exhibit_add_images" type="file" ref="files" multiple @change="file_upload" class="form-control"/>
							<div class="mt-2" v-if="exhibit_add_images.length > 0">
								<p class="text-success">Выбрано файлов: {{ exhibit_add_images.length }}</p>
								<button type="button" class="btn btn-sm btn-outline-secondary" @click="resetImages">Очистить изображения</button>
							</div>
						</div>
						<button class="btn btn-primary w-100" id="exhibit_add_submit" type="submit">Отправить</button>
					</form>
					</div>

					<div v-else-if="this.mode='edit'">
					<hr/>
					<form @submit.prevent="edit_exhibit" enctype="multipart/form-data" id="exhibit_add" class="mb-3">
						<div class="mb-3">
							<label for="exhibit_edit_id" class="form-label">ID</label>
							<input id="exhibit_edit_id" v-model="exhibit_edit_id" class="form-control"/>	
						</div>
						<div class="mb-3">
							<label for="exhibit_edit_name" class="form-label">Новое название</label>
							<input id="exhibit_edit_name" v-model="exhibit_edit_name" class="form-control"/>	
						</div>
						<div class="mb-3">
							<label for="exhibit_edit_desc" class="form-label">Новое описание</label>
							<textarea id="exhibit_edit_desc" v-model="exhibit_edit_desc" class="form-control" rows="3"></textarea>	
						</div>
						<div class="mb-3">
							<label for="exhibit_edit_collection" class="form-label">Коллекция</label>
							<input id="exhibit_edit_collection" v-model="exhibit_edit_collection" class="form-control"/>	
						</div>
						<div class="mb-3">
							<label for="exhibit_edit_era" class="form-label">Эпоха</label>
							<input id="exhibit_edit_era" v-model="exhibit_edit_era" class="form-control"/>	
						</div>
						<button class="btn btn-primary w-100" id="exhibit_edit_submit" type="submit">Обновить</button>
					</form>
					</div>
				</div>
			</div>
			<div id="exhibits" class="col-9">
				<div class="p-3 border rounded bg-light">
					<div class="exhibit-wrapper container-fluid">
						<div class="row row-cols-1">
							<div class="card exhibit-card p-2 m-2" v-for="ob in this.exhibits">
							  <div class="row">
							    <div class="col-md-5">
							      <div class="exhibit-image-container">
								    <img v-if="ob.images && ob.images.length > 0" :src="ob.images[0].imagePath" class="card-img-top" :alt="ob.images[0].altText">
							      </div>
							      <div class="mt-2">
							        <button class="btn btn-danger w-100 mb-2" @click="remove_exhibit(ob.exhibitId)">Удалить</button>
							        <div class="badge bg-secondary d-block">{{ ob.collection || 'Без коллекции' }}</div>
							      </div>
							    </div>
							    <div class="col-md-7">
							      <div class="card-body">
								    <h5 class="card-title">{{ ob.title }} <span class="badge bg-light text-dark">ID: {{ ob.exhibitId }}</span></h5>
								    <hr/>
								    <p class="card-text exhibit-year"><i class="bi bi-calendar3 me-1"></i> {{ ob.addedAt.slice(0,-1).split("T").join(" ") }}</p>
								    <p class="card-text exhibit-era" v-if="ob.era"><i class="bi bi-clock-history me-1"></i> {{ ob.era }}</p>
								    <hr/>
								    <p class="card-text">{{ ob.description }}</p>
							      </div>
							    </div>
							  </div>
							</div>
						</div>
					</div>
				</div>
			</div>
		</div>
	</div>

	<div id="container-nologin" v-else>
		<h3>Просмотр страницы требует авторизации</h3>
		<br/> <br/>
		<h5>Войдите в аккаунт работника музея</h5>
	</div>
</template>

<script>
export default {
  name: 'EmployeePanel',
  data() {
    return {
		token: undefined,
		mode: "",
		exhibits: [],
		exhibit_add_name: "",
		exhibit_add_desc: "",
		exhibit_add_collection: "",
		exhibit_add_era: "",
		exhibit_add_loadorder: 1,
		exhibit_add_images: [],
		exhibit_edit_name: "",
		exhibit_edit_desc: "",
		exhibit_edit_collection: "",
		exhibit_edit_era: "",
		exhibit_edit_id: undefined,
		showConfirmation: false,
		confirmationExhibitName: "",
		exhibitToDelete: null,
		showNotification: false,
		notificationMessage: "",
		notificationType: "",
    }
  },
  computed: {
  },
  methods: {
	  file_upload(e) {
		this.exhibit_add_images = [];
		
		for (let i = 0; i < e.target.files.length; i++) {
			const fileExt = e.target.files[i].name.split(".").pop()
			const altText = this.exhibit_add_name || 'Изображение экспоната'
			const reader = new FileReader();
			const fileByteArray = [];
			reader.readAsArrayBuffer(e.target.files[i])
			reader.onloadend = (evt) => {
				if (evt.target.readyState === FileReader.DONE) {
				  const arrayBuffer = evt.target.result,
					array = new Uint8Array(arrayBuffer);
				  for (const a of array) {
					fileByteArray.push(a);
				}
			  }
				this.exhibit_add_images.push({imageExt: fileExt, altText: altText, byteArray: fileByteArray })
			};
		}
	  },
	  resetImages() {
		this.exhibit_add_images = [];
		if (this.$refs.files) {
			this.$refs.files.value = '';
		}
	  },
	  async fetch_exhibits() {
		  const response_raw = await fetch("http://" + import.meta.env.VITE_API_LINK + "/api/Exhibits", {
			method: "GET",
			headers: {
				"accept": "text/plain",
			},
		  });
		  const response = await response_raw.json()

		  for (let i = 0; i < response.length; i++) {
			for (let j = 0; j < response[i].images.length; j++) {
			  response[i].images[j].imagePath = "http://" + import.meta.env.VITE_API_LINK + response[i].images[j].imagePath
			}
		  }
		  this.exhibits = response
	  },
	  async remove_exhibit(id) {
		  const exhibit = this.exhibits.find(ex => ex.exhibitId === id);
		  
		  this.exhibitToDelete = id;
		  this.confirmationExhibitName = exhibit.title;
		  this.showConfirmation = true;
	  },
	  confirmDelete() {
		  this.deleteExhibit();
		  this.showConfirmation = false;
	  },
	  cancelDelete() {
		  this.showConfirmation = false;
		  this.exhibitToDelete = null;
	  },
	  async deleteExhibit() {
		  if (!this.exhibitToDelete) return;
		  
		  const response_raw = await fetch("http://" + import.meta.env.VITE_API_LINK + "/api/Exhibits/" + this.exhibitToDelete, {
			method: "DELETE",
			headers: {
				"accept": "text/plain",
				"Authorization": "Bearer " + this.token,
			},
		  });
		  await this.fetch_exhibits();
		  const response = await response_raw.json();
		  this.exhibitToDelete = null;
	  },
	  clear_add_form() {
		  this.exhibit_add_name = ""
		  this.exhibit_add_desc = ""
		  this.exhibit_add_collection = ""
		  this.exhibit_add_era = ""
		  this.exhibit_add_images = []
		  if (this.$refs.files) {
			  this.$refs.files.value = '';
		  }
	  },
	  async add_exhibit() {
		  if (this.exhibit_add_name == "" || 
			  this.exhibit_add_desc == "" || 
			  this.exhibit_add_images.length == 0) {
			  this.showNotificationMessage("Заполните все обязательные поля и добавьте изображения", "error");
			  return -1;
		  }
		  if (this.exhibit_add_images.length > 4) { 
			  this.showNotificationMessage("Слишком много изображений (>4)", "error");
			  return -1; 
		  }
		  
		  try {
			  const response_raw = await fetch("http://" + import.meta.env.VITE_API_LINK + "/api/Exhibits", {
				method: "POST",
				headers: {
					"accept": "application/json",
					"Content-Type": "application/json",
					"Authorization": "Bearer " + this.token,
				},
				body: JSON.stringify({
					title: this.exhibit_add_name,
					desc: this.exhibit_add_desc,
					collection: this.exhibit_add_collection,
					era: this.exhibit_add_era,
					loadOrder: this.exhibit_add_loadorder,
					images: this.exhibit_add_images,
				}),
			  });
			  
			  if (response_raw.ok) {
				  const response = await response_raw.json();
		 		  this.showNotificationMessage("Экспонат успешно добавлен", "success");
				  this.clear_add_form();
				  await this.fetch_exhibits();
			  } else {
				  const errorData = await response_raw.json();
				  this.showNotificationMessage("Ошибка при добавлении экспоната: " + (errorData.error || "Неизвестная ошибка"), "error");
				  this.resetImages();
			  }
		  } catch (error) {
			  this.showNotificationMessage("Ошибка соединения с сервером", "error");
			  this.resetImages();
		  }
	  },
	  clear_edit_form() {
		  this.exhibit_edit_name = ""
		  this.exhibit_edit_desc = ""
		  this.exhibit_edit_collection = ""
		  this.exhibit_edit_era = ""
		  this.exhibit_edit_id = undefined
	  },
	  async edit_exhibit() {
		  if (this.exhibit_edit_name == "" || 
			  this.exhibit_edit_desc == "" || 
			  this.exhibit_edit_id == undefined) {
			  this.showNotificationMessage("Заполните все обязательные поля", "error");
			  return -1;
		  }
		  
		  try {
			  const response_raw = await fetch("http://" + import.meta.env.VITE_API_LINK + "/api/Exhibits/" + this.exhibit_edit_id, {
				method: "PUT",
				headers: {
					"accept": "application/json",
					"Content-Type": "application/json",
					"Authorization": "Bearer " + this.token,
				},
				body: JSON.stringify({
					title: this.exhibit_edit_name,
					description: this.exhibit_edit_desc,
					collection: this.exhibit_edit_collection,
					era: this.exhibit_edit_era
				}),
			  });
			  
			  if (response_raw.ok) {
				  const response = await response_raw.json();
				  this.showNotificationMessage("Экспонат успешно обновлен", "success");
				  this.clear_edit_form();
				  await this.fetch_exhibits();
			  } else {
				  const errorData = await response_raw.json();
				  this.showNotificationMessage("Ошибка при обновлении экспоната: " + (errorData.message || "Неизвестная ошибка"), "error");
			  }
		  } catch (error) {
			  this.showNotificationMessage("Ошибка соединения с сервером", "error");
		  }
	  },
	  showNotificationMessage(message, type) {
		  this.notificationMessage = message;
		  this.notificationType = type;
		  this.showNotification = true;
		  
		  setTimeout(() => {
			  this.closeNotification();
		  }, 5000);
	  },
	  closeNotification() {
		  this.showNotification = false;
	  }
  },
  async mounted() {
	if (localStorage.token) {
	  this.token = localStorage.token;
	}
	await this.fetch_exhibits()
  },
}
</script>

<style scoped>
div#container-nologin > h3, h5 {
	display: block;
	text-align: center;
	margin-left: auto;
	margin-right: auto;
}

div#employee-actions {
	top: 0.5em;
	z-index: 1;
}

.card-img-top {
	object-fit: cover;
}

div#mode-picker > label {
	margin-left: 0.5em;
}

.confirmation-modal {
	position: fixed;
	top: 50%;
	left: 50%;
	transform: translate(-50%, -50%);
	background-color: white;
	padding: 20px;
	border-radius: 5px;
	box-shadow: 0 0 10px rgba(0, 0, 0, 0.3);
	z-index: 9999;
	min-width: 300px;
}

.confirmation-content {
	text-align: center;
}

.confirmation-buttons {
	margin-top: 20px;
	display: flex;
	justify-content: space-around;
}

.modal-backdrop {
	position: fixed;
	top: 0;
	left: 0;
	width: 100%;
	height: 100%;
	background-color: rgba(0, 0, 0, 0.5);
	z-index: 9998;
}

.notification-toast {
	position: fixed;
	top: 20px;
	right: 20px;
	padding: 15px;
	border-radius: 5px;
	box-shadow: 0 0 10px rgba(0, 0, 0, 0.2);
	z-index: 9997;
	min-width: 300px;
	max-width: 400px;
	animation: slide-in 0.3s ease-out forwards;
}

@keyframes slide-in {
	0% { transform: translateX(100%); }
	100% { transform: translateX(0); }
}

.notification-toast.success {
	background-color: #d4edda;
	border: 1px solid #c3e6cb;
	color: #155724;
}

.notification-toast.error {
	background-color: #f8d7da;
	border: 1px solid #f5c6cb;
	color: #721c24;
}

.notification-content {
	display: flex;
	justify-content: space-between;
	align-items: center;
}

.notification-content p {
	margin: 0;
}

.close-notification {
	background: none;
	border: none;
	font-size: 20px;
	cursor: pointer;
	padding: 0 0 0 10px;
	color: inherit;
}

.exhibit-card {
	transition: transform 0.2s ease;
}

.exhibit-card:hover {
	transform: translateY(-5px);
	box-shadow: 0 5px 15px rgba(0,0,0,0.1);
}

.exhibit-image-container {
	position: relative;
	overflow: hidden;
	padding-top: 75%;
	height: 0;
	margin-bottom: 15px;
}

.exhibit-image-container img {
	position: absolute;
	top: 0;
	left: 0;
	width: 100%;
	height: 100%;
	object-fit: cover;
}

.exhibit-image-container button,
.exhibit-image-container .badge {
	position: relative;
	z-index: 1;
}
</style>
