import axios from 'axios'


export const Login = async (input, password) => {
    try {
        var response = await axios.post('https://localhost:7176/api/User/Login', {
            input: input,
            password: password
        });
        return response;
    }
    catch (error) {
        console.log(error);
    }

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

