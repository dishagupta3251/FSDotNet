package main

import (
	"mymodule/config"
	"mymodule/repository"

	"github.com/gin-contrib/cors"
	"github.com/gin-gonic/gin"
	"github.com/joho/godotenv"
	"go.uber.org/zap"
)

var logger *zap.Logger

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
	db := config.ConnectDB()
	httpServer := gin.Default()

	httpServer.Use(cors.New(cors.Config{
		AllowOrigins:     []string{"http://localhost:8081"},
		AllowMethods:     []string{"GET", "POST", "PUT", "DELETE"},
		AllowHeaders:     []string{"Origin", "Content-Type", "Authorization"},
		ExposeHeaders:    []string{"Content-Length"},
		AllowCredentials: true,
	}))

	Repository := repository.FlightRepository{Db: db}

	httpServer.GET("/get-source", Repository.GetSource)
	httpServer.POST("/getall-flights", Repository.GetFlights)
	httpServer.GET("/get-destination/:source", Repository.GetDestination)
	httpServer.Run(":8082")
}
