<template>


    <header class="header">
        <div class="logo">
            <img src="../../../public/Screenshot_2024-11-25_124942-removebg-preview.png">
        </div>
        <nav class="nav">
            <a href="#">Help</a>
            <a href="#">Contact</a>
            <div class="dropdown">
                <button class="dropbtn">Dropdown</button>
                <div class="dropdown-content">
                    <a href="#">Link 1</a>
                    <a href="#">Link 2</a>
                    <a href="#">Link 3</a>
                </div>
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
        }
    }
}
</script>
<style scope>
.header {
    width: 100%;
    display: flex;
    justify-content: space-between;
    align-items: center;
    padding: 15px 30px;
    background-color: white;
    position: fixed;
    top: 0;
    z-index: 99;
    border-bottom: 1px solid rgb(212, 221, 217);
    box-shadow: 0 1px 2px rgb(221, 228, 225);
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
    background-color: #f9f9f9;
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
    background-color: #f1f1f1
}

.dropdown:hover .dropdown-content {
    display: block;
}


.dropdown:hover .dropbtn {
    background-color: #3e8e41;
}

.logo {


    width: 30px;
}


.nav {
    display: flex;
    align-items: center;
    gap: 20px;
}

.nav a {
    text-decoration: none;
    color: rgba(205, 121, 31, 0.856);
    font-size: 1em;
    font-style: normal;
}
</style>