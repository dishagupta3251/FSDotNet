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

	// Fetch all flights from the database
	var flights []model.Flights
	err := flightDbConnector.Find(&flights).Error

	// Handle potential errors while retrieving flights
	if err != nil {
		ctx.JSON(http.StatusInternalServerError, gin.H{"message": "Error retrieving flights"})
		return
	}

	// If no flights are available, send a message indicating so
	if len(flights) == 0 {
		ctx.JSON(http.StatusOK, gin.H{"message": "No flights available"})
		return
	}

	// If flights are found, send them in the response
	ctx.JSON(http.StatusOK, gin.H{"flights": flights})
}
