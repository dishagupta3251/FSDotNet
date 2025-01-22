package services

import (
	"github.com/gin-gonic/gin"
	gorm "gorm.io/gorm"
)

type SeatRepository struct {
	Db *gorm.DB
}

type Seat struct {
	FlightId   string `gorm:"not null;index"`
	FlightType string `gorm:"not null"`
	SeatNo     int32  `gorm:"not null"`
	BookingId  string `gorm:"not null;index"`
}

func (pr *SeatRepository) AddSeats(ctx *gin.Context) {
	var seat Seat
	if err := ctx.ShouldBindJSON(&seat); err != nil {
		ctx.JSON(400, gin.H{"message": "Invalid input", "error": err.Error()})
		return
	}
	if err := pr.Db.Create(&seat).Error; err != nil {
		ctx.JSON(500, gin.H{"message": "Error creating seats", "error": err.Error()})
		return
	}
	ctx.JSON(200, gin.H{"message": "Seats added successfully", "data": seat})
}

func (pr *SeatRepository) GetBookedSeats(ctx *gin.Context) {
	var seats []int
	flightId := ctx.Param("flightId")

	if err := pr.Db.Model(&Seat{}).Where("flight_id = ?", flightId).Pluck("seat_no", &seats).Error; err != nil {
		ctx.JSON(500, gin.H{"message": "Error getting seats", "error": err.Error()})
		return
	}

	ctx.JSON(200, gin.H{"data": seats})
}
