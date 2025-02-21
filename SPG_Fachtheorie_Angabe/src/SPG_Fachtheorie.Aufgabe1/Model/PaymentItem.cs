﻿namespace SPG_Fachtheorie.Aufgabe1.Model
{
    public class PaymentItem
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        protected PaymentItem() { }
#pragma warning restore CS8618
        public PaymentItem(string articleName, int amount, decimal price, Payment payment)
        {
            ArticleName = articleName;
            Amount = amount;
            Price = price;
            Payment = payment;
        }
        public int id { get; set; }
        public string ArticleName { get; set; }
        public int Amount { get; set; }
        public decimal Price { get; set; }
        public Payment Payment { get; set; }
        2

    }
}