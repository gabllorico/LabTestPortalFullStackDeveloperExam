using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LabTestPortal.Data.DTO;

namespace LabTestPortal.Data.Helpers
{
    public class PersonHelper
    {
        public static SelectedPersonDto NewPerson(List<StateDto> states)
        {
            var person = new SelectedPersonDto
            {
                FirstName = "",
                Gender = "",
                Id = 0,
                LastName = "",
                States = new List<PersonStateDto>(),
                StateId = 0
            };

            foreach (var state in states)
            {
                person.States.Add(new PersonStateDto
                {
                    Id = state.Id,
                    Selected = false,
                    StateCode = state.StateCode
                });
            }

            return person;
        }
    }
}
