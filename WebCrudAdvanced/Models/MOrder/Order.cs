﻿namespace WebCrudAdvanced.Models.MOrder
{
    public class Order
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int UserId { get; set; }

        public int Quantity { get; set; }

        public DateTime RegisterDate { get; set;}


    }
}