# Libray API

The Library API provide the following functionality

- **Admin Role**

  - ***Books***
    - List of all books in the library
    - Search book by title, will show whether the book is available or not , and if not will show the next available date
    - Get list of borrowed books
    - Get list of reserved books
    - Add a new book to the library
    - Borrow a book
    - Reserve a book
    - Return a book

  - ***Customers***
    - List of all customers
    - Add new customer
    - Update customer details
    - Get customer profile , also show list of borrowed and reserved books
 

  - ***Notifications***
    - List of all pending notifications
    - Add new notification for a customer
    - Cance a notificcation

- **Customer Role**

  - Get profile including borrowed/reserved books
  - Borrow a book
  - Reserve a book



## Setting up database

The project currently use in memory database, but a database server can be setup and connection settings updated accordingly:

### OAuth 2.0 Setup

The project  IdentityServer4 to handle authentication and authorization using  JWT tokens that API can use to authenticate request. Once the server been setup change the configuration url in the app seetings

```json
  "IdentityServer": {
    "url": "https://test-identity-server"
  }
```
- The roles Admin or Customer should be assigned to the users based on their priviledges


