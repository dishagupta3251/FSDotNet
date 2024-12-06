import axios from './AxiosInterceptor'


export function GetCustomer(username) {
    return axios.get('https://localhost:7176/api/Customers/GetCustomerByUsername', {
        params: { username: username }
    });

}

export function GetBuses(from, to, dateTime) {
    return axios.get('https://localhost:7176/api/Booking/SeeAllBuses', {
        params: {
            from,
            to,
            dateTime,
            pagenum: 1,
            pagesize: 10
        }
    });

}

export function GetSeats(id) {
    try {
        return axios.get(`https://localhost:7176/api/Booking/busAndSeatsDetails`, {
            params: {
                busId: id
            }
        });
    }
    catch (error) {
        console.log(error);
    }

}