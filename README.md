# Library Management System

## Overview
This is a simple **Library Management System** implemented using **SQL Server** and **Entity Framework (Database First)** with a **C# console application** interface.  
It allows managing books, members, loans, and returns.

## Features
- Register new books and members
- Record book loans and returns
- Search for books
- View all active loans
- Handle concurrency with transactions
- Database fully normalized to **3NF**
  
## Database Structure

### Tables
- **Member** – Stores library members (ID, name, email, creation date)  
- **Book** – Stores books (ID, title, author, published year, availability)  
- **Loan** – Tracks which member has borrowed which book and when  

### ER Diagram
<img width="1271" height="456" alt="image" src="https://github.com/user-attachments/assets/efc39ca3-5de2-4c60-8468-cb86b458ef95" />

## SQL Implementation
- **Primary keys** for all tables, **foreign keys** in Loan table
- **Unique constraints** for emails
- **Default values** for dates and availability
- Fully normalized to **3NF**
- Indexed `BookId` and `MemberId` in Loan table for faster queries

## Loan and Return Management

Books are loaned to members, and returns are recorded by setting a return date on the loan. 
An active loan is any loan without a return date, which allows the system to easily track current loans while keeping historical records intact. 
While the current implementation does not include a due date, a DueDate column could be added to each loan to indicate when the book is expected to be returned. 
This would make it simple to identify overdue books and manage reminders, without affecting the existing normalized structure.

## Transactions & Concurrency
- All loan and return operations wrapped in transactions
- Ensures data integrity during simultaneous operations

```sql
BEGIN TRANSACTION;

-- Example: Loan a book
UPDATE Book SET IsAvailable = 0 WHERE BookId = @BookId;
INSERT INTO Loan (BookId, MemberId) VALUES (@BookId, @MemberId);

COMMIT TRANSACTION;
