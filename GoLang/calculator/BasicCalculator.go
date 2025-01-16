package main

import "errors"

func AddNumbers(x, y int) int {
	return x + y
}
func DivideNumbers(x, y float64) (float64, error) {
	if y == 0 {
		return 0, errors.New("division by zero is undefined")
	}
	return x / y, nil
}
