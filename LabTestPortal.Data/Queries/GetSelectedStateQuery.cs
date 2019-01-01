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
    public class GetSelectedStateQuery : IRequest<StateDto>
    {
        public int Id { get; set; }
    }

    public class GetSelectedStateQueryHandler : IRequestHandler<GetSelectedStateQuery, StateDto>
    {
        public StateDto Handle(GetSelectedStateQuery request)
        {
            var state = new StateDto();

            var conn = new SqlConnection();

            using (conn)
            {
                using (var cmd = new SqlCommand("uspStateSelected", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = request.Id;
                    ConnectionStringHelper.OpenConnection(conn);
                    var reader = cmd.ExecuteReader();
                    if (!reader.HasRows)
                    {
                        throw new Exception("State not found");
                    }
                    while (reader.Read())
                    {
                        state.Id = int.Parse(reader["state_id"].ToString());
                        state.StateCode = reader["state_code"].ToString();
                    }
                    conn.Close();
                }
            }

            return state;
        }
    }
}
