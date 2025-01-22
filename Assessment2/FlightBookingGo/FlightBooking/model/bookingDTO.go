package model

type BookingRequest struct {
	Id                   string  `json:"id" gorm:"primaryKey;unique;not null"`
	TripType             string  `json:"tripType" gorm:"not null"`
	OneWayTripFlightId   string  `json:"oneWayTripFlightId" gorm:"not null"`
	RoundTripFlightId    string  `json:"roundTripFlightId"`
	OneWayTripFlightType string  `json:"oneWayTripFlightType" gorm:"not null"`
	RoundTripFlightType  string  `json:"roundTripFlightType"`
	TotalFare            float64 `json:"totalFare"`
}
