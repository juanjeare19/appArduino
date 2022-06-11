using Azure;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using ITableEntity = Azure.Data.Tables.ITableEntity;

namespace appArduino.Models
{
    class EntidadColores : TableEntity
    {
        private string _idReg;
        private string _part;

        public string Id
        {
            get
            {
                return this._idReg;
            }
            set
            {
                this.RowKey = value;
                this._idReg = value;
            }

        }

        public string Partition
        {
            get
            {
                return _part;
            }
            set
            {
                this.PartitionKey = value;
                this._part = value;
            }

        }
        public string Rgb { get; set; }
        public string Color { get; set; }
        public string Movimiento { get; set; }
        public string Fecha { get; set; }
        public string Hora { get; set; }

        public EntidadColores()
        {

        }


    }



}
