using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LabTestPortal.Data.Commands;
using LabTestPortal.Data.DTO;
using LabTestPortal.Data.Queries;
using ShortBus;

namespace LabTestPortal.Controllers
{
    public class StateController : BaseController
    {
        private readonly IMediator _mediator;

        public StateController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public ActionResult Index()
        {
            var request = _mediator.Request(new GetAllStatesQuery());
            return View(request.Data);
        }

        public ActionResult Add()
        {
            var state = new StateDto();
            
            return View(state);
        }

        public ActionResult Edit(int id)
        {
            var request = _mediator.Request(new GetSelectedStateQuery
            {
                Id = id
            });

            if (request.HasException())
            {
                Alert(request.Exception.GetBaseException().Message);
                return RedirectToAction("Index", "State");
            }

            return View(request.Data);
        }

        public ActionResult Save(StateDto state)
        {
            var request = _mediator.Request(new SaveStateCommand
            {
                StateCode = state.StateCode,
                Id = state.Id
            });

            if (request.HasException())
            {
                Alert(request.Exception.GetBaseException().Message);
            }
            else
            {
                Flash($"{state.StateCode} successfully saved");
            }

            return RedirectToAction("Index", "State");
        }
    }
}