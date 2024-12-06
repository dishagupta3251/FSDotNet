<template>
    <CustomerNavbar />
    <div class="main">
        <div class="left-screen">
            <div class="seat-map-container">
                <button disabled class="squareContainer driver my-1"
                    style="margin-left: 200px; transition: none; cursor: not-allowed;color:black">
                    Drive
                </button>
                <div class="seatmap">
                    <!-- First half of the bus (1 to 10 seats) -->
                    <section>
                        <div v-for="(row, rowIndex) in seatMapFirstHalf" :key="rowIndex" class="seat-row">
                            <button v-for="(seat, colIndex) in row" :key="colIndex" class="squareContainer my-1"
                                :class="{ selected: seat.isSelected, unavailable: seat.isAvailable }"
                                @click="toggleSelection(rowIndex, colIndex, 'firstHalf')" :disabled="seat.isAvailable">
                                <p>{{ seat.name }}</p>
                            </button>
                        </div>
                    </section>

                    <!-- Second half of the bus (11 to 20 seats) -->
                    <section>
                        <div v-for="(row, rowIndex) in seatMapSecondHalf" :key="rowIndex" class="seat-row">
                            <button v-for="(seat, colIndex) in row" :key="colIndex" class="squareContainer my-1"
                                :class="{ selected: seat.isSelected, unavailable: seat.isAvailable }"
                                @click="toggleSelection(rowIndex, colIndex, 'secondHalf')" :disabled="seat.isAvailable">
                                <p>{{ seat.name }}</p>
                            </button>
                        </div>
                    </section>
                </div>

            </div>

        </div>
        <div class="right-screen">
            <div>

            </div>
            <div v-if="selectedSeats.length == 0">

                <div style="margin-top: 50px;">
                    <div style="margin-bottom: -22px;">
                        <h5>SEAT LEGEND</h5>
                    </div>
                    <div style="display: flex; gap: 20px;">
                        <div style="margin-top: 20px;">
                            <label style="font-size:large">Available</label>
                            <h5 style="margin-top: 10px;height: 20px;" class="squareContainer my-1"> </h5>
                        </div>
                        <div style="margin-top: 20px;">
                            <label style="font-size:large">Unavailable</label>
                            <h5 style="margin-top: 5px;height: 20px;" class="squareContainer unavailable"> </h5>
                        </div>
                    </div>
                </div>
            </div>
            <div v-else>
                <BookingDetails :selected-seats="selectedSeats" :busData="busData" :selectedSeatIds="selectedSeatIds"
                    :totalFare="totalFare" />
            </div>
        </div>
    </div>



</template>

<script>
import { GetSeats } from '@/script/BusService';
import CustomerNavbar from './CustomerNavbar.vue';
import BookingDetails from './BookingDetails.vue';

export default {
    name: "SeatSelection",
    components: {
        CustomerNavbar,
        BookingDetails
    },
    props: {
        id: {
            type: Number,
            required: true,
        },
        seatsLeft: {
            type: Number,
            required: true,
        },
        totalSeats: {
            type: Number,
            required: true,
        },


    },
    data() {
        return {
            seatMapFirstHalf: [], // 2D array for the first half seat map
            seatMapSecondHalf: [], // 2D array for the second half seat map
            busData: '',
            seatsData: '',
            rowsFirstHalf: 5,
            rowsSecondHalf: 5,
            columns: 2,
            seatsDetails: [],
            selectedSeats: [],
            selectedSeatIds: [],
            totalFare: 0,

        };
    },
    methods: {
        fetchSeats(id) {
            GetSeats(id)
                .then((res) => {
                    this.busData = res.data.data;
                    this.seatsData = res.data.data.seats;
                    this.generateSeatMap();
                })
                .catch((err) => {
                    console.log(err);
                });
        },

        generateSeatMap() {
            const seats = this.seatsData;
            console.log("seats " + this.seatsData);
            for (let i = 0; i < seats.length; i++) {
                this.seatsDetails.push(seats[i]);
            }
            // console.log("seatsDetails: " + this.seatsDetails);
            const half = Math.ceil(this.totalSeats / 2);
            const firstHalfSeats = seats.slice(0, half);
            const secondHalfSeats = seats.slice(half);

            this.seatMapFirstHalf = this.createSeatRows(firstHalfSeats, this.rowsFirstHalf, this.columns);
            this.seatMapSecondHalf = this.createSeatRows(secondHalfSeats, this.rowsSecondHalf, this.columns);
        },

        createSeatRows(seatData, rows, columns) {
            const seatRows = [];
            for (let i = 0; i < rows; i++) {
                const start = i * columns;
                const rowSeats = seatData.slice(start, start + columns).map((seat) => ({
                    name: seat.seat,
                    isSelected: false,
                    seatId: seat.seatId, // Store the seat ID
                    fare: seat.price,
                    isAvailable: seat.status, // Available or not based on seat status
                }));
                if (rowSeats.length > 0) {
                    seatRows.push(rowSeats);
                }

            }

            return seatRows;
        },
        toggleSelection(rowIndex, colIndex, section) {
            const seatMap = section === 'firstHalf' ? this.seatMapFirstHalf : this.seatMapSecondHalf;
            const seat = seatMap[rowIndex][colIndex];
            seat.isSelected = !seat.isSelected;
            if (seat.isSelected) {
                this.selectedSeats.push(seat.name);
                this.selectedSeatIds.push(seat.seatId);
                this.totalFare += seat.fare;

            } else {

                const index = this.selectedSeats.indexOf(seat.name);
                if (index > -1) {
                    this.selectedSeats.splice(index, 1);
                    this.selectedSeatIds.splice(index, 1);
                    this.totalFare -= seat.fare;
                }

            } console.log("fare", this.totalFare)

        },



    },

    mounted() {
        this.fetchSeats(this.id);
    },
};
</script>

<style scoped>
.main {
    display: flex;

}

.seat-map-container {
    display: flex;
    flex-direction: column;
    align-items: left;
    margin-left: 100px;
    margin-top: 70px;
    gap: 10px;
}

.left-screen {
    height: 100%;
    width: 50%;
    position: fixed;
    z-index: 1;
    top: 0;
    overflow: hidden;
    padding-top: 20px;
}

.right-screen {
    width: 50%;
    margin-top: 100px;
    margin-left: 600px;
}

.seatmap {
    display: flex;
    gap: 28px;
    margin-top: -9px;

}

.seat-row {
    display: flex;
    justify-content: center;
    gap: 11px;
}

.squareContainer.driver:hover {
    background: none;
    border: 2px solid black;
    /* border: 2px solid transparent; */
}

.squareContainer {
    display: flex;
    justify-content: center;
    align-items: center;
    width: 50px;
    height: 70px;
    background: transparent;
    border: 2px solid black;
    border-radius: 8px;
    font-size: 0.8rem;
    font-weight: bold;
    transition: 0.2s ease-out;
}

.squareContainer:hover {
    background: transparent;
    border: 2px solid blue;
}

.squareContainer.selected {
    background: lightgreen;
    border: 2px solid transparent;
}

.squareContainer.unavailable {
    background: lightgray;
    border: 2px solid transparent;
}

.book-button {
    margin-top: 5px;
    padding: 10px 20px;
    margin-left: 400px;
    background-color: green;
    color: white;
    border: none;
    border-radius: 5px;
    cursor: pointer;
}

.book-button:hover {
    background-color: darkgreen;
}
</style>
