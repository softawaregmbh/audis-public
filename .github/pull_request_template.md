**Describe the change**

Reference an issue (e.g. `softaware-audis-issues#…`) or give a short description of the package or CI change.

**Checklist**

- [ ] Version in the affected `.csproj` updated when publishing a new NuGet release (or release will use the tag version via pipeline)
- [ ] Public API or serialization changes documented in the PR description
- [ ] `dotnet build` / `dotnet test` run for affected solutions locally or via CI
- [ ] Breaking changes called out explicitly

**NuGet release**

Only if this PR prepares a package publish: note the intended tag (`<package-prefix>/<version>`) after merge.
