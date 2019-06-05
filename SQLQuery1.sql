select * from sales.SalesTerritory

select name, countryRegionCode, [Group], SalesYTD
from sales.SalesTerritory

select name, countryRegionCode, [Group], SalesYTD
INTO SalesTestTaBLE
from sales.SalesTerritory

SELECT * FROM SalesTestTaBLE

create table dbo.EmployeeSales
(DataSource varchar(20) not null,
BusinessEntityID varchar(11) not null,
LastName varchar(40) not null,
SalesDollars money not null);

insert into dbo.EmployeeSales
select 'select', sp.BusinessEntityID, c.LastName, sp.SalesYTD
from Sales.SalesPerson as sp
inner join person.Person as c
on sp.BusinessEntityID = c.BusinessEntityID
where sp.BusinessEntityID like '2%'
order by sp.BusinessEntityID, c.LastName;

select * from EmployeeSales

select e.businessEntityID, p.Title, p.FirstName, p.LastName
from person.person p
join 
HumanResources.Employee e
on
p.BusinessEntityID = e.BusinessEntityID
where title ='Mr.'

select e.businessEntityID
from humanResources.Employee e
where e.BusinessEntityID in
	(select BusinessEntityID from
	person.Person where title='Mr.')


select p.BusinessEntityID, p.FirstName +' '+ p.LastName SALESPERSON	
from Person.Person p
where exists
	(select * 
	 from Sales.SalesOrderHeader s
	 where TotalDue>150000
	 and p.BusinessEntityID=s.SalesPersonID )

	 select * from Sales.SalesOrderHeader