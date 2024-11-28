<template>


    <div class="main">
        <!-- Search Bar Section -->
        <CustomerNavbar />

        <header class="search-bar">
            <div class="route">

                <span>{{ source }}</span>
                <i class="arrow-icon">→</i>
                <span>{{ destination }}</span>
                <span class="date">{{ formattedDate }}</span>
            </div>
            <button class="modify-btn" @click="modifySearch">Modify</button>
        </header>



        <div class="content">
            <!-- Filter Section -->
            <aside class="filters">

                <div class="filter-group">
                    <h3>Seat Availability</h3>
                    <ul>
                        <li><input type="checkbox" id="single" /> Single Seats (205)</li>
                    </ul>
                </div>
                <div class="filter-group">
                    <h3>Arrival Time</h3>
                    <ul>
                        <li><input type="checkbox" id="before6am" /> Before 6 am </li>
                        <li><input type="checkbox" id="6amto12pm" /> 6 am to 12 pm </li>
                        <li><input type="checkbox" id="12pmto6pm" /> 12 pm to 6 pm </li>
                        <li><input type="checkbox" id="after6pm" /> After 6 pm </li>
                    </ul>
                </div>
                <div class="filter-group">
                    <h3>Boarding Point</h3>
                    <input type="text" placeholder="Boarding Point" />
                </div>
                <div class="filter-group">
                    <h3>Dropping Point</h3>
                    <input type="text" placeholder="Dropping Point" />
                </div>
            </aside>

            <!-- Bus List Section -->
            <section class="bus-list">
                <div class="travel">
                    <h6>{{ source }} </h6>
                    <p> &rarr;</p>
                    <h6>{{ destination }}</h6>
                </div>
                <div class="bus-item" v-for="bus in buses" :key="bus.busId">
                    <div class="bus-company-busnumber-mapper">
                        <div class="busCompany my-1">{{ bus.companyName }}</div>
                        <div class="busNumber my-1">{{ bus.busNumber }}</div>
                        <div class="busType my-1">{{ bus.busType }}</div>
                    </div>
                    <div class="bus-departure-arrival d-flex gap-4 mt-4">
                        <div class="departureContent-mapper">
                            <h4 class="">{{ formatDate(bus.departure) }}</h4>
                            <p class="station">Departure</p>
                        </div>
                        <div class="arrivalContent-mapper">
                            <h4>{{ formatDate(bus.arrival) }}</h4>
                            <p class="station">Arrival</p>
                        </div>
                    </div>
                    <div class="priceContentMapper mt-4">
                        <p class="m-0 priceTitle">Starts from</p>
                        <div><span class="priceValue">₹{{ bus.standardFare }}</span> <span
                                class="price-type">(StandardFare)</span></div>
                        <div><span class="priceValue">₹{{ bus.premiumFare }}</span> <span
                                class="price-type">(PremiumFare)</span>
                        </div>
                    </div>
                    <div class="seatContent mt-5">
                        <div><span class="seatAvailableValue mx-1">{{ bus.seatsLeft }}</span><span
                                class="seat-text">Seats
                                available</span></div>
                        <!-- <div>Status: {{ bus.status }}</div> -->
                    </div>
                    <button class=" view-seats-btn" @click="watch(bus.busId)">View Seats</button>
                    <div class="about-text">Why {{ bus.companyName
                        }} | PhotosBoarding & Dropping
                        | Points | Reviews | Booking policies | Bus | Route</div>
                </div>
            </section>
        </div>
    </div>
</template>

<script>
import { GetBuses } from '../../script/BusService';
import CustomerNavbar from './CustomerNavbar.vue';

export default {
    name: "SearchResult",
    components: {
        CustomerNavbar
    },
    data() {
        return {
            buses: [],
            source: '',
            destination: '',
            date: ''
        };
    },
    computed: {
        formattedDate() {
            return new Date(this.date).toLocaleDateString();
        }
    },
    methods: {
        fetchBuses(source, destination, date) {
            const formattedDate = new Date(date).toISOString();

            GetBuses(source, destination, formattedDate)
                .then((response) => {
                    this.buses = response.data;
                    console.log(response)
                })
                .catch((err) => {
                    console.error(err);
                    alert('Error fetching buses.');
                });
        },
        formatDate(dateString) {
            const date = new Date(dateString);
            return date.toLocaleTimeString([], { hour: '2-digit', minute: '2-digit' });
        },
        modifySearch() {
            this.$router.push({ name: 'SearchBus' });
        },
        watch(id) {
            console.log(id);
            this.$router.push({ name: 'Seats', params: { id: id } });

        }
    },
    mounted() {

        const { source, destination, date } = this.$route.query;

        this.source = source;
        this.destination = destination;
        this.date = date;


        this.fetchBuses(this.source, this.destination, this.date);
    }

};
</script>

