--Q1
Select * from Products

--Q2
Select * from Products where UnitPrice >10

--Q3
Select * from Products order by UnitPrice

--Q4
Select * from Products where UnitPrice between	 10 and 25

--Q5
Select * from Products where QuantityPerUnit like '%box%'

--Q6 
Select * from Products where UnitsInStock>10 and  ReorderLevel>0

--Q7
Select * from Products where ReorderLevel>0

--Q8
Select * from Customers where Region IS NULL

--Q9
Select ContactName as FullName from Customers

--Q10
Select country, COUNT(customerID) Number_of_Customers from Customers group by country

--Q11
Select country, count( distinct City) Number_of_Cities from Customers group by country

--Q12
Select EmployeeID, COUNT(OrderID) Sales_by_Employee from Orders group by EmployeeID 

--Q13
Select CustomerID, SUM(Freight) Total_Freight_Charge from Orders group by CustomerID

--Q14

Select ProductID, Count(OrderID) Number_of_Times_Ordered  from "Order Details" group by ProductID

--Q15

Select CategoryID , AVG(UnitPrice) Average_Price, Count(ProductID) Number_of_Products from Products group by CategoryID Having COUNT(ProductID)>2  Order by AVG(UnitPrice) Desc  
