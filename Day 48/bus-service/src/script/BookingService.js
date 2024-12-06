import axios from './AxiosInterceptor'


export function GetBooking(id, custId, dateTime, seats) {
    return axios.post('https://localhost:7176/api/Booking/BookBus', {

        busId: id,
        customerId: custId,
        dateTime,
        selectedSeatIds: seats

    })
}

export function Payment(id, type) {
    return axios.post('https://localhost:7176/api/Booking/payment', {

        bookingId: id,
        type: type

    })
}

export function History(id) {
    return axios.get(`https://localhost:7176/api/Booking/history?customerId=${id}`);
}