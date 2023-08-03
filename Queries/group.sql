-- for each student you have 
--drop table if exists assignments;
Create Procedure Groupby_Area_Tmode
AS
delete assignments;
delete assignment_info;

create table areagroup(id varchar(10), first_name varchar(20), last_name varchar(20), gender varchar(5), batch int, area_Id int);
begin
declare @area_val varchar(10);
declare @assignment_id int;
set @assignment_id = 1;
set @area_val = (Select min(area_ID) from Students)
declare @tmode_val int;
while(@area_val is not null)
begin
	insert into areagroup select id, first_name, last_name, gender, batch, area_ID from students where area_ID = @area_val;
	set @tmode_val = (select min(tm_id) from TransportModes);
	while(@tmode_val is not null)
	begin
		insert into assignments select @assignment_id, A.id from areagroup as A,Student_Transport_Modes as STM where A.id = STM.student_ID and STM.TM_ID = @tmode_val; 
		insert into assignment_info values (@assignment_id, @area_val, @tmode_val);
		set @assignment_id = @assignment_id + 1;
		set @tmode_val = (select min(tm_id) from TransportModes where tm_id > @tmode_val);
	end
	set @area_val = (select min(area_ID) from students where area_ID > @area_val);

	delete areagroup;
end
end
drop table areagroup;
GO

exec Groupby_Area_Tmode;
select * from assignment_info;
select * from assignments,students where assignments.std_id = students.id and assignments.id = 1;


