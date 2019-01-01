using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using LabTestPortal.Data.DTO;
using LabTestPortal.Data.Helpers;
using ShortBus;

namespace LabTestPortal.Data.Queries
{
    public class GetAllStatesQuery : IRequest<List<StateDto>>
    {
    }

    public class GetAllStatesQueryHandler : IRequestHandler<GetAllStatesQuery, List<StateDto>>
    {
        public List<StateDto> Handle(GetAllStatesQuery request)
        {
            var listOfStates = new List<StateDto>();

            var conn = new SqlConnection();

            using (conn)
            {
                using (var cmd = new SqlCommand("uspStatesList", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    ConnectionStringHelper.OpenConnection(conn);
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var state = new StateDto
                        {
                            Id = int.Parse(reader["state_id"].ToString()),
                            StateCode = reader["state_code"].ToString()
                        };
                        listOfStates.Add(state);
                    }
                    conn.Close();
                }
            }

            if (listOfStates.Count == 0)
            {
                throw new Exception("Add states first.");
            }

            return listOfStates;
        }
    }
}
