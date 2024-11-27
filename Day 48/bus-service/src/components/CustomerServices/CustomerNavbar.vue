<template>
    <header class="header">
        <div class="logo">
            <img src="../../../public/Screenshot_2024-11-25_124942-removebg-preview.png">
        </div>
        <nav class="nav">
            <a href="#">Help</a>
            <a href="#">History</a>
            <a href="#">Profile</a>
        </nav>
    </header>
</template>
<script>
export default {
    name: "CustomerNavbar",
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
    color: rgba(205, 121, 31, 0.856);
    font-size: 1em;
    font-style: normal;
}


.lang-select {
    padding: 5px;
    font-size: 0.9em;
    border-radius: 4px;
    border: 1px solid #ddd;
}
</style>