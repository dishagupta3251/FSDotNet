<template>
    <h1>heelo



    </h1>
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
                <label for="expiryMonth" class="form-label">Expiry Month</label>
                <input type="number" id="expiryMonth" class="form-control" v-model="payment.expiryMonth"
                    @input="validateExpiryMonth" :class="{ 'is-invalid': errors.expiryMonth }" placeholder="MM" min="1"
                    max="12" required>
                <div class="invalid-feedback">{{ errors.expiryMonth }}</div>
            </div>
            <div class="mb-3">
                <label for="expiryYear" class="form-label">Expiry Year</label>
                <input type="number" id="expiryYear" class="form-control" v-model="payment.expiryYear"
                    @input="validateExpiryYear" :class="{ 'is-invalid': errors.expiryYear }" placeholder="YYYY"
                    :min="currentYear" required>
                <div class="invalid-feedback">{{ errors.expiryYear }}</div>
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
export default {
    name: 'PaymentDetails',
    data() {
        return {
            payment: {
                ccNumber: '',
                cvv: '',
                expiryMonth: '',
                expiryYear: ''
            },
            bookingReference: null,
            currentYear: new Date().getFullYear()
        };
    },
    methods: {
        validateCCNumber() {
            const regex = /^\d{16}$/;
            return regex.test(this.payment.ccNumber);
        },
        validateCVV() {
            const regex = /^\d{3}$/;
            return regex.test(this.payment.cvv);
        },
        validateExpiryMonth() {
            const month = parseInt(this.payment.expiryMonth, 10);
            return month >= 1 && month <= 12;
        },
        validateExpiryYear() {
            const year = parseInt(this.payment.expiryYear, 10);
            return year >= this.currentYear;
        },
        // async submitPayment() {
        //     const isCCNumberValid = this.validateCCNumber();
        //     const isCVVValid = this.validateCVV();
        //     const isExpiryMonthValid = this.validateExpiryMonth();
        //     const isExpiryYearValid = this.validateExpiryYear();

        //     // if (isCCNumberValid && isCVVValid && isExpiryMonthValid && isExpiryYearValid) {

        //     // } else {

        //     // }
        // }
    }
};
</script>

<style>
.is-invalid {
    border-color: #dc3545;
}
</style>