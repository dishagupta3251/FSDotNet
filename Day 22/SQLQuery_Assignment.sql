create database dbCompany07Oct2024
 
use dbCompany07Oct2024
 

CREATE TABLE EMP( emp_no int identity(1,1) constraint pk_emp_no primary key, emp_name varchar(50), salary decimal,boss_no int constraint fk_boss_no foreign key references EMP(emp_no) null )

CREATE Table department(dept_name varchar(10) constraint pk_dept_name primary key, floor_no int, phone varchar(15), 
emp_no int constraint fk_emp_no foreign key references EMP(emp_no) not null)

alter table EMP
add dept_name varchar(10) constraint fk_dept_name foreign key references department(dept_name);

CREATE TABLE Sales( sale_no int  constraint pk_sale_no primary key, sale_qty varchar(50), item_name varchar(30) constraint fk_item_name foreign key references Items(item_name) not null,
dept_name varchar(10) constraint fk_dept_name_sales foreign key references department(dept_name) not null )

CREATE TABLE Items(item_name varchar(30) constraint pk_item_name primary key, item_type varchar(30), item_color varchar(10))



sp_help emp


INSERT INTO EMP (emp_name, salary, boss_no)
VALUES 
('Alice', 75000, NULL),
('Ned', 45000, 1),
('Andrew', 25000, 2),
('Clare', 22000, 2),
('Todd', 38000, 1),
('Nancy', 22000, 5),
('Brier', 43000, 1),
('Sarah', 56000, 7),
('Sophile', 35000, 1),
('Sanjay', 15000, 3),
('Rita', 15000, 4),
('Gigi', 16000, 4),
('Maggie', 11000, 4),
('Paul', 15000, 3),
('James', 15000, 3),
('Pat', 15000, 3),
('Mark', 15000, 3);

select * from EMP

INSERT INTO department (dept_name, floor_no, phone, emp_no)
VALUES
('Management', 5, '34', 1),
('Books', 1, '81', 4),
('Clothes', 2, '24', 4),
('Equipment', 3, '57', 3),
('Furniture', 4, '14', 3),
('Navigation', 1, '41', 3),
('Recreation', 2, '29', 4),
('Accounting', 5, '35', 5),
('Purchasing', 5, '36', 7),
('Personnel', 5, '37', 9),
('Marketing', 5, '38', 2);


UPDATE EMP SET dept_name = 'Management' WHERE emp_no = 1;
UPDATE EMP SET dept_name = 'Marketing' WHERE emp_no IN (2, 3, 4);
UPDATE EMP SET dept_name = 'Accounting' WHERE emp_no IN (5, 6);
UPDATE EMP SET dept_name = 'Purchasing' WHERE emp_no IN (7, 8);
UPDATE EMP SET dept_name = 'Personnel' WHERE emp_no = 9;
UPDATE EMP SET dept_name = 'Navigation' WHERE emp_no = 10;
UPDATE EMP SET dept_name = 'Books' WHERE emp_no = 11;
UPDATE EMP SET dept_name = 'Clothes' WHERE emp_no IN (12, 13);
UPDATE EMP SET dept_name = 'Equipment' WHERE emp_no IN (14, 15);
UPDATE EMP SET dept_name = 'Furniture' WHERE emp_no = 16;
UPDATE EMP SET dept_name = 'Recreation' WHERE emp_no = 17;



INSERT INTO Items (item_name, item_type, item_color)
VALUES
('Pocket Knife-Nile', 'E', 'Brown'),
('Pocket Knife-Avon', 'E', 'Brown'),
('Compass', 'N', NULL),
('Geo positioning system', 'N', NULL),
('Elephant Polo stick', 'R', 'Bamboo'),
('Camel Saddle', 'R', 'Brown'),
('Sextant', 'N', NULL),
('Map Measure', 'N', NULL),
('Boots-snake proof', 'C', 'Green'),
('Pith Helmet', 'C', 'Khaki'),
('Hat-polar Explorer', 'C', 'White'),
('Exploring in 10 Easy Lessons', 'B', NULL),
('Hammock', 'F', 'Khaki'),
('How to win Foreign Friends', 'B', NULL),
('Map case', 'E', 'Brown'),
('Safari Chair', 'F', 'Khaki'),
('Safari cooking kit', 'F', 'Khaki'),
('Stetson', 'C', 'Black'),
('Tent - 2 person', 'F', 'Khaki'),
('Tent -8 person', 'F', 'Khaki');

select * from department


INSERT INTO Sales (sale_qty, item_name, dept_name)
VALUES
('2', 'Boots-snake proof', 'Clothes'),
('1', 'Pith Helmet', 'Clothes'),
('1', 'Sextant', 'Navigation'),
('3', 'Hat-polar Explorer', 'Clothes'),
('5', 'Pith Helmet', 'Equipment'),
('2', 'Pocket Knife-Nile', 'Clothes'),
('3', 'Pocket Knife-Nile', 'Recreation'),
('1', 'Compass', 'Navigation'),
('2', 'Geo positioning system', 'Navigation'),
('5', 'Map Measure', 'Navigation'),
('1', 'Geo positioning system', 'Books'),
('1', 'Sextant', 'Books'),
('3', 'Pocket Knife-Nile', 'Books'),
('1', 'Pocket Knife-Nile', 'Navigation'),
('1', 'Pocket Knife-Nile', 'Equipment'),
('1', 'Sextant', 'Clothes'),
('1', 'Exploring in 10 Easy Lessons', 'Books'),
('1', 'Elephant Polo stick', 'Recreation'),
('1', 'Camel Saddle', 'Recreation');


update department set emp_no=10 where dept_name='Books'

