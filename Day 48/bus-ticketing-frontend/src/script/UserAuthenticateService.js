import axios from 'axios'

export function Login(input, password) {
    return axios.post('https://localhost:7176/api/User/Login', {
        input: input,
        password: password
    });
}

export function Register(fname, lname, password, contact, email, role) {
    return axios.post('https://localhost:7176/api/User/Register', {
        firstName: fname,
        lastName: lname,
        password: password,
        contactNumber: contact,
        email: email,
        role: +role
    });
}