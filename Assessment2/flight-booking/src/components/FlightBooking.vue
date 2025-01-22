<template>
    <button class="btn btn-primary button" style="margin-left: 5%;" @click="handleBack()">Back</button>
    <h1>Flight Search</h1>
    <div class="main">
        <div class="container py-4">
            <div class="box shadow p-4 rounded bg-white">
                <div class="d-flex justify-content-center mb-3">
                    <div class="form-check form-check-inline">
                        <input type="radio" id="oneway" name="tripType" class="form-check-input" value="oneway"
                            v-model="bookingStore.trip" required />
                        <label class="form-check-label" for="oneway">One Way</label>
                    </div>
                    <div class="form-check form-check-inline">
                        <input type="radio" id="roundtrip" name="tripType" class="form-check-input" value="roundtrip"
                            v-model="bookingStore.trip" required />
                        <label class="form-check-label" for="roundtrip">Round Trip</label>
                    </div>
                </div>

                <form class="d-flex flex-row mb-3 gap-3" @submit.prevent="search">
                    <div class="col-md-2">
                        <label for="source" class="form-label">Source</label>
                        <select class="form-select" id="source" v-model="bookingStore.source" required>
                            <option value="" disabled>Select Source</option>
                            <option v-for="(source, index) in sources" :key="index" :value="source">{{ source }}
                            </option>
                        </select>
                    </div>

                    <div class="col-md-2">
                        <label for="destination" class="form-label">Destination</label>
                        <select class="form-select" id="destination" v-model="bookingStore.destination" required>
                            <option value="" disabled>Select Destination</option>
                            <option v-for="(destination, index) in destinations" :key="index" :value="destination">{{
                                destination }}</option>
                        </select>
                    </div>

                    <div class="col-md-2">
                        <label for="departureDate" class="form-label">Departure Date</label>
                        <input type="date" class="form-control" id="departureDate" :min="minDate"
                            v-model="bookingStore.departureDate" required />
                    </div>

                    <div class="col-md-2" v-if="showReturnDate">
                        <label for="returnDate" class="form-label">Return Date</label>
                        <input type="date" class="form-control" id="returnDate" :min="minDate"
                            v-model="bookingStore.returnDate" required />
                    </div>

                    <div class="col-md-2">
                        <label for="passengers" class="form-label">Passengers</label>
                        <select class="form-select" id="passengers" v-model="bookingStore.passengers" required>
                            <option value="" disabled>Select Passengers</option>
                            <option v-for="n in 10" :key="n" :value="n">{{ n }}</option>
                        </select>
                    </div>

                    <div class="col-md-1 d-flex align-items-end">
                        <button type="submit" class="btn btn-primary w-100">Search Flights</button>
                    </div>
                </form>
            </div>
        </div>
    </div>
</template>

<script>
import { Destination, Source } from "@/scripts/FlightBooking_Services";
import { useBookingStore } from "@/store/bookingStore";

export default {
    name: "FlightBooking",

    data() {
        return {
            bookingStore: useBookingStore(),
            sources: [],
            destinations: [],
            minDate: "",
        };
    },
    computed: {
        showReturnDate() {
            return this.bookingStore.trip === "roundtrip";
        },
    },
    methods: {
        fetchSource() {
            Source()
                .then((response) => {
                    this.sources = response.data;
                })
                .catch((error) => console.error(error));
        },
        fetchDestination(source) {
            Destination(source)
                .then((response) => {
                    this.destinations = response.data;
                })
                .catch((error) => console.error(error));
        },
        handleBack() {
            this.$router.push("/");
        },
        setMinDate() {
            const today = new Date();
            const yyyy = today.getFullYear();
            const mm = String(today.getMonth() + 1).padStart(2, "0");
            const dd = String(today.getDate()).padStart(2, "0");
            this.minDate = `${yyyy}-${mm}-${dd}`;
        },
        search() {
            console.log("Search triggered with:", this.bookingStore);
            this.$router.push("/result");
        },
    },
    watch: {
        "bookingStore.source"(newSource) {
            if (newSource) {
                this.fetchDestination(newSource);
            }
        },
    },
    mounted() {
        this.fetchSource();
        this.setMinDate();
    },
};
</script>

<style scoped>
h1 {
    text-align: center;
}

.main {
    max-width: 100%;
    margin-left: 5%;
}

.box {
    border: 1px solid #dee2e6;
    border-radius: 8px;
    background-color: #ffffff;
}

.shadow {
    box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
}

.container {
    max-width: 1200px;
    margin: 0 auto;
}
</style>
