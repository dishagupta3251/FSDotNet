package main

import (
	"fmt"
	"mymodule/model"
	"net/http"

	"github.com/gin-gonic/gin"
	"gorm.io/gorm"
)

func DeleteFlight(ctx *gin.Context) {

	flightId := ctx.Param("id")

	// Get user email and role from the context
	userEmail := ctx.GetString("userEmail")
	if userEmail == "" {
		logger.Error("failed to get the token from the header")
		ctx.JSON(http.StatusInternalServerError, gin.H{"message": "failed to get the token from the header"})
		return
	}

	userRole := ctx.GetString("userRole")
	if userRole != "admin" {
		logger.Error("User is not authorized to delete a flight")
		ctx.JSON(http.StatusUnauthorized, gin.H{"message": "User is not authorized to delete a flight"})
		return
	}

	// Fetch the flight from the database
	var flight model.Flights
	err := flightDbConnector.Where("Id = ?", flightId).First(&flight).Error

	if err != nil {
		if err == gorm.ErrRecordNotFound {
			ctx.JSON(http.StatusNotFound, gin.H{"message": "Flight not found"})
		} else {
			ctx.JSON(http.StatusInternalServerError, gin.H{"message": "Error deleting flight"})
		}
		return
	}

	// Proceed with deletion
	deleteErr := flightDbConnector.Delete(&flight).Error
	if deleteErr != nil {
		ctx.JSON(http.StatusInternalServerError, gin.H{"message": "Error deleting flight"})
		return
	}

	logger.Info(fmt.Sprintf("Flight %s deleted successfully", flight.ID))
	ctx.JSON(http.StatusOK, gin.H{"message": "Flight deleted successfully"})
}
