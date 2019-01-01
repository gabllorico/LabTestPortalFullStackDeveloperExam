Use labtestportal
Go
Create Procedure [uspPersonSelected]
@id int
As
Select TOP 1 * from person inner join states on person.state_id = states.state_id  
				where person_id = @id;