Contributing to SurfStat
==========================

Deploying the NuGet Package
----------------------------

1. Update the `CHANGELOG.md`.
2. Update the relase notes in the `*.nuspec`.
3. Update the versions in any modified assemblies.
4. Update the version in the `*.nuspec`.
5. `nuget pack -Prop Configuration=Release -Build`
6. `nuget push SurfStat.nupkg`