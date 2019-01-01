Use labtestportal
Go
Create Procedure [uspStateSelected]
@id int
As
Select TOP 1 * from states where state_id = @id;