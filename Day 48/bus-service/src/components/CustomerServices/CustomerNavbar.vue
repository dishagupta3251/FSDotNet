<template>
    <header class="header">
        <div class="logo">
            <img src="../../../public/Screenshot_2024-11-25_124942-removebg-preview.png">
        </div>
        <nav class="nav">
            <a href="#">Help</a>
            <a href="#">Contact</a>
            <div class="dropdown">
                <a href="#" data-bs-toggle="dropdown" aria-expanded="false" style="display: flex;">
                    <i class='bx bxs-user-circle' style="line-height: 3;"></i>
                    <p style="margin-top: 24px;
    margin-left: 7px;">Account</p>
                </a>

                <ul class="dropdown-menu">
                    <li><router-link class="dropdown-item" to="/search">Home</router-link></li>
                    <li><a class="dropdown-item" href="#">Profile</a></li>
                    <li><a class="dropdown-item" href="#" @click="showHistory">Your Bookings</a></li>
                    <li>
                        <hr class="dropdown-divider">
                    </li>
                    <li><a class="dropdown-item" href="#" @click="logout">Logout</a></li>
                </ul>
            </div>
        </nav>
    </header>

</template>
<script>
export default {
    name: "CustomerNavbar",
    methods: {
        logout() {
            sessionStorage.clear();
            this.$router.push("/auth");
        },

        profile() {
            this.$router.push("/profile");
        },
        showHistory() {
            this.$router.push("/history");
        }
    }
}
</script>
<style scope>
.header {
    width: 100%;
    display: flex;
    height: 80px;
    justify-content: space-between;
    align-items: center;
    padding: 15px 20px;
    background-color: #ffffff;
    position: fixed;
    top: 0;
    z-index: 99;
    border-bottom: 1px solid rgb(212, 221, 217);
    box-shadow: 0 1px 2px rgb(221, 228, 225);
}



i {

    font-size: 23px;
}

.dropbtn {
    background-color: #4CAF50;
    color: white;
    padding: 16px;
    font-size: 16px;
    border: none;
    cursor: pointer;
}

.dropdown {
    position: relative;
    display: inline-block;
}


.dropdown-content {
    display: none;
    position: absolute;
    background-color: #ffffff;
    min-width: 160px;
    box-shadow: 0px 8px 16px 0px rgba(0, 0, 0, 0.2);
    z-index: 1;
}


.dropdown-content a {
    color: black;
    padding: 12px 16px;
    text-decoration: none;
    display: block;
}


.dropdown-content a:hover {
    background-color: #ffffff
}

.dropdown:hover .dropdown-content {
    display: block;
}


img {
    height: 55px;
    width: 85px;
}


.dropdown:hover .dropbtn {
    background-color: #3e8e41;
}

.logo {
    width: 30px;
    /* position: relative;
    left: 0;
    transition: 5s ease-out */
}

/* .logo:hover {
    cursor: pointer;
    left: 900px;
} */


.nav {
    display: flex;
    align-items: center;
    gap: 20px;
}

.nav a {
    text-decoration: none;
    color: rgba(184, 101, 13, 0.856);
    font-size: 1em;
    font-style: normal;
}
</style>