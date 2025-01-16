<template>
    <h1 style="padding:2%;">Add Customer</h1>
    <form class="form" @submit.prevent="add">
        <div class="form-row d-flex">
            <div class="form-group col-md-6">
                <label for="firstname">FirstName</label>
                <input type="text" class="form-control" v-model="firstName" id="firstname" placeholder="FirstName">
            </div>
        </div>
        <div class="form-row d-flex">
            <div class="form-group col-md-6">
                <label for="lastname">LastName</label>
                <input type="text" class="form-control" v-model="lastName" id="lastname" placeholder="LastName">
            </div>
        </div>
        <div class="form-row d-flex">
            <div class="form-group col-md-6">
                <label for="email">Email</label>
                <input type="email" class="form-control" v-model="email" id="email" placeholder="Email">
            </div>
        </div>

        <div class="form-row d-flex">
            <div class="form-group col-md-6">
                <label for="inputEmail4">Address</label>
                <input type="text" class="form-control" v-model="address" id="address" placeholder="Lane No.">
            </div>

        </div>
        <div class="form-row d-flex">
            <div class="form-group">
                <label for="inputDate">Date of Birth</label>
                <input type="date" class="form-control" id="inputDate" v-model="dateOfBirth">
            </div>
        </div>
        <div class="form-row">
            <div class="form-group col-md-6">
                <label for="inputCity">City</label>
                <input type="text" class="form-control" v-model="city" id="inputCity">
            </div>

        </div>
        <div class="form-row d-flex">
            <div class="form-group col-md-6">
                <label for="phone">Phone</label>
                <input type="text" class="form-control" v-model="phone" id="phone">
            </div>

        </div>
        <button type="submit" style="margin-top: 2%;" class="btn btn-primary">ADD</button>
    </form>
</template>
<script>
import { Add } from '@/scripts/Services'
export default {

    name: "AddCustomer",
    data() {
        return {
            firstName: "",
            lastName: "",
            email: "",
            address: "",
            dateOfBirth: "",
            city: "",
            phone: ""
        }
    },
    methods: {

        async add() {
            console.log("This is called")
            try {
                await Add(this.firstName, this.lastName, this.address, this.city, this.email, this.dateOfBirth, this.phone)
                    .then((response) =>

                        console.log(response.data)
                    );
                this.$router.push('/dashboard');
            }
            catch (error) {
                alert("Error: " + error)
            }


        }
    },

}
</script>

<style scoped>
.form {
    padding-left: 10%;
}
</style>