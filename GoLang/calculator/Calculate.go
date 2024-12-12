package calculator

import (
	"fmt"
)

func Calculator() {

	var Periods float64
	var startingAmount float64
	var interest float64
	var Deposit float64

	fmt.Println("Enter Number of Periods : - ")
	fmt.Scanln(&Periods)

	fmt.Println("Starting Amount : - ")
	fmt.Scanln(&startingAmount)

	fmt.Println("Interest Rate : - ")
	fmt.Scanln(&interest)

	fmt.Println("Periodic Deposit : - ")
	fmt.Scanln(&Deposit)

	var endbalance float64 = 0

	var endResult float64 = 0
	fmt.Println("Slno.\tStartBalance \tInterest\tDeposit\tEndBalance")
	for i := 0; i < int(Periods); i++ {
		if endbalance == 0 {
			endResult = startingAmount
		} else {
			endResult = endbalance
		}
		interestRate := endResult * (interest / 100)
		endbalance = endResult + Deposit + interestRate
		fmt.Println(i+1, "\t", (endResult*100)/100, "\t", (interestRate*100)/100, "\t", (Deposit*100)/100, "\t", (endbalance*100)/100)
	}

}
