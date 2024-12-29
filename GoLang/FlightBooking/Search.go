package main

import (
	"fmt"
	"mymodule/model"
	"net/http"
	"time"

	"github.com/gin-gonic/gin"
)

type Search struct {
	From string `json:"from"`
	To   string `json:"to"`
	Date string `json:"date"` // Change to string to match JSON input
}

func SearchFlight(ctx *gin.Context) {
	var search Search
	if err := ctx.ShouldBindJSON(&search); err != nil {
		ctx.JSON(http.StatusBadRequest, gin.H{"message": "Invalid input"})
		return
	}

	// Parse the Date field from string to time.Time
	layout := "2006-01-02T15:04:05Z"
	parsedDate, err := time.Parse(layout, search.Date)
	if err != nil {
		ctx.JSON(http.StatusBadRequest, gin.H{"message": "Invalid date format"})
		return
	}

	formattedDate := parsedDate.Format("2006-01-02 15:04:05") // Format to match the database

	fmt.Println("From:", search.From)
	fmt.Println("To:", search.To)
	fmt.Println("Date:", formattedDate)

	var flights []model.Flights
	flightErr := flightDbConnector.Where("source = ? AND destination = ? AND date = ?", search.From, search.To, formattedDate).Find(&flights).Error
	if flightErr != nil {
		ctx.JSON(http.StatusInternalServerError, gin.H{"message": "Error retrieving flights"})
		return
	}

	if len(flights) == 0 {
		ctx.JSON(http.StatusOK, gin.H{"message": "No flights available"})
		return
	}

	var flightDTO []model.FlightDTO
	for _, flight := range flights {
		flightDTO = append(flightDTO, model.FlightDTO{
			Id:          flight.Id,
			Source:      flight.Source,
			Destination: flight.Destination,
			Type:        flight.Type,
			Seats:       flight.Seats,
			Fair:        flight.Fair,
			Airline:     flight.Airline,
			Date:        flight.Date,
		})
	}

	ctx.JSON(http.StatusOK, gin.H{"flights": flightDTO})
}
