# LinkShorter
Решение состоит из двух проектов: `MVC`и `API`.
## MVC
MVC служит для общения с пользователем (интерфейс), а также для запросов к API.  
Состоит из трёх контроллеров:
* **Account** - для регистрации/авторизации.
* **Home** - основной контроллер интерфейса, представляет собой главную страницу и страницу ошибок.
* **Redirect** - для осщуествления редиректа с короткой ссылки на полную.
## API
Состоит двух контроллеров:
* **Auth** - для авторизации и регистрации.
* **Shorten** - основной контроллер для работы с ссылками (создание, удаление, получение).

Так же в проекте с API присутствует три сервиса:
* **Account** - для авторизации/регистрации.
* **HashCalculator** - для вычисления хэша пароля и создания коротких токенов.
* **Shorten** - для работы с ссылками (создание, удаление, получение).
## Общие сведения
[Авторизация](https://github.com/SMarkls/LinkShorter/blob/master/LinkShortener.Mvc/Controllers/AccountController.cs#L37) в проекте реализована
максимально просто на основе `ClaimsIdentity` и `Cookie`.  

[Контроллеры API](https://github.com/SMarkls/LinkShorter/blob/master/LinkShortener.Api/Controllers/ShortenController.cs#L30),
для добавления, получения и удаления коротких ссылок требуют в запросе заголовок `ownerId`, для получения информации о пользователе.  

[База данных](https://github.com/SMarkls/LinkShorter/blob/master/LinkShortener.Api/ApiDataBase.db) - `SQLite` для удобства её передачи через репозиторий.