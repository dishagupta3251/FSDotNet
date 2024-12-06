<template>

    <main>

        <DashboardLayout :menuItems="Items" :profile="profile" :logout="logout" />
        <div class="router-view">

            <router-view />
        </div>
    </main>

</template>
<script>

import DashboardLayout from '../DashboardLayout.vue'
export default {
    name: "AdminDashboard",
    components: {
        DashboardLayout
    },
    data() {
        return {
            Items: [
                { id: 1, name: "AdminDashboard", link: "/admindashboard", icon: "bx bxs-dashboard" },
                { id: 2, name: "Users", link: "/admindashboard/users", icon: "bx bxs-user" },
                { id: 3, name: "Operators", link: "/admindashboard/operators", icon: "bx bxs-user-badge" },
                { id: 4, name: "Buses", link: "/admindashboard/allbuses", icon: "bx bx-bus" },
                { id: 5, name: "Payments", link: "/admindashboard/payments", icon: "bx bx-money" },
                { id: 6, name: "Routes", link: "/admindashboard/routes", icon: "bx bx-trip" },
                { id: 7, name: "Bookings", link: "/admindashboard/bookings", icon: "bx bxs-bookmark-star" },
            ],
            profile: { name: "Profile", link: "/admindashboard/users", icon: "bx bx-user-circle" },
            logout:
                { id: 2, name: "Logout", link: "../auth", icon: "bx bx-log-out-circle" }
        }
    }
}

</script>
<style scoped>
main {
    display: flex;
}

.router-view {
    width: 100%;
    display: flex;
    justify-content: center;
    align-items: center;
}
</style>
