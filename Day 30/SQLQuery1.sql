--CHECK CONSTRAINT
CREATE TABLE Employees (
    EmployeeID INT PRIMARY KEY,
    Salary DECIMAL(10, 2) CHECK (Salary > 0),
    Department VARCHAR(50) CHECK (Department IN ('HR', 'IT', 'Finance'))
);

INSERT INTO Employees values(1,10000,'Finance')
INSERT INTO Employees values(2,0,'Testing')


--NOT NULL CONSTRAINT

CREATE TABLE EmployeeDetails (
          EmployeeID INT PRIMARY KEY,
          Age INT NOT NULL,
          FirstName VARCHAR(50));

INSERT INTO EmployeeDetails values(1,22,'DISHA')
INSERT INTO EmployeeDetails values(2,NULL,'NIHARIKA')
