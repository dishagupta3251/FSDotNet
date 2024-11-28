<template>
	<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css" />

	<div :class="['container', { 'right-panel-active': isSignUp }]">

		<div class="form-container sign-up-container">
			<form>
				<h1>Create account</h1>
				<div class="social-container">
					<a href="#" class="social" target="_blank" aria-label="Facebook">
						<i class="fab fa-facebook-f"></i>
					</a>
					<a href="#" class="social" target="_blank" aria-label="Google">
						<i class="fab fa-google-plus-g"></i>
					</a>
					<a href="#" class="social" target="_blank" aria-label="LinkedIn">
						<i class="fab fa-linkedin-in"></i>
					</a>
				</div>
				<span>or use your email for registration</span>
				<input v-model="fname" placeholder="First Name" />
				<input v-model="lname" placeholder="Last Name" />
				<input v-model="contact" placeholder="Contact" />
				<input v-model="email" placeholder="Email" />
				<input v-model="registerPassword" type="password" placeholder="Password" />
				<!-- Role Dropdown -->

				<div class="dropdown">

					<select v-model="role" id="role" required>
						<option value="" disabled selected>Select your role</option>
						<option value="2">Admin</option>
						<option value="1">Bus Operator</option>
						<option value="0">Customer</option>
					</select>
				</div>
				<button type="submit" @click="register">Sign Up</button>
			</form>
		</div>

		<div class="form-container sign-in-container">
			<form>
				<h1>Sign In</h1>
				<div class="social-container">
					<a href="#" class="social"><i class="fab fa-facebook-f"></i></a>
					<a href="#" class="social"><i class="fab fa-google-plus-g"></i></a>
					<a href="#" class="social"><i class="fab fa-linkedin-in"></i></a>
				</div>
				<span>or use your account</span>
				<input v-model="input" placeholder="Email or Username" />
				<input v-model="loginPassword" type="password" placeholder="Password" />
				<a href="#">Forgot your password?</a>
				<button type="submit" @click="login">Sign In</button>
			</form>
		</div>

		<div>
			<div v-if="showToastMessage" class="toast">
				{{ username }}
			</div>
		</div>

		<div class="overlay-container">
			<div class="overlay">
				<div class="overlay-panel overlay-left">
					<h1>Welcome Back!</h1>
					<p>To keep connected with us please login with your personal info</p>
					<button class="ghost" @click="toggleSignIn">Sign In</button>
				</div>
				<div class="overlay-panel overlay-right">
					<h1>Hello, Friend!</h1>
					<p>Enter your personal details and start your journey with us</p>
					<button class="ghost" @click="toggleSignUp">Sign Up</button>
				</div>
			</div>
		</div>
	</div>

</template>

<script>
import router from '@/script/router';
import { Register, Login } from "@/script/UserAuthenticateService";
import { jwtDecode } from 'jwt-decode';

