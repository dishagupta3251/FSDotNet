<template>
    <main>
        <BusOperatorDashboard />
        <router-view />
        <h1>{{ customer }}</h1>
    </main>
</template>
<script>
import BusOperatorDashboard from './BusOperatorDashboard.vue';

export default {
    name: "AllCustomers",
    components: {
        BusOperatorDashboard
    },
    data() {
        return {
            customer: 'Hello'
        }
    }

}
</script>
<style scoped>
h1 {
    margin-left: 100px;
    color: black
}
</style>