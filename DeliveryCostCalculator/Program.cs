using System;

namespace DeliveryCanculator
{
    enum DeliveryType { Pickup, Courier, DoorToDoor }
    enum DeliveryZone { City, OutsideCity, Remote }

    class Program
    {
        static decimal ApplyRule(decimal price, Func<decimal,decimal> rule) => rule(price);

        static decimal CalculateFinalPrice(decimal basePrice, int item, DeliveryType type, DeliveryZone zone, bool express)
        {
            Func<decimal, decimal> itemRule = p =>
            {
                if (item >= 8) return p * 1.20m;
                if (item >= 4) return p * 1.10m;
                return p;
            };

            Func<decimal, decimal> typeRule = p =>
            {
              if (type == DeliveryType.Pickup) return p * 0.80m;
              if (type == DeliveryType.DoorToDoor) return p * 1.15m;
              return p;  
            };

            Func<decimal,decimal> zoneRule = p =>
            {
              if (zone == DeliveryZone.OutsideCity) return p * 1.25m;
              return p;  
            };

            Func<decimal,decimal> expressRule = p => express ? p * 1.30m : p;

            decimal price = basePrice;
            price = ApplyRule(price, itemRule);
            price = ApplyRule(price, typeRule);
            price = ApplyRule(price, zoneRule);
            price = ApplyRule(price, expressRule);

            return Math.Round(price,2);
        }

        static void Main()
        {
            Console.WriteLine("Delivery Cost Calculator");

            Console.Write("Enter base price: ");
            if(!decimal.TryParse(Console.ReadLine(), out decimal basePrice) || basePrice < 0)
            {
                Console.WriteLine("Error: Invalid or missing base price. Must be a positive number");
                return;
            }

            Console.Write("Enter number of items: ");
            if(!int.TryParse(Console.ReadLine(), out int item) || item < 1)
            {
                Console.WriteLine("Error: Invalid number of items. Must be 1 or greater");
                return;
            }

            Console.Write("Enter Delivery Type (Pickup, Courier, DoorToDoor): ");
            if(!Enum.TryParse<DeliveryType>(Console.ReadLine(), true, out DeliveryType deliveryType))
            {
                Console.WriteLine("Error: Invalid delivery type.");
                return;
            }

            Console.Write("Enter Delivery Zone (City, OutsideCity, Remote): ");
            if(!Enum.TryParse<DeliveryZone>(Console.ReadLine(), true, out DeliveryZone deliveryZone))
            {
                Console.WriteLine("Error: Invalid delivery zone.");
                return;
            }

            Console.Write("Is it express delivery? (true/false): ");
            if(!bool.TryParse(Console.ReadLine(), out bool express))
            {
                Console.WriteLine("Error: Invalid express status. Must be 'true' or 'false'.");
                return;
            }

            decimal finalPrice = CalculateFinalPrice(basePrice, item, deliveryType, deliveryZone, express);

            Console.WriteLine($"Final price: {finalPrice}");
        }
    }
}
