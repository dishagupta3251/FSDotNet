package BookingServices

import (
	"fmt"
	"sort"
)

func Print(Flights []Flight) {
	if len(Flights) == 0 {
		fmt.Println("No flights available.")
		return
	}

	for _, flight := range Flights {
		fmt.Printf("%s -> %s | Date: %s | Company: %s | Class: %s | Price: %.2f | Seats: %s\n",
			flight.Source, flight.Destination, flight.Date, flight.CompanyName, flight.Type, flight.Price, flight.Seats)
	}
	fmt.Println("=======================================================================================================")
}

// sorting the flights on the basis of Airline
func filterAirlines(AllFlights []Flight) {
	var input []Flight
	var airline string
	fmt.Println("Enter Airline Name: ")
	fmt.Scan(&airline)
	for _, flight := range AllFlights {
		if flight.CompanyName == airline {
			input = append(input, flight)
		}
	}
	Print(input)
}

// sorting the flight on the basis of Price
func sortPrice(input []Flight) {
	sort.Slice(input, func(i, j int) bool {
		return input[i].Price < input[j].Price
	})
	Print(input)
}

// searching for the flights based on Source, Destination and Date
func searchResult() {
	var source, destination, date string
	fmt.Print("Source: ")
	fmt.Scan(&source)

	fmt.Print("Destination: ")
	fmt.Scan(&destination)

	fmt.Print("Date (YYYY-MM-DD): ")
	fmt.Scan(&date)

	var result []Flight
	for _, flight := range Flights {
		if flight.Source == source && flight.Destination == destination && flight.Date == date {
			result = append(result, flight)
		}
	}
	Print(result)
	fmt.Println("Want to apply filters")
	fmt.Println("Press 1 or else Press 0")
	var filterChoice int
	fmt.Scan(&filterChoice)
	if filterChoice == 0 {
		return
	}
	input := -1
	for input != 0 {
		fmt.Println()
		fmt.Println("\nChoose an option:")
		fmt.Println("1. Price")
		fmt.Println("2. Airlines")
		fmt.Println("0. Exit")
		fmt.Scan(&input)
		switch input {
		case 1:
			sortPrice(result)
		case 2:
			filterAirlines(result)
		case 0:
			return
		default:
			fmt.Println("Invalid option.")
		}
	}

}
func UserService() {
	fmt.Println("")
	fmt.Println("Book your Flights.")
	fmt.Println("Enter the following details:")
	searchResult()
}
