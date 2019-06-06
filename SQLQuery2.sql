--A simple stored procedure

Create procedure GetPhone
AS
select p.LastName + ', ' +p.FirstName Name, pp.PhoneNumber
from person.Person p
join Person.PersonPhone pp
on
p.BusinessEntityID=pp.BusinessEntityID
where LastName = 'Abel'

GetPhone

--Adding input parameters to a stored procedure
Alter procedure GetPhone
@lastname varchar(40)='Abel',
@firstname varchar(40)
AS
select p.LastName + ', ' +p.FirstName Name, pp.PhoneNumber
from person.Person p
join Person.PersonPhone pp
on
p.BusinessEntityID=pp.BusinessEntityID
where LastName LIKE @lastname and FirstName Like @firstname

GetPhone 'Abel', 'Catherine'
--OR--
GetPhone @firstname='Catherine',@lastname='Abel'

--Stored procedures OUTPUT parameters

create procedure ml
	@lname varchar(40),
	@numrows int=0 OUTPUT
AS
	select LastName from Person.Person
	where LastName like @lname
	SET @numrows=@@rowcount

DECLARE @retrows int
EXEC ml 'B%', @numrows=@retrows OUTPUT
select @retrows AS 'Rows'

--Using CASE Logic

select * from sales.SalesOrderHeader

select salesordernumber, customerId, totaldue,
	CASE
		WHEN totaldue<2500 THEN 'LOW'
		WHEN totaldue>2500 AND totaldue < 10000 THEN 'AVG'
		WHEN totaldue>10000 THEN 'HIGH'
	END as 'Custom Rating'
from sales.SalesOrderHeader

--inline column statements

