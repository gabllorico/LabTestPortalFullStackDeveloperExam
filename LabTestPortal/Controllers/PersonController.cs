using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LabTestPortal.Data.Commands;
using LabTestPortal.Data.DTO;
using LabTestPortal.Data.Helpers;
using LabTestPortal.Data.Queries;
using ShortBus;

namespace LabTestPortal.Controllers
{
    public class PersonController : BaseController
    {
        private readonly IMediator _mediator;

        public PersonController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public ActionResult Index()
        {
            var request = _mediator.Request(new GetAllPersonsQuery());
            return View(request.Data);
        }

        public ActionResult SearchResult(string search)
        {
            var request = _mediator.Request(new GetAllPersonsQuery
            {
                Search = search
            });
            ViewBag.Search = search;
            return View(request.Data);
        }

        public ActionResult Add()
        {
            var request = _mediator.Request(new GetAllStatesQuery());
            if (request.HasException())
            {
                Alert(request.Exception.GetBaseException().Message);
                return RedirectToAction("Index", "Person");
            }
            if (request.Data.Count == 0)
            {
                Alert("Add states first");
                return RedirectToAction("Index", "Person");
            }

            var newPerson = PersonHelper.NewPerson(request.Data);
            return View(newPerson);
        }

        public ActionResult Edit(int id)
        {
            var request = _mediator.Request(new GetSelectedPersonQuery
            {
                Id = id
            });

            if (request.HasException())
            {
                Alert(request.Exception.GetBaseException().Message);
                return RedirectToAction("Index", "Person");
            }

            return View(request.Data);
        }

        public ActionResult Save(SelectedPersonDto person)
        {
            var request = _mediator.Request(new SavePersonCommand
            {
                DateOfBirth = person.DateOfBirth,
                Id = person.Id,
                FirstName = person.FirstName,
                Gender = person.Gender,
                LastName = person.LastName,
                StateId = person.StateId
            });

            if (request.HasException())
            {
                Alert(request.Exception.GetBaseException().Message);
            }
            else
            {
                Flash($"{person.FullName} successfully saved");
            }
            
            return RedirectToAction("Index", "Person");
        }
    }
}