<template>
    <div class="search">
        <CustomerNavbar />

        <!-- Main Content -->
        <main class="main-content">



            <div class="search">
                <div class="title">
                    <h2>Book your trip and let the journey begin</h2>
                </div>
                <div class="search-bar">


                    <div class="input-group">
                        <label for="source">Source</label>
                        <input id="source" type="text" v-model="source" placeholder="Source" />
                    </div>
                    <div class="input-group">
                        <label for="destination">Destination</label>
                        <input id="destination" type="text" v-model="destination" placeholder="Destination" />
                    </div>
                    <div class="input-group">
                        <label for="date">Date</label>
                        <input id="date" type="date" v-model="date" :min="minDate" />
                    </div>
                    <div class="button-group">
                        <button class="search-button" style="margin-top: 18px;" @click="search">Search</button>
                    </div>

                </div>

            </div>

        </main>
    </div>





    <div class="trending-offers">
        <h3>Trending Offers</h3>
        <div class="offers-container">
            <div class="offer-card" v-for="(offer, index) in offers" :key="index" :class="'offer-' + (index + 1)">
                <div class="offer-type">BUS</div>
                <h5>{{ offer.title }}</h5>
                <p>Valid till {{ offer.validity }}</p>
                <div class="code-container">
                    <span class="code">{{ offer.code }}</span>
                    <button class="copy-button">
                        📋
                    </button>
                </div>
            </div>
        </div>
    </div>
    <FooterDetails />


</template>

<script>
import FooterDetails from '../FooterDetails.vue';
import CustomerNavbar from './CustomerNavbar.vue';


export default {
    name: "SearchBus",
    components: {
        CustomerNavbar,
        FooterDetails,

    },
    data() {
        return {
            source: '',
            destination: '',
            date: '',
            formattedDate: '',
            minDate: '',
            show: false,
            offers: [
                {
                    title: "Save up to Rs 100 on Kukkesree Travels",
                    validity: "10 Dec",
                    code: "KUKKE10",
                },
                {
                    title: "Save up to Rs 100 on Gajal Travels",
                    validity: "31 Dec",
                    code: "GAJALBUS10",
                },
                {
                    title: "Save up to Rs 50 on IntrCity SmartBus",
                    validity: "15 Feb",
                    code: "INTRCITY",
                },
                {
                    title: "Save up to Rs 100 on Falcon Travels",
                    validity: "31 Dec",
                    code: "FALCON10",
                },
            ],
        }
    },
    methods: {
        search() {
            sessionStorage.setItem('selectedDate', this.date);

            this.$router.push({
                name: 'SearchResult',
                query: {
                    source: this.source,
                    destination: this.destination,
                    date: this.date
                }
            });

        },

        setMinDate() {
            const today = new Date();
            const yyyy = today.getFullYear();
            let mm = today.getMonth() + 1;
            let dd = today.getDate();


            if (mm < 10) mm = '0' + mm;
            if (dd < 10) dd = '0' + dd;

            this.minDate = `${yyyy}-${mm}-${dd}`;
        },
    },
    mounted() {
        this.setMinDate();
    }

};
</script>

<style>
/* General Styles */
body {
    font-family: Arial, sans-serif;
    margin: 0;
    padding: 0;
    box-sizing: border-box;
    background-color: #ffffff;
    color: #333;
}

.search {
    display: flex;
    flex-direction: column;
    align-items: center;
}

/* Header Styles */

/* Main Content Styles */
.main-content {


    width: 100%;
    text-align: center;
    height: 350px;
    background-image: url('../../../public/Screenshot\ 2024-11-22\ 105120.png');

    background-size: cover;

    background-position: center;

    background-repeat: no-repeat;

    color: white;


}

input {
    background-color: #ffffff;
    border: none;
    padding: 12px 15px;
    margin: 8px 0;
    width: 100%;

}

.button-group {
    display: flex;
    flex-direction: column;
}

.title {
    font-size: 1.8em;
    font-weight: bold;
    margin-top: 120px;
}

/* Horizontal Search Bar */
.search-bar {
    display: flex;
    gap: 10px;
    align-items: center;
    justify-content: center;
    margin-bottom: 63px;
    padding: 5px 0;

}



.input-group label {
    font-size: 0.85em;
    margin-bottom: 5px;
    font-weight: bold;


}

.input-group input {
    padding: 7px;
    font-size: 0.9em;
    border: 1px solid #ddd;
    box-shadow: 0px 2px 4px #333;
}

.search-button {
    padding: 10px 20px;
    background-color: rgb(205 121 31);
    box-shadow: 0px 2px 4px #333;
    color: white;
    font-size: 1em;
    border: none;
    border-radius: 5px;
    cursor: pointer;
}

.search-button:hover {
    background-color: #c05b09;
}

/* Features Section */
.features {
    display: flex;
    gap: 20px;
    justify-content: center;
    margin-top: 20px;
}

.feature {
    text-align: center;
    background-color: #ffffff;
    padding: 50px;
    border-radius: 8px;
    box-shadow: 0 2px 8px rgba(0, 0, 0, 0.1);
}

.feature img {
    max-width: 80px;
    margin-bottom: 15px;
}

.feature h3 {
    font-size: 1.2em;
    margin-bottom: 10px;
}

.feature p {
    font-size: 0.9em;
    color: #666;
}

/* Responsive Design */
@media (max-width: 768px) {
    .search-bar {
        flex-direction: column;
        align-items: center;
        gap: 15px;
    }

    .features {
        flex-direction: column;
    }
}

.trending-offers {
    text-align: center;
    margin: 30px 0;
}

.offers-container {
    display: flex;
    justify-content: center;
    gap: 25px;
    margin: 20px 80px;
    /* flex-wrap: wrap; */
}

.offer-card {
    min-width: 100px;
    padding: 10px;
    height: 200px;
    border-radius: 10px;
    color: rgba(5, 0, 0, 0.658);
    box-shadow: 0 5px 10px rgba(0, 0, 0, 0.726);
    display: flex;
    flex-direction: column;
    justify-content: space-between;
    border: 3px solid;
    background: linear-gradient(145deg, rgba(255, 255, 255, 0.15), rgba(0, 0, 0, 0.1));
}


.offer-1 {
    border-color: #a971f2;
    /* Bright purple border */
    background-color: #d4b3f8;
    /* Soft lavender content */
}

.offer-2 {
    border-color: #488ed1;
    /* Bright blue border */
    background-color: #b5d6f3;
    /* Softer sky-blue content */
}

.offer-3 {
    border-color: #d6544f;
    /* Bright red border */
    background-color: #f9b3b0;
    /* Warm peach content */
}

.offer-4 {
    border-color: #4a9d4f;
    /* Bright green border */
    background-color: #a8e5b3;
    /* Soft mint content */
}

.offer-type {
    background-color: rgba(255, 255, 255, 0.25);
    padding: 5px 10px;
    border-radius: 5px;
    font-size: 0.9em;
    display: inline-block;
    text-transform: uppercase;
}

.code-container {
    margin-top: 10px;
    display: flex;
    margin-left: 50px;
    gap: 10px;
}

.code {
    font-size: 1.2em;
    font-weight: bold;
    color: #333;
    padding: 5px 10px;
    background: rgba(255, 255, 255, 0.3);
    border-radius: 5px;
}

.copy-button {
    background: none;
    border: none;
    cursor: pointer;
    font-size: 1.2em;
    color: #333;
}

.copy-button:hover {
    color: #555;
}
</style>