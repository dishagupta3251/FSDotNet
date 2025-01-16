package config

import (
	"fmt"
	"mymodule/model"
	"os"

	"gorm.io/driver/mysql"
	"gorm.io/gorm"
)

func DatabaseDsn() string {
	// return fmt.Sprintf("root:mysql@tcp(127.0.0.1:3306)/flights?charset=utf8&parseTime=True&loc=Local")
	return fmt.Sprintf("%s:%s@tcp(%s:%s)/%s?charset=utf8&parseTime=True&loc=Local",
		os.Getenv("MYSQL_USER"),
		os.Getenv("MYSQL_PASSWORD"),
		os.Getenv("MYSQL_HOST"),
		os.Getenv("MYSQL_PORT"),
		os.Getenv("MYSQL_DATABASE"),
	)
}

func ConnectDB() *gorm.DB {
	flightdb, err := gorm.Open(mysql.Open(DatabaseDsn()), &gorm.Config{})

	if err != nil {
		panic("Failed to connect DB")
	}
	flightdb.AutoMigrate(&model.Flight{}, &model.Route{})
	return flightdb
}
