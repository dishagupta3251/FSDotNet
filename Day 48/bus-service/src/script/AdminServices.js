import axios from './AxiosInterceptor'
export function AllUsers() {
    return axios.get('https://localhost:7176/api/User/GetAllUsers');
}

export function GetAllBuses() {
    return axios.get('https://localhost:7176/api/Bus/GetAllBuses');
}

export function GetRoutes() {
    return axios.get('https://localhost:7176/api/Route/GetAllRoutes');
}

export function AddRoute(origin, destination) {
    return axios.post('https://localhost:7176/api/Route/CreateRoute', {
        origin,
        destination
    });
}


