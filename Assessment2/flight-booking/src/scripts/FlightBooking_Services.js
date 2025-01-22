import axios from "axios"

export function Source() {
    return axios.get('http://localhost:8082/get-source')
}

export function Destination(source) {
    return axios.get(`http://localhost:8082/get-destination/${source}`);
}



export function Flights(source, destination, date, returnDate) {
    return axios.post('http://localhost:8082/getall-flights', {
        source: source,
        destination: destination,
        date: date,
        return_date: returnDate
    })
}

export function Booking(id, type, onewayFlightId, roundFlightId, oneWayType, roundWayType, totalFare) {
    return axios.post('http://localhost:8082/create-booking', {
        id: id,
        tripType: type,
        oneWayTripFlightId: onewayFlightId,
        roundTripFlightId: roundFlightId,
        oneWayTripFlightType: oneWayType,
        roundTripFlightType: roundWayType,
        totalFare: totalFare
    })
}

export function Passengers(bookingId, Title, FirstName, LastName) {
    return axios.post('http://localhost:8082/add-passenger', {
        BookingId: bookingId,
        Title: Title,
        FirstName: FirstName,
        LastName: LastName
    }
    )
}

export function AddSeats(flightId, type, seat, bookingId) {
    return axios.post('http://localhost:8082/add-seats', {
        FlightId: flightId,
        FlightType: type,
        SeatNo: seat,
        BookingId: bookingId
    })

}
export function GetBookedSeats(flightId) {
    return axios.get(`http://localhost:8082/get-seats/${flightId}`);

}
export function GetPdf(id) {
    return axios.get(`http://localhost:8082/get-destination/${id}`);
}
