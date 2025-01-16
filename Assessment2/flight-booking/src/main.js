import { createApp } from 'vue'
import App from './App.vue'
import routes from './scripts/router'

createApp(App).use(routes).mount('#app')
