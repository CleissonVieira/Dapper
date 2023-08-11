using Dapper;
using eCommerce.API.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;

namespace eCommerce.API.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private IDbConnection _connection;
        public UsuarioRepository()
        {
            _connection = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=eCommerce;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
        }


        private static List<Usuario> _db = new()
        {
            new Usuario(){ Id = 1, Nome = "Nome usuario 1", Email = "email.usuario1@gmail.com" },
            new Usuario(){ Id = 2, Nome = "Nome usuario 2", Email = "email.usuario2@gmail.com" },
            new Usuario(){ Id = 3, Nome = "Nome usuario 3", Email = "email.usuario3@gmail.com" },
            new Usuario(){ Id = 4, Nome = "Nome usuario 4", Email = "email.usuario4@gmail.com" },
        };

        public List<Usuario> Get()
        {
            return _connection.Query<Usuario>("SELECT * FROM USUARIOS").ToList();
        }

        public Usuario Get(int id)
        {
            return _db.FirstOrDefault(x => x.Id.Equals(id));
        }

        public void Insert(Usuario usuario)
        {
            var ultimoUsuario = _db.LastOrDefault();

            if (ultimoUsuario == null)
                usuario.Id = 1;
            else
                usuario.Id = ultimoUsuario.Id+1;

            _db.Add(usuario);
        }

        public void Update(Usuario usuario)
        {
            _db.Remove(_db.FirstOrDefault(x => x.Id.Equals(usuario.Id)));
            _db.Add(usuario);
        }

        public void Delete(int id)
        {
            _db.Remove(_db.FirstOrDefault(x => x.Id.Equals(id)));
        }
    }
}
