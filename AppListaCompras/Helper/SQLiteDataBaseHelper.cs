using AppListaCompras.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppListaCompras.Helper
{
    public class SQLiteDataBaseHelper
    {
        readonly SQLiteAsyncConnection _connection;

        public SQLiteDataBaseHelper(string path)
        {
            _connection = new SQLiteAsyncConnection(path);
            _connection.CreateTableAsync<Produto>().Wait();
        }
        public Task<int> insert(Produto p)
        {
            return _connection.InsertAsync(p);
        }

        public void update(Produto p)
        {
            string sql = "UPDATE Produto set desc = ?, qnt = ?, preco = ? where id = ?";
            _connection.QueryAsync<Produto>(sql, p.desc, p.qnt, p.preco, p.id);
        }

        public void delete(int id)
        {
            _connection.Table<Produto>().DeleteAsync(i => i.id == id);
        }

        public Task<List<Produto>> getAll()
        {
            return _connection.Table<Produto>().ToListAsync();
        }
    }
}
