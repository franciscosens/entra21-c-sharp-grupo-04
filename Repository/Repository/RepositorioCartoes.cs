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
    public class RepositorioCartoes
    {

        public List<Cartoes> ObterTodosCartoes()
        {
            List<Cartoes> cartoes = new List<Cartoes>();
            SqlCommand comando = new DBconnection().GetConnction();
            comando.CommandText = "SELECT  id , id_pessoas, numero, conta, bandeira, banco FROM cartoes";
            DataTable tabela = new DataTable();
            tabela.Load(comando.ExecuteReader());
            foreach (DataRow linha in tabela.Rows)
            {
                Cartoes cartao = new Cartoes()
                {
                    Id = Convert.ToInt32(linha["id"].ToString()),
                    IdPessoas = Convert.ToInt32(linha["id_pessoas"].ToString()),
                    Numero = linha["numero"].ToString(),
                    Conta = linha["conta"].ToString(),
                    Bandeira = linha["bandeira"].ToString(),
                    Banco = linha["banco"].ToString()

                };
                cartoes.Add(cartao);
            }
            return cartoes;
        }



        public int CadastrarCartao(Cartoes cartoes)
        {
            SqlCommand comando = new DBconnection().GetConnction();
            comando.CommandText = @"INSERT INTO cartoes (numero, conta, bandeira, banco, id_pessoas) OUTPUT INSERTED.ID VALUES ( @NUMERO, @CONTA, @BANDEIRA, @BANCO, @ID_PESSOAS)";
            comando.Parameters.AddWithValue("@ID_PESSOAS", cartoes.IdPessoas);
            comando.Parameters.AddWithValue("@NUMERO", cartoes.Numero);
            comando.Parameters.AddWithValue("@CONTA", cartoes.Conta);
            comando.Parameters.AddWithValue("@BANDEIRA", cartoes.Bandeira);
            comando.Parameters.AddWithValue("@BANCO", cartoes.Banco);
            int id = Convert.ToInt32(comando.ExecuteScalar().ToString());
            return id;
        }

        public bool ExcluirCartoes(int id)
        {
            SqlCommand comando = new DBconnection().GetConnction();
            comando.CommandText = "DELETE FROM cartoes WHERE id = @ID";
            comando.Parameters.AddWithValue("@ID", id);
            return comando.ExecuteNonQuery() == 1;
        }

        public Cartoes ObterPeloIdCartoes(int id)
        {
            Cartoes cartoes = null;
            SqlCommand comando = new DBconnection().GetConnction();
            comando.CommandText = "SELECT numero, conta, bandeira, banco, id_pessoas FROM cartoes WHERE id = @ID";
            comando.Parameters.AddWithValue("@ID", id);
            DataTable tabela = new DataTable();
            tabela.Load(comando.ExecuteReader());
            if (tabela.Rows.Count == 1)
            {
                cartoes = new Cartoes();
                cartoes.Id = id;
                cartoes.IdPessoas = Convert.ToInt32(tabela.Rows[0]["id_pessoas"].ToString());
                cartoes.Numero = tabela.Rows[0]["numero"].ToString();
                cartoes.Conta = tabela.Rows[0]["conta"].ToString();
                cartoes.Bandeira = tabela.Rows[0]["bandeira"].ToString();
                cartoes.Banco = tabela.Rows[0]["banco"].ToString();

            }
            return cartoes;
        }




        public bool AlterarCartoes(Cartoes cartao)
        {
            SqlCommand comando = new DBconnection().GetConnction();
            comando.CommandText = "UPDATE cartoes SET numero = @NUMERO, conta = @CONTA, bandeira = @BANDEIRA, banco = @BANCO  WHERE id = @ID";
            comando.Parameters.AddWithValue("@NUMERO", cartao.Numero);
            comando.Parameters.AddWithValue("@CONTA", cartao.Conta);
            comando.Parameters.AddWithValue("@BANDEIRA", cartao.Bandeira);
            comando.Parameters.AddWithValue("@BANCO", cartao.Banco);
            comando.Parameters.AddWithValue("@ID", cartao.Id);
            return comando.ExecuteNonQuery() == 1;
        }

        public Cartoes ObterPeloIdpessoas(int id)
        {
            Cartoes cartoes = null;
            SqlCommand comando = new DBconnection().GetConnction();
            comando.CommandText = "SELECT numero, conta, bandeira, banco FROM cartoes WHERE id_pessoas = @ID";
            comando.Parameters.AddWithValue("@ID", id);
            DataTable tabela = new DataTable();
            tabela.Load(comando.ExecuteReader());
            if (tabela.Rows.Count == 1)
            {
                cartoes = new Cartoes();
                //cartoes.Id = id;
                cartoes.IdPessoas = id;
                cartoes.Numero = tabela.Rows[0]["numero"].ToString();
                cartoes.Conta = tabela.Rows[0]["conta"].ToString();
                cartoes.Bandeira = tabela.Rows[0]["bandeira"].ToString();
                cartoes.Banco = tabela.Rows[0]["banco"].ToString();

            }
            return cartoes;
        }

        public List<Object> ObterTodosCartoesParaSelect2(int id)
        {
            List<Object> registros = new List<object>();
            SqlCommand comando = new DBconnection().GetConnction();
            comando.CommandText = "SELECT  id, conta FROM cartoes WHERE id_pessoas = @ID";
            comando.Parameters.AddWithValue("@ID", id);
            DataTable tabela = new DataTable();
            tabela.Load(comando.ExecuteReader());
            foreach (DataRow linha in tabela.Rows)
            {
                registros.Add(new
                {
                   id = Convert.ToInt32(linha["id"].ToString()),
                   text = linha["conta"].ToString(),
                });
            }
            return registros;
        }
    }

}