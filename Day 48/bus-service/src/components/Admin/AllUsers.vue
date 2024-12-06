<template>
    <div class="main-container">
        <main class="content-area">
            <h3>Users List</h3>
            <div class="table-responsive">
                <table class="table">
                    <thead class="thead-dark">
                        <tr>
                            <th scope="col">UserId</th>
                            <th scope="col">Name</th>
                            <th scope="col">Username</th>
                            <th scope="col">Email</th>
                            <th scope="col">Role</th>
                            <th scope="col">Delete</th>
                            <th scope="col">Info</th>

                        </tr>
                    </thead>
                    <tbody>
                        <tr v-for="user in users" :key="user.userId">
                            <th scope="row">{{ user.userId }}</th>
                            <td>{{ user.firstName }}</td>
                            <td>{{ user.username }}</td>
                            <td>{{ user.email }}</td>
                            <td>{{ getRoleLabel(user.role) }}</td>

                            <td><button type="button" class="btn btn-danger">Delete</button></td>
                            <td><button type="button" class="btn btn-info">Info</button></td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </main>
    </div>
</template>

<script>
import { AllUsers } from '@/script/AdminServices';
export default {
    name: " AdminUsers",

    data() {
        return {
            users: []
        }
    },
    methods: {
        getUsers() {
            AllUsers()
                .then((res) => {
                    this.users = res.data;
                    console.log(res.data);
                })
                .catch((err) => console.error(err));
        },
        getRoleLabel(role) {
            console.log(role);
            if (role == 0) return "Customer";
            else if (role == 1) return "BusOperator";
            else return "Admin";
        }
    },

    mounted() {
        this.getUsers();
    }
} 
</script>
<style scoped>
.main-container {
    display: flex;
    margin-left: 40px;
    width: 100%;
    justify-content: center;
    background-color: #f8f9fa;
}

/* Content area styling */
.content-area {
    padding: 20px;
    background: #ffffff;
    width: 100%;
    border-radius: 8px;
    box-shadow: 0 4px 10px rgba(0, 0, 0, 0.1);
}

/* Table responsiveness */
.table-responsive {
    max-height: 400px;

    overflow-y: auto;
}

/* Table styling */
.table {
    width: 100%;
    border-collapse: collapse;
}

thead {
    background-color: #343a40;
    color: white;
}

th,
td {
    text-align: left;
    padding: 12px;
}

tbody tr:nth-child(odd) {
    background-color: #f2f2f2;
}

tbody tr:nth-child(even) {
    background-color: #ffffff;
}

tbody tr:hover {
    background-color: #e9ecef;
}
</style>
