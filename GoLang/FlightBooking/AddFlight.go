package main

import (
	"fmt"
	"mymodule/model"
	"net/http"
	"strconv"

	"github.com/gin-gonic/gin"
	"go.uber.org/zap"
	"gorm.io/gorm"
)

func AddFlight(ctx *gin.Context) {
	var flight model.Flights
	ctx.ShouldBindJSON(&flight)

	// * 1 Get User Email from Claims

	userEmail := ctx.GetString("userEmail")

	if userEmail == "" {
		logger.Error("failed to get the token from the header")
		ctx.JSON(http.StatusInternalServerError, gin.H{"message": "failed to get the token from the header"})
		return
	}

	// * 2. Check user role
	userRole := ctx.GetString("userRole")
	if userRole != "admin" {
		logger.Error("User is not authorized to add a flight")
		ctx.JSON(http.StatusUnauthorized, gin.H{"message": "User is not authorized to add a flight"})
		return
	}

	// * 3. Check for Existing Flight.
	// var existingFlight flightstructs.FlightStruct
	var existingFlight model.Flights

	flightNotFoundError := flightDbConnector.Where("Id= ?", flight.Id).First(&existingFlight).Error

	if flightNotFoundError == gorm.ErrRecordNotFound {

		//  Get the user from the database
		var user model.User
		flightDbConnector.Where("email = ?", userEmail).First(&user)

		//  adding the user id inside the flight
		flight.UserId = strconv.FormatUint(uint64(user.ID), 10)

		primaryKey := flightDbConnector.Create(&flight)
		if primaryKey.Error != nil {
			logger.Error("Failed to Add Flight", zap.String("flight id ", flight.Id), zap.Error(primaryKey.Error))
			ctx.JSON(http.StatusConflict, gin.H{"message": "The Flight is already added"})
			return
		}

		logger.Info(fmt.Sprintf("flight %s created successfully", flight.Id))
		ctx.JSON(http.StatusCreated, gin.H{"message": "Flight added successfully"})

	} else {
		logger.Warn("User Flight Already Exist", zap.String("flight number", flight.Id))
		ctx.JSON(http.StatusConflict, gin.H{"message": "Flight Already Exist"})
	}

}
