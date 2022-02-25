using Newtonsoft.Json;

namespace ProyectoFinal_natalinviquez.Models
{
    public class Producto
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        public string Nombre { get; set; }

        public int Precio { get; set; }

    }
}
