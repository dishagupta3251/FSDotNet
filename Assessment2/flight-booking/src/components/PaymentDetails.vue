<template>
    <button class="btn btn-primary button" style="margin-left: 5%;" @click="handleBack">Back</button>
    <div class="container mt-5">
        <h2 class="mb-4">Payment Details</h2>
        <form @submit.prevent="submitPayment">
            <div class="mb-3">
                <label for="ccNumber" class="form-label">Credit Card Number</label>
                <input type="text" id="ccNumber" class="form-control" v-model="payment.ccNumber"
                    @input="validateCCNumber" :class="{ 'is-invalid': errors.ccNumber }"
                    placeholder="Enter your credit card number" maxlength="16" required>
                <div class="invalid-feedback">{{ errors.ccNumber }}</div>
            </div>
            <div class="mb-3">
                <label for="cvv" class="form-label">CVV</label>
                <input type="text" id="cvv" class="form-control" v-model="payment.cvv" @input="validateCVV"
                    :class="{ 'is-invalid': errors.cvv }" placeholder="Enter CVV" maxlength="3" required>
                <div class="invalid-feedback">{{ errors.cvv }}</div>
            </div>
            <div class="mb-3">
                <label for="expiry" class="form-label">Expiry (MM/YY)</label>
                <input type="text" id="expiry" class="form-control" v-model="payment.expiry" @input="validateExpiry"
                    :class="{ 'is-invalid': errors.expiry }" placeholder="MM/YY" maxlength="5" required>
                <div class="invalid-feedback">{{ errors.expiry }}</div>
            </div>
            <button type="submit" class="btn btn-primary">Submit Payment</button>
        </form>

        <div v-if="bookingReference" class="mt-4 alert alert-success">
            <h5>Booking Successful!</h5>
            <p>Your booking reference number is: <strong>{{ bookingReference }}</strong></p>
        </div>
    </div>
</template>

<script>
import { AddSeats, Booking, Passengers } from '@/scripts/FlightBooking_Services';
import { useBookingStore } from '@/store/bookingStore';
import { usePassengerStore } from '@/store/passengerStore';
import { useSeatStore } from '@/store/seatsStore';

export default {
    name: 'PaymentDetails',
    data() {
        return {
            payment: {
                ccNumber: '',
                cvv: '',
                expiry: ''
            },
            errors: {
                ccNumber: '',
                cvv: '',
                expiry: ''
            },
            bookingReference: null,
            currentYear: new Date().getFullYear(),
            seatStore: useSeatStore(),
            bookingStore: useBookingStore(),
            passengerStore: usePassengerStore()
        };
    },
    computed: {
        isCCNumberInvalid() {
            return this.errors.ccNumber !== '';
        },
        isCVVInvalid() {
            return this.errors.cvv !== '';
        },
        isExpiryInvalid() {
            return this.errors.expiry !== '';
        }
    },
    methods: {
        validateCCNumber() {
            const regex = /^\d{16}$/;
            this.errors.ccNumber = regex.test(this.payment.ccNumber) ? '' : 'Invalid credit card number';
        },
        validateCVV() {
            const regex = /^\d{3}$/;
            this.errors.cvv = regex.test(this.payment.cvv) ? '' : 'Invalid CVV';
        },
        validateExpiry() {
            const regex = /^(0[1-9]|1[0-2])\/(\d{2})$/;
            if (regex.test(this.payment.expiry)) {
                const [month, year] = this.payment.expiry.split('/').map(Number);
                const fullYear = 2000 + year;
                if (month >= 1 && month <= 12 && fullYear >= this.currentYear) {
                    this.errors.expiry = '';
                } else {
                    this.errors.expiry = 'Invalid expiry date';
                }
            } else {
                this.errors.expiry = 'Invalid expiry format';
            }
        },
        generateReferenceNumber() {
            return Math.floor(Math.random() * 1000);
        },
        generateBookingReference() {
            const ref1 = this.generateReferenceNumber();
            const ref2 = this.generateReferenceNumber();
            return `BK${ref1}${ref2}`;
        },
        handleBack() {
            this.seatStore.resetSeats();
            this.$router.push("/seat");
        },
        async booking() {
            try {
                let response;
                if (this.bookingStore.trip === 'oneway') {
                    response = await Booking(
                        this.bookingReference,
                        this.bookingStore.trip,
                        this.bookingStore.selectedFlight.Id,
                        null,
                        this.bookingStore.selectedFlight.selectedType,
                        null,
                        this.bookingStore.totalPrice
                    );
                } else {
                    response = await Booking(
                        this.bookingReference,
                        this.bookingStore.trip,
                        this.bookingStore.selectedFlight.id,
                        this.bookingStore.selectedReturnFlight.id,
                        this.bookingStore.selectedFlight.selectedType,
                        this.bookingStore.selectedReturnFlight.selectedType,
                        this.bookingStore.totalPrice
                    );
                }
                console.log("Booking created successfully:", response.data);
            } catch (error) {
                console.error("Error creating booking:", error.response?.data || error);
                throw error;
            }
        },
        async addSeats() {
            const bookingId = this.bookingReference;
            const { departure, return: returnSeats } = this.seatStore.seats;
            for (const seat of departure) {
                try {

                    const response = await AddSeats(this.bookingStore.selectedFlight.Id, 'departure', seat, bookingId);
                    console.log(`Seat added successfully for departure: ${seat}`, response.data);
                } catch (error) {
                    console.error(`Failed to add seat for departure: ${seat}`, error);
                }
            }
            if (returnSeats.length > 0 && this.bookingStore.trip === 'roundtrip') {
                for (const seat of returnSeats) {
                    try {
                        const response = await AddSeats(this.bookingStore.selectedReturnFlight.Id, 'return', seat, bookingId);
                        console.log(`Seat added successfully for return: ${seat}`, response.data);
                    } catch (error) {
                        console.error(`Failed to add seat for return: ${seat}`, error);
                    }
                }
            }
        },
        async addPassengers() {
            const bookingId = this.bookingReference;
            const passengers = this.passengerStore.passengers;
            for (const passenger of passengers) {
                try {
                    const response = await Passengers(
                        bookingId,
                        passenger.title,
                        passenger.firstName,
                        passenger.lastName
                    );
                    console.log(`Passenger added successfully: ${passenger.firstName} ${passenger.lastName}`, response.data);
                } catch (error) {
                    console.error(`Failed to add passenger: ${passenger.firstName} ${passenger.lastName}`, error);
                }
            }
        },
        async submitPayment() {
            this.validateCCNumber();
            this.validateCVV();
            this.validateExpiry();
            if (!this.errors.ccNumber && !this.errors.cvv && !this.errors.expiry) {
                this.bookingReference = this.generateBookingReference();
                this.bookingStore.setId(this.bookingReference);
                try {
                    await this.booking();
                    await this.addPassengers();
                    await this.addSeats();
                    this.$router.push("/confirm");
                } catch (error) {
                    alert("Error completing the booking process. Please try again.");
                    console.error(error);
                }
            } else {
                alert('Please fix the errors in the form.');
            }
        }
    },
    mounted() {
        console.log(this.bookingStore.selectedFlight.Id);
    }
};
</script>



<style>
.is-invalid {
    border-color: #dc3545;
}
</style>
