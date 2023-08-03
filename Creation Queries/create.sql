
CREATE TABLE [Students] (
	ID varchar (10) NOT NULL,
	first_name varchar(20) NOT NULL,
	last_name varchar(20) NOT NULL,
	gender varchar(1) NOT NULL,
	batch integer NOT NULL,
	area_ID integer NOT NULL,
  CONSTRAINT [PK_STUDENTS] PRIMARY KEY CLUSTERED
  (
  [ID] ASC
  ) WITH (IGNORE_DUP_KEY = OFF)

)
GO
CREATE TABLE [Areas] (
	area_ID integer NOT NULL,
	area_name varchar(255) NOT NULL,
  CONSTRAINT [PK_AREAS] PRIMARY KEY CLUSTERED
  (
  [area_ID] ASC
  ) WITH (IGNORE_DUP_KEY = OFF)

)
GO
CREATE TABLE [Schedule] (
	student_ID varchar(10) NOT NULL,
	[day] varchar(10) NOT NULL,
	in_time integer NOT NULL,
	out_time integer NOT NULL,
  CONSTRAINT [PK_SCHEDULE] PRIMARY KEY CLUSTERED
  (
  [student_ID]ASC,
  [day] 
  ) WITH (IGNORE_DUP_KEY = OFF)

)
GO
CREATE TABLE [TransportModes] (
	TM_ID integer NOT NULL,
	vehicle varchar(255) NOT NULL,
	capacity integer NOT NULL,
  CONSTRAINT [PK_TRANSPORTMODES] PRIMARY KEY CLUSTERED
  (
  [TM_ID] ASC
  ) WITH (IGNORE_DUP_KEY = OFF)

)
GO
CREATE TABLE [Connected_Areas] (
	student_ID varchar(10) NOT NULL,
	area_ID integer NOT NULL,
  CONSTRAINT [PK_CONNECTED_AREAS] PRIMARY KEY CLUSTERED
  (
  [student_ID] ASC
  ) WITH (IGNORE_DUP_KEY = OFF)

)
GO
CREATE TABLE [Preferences] (
	pref_ID integer NOT NULL,
	student_ID varchar(10) NOT NULL,
	gender_pref varchar(1) NOT NULL,
  CONSTRAINT [PK_PREFERENCES] PRIMARY KEY CLUSTERED
  (
  [pref_ID] ASC
  ) WITH (IGNORE_DUP_KEY = OFF)

)
GO
CREATE TABLE [Student_Transport_Modes] (
	student_ID varchar(10) NOT NULL,
	TM_ID integer,
	is_owner bit NOT NULL,
  CONSTRAINT [PK_STUDENT_TRANSPORT_MODES] PRIMARY KEY CLUSTERED
  (
  [student_ID] ASC
  ) WITH (IGNORE_DUP_KEY = OFF)

)
GO
CREATE TABLE [ValidTimes] (
	time_id integer NOT NULL,
	[hour] varchar(5) NOT NULL,
	[minutes] varchar(5) NOT NULL,
  CONSTRAINT [PK_VALIDTIMES] PRIMARY KEY CLUSTERED
  (
  [time_id] ASC
  ) WITH (IGNORE_DUP_KEY = OFF)

)
GO
CREATE TABLE [Assigments] (
	student1_ID varchar(10) NOT NULL,
	student2_ID varchar(10) NOT NULL,
  CONSTRAINT [PK_ASSIGMENTS] PRIMARY KEY CLUSTERED
  (
  [student1_ID] ASC
  ) WITH (IGNORE_DUP_KEY = OFF)

)
GO

create table result (group_id int, id int, [day] varchar(5), std_id varchar(10)
constraint [pk_result] primary key clustered
	(
		[group_id] asc,
		[day] asc,
		[id] asc,
		[std_id] asc
	) with (ignore_dup_key = off)
)
GO

create table assignments(id int, std_id varchar(10)
	constraint [pk_assignments] primary key clustered
	(
		[id] asc,
		[std_id] asc
	) with (ignore_dup_key = off)
)
GO

create table assignment_info (id int, area_id int, tmode_id int
constraint [pk_assignment_info] primary key clustered
	(
		[id] asc
	) with (ignore_dup_key = off)
)

ALTER TABLE [Students] WITH CHECK ADD CONSTRAINT [Students_fk0] FOREIGN KEY ([area_ID]) REFERENCES [Areas]([area_ID])
ON UPDATE CASCADE
GO
ALTER TABLE [Students] CHECK CONSTRAINT [Students_fk0]
GO


ALTER TABLE [Schedule] WITH CHECK ADD CONSTRAINT [Schedule_fk0] FOREIGN KEY ([student_ID]) REFERENCES [Students]([ID])
ON UPDATE CASCADE
GO
ALTER TABLE [Schedule] CHECK CONSTRAINT [Schedule_fk0]
GO




ALTER TABLE [Connected_Areas] WITH CHECK ADD CONSTRAINT [Connected_Areas_fk0] FOREIGN KEY ([student_ID]) REFERENCES [Students]([ID])
ON UPDATE CASCADE
GO
ALTER TABLE [Connected_Areas] CHECK CONSTRAINT [Connected_Areas_fk0]
GO

ALTER TABLE [Preferences] WITH CHECK ADD CONSTRAINT [Preferences_fk0] FOREIGN KEY ([student_ID]) REFERENCES [Students]([ID])
ON UPDATE CASCADE
GO
ALTER TABLE [Preferences] CHECK CONSTRAINT [Preferences_fk0]
GO

ALTER TABLE [Student_Transport_Modes] WITH CHECK ADD CONSTRAINT [Student_Transport_Modes_fk0] FOREIGN KEY ([student_ID]) REFERENCES [Students]([ID])
ON UPDATE CASCADE
GO
ALTER TABLE [Student_Transport_Modes] CHECK CONSTRAINT [Student_Transport_Modes_fk0]
GO
ALTER TABLE [Student_Transport_Modes] WITH CHECK ADD CONSTRAINT [Student_Transport_Modes_fk1] FOREIGN KEY ([TM_ID]) REFERENCES [TransportModes]([TM_ID])
ON UPDATE CASCADE
GO
ALTER TABLE [Student_Transport_Modes] CHECK CONSTRAINT [Student_Transport_Modes_fk1]
GO

