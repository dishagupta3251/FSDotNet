<template>
    <div>
        <button class="btn btn-primary button" style="margin-left: 5%;" @click="handleBack">Back</button>
        <h3 class="text-center mb-4">Book Flights</h3>
        <div v-if="noFlightsFound" class="d-flex justify-content-center align-items-center" style="margin-top: 100px;">
            <div class="alert alert-warning text-center shadow p-4 rounded" role="alert">
                <h4 class="alert-heading"><strong>No Flights Found</strong></h4>
                <p>We couldn't find any flights matching your search criteria. Please try again with different options.
                </p>
                <hr>
            </div>
        </div>

        <div class="main d-flex justify-content-between" v-if="flightsAvailable">
            <!-- Departure Flights Section -->
            <div class="flights-section">
                <h5>Departure Flights</h5>
                <div v-for="(flight, index) in flights" :key="index" class="flight-box">
                    <div class="accordion" :id="`departureAccordion-${index}`">
                        <div class="accordion-item box shadow p-4 rounded">
                            <div class="accordion-header">
                                <div class="box shadow p-4 rounded" :class="{ 'highlighted': isSelectedFlight(index) }">
                                    <div class="d-flex justify-content-center mb-3">
                                        <div class="about-flight">
                                            <span>{{ flight.airline }}</span>
                                            <span><strong>{{ formatTime(flight.arrivalTime) }}</strong></span>
                                            <span><strong>{{ formatTime(flight.departureTime) }}</strong></span>
                                            <span>{{ flight.source }}</span>
                                            <span>{{ flight.destination }}</span>
                                            <span><strong>&#8377;{{ formattedFare(flight.fare) }}</strong></span>
                                            <button class="accordion-button collapsed" type="button"
                                                data-bs-toggle="collapse"
                                                :data-bs-target="`#departureCollapse-${index}`" aria-expanded="false"
                                                :aria-controls="`departureCollapse-${index}`"
                                                @click="selectFlight(index)">
                                            </button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div :id="`departureCollapse-${index}`" class="accordion-collapse collapse"
                                :data-bs-parent="`#departureAccordion-${index}`">
                                <div class="accordion-body">
                                    <div class="d-flex justify-content-center mb-3">
                                        <div class="form-check form-check-inline">
                                            <input type="radio" :id="`saver-${index}`" :name="`tripType-${index}`"
                                                class="form-check-input" value="saver" v-model="flight.selectedType"
                                                checked required />
                                            <div class="card" style="width: 18rem;">
                                                <div class="card-body">
                                                    <h5 class="card-title">Saver</h5>
                                                    <h6 class="card-subtitle mb-2 text-muted">Non-refundable</h6>
                                                    <h6 class="card-subtitle mb-2 text-muted">Cannot be rescheduled</h6>
                                                    <p class="card-text">
                                                        <strong>&#8377;{{ formattedFare(flight.fare) }}</strong>
                                                    </p>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-check form-check-inline">
                                            <input type="radio" :id="`flexi-${index}`" :name="`tripType-${index}`"
                                                class="form-check-input" value="flexi" v-model="flight.selectedType"
                                                required />
                                            <div class="card" style="width: 18rem;">
                                                <div class="card-body">
                                                    <h5 class="card-title">Flexi</h5>
                                                    <h6 class="card-subtitle mb-2 text-muted">Refundable</h6>
                                                    <h6 class="card-subtitle mb-2 text-muted">Can be rescheduled</h6>
                                                    <p class="card-text">
                                                        <strong>&#8377;{{ formattedFlexiFare(flight.fare) }}</strong>
                                                    </p>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="flights-section" v-if="isRoundTrip">
                <h5>Return Flights</h5>
                <div v-for="(returnFlight, returnIndex) in returnFlights" :key="returnIndex" class="flight-box">
                    <div class="accordion" :id="`returnAccordion-${returnIndex}`">
                        <div class="accordion-item box shadow p-4 rounded">
                            <div class="accordion-header">
                                <div class="box shadow p-4 rounded"
                                    :class="{ 'highlighted': isSelectedReturnFlight(returnIndex) }">
                                    <div class="d-flex justify-content-center mb-3">
                                        <div class="about-flight">
                                            <span>{{ returnFlight.airline }}</span>
                                            <span><strong>{{ formatTime(returnFlight.arrivalTime) }}</strong></span>
                                            <span><strong>{{ formatTime(returnFlight.departureTime) }}</strong></span>
                                            <span>{{ returnFlight.source }}</span>
                                            <span>{{ returnFlight.destination }}</span>
                                            <span><strong>&#8377;{{ formattedFare(returnFlight.fare) }}</strong></span>
                                            <button class="accordion-button collapsed" type="button"
                                                data-bs-toggle="collapse"
                                                :data-bs-target="`#returnCollapse-${returnIndex}`" aria-expanded="false"
                                                :aria-controls="`returnCollapse-${returnIndex}`"
                                                @click="selectReturnFlight(returnIndex)">
                                            </button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div :id="`returnCollapse-${returnIndex}`" class="accordion-collapse collapse"
                                :data-bs-parent="`#returnAccordion-${returnIndex}`">
                                <div class="accordion-body">
                                    <div class="d-flex justify-content-center mb-3">
                                        <div class="form-check form-check-inline">
                                            <input type="radio" :id="`returnSaver-${returnIndex}`"
                                                :name="`returnTripType-${returnIndex}`" class="form-check-input"
                                                value="saver" v-model="returnFlight.selectedType" checked required />
                                            <div class="card" style="width: 18rem;">
                                                <div class="card-body">
                                                    <h5 class="card-title">Saver</h5>
                                                    <h6 class="card-subtitle mb-2 text-muted">Non-refundable</h6>
                                                    <h6 class="card-subtitle mb-2 text-muted">Cannot be rescheduled</h6>
                                                    <p class="card-text">
                                                        <strong>&#8377;{{ formattedFare(returnFlight.fare) }}</strong>
                                                    </p>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-check form-check-inline">
                                            <input type="radio" :id="`returnFlexi-${returnIndex}`"
                                                :name="`returnTripType-${returnIndex}`" class="form-check-input"
                                                value="flexi" v-model="returnFlight.selectedType" required />
                                            <div class="card" style="width: 18rem;">
                                                <div class="card-body">
                                                    <h5 class="card-title">Flexi</h5>
                                                    <h6 class="card-subtitle mb-2 text-muted">Refundable</h6>
                                                    <h6 class="card-subtitle mb-2 text-muted">Can be rescheduled</h6>
                                                    <p class="card-text">
                                                        <strong>&#8377;{{ formattedFlexiFare(returnFlight.fare)
                                                            }}</strong>
                                                    </p>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </div>
        <div>
            <button class="btn btn-primary" :disabled="!canContinue" @click="continueBooking"
                style="margin-top: 30px; margin-left: 90%;">Continue</button>
        </div>

    </div>
    <div class="footer">
        <FooterDetails />
    </div>
    <h3>{{ bookingStore }}</h3>
