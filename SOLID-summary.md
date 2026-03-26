# SOLID Summary - IceCity Refactor

## Single Responsibility Principle (SRP)
- `CostService` now coordinates cost-related calculations and delegates pricing rules to strategies.
- Each strategy class (`StandardCostStrategy`, `EcoCostStrategy`, `SolarCostStrategy`) is responsible only for one pricing algorithm.
- `CostStrategyFactory` is responsible only for resolving the correct strategy.

## Open/Closed Principle (OCP)
- New pricing behavior is added by creating a new `ICostCalculationStrategy` implementation.
- Existing service logic does not need modification when adding a new strategy.
- `SolarCostStrategy` demonstrates extension without changing `CostService`.

## Liskov Substitution Principle (LSP)
- All concrete strategies can be used anywhere `ICostCalculationStrategy` is expected.
- Factory and services depend on the interface contract, not concrete strategy details.

## Interface Segregation Principle (ISP)
- `ICostService` exposes cost operations for consumers.
- `ICostStrategyFactory` exposes strategy resolution behavior only.
- `ICostCalculationStrategy` focuses on strategy-specific calculation capabilities.

## Dependency Inversion Principle (DIP)
- `CostService` depends on `ICostStrategyFactory`, not concrete strategies.
- `Report` depends on `ICostService`, not `CostService`.
- Strategy instances are registered in the DI container and injected via abstractions.

## Design Patterns Applied
- Strategy Pattern: pricing rules are encapsulated in independent strategy classes.
- Factory Pattern: `CostStrategyFactory` selects strategy implementations from injected registrations.
- Dependency Injection: `Program` composes the application object graph through `ServiceCollection`.
