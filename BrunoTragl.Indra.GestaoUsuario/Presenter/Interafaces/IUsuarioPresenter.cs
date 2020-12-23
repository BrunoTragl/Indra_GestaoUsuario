namespace BrunoTragl.Indra.GestaoUsuario.Presenter.Interafaces
{
    public interface IUsuarioPresenter
    {
        void ImportarUsuarios();
        void ListarUsuariosComIdImpar();
        void AlterarNomeDeUsuariosComIniciaisSr();
        void ListarUsuariosComPerfilAdministrador();
        void QuantidadeUsuariosInativos();
    }
}
