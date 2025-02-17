﻿using SistemaFinanceiro.DataBase;
using SistemaFinanceiro.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SistemaFinanceiro.Repositório
{
    public class RepositorioCategoria
    {

        public int CadastrarCategoria(Categoria categorias)
        {

            SqlCommand comando = new DBconnection().GetConnction();
            comando.CommandText = @"INSERT INTO categorias (nome) OUTPUT INSERTED.ID VALUES (@NOME)";
            comando.Parameters.AddWithValue("@NOME", categorias.Nome);
            int id = Convert.ToInt32(comando.ExecuteScalar().ToString());
            return id;

        }

        public bool AlterarCategorias(Categoria categorias)
        {

            SqlCommand comando = new DBconnection().GetConnction();
            comando.CommandText = "UPDATE categorias SET nome = @NOME WHERE id = @ID";
            comando.Parameters.AddWithValue("@ID", categorias.Id);
            comando.Parameters.AddWithValue("@NOME", categorias.Nome);

            return comando.ExecuteNonQuery() == 1;
        }

        public bool ExcluirCategoria(int id)
        {
            SqlCommand comando = new DBconnection().GetConnction();
            comando.CommandText = "DELETE FROM categorias WHERE id = @ID";
            comando.Parameters.AddWithValue("@ID", id);
            return comando.ExecuteNonQuery() == 1;
        }

        public List<Categoria> ObterTodosCategoria()
        {
            List<Categoria> categorias = new List<Categoria>();
            SqlCommand comando = new DBconnection().GetConnction();
            comando.CommandText = "SELECT id AS 'id', nome AS 'nome' FROM categorias";
            DataTable tabela = new DataTable();
            tabela.Load(comando.ExecuteReader());
            foreach (DataRow linha in tabela.Rows)
            {
                Categoria categoria = new Categoria()
                {
                    Id = Convert.ToInt32(linha["id"].ToString()),
                    Nome = linha["nome"].ToString()
                };
                categorias.Add(categoria);
            }
            return categorias;
        }

        public List<Object> ObterTodosCategoriaParaSelect2()
        {
            List<Object> registros = new List<object>();
            SqlCommand comando = new DBconnection().GetConnction();
            comando.CommandText = "SELECT id AS 'id', nome AS 'nome' FROM categorias";
            DataTable tabela = new DataTable();
            tabela.Load(comando.ExecuteReader());
            foreach (DataRow linha in tabela.Rows)
            {
                registros.Add(new
                {
                    id = Convert.ToInt32(linha["id"].ToString()),
                    text = linha["nome"].ToString()
                });
            }
            return registros;
        }

        public Categoria ObterPeloIdCategoria(int id)
        {
            Categoria categoria = null;
            SqlCommand comando = new DBconnection().GetConnction();
            comando.CommandText = "SELECT nome FROM categorias WHERE id = @ID";
            comando.Parameters.AddWithValue("@ID", id);
            DataTable tabela = new DataTable();
            tabela.Load(comando.ExecuteReader());
            if (tabela.Rows.Count == 1)
            {
                categoria = new Categoria();
                categoria.Id = id;
                categoria.Nome = tabela.Rows[0]["nome"].ToString();


            }


            return categoria;
        }


    }
}