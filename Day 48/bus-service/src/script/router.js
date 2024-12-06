import { createRouter, createWebHistory } from "vue-router";
import AuthForm from "@/components/AuthForm.vue";
import AdminDashboard from "@/components/Admin/AdminDashboard.vue";
import AllBuses from "@/components/Admin/AllBuses.vue";
import AllUsers from "@/components/Admin/AllUsers.vue";
import BusOperators from "@/components/Admin/BusOperators.vue";
import BusRoutes from "@/components/Admin/BusRoutes.vue";
import PaymentsHistory from "@/components/Admin/PaymentsHistory.vue";
import AllCustomers from "@/components/BusOperator/AllCustomers.vue";
import OperatorBuses from "@/components/BusOperator/OperatorBuses.vue";
import OperatorReviews from "@/components/BusOperator/OperatorReviews.vue";
import SearchBus from "@/components/CustomerServices/SearchBus.vue";
import SearchResult from "@/components/CustomerServices/SearchResult.vue";
import SeatsSelection from "@/components/CustomerServices/SeatsSelection.vue";
import BusOperatorDashboard from "@/components/BusOperator/BusOperatorDashboard.vue";
import DashboardLayout from "@/components/DashboardLayout.vue";
import UnauthorizedPage from "@/components/UnauthorizedPage.vue";
import { jwtDecode } from "jwt-decode";
import LandingPage from "@/components/LandingPage.vue";
import CustomerNavbar from "@/components/CustomerServices/CustomerNavbar.vue";
// import CustomerProfile from "@/components/CustomerServices/CustomerProfile.vue";
import BookingDetails from "@/components/CustomerServices/BookingDetails.vue";
import PaymentPage from "@/components/CustomerServices/PaymentPage.vue";
import BookingHistory from "@/components/CustomerServices/BookingHistory.vue";






const routes = [
    { path: '/', component: LandingPage, },
    { path: '/auth', component: AuthForm },
    { path: '/dashboard', component: DashboardLayout },
    { path: '/unauthorized', component: UnauthorizedPage },
    {
        path: '/admindashboard',
        component: AdminDashboard,
        meta: { requireAuth: true, role: "Admin" },
        children: [
            { path: "users", component: AllUsers },
            { path: "allbuses", component: AllBuses },
            { path: "operators", component: BusOperators },
            { path: "payments", component: PaymentsHistory },
            { path: "routes", component: BusRoutes },
        ],
    },
    {
        path: '/operatordashboard',
        component: BusOperatorDashboard,
        meta: { requireAuth: true, role: "BusOperator" },
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
        meta: { requireAuth: true, role: "Customer" },
    },
    {
        path: '/searchResult',
        name: 'SearchResult',
        component: SearchResult,

    },
    {
        path: '/seat/:id',
        name: 'Seats',
        component: SeatsSelection,
        meta: { requireAuth: true, role: "Customer" },
        props: (route) => ({
            id: route.params.id,
            seatsLeft: route.query.seatsLeft,
            totalSeats: route.query.totalSeats,
        }),
        children: [
            {
                path: 'bookingDetails',
                name: 'BookingDetails',
                component: BookingDetails,


            }
        ]
    },
    {
        path: '/customerNavbar',
        name: 'CustomerNavbar',
        component: CustomerNavbar,
        meta: { requireAuth: true, role: "Customer" },

    },
    // {
    //     path: "/profile",
    //     name: 'CustomerProfile',
    //     component: CustomerProfile
    // },
    {
        path: "/payment",
        name: 'PaymentPage',
        component: PaymentPage
    },
    {
        path: "/history",
        name: 'BookingHistory',
        component: BookingHistory
    }

];




const router = createRouter({
    history: createWebHistory(),
    routes
});

router.beforeEach((to, from, next) => {
    if (to.meta.requireAuth) {
        const token = sessionStorage.getItem("token");
        if (token) {
            const decoded = jwtDecode(token);
            const role =
                decoded["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];
            console.log(role);
            if (to.meta.role && to.meta.role !== role) {
                return next("/unauthorized");
            }
            next();
        } else {

            next("/auth");
        }
    } else {
        next();
    }
});



export default router;
