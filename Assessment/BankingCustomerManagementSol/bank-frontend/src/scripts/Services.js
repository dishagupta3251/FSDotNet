import axios from './AxiosInterceptor'

export function GetCustomers() {
    return axios.get('https://localhost:7282/api/Customer/GetAll');

}

export function Add(firstName, lastName, address, city, email, dateOfBirth, phone) {
    return axios.post('https://localhost:7282/api/Customer', {
        firstName,
        lastName,
        address,
        city,
        email,
        dateOfBirth,
        phoneNumber: phone,

    });
}

export function GetById(id) {
    return axios.get(`https://localhost:7282/customerId/${id}`);

}
export function UpdateCustomer(id, customer) {
    console.log(id, customer);
    return axios.put(`https://localhost:7282/api/Customer/${id}`, {
        firstName: customer.firstName,
        lastName: customer.lastName,
        address: customer.address,
        city: customer.city,
        email: customer.email,
        dateOfBirth: customer.dateOfBirth,
        phoneNumber: customer.phone,
    });
}

export function GetCustomerLastName(lastName) {
    return axios.get(`https://localhost:7282/lastname/${lastName}`);
}

export function GetCustomerFirstName(firstName) {
    return axios.get(`https://localhost:7282/firstname/${firstName}`);
}

export function GetCustomerPhone(phone) {
    return axios.get(`https://localhost:7282/phone/${phone}`);

}

export function GetCustomerAccount(account) {
    return axios.get(`https://localhost:7282/account/${account}`);
}

export function DeleteCustomer(id) {
    return axios.delete(`https://localhost:7282/api/Customer/${id}`);
}

