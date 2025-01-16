package main

import (
	"fmt"
	model "mymodule/models"
	"net/http"

	"github.com/gin-gonic/gin"
)

func AddCustomer(ctx *gin.Context) {
	var customer model.Customer
	ctx.ShouldBindJSON(&customer)

	primaryKey := customerDBConnector.Create(&customer)
	if primaryKey.Error != nil {
		logger.Error("Failed to Add Customer")
		ctx.JSON(http.StatusConflict, gin.H{"message": "Cannot add customer"})
		return
	}

	logger.Info(fmt.Sprintf("customer %s created successfully", &customer.Id))
	ctx.JSON(http.StatusCreated, gin.H{"message": "Customer added successfully"})

}
