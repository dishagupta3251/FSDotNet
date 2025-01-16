<template>
    <div class="unauthorized">
        <h1>Unauthorized</h1>
        <p>You do not have permission to access this page.</p>
        <router-link to="/dashboard">Go Back</router-link>
    </div>
</template>
<script>
export default {
    name: 'UnauthorizedPage'
}

</script>

<style scoped>
.unauthorized {
    text-align: center;
    margin-top: 100px;
}
</style>