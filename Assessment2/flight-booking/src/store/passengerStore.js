
import { defineStore } from "pinia";
export const usePassengerStore = defineStore("passengerStore", {
    state: () => ({
        passengers: [],
    }),
    actions: {
        setPassengers(passengers) {
            this.passengers = passengers;
        },
        resetPassengers() {
            this.passengers = [];
        }
    }

});