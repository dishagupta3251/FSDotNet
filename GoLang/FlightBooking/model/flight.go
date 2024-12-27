package model

import (
	"time"

	"gorm.io/gorm"
)

type Flights struct {
	gorm.Model
	Source      string `gorm:"not null"`
	Destination string `gorm:"not null"`
	Seats       string `gorm:"not null"`
	Type        string `gorm:"not null"`
	Airline     string `gorm:"not null"`
	Fair        float64
	Date        time.Time
	UserId      string `gorm:"type:varchar(255);uniqueIndex:user_flight_unique"`
}
