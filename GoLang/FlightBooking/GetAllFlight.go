package main

import (
	"mymodule/model"
	"net/http"

	"github.com/gin-gonic/gin"
)

func GetAllFlights(ctx *gin.Context) {
	userRole := ctx.GetString("userRole")
	if userRole != "admin" {
		logger.Error("User is not authorized to access all flights")
		ctx.JSON(http.StatusUnauthorized, gin.H{"message": "User is not authorized to view all flights"})
		return
	}

	// Fetching the user in user db

	email := ctx.GetString("userEmail")
	var user model.User
	err := flightDbConnector.Where("email = ?", email).First(&user).Error
	if err != nil {
		ctx.JSON(http.StatusInternalServerError, gin.H{"message": "Error retrieving user"})
		return
	}

	// Fetch all flights from the database
	var flights []model.Flights
	flightErr := flightDbConnector.Where("user_id = ?", user.ID).Find(&flights).Error

	// Handle potential errors while retrieving flights
	if flightErr != nil {
		ctx.JSON(http.StatusInternalServerError, gin.H{"message": "Error retrieving flights"})
		return
	}

	// If no flights are available, send a message indicating so
	if len(flights) == 0 {
		ctx.JSON(http.StatusOK, gin.H{"message": "No flights available"})
		return
	}

	var flightDTO []model.FlightDTO
	for _, flight := range flights {
		flightDTO = append(flightDTO, model.FlightDTO{Id: flight.Id, Source: flight.Source, Destination: flight.Destination, Type: flight.Type, Seats: flight.Seats, Fair: flight.Fair, Airline: flight.Airline, Date: flight.Date})
	}

	// If flights are found, send them in the response
	ctx.JSON(http.StatusOK, gin.H{"flights": flightDTO})
}
