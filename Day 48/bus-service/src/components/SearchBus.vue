<template>
    <div class="search">
        <!-- Header Section -->
        <header class="header">
            <div class="logo">Bus</div>
            <nav class="nav">
                <a href="#">Help</a>
                <a href="#">Profile</a>
            </nav>
        </header>

        <!-- Main Content -->
        <main class="main-content">
            <h1 class="title">
                Book your journey now and explore the world with comfort and ease!
            </h1>

            <!-- Horizontal Search Bar -->
            <div class="search-bar">
                <div class="input-group">
                    <label for="source">Source</label>
                    <input id="source" type="text" placeholder="Source" />
                </div>
                <div class="input-group">
                    <label for="destination">Destination</label>
                    <input id="destination" type="text" placeholder="Destination" />
                </div>
                <div class="input-group">
                    <label for="date">Date</label>
                    <input id="date" type="date" />
                </div>
                <button class="search-button">Search</button>
            </div>


        </main>

        <!-- Features Section -->
        <!-- <div class="features">
            <div class="feature">
                <h3>36 Million</h3>
            </div>
            <div class="feature">
                <h3>Over 5000</h3>
            </div>
            <div class="feature">
                <h3>200,000+</h3>
            </div>
        </div> -->
    </div>
</template>

<script>
export default {
    name: "App",
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
.header {

    width: 100%;
    display: flex;
    justify-content: space-between;
    align-items: center;
    text-align: center;
    padding: 25px 30px;
    background-color: white;
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
    color: black;
    font-size: 0.9em;
}

.lang-select {
    padding: 5px;
    font-size: 0.9em;
    border-radius: 4px;
    border: 1px solid #ddd;
}

/* Main Content Styles */
.main-content {


    width: 100%;
    text-align: center;
    height: 280px;
    background-image: url('../../public/Screenshot\ 2024-11-22\ 105120.png');
    /* Replace 'your-image.jpg' with the path to your image */
    background-size: cover;
    /* Ensures the image covers the entire section */
    background-position: center;
    /* Centers the image */
    background-repeat: no-repeat;

    color: white;


}

.title {
    font-size: 1.8em;
    font-weight: bold;
    margin-top: 15px;
    margin-bottom: 30px;
}

/* Horizontal Search Bar */
.search-bar {
    display: flex;
    gap: 10px;
    align-items: flex-end;
    justify-content: center;
    margin-bottom: 40px;
    padding: 20px 0;
}

.input-group {
    display: flex;
    flex-direction: column;
    align-items: flex-start;
}

.input-group label {
    font-size: 0.85em;
    margin-bottom: 5px;
    font-weight: bold;
}

.input-group input {
    padding: 10px;
    font-size: 0.9em;
    border: 1px solid #ddd;
    border-radius: 4px;
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

.feature {
    text-align: center;
    background-color: #ffffff;
    padding: 20px;
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