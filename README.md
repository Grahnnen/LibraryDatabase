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
- Indexed `BookId`, `MemberId` and `ReturnDate` in Loan table for faster queries
  
  <img width="1340" height="390" alt="image" src="https://github.com/user-attachments/assets/6d59e090-ba60-46c1-be1b-6346d42dfa36" />

Primary and foreign key constraints are used to maintain relationships between books, members, and loans, ensuring that a loan can never reference a book or member that does not exist. A unique constraint on member email addresses is also applied to avoid duplicate registrations.

To improve performance, indexes are added to foreign key columns, especially in the Loan table, since these columns are frequently used in joins and filtering of active loans. Transactions are used when registering loans and returns to make sure that all related changes are completed together, which prevents inconsistent data during simultaneous operations.Primary and foreign key constraints are used to maintain relationships between books, members, and loans, ensuring that a loan can never reference a book or member that does not exist. A unique constraint on member email addresses is also applied to avoid duplicate registrations.

## Loan and Return Management

Books are loaned to members, and returns are recorded by setting a return date on the loan. 
An active loan is any loan without a return date, which allows the system to track current loans while keeping historical records intact. 
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
