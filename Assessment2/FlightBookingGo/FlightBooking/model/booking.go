package model

import "time"

type Booking struct {
	Id                   string      `gorm:"primaryKey;unique;not null"`
	TripType             string      `gorm:"not null"`
	OneWayTripFlightId   string      `gorm:"size:255;null"`
	OneWayTripFlight     Flight      `gorm:"foreignKey:OneWayTripFlightId;references:Id"`
	RoundTripFlightId    string      `gorm:"size:255;default:NULL"`
	RoundTripFlight      Flight      `gorm:"foreignKey:RoundTripFlightId;references:Id"`
	OneWayTripFlightType string      `gorm:"not null"`
	RoundTripFlightType  string      `gorm:"null"`
	TotalFare            float64     `gorm:"not null"`
	DateOfBooking        time.Time   `gorm:"not null"`
	Passenger            []Passenger `gorm:"foreignKey:BookingId;references:Id"`
	Seat                 []Seat      `gorm:"foreignKey:BookingId;references:Id"`
}
