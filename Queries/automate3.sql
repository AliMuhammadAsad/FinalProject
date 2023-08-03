--drop procedure Automate
create procedure Automate
AS
exec Groupby_Area_Tmode;
drop table if exists temp_table; delete result;
declare @grpid int;
declare @intime int
declare @outtime int;
declare @tmode_capacity int;
declare @id int;
set @id = 1;
set @intime = (select min(time_id) from ValidTimes); set @outtime = (select max(time_id) from ValidTimes); 
set @grpid = (select min(id) from assignment_info);

create table temp_table(std_id varchar(10));
while(@grpid is not null)
begin
	set @tmode_capacity = (select capacity from TransportModes where TM_ID = (select tmode_id from assignment_info where id = @grpid));
	
	exec dosomething @grp_id = @grpid, @day = 'M', @capacity = @tmode_capacity, @maxHourDiff = 4;
	insert into result select @grpid as "grpid", id, [day], std_id from temp;
	delete temp;
	exec dosomething @grp_id = @grpid, @day = 'T', @capacity = @tmode_capacity, @maxHourDiff = 4;
	insert into result select @grpid as "grpid", id, [day], std_id from temp;
	delete temp;
	exec dosomething @grp_id = @grpid, @day = 'W', @capacity = @tmode_capacity, @maxHourDiff = 4;
	insert into result select @grpid as "grpid", id, [day], std_id from temp;
	delete temp;
	exec dosomething @grp_id = @grpid, @day = 'R', @capacity = @tmode_capacity, @maxHourDiff = 4;
	insert into result select @grpid as "grpid", id, [day], std_id from temp;
	delete temp;
	exec dosomething @grp_id = @grpid, @day = 'F', @capacity = @tmode_capacity, @maxHourDiff = 4;
	insert into result select @grpid as "grpid", id, [day], std_id from temp;
	delete temp;
	set @grpid = (select min(id) from assignment_info where id > @grpid);
	select * from assignment_info where id = @grpid;

end
drop table if exists temp_table;
GO
exec Automate;
select * from result;
--select * from assignment_info
Select Id, first_name + ' ' + last_name from students where id in (select std_id from result where id = 2 AND [day] = 'M' AND group_id = 1)
