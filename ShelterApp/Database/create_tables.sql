CREATE TABLE IF NOT EXISTS Shelter (
    Id SERIAL PRIMARY KEY,
    Name VARCHAR(255) NOT NULL,
    Address VARCHAR(255),
    Number VARCHAR(25)
);

CREATE TABLE IF NOT EXISTS Enclosure (
    Id SERIAL PRIMARY KEY,
    Number VARCHAR(50) NOT NULL,
    Capacity INT NOT NULL,
    TypeOfEnclosure VARCHAR(50)
);

CREATE TABLE IF NOT EXISTS Guardian (
    Id SERIAL PRIMARY KEY,
    Name VARCHAR(255) NOT NULL,
    Number VARCHAR(20),
    Email VARCHAR(150),
    Address VARCHAR(255),
    GuardianshipDay DATE
);

CREATE TABLE IF NOT EXISTS Veterinarian (
    Id SERIAL PRIMARY KEY,
    Name VARCHAR(255) NOT NULL,
    DateOfAccreditation DATE
);

CREATE TABLE IF NOT EXISTS Shelter_Employee (
    Id SERIAL PRIMARY KEY,
    Name VARCHAR(255) NOT NULL,
    JobTitle VARCHAR(255)
);

CREATE TABLE IF NOT EXISTS Animal (
    Id SERIAL PRIMARY KEY,
    Name VARCHAR(100) NOT NULL,
    Type VARCHAR(50) NOT NULL,
    Breed VARCHAR(100),
    DateOfBirth DATE,
    Id_Enclosure INT,
    Id_Guardian INT,
    CurrentStatus VARCHAR(50) DEFAULT 'Новый',
    Gender VARCHAR(20),
    Size VARCHAR(20),
    Temperament TEXT,
    Photo1 TEXT,
    Photo2 TEXT,
    Photo3 TEXT,
    FOREIGN KEY (Id_Enclosure) REFERENCES Enclosure(Id) ON DELETE SET NULL,
    FOREIGN KEY (Id_Guardian) REFERENCES Guardian(Id) ON DELETE SET NULL
);

CREATE TABLE IF NOT EXISTS Application (
    Id SERIAL PRIMARY KEY,
    Id_Guardian INT NOT NULL,
    Id_Animal INT NOT NULL,
    SubmissionDate DATE DEFAULT CURRENT_DATE,
    ApplicationStatus VARCHAR(50) DEFAULT 'В рассмотрении',
    Id_Employee INT,
    FOREIGN KEY (Id_Guardian) REFERENCES Guardian(Id) ON DELETE CASCADE,
    FOREIGN KEY (Id_Animal) REFERENCES Animal(Id) ON DELETE CASCADE,
    FOREIGN KEY (Id_Employee) REFERENCES Shelter_Employee(Id) ON DELETE SET NULL
);

CREATE TABLE IF NOT EXISTS Medical_Record (
    Id SERIAL PRIMARY KEY,
    Id_Animal INT NOT NULL,
    Id_Veterinarian INT NOT NULL,
    DateOfInspection DATE DEFAULT CURRENT_DATE,
    Diagnosis TEXT,
    Treatment TEXT,
    FOREIGN KEY (Id_Animal) REFERENCES Animal(Id) ON DELETE CASCADE,
    FOREIGN KEY (Id_Veterinarian) REFERENCES Veterinarian(Id) ON DELETE CASCADE
);

CREATE TABLE IF NOT EXISTS Users (
    Id SERIAL PRIMARY KEY,
    Username VARCHAR(100) UNIQUE NOT NULL,
    PasswordHash VARCHAR(255) NOT NULL,
    Email VARCHAR(150) UNIQUE NOT NULL,
    FullName VARCHAR(255),
    Role VARCHAR(50) DEFAULT 'User',
    Photo TEXT,
    Id_Guardian INT,
    CreatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (Id_Guardian) REFERENCES Guardian(Id) ON DELETE SET NULL
);

CREATE TABLE IF NOT EXISTS Favorites (
    Id SERIAL PRIMARY KEY,
    Id_User INT NOT NULL,
    Id_Animal INT NOT NULL,
    AddedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (Id_User) REFERENCES Users(Id) ON DELETE CASCADE,
    FOREIGN KEY (Id_Animal) REFERENCES Animal(Id) ON DELETE CASCADE,
    UNIQUE(Id_User, Id_Animal)
);
