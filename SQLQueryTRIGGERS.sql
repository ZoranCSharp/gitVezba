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