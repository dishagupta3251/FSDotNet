package SortingAlgo

func SelectionSort(array []int) []int {
	for currentIndex := 0; currentIndex < len(array)-1; currentIndex++ {
		smallestValueIndex := currentIndex
		for searchIndex := currentIndex + 1; searchIndex < len(array); searchIndex++ {
			if array[searchIndex] < array[smallestValueIndex] {
				smallestValueIndex = searchIndex
			}
		}
		array[currentIndex], array[smallestValueIndex] = array[smallestValueIndex], array[currentIndex]
	}
	return array
}
