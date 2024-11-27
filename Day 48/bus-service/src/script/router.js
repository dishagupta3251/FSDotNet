import { createRouter, createWebHistory } from "vue-router";
import AuthForm from "@/components/AuthForm.vue";
import AdminDashboard from "@/components/Admin/AdminDashboard.vue";
import AllBuses from "@/components/Admin/AllBuses.vue";
import AdminUsers from "@/components/Admin/AdminUsers.vue";
import BusOperators from "@/components/Admin/BusOperators.vue";
import BusRoutes from "@/components/Admin/BusRoutes.vue";
import PaymentsHistory from "@/components/Admin/PaymentsHistory.vue";

import AllCustomers from "@/components/BusOperator/AllCustomers.vue";

import OperatorBuses from "@/components/BusOperator/OperatorBuses.vue";

import OperatorReviews from "@/components/BusOperator/OperatorReviews.vue";
import LandingPage from "@/components/LandingPage.vue";
import SearchBus from "@/components/CustomerServices/SearchBus.vue";
import SearchResult from "@/components/CustomerServices/SearchResult.vue";
import SeatsSelection from "@/components/CustomerServices/SeatsSelection.vue";
import BusOperatorDashboard from "@/components/BusOperator/BusOperatorDashboard.vue";
import DashboardLayout from "@/components/DashboardLayout.vue";



const routes = [
    { path: '/', component: LandingPage, },
    { path: '/auth', component: AuthForm },
    { path: '/dashboard', component: DashboardLayout },

    {
        path: '/admindashboard',
        component: AdminDashboard,
        meta: { requireAuth: true },
        children: [
            { path: "users", component: AdminUsers },
            { path: "allbuses", component: AllBuses },
            { path: "operators", component: BusOperators },
            { path: "payments", component: PaymentsHistory },
            { path: "routes", component: BusRoutes },
        ],
    },
    {
        path: '/operatordashboard',
        component: BusOperatorDashboard,
        meta: { requireAuth: true },
        children: [
            { path: "customers", component: AllCustomers },
            { path: "buses", component: OperatorBuses },
            { path: "reviews", component: OperatorReviews },
        ],
    },
    {
        path: '/search',
        name: 'SearchBus',
        component: SearchBus,
        meta: { requireAuth: true },
    },
    {
        path: '/searchResult',
        name: 'SearchResult',
        component: SearchResult,
        meta: { requireAuth: true },
    },
    {
        path: '/seat/:id',
        name: 'Seats',
        component: SeatsSelection,
        meta: { requireAuth: true },
    },
];




const router = createRouter({
    history: createWebHistory(),
    routes
});

router.beforeEach((to, from, next) => {
    const userRole = localStorage.getItem('role');

    if (to.meta.role && to.meta.role !== userRole) {
        alert('Access denied!');
        next('/');
    } else {
        next();
    }
});

export default router;
