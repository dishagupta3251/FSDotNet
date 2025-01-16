
import { createRouter, createWebHistory } from 'vue-router';
import DashboardPage from "@/components/DashboardPage.vue";
import EditCustomer from '@/components/EditCustomer.vue';
import AuthPage from '@/components/AuthPage.vue';
import AddCustomer from '@/components/AddCustomer.vue';
import UnauthorisedPage from '@/components/UnauthorisedPage.vue';
const routes = [
    { path: '/', component: AuthPage },
    { path: '/dashboard', component: DashboardPage },
    {
        path: '/edit', component: EditCustomer,
        meta: { requireAuth: true, role: ["ZoneManager", "AccountManager", "BranchManager"] },
    },
    {
        path: '/add', component: AddCustomer,
        meta: { requireAuth: true, role: ["ZoneManager", "AccountManager", "BranchManager"] },
    },
    {
        path: '/unauthorized', component: UnauthorisedPage
    }

]

const router = createRouter({
    history: createWebHistory(),
    routes
});


router.beforeEach((to, from, next) => {
    if (to.meta.requireAuth) {
        const token = sessionStorage.getItem("token");
        if (token) {
            const payload = JSON.parse(atob(token.split(".")[1]));
            const role = payload["Roles"];
            sessionStorage.setItem("role", role);
            console.log(role);
            if (to.meta.role && !to.meta.role.includes(role)) {
                return next("/unauthorized");
            }
            next();
        } else {

            next("/");
        }
    } else {
        next();
    }
});


export default router;