import { createApp } from 'vue';
import "primeicons/primeicons.css";
import App from './App.vue';
import router from './script/router';
import 'bootstrap/dist/css/bootstrap.min.css';
import 'bootstrap';





const app = createApp(App);
app.use(router);

app.mount('#app');
