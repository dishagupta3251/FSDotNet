package model

type PassengerRequest struct {
	BookingId string `gorm:"not null"`
	Title     string `gorm:"not null"`
	FirstName string `gorm:"not null"`
	LastName  string `gorm:"not null"`
}
