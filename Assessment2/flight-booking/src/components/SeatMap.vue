<template>
    <div style="display: flex;">
        <button class="btn btn-primary" style="margin-left: 5%;height: 46px;" @click="handleBack">Back</button>
        <div class="total-fare">
            <div>
                <h3>Total Fare: ₹{{ totalFare }}</h3>
            </div>
            <button class="btn btn-primary button-continue" :disabled="!canContinue" @click="proceedToPayment">
                Continue
            </button>
        </div>
    </div>



    <div class="container">
        <h2>Select Seats</h2>
        <div class="legends">
            <div class="legend">
                <div class="seat available"></div>
                <span>Available</span>
            </div>
            <div class="legend">
                <div class="seat selected"></div>
                <span>Selected</span>
            </div>
            <div class="legend">
                <div class="seat booked"></div>
                <span>Booked</span>
            </div>
        </div>
        <div class="seat-layout">
            <div class="seat-row" v-for="row in seatRows" :key="row">
                <div class="seat-group">
                    <div class="seat" v-for="seat in seatColumns" :key="seat" :class="getSeatClass(row, seat)"
                        @click="toggleSeatSelection(seatNumber(row, seat))">
                        {{ seatNumber(row, seat) }}
                    </div>
                </div>
                <div class="spacer"></div>
                <div class="seat-group">
                    <div class="seat" v-for="seat in seatColumns" :key="seat" :class="getSeatClass(row, seat + 3)"
                        @click="toggleSeatSelection(seatNumber(row, seat + 3))">
                        {{ seatNumber(row, seat + 3) }}
                    </div>
                </div>
            </div>
        </div>




    </div>
</template>

<script>
import { useSeatStore } from "@/store/seatsStore";
import { useBookingStore } from "@/store/bookingStore";
import { usePassengerStore } from "@/store/passengerStore";
import { GetBookedSeats } from "@/scripts/FlightBooking_Services";


export default {
    name: "SeatMap",

    data() {
        return {
            selectedSeats: [],
            totalFare: 0, // Track the total fare dynamically
            bookingStore: useBookingStore(),
            passengerStore: usePassengerStore(),
            seatStore: useSeatStore(),
            bookedSeats: [], // Example of booked seats
        };
    },
    computed: {
        passengers() {
            return this.bookingStore.passengers;
        },
        canContinue() {
            return this.selectedSeats.length === this.passengers;
        },
        seatRows() {
            return 30;
        },
        seatColumns() {
            return 3;
        }
    },
    methods: {
        seatNumber(row, seat) {
            return (row - 1) * 6 + seat;
        },
        toggleSeatSelection(seat) {
            if (this.isBooked(seat)) return;

            const Fare = this.bookingStore.selectedFlight.fare;
            const index = this.selectedSeats.indexOf(seat);

            if (index > -1) {
                this.selectedSeats.splice(index, 1);
                this.totalFare -= Fare;
            } else if (this.selectedSeats.length < this.passengers) {
                this.selectedSeats.push(seat);
                this.totalFare += Fare;
            }

            this.seatStore.setSeats([...this.selectedSeats], 'departure');
            this.bookingStore.setPrice(this.totalFare);
        },
        isSelected(seat) {
            return this.selectedSeats.includes(seat);
        },
        isBooked(seat) {
            return this.bookedSeats.includes(seat);
        },
        getSeatClass(row, seat) {
            const seatNum = this.seatNumber(row, seat);
            return {
                selected: this.isSelected(seatNum),
                booked: this.isBooked(seatNum),
            };
        },
        addBookedSeats(seats) {
            console.log("addBookedSeats", seats)
            for (var i = 0; i < seats.length; i++) {
                this.bookedSeats.push(seats[i]);
            }
        },
        getbookedSeats(Id) {
            console.log(Id)
            GetBookedSeats(Id)
                .then((response) => {
                    console.log(response.data)
                    this.addBookedSeats(response.data.data)
                })
                .catch((error) => console.error(error));
        },
        proceedToPayment() {
            this.$router.push("/payment");
        },
        handleBack() {
            this.seatStore.resetSeats();
            this.passengerStore.resetPassengers();
            this.$router.push("/form");
        },
    },
    mounted() {
        this.totalFare = this.bookingStore.totalPrice;
        if (this.bookingStore.trip === 'roundtrip') {
            this.getbookedSeats(this.bookingStore.selectedReturnFlight.Id);
        }
        else {
            console.log(this.bookingStore.selectedFlight.Id)
            this.getbookedSeats(this.bookingStore.selectedFlight.Id);
        }

    }
}

</script>


<style scoped>
body {
    font-family: Arial, sans-serif;
    background-color: #f5f5f5;
    display: flex;
    justify-content: center;
    align-items: center;
    height: 100vh;
    margin: 0;
}

.container {
    background-color: white;
    border: 1px solid #ccc;
    padding: 30px;
    border-radius: 8px;
    text-align: center;
    width: 400px;
}

h2 {
    margin-bottom: 20px;
}

.legends {
    display: flex;
    justify-content: space-around;
    margin-bottom: 20px;
}

.legend {
    display: flex;
    align-items: center;
}

.legend .seat {
    margin-right: 5px;
}

.seat-layout {
    display: flex;
    flex-direction: column;
    gap: 10px;
}

.seat-row {
    display: flex;
    justify-content: center;
    align-items: center;
    gap: 20px;
}

.seat-group {
    display: flex;
    gap: 5px;
}

.seat {
    width: 30px;
    height: 30px;
    background-color: #ddd;
    border: 1px solid #999;
    border-radius: 4px;
    cursor: pointer;
    display: flex;
    justify-content: center;
    align-items: center;
}

.seat.selected {
    background-color: #4caf50;
    color: white;
}

.seat.booked {
    background-color: #f44336;
    color: white;
    cursor: not-allowed;
}

.seat:hover {
    background-color: #b3e5fc;
}

.spacer {
    width: 40px;
}

.total-fare {
    margin-left: 70%;
}

.button-continue {
    margin-left: 70%;
}
</style>
