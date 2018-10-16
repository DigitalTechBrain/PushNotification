create table SignalRDemo
(
Sno int identity(1,1) not null,
Sname varchar(max),
Sprice int,
Quantity int
)

insert into SignalRDemo
values('Mack',55,99)

select * from SignalRDemo