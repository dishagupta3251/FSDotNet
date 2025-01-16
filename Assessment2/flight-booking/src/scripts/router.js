import FlightBooking from "@/components/FlightBooking.vue"
import FormDetails from "@/components/FormDetails.vue";
import LandingPage from "@/components/LandingPage.vue";
import PaymentDetails from "@/components/PaymentDetails.vue";
import SearchResult from "@/components/SearchResult.vue";
import SeatMap from "@/components/SeatMap.vue";
import { createRouter, createWebHistory } from "vue-router"


const routes = [
    { path: '/', component: LandingPage },
    { path: '/search', component: FlightBooking },
    { path: '/result', component: SearchResult },
    { path: '/seat', component: SeatMap },
    {
        path: '/form', component: FormDetails
    },
    { path: '/seat', component: SeatMap },
    { path: '/payment', component: PaymentDetails }
]

const router = createRouter({
    history: createWebHistory(),
    routes
});

export default router

