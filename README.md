# Airline Ticketing System

A C# client-server flight reservation system.

https://github.com/user-attachments/assets/6d80fa4f-9cd2-4b36-a8fd-f6d74798e409

## Features

### For Users:
* Flight Search: Find flights based on required parameters, such as departure airport, arrival airport, and departure date.
* Seat Selection and Additional Options: Select seats on the flight and choose additional options, such as the maximum baggage allowance.
* Ticket Booking: Book one or more tickets for a flight.
* View Reserved Seats: Check which seats have been reserved (by PNR - Passenger Name Record, or, if logged in, from history), including those automatically assigned to passengers.
* Ticket Cancellation: Cancel one or more booked tickets for a flight.
* View All Booked Flights: View a list of all flights for which the user has booked tickets.
* View Ticket Information: Retrieve details of reserved tickets using the passenger's last name and PNR.

### For Booking System Operators:
* View Scheduled Flights: View flights scheduled for a specific date, with filtering options based on departure and arrival airports.
* Passenger Data Management: View information about passengers who have booked or canceled tickets for a specific flight, including the number of booked and canceled seats.
* Passenger Attendance Tracking: Record which passengers have arrived and which have not for a specific flight.
* View Booked Tickets: Access a list of all booked tickets with filtering options based on passenger and ticket details.

## Implementation

The clients and server are implemented in C# with the client interface developed using Windows Forms. Communication between clients and server is implemented using Windows Communication Foundation.

### Server

The application uses SQL Server as the database for storing flight and ticketing information.

Server class diagram:

![image](https://github.com/user-attachments/assets/37089bab-876a-4635-9cfd-6ad199baa788)

Passenger documents class diagram:

![image](https://github.com/user-attachments/assets/68cdaf3e-e217-4982-be67-3ec14496a156)

Flight options class diagram:

![image](https://github.com/user-attachments/assets/abcdb67c-eda7-4f8d-a5d5-7fc9cb4b996a)

Database schema:

![image](https://github.com/user-attachments/assets/885cf5f3-146a-4f22-9eab-b327846ece03)

## Client for Passengers

You can filter flights by departure and arrival locations, as well as time.
![image](https://github.com/user-attachments/assets/b9538fd7-6dbf-4c5a-84f8-0bd4807ff2ae)

![image](https://github.com/user-attachments/assets/65a0f8ae-deb4-4eae-83bd-efe80ad3654b)
<p align="center"><i>
Example of a flight search result
</i></p>

After choosing a flight, you can add passengers and choose their seats, or let the system choose a seat for you when booking.

![image](https://github.com/user-attachments/assets/fddd5eb5-3e78-43c5-a20d-bd0c0ca356fd)

When filling in passenger data, previously filled information is accessible and can be reused (if logged in).
![image](https://github.com/user-attachments/assets/3274c7b7-7754-4f98-a090-1e7341c6a03c)

Extra baggage can be added per-passenger.
![image](https://github.com/user-attachments/assets/c6fb28b2-406b-42db-a772-ede6e8ef898c)

![image](https://github.com/user-attachments/assets/c5533c82-072b-49b6-991f-a95f7f85ef02)
<p align="center"><i>
Booking a flight
</i></p>

## Admin Client

Admins can search for a flight or look up individual passengers. 
![image](https://github.com/user-attachments/assets/2194efdb-3e57-45bc-8c37-9896b46f0a1f)

Passengers can be searched by name and PNR.
![image](https://github.com/user-attachments/assets/31944066-b742-44aa-b1bf-e09b774121ef)

After searching, a list of matching passengers is shown.
![image](https://github.com/user-attachments/assets/351bb9b1-76e4-4570-8a8f-e2d3686b6692)

Flights can be filtered by departure location and arrival, as well as by time.
![image](https://github.com/user-attachments/assets/529e9eec-0842-402e-8ed8-cc4c22fecddf)

![image](https://github.com/user-attachments/assets/692570dd-4cfc-479f-a207-4e378f4b7b76)

Flight screen. Booked seats can be marked as "arrived" or "canceled".
![image](https://github.com/user-attachments/assets/32eda244-7b27-4bf0-b0ea-7ec71e0a0c93)
