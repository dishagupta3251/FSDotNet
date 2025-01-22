package services

import (
	"bytes"
	"fmt"
	"log"
	"mymodule/model"
	"net/http"
	"time"

	"github.com/andrewcharlton/wkhtmltopdf-go"
	"github.com/gin-gonic/gin"
	gorm "gorm.io/gorm"
)

type BookingRepository struct {
	Db *gorm.DB
}

func (br *BookingRepository) CreateBooking(ctx *gin.Context) {
	var bookingDto model.BookingRequest
	if err := ctx.ShouldBindJSON(&bookingDto); err != nil {
		ctx.JSON(400, gin.H{"message": "Invalid input", "error": err.Error()})
		return
	}
	var booking model.Booking
	booking.Id = bookingDto.Id
	booking.OneWayTripFlightType = bookingDto.OneWayTripFlightType
	booking.RoundTripFlightType = bookingDto.RoundTripFlightType
	booking.TotalFare = bookingDto.TotalFare
	booking.TripType = bookingDto.TripType
	booking.OneWayTripFlightId = bookingDto.OneWayTripFlightId
	booking.RoundTripFlightId = bookingDto.RoundTripFlightId

	booking.DateOfBooking = time.Now()
	if err := br.Db.Create(&booking).Error; err != nil {
		ctx.JSON(500, gin.H{"message": "Error creating booking", "error": err.Error()})
		return
	}
	ctx.JSON(200, gin.H{"message": "Booking created successfully", "data": booking})

}

func (br *BookingRepository) GetPdf(ctx *gin.Context) {
	bookingReference := ctx.Param("bookingReference")
	if bookingReference == "" {
		ctx.JSON(http.StatusBadRequest, gin.H{"error": "Booking reference is required"})
		return
	}

	// Retrieve booking details along with passengers, flights, and seats
	var booking model.Booking
	if err := br.Db.Where("id = ?", bookingReference).First(&booking).Error; err != nil {
		log.Println("Error fetching booking:", err)
		ctx.JSON(http.StatusNotFound, gin.H{"error": "Booking not found"})
		return
	}

	var flight model.Flight
	if err := br.Db.Where("id = ?", booking.OneWayTripFlightId).First(&flight).Error; err != nil {
		log.Println("Error fetching booking:", err)
		ctx.JSON(http.StatusNotFound, gin.H{"error": "Booking not found"})
		return
	}

	var passenger []model.Passenger
	if err := br.Db.Where("id = ?", bookingReference).Find(&passenger).Error; err != nil {
		log.Println("Error fetching booking:", err)
		ctx.JSON(http.StatusNotFound, gin.H{"error": "Booking not found"})
		return
	}

	var seats []model.Seat
	if err := br.Db.Where("booking_id =?", bookingReference).Find(&seats).Error; err != nil {
		log.Println("Error fetching seats:", err)
		ctx.JSON(http.StatusInternalServerError, gin.H{"error": "Error fetching seats"})
		return
	}

	// Generate the HTML content for the PDF
	htmlContent := generateHtml(booking, passenger, seats, flight)

	// Convert HTML to PDF using wkhtmltopdf
	doc := wkhtmltopdf.NewDocument()
	buf := bytes.NewBufferString(htmlContent)
	pg, err := wkhtmltopdf.NewPageReader(buf)
	if err != nil {
		log.Println("Error creating PDF page reader:", err)
		ctx.JSON(http.StatusInternalServerError, gin.H{"error": "Error generating PDF"})
		return
	}
	doc.AddPages(pg)

	// Set headers to serve the PDF file as an attachment
	ctx.Header("Content-Type", "application/pdf")
	ctx.Header("Content-Disposition", `attachment; filename="booking_report.pdf"`)

	// Write the PDF to the response
	if err := doc.Write(ctx.Writer); err != nil {
		log.Println("Error generating PDF:", err)
		ctx.JSON(http.StatusInternalServerError, gin.H{"error": "Error generating PDF"})
		return
	}
}

