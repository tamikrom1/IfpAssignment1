using System;

namespace FunctionalTicketCalculator
{
    enum TicketType
    {
        Standart,
        VIP
    }

    enum DayType
    {
        Weekday,
        Weekend
    }

    class Program
    {
        static decimal ApplyRule(decimal price, Func<decimal,decimal> rule) => rule(price);

        static decimal CalculateTicketPrice(decimal basePrice, int age, bool IsStudent, TicketType ticketType, DayType dayType)
        {
            Func<decimal,decimal> ageRule = p =>
            {
                if(age >= 60) return p * 0.70m;
                if(age <= 12 && age >= 6) return p * 0.50m;
                if(age < 6) return p * 0m;
                return p;
            };

            Func<decimal,decimal> studentRule = p =>
            {
              if(IsStudent) return p * 0.85m;
              return p;  
            };

            Func<decimal,decimal> typeRule = p =>
            {
                if(ticketType == TicketType.VIP) return p * 1.25m;
                return p;
            };

            Func<decimal,decimal> dayRule = p =>
            {
                if(dayType == DayType.Weekend) return p * 1.10m;
                return p;
            };

            decimal price = basePrice;
            price = ApplyRule(price, ageRule);
            price = ApplyRule(price, studentRule);
            price = ApplyRule(price, typeRule);
            price = ApplyRule(price, dayRule);

            return Math.Round(price,2);
        }

        static void Main()
        {
            Console.WriteLine("Ticket Cost Calculator");

            Console.Write("Enter base price: ");
            if(!decimal.TryParse(Console.ReadLine(), out decimal basePrice) || basePrice < 0)
            {
                Console.WriteLine("Error: Invalid or missing base price. Must be a positive number");
                return;
            }

            Console.Write("Enter age: ");
            if(!int.TryParse(Console.ReadLine(), out int age) || age < 1)
            {
                Console.WriteLine("Error: Invalid age. Must be 1 or greater");
                return;
            }

            Console.Write("Student? (true/false): ");
            if(!bool.TryParse(Console.ReadLine(), out bool IsStudent))
            {
                Console.WriteLine("Error: Invalid student status. Must be 'true' or 'false'.");
                return;
            }

            Console.Write("Enter Ticket Type(Standart, VIP): ");
            if(!Enum.TryParse<TicketType>(Console.ReadLine(), true, out TicketType ticketType))
            {
                Console.WriteLine("Error: Invalid Ticket type");
                return;
            }

            Console.Write("Enter Day Type(Weekday, Weekend): ");
            if(!Enum.TryParse<DayType>(Console.ReadLine(), true, out DayType dayType))
            {
                Console.WriteLine("Error: Invalid day type");
                return;
            }


            decimal finalPrice = CalculateTicketPrice(basePrice, age, IsStudent, ticketType, dayType);

            Console.WriteLine($"Final price: {finalPrice}");
        }
    }
    
}