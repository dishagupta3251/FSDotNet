<template>
    <div class="token-container">
        <h2>Enter JWT Token</h2>
        <textarea v-model="jwtToken" class="form-control" rows="5" placeholder="Paste your JWT token here"></textarea>
        <button class="btn btn-primary mt-3" @click="submitToken">Submit Token</button>
    </div>
</template>

<script>


export default {
    name: 'AuthPage',
    data() {
        return {
            jwtToken: ""
        };
    },
    methods: {
        submitToken() {
            try {
                if (this.jwtToken == '') {
                    alert("Token cannot be empty.");
                    return;
                }
                sessionStorage.setItem("token", this.jwtToken)
                console.log(this.jwtToken)
                this.$router.push("/dashboard");
            } catch (error) {
                alert("Invalid token. Please try again.");
            }
        }
    }
};
</script>

<style scoped>
.token-container {
    max-width: 600px;
    margin: 100px auto;
    text-align: center;
}

textarea {
    width: 100%;
}
</style>