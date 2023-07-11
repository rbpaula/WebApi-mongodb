﻿using MongoDB.Driver.Core.Operations;
using MongoDB.Driver.GeoJsonObjectModel;

namespace WebApiMongodb.Data.Collections
{
    public class Infectado
    {
        public Infectado(DateTime dataNascimento, string sexo, double latitude, double longitude)
        {
            this.DataNascimento = dataNascimento;  
            this.Sexo = sexo;
            this.Localizacao = new GeoJson2DGeographicCoordinates(latitude, longitude);
        }

        public DateTime DataNascimento { get; set; }
        public string Sexo { get; set; }
        public GeoJson2DGeographicCoordinates Localizacao { get; set; }

    }
}
