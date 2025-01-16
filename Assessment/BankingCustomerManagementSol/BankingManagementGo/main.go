package main

import (
	"mymodule/config"

	"github.com/gin-gonic/gin"
	"github.com/joho/godotenv"
	"go.uber.org/zap"
	"gorm.io/gorm"
)

var logger *zap.Logger
var customerDBConnector *gorm.DB

func init() {
	var err error
	logger, err = zap.NewDevelopment()

	if err != nil {
		panic(err)
	}
	defer logger.Sync()
}

func main() {

	if err := godotenv.Load(".env"); err != nil {
		panic("No .env file found")
	}
	customerDBConnector = config.ConnectDB()

	// configuration of the http server.
	httpServer := gin.Default()

	httpServer.POST("/add-customer", AddCustomer)

	httpServer.Run(":8082")
}
