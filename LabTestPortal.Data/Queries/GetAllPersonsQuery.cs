using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using LabTestPortal.Data.DTO;
using LabTestPortal.Data.Helpers;
using ShortBus;

namespace LabTestPortal.Data.Queries
{
    public class GetAllPersonsQuery : IRequest<List<PersonDto>>
    {
        public string Search { get; set; }
    }

    public class GetAllPersonsQueryHandler : IRequestHandler<GetAllPersonsQuery, List<PersonDto>>
    {
        public List<PersonDto> Handle(GetAllPersonsQuery request)
        {
            if (string.IsNullOrEmpty(request.Search) || string.IsNullOrWhiteSpace(request.Search))
            {
                request.Search = "";
            }

            var listOfPersons = new List<PersonDto>();

            var conn = new SqlConnection();

            using (conn)
            {
                using (var cmd = new SqlCommand("uspPersonSearch", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@searchString", SqlDbType.NVarChar).Value = request.Search;
                    ConnectionStringHelper.OpenConnection(conn);
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var person = new PersonDto
                        {
                            Id = int.Parse(reader["person_id"].ToString()),
                            FirstName = reader["first_name"].ToString(),
                            LastName = reader["last_name"].ToString(),
                            Gender = reader["gender"].ToString(),
                            DateOfBirth = DateTime.Parse(reader["dob"].ToString()),
                            State = new StateDto
                            {
                                Id = int.Parse(reader["state_id"].ToString()),
                                StateCode = reader["state_code"].ToString()
                            }
                        };

                        listOfPersons.Add(person);
                    }
                    conn.Close();
                }
            }

            return listOfPersons;
        }
    }
}
