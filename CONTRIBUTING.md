Contributing to SurfStat
==========================

Deploying the NuGet Package
----------------------------

1. Update the versions in any modified assemblies.
2. Update the `CHANGELOG.md`.
3. Update the relase notes in the `*.nuspec`.
4. Update the version in the `*.nuspec`.
5. `nuget pack -Prop Configuration=Release`
6. `nuget push SurfStat.nupkg`