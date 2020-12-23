using BrunoTragl.Indra.GestaoUsuario.Data;
using BrunoTragl.Indra.GestaoUsuario.Data.Base;
using BrunoTragl.Indra.GestaoUsuario.Data.Base.Interfaces;
using BrunoTragl.Indra.GestaoUsuario.Data.Interfaces;
using BrunoTragl.Indra.GestaoUsuario.IO;
using BrunoTragl.Indra.GestaoUsuario.IO.Interfaces;
using BrunoTragl.Indra.GestaoUsuario.Presenter;
using BrunoTragl.Indra.GestaoUsuario.Presenter.Interafaces;
using Ninject.Modules;

namespace BrunoTragl.Indra.GestaoUsuario.IOC
{
    public class ApplicationModule : NinjectModule
    {
        public override void Load()
        {
            Bind(typeof(IConnectionSettings)).To(typeof(ConnectionSettings)).InTransientScope();
            Bind(typeof(IUsuarioPresenter)).To(typeof(UsuarioPresenter)).InTransientScope();
            Bind(typeof(IPerfilUsuarioPresenter)).To(typeof(PerfilUsuarioPresenter)).InTransientScope();
            Bind(typeof(IStatusPresenter)).To(typeof(StatusPresenter)).InTransientScope();
            Bind(typeof(IPerfilUsuarioData)).To(typeof(PerfilUsuarioData)).InTransientScope();
            Bind(typeof(IStatusData)).To(typeof(StatusData)).InTransientScope();
            Bind(typeof(IUsuarioData)).To(typeof(UsuarioData)).InTransientScope();
            Bind(typeof(IStatusManipulation)).To(typeof(StatusManipulation)).InTransientScope();
            Bind(typeof(IUsuarioPerfilManipulation)).To(typeof(UsuarioPerfilManipulation)).InTransientScope();
        }
    }
}
