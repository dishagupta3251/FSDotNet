package SortingAlgo

func InsertionSort(array []int) []int {
	for unsortedIndex := 1; unsortedIndex < len(array); unsortedIndex++ {
		currentValue := array[unsortedIndex]
		sortedIndex := unsortedIndex - 1
		for sortedIndex >= 0 && array[sortedIndex] > currentValue {
			array[sortedIndex+1] = array[sortedIndex]
			sortedIndex--
		}
		array[sortedIndex+1] = currentValue
	}
	return array
}
