Create View AssignedGroups AS
select r.group_id as "Group ID", r.[day] as "Day", r.id as "Assigned ID", TM.vehicle as "Vehicle Name", ar.area_name as "Area Name" from result as r, assignment_info as a, Areas as ar, TransportModes as TM
where r.group_id = a.id
AND a.area_id = ar.area_ID
AND a.tmode_id = TM.TM_ID
group by r.id, r.[day], r.group_id, TM.vehicle, ar.area_name;


set identity_insert students off;
insert into students(id, first_name,last_name,gender,batch,area_ID) values ('be08824','Burhanuddin', 'Aliasghar', 'M', 2025, 74700);

select max(id) from students

insert into Student_Transport_Modes


select * from Student_Transport_Modes where Student_Transport_Modes.student_ID = 'be08824';

select vehicle, capacity from Student_Transport_Modes as S, TransportModes as T where S.TM_id = T.TM_ID AND S.student_id = 'be08824';

create procedure Del_student @std_id varchar(10) AS
BEGIN

Delete from Student_Transport_Modes
where student_ID = @std_id;
Delete from Schedule
where student_ID = @std_id;
Delete from Students
where ID = @std_id;
END

drop procedure Del_student

select * from Schedule where student_id = 'be06624';

select * from students;

create function get_schedules (@id varchar (10))
RETURNS TABLE
AS
RETURN
select S1.[day], (V1.[hour] + ':' + V2.[minutes]) as intime, (V2.[hour] + ':' + V2.[minutes]) as outtime from ValidTimes as V1, ValidTimes as V2, Schedule as S1, schedule as S2
where S1.in_time = V1.time_id
AND S2.out_time = V2.time_id
AND S1.student_ID = S2.student_ID
AND S1.[day] = S2.[day]
AND S2.student_ID = @id
Group by V1.[hour], V1.[minutes], V2.[hour], V2.[minutes], S1.[day];

exec Del_student @std_id = 'aa01183';

select * from get_schedules('aa01183');

drop function get_schedules