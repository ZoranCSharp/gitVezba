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
select * from sales.SalesOrderHeader

select 
	MONTH(OrderDate) 'Month',
	SUM(CASE YEAR(OrderDate) WHEN 2005 THEN 1 else 0 END) as 'ORDERS',
	SUM(CASE YEAR(OrderDate) WHEN 2005 THEN TotalDue ELSE 0 END) as 'TotalValue'
from sales.SalesOrderHeader
group by Month(OrderDate)
order by Month(OrderDate) asc

--Simple TRY CATCH demonstration

BEGIN TRY
	select 1/0
END TRY

BEGIN CATCH

SELECT
	--NOTE
	--In the CATCH block we can report the error
		ERROR_NUMBER() AS ErrorNumber,
		ERROR_SEVERITY() AS ErrorSeverity,
		ERROR_STATE() AS ErrorState,
		ERROR_PROCEDURE() AS ErrorProcedure,
		ERROR_LINE() AS ErrorLine,
		ERROR_MESSAGE() AS ErrorMessage;
END CATCH

--Creating A Scalar Function

select TaxAmt, Freight 
from sales.SalesOrderHeader
where SalesOrderID = 43660

create function TandF
(
@tax money,
@freight money
)

returns int
as 
begin 
	return @tax + @freight
end


select SalesOrderID, dbo.TandF(TaxAmt, Freight) 'Tax/Freight'
from sales.SalesOrderHeader

--CREATING A TABLE VALUED FUNCTION	

create function dbo.PhoneNbrs()
returns table 
as
return select p.FirstName + ' ' +p.LastName 'Name',
pp.PhoneNumber
from Person.Person p
join
Person.PersonPhone pp
on
p.BusinessEntityID=pp.BusinessEntityID

select * from PhoneNbrs()
