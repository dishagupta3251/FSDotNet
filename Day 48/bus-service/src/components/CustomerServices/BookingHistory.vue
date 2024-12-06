<template>
    <CustomerNavbar />
    <h2 class="text-center mb-4" style="margin-top: 100px;">Booking Details</h2>
    <div class="booking-details gap-4 p-5 d-flex justify-content-center">

        <!-- Upcoming Bookings -->
        <div v-if="show" class="p-3" style="border: 2px solid green; border-radius: 10px;">
            <h3 class="section-title">Upcoming Bookings</h3>
            <div v-if="upcomingBookings.length > 0" class="bookings-container">
                <div v-for="(booking, index) in upcomingBookings" :key="index" class="booking-card upcoming"
                    style="border:1px solid blue; margin-bottom: 10px;">
                    <p>Bus: {{ booking.busNumber }}</p>
                    <p><strong>Journey Date:</strong> {{ booking.bookedForDate }}</p>
                    <p><strong>Seats:</strong> {{ booking.bookedSeats }}</p>
                    <p><strong>Total Fare:</strong> Rs. {{ booking.totalFare }}</p>
                    <p><strong>Status:</strong> {{ booking.isConfirmed }}</p>
                </div>
            </div>
            <p v-else class="no-bookings">No upcoming bookings found.</p>
        </div>

        <!-- Past Bookings -->
        <div v-if="show" class="p-3" style="border: 2px solid green; border-radius: 10px;">
            <h3 class="section-title">Past Bookings</h3>
            <div v-if="pastBookings.length > 0" class="bookings-container">
                <div v-for="(booking, index) in pastBookings" :key="index" class="booking-card past">
                    <h4>Bus: {{ booking.busNumber }}</h4>
                    <p><strong>Journey Date:</strong> {{ booking.journeyDate }}</p>
                    <p><strong>Seats:</strong> {{ booking.seats }}</p>
                    <p><strong>Total Fare:</strong> Rs. {{ booking.totalFare }}</p>
                </div>
            </div>
            <p v-else class="no-bookings">No past bookings found.</p>
        </div>
    </div>
    <DotLottieVue class="loading" v-if="!show" autoplay loop
        src="https://lottie.host/4480640a-0a4e-4a35-95bd-48e4dddb1690/IL1ds5D6Vz.lottie" />

</template>
<script>
import { History } from '@/script/BookingService';
import CustomerNavbar from './CustomerNavbar.vue';
import { DotLottieVue } from '@lottiefiles/dotlottie-vue';

export default {
    name: "BookingHistory",
    components: {
        CustomerNavbar,
        DotLottieVue
    },
    data() {
        return {
            pastBookings: [],
            upcomingBookings: [],
            id: 0,
            show: false
        };
    },
    methods: {
        fetchBookings(id) {
            History(id)
                .then((res) => {
                    console.log(res.data);
                    this.pastBookings = res.data.pastBookings

                    this.show = true;
                    this.upcomingBookings = res.data.upcomingBookings

                })
                .catch((err) => {
                    console.error("Error fetching booking data:", err);
                });
        },
    },

    mounted() {

        const customer = JSON.parse(sessionStorage.getItem("customer"));
        this.fetchBookings(customer.customerId)
    },
}


</script>

<style scoped>
.loading {
    height: 200px;
    width: 200px;
    margin-top: 30px;
    margin-left: 600px;
}
</style>