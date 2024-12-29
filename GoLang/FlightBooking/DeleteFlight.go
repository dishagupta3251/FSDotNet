package main

import (
	"fmt"
	"mymodule/model"
	"net/http"

	"github.com/gin-gonic/gin"
	"gorm.io/gorm"
)

func DeleteFlight(ctx *gin.Context) {

	flightId := ctx.Param("id")
	userRole := ctx.GetString("userRole")
	if userRole != "admin" {
		logger.Error("User is not authorized to delete a flight")
		ctx.JSON(http.StatusUnauthorized, gin.H{"message": "User is not authorized to delete a flight"})
		return
	}

	// Get user email and role from the context
	userEmail := ctx.GetString("userEmail")
	if userEmail == "" {
		logger.Error("failed to get the token from the header")
		ctx.JSON(http.StatusInternalServerError, gin.H{"message": "failed to get the token from the header"})
		return
	}
	var user model.User
	err := flightDbConnector.Where("email = ?", userEmail).First(&user).Error

	if err != nil {
		if err == gorm.ErrRecordNotFound {
			ctx.JSON(http.StatusNotFound, gin.H{"message": "User not found"})
		} else {
			ctx.JSON(http.StatusInternalServerError, gin.H{"message": "Error fetching user"})
		}
		return
	}
	deleteErr := flightDbConnector.Unscoped().Delete(&model.Flights{}, "id=? AND user_id=?", flightId, user.ID)

	if deleteErr.RowsAffected == 0 {
		ctx.JSON(http.StatusNotFound, gin.H{"message": "Flight not found or you are not authorized to delete this flight"})
		return
	}

	if deleteErr.Error != nil {
		ctx.JSON(http.StatusInternalServerError, gin.H{"message": "Error deleting flight"})
		return
	}

	logger.Info(fmt.Sprintf("Flight %s deleted successfully", flightId))
	ctx.JSON(http.StatusOK, gin.H{"message": "Flight deleted successfully"})
}
