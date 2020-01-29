using Beauty.Barry.Application;
using Beauty.Barry.Domain.Department;
using Beauty.Barry.Infrastructure.DaoMongoDB;
using Beauty.Dick.Helpers.Builders.Interface;
using Beauty.Dick.Helpers.Builders.Interface.Impl;
using LightInject;


namespace Beauty.Barry.Api
{
    public class CompositionRoot : ICompositionRoot
    {
        public void Compose(IServiceRegistry serviceRegistry)
        {
            serviceRegistry.Register<IDepartmentApplication, DepartmentApplication>(new PerContainerLifetime());
            serviceRegistry.Register<IDepartmentRepository, DepartmentDao>(new PerContainerLifetime());
            
            serviceRegistry.Register<ICodeBuilder, SequencialFromDbCodeBuilder>(new PerContainerLifetime());
            serviceRegistry.Register<ICodeBuilderRepository, CodeBuilderDao>(new PerContainerLifetime());
        }
    }
}