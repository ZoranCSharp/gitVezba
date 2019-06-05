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