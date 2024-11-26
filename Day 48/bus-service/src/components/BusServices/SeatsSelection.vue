<template>
    <!-- <div class="seat-selection">
        
        <div class="seat-row" v-for="(row, rowIndex) in seatLayout" :key="`row-${rowIndex}`">
            <div class="seat" v-for="(seat, seatIndex) in row" :key="`seat-${rowIndex}-${seatIndex}`" :class="{
                'booked-seat': bookedSeats.includes(seat.seatNo),
                'selected-seat': selectedSeats.includes(seat.seatNo),
                'available-seat': !bookedSeats.includes(seat.seatNo),
            }" @click="toggleSeatSelection(seat.seatNo)" v-if="seat.seatNo">
                {{ seat.seatNo }}
            </div>
        </div>

          <div class="selected-seats">
            <h3>Selected Seats:</h3>
            <p v-if="selectedSeats.length === 0">No seats selected</p>
            <p v-else>{{ selectedSeats.join(', ') }}</p>
        </div>

     
        <button class="proceed-btn" :disabled="selectedSeats.length === 0" @click="proceedToBooking">
            Proceed to Book
        </button>
    </div> -->
    <div>
        <p>
            A:Aisle
            W:Window
        </p>
    </div>
    <div v-for="seat in seats" :key="seat.seatsId">
        <p>{{ seat.seatsId }}</p>
        <p>{{ seat.seat }}</p>
        <p>{{ seat.price }}</p>

    </div>
</template>

<script>
import { GetSeats } from '../../script/BusService';

export default {
    name: "SeatSelection",
    // props: {
    //     totalRows: { type: Number, default: 10 },
    //     seatsPerRow: { type: Number, default: 4 },
    //     bookedSeats: { type: Array, default: () => [] },
    // },
    data() {
        return {
            // selectedSeats: [],
            seats: [],
            id: ''
        };
    },
    // created() {
    //     this.generateSeatLayout();
    // },
    methods: {
        generateSeatLayout(id) {
            GetSeats(id)
                .then((res) => {
                    this.seats = res.data.data.seats;
                    console.log(this.seats);

                })
        },

    },
    //         let seatCounter = 1;

    //         for (let i = 0; i < this.totalRows; i++) {
    //             const row = [];
    //             for (let j = 0; j < this.seatsPerRow; j++) {
    //                 row.push({ seatNo: seatCounter });
    //                 seatCounter++;
    //             }
    //             layout.push(row);
    //         }

    //         this.seatLayout = layout;
    //     },
    //     toggleSeatSelection(seatNo) {
    //         if (this.bookedSeats.includes(seatNo)) return;

    //         if (this.selectedSeats.includes(seatNo)) {
    //             this.selectedSeats = this.selectedSeats.filter((seat) => seat !== seatNo);
    //         } else {
    //             this.selectedSeats.push(seatNo);
    //         }
    //     },
    //     proceedToBooking() {
    //         alert(`Proceeding to book seats: ${this.selectedSeats.join(", ")}`);
    //     },
    // },
    mounted() {
        const { id } = this.$route.params;
        this.id = id;
        this.generateSeatLayout(id);
    }
};
</script>

<style scoped>
/* .seat-selection {
    display: flex;
    flex-direction: column;
    align-items: center;
    gap: 20px;
    background-color: #f7f7f7;
    padding: 20px;
    border: 1px solid #ddd;
    border-radius: 8px;
}

.seat-row {
    display: grid;
    grid-template-columns: repeat(4, 50px);
    gap: 10px;
    justify-content: center;
    margin-bottom: 10px;
}

.seat {
    width: 50px;
    height: 50px;
    display: flex;
    justify-content: center;
    align-items: center;
    border: 1px solid #ccc;
    border-radius: 4px;
    background-color: #fff;
    cursor: pointer;
}

.booked-seat {
    background-color: #d84e55;
    color: #fff;
    cursor: not-allowed;
}

.selected-seat {
    background-color: #4caf50;
    color: #fff;
}

.available-seat:hover {
    background-color: #e0f7fa;
}

.proceed-btn {
    background-color: #d84e55;
    color: #fff;
    padding: 10px 20px;
    border: none;
    border-radius: 4px;
    font-size: 16px;
    cursor: pointer;
}

.proceed-btn:disabled {
    background-color: #ccc;
    cursor: not-allowed;
}

.selected-seats {
    text-align: center;
} */
</style>