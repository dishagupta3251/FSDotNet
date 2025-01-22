<template>
    <button class="btn btn-primary button" style="margin-left: 5%;" @click="handleBack()">Back</button>
    <div class="form-container">
        <form @submit.prevent="submitForm">
            <div v-for="(passenger, index) in passengerList" :key="index">
                <h3>Passenger {{ index + 1 }}</h3>
                <div class="form-row">
                    <div class="form-group">
                        <label for="title">Title</label>
                        <select v-model="passenger.title" required>
                            <option value="" disabled selected>Select</option>
                            <option value="Mr.">Mr.</option>
                            <option value="Ms.">Ms.</option>
                            <option value="Mrs.">Mrs.</option>
                        </select>
                    </div>

                    <!-- First Name -->
                    <div class="form-group">
                        <label for="firstName">First Name</label>
                        <input type="text" v-model="passenger.firstName" placeholder="First Name" required />
                    </div>

                    <!-- Last Name -->
                    <div class="form-group">
                        <label for="lastName">Last Name</label>
                        <input type="text" v-model="passenger.lastName" placeholder="Last Name" required />
                    </div>
                </div>
            </div>

            <button type="submit" class="btn-submit">Submit</button>
        </form>
    </div>
</template>

<script>
import { useBookingStore } from '../store/bookingStore';
import { usePassengerStore } from '../store/passengerStore';

export default {
    data() {
        return {
            passengers: [],
            submitted: false,
            bookingStore: useBookingStore(),
            passengerStore: usePassengerStore()
        };
    },
    computed: {
        passengerList() {
            return this.passengers;
        },
    },
    methods: {
        submitForm() {
            this.submitted = true;
            this.passengerStore.setPassengers(this.passengers);
            console.log("Passengers:", this.passengerStore.passengers);
            console.log("Form Submitted", this.passengers);
            this.$router.push('/seat');
        },
        initializePassengers() {
            const numberOfPassengers = this.bookingStore.passengers;
            this.passengers = Array.from({ length: numberOfPassengers }, () => ({
                title: "",
                firstName: "",
                lastName: "",
            }));
        },
        handleBack() {
            this.$router.push('/result');
        },
    },
    mounted() {
        this.initializePassengers();
    },
};
</script>

<style scoped>
.form-container {
    max-width: 900px;
    margin: 0 auto;
}

h3 {
    margin-top: 20px;
    margin-bottom: 10px;
    color: #333;
}

.form-row {
    display: flex;
    gap: 15px;
    align-items: flex-start;
}

.form-group {
    display: flex;
    flex-direction: column;
    flex: 1;
}

label {
    font-weight: bold;
    margin-bottom: 5px;
}

input,
select {
    padding: 10px;
    border: 1px solid #ccc;
    border-radius: 4px;
    font-size: 14px;
}

.btn-submit {
    margin-top: 20px;
    width: 100px;
    padding: 10px;
    background-color: #007bff;
    color: white;
    border: none;
    border-radius: 4px;
    font-size: 16px;
    cursor: pointer;
}

.btn-submit:hover {
    background-color: #0056b3;
}

h2 {
    margin-top: 20px;
    color: #444;
}

p {
    margin: 5px 0;
}
</style>
