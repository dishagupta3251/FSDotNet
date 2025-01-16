<template>
    <button class="btn btn-primary button" style="margin-left: 5%;" @click="handleBack()">Back</button>
    <h1>Flight Search</h1>
    <div class="main">
        <div class="container py-4">
            <div class="box shadow p-4 rounded bg-white">
                <div class="d-flex justify-content-center mb-3">
                    <div class="form-check form-check-inline">
                        <input type="radio" id="oneway" name="tripType" class="form-check-input" value="oneway"
                            v-model="trip" checked required />
                        <label class="form-check-label" for="oneway">One Way</label>
                    </div>
                    <div class="form-check form-check-inline">
                        <input type="radio" id="roundtrip" name="tripType" class="form-check-input" value="roundtrip"
                            v-model="trip" required />
                        <label class="form-check-label" for="roundtrip">Round Trip</label>
                    </div>
                </div>

                <form class="d-flex flex-row mb-3 gap-3" @submit.prevent="search">
                    <div class="col-md-2">
                        <label for="source" class="form-label">Source</label>
                        <select class="form-select" id="source" v-model="source" required>
                            <option value="" disabled selected>Select Source</option>
                            <option v-for="(source, index) in sources" :key="index" :value="source">{{ source }}
                            </option>
                        </select>
                    </div>

                    <div class="col-md-2">
                        <label for="destination" class="form-label">Destination</label>
                        <select class="form-select" id="destination" v-model="destination" required>
                            <option value="" disabled selected>Select Destination</option>
                            <option v-for="(destination, index) in destinations" :key="index" :value="destination">{{
                                destination }}</option>
                        </select>
                    </div>

                    <div class="col-md-2">
                        <label for="departureDate" class="form-label">Departure Date</label>
                        <input type="date" class="form-control" id="departureDate" :min="minDate"
                            v-model="departureDate" required />
                    </div>

                    <div class="col-md-2" v-if="!disable_return">
                        <label for="returnDate" class="form-label">Return Date</label>
                        <input type="date" class="form-control" id="returnDate" :min="minDate" v-model="returnDate"
                            required />
                    </div>

                    <div class="col-md-2">
                        <label for="passengers" class="form-label">Passengers</label>
                        <select class="form-select" id="passengers" v-model="passengers" required>
                            <option value="" disabled selected>Select Passengers</option>
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
    <div class="footer">
        <FooterDetails />
    </div>
</template>

<script>
import { Destination, Source } from "@/scripts/FlightBooking_Services";
import FooterDetails from "./FooterDetails.vue";

export default {
    name: "FlightBooking",
    components: {
        FooterDetails
    },
    data() {
        return {
            sources: [],
            destinations: [],
            minDate: "",
            trip: "oneway",
            disable_return: true,
            source: "",
            destination: "",
            departureDate: "",
            returnDate: "",
            passengers: "",
        };
    },
    methods: {
        fetchSource() {
            Source()
                .then((response) => {
                    this.sources = response.data;
                    console.log(this.sources)
                })
                .catch((error) => console.error(error));
        },
        fetchDestination(source) {
            Destination(source)
                .then((response) => {
                    this.destinations = response.data;
                    console.log(this.destinations)
                })
                .catch((error) => {
                    console.error(error)

                });

        },
        handleBack() {
            this.$router.push('/')
        },
        setMinDate() {
            const today = new Date();
            const yyyy = today.getFullYear();
            let mm = today.getMonth() + 1;
            let dd = today.getDate();

            if (mm < 10) mm = "0" + mm;
            if (dd < 10) dd = "0" + dd;

            this.minDate = `${yyyy}-${mm}-${dd}`;
        },
        search() {
            sessionStorage.setItem('source', this.source);
            sessionStorage.setItem('destination', this.destination);
            sessionStorage.setItem('departureDate', this.departureDate);
            sessionStorage.setItem('returnDate', this.returnDate);
            sessionStorage.setItem('passengers', this.passengers);
            this.$router.push('/result');
        },
    },
    watch: {
        source(newSource) {
            if (newSource) {
                this.fetchDestination(newSource);
            }
        },
        trip(newTrip) {
            if (newTrip === "oneway") {
                this.disable_return = true;
            } else if (newTrip === "roundtrip") {
                this.disable_return = false;
            }
        }
    },
    mounted() {
        this.fetchSource();
        this.setMinDate();
    }
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

.field {
    display: flex;
    align-items: center;
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