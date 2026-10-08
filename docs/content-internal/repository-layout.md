---
id: audis-public-repository-layout
title: Repository- und Paketlayout
sidebar_label: Repository-Layout
sidebar_position: 1
---

# Repository-Layout

Quellcode unter `src/<PackageName>/`. Jedes Paket hat ein eigenes Projekt und wird separat auf NuGet veröffentlicht.

## Abhängigkeiten

```text
Audis.Primitives (Basis)
├── Audis.Analyzer.Contract
│   └── Audis.Analyzer.Common
├── Audis.Endpoints.Contract
└── Audis.KnowledgeEnrichers.Contract

Audis.AI.Relay.Contract (unabhängig)
Audis.Catalog.Contract → Audis.Primitives
Audis.Search.Contract → Audis.Primitives
Audis.OpenID.* (unabhängig)
Audis.Location (unabhängig)
```

## Release

Versionierung und Veröffentlichung erfolgen über die CI-Pipeline des Repositories; Release Notes unter `docs/changelog/` (`vX.Y.Z.md`, Entwurf in `NEXT.md`).
