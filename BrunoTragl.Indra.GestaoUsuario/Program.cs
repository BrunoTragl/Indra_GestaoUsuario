using BrunoTragl.Indra.GestaoUsuario.IOC;
using BrunoTragl.Indra.GestaoUsuario.Presenter.Interafaces;
using System;

namespace BrunoTragl.Indra.GestaoUsuario
{
    public class Program
    {
        private static IUsuarioPresenter _usuarioPresenter;
        private static IStatusPresenter _statusPresenter;
        private static IPerfilUsuarioPresenter _perfilUsuarioPresenter;
        
        public static void InitializeDependencies()
        {
            DependencyInjectionConfiguration.Wire(new ApplicationModule());
            _usuarioPresenter = DependencyInjectionConfiguration.Resolve<IUsuarioPresenter>();
            _statusPresenter = DependencyInjectionConfiguration.Resolve<IStatusPresenter>();
            _perfilUsuarioPresenter = DependencyInjectionConfiguration.Resolve<IPerfilUsuarioPresenter>();
        }

        public static void Main(string[] args)
        {
            try
            {
                InitializeDependencies();

                _statusPresenter.ImportarStatus();
                _perfilUsuarioPresenter.ImportarPerfis();
                _usuarioPresenter.ImportarUsuarios();

                _usuarioPresenter.ListarUsuariosComIdImpar();
                _usuarioPresenter.AlterarNomeDeUsuariosComIniciaisSr();
                _usuarioPresenter.ListarUsuariosComPerfilAdministrador();
                _usuarioPresenter.QuantidadeUsuariosInativos();
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("Ocorreu um erro e a seguinte exceção foi lançada:");
                Console.WriteLine($"Exception Message: {ex.Message}");
                Console.WriteLine($"Exception StackTrace: {ex.StackTrace}");
                Console.WriteLine($"Exception Source: {ex.Source}");
                Console.WriteLine($"Exception TargetSite: {ex.TargetSite}");
                Console.ReadKey();
            }
        }
    }
}