<template>
    <button class="btn btn-primary button" style="margin-left: 5%;" @click="handleBack()">Back</button>
    <div class="container mt-5">
        <div class="alert alert-success text-center">
            <h3>Booking Confirmed!</h3>
            <p>Your booking reference ID is: <strong>{{ bookingStore.Id }}</strong></p>
        </div>
        <div class="d-flex justify-content-center mt-4">
            <button class="btn btn-primary me-3">
                <i class="fas fa-envelope"></i> Download via Email
            </button>
            <button class="btn btn-secondary">
                <i class="fas fa-file-pdf"></i> Download PDF
            </button>
        </div>
    </div>
</template>

<script>
import { useBookingStore } from '@/store/bookingStore';

export default {
    name: "ConfirmationPage",
    data() {
        return {
            bookingStore: useBookingStore(),
        };
    },
    methods: {
        handleBack() {
            this.bookingStore.resetDetails();
            this.$router.push('/');
        },
    }
};
</script>

<style>
.container {
    max-width: 600px;
}
</style>