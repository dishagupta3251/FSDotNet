package main

import (
	"Problems/SortingAlgo"
	"fmt"
)

//"Problems/Bank"
//calculator "Problems/Calculator"

func main() {

	//Bank.Service()
	//calculator.Calculator()
	arr := []int{64, 34, 29, 4, 22, 11, 90}
	fmt.Println("Original array:", arr)
	fmt.Println()
	fmt.Println("Sorted array with Bubble Sort:", SortingAlgo.BubbleSort(arr))
	fmt.Println("Sorted array with Insertion Sort:", SortingAlgo.InsertionSort(arr))
	fmt.Println("Sorted array with Selection Sort:", SortingAlgo.SelectionSort(arr))

}
