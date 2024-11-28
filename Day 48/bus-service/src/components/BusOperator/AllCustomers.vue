<template>
    <main class="main">
        <h1> Hello {{ customer }}</h1>
    </main>
</template>
<script>


export default {
    name: "AllCustomers",

    data() {
        return {
            customer: 'Hello'
        }
    },
    mounted() {
        console.log('Rendered')
    }

}
</script>
<style scoped></style>