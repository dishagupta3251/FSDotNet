import axios from './AxiosInterceptor'
export function AllUsers() {
    return axios.get('https://localhost:7176/api/User/GetAllUsers');
}

export function GetAllBuses() {
    return axios.get('https://localhost:7176/api/Bus/GetAllBuses');
}
