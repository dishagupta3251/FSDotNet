<template>


    <div class="main">
        <!-- Search Bar Section -->
        <CustomerNavbar />
        <div>
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

                    <div class="filter-group-type">
                        <h3>Type</h3>
                        <div class="typeSelectorMapper d-flex align-items-center">
                            <label for="ac">AC</label>
                            <input type="checkbox" :value="1" id="ac" @change="onCheckboxClick($event)" name="ac">
                        </div>
                        <div class="typeSelectorMapper d-flex align-items-center">
                            <label for="nonAc">Non-AC</label>
                            <input type="checkbox" :value="2" id="nonAc" @change="onCheckboxClick($event)"
                                name="nonAc" />
                        </div>
                    </div>
                    <div class="filter-group mt-4">
                        <h3>Arrival Time</h3>
                        <div class="arrivalTimeSelectorMapper d-flex align-items-center">
                            <label for="before6am">Before 6 am</label>
                            <input type="checkbox" :value="1" id="before6am" @change="onCheckboxClickTime($event)"
                                name="before6am">
                        </div>
                        <div class="arrivalTimeSelectorMapper d-flex align-items-center">
                            <label for="6amto12pm">6 am to 12 pm</label>
                            <input type="checkbox" :value="2" id="6amto12pm" @change="onCheckboxClickTime($event)"
                                name="6amto12pm" />
                        </div>
                        <div class="arrivalTimeSelectorMapper d-flex align-items-center">
                            <label for="12pmto6pm">12 pm to 6 pm </label>
                            <input type="checkbox" :value="3" id="12pmto6pm" @change="onCheckboxClickTime($event)"
                                name="12pmto6pm" />
                        </div>
                        <div class="arrivalTimeSelectorMapper d-flex align-items-center" style="margin-bottom: 30px;">
                            <label for="after6pm"> After 6 pm </label>
                            <input type="checkbox" :value="4" id="after6pm" @change="onCheckboxClickTime($event)"
                                name="after6pm">
                        </div>
                    </div>
                    <div class="filter-group-type">
                        <h3>Operators Available</h3>
                        <div class="typeSelectorMapper d-flex align-items-center" v-for="name in names" :key="name">
                            <p class="m-0">{{ name }}</p>
                        </div>

                    </div>

                </aside>

                <!-- Bus List Section -->
                <section class="bus-list">
                    <DotLottieVue class="loading" v-if="loading" autoplay loop
                        src="https://lottie.host/4480640a-0a4e-4a35-95bd-48e4dddb1690/IL1ds5D6Vz.lottie" />
                    <div v-if="notfound" class="notfound">
                        <DotLottieVue autoplay loop style="height: 150px;margin-top: 80px;"
                            src="https://lottie.host/f10beee0-175f-4cf7-9f3c-9a3404d304e5/1jK8lAy4cb.lottie" />
                        <p style="color: grey; margin-left: 480px">No buses found</p>
                    </div>

                    <div>
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
                            <div class="seatContent mt-4">
                                <div :class="['blinking-text']" :style="{ color: busColor(bus.status) }">{{ bus.status
                                    }}
                                </div>
                                <div><span class="seatAvailableValue mx-1">{{ bus.seatsLeft }}</span><span
                                        class="seat-text">Seats
                                        available</span></div>

                            </div>

                            <button class=" view-seats-btn" :disabled="bus.status == 'Regret'"
                                @click="watch(bus.busId)">View
                                Seats</button>
                            <div class="about-text">
                                <p>Booking policies</p>
                                <p>{{ bus.companyName }}</p>
                            </div>
                        </div>
                    </div>

                </section>
            </div>
        </div>
    </div>
    <BookingDetails></BookingDetails>
    <FooterDetails />
</template>

<script>
import { GetBuses } from '../../script/BusService';
import CustomerNavbar from './CustomerNavbar.vue';
import { DotLottieVue } from '@lottiefiles/dotlottie-vue'
import FooterDetails from '../FooterDetails.vue';


