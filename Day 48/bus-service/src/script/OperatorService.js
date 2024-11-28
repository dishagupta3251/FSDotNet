
export function GetOperatorBuses(id) {
    return axios.get('https://localhost:7176/api/BusOperators/BusesWithOperator', {
        param: {
            id: id
        }
    });
}