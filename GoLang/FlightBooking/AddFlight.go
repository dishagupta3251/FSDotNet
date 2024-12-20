package main

import (
	"fmt"
	BookingServices "mymodule/BookingServices"
	"net/http"

	"github.com/gin-gonic/gin"
	"go.uber.org/zap"
	"gorm.io/gorm"
)

func AddFlight(ctx *gin.Context) {
	var flight BookingServices.Flight
	ctx.ShouldBindJSON(&flight)

	var existingFlight BookingServices.Flight

	flightNotFoundError := flightDbConnector.Where("id = ?", flight.Id).First(&existingFlight).Error

	if flightNotFoundError == gorm.ErrRecordNotFound {
		newFlight := &BookingServices.Flight{Id: flight.Id, CompanyName: flight.CompanyName, Price: flight.Price, Type: flight.Type, Source: flight.Source, Destination: flight.Destination, Seats: flight.Seats, Date: flight.Date}

		primaryKey := flightDbConnector.Create(newFlight)

		if primaryKey.Error != nil {
			logger.Error("Failed to Add Flight", zap.Int("flight id", flight.Id), zap.Error(primaryKey.Error))
			ctx.JSON(http.StatusConflict, gin.H{"message": "The Flight is already added"})
			return
		}
		logger.Info(fmt.Sprintf("Flight added successfully", flight.Id))
		ctx.JSON(http.StatusCreated, gin.H{"message": "Flight added successfully"})

	} else {
		logger.Warn("User Flight Already Exist", zap.Int("flight Id", flight.Id))
		ctx.JSON(http.StatusConflict, gin.H{"message": "Flight Already Exist"})
	}

}
