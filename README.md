# Concert Ticket Booking System

The Concert Ticket Booking System is a .NET Core application enabling users to book concert tickets. The application uses a PostgreSQL database to manage data. Follow the instructions below to set up the application on your local machine.

## Prerequisites

- .NET Core 6
- PostgreSQL installed and running on your localhost
- Git

## Configuration

Ensure that you have PostgreSQL installed on your local host with the following configuration :

## PostgreSQL Local Setup

Ensure that you have PostgreSQL installed on your local host with the following configuration:

|      Access Information      |           |
|-----------------------------|-----------|
| **Host**                     | localhost |
| **Port**                     | 5432      |
| **Database**                 | postgres  |
| **Username**                 | postgres  |
| **Password**                 | postgres  |

## Project Details

- The project runs on port 8081.
- Access the documentation at https://localhost:7163/swagger/index.html

## Endpoints

### Get Concert by ID

Retrieve concert details by providing their ID.

| Method   | Endpoint              | Description                            |
|----------|-----------------------|----------------------------------------|
| GET      | /api/concert/{id}    | Retrieve concert details by ID.        |

**Parameters:**
- `id`: ID of the concert

### Get All Concerts and Search

Retrieve a list of all concerts and optionally search by name and venue.

| Method   | Endpoint          | Description                            |
|----------|-------------------|----------------------------------------|
| GET      | /api/concert    | Retrieve all concerts with optional search filters. |

**Query Parameters:**
- `name` (optional): Search concerts by name
- `venue` (optional): Search concerts by venue
- `page` (optional): Page number for pagination
- `size` (optional): Number of items per page

**Example Usage:**
To search for concerts with the name "Concert z" and venue "Venue x", while paginating the results with 10 items per page, use the following endpoint:

```bash
GET /api/concerts?name=Concert A&venue=Venue A&page=1&size=10
```

### Insert Concert

Insert new concert.

| Method   | Endpoint              | Description                            |
|----------|-----------------------|----------------------------------------|
| POST      | /api/concert          | Insert new concert.                    |

**Parameters:**
- `name`: name of the concert
- `date` : date of the concert
- `venue` : venue of the concert

 ### Update Concert

Update existing concert.

| Method   | Endpoint              | Description                            |
|----------|-----------------------|----------------------------------------|
| PUT      | /api/concert/{id}     | Update existing concert.               |

**Parameters:**
- `id` : id of the concert
- `name`: name of the concert
- `date` : date of the concert
- `venue` : venue of the concert
  
### Delete Concert

Delete existing concert by ID.

| Method   | Endpoint              | Description                            |
|----------|-----------------------|----------------------------------------|
| DELETE      | /api/concert/{id}  | Delete existing concert by ID.         |

**Parameters:**
- `id`: ID of the concert

### Book Ticket

Retrieve concert details by providing their ID.

| Method   | Endpoint              | Description                            |
|----------|-----------------------|----------------------------------------|
| POST      | /api/book            | Book a ticket for a concert.        |

**Request Body Example:**
```json
{
    "concert_id": "5d700f63-7e12-4a50-a9d7-70adad1d423a",
    "type": "VIP"
}
```

# Concert Tickets Database

## Overview

This repository contains a well-structured database designed to manage information about concerts and their associated tickets. The database comprises two main tables: "Concerts" and "Tickets."

## Table Structures

### Concert Table

The "Concert" table stores essential details about various concerts, including their names, dates, and venues.

| Column      | Data Type | Description                       |
|-------------|-----------|-----------------------------------|
| id          | UUID      | Unique concert identifier.        |
| name        | varchar   | Name of the concert.              |
| date        | Date      | Date of the concert.              |
| venue       | varchar   | Venue where the concert is held. |

### Ticket Table

The "Ticket" table holds information about available tickets for each concert, including their types, prices, and quantities.

| Column       | Data Type | Description                                        |
|--------------|-----------|----------------------------------------------------|
| id           | UUID      | Unique ticket identifier.                          |
| concert_id   | varchar   | Foreign key referencing the corresponding concert.|
| type         | varchar   | Type of the ticket (e.g., VIP, Regular).           |
| price        | Decimal   | Price of the ticket.                              |
| available_qty | Integer  | Number of tickets available for this type.        |

## Relationship

Each ticket in the "Ticket" table is linked to a specific concert through the "concert_id" foreign key. This establishes a connection between the "Concert" and "Ticket" tables.

## Example Usage

**Scenario:**
A user wishes to purchase tickets for "Concert A" at "Venue X."

**Steps:**
1. Query the "Concerts" table using the concert's name or date to retrieve details about "Concert A."
2. Use the concert's ID from the previous step to query the "Ticket" table. This will display available ticket types, prices, and quantities.
3. Select a preferred ticket type (e.g., VIP) and proceed with the purchase.
4. Update the "available_qty" in the "Ticket" table to reflect the reduced availability of the chosen ticket type for "Concert A."

In summary, this database design serves as a valuable tool for efficiently managing concert and ticket data, streamlining processes such as ticket sales and inventory tracking.
