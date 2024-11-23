<template>
    <div class="landingpage">
        <!-- Navigation Bar -->
        <header class="header">
            <div class="logo">Bus</div>
            <nav class="nav">
                <a href="#">Help</a>
                <router-link to="/auth">Login</router-link>
                <select class="lang-select">
                    <option value="en">🇺🇸 English</option>
                    <option value="hi">🇮🇳 Hindi</option>
                </select>
            </nav>
        </header>

        <!-- Main Content -->
        <div class="content">
            <!-- Left Side: Search Section -->
            <div class="search-section">
                <h2>Search for a Bus</h2>
                <form>
                    <div class="form-group">
                        <label for="from">From:</label>
                        <input type="text" id="from" v-model="from" placeholder="Enter departure city" />
                    </div>
                    <div class="form-group">
                        <label for="to">To:</label>
                        <input type="text" id="to" v-model="to" placeholder="Enter destination city" />
                    </div>
                    <div class="form-group">
                        <label for="date">Date:</label>
                        <input type="date" id="date" v-model="date" />
                    </div>
                    <button type="submit" class="search-btn" @click="searchBus">Search</button>
                </form>
            </div>
            <div>
                <div v-if="showToastMessage" class="toast">
                    Need to Sign in First!
                </div>
            </div>
            <!-- Right Side: Image Section -->
            <div class="image-section">
                <img src="group-buses-driving-along-road-sunset_157027-4307.avif" alt="Bus" />
            </div>
        </div>
    </div>
</template>

<script>
import router from '@/router';
import { GetBuses } from '@/script/LandingPageService';


export default {
    name: "LandingPage",
    data() {
        return {
            from: '',
            to: '',
            date: '',
            showToastMessage: false
        }
    },
    methods: {
        searchBus(event) {
            event.preventDefault();
            const date = new Date(this.date).toISOString()
            GetBuses(this.from, this.to, date)
                .then((response) => { console.log(response.data) }
                )
                .catch((err) => {
                    alert(err.response.data)
                }
                );
            this.loginRedirect()

        },
        loginRedirect() {
            this.showToastMessage = true;
            setTimeout(() => {
                this.showToastMessage = false;
                router.push('/auth')
            }, 1000);
        },
    }
}

</script>

<style scoped>
/* Styling the Navbar */
.header {
    width: 100%;
    display: flex;
    justify-content: space-between;
    align-items: center;
    padding: 30px 30px;
    background-color: rgb(19 68 50);

    border-bottom: 1px solid rgb(10, 39, 28);
    box-shadow: 0 1px 2px rgb(0, 10, 6);
}

.logo {
    font-size: 1.5em;
    font-weight: bold;
    color: rgb(205 121 31);
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

.lang-select {
    padding: 5px;
    font-size: 0.9em;
    border-radius: 4px;
    border: 1px solid #ddd;
}

.btn {
    background-color: rgb(205 121 31);
    color: white;
    border: none;
    border-radius: 4px;
    cursor: pointer;
    padding: 15px;
}

.toast {
    position: fixed;
    top: 20px;
    left: 20px;
    background-color: #28a745;
    color: white;
    padding: 15px 20px;
    border-radius: 5px;
    box-shadow: 0 4px 8px rgba(0, 0, 0, 0.2);
    z-index: 1000;
    font-size: 14px;
    animation: fadeInOut 3s ease forwards;
}

/* Fade-in and Fade-out Animation */
@keyframes fadeInOut {
    0% {
        opacity: 0;
        transform: translateY(10px);
    }

    10%,
    90% {
        opacity: 1;
        transform: translateY(0);
    }

    100% {
        opacity: 0;
        transform: translateY(10px);
    }
}

/* Styling the main content */
.content {
    display: flex;
    margin: 2rem;
}

.search-section {
    text-decoration: none;
    flex: 0.7;
    padding: 2rem;
    background-color: #f9f9f9ef;
    font-size: medium;
    border-radius: 8px;
    box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
}

.search-section h2 {
    margin-bottom: 1rem;
    color: #333;
}

.form-group {
    margin-bottom: 1rem;
}

.form-group label {
    display: block;
    margin-bottom: 0.5rem;
    font-weight: bold;
}

.form-group input {
    width: 100%;
    padding: 0.5rem;
    border: 1px solid #ccc;
    border-radius: 4px;
}

.search-btn {
    padding: 0.7rem 1.5rem;
    background-color: rgb(205 121 31);
    color: white;
    border: none;
    border-radius: 4px;
    cursor: pointer;
}

.search-btn:hover {
    background-color: rgb(168, 94, 14);
}

/* Styling the image section */
.image-section {
    flex: 1;
    display: flex;
    justify-content: center;
    align-items: center;
    padding: 2rem;
}

.image-section img {
    max-width: 100%;
    height: auto;

    border-radius: 8px;
}
</style>