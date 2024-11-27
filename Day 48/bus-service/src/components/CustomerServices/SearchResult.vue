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
                <div class="bus-item" v-for="bus in buses" :key="bus.busId">
                    <div class="bus-details">

                        <h4>{{ bus.busNumber }}</h4>
                        <div class="display">
                            <div>Type: {{ bus.busType }}</div>
                            <div>Seats Available: {{ bus.seatsLeft }}</div>
                            <div>Status: {{ bus.status }}</div>
                            <div>StandardFare: INR {{ bus.standardFare }}</div>
                            <div>PremiumFare: INR {{ bus.premiumFare }}</div>
                            <div>Company: {{ bus.companyName }}</div>
                            <div>Arrival: {{ formatDate(bus.arrival) }}</div>
                            <div>Departure: {{ formatDate(bus.departure) }}</div>
                        </div>
                    </div>

                    <button class="view-seats-btn" @click="watch(bus.busId)">View Seats</button>
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
    height: 100%;
    width: 100%;
    margin: 0%;

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

.display {
    margin-top: 20px;
    display: flex;


}

.modify-btn:hover {
    background-color: #0056b3;
}

.content {
    width: 100%;
    display: flex;
}

.filters {
    width: 20%;
    padding: 30px;

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

.bus-item {
    display: flex;
    justify-content: space-between;
    border: 1px solid #ccc;
    padding: 10px;
    margin-bottom: 10px;
    background: white;
    border-radius: 5px;
}

.bus-details {
    display: flex;
}

.view-seats-btn {
    background-color: rgb(205 121 31);
    color: FFFAFF;
    border: none;

    padding: 5px 10px;
    border-radius: 5px;
    cursor: pointer;
}

.view-seats-btn:hover {
    background-color: rgb(155, 87, 14);
}

.rating-price .low {
    color: red;
}
</style>