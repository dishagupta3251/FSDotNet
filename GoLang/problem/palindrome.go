package problem

import (
	"fmt"
)

func isPalindrome(str string) {
	var new string
	for i := 0; i < len(str); i++ {
		new = string(str[i]) + new
	}
	if new == str {
		fmt.Println(str, "is a palindrome")
	} else {
		fmt.Println(str, "is not a palindrome")
	}
}
func countSetBits(number int) int {
	count := 0
	for number > 0 {
		if number%2 == 1 {
			count++
		}
		number = number / 2

	}
	return count
}
func Check() {
	//word := "disha"
	//isPalindrome(word)
	fmt.Println(countSetBits(15))
}
