<template>
    <div class="main">
        <div class="details">
            <div style="display: flex;gap:337px"><label>
                    Date
                </label>
                <p>{{ date }}</p>
            </div>
            <div style="display: flex;gap:270px">
                <lable>Boarding Stop</lable>
                <p>{{ busData.source }}</p>
            </div>
            <div style="display: flex; gap:270px">
                <label>Dropping Stop</label>
                <p>{{ busData.destination }}</p>
            </div>
            <hr>
            <div>
                <div>
                    <p style="    margin-bottom: -12px;">Seat No.</p>
                </div>
                <div class="seats">
                    <p v-for="(item, index) in selectedSeats" :key="index">{{ item }}</p>
                </div>

            </div>

            <hr>
            <div class="fare">
                <div style="display: flex; gap:320px">
                    <p>Total Fare</p>
                    <p>Rs.{{ totalFare }}</p>
                </div>
            </div>
            <button @click="book">Book</button>
        </div>


    </div>



</template>

<script>

import { GetBooking } from '@/script/BookingService';



export default {
    name: "BookingDetails",
    data() {
        return {
            customer: '',
            date: '',
            bookingId: 0,
        }
    },

    props: {
        selectedSeats: {
            type: Array,
            required: true
        },
        selectedSeatIds: {
            type: Array,
            required: true
        },
        busData:
        {
            type: Object,
            required: true
        },
        totalFare: {
            type: Number,
            required: true
        }

    },
    methods: {
        book() {
            GetBooking(this.busData.busId, this.customer.customerId, this.date, this.selectedSeatIds)
                .then((res) => {
                    this.bookingId = res.data.id
                    this.$router.push({
                        name: 'PaymentPage',
                        query: {
                            bookingId: this.bookingId
                        }
                    });
                })
                .catch((err) => {
                    console.log(err)
                    alert('Failed to book. Please try again later.')

                })

        }


    },

    mounted() {
        this.customer = JSON.parse(sessionStorage.getItem("customer"))
        console.log(this.customer);
        this.date = sessionStorage.getItem('selectedDate')
        console.log(this.date)

    }

};
</script>

<style scoped>
.seats {
    display: flex;
    flex-direction: row-reverse;
    gap: 2%;
    justify-content: flex-start;
    margin-top: 10px;
    align-items: center;


}

button {
    margin-top: 5px;
    width: 100%;
}

button:hover {
    background-color: red;
}

.main {


    /* justify-content: space-between; */
    border: 2px solid white;
    box-shadow: 0px 2px 4px #333;

    width: 550px;
    height: 400px;
    padding: 43px;
    background: white;
    position: relative;

}

p {
    font-weight: bold;
}

label {
    font-size: medium;
}

.details {
    width: 100%;
}

button {
    padding: 10px 20px;
    background-color: rgb(205 121 31);
    box-shadow: 0px 2px 4px #333;
    color: white;
    font-size: 1em;
    border: none;
    border-radius: 5px;
    cursor: pointer;
}
</style>