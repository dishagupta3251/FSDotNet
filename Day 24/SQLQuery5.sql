select * from titles where pub_id=(select pub_id from publishers where pub_name='Binnet & Hardley')
select * from publishers

select * from authors where au_id in
(select au_id  from titleauthor where title_id in
(select title_id from titles where price > 15))

select * from sales where title_id in
(select title_id from titles where pub_id=
(select pub_id from publishers where pub_name='Binnet & Hardley'))
select * from sales

select * ,  dense_Rank() over (order by price desc) ranking from titles

select pub_name Publisher_name, title as Book from publishers p join titles t on p.pub_id=t.pub_id

select title Book_name , ord_num Bill_no, qty Quantity from sales s  join titles t on t.title_id= s.title_id

select * from authors

select j.job_id,  job_desc Description, CONCAT(e.fname,' ',e.lname) FullName  from jobs j join employee e 
on
e.job_id=j.job_id order by job_id


select pub_name Publisher_name, title as Book from publishers p left outer join titles t on p.pub_id=t.pub_id

