﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DemoWebCakeDB.Models;

namespace DemoWebCakeDB.Models
{
    public class CartItem
    {
        QLHOAEntities db = new QLHOAEntities();
        public int ProductID { get; set; }
        public string NamePro { get; set; }
        public string ImagePro { get; set; }
        public decimal Price { get; set; }
        public int Number { get; set; }
        //Tính FinalPrice = Price * Number
        public decimal FinalPrice()
        {
            return Number * Price;
        }
        public CartItem(int ProductID)
        {
            this.ProductID = ProductID;
            var productDB = db.Products.Single(s => s.ProductID ==
            this.ProductID);
            this.NamePro = productDB.NamePro;
            this.ImagePro = productDB.ImagePro;
            this.Price = (decimal)productDB.Price;
            this.Number = 1;
        }
    }
}