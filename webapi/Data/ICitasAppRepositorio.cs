using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using webapi.Helpers;
using webapi.Models;

namespace webapi.Data
{
    public interface ICitasAppRepositorio
    {
         void Agregar<T>(T entity) where T: class;
        void Eliminar<T>(T entity) where T: class;
        Task<bool> GuardarTodo();
        Task<ListaPagina<Usuario>> ObtenerUsuarios(ParametrosUsuarios parametosUsuario);
        Task<Usuario> ObtenerUsuario(int id);
        Task<Foto> ObtenerFoto(int id);
        Task<Foto> ObtenerFotoActual(int idUsario);
        Task<Like> ObtenerLike( int idUsuario, int recipienteId);

        Task<Mensajes> ObtenerMensaje(int id);
        Task<ListaPagina<Mensajes>> ObtenerMensajesPorUsuario(MensajeParams mensajeParams);
        Task<IEnumerable<Mensajes>> ObtenerMensajesLeido(int usuarioId , int remitenteId);
    }
}