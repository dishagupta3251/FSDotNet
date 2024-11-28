<template>
    <main>
        <DashboardLayout :menuItems="menuItems" />
        <router-view />
    </main>

</template>
<script>

import DashboardLayout from '../DashboardLayout.vue'
export default {
    name: "BusOperatorDashboard",
    components: {
        DashboardLayout
    },
    data() {
        return {
            menuItems: [
                { name: "OperatorDashboard", link: "/operatordashboard", icon: "bx bxs-dashboard" },
                { name: "Customers", link: "/operatordashboard/customers", icon: "bx bx-user" },
                { name: "Buses", link: "/operatordashboard/buses", icon: "bx bx-file" },
                { name: "Reviews", link: "/operatordashboard/reviews", icon: "bx bxs-bank" },
            ]
        }
    }
}

</script>
<style scoped>
main {
    display: flex;
}
</style>
