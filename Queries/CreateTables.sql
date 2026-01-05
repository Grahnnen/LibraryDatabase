-- Members of the library
CREATE TABLE Member (
    MemberId INT IDENTITY PRIMARY KEY,        -- Primary key
    FirstName NVARCHAR(50) NOT NULL,          -- First name
    LastName NVARCHAR(50) NOT NULL,           -- Last name
    Email NVARCHAR(100) NOT NULL UNIQUE,      -- Email, unique
    CreatedDate DATETIME2 NOT NULL DEFAULT SYSDATETIME()  -- Date created
);

-- Books in the library
CREATE TABLE Book (
    BookId INT IDENTITY PRIMARY KEY,          -- Primary key
    Title NVARCHAR(200) NOT NULL,             -- Book title
    Author NVARCHAR(100) NOT NULL,            -- Author
    PublishedYear INT NOT NULL,               -- Year published
    IsAvailable BIT NOT NULL DEFAULT 1        -- Availability flag
);

-- Records of book loans
CREATE TABLE Loan (
    LoanId INT IDENTITY PRIMARY KEY,          -- Primary key
    BookId INT NOT NULL,                       -- FK to Book
    MemberId INT NOT NULL,                     -- FK to Member
    LoanDate DATETIME2 NOT NULL DEFAULT SYSDATETIME(),   -- Loan date
    ReturnDate DATETIME2 NULL,                 -- Return date
    CONSTRAINT FK_Loan_Book FOREIGN KEY (BookId) REFERENCES Book(BookId),      -- Book FK
    CONSTRAINT FK_Loan_Member FOREIGN KEY (MemberId) REFERENCES Member(MemberId) -- Member FK
);
