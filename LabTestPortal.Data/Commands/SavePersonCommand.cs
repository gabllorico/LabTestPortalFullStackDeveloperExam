using System;
using System.Data;
using System.Data.SqlClient;
using LabTestPortal.Data.Helpers;
using ShortBus;

namespace LabTestPortal.Data.Commands
{
    public class SavePersonCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int StateId { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
    }

    public class SavePersonCommandHandler : IRequestHandler<SavePersonCommand, bool>
    {
        public bool Handle(SavePersonCommand request)
        {
            var conn = new SqlConnection();
            using (conn)
            {
                using (var cmd = new SqlCommand("uspPersonUpsert", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = request.Id;
                    cmd.Parameters.Add("@firstName", SqlDbType.NVarChar).Value = request.FirstName;
                    cmd.Parameters.Add("@lastName", SqlDbType.NVarChar).Value = request.LastName;
                    cmd.Parameters.Add("@stateId", SqlDbType.Int).Value = request.StateId;
                    cmd.Parameters.Add("@gender", SqlDbType.Char).Value = request.Id;
                    cmd.Parameters.Add("@dateOfBirth", SqlDbType.DateTime).Value = request.DateOfBirth;
                    ConnectionStringHelper.OpenConnection(conn);
                    cmd.ExecuteNonQuery();
                }
            }
            return true;
        }
    }
}
