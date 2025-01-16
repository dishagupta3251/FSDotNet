package model

type Route struct {
	Id          string `gorm:"unique"`
	Source      string `gorm:"not null"`
	Destination string `gorm:"not null"`
}
