create database dbCompany07Oct2024
 
use dbCompany07Oct2024
 
--create,alter,drop - DDL

CREATE TABLE Area

(area varchar(10),

zipcode char(6)

)
 
select * from Area
 
sp_help Area
 
ALTER TABLE Area

add remarks varchar(20)
 
ALTER TABLE Area

drop column remarks
 
CREATE TABLE Areas(area varchar(10) constraint pk_area primary key, zipcode char(6))
 

 create table Employees(Id int identity(101,1) constraint pk_employee_ID primary key, name varchar(20) not null, phone varchar(15), area varchar(10) constraint fk_area foreign key references Areas(area))

 sp_help Employees

 insert into Areas(area,zipcode) values ('ABC','12788'),('BVH','82781'),('JKL','29892')

 insert into Employees(name,phone,area) values ('Ramu','83782728738','ABC'),('Sonu','834879738','JKL'),('Somu','889990738','BVH')
 --error for adding area which is not present in area table as it is foreign key
 insert into Employees(name,phone,area) values ('Ramu','83782728738','KKK')
 --error inserting null value after in a not null column
  insert into Employees(name,phone,area) values (null,'83782728738','ABC')

  create table skills(skill_name varchar(15) constraint pk_skill_name primary key, skill_description varchar( 50))
  insert into skills values('C#','Web'),('Java','OOPS'),('SQL','RDBMS')

  create table Employee_Skills(Emp_id int constraint fk_emp_id foreign key references Employees(Id), Emp_Skills varchar(15) constraint fk_emp_skills foreign key references skills(skill_name) , Skill_level float, constraint pk_employee_skills primary key(Emp_id,Emp_Skills))
  insert into Employee_Skills values (101,'C#',3),(101,'SQL',8),(102,'C#',9),(102,'Java',3)

   select * from Employee_Skills

   update Employee_Skills set  Skill_level=7 where Emp_id='101' and Emp_Skills='SQL'

   delete from Employee_Skills where Emp_id='101' and Emp_Skills='C#'
 select * from Areas

  select * from Employees