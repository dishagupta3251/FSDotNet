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

