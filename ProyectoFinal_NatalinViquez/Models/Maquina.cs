using Newtonsoft.Json;

namespace ProyectoFinal_natalinviquez.Models
{
    public class Maquina
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        public int cantidadProductoPorHora { get; set; }


        public int costoEnColones { get; set; }


        public int costoPorHora { get; set; }


        public double probabilidadFallo { get; set; }


        public string garantia { get; set; }
    
        public bool estado { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
