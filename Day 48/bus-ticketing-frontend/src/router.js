import { createRouter, createWebHistory } from "vue-router";
import HomePage from "@/components/HomePage.vue"
import AuthForm from "./components/AuthForm.vue";
import CustomerDashboard from "./components/CustomerDashboard.vue";
import AdminDashboard from "./components/AdminDashboard.vue";
import BaseNav from "./components/BaseNav.vue";




const routes = [
    { path: '/', component: HomePage },
    { path: '/auth', component: AuthForm },
    { path: '/custdashboard', component: CustomerDashboard },
    { path: '/admindashboard', component: AdminDashboard },
    { path: '/navbar', component: BaseNav }

];

const router = createRouter({
    history: createWebHistory(),
    routes
});

export default router;
