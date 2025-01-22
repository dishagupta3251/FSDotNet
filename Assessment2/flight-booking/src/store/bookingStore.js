import { defineStore } from "pinia";

export const useBookingStore = defineStore("bookingStore", {
    state: () => ({
        Id: "",
        source: "",
        destination: "",
        selectedFlight: "",
        selectedReturnFlight: "",
        passengers: 0,
        totalPrice: 0,
        departureDate: "",
        returnDate: "",
        trip: "oneway",

    }),
    actions: {
        setDetails(source, destination, passengers, trip, selectedFlight, selectedReturnFlight, departureDate, returnDate) {
            this.source = source;
            this.destination = destination;
            this.passengers = passengers;
            this.selectedFlight = selectedFlight;
            this.selectedReturnFlight = selectedReturnFlight
            this.trip = trip;
            this.departureDate = departureDate;
            this.returnDate = returnDate;
        },
        setPrice(price) {
            this.totalPrice = price;
        },
        resetDetails() {
            this.Id = "";
            this.source = "";
            this.destination = "";
            this.selectedFlight = "";
            this.selectedReturnFlight = "";
            this.totalPrice = 0;
            this.trip = "oneway";
            this.departureDate = "";
            this.returnDate = "";
        },
        setId(id) {
            this.Id = id;
        },


    },

});