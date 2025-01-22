package services

import (
	"mymodule/model"

	"github.com/gin-gonic/gin"
	gorm "gorm.io/gorm"
)

type PassengerRepository struct {
	Db *gorm.DB
}

func (pr *PassengerRepository) CreatePassenger(ctx *gin.Context) {
	var passengerDto model.PassengerRequest
	if err := ctx.ShouldBindJSON(&passengerDto); err != nil {
		ctx.JSON(400, gin.H{"message": "Invalid input", "error": err.Error()})
		return
	}
	var passenger model.Passenger
	passenger.FirstName = passengerDto.FirstName
	passenger.LastName = passengerDto.LastName
	passenger.Title = passengerDto.Title
	passenger.BookingId = passengerDto.BookingId

	if err := pr.Db.Create(&passenger).Error; err != nil {
		ctx.JSON(500, gin.H{"message": "Error creating passenger", "error": err.Error()})
		return
	}
	ctx.JSON(200, gin.H{"message": "Passenger created successfully", "data": passenger})
}
