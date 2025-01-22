package main

import (
	"mymodule/config"
	"mymodule/services"

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
		AllowOrigins:     []string{"http://localhost:8080"},
		AllowMethods:     []string{"GET", "POST", "PUT", "DELETE"},
		AllowHeaders:     []string{"Origin", "Content-Type", "Authorization"},
		ExposeHeaders:    []string{"Content-Length"},
		AllowCredentials: true,
	}))

	FlightServices := services.FlightRepository{Db: db}
	BookingServices := services.BookingRepository{Db: db}
	PassengerServices := services.PassengerRepository{Db: db}
	SeatServices := services.SeatRepository{Db: db}

	httpServer.GET("/get-source", FlightServices.GetSource)
	httpServer.POST("/getall-flights", FlightServices.GetFlights)
	httpServer.GET("/get-destination/:source", FlightServices.GetDestination)
	httpServer.POST("/create-booking", BookingServices.CreateBooking)
	httpServer.POST("/add-passenger", PassengerServices.CreatePassenger)
	httpServer.POST("/add-seats", SeatServices.AddSeats)
	httpServer.GET("/get-seats/:flightId", SeatServices.GetBookedSeats)
	httpServer.GET("/get-pdf/:bookingReference", BookingServices.GetPdf)
	httpServer.Run(":8082")
}