<style scoped>
.main {
    height: 50%;
    width: 100%;
    margin: 0%;

}

.travel {
    display: flex;
    align-items: flex-start;
    gap: 20px;

}

.header {
    width: 100%;
    display: flex;
    justify-content: space-between;
    align-items: center;
    padding: 15px 30px;
    background-color: white;
    position: fixed;
    top: 0;
    border-bottom: 1px solid rgb(212, 221, 217);
    box-shadow: 0 1px 2px rgb(221, 228, 225);
}

.logo {


    width: 10px;
}

.nav {
    display: flex;
    align-items: center;
    gap: 20px;
}

.nav a {
    text-decoration: none;
    color: FFFAFF;
    font-size: 1em;
}

.search-bar {
    width: 100%;
    display: flex;
    justify-content: flex-start;
    padding: 10px 10px;
    background-color: FFFAFF;

    border-bottom: 1px solid rgb(230, 241, 237);
    box-shadow: 0 1px 2px rgb(161, 167, 164);
}

.route {
    display: flex;
    gap: 10px;
    /* align-items: center; */
}

.modify-btn {
    background-color: #007bff;
    color: FFFAFF;
    border: none;
    padding: 5px 15px;
    border-radius: 5px;
    cursor: pointer;
}

/* .display {
    margin-top: 20px;
    display: flex;
 
 
} */

.bus-details-mapper {
    padding: 20px;
}


.busCompany {
    color: #113979;
    font-weight: bolder;
}

.busNumber {
    font-weight: bolder;
}

.busType,
.station,
.seat-text {
    font-size: small;
    color: #7e7e8c;
}

.departureContent-mapper h4,
.arrivalContent-mapper h4 {
    color: #3e3e52;
    font-weight: bolder;
}

.priceTitle {
    color: #3e3e52;
    font-size: 14px;
}

.priceValue {
    font-weight: bolder;
    font-size: 20px;
}

.price-type {
    font-size: smaller;
}

.seatAvailableValue,
.seat-text {
    font-size: medium;
    color: #7e7e8c;
}

.seatAvailableValue {
    color: rgb(32, 30, 30);
}

.about-text {
    position: absolute;
    bottom: 10px;
    font-size: 14px;
    color: #0056b3;
    opacity: 0;
    transition: 1.2s ease-out;
}


.modify-btn:hover {
    background-color: #0056b3;
}


.view-seats-btn {
    font-size: smaller;
    text-transform: uppercase;
    position: absolute;
    bottom: 0;
    right: 0;
    padding: 7px 20px;
    border: none;
    outline: none;
    background: orangered;
    color: white;
    transition: 0.4s ease-out;
}

.view-seats-btn:hover {
    background-color: rgba(255, 68, 0, 0.349);
}

.content {
    width: 100%;
    display: flex;
}

.filters {
    width: 20%;
    padding: 20px;
    font-size: medium;
    background-color: FFFAFF;
    border-right: 1px solid #ccc;
}

.filter-group {
    margin-bottom: 20px;
}

.bus-list {
    width: 80%;
    padding: 20px;
}

.bus-item:hover {
    box-shadow: rgba(0, 0, 0, 0.25) 0px 14px 28px, rgba(0, 0, 0, 0.22) 0px 10px 10px;
}

.bus-item:hover .about-text {
    opacity: 1;
}

.bus-item {
    cursor: pointer;
    display: flex;
    justify-content: space-between;
    border: 1px solid #ccc;
    padding: 40px;
    margin-bottom: 30px;
    background: white;
    position: relative;
    transition: 0.5s ease-out;
}

.bus-details {
    display: flex;
}

/* .view-seats-btn {
background-color: rgb(205 121 31);
color: FFFAFF;
border: none;
 
padding: 5px 10px;
border-radius: 5px;
cursor: pointer;
}
 
 
 
.rating-price .low {
color: red;
}*/
</style>