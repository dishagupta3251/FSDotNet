package BookingServices

import (
	"fmt"
	"time"
)

// Function to add flight information
func addFlight() {
	var source, destination, date, companyName, class, seats string
	var price float32

	currentTime := time.Now()

	fmt.Print("Enter source city: ")
	fmt.Scan(&source)

	fmt.Print("Enter destination city: ")
	fmt.Scan(&destination)

	for {
		fmt.Print("Enter date of flight (YYYY-MM-DD): ")
		fmt.Scan(&date)

		flightDate, err := time.Parse("2006-01-02", date)
		if err != nil {
			fmt.Println("Invalid date format. Please enter the date in YYYY-MM-DD format.")
			continue
		}

		if flightDate.Before(currentTime) {
			fmt.Println("The flight date cannot be in the past. Please enter a valid date.")
		} else {

			break
		}
	}

	fmt.Print("Enter the airline/company name: ")
	fmt.Scan(&companyName)

	fmt.Print("Enter flight class (e.g., Economy, Business): ")
	fmt.Scan(&class)

	fmt.Print("Enter price of the flight: ")
	fmt.Scan(&price)

	fmt.Print("Enter available seats: ")
	fmt.Scan(&seats)

	// Add the flight to the Flights slice
	Flights = append(Flights, Flight{
		Id:          len(Flights) + 1,
		Source:      source,
		Destination: destination,
		Date:        date,
		CompanyName: companyName,
		Type:        class,
		Price:       price,
		Seats:       seats,
	})

	fmt.Println("Flight added successfully!")
	fmt.Println("===========================================================================================================")
}

// Function to delete a flight
func deleteFlight() {
	fmt.Print("Enter the id of the flight to delete: ")
	var id, indexToDelete int
	fmt.Scan(&id)

	if id > 0 && id <= len(Flights) {
		fmt.Println("Are you sure you want to delete?")
		fmt.Println("1-Yes")
		fmt.Println("2-No")
		var confirm int
		fmt.Scan(&confirm)

		if confirm == 1 {
			for index, flight := range Flights {
				if flight.Id == id {
					indexToDelete = index
				}
			}
			Flights = append(Flights[:indexToDelete], Flights[indexToDelete+1:]...)
			fmt.Printf("Flight deleted successfully!\n")
		} else {
			fmt.Println("Operation canceled.")
		}
	} else {
		fmt.Println("Invalid id")
		AdminService()
	}
	fmt.Println("===========================================================================================================")
}

// Function to view all flights
func viewFlights() {
	if len(Flights) == 0 {
		fmt.Println("No flights available.")
		return
	}

	for _, flight := range Flights {
		fmt.Printf("Id: %d | %s -> %s | Date: %s | Company: %s | Class: %s | Price: %.2f | Seats: %s\n",
			flight.Id, flight.Source, flight.Destination, flight.Date, flight.CompanyName, flight.Type, flight.Price, flight.Seats)
	}
	fmt.Println("=======================================================================================================")
}

// Admin service function to manage flights
func AdminService() {
	fmt.Print()
	fmt.Println("Welcome Admin")
	input := -1
	for input != 0 {
		fmt.Println("\nChoose an option:")
		fmt.Println("1. Add flight information")
		fmt.Println("2. Delete a flight")
		fmt.Println("3. View All flights")
		fmt.Println("0. Exit")
		fmt.Scan(&input)

		switch input {
		case 1:
			addFlight()
		case 2:
			deleteFlight()
		case 3:
			viewFlights()
		case 0:
			return
		default:
			fmt.Println("Invalid option!")
		}
	}
}
