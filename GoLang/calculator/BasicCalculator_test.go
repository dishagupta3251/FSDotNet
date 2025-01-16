package main

import (
	"testing"
)

func TestAdd(t *testing.T) {
	ans := AddNumbers(3, 2)
	expected := 5
	if ans != expected {
		t.Error("Expected 5, got", ans)
	}
}

func TestDivide(t *testing.T) {
	ans, err := DivideNumbers(10, 2)
	expected := 5.0
	if ans != expected && err != nil {
		t.Error("Expected 5, got", ans)
	}
}

func TestDivideError(t *testing.T) {
	result, err := DivideNumbers(10, 0)
	if err == nil {
		t.Error("Expected an error, got none", result)
	}
}
