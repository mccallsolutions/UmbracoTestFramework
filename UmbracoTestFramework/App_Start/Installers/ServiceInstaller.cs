namespace UmbracoTestFramework.Installers
{
    using Castle.MicroKernel.Registration;
    using Castle.MicroKernel.SubSystems.Configuration;
    using Castle.Windsor;
    using Services;
    using Services.Contracts;
    using Zone.UmbracoMapper;

    public class ServiceInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            // Castle windsor component
            container.Register(Component.For<IWindsorContainer>().Instance(container));
           
            // Services components            
            container.Register(
                Component.For<IUmbracoMapper>()
                .ImplementedBy<UmbracoMapper>()
                .LifestyleTransient());
            container.Register(
                Component.For<IUmbracoHelperService>()
                .ImplementedBy<UmbracoHelperService>()
                .LifestyleTransient());
            container.Register(
                Component.For<INodeService>()
                .ImplementedBy<NodeService>()
                .LifestyleTransient());
            container.Register(
                Component.For<IPublishedContentExtensionService>()
                    .ImplementedBy<PublishedContentExtensionService>()
                    .LifestyleTransient());
        }
    }
}