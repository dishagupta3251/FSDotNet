package model

import (
	"time"
)

type Flight struct {
	Id            string    `gorm:"primaryKey"`
	Source        string    `gorm:"not null" json:"source"`
	Destination   string    `gorm:"not null" json:"destination"`
	Seats         string    `gorm:"not null" json:"seats"`
	ArrivalTime   time.Time `json:"arrivalTime"`
	DepartureTime time.Time `json:"departureTime"`
	Airline       string    `gorm:"not null" json:"airline"`
	Fare          float64   `gorm:"not null" json:"fare"`
	Date          time.Time `gorm:"not null" json:"date"`
	Seat          []Seat    `gorm:"foreignKey:FlightId;references:Id"`
}
