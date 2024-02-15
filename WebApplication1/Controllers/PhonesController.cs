using Dapper;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]

    public class PhonesController : ControllerBase
    {
        const string ConnectionString = "Host=localhost;Port=5432;Database=Fordapper; username=postgres;Password=psqlDB;";

        [HttpGet]
        public List<Phones> Get()
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(ConnectionString))
            {
                return connection.Query<Phones>("SELECT * FROM phones_list;").ToList();
            }
        }

        [HttpPost]
        public Insertmodel InsertInto(Insertmodel viewModel)
        {
            string sql = "INSERT INTO phones_list (product, description) VALUES (@product, @description);";

            using(NpgsqlConnection connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Execute(sql, new Insertmodel
                {
                    product = viewModel.product,
                    description = viewModel.description
                });
                return viewModel;
            }
        }
        [HttpPut]
        public Insertmodel Update(int id, Insertmodel viewModel) 
        {
            string sql = @$"UPDATE phones_list SET product = @product, description = @description WHERE id = {id};";

            using(NpgsqlConnection connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Execute(sql, new Insertmodel
                {
                    product = viewModel.product,
                    description = viewModel.description,
                });

                return viewModel;
            }
        }
        [HttpPatch]
        public int UpdateOne(int id, string product)
        {
            string sql = "UPDATE phones_list SET product = @product WHERE id = @id";

            using(NpgsqlConnection connection = new NpgsqlConnection(ConnectionString)) 
            {
                var something = connection.Execute(sql, new { product = product, id = id});

                return something;
            }
        }
        [HttpDelete]
        public int Delete(int id)
        {
            string sql = @$"DELETE FROM phones_list WHERE id = {id}";

            using(NpgsqlConnection connection = new NpgsqlConnection(ConnectionString))
            {
                var something = connection.Execute(sql, new {id = id });
                return something;
            }
        }
    }
}
