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





    <!-- Features Section -->
    <div class="features">
        <div class="feature">
            <h3>36 Million</h3>
        </div>
        <div class="feature">
            <h3>Over 5000</h3>
        </div>
        <div class="feature">
            <h3>200,000+</h3>
        </div>
        <div class="feature">
            <h3>200,000+</h3>
        </div>
    </div>

</template>

<script>
import CustomerNavbar from './CustomerNavbar.vue';

export default {
    name: "SearchBus",
    components: {
        CustomerNavbar
    },
    data() {
        return {
            source: '',
            destination: '',
            date: '',
            minDate: ''
        }
    },
    methods: {
        search() {

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
            let mm = today.getMonth() + 1; // Months are zero-indexed
            let dd = today.getDate();

            // Format the date as YYYY-MM-DD
            if (mm < 10) mm = '0' + mm; // Ensure month is two digits
            if (dd < 10) dd = '0' + dd; // Ensure day is two digits

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
    background-color: #f9f9f9;
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
    height: 300px;
    background-image: url('../../../public/Screenshot\ 2024-11-22\ 105120.png');

    background-size: cover;

    background-position: center;

    background-repeat: no-repeat;

    color: white;


}

input {
    background-color: #eee;
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
    margin-top: 100px;
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
}

.search-button {
    padding: 10px 20px;
    background-color: rgb(205 121 31);
    color: white;
    font-size: 1em;
    border: none;
    border-radius: 5px;
    cursor: pointer;
}

.search-button:hover {
    background-color: #d62c1a;
}

/* Features Section */
.features {
    display: flex;
    gap: 20px;
    justify-content: center;
    margin-top: 20px;
}

img {
    height: 50px;
    width: 70px;
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
</style>