# audis-public

[![CodeScene Code Health](https://codescene.softaware.at/8/status-badges/code-health)](https://codescene.softaware.at/8/analyses/latest)

An Audis library which includes shared data types, DTOs, ...

The repository contains the following packages, also available on [NuGet](https://www.nuget.org/packages?q=Audis).

## Branching

- `master` is the stable, release-ready line (protected; changes via pull request).
- `dev` is the integration branch for ongoing work (protected; changes via pull request).
- Use `feat/`, `fix/`, or `chore/` branches and open pull requests into `dev`.

## Continuous integration

- `azure-pipelines.yml` builds and tests all solutions on pushes to `master` and `dev`.
- CodeScene analyzes pull requests for code health; comment `/codescene` on a PR to run the refactoring agent.

## NuGet releases (nuget.org)

Packages are published **independently** from this repository. Pushes to `master` or `dev` do **not** publish to nuget.org.

To release a package, create and push a git tag in the form:

```text
<package-prefix>/<version>
```

Example:

```bash
git tag endpoints-contract/3.5.0
git push origin endpoints-contract/3.5.0
```

The pipeline `azure-pipelines-nuget.yml` packs the matching project, runs configured tests, and pushes to [nuget.org](https://www.nuget.org/) (requires `NuGetOrgApiKey` in the `Global-None-KeyVault-Variables` variable group).

### Package tag prefixes

| NuGet package | Git tag prefix | Example tag |
| --- | --- | --- |
| `Audis.Primitives` | `primitives/` | `primitives/5.1.1` |
| `Audis.Contract` | `contract/` | `contract/1.2.0` |
| `Audis.Analyzer.Common` | `analyzer-common/` | `analyzer-common/8.0.1` |
| `Audis.Analyzer.Contract` | `analyzer-contract/` | `analyzer-contract/4.1.1` |
| `Audis.Endpoints.Contract` | `endpoints-contract/` | `endpoints-contract/3.5.0` |
| `Audis.AI.Relay.Contract` | `ai-relay-contract/` | `ai-relay-contract/1.0.0` |
| `Audis.Catalog.Contract` | `catalog-contract/` | `catalog-contract/1.0.1` |
| `Audis.Search.Contract` | `search-contract/` | `search-contract/1.0.1` |
| `Audis.KnowledgeEnrichers.Contract` | `knowledge-enrichers-contract/` | `knowledge-enrichers-contract/3.2.1` |
| `Audis.OpenID.Authentication` | `openid-authentication/` | `openid-authentication/1.0.0` |
| `Audis.OpenID.Authorization` | `openid-authorization/` | `openid-authorization/1.0.0` |
| `Audis.Location` | `location/` | `location/1.3.0` |

To release multiple packages from the same commit, create one tag per package and push them separately. Each tag triggers its own pipeline run.

| Package | Description |
| --- | --- |
[`Audis.Primitives`](src/Audis.Primitives) | Provides primitive data types used in Audis, e.g. KnowlegeIdentifiers, KnowledgeValues, DispositionLevels, ScenarioIdentifier, ... |
[`Audis.Analyzer.Common`](src/Audis.Analyzer.Common) | Provides DTOs and interfaces for implementing analyzers. |
[`Audis.Analyzer.Contract`](src/Audis.Analyzer.Contract) | Provides common DTOs and extension methods for Analyzers. |
[`Audis.Endpoints.Contract`](src/Audis.Endpoints.Contract) | Defines outbound notification DTOs for endpoints Audis calls (including KnowledgeSummaryGenerated and RecordingCompleted). Synchronous AI relay request/response pairs live in Audis.AI.Relay.Contract. |
[`Audis.AI.Relay.Contract`](src/Audis.AI.Relay.Contract) | Defines request/response DTOs for the Audis AI Relay API (`/process`, pipeline, and CaseDetermination / case-record suggestion). |
[`Audis.Catalog.Contract`](src/Audis.Catalog.Contract) | Defines request/response DTOs for the Catalog API (question access with answers/synonyms for AI training/export, and text enrichment). |
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
