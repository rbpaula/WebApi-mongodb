using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using WebApiMongodb.Data.Collections;
using WebApiMongodb.Models;

namespace WebApiMongodb.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InfectadoController : ControllerBase
    {
        Data.MongoDB _mongoDB;
        IMongoCollection<Infectado> _infectadosCollection;
        public InfectadoController(Data.MongoDB mongoDB) 
        {
            _mongoDB = mongoDB;
            _infectadosCollection = _mongoDB.DB.GetCollection<Infectado>(typeof(Infectado).Name.ToLower());
        }

        [HttpPost]
        public IActionResult SalvarInfectado([FromBody] InfectadoDto dto) 
        {
            var infectado = new Infectado(dto.DataNascimento, dto.Sexo, dto.Latitude, dto.Longitude);

            _infectadosCollection.InsertOne(infectado);

            return StatusCode(200, "Infectado adicionado com sucesso");
        }

        [HttpGet]
        public ActionResult ObterInfectados()
        {
            var infectados = _infectadosCollection.Find(Builders<Infectado>.Filter.Empty).ToList();

            return Ok(infectados);
        }

        [HttpPut]
        public ActionResult AtualizarInfectado([FromBody] InfectadoDto dto) 
        {
            var filter = Builders<Infectado>.Filter.Eq(infectado => infectado.DataNascimento, dto.DataNascimento);

            var oldInfectado = _infectadosCollection.Find(filter).First();
            var oldId = oldInfectado.DataNascimento;

            Infectado newInfectado = new Infectado(dto.DataNascimento, dto.Sexo, dto.Latitude, dto.Longitude);

            var replaceResult = _infectadosCollection.ReplaceOne(filter, newInfectado);

            return StatusCode(200, "Infectado atualizado com sucesso");
        }


        [HttpDelete]
        public ActionResult ExcluirInfectado(DateTime dataNascimento)
        {
            var filter = Builders<Infectado>.Filter.Eq(infectado => infectado.DataNascimento, dataNascimento);

           var deleteResult = _infectadosCollection.DeleteOne(filter);

            return StatusCode(200, "Infectado excluído com sucesso");
        }
    }
}
