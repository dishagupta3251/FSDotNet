package main

import (
	"fmt"
	"mymodule/model"
	"net/http"

	"github.com/gin-gonic/gin"
	"gorm.io/gorm"
)

func UpdateFlight(ctx *gin.Context) {

	flightId := ctx.Param("id")
	userRole := ctx.GetString("userRole")
	if userRole != "admin" {
		logger.Error("User is not authorized to update any flight")
		ctx.JSON(http.StatusUnauthorized, gin.H{"message": "User is not authorized to update any flight"})
		return
	}

	// Fetching the user in user db

	email := ctx.GetString("userEmail")
	var user model.User
	err := flightDbConnector.Where("email = ?", email).First(&user).Error

	if err != nil {
		if err == gorm.ErrRecordNotFound {
			ctx.JSON(http.StatusNotFound, gin.H{"message": "User not found"})
		} else {
			ctx.JSON(http.StatusInternalServerError, gin.H{"message": "Error fetching user"})
		}
		return
	}
	var flight model.Flights
	flightErr := flightDbConnector.Where("id=? AND user_id=?", flightId, user.ID).First(&flight).Error

	if flightErr != nil {
		ctx.JSON(http.StatusNotFound, gin.H{"message": "Flight not found or you are not authorized to delete this flight"})
		return
	}
	if err := ctx.ShouldBindJSON(&flight); err != nil {
		ctx.JSON(http.StatusBadRequest, gin.H{"error": "Invalid input"})
		return
	}
	err = flightDbConnector.Save(&flight).Error
	if err != nil {
		ctx.JSON(http.StatusInternalServerError, gin.H{"message": "Error updating flight"})
		return
	}
	logger.Info(fmt.Sprintf("Flight %s updated successfully", flight))
	ctx.JSON(http.StatusOK, gin.H{"message": "Flight deleted successfully", "flight": flight})
}
