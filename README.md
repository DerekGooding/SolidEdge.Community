# SolidEdge.Community

[![CI](https://github.com/SolidEdgeCommunity/SolidEdge.Community/actions/workflows/ci.yml/badge.svg)](https://github.com/SolidEdgeCommunity/SolidEdge.Community/actions/workflows/ci.yml)
[![codecov](https://codecov.io/gh/SolidEdgeCommunity/SolidEdge.Community/branch/master/graph/badge.svg?token=YOUR_TOKEN_HERE)](https://codecov.io/gh/SolidEdgeCommunity/SolidEdge.Community)

A community-driven toolkit for automating Siemens Solid Edge using modern .NET.
 This project provides a set of high-level abstractions, extension methods, and utilities to simplify COM interop and enhance developer productivity.

## Target Frameworks
This project targets **.NET 10.0-windows**. When consuming this library, ensure your project file specifies the Windows-specific TFM:
```xml
<TargetFramework>net10.0-windows</TargetFramework>
```

## Key Features

### Extension Methods
Extensive support for the Solid Edge API through idiomatic C# extension methods, covering:
- **Application & Documents:** Simplified connection, document creation (Assembly, Part, Sheet Metal, Draft), and active document retrieval.
- **Geometry & Topology:** Safe keypoint position retrieval for 3D Curves and Lines.
- **Drafting:** Enumeration of drawing views and objects, and saving sheets as Enhanced Metafiles (EMF).
- **Properties & Variables:** Streamlined access to document properties and the variable table.

### Core Utilities
- **SolidEdgeUtils:** Simplified connection management to running or new Solid Edge instances.
- **OleMessageFilter:** Built-in implementation of `IMessageFilter` to handle "Application is Busy" (RPC_E_CALL_REJECTED) errors during long-running operations.
- **Event Management:** `EventSink<T>` and `ConnectionPointController` for robustly handling COM events.
- **IsolatedTaskProxy:** Support for executing operations in dedicated STA threads.

### COM Runtime Helpers
- **ComObject:** Utilities for retrieving `ITypeInfo`, property values via reflection, and identifying managed types from COM objects.

## Testing & Quality
The project maintains a rigorous testing standard:
- **Unit Tests:** A comprehensive test suite using **MSTest** and **Moq**.
- **Mocking:** COM dependencies are fully isolated via mocking, allowing tests to run without a local Solid Edge installation.
- **Coverage:** Aiming for 80%+ code coverage to ensure long-term maintainability and reliability.
- **Mutation Testing:** Designed to be resilient against mutation testing to verify test quality.

## Contribution
This is a community project. Contributions, bug reports, and feature requests are welcome.

---
Maintained by **Derek Gooding**
