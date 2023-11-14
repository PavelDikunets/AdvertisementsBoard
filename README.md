# AdvertisementBoard - Электронная доска объявлений

AdvertisementBoard - это веб API приложение, которое позволяет пользователям размещать, просматривать и искать объявления по различным категориям и подкатегориям. Приложение также имеет функции администрирования, такие как назначение ролей пользователям, блокировка и удаление аккаунтов, а также добавление, изменение и удаление категорий и подкатегорий.

## Возможности приложения

- Размещение объявлений с заголовком, описанием, изображением, ценой, контактными данными и выбором категории и подкатегории.
- Поиск объявлений с постраничной пагинацией.
- Регистрация и вход в систему с использованием электронной почты и пароля.
- Авторизация по JWT токену, который хранится в локальном хранилище браузера.
- Роли пользователей: Администратор, Модератор, Пользователь.
- Возможность администраторов назначать роли пользователям, блокировать и удалять аккаунты.
- Возможность модераторов и администраторов добавлять, изменять и удалять категории и подкатегории.

## Технологический стек

- .NET 7.0 - платформа для разработки веб-приложений с использованием C#.
- API Restful.
- EF Core - ORM фреймворк для работы с базой данных Postgres.
- Serilog - библиотека для логирования событий приложения.
- AutoMapper - библиотека для маппинга между моделями данных и DTO.
- ELK - стек логирования, состоящий из Elasticsearch, Logstash и Kibana.
- Docker - платформа для контейнеризации и развертывания приложения.


## Установка и запуск

Для запуска приложения вам понадобятся следующие инструменты:

- .NET SDK 7.0 или выше
- Docker Desktop
- Git

Следуйте этим шагам, чтобы запустить приложение:

1. Клонируйте репозиторий с помощью команды `git clone https://github.com/PavelDikunets/AdvertisementBoard.git`
2. Перейдите в папку проекта с помощью команды `cd AdvertisementBoard`
3. Запустите приложение с помощью команды ` docker-compose -f docker-compose.yml -f docker-compose.elk.yml up -d --build`
5. Для доступа к API используйте адрес `http://localhost5259/swagger/index.html` Логин и пароль для администратора по умолчанию: admin@admin, adminadmin
6. Для доступа к Kibana используйте адрес `http://localhost:5601`
