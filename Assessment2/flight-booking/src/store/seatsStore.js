import { defineStore } from "pinia";

export const useSeatStore = defineStore("seatStore", {
    state: () => ({
        seats: {
            departure: [],
            return: []
        },
    }),
    actions: {
        setSeats(seats, flightType) {
            this.seats[flightType] = seats;
        },
        resetSeats() {
            this.seats = { departure: [], return: [] };
        }
    }
});
