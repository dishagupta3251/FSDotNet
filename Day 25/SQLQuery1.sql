--1) Print the storeid and number of orders for the store
Select s.stor_id, COUNT(ord_num) Number_of_Orders from stores s
left join 
sales o on s.stor_id=o.stor_id 
group by s.stor_id

--2) print the number of orders for every title
Select t.title_id , t.title, COUNT(o.ord_num) Number_of_Orders from titles t
left join 
sales o on t.title_id=o.title_id 
group by t.title_id,t.title


--3) print the publisher name and book name
Select p.pub_name, b.title from publishers p 
left join
titles b on p.pub_id=b.pub_id

--4) Print the author full name for al the authors
Select CONCAT(au_fname,' ',au_lname) FullName from authors

--5) Print the price or every book with tax (price + price*12.36/100)
Select title_id, title, Round((price+(price*(12.36/100))),2) Total_Price from titles

--6) Print the author name, title name
Select  CONCAT(a.au_fname,' ',a.au_lname) Author_name, t.title Title_name from authors a
join 
titleauthor ta on ta.au_id=a.au_id
join
titles t on t.title_id=ta.title_id

--7) print the author name, title name and the publisher name
Select  CONCAT(a.au_fname,' ',a.au_lname) Author_name, t.title Title_name, p.pub_name from authors a
left join 
titleauthor ta on ta.au_id=a.au_id
join
titles t on t.title_id=ta.title_id
join 
publishers p on t.pub_id=p.pub_id

--8) Print the average price of books published by every publisher
Select  p.pub_name,AVG(t.price) Average_price from publishers p
left join
titles t on t.pub_id=p.pub_id
group by p.pub_name 

--9) print the books published by 'Marjorie'
Select t.title Books, a.au_fname from titleauthor ta
join
titles t on t.title_id=ta.title_id
join
authors a on a.au_id=ta.au_id
where a.au_fname='Marjorie'


--10) Print the order numbers of books published by 'New Moon Books'
Select s.ord_num, p.pub_name from sales s 
join
titles t on t.title_id=s.title_id
join
publishers p on p.pub_id=t.pub_id
where p.pub_name='New Moon Books'


--11) Print the number of orders for every publisher
Select p.pub_name, COUNT(s.ord_num) Number_of_Orders from sales s
join 
titles t on t.title_id=s.title_id
right join
publishers p on p.pub_id=t.pub_id
group by p.pub_name


--12) print the order number , book name, quantity, price and the total price for all orders
Select s.ord_num, t.title, s.qty, t.price , (t.price*s.qty) Total_price from sales s
left join
titles t on t.title_id=s.title_id


--13) print he total order quantity for every book
Select t.title, SUM(s.qty) Total_order from titles t 
left join
sales s on s.title_id=t.title_id
group by t.title

--14) print the total order value for every book
Select t.title, SUM(s.qty*t.price) Total_order_value from titles t 
left join
sales s on s.title_id=t.title_id
group by t.title


--15) print the orders that are for the books published by the publisher for which 'Paolo' works for
select s.ord_num, t.title from sales s 
join 
titles t on t.title_id=s.title_id
join
employee e on e.pub_id=t.pub_id
where e.fname='Paolo'

