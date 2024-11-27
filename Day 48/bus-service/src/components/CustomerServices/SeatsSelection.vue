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
            // selectedSeats: [],
            seats: [],
            id: '',
            selectedSeats: []
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
        toggleSeatSelection(seatId) {
            if (this.selectedSeats.includes(seatId)) {

                this.selectedSeats = this.selectedSeats.filter((id) => id !== seatId);
            } else {

                this.selectedSeats.push(seatId);
            }
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
</style>