<template>
    <CustomerNavbar />
    <div class="payment-page" style="margin-top: 100px;">

        <!-- Payment Options Section -->
        <div v-if="payment == false" class="payment-options">
            <div style="align-items: center;">
                <h2>Select Payment Method</h2>
            </div>
            <div style="margin-top: 50px;display: flex;margin-right: 300px;">
                <div>
                    <input type="radio" id="upi" value="0" v-model="paymentMethod" />
                    <label for="upi">UPI</label>
                </div>
                <div>
                    <input type="radio" id="card" value="1" v-model="paymentMethod" />
                    <label for="card">Card</label>
                </div>
                <div class="payment-footer">
                    <button @click="confirmPayment">Confirm Payment</button>
                </div>
            </div>


            <!-- Payment Form -->

        </div>

        <!-- Booking Details Section -->
        <div v-if="payment" class="booking-details">
            <h3>Booking Details</h3>
            <p><strong>Bus Number:</strong> {{ bookingDetails.busNumber }}</p>
            <p><strong>Source:</strong> {{ bookingDetails.source }}</p>
            <p><strong>Destination:</strong> {{ bookingDetails.destination }}</p>
            <p><strong>Booking Date:</strong> {{ bookingDetails.bookedForDate }}</p>
            <p><strong>Booking Day:</strong> {{ bookingDetails.bookedForDay }}</p>
            <p><strong>Seats Booked:</strong></p>
            <ul style="display: flex;">
                <li v-for="(seat, index) in bookingDetails.seatsBooked" :key="index">
                    Seat {{ seat.seat }} - ₹{{ seat.price }}
                </li>
            </ul>
            <p><strong>Total Fare:</strong> ₹{{ bookingDetails.totalFare }}</p>
            <p><strong>Confirmed:</strong> ₹ {{ bookingDetails.isConfirmed }}</p>
        </div>


    </div>
</template>

<script>
import { Payment } from '@/script/BookingService';
import CustomerNavbar from './CustomerNavbar.vue';



export default {
    name: 'PaymentPage',
    components: {
        CustomerNavbar
    },
    data() {
        return {
            bookingId: 0,
            paymentMethod: 0, // UPI or Card
            upiId: '',
            cardNumber: '',
            expiryDate: '',
            cvv: '',
            payment: false,
            customer: {},
            bookingDetails: {}
        };
    },

    methods: {
        confirmPayment() {
            Payment(this.bookingId, this.paymentMethod)
                .then((res) => {
                    this.bookingDetails = res.data;
                    this.payment = true;
                })
                .catch((err) => {
                    console.log(err);
                });
        }
    },
    details() {
        this.payment = true;
        // setTimeout(() => {
        //     router.push('/search')
        // }, 2000)
    },
    mounted() {
        this.customer = sessionStorage.getItem('customer');
        const { bookingId } = this.$route.query;
        this.bookingId = bookingId
        console.log(this.bookingId)
    }
};
</script>

<style scoped>
.payment-page {
    display: flex;
    flex-direction: column;
    align-items: center;
}

.payment-options {
    margin-top: 50px;
    display: flex;
    text-align: center;
}

.payment-options h2 {
    margin-bottom: 20px;
}

.payment-options div {
    margin-bottom: 10px;
}

.booking-details {
    margin-top: 30px;
    text-align: left;
    width: 60%;
}

.booking-details h3 {
    margin-bottom: 20px;
}

.booking-details ul {
    list-style-type: none;
    padding: 0;
}

.booking-details li {
    margin-bottom: 5px;
}

.payment-footer {
    margin-top: 50px;
}

button {
    padding: 10px 20px;
    background-color: green;
    color: white;
    border: none;
    border-radius: 5px;
    cursor: pointer;
}

button:hover {
    background-color: darkgreen;
}
</style>