</template>

<script>
import { Flights } from '@/scripts/FlightBooking_Services';
import { useBookingStore } from '@/store/bookingStore';
import FooterDetails from './FooterDetails.vue';

export default {
    name: 'SearchResult',
    components: {
        FooterDetails
    },
    data() {
        return {
            bookingStore: useBookingStore(),
            flights: [],
            returnFlights: [],
            selectedFlightIndex: null,
            selectedReturnFlightIndex: null
        };
    },
    computed: {
        noFlightsFound() {
            return this.flights.length === 0;
        },
        flightsAvailable() {
            return this.flights.length !== 0;
        },
        isRoundTrip() {
            return this.bookingStore.trip === 'roundtrip';
        },
        canContinue() {
            return this.selectedFlightIndex !== null;
        },
        formattedFare() {
            return (fare) => fare ? fare.toFixed(2) : '0.00';
        },
        formattedFlexiFare() {
            return (fare) => fare ? (fare * 2).toFixed(2) : '0.00';
        },
        isSelectedFlight() {
            return (index) => this.selectedFlightIndex === index;
        },
        isSelectedReturnFlight() {
            return (index) => this.selectedReturnFlightIndex === index;
        }
    },
    methods: {
        fetchFlightWithReturn() {
            Flights(this.bookingStore.source, this.bookingStore.destination, this.bookingStore.departureDate, this.bookingStore.returnDate)
                .then(response => {
                    this.flights = response.data.initial_flights;
                    this.returnFlights = response.data.return_flights;
                });
        },
        formatTime(datetime) {
            const date = new Date(datetime);
            const hours = date.getHours().toString().padStart(2, '0');
            const minutes = date.getMinutes().toString().padStart(2, '0');
            return `${hours}:${minutes}`;
        },
        handleBack() {
            this.bookingStore.resetDetails();
            this.$router.push('/search');
        },
        selectFlight(index) {
            this.selectedFlightIndex = index;
            if (!this.flights[index].selectedType) {
                this.flights[index].selectedType = 'saver';
            }
        },
        selectReturnFlight(index) {
            this.selectedReturnFlightIndex = index;
            if (!this.returnFlights[index].selectedType) {
                this.returnFlights[index].selectedType = 'saver';
            }
        },
        continueBooking() {
            this.bookingStore.selectedFlight = this.flights[this.selectedFlightIndex];
            this.bookingStore.selectedReturnFlight = this.returnFlights[this.selectedReturnFlightIndex];

            this.$router.push('/form');
        }
    },
    mounted() {
        this.fetchFlightWithReturn();
    }
};
</script>
<style scoped>
.main {
    display: flex;
    justify-content: space-between;
    margin: 20px;
}

