select *
from Clients
full outer join Persons
on Clients.idClient = Persons.idClient
where Persons.idClient is not null

select *
from Clients
full outer join Entity
on Clients.idClient = Entity.idClient
where Entity.idClient is not null


select *
from Logs
full outer join Clients
on Logs.idReciever = Clients.idClient
where Logs.idLog is not null

select *
from Clients


select *
from Logs
full outer join Clients c1
on Logs.idReciever = c1.idClient
full outer join Clients c2
on Logs.idClient = c2.idClient
where Logs.idLog is not null


select *
from Clients
full outer join Persons
on Clients.idClient = Persons.idClient
full outer join Entity
on Clients.idClient = Entity.idClient

update Clients set idClient =  '5acd' where idClient =   '6'
update Persons set idClient =  '5acd' where idClient =   '6'
update Entity set idClient =   '5acd' where idClient =   '6'
update Logs set idClient =	   '5acd' where idClient =   '6'
update Logs set idReciever =   '5acd' where idReciever = '6'
update Deposits set idClient = '5acd' where idClient =   '6'

delete Clients where idClient = '835d'
delete Persons where idClient = '835d'
delete Entity where idClient =  '835d'

update Deposits 
set amount = 1
where idClient = 'a'


insert into Logs ([idClient], [time], [idReciever], [amount], [direction], [message]) VALUES (N'asda', N'2010-05-07 18:45:29', NULL, 1000, 1, NULL)
select * from Logs
delete Logs where idLog = 13 or idLog = 14

insert into Deposits ([idClient], [amount], [percent], [isCapitalised]) Values (N'asda', 123, 1.1, 0)

select * from Deposits

update Deposits
set amount = 1243, isCapitalised = 1234, [percent] = 1.2
where idClient = 'asda'

select * from Deposits