# audis-public
An Audis library which includes shared data types, DTOs, ...

The repository contains the following packages, also available on [NuGet](https://www.nuget.org/packages?q=Audis).

| Package | Description |
| --- | --- |
[`Audis.Primitives`](src/Audis.Primitives) | Provides primitive data types used in Audis, e.g. KnowlegeIdentifiers, KnowledgeValues, DispositionLevels, ScenarioIdentifier, ... |
[`Audis.Analyzer.Common`](src/Audis.Analyzer.Common) | Provides DTOs and interfaces for implementing analyzers. |
[`Audis.Analyzer.Contract`](src/Audis.Analyzer.Contract) | Provides common DTOs and extension methods for Analyzers. |
[`Audis.Endpoints.Contract`](src/Audis.Endpoints.Contract) | Defines DTOs for endpoints which are called or consumed by Audis. |
[`Audis.KnowledgeEnrichers.Contract`](src/Audis.KnowledgeEnrichers.Contract) | Provides an interface and DTOs for knowledge enrichers. |
[`Audis.OpenID.Authentication`](src/Audis.OpenID/Audis.OpenID.Authentication) | Provides methods and classes to easily authenticate against an OpenID authentication provider. |
[`Audis.OpenID.Authorization`](src/Audis.OpenID/Audis.OpenID.Authorization) | Provides methods and classes to easily protect endpoints using an OpenID authentication provider. |
[`Audis.Location`](src/Audis.Location) | Defines DTOs for the location endpoint called by Audis. |
