using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LabTestPortal.Data.DTO;
using LabTestPortal.Data.Helpers;
using ShortBus;

namespace LabTestPortal.Data.Queries
{
    public class GetSelectedPersonQuery : IRequest<SelectedPersonDto>
    {
        public int Id { get; set; }
    }

    public class GetSelectedPersonQueryHandler : IRequestHandler<GetSelectedPersonQuery, SelectedPersonDto>
    {
        public SelectedPersonDto Handle(GetSelectedPersonQuery request)
        {
            var person = new SelectedPersonDto
            {
                States = new List<PersonStateDto>()
            };
            var conn = new SqlConnection();

            using (conn)
            {
                using (var cmd = new SqlCommand("[uspPersonSelected]", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = request.Id;
                    ConnectionStringHelper.OpenConnection(conn);
                    var reader = cmd.ExecuteReader();
                    if (!reader.HasRows)
                    {
                        throw new Exception("Person not found");
                    }
                    while (reader.Read())
                    {
                        person.Id = int.Parse(reader["person_id"].ToString());
                        person.FirstName = reader["first_name"].ToString();
                        person.LastName = reader["last_name"].ToString();
                        person.Gender = reader["gender"].ToString();
                        person.DateOfBirth = DateTime.Parse(reader["dob"].ToString());
                        person.StateId = int.Parse(reader["state_id"].ToString());
                        person.States.Add(new PersonStateDto
                        {
                            StateCode = reader["state_code"].ToString(),
                            Id = int.Parse(reader["state_id"].ToString()),
                            Selected = true
                        });
                    }
                    conn.Close();
                }

                using (var cmdForState = new SqlCommand("uspStatesList", conn))
                {
                    var listOfStates = new List<StateDto>();
                    cmdForState.CommandType = CommandType.StoredProcedure;
                    ConnectionStringHelper.OpenConnection(conn);
                    var reader = cmdForState.ExecuteReader();
                    while (reader.Read())
                    {
                        if (person.States[0].Id != int.Parse(reader["state_id"].ToString()))
                        {
                            var state = new PersonStateDto
                            {
                                Id = int.Parse(reader["state_id"].ToString()),
                                StateCode = reader["state_code"].ToString(),
                                Selected = false
                            };

                            person.States.Add(state);
                        }
                    }
                    conn.Close();
                }
            }

            return person;
        }
    }
}
