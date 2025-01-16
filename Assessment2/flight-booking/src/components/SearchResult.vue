<template>
    <button class="btn btn-primary button" style="margin-left: 5%;" @click="handleBack()">Back</button>
    <h3 style="text-align: center;">Book Flights</h3>

    <div v-if="flights.length === 0" class="d-flex justify-content-center align-items-center"
        style="margin-top: 100px;">
        <div class="alert alert-warning text-center shadow p-4 rounded" role="alert">
            <h4 class="alert-heading"><strong>No Flights Found</strong></h4>
            <p>We couldn't find any flights matching your search criteria. Please try again with different options.
            </p>
            <hr>
        </div>
    </div>
    <div class="main">
        <div class="flights">
            <div>
                <div v-for="(flight, index) in flights" :key="index">
                    <div class=" accordion" id="accordionExample">
                        <div class="accordion-item box shadow p-4 rounded " style="margin-left: 150px;">
                            <div class="accordion-header">
                                <div class="box shadow p-4 rounded "
                                    :class="{ 'highlighted': selectedFlightIndex === index }">
                                    <div class="d-flex justify-content-center mb-3">
                                        <div class="about-flight">
                                            <span>{{ flight.Airline }}</span>
                                            <span><strong>{{ formatTime(flight.ArrivalTime) }}</strong></span>
                                            <span><strong>{{ formatTime(flight.DepartureTime) }}</strong></span>
                                            <span>{{ flight.Source }}</span>
                                            <span>{{ flight.Destination }}</span>
                                            <span><strong>&#8377;{{ flight.Fare ? flight.Fare.toFixed(2) : '0.00'
                                                    }}</strong></span>
                                            <button class="accordion-button collapsed" type="button"
                                                data-bs-toggle="collapse" :data-bs-target="`#collapse` + index"
                                                aria-expanded="false" :aria-controls="`collapse` + index"
                                                @click="selectFlight(index)">
                                            </button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div :id="`collapse` + index" class="accordion-collapse collapse" data-bs-parent="collapse">
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
                                                        <strong>&#8377;{{ flight.Fare ? flight.Fare.toFixed(2) : '0.00'
                                                            }}</strong>
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
                                                        <strong>&#8377;{{ flight.Fare ? (flight.Fare * 2).toFixed(2) :
                                                            '0.00'
                                                            }}</strong>
                                                    </p>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div>
                            <div v-if="returnFlights.length != 0">
                                <h5>Return Flights</h5>
                                <div v-for="(returnFlight, returnIndex) in returnFlights" :key="returnIndex">
                                    <div class="accordion" :id="`returnAccordionExample-${index}-${returnIndex}`">
                                        <div class="accordion-item box shadow p-4 rounded bg-white">
                                            <div class="accordion-header">
                                                <div class="box shadow p-4 rounded bg-white">
                                                    <div class="d-flex justify-content-center mb-3">
                                                        <div class="about-flight">
                                                            <span>{{ returnFlight.Airline }}</span>
                                                            <span><strong>{{ formatTime(returnFlight.ArrivalTime)
                                                                    }}</strong></span>
                                                            <span><strong>{{ formatTime(returnFlight.DepartureTime)
                                                                    }}</strong></span>
                                                            <span>{{ returnFlight.Source }}</span>
                                                            <span>{{ returnFlight.Destination }}</span>
                                                            <span><strong>&#8377;{{ returnFlight.Fare ?
                                                                returnFlight.Fare.toFixed(2) : '0.00'
                                                                    }}</strong></span>
                                                            <button class="accordion-button collapsed" type="button"
                                                                data-bs-toggle="collapse"
                                                                :data-bs-target="`#returnCollapse-${index}-${returnIndex}`"
                                                                aria-expanded="false"
                                                                :aria-controls="`returnCollapse-${index}-${returnIndex}`">
                                                            </button>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div :id="`returnCollapse-${index}-${returnIndex}`"
                                                class="accordion-collapse collapse"
                                                :data-bs-parent="`#returnAccordionExample-${index}-${returnIndex}`">
                                                <div class="accordion-body">
                                                    <div class="d-flex justify-content-center mb-3">
                                                        <div class="form-check form-check-inline">
                                                            <input type="radio"
                                                                :id="`returnSaver-${index}-${returnIndex}`"
                                                                :name="`returnTripType-${index}-${returnIndex}`"
                                                                class="form-check-input" value="saver"
                                                                v-model="returnFlight.selectedType" checked required />
                                                            <div class="card" style="width: 18rem;">
                                                                <div class="card-body">
                                                                    <h5 class="card-title">Saver</h5>
                                                                    <h6 class="card-subtitle mb-2 text-muted">
                                                                        Non-refundable</h6>
                                                                    <h6 class="card-subtitle mb-2 text-muted">Cannot
                                                                        be rescheduled</h6>
                                                                    <p class="card-text">
                                                                        <strong>&#8377;{{ returnFlight.Fare ?
                                                                            returnFlight.Fare.toFixed(2) : '0.00'
                                                                            }}</strong>
                                                                    </p>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-check form-check-inline">
                                                            <input type="radio"
                                                                :id="`returnFlexi-${index}-${returnIndex}`"
                                                                :name="`returnTripType-${index}-${returnIndex}`"
                                                                class="form-check-input" value="flexi"
                                                                v-model="returnFlight.selectedType" required />
                                                            <div class="card" style="width: 18rem;">
                                                                <div class="card-body">
                                                                    <h5 class="card-title">Flexi</h5>
                                                                    <h6 class="card-subtitle mb-2 text-muted">
                                                                        Refundable</h6>
                                                                    <h6 class="card-subtitle mb-2 text-muted">Can be
                                                                        rescheduled</h6>
                                                                    <p class="card-text">
                                                                        <strong>&#8377;{{ returnFlight.Fare ?
                                                                            (returnFlight.Fare * 2).toFixed(2) : '0.00'
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
                    </div>
                </div>
            </div>


        </div>
    </div>

    <button class="btn btn-primary button" :disabled="selectedFlightIndex === null"
        style="margin-left: 80%;margin-top: 2%;" @click="proceedToForm">Continue</button>

    <div class="footer">
        <FooterDetails />
    </div>

