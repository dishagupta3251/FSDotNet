import axios from './AxiosInterceptor'
export function GetOperatorBuses(id) {
    return axios.get(`https://localhost:7176/api/BusOperators/BusesWithOperator?userId=${id}`);
}

export function GetOperator(username) {
    return axios.get(`https://localhost:7176/api/BusOperators/GetOperatorByUsername?username=${username}`);
}