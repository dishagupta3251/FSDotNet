<!-- <template>

    <div class="main">
        <CustomerNavbar />
        <div style="padding: 40px;">
            <p>
                A:Aisle
                W:Window
            </p>
        </div>
        <div class="seats-detail">
            <div v-for="seat in seats" :key="seat.seatsId"
                :class="['seat-item', { selected: selectedSeats.includes(seat.seatsId) }]"
                @click="toggleSeatSelection(seat.seatsId)">
                <p>{{ seat.seatsId }}</p>
                <p>{{ seat.seat }}</p>
                <p>{{ seat.price }}</p>

            </div>

            <div>
                <button class="book-seat">Book</button>
            </div>
        </div>
    </div>
</template>

<script>
import { GetSeats } from '../../script/BusService';
import CustomerNavbar from './CustomerNavbar.vue';

export default {
    name: "SeatSelection",
    components: {
        CustomerNavbar
    },
    data() {
        return {

            seats: [],
            id: '',
            selectedSeats: []
        };
    },

    methods: {
        generateSeatLayout(id) {
            GetSeats(id)
                .then((res) => {
                    this.seats = res.data.data.seats;
                    console.log(this.seats);

                })
        },
        toggleSeatSelection(seatId) {
            if (this.selectedSeats.includes(seatId)) {

                this.selectedSeats = this.selectedSeats.filter((id) => id !== seatId);
            } else {

                this.selectedSeats.push(seatId);
            }
        },

    },

    mounted() {
        const { id } = this.$route.params;
        this.id = id;
        this.generateSeatLayout(id);
    }
};
</script>

<style scoped>
.seats-detail {
    display: flex;
    flex-wrap: wrap;
    gap: 20px;
    justify-content: center;
    padding: 20px;
    background-color: #f9f9f9;
    border-radius: 8px;
    box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
}




.seats-detail>div {
    border: 1px solid #ddd;
    border-radius: 8px;
    padding: 15px;
    width: 200px;
    background-color: #ffffff;
    text-align: center;
    transition: box-shadow 0.3s ease, transform 0.2s ease;
}


.seats-detail>div:hover {
    box-shadow: 0 4px 15px rgba(0, 0, 0, 0.2);
    transform: scale(1.05);
    background-color: rgb(205 121 31);
}


.seats-detail p:first-child {
    font-size: 18px;
    font-weight: bold;
    margin: 0 0 10px;
    color: #333;
}


.seats-detail p {
    margin: 5px 0;
    font-size: 14px;
    color: #555;
}


.book-seat {
    margin-top: 15px;
    padding: 10px 20px;
    font-size: 14px;
    font-weight: bold;
    color: #ffffff;
    background-color: #007bff;
    border: none;
    border-radius: 4px;
    cursor: pointer;
    transition: background-color 0.3s ease;
}

.book-seat:hover {
    background-color: #0056b3;
}
</style> -->
<template>
    <div id="root">
        <div class="container">
            <div class="row">
                <div class="col-8 py-5">
                    <div>
                        <table class="mx-auto">
                            <tr v-for="rowIndex in rows" :key="rowIndex">
                                <td v-for="colIndex in cols" :key="colIndex" class="pl-2" style="width: 50px">
                                    <svg v-if="!isAisle(rowIndex, colIndex)" @click="onSeatSelected(rowIndex, colIndex)"
                                        xmlns="http://www.w3.org/2000/svg" viewBox="0 0 100 100">
                                        <path :class="classifier(rowIndex, colIndex)"
                                            d="M36,17.3H80.4a8.88,8.88,0,0,1,6.72-7.25A5.77,5.77,0,0,0,81.57,6H36a5.72,5.72,0,0,0-5.76,5.66A5.71,5.71,0,0,0,36,17.3Z" />
                                        <path :class="classifier(rowIndex, colIndex)"
                                            d="M80.29,82.79H36A5.66,5.66,0,1,0,36,94.1H81.47a6.13,6.13,0,0,0,5.44-3.41A8.77,8.77,0,0,1,80.29,82.79Z" />
                                        <path :class="classifier(rowIndex, colIndex)"
                                            d="M80.08,79.7V20.5H35.92A8.85,8.85,0,0,1,27.17,13h-18a4,4,0,0,0-4.06,4V82.79a4,4,0,0,0,4.06,3.95H27.28a8.65,8.65,0,0,1,8.75-7Z" />
                                    </svg>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div class="col-4 pt-3">
                    <div class="card">
                        <div class="card-header">Selected Seats</div>
                        <div class="card-body">
                            <div v-if="selectedSeats.length > 0">
                                <p>Selected Seats: {{ selectedSeats.join(", ") }}</p>
                                <p>Total Price: Rs. {{ totalPrice }}</p>
                                <button class="btn btn-success" @click="bookSeats">
                                    Confirm Booking
                                </button>
                            </div>
                            <p v-else>No seats selected.</p>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
import { ref, computed, onMounted } from "vue";
import axios from "axios";


export default {
    name: "SeatSelection",
    props: {
        busId: { type: Number, required: true },
        seatsleft: { type: Number, required: true },
        totalseats: { type: Number, required: true }
    },
    setup(props) {
        const rows = 5;
        const cols = 12;
        const seats = ref([]); // Stores seat data from the backend
        const selectedSeats = ref([]); // Tracks selected seat IDs
        const totalPrice = computed(() =>
            selectedSeats.value.reduce(
                (sum, seatId) =>
                    sum + (seats.value.find((seat) => seat.SeatId === seatId)?.Price || 0),
                0
            )
        );

        const fetchSeats = async () => {
            try {
                const response = await axios.get(`/api/seats/${props.busId}`);
                seats.value = response.data;
            } catch (error) {
                console.error("Failed to fetch seats:", error);
            }
        };

        const isAisle = (row, col) => row === 3 && col >= 1 && col <= 11;

        const getSeat = (row, col) =>
            seats.value.find((seat) => seat.Row === row && seat.Column === col);

        const classifier = (row, col) => {
            const seat = getSeat(row, col);
            if (seat) {
                if (selectedSeats.value.includes(seat.SeatId)) {
                    return "cls-selected";
                }
                return seat.IsBooked ? "cls-booked" : "cls-available";
            }
            return "cls-default";
        };

        const onSeatSelected = (row, col) => {
            const seat = getSeat(row, col);
            if (seat && !seat.IsBooked) {
                if (selectedSeats.value.includes(seat.SeatId)) {
                    selectedSeats.value = selectedSeats.value.filter(
                        (id) => id !== seat.SeatId
                    );
                } else {
                    selectedSeats.value.push(seat.SeatId);
                }
            }
        };

        const bookSeats = async () => {
            try {
                const response = await axios.post("/api/book", {
                    BusId: props.busId,
                    CustomerId: props.customerId,
                    SelectedSeatIds: selectedSeats.value,
                    DateTime: new Date().toISOString(),
                });
                alert(`Booking successful! Booking ID: ${response.data.BookingId}`);
                fetchSeats(); // Refresh seats after booking
                selectedSeats.value = []; // Clear selected seats
            } catch (error) {
                console.error("Booking failed:", error);
                alert("Booking failed. Please try again.");
            }
        };

        onMounted(() => {
            fetchSeats();
        });

        return {
            rows,
            cols,
            seats,
            selectedSeats,
            totalPrice,
            isAisle,
            classifier,
            onSeatSelected,
            bookSeats,
        };
    },
};
</script>

<style scoped>
.pl-2 {
    padding-left: 2px;
}

.cls-available {
    fill: lightgray;
}

.cls-selected {
    fill: orange;
}

.cls-booked {
    fill: red;
}
</style>