func generateHtml(booking model.Booking, passenger []model.Passenger, seats []model.Seat, flight model.Flight) string {
	// Define the base HTML structure for the booking report
	basehtml := `
<!DOCTYPE html>
<html>
<head>
    <meta charset="UTF-8">
    <title>Flight Booking Report</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            background-color: #f9fafb;
            margin: 0;
            padding: 20px;
        }
        table {
            border-collapse: collapse;
            width: 100%;
        }
        .main-container {
            width: 800px;
            margin: 0 auto;
        }
        .report-header {
            background: white;
            padding: 24px;
            border-radius: 12px;
            margin-bottom: 24px;
            box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
        }
        .section-title {
            color: #111827;
            font-size: 24px;
            margin: 0 0 24px 0;
        }
        .info-cell {
            background: #f9fafb;
            padding: 16px;
            border-radius: 8px;
        }
        .info-label {
            color: #6b7280;
            font-size: 14px;
        }
        .info-value {
            font-size: 16px;
            font-weight: bold;
            color: #111827;
        }
        .divider {
            border-top: 1px solid #e5e7eb;
            margin: 24px 0;
        }
    </style>
</head>
<body>
    <div class="main-container">
        <div class="report-header">
            <h1 class="section-title">Flight Booking Report</h1>
            <table>
                <tr>
                    <td class="info-cell">
                        <div class="info-label">Booking Reference Number</div>
                        <div class="info-value">%v</div>
                    </td>
                    <td class="info-cell">
                        <div class="info-label">Flight ID</div>
                        <div class="info-value">%v</div>
                    </td>
                </tr>
            </table>
        </div>
        <div class="divider"></div>
        <h2 class="section-title">Flight Details</h2>
		  <table>
						<tr>
							<td width="50%%" class="info-cell">
								<div class="info-label">From</div>
								<div class="info-value">%s</div>
								<div class="info-label">Departure Time</div>
								<div class="info-value">%s</div>
							</td>
							<td width="50%%" class="info-cell" style="padding-left: 16px;">
								<div class="info-label">To</div>
								<div class="info-value">%s</div>
								<div class="info-label">Arrival Time</div>
								<div class="info-value">%s</div>
							</td>
							<td width="50%%" class="info-cell" style="padding-left: 16px;">
								<div class="info-label">Price</div>
								<div class="info-value">Rs %f</div>
							</td>
						</tr>
					</table>
        <div class="divider"></div>
        <h2 class="section-title">Passenger Details</h2>
        <table>`
	html := fmt.Sprintf(basehtml,
		booking.Id,                       // Booking Reference
		flight.Id,                        // Flight ID
		flight.Source,                    // From (Source)
		formatTime(flight.DepartureTime), // Departure Time
		flight.Destination,               // To (Destination)
		formatTime(flight.ArrivalTime),   // Arrival Time
		booking.TotalFare,                // Price
	)

	// Add passenger details dynamically
	for i, p := range passenger {

		seat := seats[i]

		passengerHtml := fmt.Sprintf(`
			<table>
				<tr>
					<td class="info-cell" style="margin-bottom: 12px;">
						<table width="">
							<tr>
								<td width="33%%">
									<div class="info-label">Title</div>
									<div class="info-value">%s</div>
								</td>
								<td width="33%%">
									<div class="info-label">Passenger Name</div>
									<div class="info-value">%s %s</div>
								</td>
								<td width="33%%">
									<div class="info-label">Seat Number</div>
									<div class="info-value">%v</div>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr class="spacer-row"><td></td></tr>`,
			p.Title,
			p.FirstName,
			p.LastName,
			seat.SeatNo,
		)
		html += passengerHtml
	}

	// Close HTML tags
	html += `
        </table>
    </div>
</body>
</html>`
	return html
}

// Helper to format time
func formatTime(timestamp time.Time) string {
	return timestamp.Format("2006-01-02 03:04 PM")
}
