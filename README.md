# Приложение "Приют для животных"

Desktop приложение для управления приютом для животных, разработанное на C# WPF с использованием .NET Framework и PostgreSQL.

## Технологический стек

- C# .NET Framework 4.8
- WPF (Windows Presentation Foundation)
- PostgreSQL
- Npgsql 4.1.14
- BCrypt.Net-Next 4.0.3

## Функциональность

### Для пользователей:
- Регистрация и авторизация
- Просмотр каталога животных с фильтрами (вид, пол, размер)
- Детальная информация о животном с фотографиями и описанием
- Подача заявок на опекунство
- Добавление животных в избранное
- Личный кабинет с редактированием профиля
- Просмотр статуса своих заявок

### Для администраторов:
- Управление животными (добавление, редактирование, удаление)
- Управление заявками на опекунство (одобрение/отклонение)
- Просмотр всех заявок

## Установка и запуск

### Предварительные требования:
1. .NET Framework 4.8
2. PostgreSQL 12+
3. Visual Studio 2019+ или Rider

### Шаги установки:

1. Клонируйте репозиторий:
```bash
git clone git@github.com:nikralNK/Shelter.git
cd Shelter
```

2. Создайте базу данных PostgreSQL:
```sql
CREATE DATABASE shelter_db;
```

3. Выполните SQL скрипт для создания таблиц:
```bash
psql -U postgres -d shelter_db -f ShelterApp/Database/create_tables.sql
```

4. (Опционально) Загрузите тестовые данные:
```bash
psql -U postgres -d shelter_db -f ShelterApp/Database/insert_test_data.sql
```

5. Настройте строку подключения в файле `ShelterApp/Database/DatabaseConnection.cs`:
```csharp
private static string connectionString = "Host=localhost;Port=5432;Database=shelter_db;Username=postgres;Password=your_password";
```

6. Откройте решение в Visual Studio:
```bash
ShelterApp.sln
```

7. Восстановите NuGet пакеты и соберите проект

8. Запустите приложение (F5)

## Тестовые данные

После загрузки тестовых данных доступны следующие учётные записи:

**Администратор:**
- Логин: `admin`
- Пароль: `admin`

**Пользователь:**
- Логин: `user1`
- Пароль: `user1`

## Структура проекта

```
ShelterApp/
├── Assets/           - Ресурсы (изображения)
├── Database/         - SQL скрипты и подключение к БД
├── Helpers/          - Вспомогательные классы
├── Models/           - Модели данных
├── Repository/       - Слой доступа к данным
├── Services/         - Бизнес-логика
├── Styles/           - XAML стили
├── ViewModels/       - ViewModel для MVVM
├── Views/            - Окна и страницы WPF
│   ├── Pages/        - Страницы приложения
│   ├── LoginWindow   - Окно входа
│   ├── RegisterWindow- Окно регистрации
│   └── MainWindow    - Главное окно
└── App.xaml          - Точка входа приложения
```

## Схема базы данных

Приложение использует следующие таблицы:
- `Animal` - информация о животных
- `Guardian` - опекуны
- `Application` - заявки на опекунство
- `Users` - пользователи системы
- `Favorites` - избранные животные
- `Medical_Record` - медицинские записи
- `Enclosure` - вольеры
- `Shelter_Employee` - сотрудники
- `Veterinarian` - ветеринары
- `Shelter` - информация о приюте

## Дизайн

Приложение реализовано в минималистичном стиле с использованием:
- Цветовая палитра: зелёный (#6B8E6F), бежевый (#F5F1E8)
- Округлые углы элементов (CornerRadius: 10-15px)
- Карточный дизайн с тенями
- Чистый и простой интерфейс

## Лицензия

Этот проект создан в образовательных целях для WorldSkills.
