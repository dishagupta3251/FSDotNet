package config

import (
	"fmt"
	BookingServices "mymodule/BookingServices"

	"gorm.io/driver/mysql"
	"gorm.io/gorm"
)

func DatabaseDsn() string {
	return fmt.Sprintf("root:mysql@tcp(127.0.0.1:3306)/flights?charset=utf8&parseTime=True&loc=Local")
}

func ConnectDB() *gorm.DB {
	userdb, err := gorm.Open(mysql.Open(DatabaseDsn()), &gorm.Config{})

	if err != nil {
		panic("Failed to connect DB")
	}
	userdb.AutoMigrate(&BookingServices.Flight{})
	return userdb
}
