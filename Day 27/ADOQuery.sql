use Northwind

create table Users( id int identity constraint pk primary key ,name varchar(10),
username varchar(20) ,
 password varchar(20)
)

drop table Users
insert into Users values('Atul','atulg','disha')

select * from Users where username='dishagupta' and password='atul'

delete from Users
select * from Users

select * from Customers
Select * from [Order Details]
Select * from Shippers
Select * from Orders 
Select o.OrderID, c.ContactName, p.ProductName , o.ShipVia from Orders o 
join Customers c on o.CustomerID=c.CustomerID 
join [Order Details] od on od.OrderID=o.OrderID
join Products p on p.ProductId=od.ProductID
where o.OrderID=11011

Select o.OrderID, s.ShipperID, s.CompanyName, s.Phone from Orders o join Shippers s on o.ShipVia=s.ShipperID  where o.OrderID=11011