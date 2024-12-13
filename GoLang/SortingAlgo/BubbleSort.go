package SortingAlgo

func BubbleSort(array []int) []int {
	for currentPass := 0; currentPass < len(array)-1; currentPass++ {
		for currentIndex := 0; currentIndex < len(array)-1-currentPass; currentIndex++ {
			if array[currentIndex] > array[currentIndex+1] {
				array[currentIndex], array[currentIndex+1] = array[currentIndex+1], array[currentIndex]
			}
		}
	}
	return array
}

// 2 5 9 8 7
