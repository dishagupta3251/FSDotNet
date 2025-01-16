package model

import (
	"time"
)

type Flight struct {
	Id            string `gorm:"unique"`
	Source        string `gorm:"not null"`
	Destination   string `gorm:"not null"`
	Seats         string `gorm:"not null"`
	ArrivalTime   time.Time
	DepartureTime time.Time
	Airline       string `gorm:"not null"`
	Fare          float64
	Date          time.Time
}
