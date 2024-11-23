import { createRouter, createWebHistory } from "vue-router";
import AuthForm from "./components/AuthForm.vue";
import CustomerDashboard from "./components/DashBoard/CustomerDashboard.vue";
import AdminDashboard from "./components/DashBoard/AdminDashboard.vue";
import SearchBus from "./components/SearchBus.vue";
import LandingPage from "./components/LandingPage.vue";





const routes = [
    { path: '/', component: LandingPage },
    { path: '/auth', component: AuthForm },
    { path: '/custdashboard', component: CustomerDashboard },
    { path: '/admindashboard', component: AdminDashboard },
    { path: '/search', component: SearchBus }


];

const router = createRouter({
    history: createWebHistory(),
    routes
});

export default router;
