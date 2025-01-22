package model

type Seat struct {
	Id         uint   `gorm:"primaryKey"`
	FlightId   string `gorm:"not null;index"`
	FlightType string `gorm:"not null"`
	SeatNo     int32  `gorm:"not null"`
	BookingId  string `gorm:"not null;index"`
}