</template>
<script>
import { Flights } from '@/scripts/FlightBooking_Services';
import FooterDetails from './FooterDetails.vue';

export default {
    name: 'SearchResult',
    components: {
        FooterDetails
    },
    data() {
        return {
            flights: [],
            returnFlights: [],
            source: '',
            destination: '',
            departureDate: '',
            returnDate: '',
            type: '',
            selectedFlightIndex: null
        }
    },
    methods: {
        fetchFlightWithReturn() {
            Flights(this.source, this.destination, this.departureDate, this.returnDate)
                .then(response => {
                    this.flights = response.data.initial_flights;
                    this.returnFlights = response.data.return_flights;
                    console.log(this.flights)
                    console.log(this.returnFlights)
                })
        },
        selectButtonHandler() {
            this.$router.push('/form')
        },
        formatTime(datetime) {

            const date = new Date(datetime);


            const hours = date.getHours().toString().padStart(2, '0');
            const minutes = date.getMinutes().toString().padStart(2, '0');
            return `${hours}:${minutes}`;
        },
        handleBack() {
            this.$router.push('/search')
            sessionStorage.removeItem('returnDate');
        },

        toggleAccordion(index) {
            this.activeIndex = this.activeIndex === index ? null : index;
        },
        proceedToForm() {
            this.$router.push('/form')
        },
        selectFlight(index) {
            this.selectedFlightIndex = index;
        },
    },
    mounted() {
        this.source = sessionStorage.getItem("source");
        this.destination = sessionStorage.getItem("destination");
        this.departureDate = sessionStorage.getItem("departureDate");
        this.returnDate = sessionStorage.getItem("returnDate")
        this.fetchFlightWithReturn(this.source, this.destination, this.departureDate, this.returnDate)



    }
}
</script>
<style scoped>
.main {
    display: flex;
}

.flights {
    display: flex;
}

.about-flight {
    display: flex;
    gap: 70px;
    justify-content: space-between;
    font-size: 18px;
    color: #444;
}

.highlighted {
    border: 2px solid #83b0e8;

}


.accordion-button {
    width: 0px;
    padding: 0px;
}

.book-button {
    margin-left: 865px;
}
</style>