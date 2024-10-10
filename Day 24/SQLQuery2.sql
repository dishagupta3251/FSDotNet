use pubs
select * from publishers

select * from publishers where country = 'USA' or city ='Boston'
go
select * from publishers where country !='USA'

select * from titles

select title, price from titles where price > 8 and price <15
select * from titles where type not in ('mod_cook','popular_comp')

select * from employee 

select * from employee where fname like '%e%'
go
select * from employee where hire_date < '1990-10-26'
go
select * from employee where pub_id =0877 and minit !=''

select * from employee order by pub_id

select * from employee order by pub_id, fname

select AVG(job_lvl) average from employee

select MIN(price) least_priced from titles 
go
select AVG(price) average_price from titles where pub_id=1389
go
select SUM(price) total_price from titles where type='business'

select title, MAX(price) most_expensive_book from titles group by title

select COUNT(price) total_price from titles where type='popular_comp'

select pub_id, count(emp_id) number_of_employees from employee group by pub_id

select country, COunt(pub_id) from publishers group by country