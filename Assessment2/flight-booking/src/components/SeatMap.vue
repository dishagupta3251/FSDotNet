<template>
    <button class="btn btn-primary button" style="margin-left: 5%;" @click="handleBack()">Back</button>
    <button class="btn btn-primary" @click="handleBack()">Back</button>
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
            <div class="seat-row" v-for="row in 30" :key="row">
                <div class="seat-group">
                    <div class="seat" v-for="seat in 3" :key="seat"
                        :class="{ selected: isSelected(seatNumber(row, seat)), booked: isBooked(seatNumber(row, seat)) }"
                        @click="toggleSeatSelection(seatNumber(row, seat))">
                        {{ seatNumber(row, seat) }}
                    </div>
                </div>
                <div class="spacer"></div>
                <div class="seat-group">
                    <div class="seat" v-for="seat in 3" :key="seat"
                        :class="{ selected: isSelected(seatNumber(row, seat + 3)), booked: isBooked(seatNumber(row, seat + 3)) }"
                        @click="toggleSeatSelection(seatNumber(row, seat + 3))">
                        {{ seatNumber(row, seat + 3) }}
                    </div>
                </div>
            </div>
        </div>
        <button class="btn btn-primary" :disabled="!canContinue" @click="proceedToPayment">Continue</button>
    </div>
</template>

<script>
export default {
    name: 'SeatMap',
    data() {
        return {
            passengers: 0,
            selectedSeats: [],
            bookedSeats: ['1', '4', '7'] // Example of booked seats
        };
    },
    computed: {
        canContinue() {
            return this.selectedSeats.length === this.passengers;
        }
    },
    methods: {
        seatNumber(row, seat) {
            return (row - 1) * 6 + seat;
        },
        toggleSeatSelection(seat) {
            if (this.isBooked(seat)) return; // Prevent selection of booked seats
            const index = this.selectedSeats.indexOf(seat);
            if (index > -1) {
                this.selectedSeats.splice(index, 1);
            } else if (this.selectedSeats.length < this.passengers) {
                this.selectedSeats.push(seat);
            }
        },
        isSelected(seat) {
            return this.selectedSeats.includes(seat);
        },
        isBooked(seat) {
            return this.bookedSeats.includes(seat);
        },
        proceedToPayment() {
            // Logic to proceed to payment
        },
        handleBack() {
            this.$router.push('/search');
        }
    },
    mounted() {
        this.passengers = parseInt(sessionStorage.getItem('passengers'), 10) || 0;
    }
};
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
    padding: 20px;
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
</style>