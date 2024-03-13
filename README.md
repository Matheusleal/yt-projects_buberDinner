# BuberDinner

### Db docker for test
```shell
docker run --name buberDinner_db -e 'HOMEBREW_NO_ENV_FILTERING=1' -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=Xablau1.' -p 1433:1433 -d mcr.microsoft.com/mssql/server:2022-latest
```

```shell
dotnet ef database update --project BuberDinner.Infrastructure --startup-project BuberDinner.Api\ --connection "Server=localhost;Database=BubberDinner;User Id=sa;Password=Xablau1.;Encrypt=false"
```
