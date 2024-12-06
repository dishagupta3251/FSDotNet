<template>

    <div class="main-container">

        <main class="content-area">
            <div class="d-flex justify-content-between">
                <h1>Routes List</h1>
                <button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#exampleModal">Add
                    Route</button>
            </div>

            <div class="table-responsive">
                <table class="table">
                    <thead class="thead-dark">
                        <tr>
                            <th scope="col">RouteId</th>
                            <th scope="col">Source</th>
                            <th scope="col">Destination</th>

                        </tr>
                    </thead>
                    <tbody>
                        <tr v-for="r in routes" :key="r.routeId">
                            <th scope="row">{{ r.routeId }}</th>
                            <td>{{ r.origin }}</td>
                            <td>{{ r.destination }}</td>


                        </tr>
                    </tbody>
                </table>
            </div>
        </main>
        <!-- Modal -->
        <div class="modal fade" id="exampleModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="exampleModalLabel">Add Route</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        <form>
                            <div>
                                <label for="source">Source</label>
                                <input type="text" placeholder="Source" v-model="source">
                            </div>
                            <div>
                                <label for="destination">Destination</label>
                                <input type="text" placeholder="destination" v-model="destination">
                            </div>
                        </form>
                        <button type="sumbit" class="btn btn-primary" @click="addRoute()">Add</button>
                    </div>
                </div>
            </div>
        </div>
        <!-- <button class="btn btn-primary">Add route</button> -->
    </div>

</template>
<script>
import { AddRoute, GetRoutes } from '@/script/AdminServices';

export default {
    name: "BusRoutes",
    data() {
        return {
            routes: [],
            source: '',
            destination: ''
        }
    },
    methods: {
        Routes() {
            GetRoutes()
                .then(response => {
                    this.routes = response.data;
                    console.log(this.routes);
                })
        },
        addRoute() {
            AddRoute(this.source, this.destination)
                .then((res) => {
                    console.log(res);
                    alert("Added")
                    this.Routes();
                })
        }
    },
    mounted() {
        this.Routes();

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