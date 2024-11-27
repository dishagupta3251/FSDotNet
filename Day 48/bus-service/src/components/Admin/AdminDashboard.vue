<template>
    <DashboardLayout :menuItems="Items">
        <!-- <router-view /> -->
    </DashboardLayout>
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
                { id: 1, name: "Dashboard", link: "/admindashboard", icon: "bx bx-grid-alt" },
                { id: 2, name: "Users", link: "/users", icon: "bx bx-user" },
                { id: 3, name: "Operator", link: "/operators", icon: "bx bx-file" },
                { id: 4, name: "AllBuses", link: "/allbuses", icon: "bx bx-file" },
                { id: 5, name: "Payments", link: "/payments", icon: "bx bxs-bank" },
                { id: 6, name: "Routes", link: "/routes", icon: "bx bx-list-check" },
            ]
        }
    }
}

</script>
