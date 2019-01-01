using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Castle.MicroKernel.Registration;
using LabTestPortal.Data;
using LabTestPortal.Data.Queries;
using ShortBus;

namespace LabTestPortal.App_Start.Installers
{
    public class CommandQueryInstaller : IWindsorInstaller
    {
        public void Install(Castle.Windsor.IWindsorContainer container, Castle.MicroKernel.SubSystems.Configuration.IConfigurationStore store)
        {
            DependencyResolver.SetResolver(new ShortBus.Windsor.WindsorDependencyResolver(container));

            container.Register(Component.For<IDependencyResolver>().Instance(DependencyResolver.Current));

            container.Register(Component.For(typeof(IMediator)).ImplementedBy(typeof(LabTestPortalMediator)));

            container.Register(Classes.FromAssembly(typeof(GetAllPersonsQuery).Assembly)
                .BasedOn
                (
                    typeof(IRequestHandler<,>),
                    typeof(IAsyncRequestHandler<,>),
                    typeof(INotificationHandler<>),
                    typeof(IAsyncNotificationHandler<>)
                ).WithService.Base()
                .LifestylePerWebRequest()
            );
        }
    }
}