export default {
	name: 'AuthForm',
	data() {
		return {
			showToastMessage: false,
			username: '',
			isSignUp: false,
			input: '',
			fname: '',
			lname: '',
			contact: '',
			email: '',
			loginPassword: '',
			registerPassword: '',
			role: ''
		};
	},
	methods: {

		toggleSignUp() {
			this.isSignUp = true;
		},

		toggleSignIn() {
			this.isSignUp = false;
		},
		register(event) {

			event.preventDefault();
			Register(this.fname, this.lname, this.registerPassword, this.contact, this.email, this.role)
				.then((response) => {

					console.log(response)
					this.showToastMessage = true;
					this.username = response.data.data
					setTimeout(() => {
						this.showToastMessage = false;
						router.push('/auth')
					});

				})
				.catch((err) => {
					alert(err.response.data);
				});

		},


		async login(event) {
			event.preventDefault();
			const res = await Login(this.input, this.loginPassword)
			if (res.status == 200) {
				sessionStorage.setItem("token", res.data.token);
				const token = res.data.token;
				const decoded = jwtDecode(token);
				const role = decoded["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];
				sessionStorage.setItem("role", role);
				sessionStorage.setItem("id", res.data.userId);
				if (role == "Customer") {
					router.push('/search')
				} else if (role == "BusOperator") {

					router.push('/operatordashboard')
				}
				else if (role == "Admin") {

					router.push('/admindashboard')
				}
				else {

					router.push('/search')
				}



			}

		}

	},
};
</script>

<style scoped>
@import url('https://fonts.googleapis.com/css?family=Montserrat:400,800');

* {
	box-sizing: border-box;
}

body {
	background: #f6f5f7;
	display: flex;
	justify-content: center;
	align-items: center;
	flex-direction: column;
	font-family: 'Montserrat', sans-serif;
	height: 100vh;
	margin: -20px 0 50px;
}

h1 {
	font-weight: bold;
	margin: 0;
}

h2 {
	text-align: center;
}

p {
	font-size: 14px;
	font-weight: 100;
	line-height: 20px;
	letter-spacing: 0.5px;
	margin: 20px 0 30px;
}

span {
	font-size: 12px;
}

.toast {
	position: fixed;
	top: 20px;
	left: 20px;
	background-color: #28a745;
	color: white;
	padding: 15px 20px;
	border-radius: 5px;
	box-shadow: 0 4px 8px rgba(0, 0, 0, 0.2);
	z-index: 1000;
	font-size: 14px;
	animation: fadeInOut 3s ease forwards;
}

/* Fade-in and Fade-out Animation */
@keyframes fadeInOut {
	0% {
		opacity: 0;
		transform: translateY(10px);
	}

	10%,
	90% {
		opacity: 1;
		transform: translateY(0);
	}

	100% {
		opacity: 0;
		transform: translateY(10px);
	}
}

a {
	color: #333;
	font-size: 14px;
	text-decoration: none;
	margin: 15px 0;
}

button {
	border-radius: 20px;
	border: 1px solid rgb(205 121 31);
	background-color: rgb(205 121 31);
	color: #FFFFFF;
	font-size: 12px;
	font-weight: bold;
	padding: 12px 45px;
	letter-spacing: 1px;
	text-transform: uppercase;
	transition: transform 80ms ease-in;
}

.dropdown select option[value=""] {
	color: #aaa;
	/* Placeholder text color */
}

/* Style the dropdown container */
.dropdown {
	width: 100%;
	margin: 10px 0;
}


.dropdown label {
	display: block;
	margin-bottom: 5px;
	font-size: 14px;
	color: #555;
}


.dropdown select {
	width: 100%;
	padding: 10px;
	border: 1px solid #ccc;
	border-radius: 4px;
	background-color: #f3f3f3;
	/* Matches input field color */
	font-size: 16px;
	box-sizing: border-box;
	appearance: none;
	/* Removes default styling */
	-webkit-appearance: none;
	-moz-appearance: none;


	background-position: right 10px center;
	background-size: 10px;
}

/* Ensure dropdown container maintains alignment */
.dropdown {
	position: relative;
}


button:active {
	transform: scale(0.95);
}

button:focus {
	outline: none;
}

button.ghost {
	background-color: transparent;
	border-color: #FFFFFF;
}

form {
	background-color: #FFFFFF;
	display: flex;
	align-items: center;
	justify-content: center;
	flex-direction: column;
	padding: 8px 20px;
	height: 99%;
	text-align: center;

}

input {
	background-color: #eee;
	border: none;
	padding: 12px 15px;
	margin: 5px 0;
	width: 100%;
}

.container {
	background-color: #fff;
	border-radius: 10px;
	box-shadow: 0 14px 28px rgba(0, 0, 0, 0.25), 0 10px 10px rgba(0, 0, 0, 0.22);
	position: relative;
	margin-top: 60px;
	font-size: small;
	overflow: hidden;
	margin-left: 305px;
	width: 768px;
	max-width: 90%;
	min-height: 526px;

}

.form-container {
	position: absolute;
	top: 0;
	height: 100%;
	transition: all 0.6s ease-in-out;
}

.sign-in-container {
	left: 0;
	width: 50%;
	z-index: 2;
}

.container.right-panel-active .sign-in-container {
	transform: translateX(100%);
}

.sign-up-container {
	left: 0;
	width: 50%;
	opacity: 0;
	z-index: 1;
}

.container.right-panel-active .sign-up-container {
	transform: translateX(100%);
	opacity: 1;
	z-index: 5;
	animation: show 0.6s;
}

@keyframes show {

	0%,
	49.99% {
		opacity: 0;
		z-index: 1;
	}

	50%,
	100% {
		opacity: 1;
		z-index: 5;
	}
}

.overlay-container {
	position: absolute;
	top: 0;
	left: 50%;
	width: 50%;
	height: 100%;
	overflow: hidden;
	transition: transform 0.6s ease-in-out;
	z-index: 100;
}

.container.right-panel-active .overlay-container {
	transform: translateX(-100%);
}

.overlay {
	background: #1ebba1;
	background-repeat: no-repeat;
	background-size: cover;
	background-position: 0 0;
	color: #FFFFFF;
	position: relative;
	left: -100%;
	height: 100%;
	width: 200%;
	transform: translateX(0);
	transition: transform 0.6s ease-in-out;
}

.container.right-panel-active .overlay {
	transform: translateX(50%);
}

.overlay-panel {
	position: absolute;
	display: flex;
	align-items: center;
	justify-content: center;
	flex-direction: column;
	padding: 0 40px;
	text-align: center;
	top: 0;
	height: 100%;
	width: 50%;
	transform: translateX(0);
	transition: transform 0.6s ease-in-out;
}

.overlay-left {
	transform: translateX(-20%);
}

.container.right-panel-active .overlay-left {
	transform: translateX(0);
}

.overlay-right {
	right: 0;
	transform: translateX(0);
}

/* .dropdowm {} */

.container.right-panel-active .overlay-right {
	transform: translateX(20%);
}

.social-container {
	margin: 10px 0;
	/* Reduced margin for social container */
}

.social-container a {
	border: 1px solid #DDDDDD;
	border-radius: 50%;
	display: inline-flex;
	justify-content: center;
	align-items: center;
	margin: 0 5px;
	height: 35px;
	height: 40px;
	width: 40px;
}





/* Media Queries for Responsiveness */
@media (max-width: 768px) {
	.container {
		width: 100%;
	}

	.form-container {
		width: 100%;
		padding: 20px;
		/* Adjusted padding for smaller screen */
	}

	.sign-up-container,
	.sign-in-container {
		width: 100%;
	}

	.overlay-container {
		display: none;
	}

	.social-container a {
		font-size: 18px;
		width: 35px;
		height: 35px;
	}

	.social-container {
		text-align: center;
	}

	.form-container input,
	.form-container select {
		width: 100%;
		font-size: 14px;
	}

	button {
		width: 100%;
	}

	.overlay-panel {
		width: 100%;
	}

	.overlay-left,
	.overlay-right {
		transform: translateX(0);
		width: 100%;
	}
}

@media (max-width: 480px) {
	.social-container a {
		width: 30px;
		height: 30px;
		font-size: 16px;
	}
}
</style>
