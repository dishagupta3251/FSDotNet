import { createApp } from 'vue'
import App from './App.vue'
import { createPinia } from 'pinia';
import routes from './scripts/router'


createApp(App).use(routes).use(createPinia()).mount('#app')
