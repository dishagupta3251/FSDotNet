package model

import (
	"gorm.io/gorm"
)

type Customer struct {
	gorm.Model
	Id            string `gorm:"unique"`
	FirstName     string `gorm:"not null"`
	LastName      string `gorm:"not null"`
	Email         string `gorm:"not null"`
	Phone         string `gorm:"not null"`
	AccountNumber string `gorm:"not null"`
	Address       string `gorm:"not null"`
}
