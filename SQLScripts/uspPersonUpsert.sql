Use labtestportal
Go
Create Procedure uspPersonUpsert
@id int,
@firstName nvarchar(50),
@lastName nvarchar(50),
@stateId int,
@gender char(1),
@dateOfBirth datetime
As
IF (@id = 0)
	Insert into person 
		(first_name, last_name, state_id, gender, dob) values 
		(@firstName, @lastName, @stateId, @gender, @dateOfBirth);
ELSE
	Update person set 
		first_name = @firstName, 
		last_name = @lastName, 
		state_id = @stateId, 
		gender = @gender, 
		dob = @dateOfBirth 
	where person_id = @id;
Go