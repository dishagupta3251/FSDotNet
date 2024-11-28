<template>
    <div class="landingpage">
        <!-- Navigation Bar -->
        <header class="header">
            <div class="logo">
                <img src="../../public/Screenshot_2024-11-25_124942-removebg-preview.png" alt="Logo">
            </div>
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
                <form @submit.prevent="searchBus">
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
                    <button type="submit" class="search-btn">Search</button>
                </form>
            </div>


            <!-- Right Side: Image Section -->
            <div class="image-section"></div>
        </div>
    </div>
</template>

<script>


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
        searchBus() {
            this.$router.push({
                name: 'SearchResult',
                query: {
                    source: this.from,
                    destination: this.to,
                    date: this.date
                }
            });

        }
    }
}
</script>

<style scoped>
/* Styling the Navbar */
.header {
    position: fixed;
    width: 100%;
    display: flex;
    justify-content: space-between;
    align-items: center;
    padding: 15px 30px;
    background-color: white;
    border-bottom: 1px solid rgb(186, 201, 195);
    box-shadow: 0 1px 2px rgb(221, 228, 225);
}

.logo img {
    width: 70px;
}

.nav {
    display: flex;
    align-items: center;
    gap: 20px;
}

.nav a {
    text-decoration: none;
    color: rgb(205, 121, 31);
    font-size: 1em;
}

.lang-select {
    padding: 5px;
    font-size: 0.9em;
    border-radius: 4px;
    background-color: rgb(205 121 31);
    color: white;
    border: 1px solid #ddd;
}

.toast {
    position: fixed;
    top: 20px;
    left: 20px;
    background-color: #28a745;
    color: #fff;
    padding: 15px 20px;
    border-radius: 5px;
    box-shadow: 0 4px 8px rgba(0, 0, 0, 0.2);
    z-index: 1000;
    font-size: 14px;
    animation: fadeInOut 3s ease forwards;
}

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
    margin: 0 0px;
    position: relative;
    align-items: center;
}

.search-section {
    position: absolute;
    top: 0;
    left: 5%;
    width: 40%;
    text-decoration: none;
    margin-top: 100px;
    padding: 2rem;
    background-color: rgb(255 255 255 / 86%);
    font-size: medium;
    border-radius: 8px;
    box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
}

@media (max-width: 850px) {
    .search-section {
        width: 60%;
        left: 10%;
    }
}

@media (max-width: 630px) {
    .search-section {
        width: 90%;
        left: 5%;
    }
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
    outline: none;
    width: 99%;
    height: 50px;
    padding: 1rem;
    border: 1px solid #ccc;
    border-radius: 4px;
}

.form-group input::placeholder {
    color: rgba(0, 0, 0, 0.616);
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

.image-section {
    position: absolute;
    width: 100%;
    top: 0;
    padding: 2rem;
    background-image: url('../../public/scene.jpg');
    background-size: cover;
    background-position: center;
    opacity: 0.8;
    height: 100vh;

    z-index: -1;

}
</style>
