# Руководство по настройке `docker-compose.yml`

Этот файл используется для запуска приложения `transactionsapp` в Docker. Ниже описано, что можно изменить.

---

## Основные настройки

### 1. **Порт приложения**
Приложение по умолчанию доступно на порту `9090`. Чтобы изменить порт, найдите строку с настройкой `ports` и замените `9090` на нужный порт.

После запуска приложение будет доступно по адресу `http://localhost:9090`. Также будет доступен Swagger UI для тестирования API по адресу `http://localhost:9090/swagger`.

---

### 2. **Папка с данными**
Приложение использует SQLite для хранения транзакций. База данных сохраняется в папку `./TransactionsApp/data` на вашем компьютере. Чтобы изменить путь к папке, найдите строку с настройкой `volumes` и укажите новый путь.

---

## Запуск

1. Сохраните изменения в `docker-compose.yml`.
2. Запустите контейнеры командой: `docker-compose up -d`.
3. Приложение будет доступно по адресу `http://localhost:9090`.

---

## Заключение

- Измените порт в `ports`, если `9090` занят.
- Укажите свой путь к папке с данными в `volumes`, если нужно сохранять базу данных в другом месте.

Для тестирования API используйте Swagger UI: `http://localhost:9090/swagger`.

---

С уважением,  
Ваш помощник по настройке Docker 🐳 


PS: чтобы дипсик написал корректный ридми ему надо подскзаать что через вебинтерфейс не пролезают сложные конструкции.
PPS: 
в проекте взял:
- тз T3.pdf
- minimal api (похоже на микросервис а не на гейт, минималистичного вариант должно хватить)
- sqlite (пожалел, лучше бы постгрес в компосе сразу поднял)
из интересного:
- добавил opentelemetry, ему конечно бы порезать лишние метрики, но для теста - врядли мы заспамим логи докера
- попробовал REPR паттерн
- добавил миграции
- добавил вертикальный слайсинг по фичам для роста функционала (опять же ориентировался на микросервис а не на монолит с миллионом разнородных фич)
- логика получилась довольно компактной - в сервисы не выносил
- ограничение на 100 записей и конкурентность при создании одного и того же айди решил на стороне базы
- ну про компос уже дипсик рассказал
