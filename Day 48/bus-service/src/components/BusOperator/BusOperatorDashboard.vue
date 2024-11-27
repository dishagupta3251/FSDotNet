<template>
    <DashboardLayout :menuItems="menuItems">
        <router-view />
        <h1>Hello</h1>
    </DashboardLayout>
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
                { name: "Dashboard", link: "/operatordashboard", icon: "bx bx-grid-alt" },
                { name: "Customers", link: "/operatordashboard/customers", icon: "bx bx-user" },
                { name: "Buses", link: "/operatordashboard/buses", icon: "bx bx-file" },
                { name: "Reviews", link: "/operatordashboard/reviews", icon: "bx bxs-bank" },
            ]
        }
    }
}

</script>
