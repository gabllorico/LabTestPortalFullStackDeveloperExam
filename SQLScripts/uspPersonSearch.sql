Use labtestportal
Go
Create Procedure uspPersonSearch
@searchString nvarchar(50)
As
Select * from person inner join states on person.state_id = states.state_id where first_name like '%'+@searchString+'%' or last_name like '%'+@searchString+'%';
Go