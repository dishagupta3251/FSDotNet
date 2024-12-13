package main

import (
	"Problems/FlightBooking"
	"fmt"
)

func main() {

	fmt.Println("Welcome to the FlightBooking Service.")
	input := -1
	for input != 0 {
		fmt.Println("\nChoose an option:")
		fmt.Println("1. User")
		fmt.Println("2. Admin")
		fmt.Println("0. Exit")
		fmt.Scan(&input)
		switch input {
		case 1:
			FlightBooking.UserService()
		case 2:
			FlightBooking.AdminService()
		case 0:
			fmt.Println("Thank you for using our service.")
			return
		default:
			fmt.Println("Invalid option.")
		}
	}

}
