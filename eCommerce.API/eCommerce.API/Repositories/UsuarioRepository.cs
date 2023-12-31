﻿using Dapper;
using eCommerce.API.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace eCommerce.API.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private IDbConnection _connection;
        public UsuarioRepository()
        {
            _connection = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=eCommerce;Integrated Security=True;Connect Timeout=30;Encrypt=False;");
        }

        public List<Usuario> Get()
        {
            return _connection.Query<Usuario>("SELECT * FROM USUARIOS").ToList();
        }

        public Usuario Get(int id)
        {
            return _connection.Query<Usuario, Contato, Usuario>(
                "SELECT * FROM Usuarios U LEFT JOIN Contatos C ON C.UsuarioId=U.Id WHERE U.Id = @Id",
                (usuario, contato) =>
                {
                    usuario.Contato = contato;
                    return usuario;
                },
                new { Id = id }
            ).SingleOrDefault();
        }

        public void Insert(Usuario usuario)
        {
            string sql = "INSERT INTO Usuarios (Nome, Email, Sexo, RG, CPF, NomeMae, SituacaoCadastro, DataCadastro) VALUES (@Nome, @Email, @Sexo, @RG, @CPF, @NomeMae, @SituacaoCadastro, @DataCadastro); SELECT CAST(SCOPE_IDENTITY() AS INT);";
            _ = _connection.Query<int>(sql, usuario).Single();
        }

        public void Update(Usuario usuario)
        {
            string sql = "UPDATE Usuarios SET Nome = @Nome, Email = @Email, Sexo = @Sexo, RG = @RG, CPF = @CPF, NomeMae = @NomeMae, SituacaoCadastro = @SituacaoCadastro, DataCadastro = @DataCadastro WHERE Id = @Id";
            _connection.Execute(sql, usuario);
        }

        public void Delete(int id)
        {
            _connection.Execute("DELETE FROM Usuarios WHERE Id = @Id", new { Id = id });
        }
    }
}
