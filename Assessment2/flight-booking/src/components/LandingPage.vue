<template>
    <div class="button">
        <button type="button" class="btn btn-outline-success" @click="flight">Flight Booking</button>
        <button type="button" class="btn btn-outline-success" @click="hotel">Hotel Booking</button>
    </div>

    <div class="footer">
        <FooterDetails />
    </div>


</template>
<script>
import FooterDetails from './FooterDetails.vue';
export default {
    name: 'LandingPage',
    components: {
        FooterDetails
    },
    methods: {
        flight() {
            this.$router.push('/search');
        },
        hotel() {
            this.$router.push('/hotel');
        }
    }
}
</script>
<style scoped>
.button {
    display: flex;
    justify-content: space-around;
}
</style>