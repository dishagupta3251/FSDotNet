package BookingServices

type Search struct {
	From string
	To   string
	Date string
}

type Flight struct {
	Id          int
	CompanyName string
	Price       float32
	Type        string
	Source      string
	Destination string
	Seats       string
	Date        string
}

var Flights []Flight
var AvailableRoutes []Search

// func init() {
// 	Flights = append(Flights, Flight{
// 		Id:          1,
// 		CompanyName: "Indigo",
// 		Price:       500.75,
// 		Type:        "Business",
// 		Source:      "Lucknow",
// 		Destination: "Noida",
// 		Seats:       "120",
// 		Date:        "2024-12-25",
// 	})
// 	Flights = append(Flights, Flight{
// 		Id:          2,
// 		CompanyName: "Indigo",
// 		Price:       500.75,
// 		Type:        "Business",
// 		Source:      "Lucknow",
// 		Destination: "Banglore",
// 		Seats:       "120",
// 		Date:        "2024-12-25",
// 	})
// 	Flights = append(Flights, Flight{
// 		Id:          4,
// 		CompanyName: "Indigo",
// 		Price:       350.50,
// 		Type:        "Business",
// 		Source:      "Noida",
// 		Destination: "Lucknow",
// 		Seats:       "120",
// 		Date:        "2024-12-26",
// 	})
// 	Flights = append(Flights, Flight{
// 		Id:          3,
// 		CompanyName: "AirIndia",
// 		Price:       300.50,
// 		Type:        "Economy",
// 		Source:      "Noida",
// 		Destination: "Lucknow",
// 		Seats:       "150",
// 		Date:        "2024-12-26",
// 	})

// 	Flights = append(Flights, Flight{
// 		Id:          5,
// 		CompanyName: "AirIndia",
// 		Price:       400.50,
// 		Type:        "Economy",
// 		Source:      "Lucknow",
// 		Destination: "Noida",
// 		Seats:       "150",
// 		Date:        "2024-12-26",
// 	})

// 	for _, flight := range Flights {
// 		AvailableRoutes = append(AvailableRoutes, Search{
// 			From: flight.Source,
// 			To:   flight.Destination,
// 			Date: flight.Date,
// 		})
// 	}
// }
