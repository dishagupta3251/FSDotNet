<template>
    <h1 style="padding-left:8%;margin-top: 20px;">Edit Customer</h1>
    <form class="form" @submit.prevent="edit">
        <div class="form-row d-flex">
            <div class="form-group col-md-6">
                <label for="firstname">FirstName</label>
                <input type="text" class="form-control" v-model="customer.firstName" id="firstname"
                    placeholder="FirstName">
            </div>
        </div>
        <div class="form-row d-flex">
            <div class="form-group col-md-6">
                <label for="lastname">LastName</label>
                <input type="text" class="form-control" v-model="customer.lastName" id="lastname"
                    placeholder="LastName">
            </div>
        </div>
        <div class="form-row d-flex">
            <div class="form-group col-md-6">
                <label for="email">Email</label>
                <input type="email" class="form-control" v-model="customer.email" id="email" placeholder="Email">
            </div>
        </div>

        <div class="form-row d-flex">
            <div class="form-group col-md-6">
                <label for="inputEmail4">Address</label>
                <input type="text" class="form-control" v-model="customer.address" id="address" placeholder="Lane No.">
            </div>

        </div>
        <div class="form-row d-flex">
            <div class="form-group">
                <label for="inputDate">Date of Birth</label>
                <input type="date" class="form-control" id="inputDate" v-model="customer.dateOfBirth">
            </div>
        </div>
        <div class="form-row">
            <div class="form-group col-md-6">
                <label for="inputCity">City</label>
                <input type="text" class="form-control" v-model="customer.city" id="inputCity">
            </div>

        </div>
        <div class="form-row d-flex">
            <div class="form-group col-md-6">
                <label for="phone">Phone</label>
                <input type="text" class="form-control" v-model="customer.phoneNumber" id="phone">
            </div>

        </div>
        <button type="submit" style="margin-top: 2%;" class="btn btn-primary">Edit</button>
    </form>
</template>
<script scoped>
import { GetById } from '@/scripts/Services'
import { UpdateCustomer } from '@/scripts/Services'
export default {
    name: 'EditCustomer',
    data() {
        return {

            firstName: '',
            lastName: '',
            email: '',
            address: '',
            dateOfBirth: '',
            city: '',
            phone: '',
            customer: {}
        }
    },
    methods: {
        getCustomer() {
            GetById(sessionStorage.getItem('customerId'))
                .then((res) => {
                    this.customer = res.data
                    console.log(this.customer)
                })
        },
        edit() {
            const updatedCustomer = {
                firstName: this.customer.firstName,
                lastName: this.customer.lastName,
                email: this.customer.email,
                address: this.customer.address,
                dateOfBirth: this.customer.dateOfBirth,
                city: this.customer.city,
                phone: this.customer.phoneNumber,

            }
            console.log(updatedCustomer)
            UpdateCustomer(sessionStorage.getItem('customerId'), updatedCustomer)
                .then((res) => {
                    console.log(res.data)
                    this.$router.push('/dashboard')

                })

        }

    },
    mounted() {
        this.getCustomer()
    }
}
</script>
<style>
.form {
    padding-left: 10%;
}
</style>