.flights-section {
    flex: 1;
    margin: 0 10px;
}

.flight-box {
    margin-bottom: 20px;
}

.about-flight {
    display: flex;
    justify-content: space-between;
    align-items: center;
    font-size: 16px;
    color: #444;
    gap: 20px;
    padding: 10px 0;
}

.about-flight span {
    display: flex;
    align-items: center;
}

.highlighted {
    border: 3px solid #007bff;
    background-color: #f8f9fa;
    /* box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
    transition: border-color 0.3s, background-color 0.3s, box-shadow 0.3s; */
}

.accordion-item {
    margin-bottom: 20px;
    border: none;
}

.accordion-button {
    width: 0px;
    padding: 0px;
    margin-left: auto;
    background-color: transparent;
    border: none;
    box-shadow: none;
}

.accordion-button:focus {
    box-shadow: none;
}

.accordion-button.collapsed {
    background-image: none;

}

.margin150 {
    margin-left: 150px;
}

.margin20 {
    margin-left: 20px;
}

.box {
    margin: 15px 0;
    background-color: #fff;
    border-radius: 8px;
    box-shadow: 0 2px 5px rgba(0, 0, 0, 0.1);
    padding: 15px;
    transition: transform 0.3s, box-shadow 0.3s;
}

.box:hover {
    transform: translateY(-3px);
    box-shadow: 0 4px 10px rgba(0, 0, 0, 0.2);
}

.card {
    border: 1px solid #ddd;
    border-radius: 8px;
    overflow: hidden;
}

.card-body {
    padding: 15px;
    text-align: center;
}

.card-title {
    font-size: 18px;
    font-weight: bold;
}

.card-subtitle {
    font-size: 14px;
    color: #888;
}

.card-text {
    font-size: 16px;
    font-weight: bold;
    color: #444;
}

h5 {
    font-size: 20px;
    margin-bottom: 15px;
    font-weight: bold;
    text-align: left;
}

h3 {
    font-size: 24px;
    font-weight: bold;
    margin-bottom: 20px;
    text-align: center;
}

.btn {
    padding: 10px 20px;
    font-size: 16px;
    border-radius: 6px;
    transition: background-color 0.3s, box-shadow 0.3s;
}

.btn-primary {
    background-color: #007bff;
    border: none;
}

.btn-primary:hover {
    background-color: #0056b3;
    box-shadow: 0 4px 8px rgba(0, 0, 0, 0.2);
}

.footer {
    margin-top: 30px;
    padding: 20px;
    text-align: center;
    background-color: #f8f9fa;
    border-top: 1px solid #ddd;
}

button:disabled {
    background-color: #ccc;
    cursor: not-allowed;
}

button:not(:disabled) {
    cursor: pointer;
}
</style>