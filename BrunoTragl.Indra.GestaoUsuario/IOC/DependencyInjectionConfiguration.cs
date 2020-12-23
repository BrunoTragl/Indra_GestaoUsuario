using Ninject;
using Ninject.Modules;

namespace BrunoTragl.Indra.GestaoUsuario.IOC
{
    public static class DependencyInjectionConfiguration
    {
        private static IKernel _kernel;

        public static void Wire(INinjectModule ninjectModule)
        {
            _kernel = new StandardKernel(ninjectModule);
        }

        public static T Resolve<T>()
        {
            return _kernel.Get<T>();
        }
    }
}
