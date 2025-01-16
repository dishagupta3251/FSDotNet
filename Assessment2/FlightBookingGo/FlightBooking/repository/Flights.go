package repository

import (
	"fmt"
	"mymodule/model"

	"github.com/gin-gonic/gin"
	"gorm.io/gorm"
)

type FlightRepository struct {
	Db *gorm.DB
}

var request struct {
	Source      string `json:"source"`
	Destination string `json:"destination"`
	Date        string `json:"date"`
	ReturnDate  string `json:"return_date"`
}

func (fs *FlightRepository) GetFlights(ctx *gin.Context) {
	if err := ctx.ShouldBindJSON(&request); err != nil {
		ctx.JSON(400, gin.H{"message": "Invalid input", "error": err.Error()})
		return
	}

	fmt.Printf("Source: %s, Destination: %s\n", request.Date, request.ReturnDate)

	var returnFlights []model.Flight
	var initialFlights []model.Flight

	initialFlights, err := fs.findFlights(request.Source, request.Destination, request.Date)
	if err != nil {
		ctx.JSON(500, gin.H{"message": "Error retrieving initial flights", "error": err.Error()})
		return
	}

	if request.ReturnDate != "" {
		var err error
		returnFlights, err = fs.findFlights(request.Destination, request.Source, request.ReturnDate)
		if err != nil {
			ctx.JSON(500, gin.H{"message": "Error retrieving return flights", "error": err.Error()})
			return
		}
	} else {
		returnFlights = []model.Flight{}
	}

	ctx.JSON(200, gin.H{
		"initial_flights": initialFlights,
		"return_flights":  returnFlights,
	})
}

func (fs *FlightRepository) findFlights(source, destination, date string) ([]model.Flight, error) {
	var flights []model.Flight
	err := fs.Db.Where("source = ? AND destination = ? AND date = ?", source, destination, date).Find(&flights).Error
	return flights, err
}

func (fs *FlightRepository) GetSource(ctx *gin.Context) {
	var routes []model.Route
	error := fs.Db.Find(&routes).Error
	if error != nil {
		ctx.JSON(500, gin.H{"message": "Error retrieving routes"})
		return
	}

	var sources []string
	for _, route := range routes {
		sources = append(sources, route.Source)
	}

	ctx.JSON(200, sources)
}

func (fs *FlightRepository) GetDestination(ctx *gin.Context) {
	var routes []model.Route

	source := ctx.Param("source")

	error := fs.Db.Where("source = ?", source).Find(&routes).Error
	if error != nil {
		ctx.JSON(500, gin.H{"message": "Error retrieving routes"})
		return
	}

	var destinations []string
	for _, route := range routes {
		destinations = append(destinations, route.Destination)
	}

	ctx.JSON(200, destinations)
}
