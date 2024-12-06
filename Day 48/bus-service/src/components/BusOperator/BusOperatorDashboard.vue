<template>
    <main>
        <OperatorDashboardLayour :menuItems="menuItems" :logout="logout" />
        <router-view />
    </main>

</template>
<script>
import OperatorDashboardLayour from './OperatorDashboardLayour.vue';


export default {
    name: "BusOperatorDashboard",
    components: {
        OperatorDashboardLayour
    },
    data() {
        return {
            menuItems: [
                { name: "OperatorDashboard", link: "/operatordashboard", icon: "bx bxs-dashboard" },
                { name: "Customers", link: "/operatordashboard/customers", icon: "bx bx-user" },
                { name: "Buses", link: "/operatordashboard/buses", icon: "bx bx-file" },
                { name: "Reviews", link: "/operatordashboard/reviews", icon: "bx bxs-bank" },

            ],
            // profile: { name: "Profile", link: "/operatordashboard/users", icon: "bx bx-user-circle" },
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
</style>
