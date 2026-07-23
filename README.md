# audis-public
An Audis library which includes shared data types, DTOs, ...

The repository contains the following packages, also available on [NuGet](https://www.nuget.org/packages?q=Audis).

| Package | Description |
| --- | --- |
[`Audis.Primitives`](src/Audis.Primitives) | Provides primitive data types used in Audis, e.g. KnowlegeIdentifiers, KnowledgeValues, DispositionLevels, ScenarioIdentifier, ... |
[`Audis.Analyzer.Common`](src/Audis.Analyzer.Common) | Provides DTOs and interfaces for implementing analyzers. |
[`Audis.Analyzer.Contract`](src/Audis.Analyzer.Contract) | Provides common DTOs and extension methods for Analyzers. |
[`Audis.Endpoints.Contract`](src/Audis.Endpoints.Contract) | Defines outbound notification DTOs for endpoints Audis calls (including KnowledgeSummaryGenerated and RecordingCompleted). Synchronous AI relay request/response pairs live in Audis.AI.Relay.Contract. |
[`Audis.AI.Relay.Contract`](src/Audis.AI.Relay.Contract) | Defines request/response DTOs for the Audis AI Relay API (`/process`, pipeline, and CaseDetermination / case-record suggestion). |
[`Audis.Catalog.Contract`](src/Audis.Catalog.Contract) | Defines request/response DTOs for the Catalog API (question access and text enrichment). |
[`Audis.Search.Contract`](src/Audis.Search.Contract) | Defines request/response DTOs for external answer-search endpoints (wire format only). |
[`Audis.KnowledgeEnrichers.Contract`](src/Audis.KnowledgeEnrichers.Contract) | Provides an interface and DTOs for knowledge enrichers. |
[`Audis.OpenID.Authentication`](src/Audis.OpenID/Audis.OpenID.Authentication) | Provides methods and classes to easily authenticate against an OpenID authentication provider. |
[`Audis.OpenID.Authorization`](src/Audis.OpenID/Audis.OpenID.Authorization) | Provides methods and classes to easily protect endpoints using an OpenID authentication provider. |
[`Audis.Location`](src/Audis.Location) | Defines DTOs for the location endpoint called by Audis. |

## Package Dependencies

The following diagram shows the internal dependencies between Audis packages:

```
Audis.Primitives (base package)
├── Audis.Analyzer.Contract
│   └── Audis.Analyzer.Common
├── Audis.Endpoints.Contract
└── Audis.KnowledgeEnrichers.Contract

Audis.AI.Relay.Contract (independent)

Audis.Catalog.Contract
└── Audis.Primitives

Audis.Search.Contract
└── Audis.Primitives

Audis.OpenID.Authentication (independent)

Audis.OpenID.Authorization (independent)

Audis.Location (independent)
```
