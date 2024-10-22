--PRIMARY KEY
CREATE TABLE EmployeesTable (
employeeid INT PRIMARY KEY,
salary DECIMAL(10, 2),                              
department VARCHAR(50));

INSERT INTO EmployeesTable values(1,10000,'Finance')
INSERT INTO EmployeesTable values(NULL,15000,'Testing')
INSERT INTO EmployeesTable values(1,15000,'Testing')

--FOREING Key

CREATE TABLE Student (                     
Studentid INT PRIMARY KEY,                     
Name VARCHAR(50));

CREATE TABLE Course (CourseId INT PRIMARY KEY, 
Name VARCHAR(50),
Studentid INT,
FOREIGN KEY (Studentid)
REFERENCES Student(Studentid));

INSERT INTO Student values(1,'DISHA'),(2,'Niharika')
INSERT INTO Course values(1,'Johnny',2)

--UNIQUE Constraint
CREATE TABLE Products ( 
ProductID INT PRIMARY KEY,
ProductCode VARCHAR(20) UNIQUE,
ProductName VARCHAR(100),
Price DECIMAL(10, 2));

INSERT INTO Products VALUES (1,2033,'Laptop',9000)
INSERT INTO Products VALUES (2,2033,'Mobile',8000)
INSERT INTO Products VALUES (3,NULL,'Charger',900)
INSERT INTO Products VALUES (4,NULL,'abc',900)



select * from Products



