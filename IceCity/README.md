# IceCity Heating System

A console-based heating cost calculator application built with C# and .NET, demonstrating object-oriented programming principles including inheritance, polymorphism, encapsulation, and abstraction.

## 📋 Project Overview

IceCity is a heating management system that calculates monthly heating costs based on daily usage data, heater types, and power consumption. The application models real-world heating scenarios where different heater types (Electric vs Gas) have different efficiency rates.

## 🎯 Features

- **Owner Management**: Track house owners with validation
- **Multiple Heater Types**: Support for Electric (100% efficiency) and Gas (80% efficiency) heaters
- **Daily Usage Tracking**: Records 30 days of heating usage data
- **Automated Calculations**:
  - Total working hours
  - Median heater value
  - Monthly average heating cost
- **Professional Reports**: Generates formatted monthly heating reports

## 🏗️ Architecture

### Object-Oriented Design

The project follows SOLID principles and implements core OOP concepts:

#### Classes Structure
```
IceCity/
├── Models/
│   ├── Owner.cs              # Represents house owner
│   ├── House.cs              # Contains heaters and usage data
│   ├── Heater.cs             # Abstract base class for heaters
│   ├── ElectricHeater.cs     # Concrete heater implementation
│   ├── GasHeater.cs          # Concrete heater implementation
│   └── DailyUsage.cs         # Daily heating usage record
├── Services/
│   ├── CalculationService.cs # Business logic for calculations
│   └── Report.cs             # Report generation service
└── Program.cs                # Application entry point
```

### OOP Principles Demonstrated

1. **Encapsulation**
   - All fields are private with public property accessors
   - Data validation in property setters
   - Example: `DailyUsage` validates hours (0-24) and heater values (>0)

2. **Abstraction**
   - Abstract `Heater` base class defines contract for all heaters
   - Abstract method `CalculateEffectivePower()` must be implemented by derived classes

3. **Inheritance**
   - `ElectricHeater` and `GasHeater` inherit from `Heater`
   - Share common properties (Power) while implementing unique behavior

4. **Polymorphism**
   - `List<Heater>` can store both Electric and Gas heaters
   - Runtime method binding: `heater.CalculateEffectivePower()` calls correct implementation
   - Example:
```csharp
     Heater heater1 = new ElectricHeater(5000); // 100% efficient
     Heater heater2 = new GasHeater(3000);      // 80% efficient
```

## 🚀 Getting Started

### Prerequisites

- .NET 6.0 or higher
- Visual Studio 2022 or VS Code with C# extension

### Installation

1. Clone the repository
```bash
   git clone https://github.com/ziad276/icecity.git
   cd icecity
```

2. Build the project
```bash
   dotnet build
```

3. Run the application
```bash
   dotnet run
```

## 💻 Usage

1. **Enter Owner Information**
```
   Enter owner name: John Doe
```

2. **Configure Heaters**
```
   How many heaters does the house have? 2
   
   --- Heater 1 ---
   Type (1=Electric, 2=Gas): 1
   Power (watts): 5000
   
   --- Heater 2 ---
   Type (1=Electric, 2=Gas): 2
   Power (watts): 3000
```

3. **View Generated Report**
   - Application automatically generates 30 days of sample usage data
   - Displays comprehensive monthly heating report with cost analysis

## 📊 Sample Output
```
========================================
     ICECITY MONTHLY HEATING REPORT     
========================================

Owner: John Doe
Total Working Hours: 450 hours
Median Heater Value: 4200 watts
Monthly Average Cost: $262.50

========================================
```

## 🧮 Calculation Logic

### Total Working Hours
Sum of all daily working hours over the month.

### Median Heater Value
Middle value of sorted heater values from all 30 days.

### Monthly Average Cost
```
Cost = Median Heater Value × (Total Hours / (24 × Number of Days))
```

## 🏛️ Design Patterns Used

- **Service Layer Pattern**: Separation of business logic (CalculationService) from domain models
- **Repository Pattern**: House acts as aggregate root managing heaters and daily usage
- **Factory Pattern**: Heater instantiation based on type selection

## 📝 Class Responsibilities

| Class | Responsibility |
|-------|---------------|
| **Owner** | Store owner information and manage houses |
| **House** | Contain heaters, track usage, coordinate calculations |
| **Heater** | Define heater contract with power and efficiency |
| **ElectricHeater** | Implement 100% efficiency electric heating |
| **GasHeater** | Implement 80% efficiency gas heating |
| **DailyUsage** | Store daily heating data with validation |
| **CalculationService** | Perform all heating cost calculations |
| **Report** | Format and present calculation results |
| **Program** | Orchestrate application flow (no business logic) |

## 🔒 Data Validation

- **Owner Name**: Cannot be null or whitespace
- **Heater Power**: Must be greater than 0
- **Daily Hours**: Must be between 0 and 24
- **Heater Value**: Must be greater than 0
- **Age**: Must be non-negative (if used)

## 🛠️ Technologies

- **Language**: C# 10.0
- **Framework**: .NET 6.0
- **IDE**: Visual Studio 2022 / VS Code
- **Version Control**: Git

## 📦 Project Structure
```
IceCity/
├── Models/           # Domain entities
├── Services/         # Business logic and reporting
├── Program.cs        # Entry point
└── README.md         # This file
```

## 🤝 Contributing

Contributions are welcome! Please follow these steps:

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 👤 Author

**Ziad**
- GitHub: [@ziad276](https://github.com/ziad276)

## 🙏 Acknowledgments

- Built as part of a college project for the CIS team
- Demonstrates practical application of OOP principles
- Inspired by real-world heating cost management systems

---

⭐ Star this repo if you find it helpful!
