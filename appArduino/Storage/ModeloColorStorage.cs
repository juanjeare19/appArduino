using appArduino.Models;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Configuration;

namespace appArduino.Storage
{
    class ModeloColorStorage
    {
        readonly CloudStorageAccount storageAccount;
        readonly string connectionString;

        public ModeloColorStorage()
        {
            // var connectionString = ConfigurationManager.AppSettings["StorageConnectionString"];
            connectionString = ConfigurationManager.AppSettings["connectionStringColores"];
            storageAccount = CloudStorageAccount.Parse(connectionString);
            // CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
            // CloudTable table = tableClient.GetTableReference("Login");
        }

        public CloudTable CrearTablaAzureStorage()
        {
            //CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
            CloudTable table = tableClient.GetTableReference("TablaColores");

            table.CreateIfNotExists();

            return table;
        }

        public bool GuardarRegistros(string pFecha, string pHora, string pRgb, string pColor, string pMovimiento)
        {
            bool aux = false;

            CloudTable Tabla = this.CrearTablaAzureStorage();
            EntidadColores Color = new EntidadColores
            {
                Fecha = pFecha,
                Hora = pHora,
                Rgb = pRgb,
                Color = pColor,
                Movimiento = pMovimiento,
                PartitionKey = "Colores",
                RowKey = System.Guid.NewGuid().ToString()

            };

            TableOperation insertOperation = TableOperation.Insert(Color);

            Tabla.Execute(insertOperation);
            //var connectionString = ConfigurationManager.AppSettings["connectionStringTemperatura"];

            //CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);
            //CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
            //CloudTable table = tableClient.GetTableReference("TablaTemperatura");

            return aux;
        }

    }
}
