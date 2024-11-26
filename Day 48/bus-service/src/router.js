import { createRouter, createWebHistory } from "vue-router";
import AuthForm from "./components/AuthForm.vue";
import AdminDashboard from "./components/DashBoard/AdminDashboard.vue";
import SearchBus from "./components/BusServices/SearchBus.vue";
import LandingPage from "./components/LandingPage.vue";
import SearchResult from "./components/BusServices/SearchResult.vue";
import SeatsSelection from "./components/BusServices/SeatsSelection.vue";





const routes = [
    { path: '/', component: LandingPage },
    { path: '/auth', component: AuthForm },
    { path: '/admindashboard', component: AdminDashboard },
    { path: '/search', name: 'SearchBus', component: SearchBus },
    { path: '/seats/:id', name: 'Seats', component: SeatsSelection },
    { path: '/searchResult', name: 'SearchResult', component: SearchResult }
];

const router = createRouter({
    history: createWebHistory(),
    routes
});

export default router;
