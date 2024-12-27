package main

import (
	"mymodule/config"
	"mymodule/jwt"
	"time"

	"github.com/gin-gonic/gin"
	"github.com/joho/godotenv"
	"go.uber.org/zap"
	"gorm.io/gorm"
)

var logger *zap.Logger
var flightDbConnector *gorm.DB
var jwtManager *jwt.JWTManager

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
	flightDbConnector = config.ConnectDB()

	// * Create a new jwt manager
	jwtManager = jwt.NewJWTManager("SECRET_KEY", 5*time.Hour)

	// configuration of the http server.
	httpServer := gin.Default()

	// ? Unprotected Routes
	httpServer.POST("/save-user", AddUser)
	httpServer.POST("/login-user", Login)

	// ? Protected Routes
	httpServer.Use(jwt.AuthorizeJwtToken())
	httpServer.POST("/add-flight", AddFlight)
	httpServer.GET("/getall-flight", GetAllFlights)
	//httpServer.GET("/search-flight", SearchFlight)
	httpServer.DELETE("/delete-flight/:id", DeleteFlight)

	// running the server
	httpServer.Run(":8081")
}
