using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LabTestPortal.Data.Helpers;
using ShortBus;

namespace LabTestPortal.Data.Commands
{
    public class SaveStateCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public string StateCode { get; set; }
    }

    public class SaveStateCommandHandler : IRequestHandler<SaveStateCommand, bool>
    {
        public bool Handle(SaveStateCommand request)
        {
            var conn = new SqlConnection();
            using (conn)
            {
                using (var cmd = new SqlCommand("uspStateUpsert", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = request.Id;
                    cmd.Parameters.Add("@stateCode", SqlDbType.NVarChar).Value = request.StateCode;
                    ConnectionStringHelper.OpenConnection(conn);
                    cmd.ExecuteNonQuery();
                }
            }
            return true;
        }
    }
}
