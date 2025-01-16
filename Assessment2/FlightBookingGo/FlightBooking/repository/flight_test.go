package repository

import (
	"bytes"
	"encoding/json"
	"net/http"
	"net/http/httptest"
	"testing"
	"time"

	"mymodule/model"

	"github.com/gin-gonic/gin"
	"github.com/stretchr/testify/assert"
	"gorm.io/driver/sqlite"
	"gorm.io/gorm"
)

func TestGetFlights(t *testing.T) {

	gin.SetMode(gin.TestMode)
	router := gin.Default()

	db, err := gorm.Open(sqlite.Open("file::memory:?cache=shared"), &gorm.Config{})
	if err != nil {
		t.Fatalf("Failed to connect to the database: %v", err)
	}

	db.AutoMigrate(&model.Flight{})

	flightRepository := FlightRepository{Db: db}

	router.POST("/getall-flights", flightRepository.GetFlights)

	initialFlight := model.Flight{
		Source:      "NYC",
		Destination: "LAX",
		Date:        time.Date(2023, time.October, 10, 0, 0, 0, 0, time.UTC),
	}
	returnFlight := model.Flight{
		Source:      "LAX",
		Destination: "NYC",
		Date:        time.Date(2023, time.October, 20, 0, 0, 0, 0, time.UTC),
	}
	db.Create(&initialFlight)
	db.Create(&returnFlight)

	requestPayload := map[string]string{
		"source":      "NYC",
		"destination": "LAX",
		"date":        "2023-10-10",
		"return_date": "2023-10-20",
	}
	requestBody, _ := json.Marshal(requestPayload)

	req, err := http.NewRequest(http.MethodPost, "/getall-flights", bytes.NewBuffer(requestBody))
	if err != nil {
		t.Fatalf("Failed to create a new HTTP request: %v", err)
	}
	req.Header.Set("Content-Type", "application/json")

	rr := httptest.NewRecorder()

	router.ServeHTTP(rr, req)

	assert.Equal(t, http.StatusOK, rr.Code)

	var response map[string][]model.Flight
	err = json.Unmarshal(rr.Body.Bytes(), &response)
	if err != nil {
		t.Fatalf("Failed to unmarshal response body: %v", err)
	}

	initialFlights, initialExists := response["initial_flights"]
	returnFlights, returnExists := response["return_flights"]
	assert.True(t, initialExists)
	assert.True(t, returnExists)
	assert.Len(t, initialFlights, 1)
	assert.Len(t, returnFlights, 1)
	assert.Equal(t, initialFlight.Source, initialFlights[0].Source)
	assert.Equal(t, initialFlight.Destination, initialFlights[0].Destination)
	assert.Equal(t, initialFlight.Date, initialFlights[0].Date)
	assert.Equal(t, returnFlight.Source, returnFlights[0].Source)
	assert.Equal(t, returnFlight.Destination, returnFlights[0].Destination)
	assert.Equal(t, returnFlight.Date, returnFlights[0].Date)
}
