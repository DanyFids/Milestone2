using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Milestone2.Models
{
    public class BikeListModel
    {
        public BikeListModel(string ProdModel, string Desc, int ProdModelID)
        {
            ProductModel = ProdModel;
            Description = Desc;
            ID = ProdModelID;
        }

        public string ProductModel { get; set; }
        public string Description { get; set; }
        public int ID { get; set; }
    }
}