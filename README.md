# Functional Delivery Calculator

A console-based C# application that calculates total delivery charges based on various parameters including order volume, delivery type, delivery zone, and priority options. The application demonstrates core functional programming concepts, robust input parsing, and strict separation between I/O and calculation logic.


## How to Run the Application

1. **Prerequisites**: Ensure you have the .NET SDK installed.
2. **Clone / Extract**: Download or extract the repository files to your local machine.
3. **Open Terminal**: Navigate to the directory containing `FunctionalDeliveryCalculator.csproj`:
   ```bash
   cd path/to/FunctionalDeliveryCalculator
   ```
4. **Build and Run**: Execute the program via the .NET CLI:
   ```bash
   dotnet run
   ```


## Technical Questions & Answers

### 1. Which parts of your program handle user input and output?
All user input and output operations are confined to the `Main` method inside `Program.cs`. This acts as the boundary method where inputs are collected via `Console.ReadLine()`, validated, and errors or calculated final prices are printed using `Console.WriteLine()`.

### 2. Which functions perform only delivery price calculations?
The calculation domain is strictly isolated inside:
* `CalculateFinalPrice`: Orchestrates the order of calculation rules without performing any I/O.
* `ApplyRule`: A higher-order function that applies a single pricing transformation.
* `itemRule`, `typeRule`, `zoneRule`, and `expressRule`: Local `Func<decimal, decimal>` lambda delegates representing individual business rules.

None of these functions read from or write to the console or mutate external state.

### 3. How is `Func<...>` used to apply delivery pricing rules?
The `Func<decimal, decimal>` generic delegate type represents a reference to a function that takes a `decimal` (current price) as input and returns a modified `decimal` (new price). Each pricing rule is declared as a `Func<decimal, decimal>` lambda that computes multipliers based on specific conditions (e.g., item count or zone) and returns the updated amount.

### 4. Why is `TryParse` useful when processing delivery data entered by the user?
`TryParse` (such as `decimal.TryParse`, `int.TryParse`, `bool.TryParse`, and `Enum.TryParse`) attempts to parse a string into the desired data type without raising runtime exceptions on invalid data. It returns a `bool` indicating success or failure, allowing the program to gracefully intercept invalid user inputs, display custom error messages, and terminate via early returns without crashing.


## Test Cases

| # | Base Price | Items | Delivery Type | Delivery Zone | Express | Expected Result / Output |
|---|------------|-------|---------------|---------------|---------|--------------------------|
| **1** | `1000` | `2` | `Courier` | `City` | `false` | **1000.00** |
| **2** | `1000` | `5` | `Pickup` | `City` | `false` | **880.00** |
| **3** | `2000` | `10` | `DoorToDoor` | `OutsideCity` | `true` | **4286.10** |
| **4** | `0` | `1` | `Courier` | `Remote` | `false` | **0.00** |
| **5** | `-500` | `2` | `Courier` | `City` | `false` | **Error Message** |
| **6** | `1000` | `0` | `Courier` | `City` | `false` | **Error Message** |
| **7** | `1000` | `3` | `Drone` | `City` | `false` | **Error Message** |
| **8** | `1000` | `3` | `Courier` | `City` | `yes` | **Error Message** |
