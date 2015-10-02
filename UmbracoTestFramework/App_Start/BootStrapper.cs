namespace UmbracoTestFramework
{
    using Castle.Windsor;
    using Installers;

    public class BootStrapper
    {
        private static IWindsorContainer _container;

        public static IWindsorContainer Container
        {
            get { return _container; }
        }

        public static IWindsorContainer BootUp()
        {
            _container = new WindsorContainer();

            // Dependency injections
            _container.Install(
                new ControllersInstaller(),
                new ServiceInstaller());

            return _container;
        }

        public static void ShutDown()
        {
            _container.Dispose();
        }
    }
}