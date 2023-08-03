--drop procedure if exists dosomething;
create procedure dosomething @grp_id int, @day varchar(5), @capacity int, @maxHourDiff int
AS
drop table if exists tab1; drop table if exists tab2; drop table if exists temp;
declare @intime int, @outtime int;
declare @id int;
create table temp (id int, [day] varchar(5), std_id varchar(10)
constraint [pk_temp] primary key clustered
	(
		[id] asc,
		[day] asc,
		[std_id] asc
	) with (ignore_dup_key = off)
);
create table tab1 (std_id varchar(10));
create table tab2 (std_id varchar(10));
set @outtime = 16
set @intime = 1;
set @id = 1;
delete tab1;
while(@outtime < 22)
begin
	set @intime = 1;
	while(@intime < 10)
	begin
			delete tab2;
			with cte as
			(select top (@capacity) std_id from assignments as a, schedule as s where a.id = @grp_id 
			AND  a.std_id = s.student_ID
			AND s.[day] = @day 
			AND ABS(@intime - s.in_time) < @maxHourDiff AND ABS(@outtime - s.out_time) < @maxHourDiff
			AND a.std_id not in (select std_id from tab1))
			insert into tab2 select * from cte;
			insert into tab1 select * from tab2;
			if ((select count(*) from tab2) = (@capacity))
			begin
				insert into temp select @id, @day, std_id from tab2;
				set @id = @id + 1;
			end
			else if ((select count(*) from tab2) >= 2)
			begin
				set @intime = @intime + 1
				insert into temp select @id, @day, std_id from tab2;
				set @id = @id + 1;
			end
			else
			begin
				set @intime = @intime + 1;
			end
	end
	set @outtime = @outtime + 1;
end
GO
--select * from temp
--drop table tab1
--exec dosomething @grp_id = 1, @day = 'M', @capacity = 4, @maxHourDiff = 4;
--drop procedure dosomething