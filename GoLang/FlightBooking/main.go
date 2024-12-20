package main

import (
	"mymodule/BookingServices"
	"mymodule/config"
	"net/http"

	"github.com/gin-gonic/gin"
	"go.uber.org/zap"
	"gorm.io/gorm"
)

//	func main() {
//		fmt.Println("Welcome to the FlightBooking Service.")
//		input := -1
//		for input != 0 {
//			fmt.Println("\nChoose an option:")
//			fmt.Println("1. User")
//			fmt.Println("2. Admin")
//			fmt.Println("0. Exit")
//			fmt.Scan(&input)
//			switch input {
//			case 1:
//				BookingServices.UserService()
//			case 2:
//				BookingServices.AdminService()
//			case 0:
//				fmt.Println("Thank you for using our service.")
//				return
//			default:
//				fmt.Println("Invalid option.")
//			}
//		}
//	}
var logger *zap.Logger
var flightDbConnector *gorm.DB

func init() {
	var err error
	logger, err = zap.NewDevelopment()

	if err != nil {
		panic(err)
	}
	defer logger.Sync()
}

func main() {
	flightDbConnector = config.ConnectDB()

	// configuration of the http server.
	httpServer := gin.Default()
	//? Method : @POST
	httpServer.POST("/save-flight", AddFlight)

	httpServer.GET("/getall-flight", func(ctx *gin.Context) {
		var flights []BookingServices.Flight

		if err := flightDbConnector.Find(&flights).Error; err != nil {
			ctx.JSON(http.StatusInternalServerError, gin.H{"error": "Unable to retrieve flights"})
			return
		}
		ctx.JSON(http.StatusOK, gin.H{"flights": flights})
	})

	// running the server
	httpServer.Run(":8081")
}
