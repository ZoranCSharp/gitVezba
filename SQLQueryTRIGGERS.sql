--Trigger Examples

create table TrigEx
(
LastName varchar(50),
FirstName varchar(50)
)

create trigger Notify
on TrigEx
for insert
as
print 'ATTENTION!!!'
print 'A row was inserted into the TrigEx table...'
SELECT 'Then inserrted data was: ', * from inserted
SELECT * from TrigEx

insert into TrigEx
values
('James', 'Jack'), ('James','Frank'),('Long','Mark')


create trigger ModEx
ON TrigEx
for update 
as
print 'Attention!!!'
print 'A row was modified in the TrigEx table...'

select 'deleted: The original data was: ', *from deleted
select 'inserted: The new data is: ', * from inserted
select * from TrigEx

update TrigEx
set LastName='Longly'
where FirstName='Mark' and LastName='Long'

select * from TrigEx

--Simple Transaction and Locking Example

BEGIN TRANSACTION ML
update Person.Person
set LastName = 'Long'

ROLLBACK TRANSACTION ML

COMMIT TRANSACTION ML

select * from Person.Person

begin tran mtl
update Person.Person
set FirstName='Jack'
where FirstName='Terri'  and LastName='Duffy'

select * from Person.Person
where BusinessEntityID=2

--FOR XML RAW
select top 3 FirstName, LastName from Person.Person

for xml raw('Employee'), elements, root ('Employees')

--FOR XML AUTO

select top 3 FirstName, LastName from Person.Person

FOR XML AUTO, ELEMENTS, XMLSCHEMA('TestSchema')


--FOR XML PATH

select top 3 BusinessEntityID as[@id], FirstName , LastName from Person.Person
FOR XML PATH  ('Employee'), ROOT ('Employees')


