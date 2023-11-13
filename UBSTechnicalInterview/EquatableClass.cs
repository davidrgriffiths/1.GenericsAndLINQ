using System;

namespace GenericsAndLINQ
{
	public class EquatableClass : IEquatable<EquatableClass>
	{
        public int SerialNumber { get; init; }
        public float Price { get; init; }
        public string ItemDescription { get; init; }
        public bool IsInStock { get; init; }

        public EquatableClass(int serialNumber, float price, string itemDescription, bool isInStock)
		{
            SerialNumber = serialNumber;
            Price = price;
            ItemDescription = itemDescription;
            IsInStock = isInStock;
		}

        public bool Equals(EquatableClass? other)
        {
            if (other is null)
            {
                return false;
            }
            else
            {
                return this.SerialNumber == ((EquatableClass)other).SerialNumber;
            }
        }

        public override bool Equals(object? other)
        {
            if (other is EquatableClass equatableClass)
                return Equals(equatableClass);
            else
                return false;
        }

        public override int GetHashCode()
        {
            return SerialNumber.GetHashCode();
        }
    }
}