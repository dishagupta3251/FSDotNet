<template>
    <nav class="navbar navbar-expand-lg navbar-light bg-light">
        <h3 style="margin-left: 1%;">Dashboard</h3>
        <div class="collapse navbar-collapse" id="navbarSupportedContent">
            <ul class="navbar-nav mr-auto">
                <li class="nav-item ">
                    <a href="#" @click="logout">Logout</a>
                </li>
            </ul>
            <form class="form-inline my-2 my-lg-0 d-flex p-2" @submit.prevent="searchCustomers">
                <input class="mr-sm-2" type="text" v-model="search.firstName" placeholder="Search by FirstName"
                    aria-label="Search">
                <input class="mr-sm-2" type="text" v-model="search.lastName" placeholder="Search by LastName"
                    aria-label="Search">
                <input class="mr-sm-2" type="text" v-model="search.phoneNumber" placeholder="Search by Phone"
                    aria-label="Search">
                <input class="mr-sm-2" type="text" v-model="search.accountNumber" placeholder="Search by AccountNumber"
                    aria-label="Search">
                <button class="btn btn-outline-success my-2 my-sm-0 btn-search" type="submit">Search</button>
            </form>
        </div>
    </nav>
    <div class="main-container-1" v-if="check">
        <main class="content-area">
            <div class="table-responsive">
                <table class="table">
                    <thead class="thead-dark">
                        <tr>
                            <th scope="col">FirstName</th>
                            <th scope="col">LastName</th>
                            <th scope="col">D.O.B</th>
                            <th scope="col">Email</th>
                            <th scope="col">Address</th>
                            <th scope="col">City</th>
                            <th scope="col">Phone</th>
                            <th scope="col">AccountNumber</th>

                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>{{ customer.firstName }}</td>
                            <td>{{ customer.lastName }}</td>
                            <td>{{ customer.dateOfBirth }}</td>
                            <td>{{ customer.email }}</td>
                            <td>{{ customer.address }}</td>
                            <td>{{ customer.city }}</td>
                            <td>{{ customer.phoneNumber }}</td>
                            <td>{{ customer.accountNumber }}</td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </main>
    </div>
    <div class="container my-5">
        <div class="d-flex justify-content-between align-items-center mb-3">
            <h2 class="text-center">Customer List</h2>
            <button class="btn btn-primary" @click="add">Add Customer</button>
        </div>
        <div class="main-container">
            <main class="content-area">
                <div class="table-responsive">
                    <table class="table">
                        <thead class="thead-dark">
                            <tr>
                                <th scope="col">FirstName</th>
                                <th scope="col">LastName</th>
                                <th scope="col">D.O.B</th>
                                <th scope="col">Email</th>
                                <th scope="col">Address</th>
                                <th scope="col">City</th>
                                <th scope="col">Phone</th>
                                <th scope="col">AccountNumber</th>
                                <th scope="col">Delete</th>
                                <th scope="col">Edit</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr v-for="customer in customers" :key="customer.custId">
                                <td>{{ customer.firstName }}</td>
                                <td>{{ customer.lastName }}</td>
                                <td>{{ customer.dateOfBirth }}</td>
                                <td>{{ customer.email }}</td>
                                <td>{{ customer.address }}</td>
                                <td>{{ customer.city }}</td>
                                <td>{{ customer.phoneNumber }}</td>
                                <td>{{ customer.accountNumber }}</td>
                                <td>
                                    <button type="button" class="btn btn-danger"
                                        @click="deleteCustomer(customer.custId)">Delete</button>
                                </td>
                                <td>
                                    <button type="button" class="btn btn-info"
                                        @click="edit(customer.custId)">Edit</button>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </main>
        </div>
    </div>
</template>

<script>
import {
    GetCustomers,
    GetCustomerFirstName,
    GetCustomerLastName,
    GetCustomerPhone,
    GetCustomerAccount,
    DeleteCustomer
} from "@/scripts/Services";

export default {
    name: "DashboardPage",
    data() {
        return {
            customers: [],
            search: {
                firstName: "",
                lastName: "",
                phoneNumber: "",
                accountNumber: ""
            },
            customer: {},
            check: false
        };
    },
    methods: {
        getUsers() {
            GetCustomers()
                .then((res) => {
                    this.customers = res.data;
                })
                .catch((err) => console.error(err));
        },
        edit(customerId) {
            sessionStorage.setItem("customerId", customerId);
            this.$router.push("/edit");
        },
        logout() {
            sessionStorage.removeItem("token");
            this.$router.push("/");
        },
        add() {
            this.$router.push("/add");
        },
        searchCustomers() {
            this.check = true;
            if (this.search.firstName) {

                GetCustomerFirstName(this.search.firstName)
                    .then((res) => {
                        this.customer = res.data;
                    })
                    .catch((err) => console.error(err));
            } else if (this.search.lastName) {

                GetCustomerLastName(this.search.lastName)
                    .then((res) => {
                        this.customer = res.data;
                    })
                    .catch((err) => console.error(err));
            } else if (this.search.phoneNumber) {
                GetCustomerPhone(this.search.phoneNumber)
                    .then((res) => {
                        this.customer = res.data;
                        console.log(res.data);
                    })
                    .catch((err) => console.error(err));
            } else if (this.search.accountNumber) {
                GetCustomerAccount(this.search.accountNumber)
                    .then((res) => {
                        this.customer = res.data;
                    })
                    .catch((err) => console.error(err));
            } else {
                this.getUsers();
            }
        },
        deleteCustomer(customerId) {
            if (sessionStorage.getItem('role') != "Teller") {
                if (confirm("Are you sure you want to delete this customer?")) {
                    DeleteCustomer(customerId)
                        .then(() => {
                            alert("Customer deleted successfully!");
                            this.getUsers();
                        })
                        .catch((err) => console.error(err));
                }
            }
            else {
                this.$router.push('/unauthorized')
            }
        }
    },
    mounted() {
        this.getUsers();
    }
};
</script>

<style scoped>
.container {
    flex-direction: column;
    max-height: 100vh;
}

.table {
    margin-top: 20px;
    width: 80%;
}

.main-container-1 {
    flex-direction: column;
    max-height: 100vh;
    margin-left: 10%;
}

#navbarSupportedContent {
    margin-left: 10%;
}

.btn-search {
    margin-left: 5%;
}
</style>
