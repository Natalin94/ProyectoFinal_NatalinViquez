using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ProyectoFinal_natalinviquez.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoFinal_natalinviquez.Services
{
    public class ServiceCosmosDb
    {

        DocumentClient client;
        String bbdd;
        String collection;
        String collectionProducto;
        public ServiceCosmosDb(IConfiguration configuration)
        {
            String endpoint = configuration["CosmosDb:endPoint"];
            String primarykey = configuration["CosmosDb:primaryKey"];
            this.bbdd = "Simulacion BBDD";
            this.collection = "Maquinas";
           
            this.client = new DocumentClient(new Uri(endpoint), primarykey);
          
        }


        public async Task CrearBbddAsync()
        {
            Database bbdd = new Database() { Id = this.bbdd };
            await this.client.CreateDatabaseAsync(bbdd);
        }

        public async Task CrearColeccionAsync()
        {
            DocumentCollection coleccion = new DocumentCollection() { Id = this.collection };
            DocumentCollection collectionProducto = new DocumentCollection() { Id = this.collectionProducto };
            //Factory es para recuperar de cosmos la base de datos
            await this.client.CreateDocumentCollectionAsync(UriFactory.CreateDatabaseUri(this.bbdd), collectionProducto);
            
        }

        public async Task InsertarMaquina(Maquina maquina)
        {
            //recuperamos la URI para la coleccion donde ira el vehiculo
            Uri uri = UriFactory.CreateDocumentCollectionUri(this.bbdd, this.collection);
            await this.client.CreateDocumentAsync(uri, maquina);
        }

        public List<Maquina> GetMaquinas()
        {
            // debemos indicar el numero de peliculas a recuperar
            FeedOptions options = new FeedOptions() { MaxItemCount = -1 };
            String sql = "SELECT * FROM C"; // a todo lo llama 'c'
            Uri uri = UriFactory.CreateDocumentCollectionUri(this.bbdd, this.collection);
            IQueryable<Maquina> consulta = this.client.CreateDocumentQuery<Maquina>(uri, sql, options);
            return consulta.ToList();
        }

        public async Task<Maquina> FindPeliculaAsyn(String id)
        {
            Uri uri = UriFactory.CreateDocumentUri(this.bbdd, this.collection, id);
            //lo que recupera es de la clase document

            Document document = await this.client.ReadDocumentAsync(uri);
            //este documento es un stream
            //guardamos en el objeto stream en memoria lo que recuperamos, para luego leerlo en memoria
            MemoryStream memory = new MemoryStream();
            using (var stream = new StreamReader(memory))
            {
                document.SaveTo(memory);
                memory.Position = 0;
                //deserializamos con JsonConvert
                Maquina maquina = JsonConvert.DeserializeObject<Maquina>(await stream.ReadToEndAsync());
                return maquina;
            }
        }

        public async Task ModificarMaquina(Maquina maquina)
        {
            Uri uri = UriFactory.CreateDocumentUri(this.bbdd, this.collection, maquina.Id.ToString());
            await this.client.ReplaceDocumentAsync(uri, maquina);
        }


        public async Task EliminarMaquina(String id)
        {
            Uri uri = UriFactory.CreateDocumentUri(this.bbdd, this.collection, id);
            await this.client.DeleteDocumentAsync(uri);
        }

        public List<Maquina> BuscarMaquina(String categoria)
        {
            FeedOptions options = new FeedOptions() { MaxItemCount = -1 };
            Uri uri = UriFactory.CreateDocumentCollectionUri(this.bbdd, this.collection);
            String sql = "select * from c where c.Categoria='" + categoria + "'";
            IQueryable<Maquina> query = this.client.CreateDocumentQuery<Maquina>(uri, sql, options);
            IQueryable<Maquina> querylambda = this.client.CreateDocumentQuery<Maquina>(uri, options)
                    .Where(z => z.Id.ToString() == categoria);

            return query.ToList();
        }

       
    }
}
