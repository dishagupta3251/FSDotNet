package SortingAlgo

func SelectionSort(arr []int) []int {
	for i := 0; i < len(arr)-1; i++ {

		smallestIndex := i
		for j := i + 1; j < len(arr); j++ {
			if arr[j] < arr[smallestIndex] {
				smallestIndex = j
			}
		}

		arr[i], arr[smallestIndex] = arr[smallestIndex], arr[i]
	}
	return arr
}
