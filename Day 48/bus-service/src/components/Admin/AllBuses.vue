<template>
    <div class="main-container">
        <main class="content-area">
            <h3>Buses List</h3>
            <div class="table-responsive">
                <table class="table">
                    <thead class="thead-dark">
                        <tr>
                            <th scope="col">BusId</th>
                            <th scope="col">BusName</th>
                            <th scope="col">Type</th>
                            <th scope="col">Total Seats</th>
                            <th scope="col">Standard Fare</th>
                            <th scope="col">Premium Fare</th>
                            <th scope="col">Status</th>
                            <th scope="col">Schedule</th>
                            <th scope="col">Operator Details</th>
                            <th scope="col">Delete</th>

                        </tr>
                    </thead>
                    <tbody>
                        <tr v-for="bus in buses" :key="bus.busId">
                            <th scope="row">{{ bus.busId }}</th>
                            <td>{{ bus.busNumber }}</td>
                            <td>{{ getType(bus.type) }}</td>
                            <td>{{ bus.numberOfSeats }}</td>
                            <td>{{ bus.standardFare }}</td>
                            <td>{{ bus.premiumFare }}</td>
                            <td class="status">{{ getStatus(bus.status).stat }}</td>
                            <td><button type="button" class="btn btn-primary">Details</button></td>
                            <td><button type="button" class="btn btn-info">Info</button></td>
                            <td><button type="button" class="btn btn-danger">Delete</button></td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </main>
    </div>
</template>
<script>
import { GetAllBuses } from '@/script/AdminServices';
export default {
    name: "AllBuses",
    data() {
        return {
            buses: [],
            busStatus: {
                stat: '',
                color: ''
            }
        }
    },
    methods: {
        async getBuses() {
            await GetAllBuses()
                .then((res) => {
                    this.buses = res.data;
                })
                .catch((err) => { console.error(err); });
        },
        getType(type) {
            if (type == 0) return "AC"
            return "Non-AC"
        },
        getStatus(status) {
            if (status == 0) {

                return {
                    stat: "Running",
                    color: "green"
                }
            }
            else {
                return {
                    stat: "Stopped",
                    color: "red"
                }
            }
        }

    },
    mounted() {
        this.getBuses();
    }

}

</script>
<style scoped>
.main-container {
    display: flex;
    width: 80%;
    margin-left: 40px;
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

button {
    font-size: 15px;
}

.status {
    color: green;
    font-weight: bold;
}

/* Table responsiveness */
.table-responsive {
    max-height: 650px;

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