export default {
    name: "SearchResult",
    components: {
        CustomerNavbar,
        DotLottieVue,
        FooterDetails
    },
    data() {
        return {
            buses: [],
            source: '',
            destination: '',
            date: '',
            data: '',
            names: [],
            loading: false,
            isChecked: false,
            notfound: false,

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
                    console.log(this.buses);
                    this.data = response.data
                    if (response.data.length == 0) {
                        this.notfound = true;

                    }
                    this.loading = false;
                    this.operatorDetails();

                })
                .catch((err) => {
                    console.log(err);
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
            const bus = this.buses.find((bus) => bus.busId === id);
            this.$router.push({
                name: 'Seats', params: {
                    id: id,
                },
                query:
                {
                    seatsLeft: bus.seatsLeft,
                    totalSeats: bus.totalSeats
                }
            });

        },
        busColor(status) {
            return status === 'Running' ? 'green' : 'red';
        },
        operatorDetails() {
            const allNames = this.buses.map((bus) => bus.companyName);
            this.names = [...new Set(allNames)];
        },
        onCheckboxClick(event) {
            if (event.target.checked) {
                const value = event.target.value;
                const type = value == 1 ? "AC" : "Non_AC";
                this.buses = this.buses.filter((bus) => bus.busType === type);
            }
            else {
                this.buses = this.data;
            }

        },
        onCheckboxClickTime(event) {
            if (event.target.checked) {
                const value = parseInt(event.target.value, 10);

                this.buses = this.data.filter((bus) => {
                    const arrivalHour = new Date(bus.arrival).getHours();
                    switch (value) {
                        case 1:
                            return arrivalHour < 6;
                        case 2:
                            return arrivalHour >= 6 && arrivalHour < 12;
                        case 3:
                            return arrivalHour >= 12 && arrivalHour < 18;
                        case 4:
                            return arrivalHour >= 18;
                        default:
                            return true;

                    }
                });
                if (this.buses.length == 0) this.notfound = true;
            } else {
                this.notfound = false;
                this.buses = this.data;
            }
        }

    },
    mounted() {

        const { source, destination, date } = this.$route.query;

        this.source = source;
        this.destination = destination;
        this.date = date;
        this.loading = true;
        this.fetchBuses(this.source, this.destination, this.date);


    }

};
</script>

<style scoped>
.main {
    height: 50%;
    width: 100%;
    margin: 0%;
    background-color: #f9f9ff;

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
    color: white;
    font-size: 1em;
}

.search-bar {
    width: 100%;
    display: flex;
    justify-content: flex-start;
    align-items: end;
    position: fixed;
    z-index: 2;
    height: 130px;
    padding: 15px 42px;
    background-color: white;
    border-bottom: 1px solid rgb(230, 241, 237);
    box-shadow: 0 1px 2px rgb(192, 197, 195);
}

.route {
    display: flex;
    gap: 10px;
    margin-top: 20px;
    height: 25px;
    align-items: center;
}

.typeSelectorMapper input,
.arrivalTimeSelectorMapper input {

    cursor: pointer;
    width: auto;
}

.typeSelectorMapper label,
.arrivalTimeSelectorMapper label {
    flex: 1;
    cursor: pointer;
}

.modify-btn {
    background-color: rgb(205 121 31);
    color: white;
    border: none;
    align-items: end;
    padding: 1px 15px;
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
    display: flex;
    bottom: 1px;
    gap: 20px;
    font-size: 12px;
    color: #0056b3;
    opacity: 0;
    transition: 1.2s ease-out;
}


.modify-btn:hover {
    background-color: rgb(148, 79, 5);
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
    background: rgb(205 121 31);
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
    margin-top: 170px;
    font-size: medium;
    background-color: white;
    border-right: 1px solid #ccc;
    box-shadow: 0 2px 2px rgba(192, 197, 195);
}

.filter-group-type {
    display: inline;
    margin-bottom: 20px;
}

.filters h3 {
    font-weight: bolder;
    border-bottom: 4px solid rgb(205 121 31);
    color: #3e3e52;
    font-size: 16px;
    text-transform: uppercase;
}

.bus-list {
    width: 80%;
    padding: 20px;
    margin-top: 150px;
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

@keyframes blink {
    0% {
        opacity: 1;
    }

    50% {
        opacity: 0;
    }

    100% {
        opacity: 1;
    }
}

.blinking-text {
    text-align: center;
    margin-top: 5%;
    font-size: 15px;
    color: green;
    animation: blink 3s infinite;
}

.loading {
    height: 200px;
    width: 200px;
    margin-top: 100px;
    margin-left: 400px;
}
</style>