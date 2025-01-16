# Flight Booking System

## Overview
The **Flight Booking System** is a console-based application implemented in Go, designed for managing flights. It features distinct modules for admin and user functionalities, ensuring seamless interaction for searching and managing flight details.



## Features

### Admin Functionalities:
- **Add Flights**:
  - Admin can add new flights with details like source, destination, date, airline, price, class, and seats.
  - Ensures that no past dates can be entered for the flight schedule.

- **Delete Flights**:
  - Allows deletion of a flight based on its unique ID.
  - Prompts for confirmation before deletion.

- **View All Flights**:
  - Displays a list of all available flights with their details.

- **Access Control**:
  - Provides a dedicated admin interface for managing flights.

### User Functionalities:
- **Search Flights**:
  - Search flights based on source, destination, and date.
  - Ensures easy and intuitive searching.

- **Filter Results**:
  - Users can apply filters on search results:
    - Filter by price (ascending order).
    - Filter by airline.

- **View Search Results**:
  - Displays detailed results for searched flights, including source, destination, airline, price, class, and available seats.


## Usage Workflow

### Admin
- **Access Admin Service**:
  - Menu-driven interface for managing flights.
  - Options include adding, deleting, and viewing all flights.

- **Adding Flights**:
  - Input required details: source, destination, date (validates for future dates), airline name, price, class, and seats.

- **Deleting Flights**:
  - Provide the unique flight ID for deletion.
  - Confirm deletion.

- **Viewing Flights**:
  - Displays all flights in a detailed format.

### User
- **Search Flights**:
  - Enter source, destination, and date to view matching flights.

- **Apply Filters**:
  - Filter the search results by:
    - **Price**: Sort flights by ticket price in ascending order.
    - **Airline**: Filter flights by a specific airline.

- **View Results**:
  - Displays flight details such as company, price, type, source, destination, date, and seats.



## Validation Features
- **Date Validation**:
  - Ensures flights cannot be scheduled for past dates during the add operation.

- **Input Validation**:
  - Prompts for invalid entries in menus and ensures correct data input.



## Example Scenarios

### Admin
- **Add a Flight**:
  - Enter details such as:
    - Source: `Lucknow`
    - Destination: `Noida`
    - Valid future date: `2024-12-25`
    - Airline name: `Indigo`
    - Ticket price, class: `Business`
    - Available seats: `50`
  - Result: The flight is added to the list.

- **Delete a Flight**:
  - Provide the unique flight ID (e.g., `1`) of the flight to delete.
  - Confirm deletion.


### User
- **Search for Flights**:
  - Input search criteria:
    - Source: `Lucknow`
    - Destination: `Noida`
    - Date: `2024-12-25`
  - View matching flights.

- **Apply Filters**:
  - Sort results by price or filter by a specific airline.

## Output Screenshots

### Admin Panel

  ![ Flight Screenshot](Screenshots/Screenshot%202024-12-13%20205157.png)


  ![ Flight Screenshot](Screenshots/Screenshot%202024-12-13%20205158.png)

  
  ![ Flights Screenshot](Screenshots/Screenshot%202024-12-13%20205540.png)

### User Panel

  ![ Flights Screenshot](Screenshots/Screenshot%202024-12-13%20211341.png)

 
  ![ Screenshot](Screenshots/Screenshot%202024-12-13%20211612.png)

## Notes
- This system is a console-based application and requires user input for all operations.
- Ensures robust validation for flight details, including a strict check to prevent past dates.
