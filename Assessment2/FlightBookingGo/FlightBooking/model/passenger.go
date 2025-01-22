package model

type Passenger struct {
	Id        uint   `gorm:"primaryKey"`
	BookingId string `gorm:"not null;index"`
	Title     string `gorm:"not null"`
	FirstName string `gorm:"not null"`
	LastName  string `gorm:"not null"`
}
