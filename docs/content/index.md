---
id: audis-public-overview
title: Public Packages
sidebar_label: Übersicht
sidebar_position: 1
---

# audis-public

Gemeinsame **NuGet-Pakete** für AUDIS, Analyzer, Adapter und Integrationen. Veröffentlichung: [NuGet — Audis](https://www.nuget.org/packages?q=Audis).

## Pakete

| Paket | Beschreibung |
| --- | --- |
| [`Audis.Primitives`](https://www.nuget.org/packages/Audis.Primitives) | Primitive Typen (KnowledgeIdentifiers, DispositionLevels, ScenarioIdentifier, …) |
| [`Audis.Analyzer.Contract`](https://www.nuget.org/packages/Audis.Analyzer.Contract) | Gemeinsame Analyzer-DTOs und Erweiterungen |
| [`Audis.Analyzer.Common`](https://www.nuget.org/packages/Audis.Analyzer.Common) | DTOs und Interfaces zur Analyzer-Implementierung |
| [`Audis.Endpoints.Contract`](https://www.nuget.org/packages/Audis.Endpoints.Contract) | Outbound-Notification-DTOs (KnowledgeSummaryGenerated, RecordingCompleted, …) |
| [`Audis.AI.Relay.Contract`](https://www.nuget.org/packages/Audis.AI.Relay.Contract) | DTOs für die AI Relay API |
| [`Audis.Catalog.Contract`](https://www.nuget.org/packages/Audis.Catalog.Contract) | Catalog API (Fragen/Antworten, Enrichment) |
| [`Audis.Search.Contract`](https://www.nuget.org/packages/Audis.Search.Contract) | Wire-Format für externe Antwort-Suche |
| [`Audis.KnowledgeEnrichers.Contract`](https://www.nuget.org/packages/Audis.KnowledgeEnrichers.Contract) | Knowledge-Enricher-Vertrag |
| [`Audis.OpenID.Authentication`](https://www.nuget.org/packages/Audis.OpenID.Authentication) | OpenID-Client-Hilfen |
| [`Audis.OpenID.Authorization`](https://www.nuget.org/packages/Audis.OpenID.Authorization) | OpenID-Schutz für Endpoints |
| [`Audis.Location`](https://www.nuget.org/packages/Audis.Location) | DTOs für den Location-Endpoint |

## Abhängigkeiten (Kurz)

`Audis.Primitives` ist Basis für Analyzer-, Endpoints-, Catalog-, Search- und KnowledgeEnrichers-Contracts. `Audis.AI.Relay.Contract`, OpenID- und Location-Pakete sind weitgehend unabhängig.

Details im Docs-Hub unter **Internal → Repository- und Paketlayout**.
