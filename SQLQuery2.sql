create database ProductInventoryDb
use ProductInventoryDb

create table Products
(
ProductId int primary key,
ProductName nvarchar(50),
Price float,
Quantity int,
MfgDate date,
ExpDate date )

insert into Products values (1,'Face Cream',150.25,1,'01/01/2022','01/01/2023')
insert into Products values (2,'Face Wash',200.55,3,'08/08/2022','08/08/2023')
insert into Products values (3,'Shampoo',2500.25,1,'09/01/2021','01/01/2023')
insert into Products values (4,'Body Spray',400.25,2,'10/10/2021','01/01/2023')
select * from Products