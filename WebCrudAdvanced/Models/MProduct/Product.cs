﻿namespace WebCrudAdvanced.Models.MProduct
{
    public class Product
    {
        public int Id { get; set; }   
        public string Name { get; set; }
        public double Price { get; set; }  
        public string Description { get; set; }

        public int UserId { get; set; }
    }
}
