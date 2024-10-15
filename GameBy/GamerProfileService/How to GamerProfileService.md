docker-compose up

http://localhost:8080/swagger/index.html
http://localhost:8080/weatherforecast

docker-compose -f docker-compose-infrastructure.yml up

1.Сгенерировать миграцию
dotnet ef migrations add <название миграции> --startup-project <стартовый проект> --project <проект с EF> --context <наименование контекста>
dotnet ef migrations add InitialMigration --startup-project GamerProfileService --project GameBy.DataAccess --context ApplicationDBContext


2.Обновить БД
dotnet ef database update --startup-project <стартовый проект> --project <проект с EF> --context <наименование контекста>
dotnet ef database update --startup-project GamerProfileService --project GameBy.DataAccess --context ApplicationDBContext


3.Обновить БД до миграции
dotnet ef database update <название миграции> --startup-project <стартовый проект> --project <проект с EF> --context <наименование контекста>
dotnet ef database update InitialMigration --startup-project GamerProfileService --project GameBy.DataAccess --context ApplicationDBContext


4.Удалить последнюю миграцию
dotnet ef migrations remove --startup-project <стартовый проект> --project <проект с EF> --context <наименование контекста>
dotnet ef migrations remove --startup-project GamerProfileService --project GameBy.DataAccess --context ApplicationDBContext