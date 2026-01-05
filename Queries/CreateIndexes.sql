-- Check if book is loaned 
CREATE INDEX IX_Loan_BookId ON Loan(BookId);
-- Show members loans 
CREATE INDEX IX_Loan_MemberId ON Loan(MemberId);
-- Find active loans 
CREATE INDEX IX_Loan_ReturnDate ON Loan(ReturnDate);
-- These are the most used ids in the program, changing them to Index seek instead of tablescan makes it run faster 
