Use labtestportal
Go
Create Procedure uspStateUpsert
@id int,
@stateCode char(2)
As
IF (@id = 0)
	Insert into states 
		(state_code) values 
		(@stateCode);
ELSE
	Update states set 
		state_code = @stateCode
	where state_id = @id;
Go