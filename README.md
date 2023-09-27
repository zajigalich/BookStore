# Bookstore API

Bookstore API is a RESTful API designed to manage a library of books, authors, and genres. It allows users to quickly search, add, update, and delete book information in an organized manner. 

## Features

- Manage books with information like title, author, and genre
- Manage authors and their related information
- Manage genres and their related information
- Search through books based on title, author, and genre
- Pagination support for list results
- User authentication and authorization for managing resources (optional)

## Requirements

- .NET 6.0 or newer
- A SQL Server instance for the database

## Installation

1. Clone the repository: 		
   git clone https://github.com/zajigalich/BookStore.git

2. (Optional) If you want to use a relational database, set up your database and update the connection string in the `appsettings.json` file in the `BookStore.API` directory:
	
	"DefaultConnection": "Server=localhost;Database=bookStore_db;User=user;Password=password"

3. Install the required dependencies:
	dotnet restore
	
4. Build the project:
	dotnet build

5. (Optional) If you want to run the test suite, navigate to the `BookStore.Tests` directory, then run:
	dotnet test


## Usage
You can use any RESTful API client, such as Postman or Insomnia, to interact with the API's endpoints. The following are a few examples of how to use the Bookstore API:

### Retrieve all books

GET http://localhost:5000/api/books

### Search books by title

GET http://localhost:5000/api/books?title=example

### Add a new book

POST http://localhost:5000/api/books

{
  "title": "New Book Title",
  "authorId": 1,
  "genreId": 1
}

### Update a book

PUT http://localhost:5000/api/books/1

{
  "title": "Updated Book Title",
  "authorId": 2,
  "genreId": 2
}

### Delete a book

DELETE http://localhost:5000/api/books/1

Make sure to replace http://localhost:5000 with the address your API is running on. For more information, visit the API documentation (e.g., Swagger/OpenAPI).

## Running Tests

To run the test suite, navigate to the `BookStore.Tests` directory and run the following command:

dotnet test

## License

This project is licensed under the [MIT License](https://opensource.org/licenses/MIT).

## Contact Information
- [GitHub](https://github.com/zajigalich)
- [LinkedIn](https://www.linkedin.com/in/%D1%82%D0%B8%D1%82%D0%B0%D1%80%D0%B5%D0%BD%D0%BA%D0%BE-%D0%BA%D0%BE%D0%BD%D1%81%D1%82%D0%B0%D0%BD%D1%82%D0%B8%D0%BD-6a5ba3269/)

## Feedback 
- Was it easy to complete the task using AI? 
  
	Yes, it was easy
  
- How long did task take you to complete? (Please be honest, we need it to gather anonymized statistics) 
 
	About 3-5 hours
	
- Was the code ready to run after generation? What did you have to change to make it usable?
 
	No, it wasn't. I had to change test configuration file `TestDbContextFactory`

- Which challenges did you face during completion of the task?
 
	Some explanations of mistakes in generated code lead to GPT repeating simular mistakes
	
- Which specific prompts you learned as a good practice to complete the task?

	Starting conversation with description of desired projects and providing acceptence criteria, it's usefull to add: 

	Create a list of tasks with examples of prompts I can ask you for each task to get relevant examples. 

	Thus, it's easy to nagigate beetween all stages of development using provided promts.