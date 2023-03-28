using Microsoft.EntityFrameworkCore;
using PracticaMvcCore2Iniciales.Data;
using PracticaMvcCore2Iniciales.Models;

namespace PracticaMvcCore2Iniciales.Repositories
{
    public class RepositoryUsuarios
    {
        private TiendaContext context;

        public RepositoryUsuarios(TiendaContext context)
        {
            this.context = context;
        }

        public async Task<Usuario> FindEmailAsync
    (string email)
        {
            Usuario user =
                await this.context.Usuarios.FirstOrDefaultAsync
                (x => x.Email == email);
            return user;
        }

        public async Task<Usuario> ExisteUsuario
            (string email, string password)
        {
            Usuario user = await this.FindEmailAsync(email);
            var usuario = await this.context.Usuarios.Where
                (x => x.Email == email && x.Password ==
                password).FirstOrDefaultAsync();
            return usuario;
        }
    }
}
