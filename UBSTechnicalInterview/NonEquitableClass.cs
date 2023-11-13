using System;

namespace GenericsAndLINQ
{
    public class NonEquatableClass
    {
        public int SerialNumber { get; init; }
        public float Price { get; init; }
        public string ItemDescription { get; init; }
        public bool IsInStock { get; init; }

        public NonEquatableClass(int serialNumber, float price, string itemDescription, bool isInStock)
        {
            SerialNumber = serialNumber;
            Price = price;
            ItemDescription = itemDescription;
            IsInStock = isInStock;
        }
    }
}