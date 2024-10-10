-- Print the product from the category 'Dairy Products'
Select ProductID, ProductName from Products where CategoryID= 
(Select CategoryID from Categories where CategoryName='Dairy Products')

--Print the products supplied by 'Tokyo Traders'
Select * from Products where SupplierID= 
(Select SupplierID from Suppliers where CompanyName='Tokyo Traders')

--Print the categories in which 'Tokyo Traders' supply products
Select CategoryName from Categories where CategoryID in
(Select CategoryID from Products where SupplierID=
(select SupplierID from Suppliers where CompanyName='Tokyo Traders'))

--Print all orders by customers from 'Spain'
Select * from Orders where CustomerID in
(Select CustomerID from Customers where Country='Spain')

-- Print the Customer name and the freight charge
Select ContactName, SUM(freight) Freight_charge from Customers c Join Orders o on c.CustomerID=o.CustomerID  group by ContactName

--Print product name and quantity sold for all orders
Select ProductName, Quantity Quantity_sold from Products p join [Order Details] o on p.ProductID=o.ProductID

--print the products that are billed and the unbilled products with the price and sale price and the difference
Select p.ProductName,p.UnitPrice Cost_Price, o.UnitPrice Sale_Price, (p.UnitPrice-o.UnitPrice) Difference from Products p
 left join [Order Details] o on p.ProductID=o.ProductID

 --Print the order number, Customer name, Product name and the quantity sold for all orders
 Select o.OrderID, c.ContactName, ProductName, od.Quantity Quantity_sold from [Order Details] od join Orders o
 on od.OrderID=o.OrderID
 join
 Customers c on c.CustomerID=o.CustomerID
 join
 Products p on od.ProductID=p.ProductID
 
-- Print the total order amount for every order(price*quantity)+freight
Select od.orderID ,SUM((od.UnitPrice*od.quantity)+o.freight) Total_Amount from [Order Details] od
join Orders o on o.OrderID=od.OrderID
group by od.orderID

-- Print the customer name, Phone, shipper name, phone for every order
Select c.contactname Customer_Name, c.Phone, s.companyName, s.phone from Customers c join Orders o on c.CustomerId=o.CustomerID
join
Shippers s on s.ShipperID=o.ShipVia


-- print the shipper name and number of order by the shipper and the total freight charge
select s.CompanyName, COUNT(o.OrderID) No_Of_Orders, SUM(o.Freight) Total_freight_charge from 
Shippers s join Orders o
on s.ShipperID=o.ShipVia 
group by s.CompanyName
order by s.CompanyName 


-- Print the product name, customer name, total quantity bought for all products sold by employees from 'USA'
Select  p.ProductName,c.ContactName, SUM(od.quantity) Total_Quantity from Orders o
join 
[Order Details] od on o.OrderID=od.OrderID
join
Employees e on e.EmployeeID=o.EmployeeID
join 
Customers c on c.CustomerID=o.CustomerID
join
Products p on p.ProductID=od.ProductID
where e.Country='USA'
group by p.ProductName,c.ContactName




-- Print the product name, category and the total sale amount sorted by category, Include all products and all categories
select p.productName, c.categoryID, SUM(od.UnitPrice) Total_Sale from
Products p 
left join
Categories c on p.CategoryID =c.CategoryID
left join
[Order Details] od on od.ProductID=p.ProductID
group by p.ProductName,c.CategoryID
order by c.CategoryID

-- Print the category name and the total sale for category for all
select c.categoryName,p.ProductName, COUNT(od.OrderID) Total_Sale from [Order Details] od join Orders o on od.OrderID=o.OrderID 
right outer join 
Products p on p.ProductID=od.ProductID
full join 
Categories c on c.CategoryID=p.CategoryID
group by c.CategoryName,p.ProductName




