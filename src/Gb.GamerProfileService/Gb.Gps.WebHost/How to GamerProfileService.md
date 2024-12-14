

http://localhost:8091/swagger/index.html
http://localhost:8080/weatherforecast

# Базы данных
docker-compose up game-by-gamer-profile-service-db

# Сервисы
docker-compose up game-by-gamer-profile-service-api


1.Сгенерировать миграцию
dotnet ef migrations add <название миграции> --startup-project <стартовый проект> --project <проект с EF> --context <наименование контекста>
dotnet-ef migrations add InitialMigration --startup-project Gb.Gps.WebHost --project Gb.Gps.Infrastructure.EntityFramework --context ApplicationDBContext


2.Обновить БД
dotnet ef database update --startup-project <стартовый проект> --project <проект с EF> --context <наименование контекста>
dotnet ef database update --startup-project Gb.Gps.WebHost --project Gb.Gps.Infrastructure.EntityFramework --context ApplicationDBContext


3.Обновить БД до миграции
dotnet ef database update <название миграции> --startup-project <стартовый проект> --project <проект с EF> --context <наименование контекста>
dotnet ef database update InitialMigration --startup-project Gb.Gps.WebHost --project Gb.Gps.Infrastructure.EntityFramework --context ApplicationDBContext


4.Удалить последнюю миграцию
dotnet ef migrations remove --startup-project <стартовый проект> --project <проект с EF> --context <наименование контекста>
dotnet ef migrations remove --startup-project Gb.Gps.WebHost --project Gb.Gps.Infrastructure.EntityFramework --context ApplicationDBContext