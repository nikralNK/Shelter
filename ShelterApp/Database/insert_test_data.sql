INSERT INTO Shelter (Name, Address, Number) VALUES
('Приют "Добрые руки"', 'ул. Лесная, 15', '+7-900-123-45-67');

INSERT INTO Enclosure (Number, Capacity, TypeOfEnclosure) VALUES
('A-1', 5, 'Крытый вольер'),
('A-2', 3, 'Открытый вольер'),
('B-1', 10, 'Общий вольер'),
('C-1', 2, 'Карантинный вольер');

INSERT INTO Veterinarian (Name, DateOfAccreditation) VALUES
('Иванова Мария Петровна', '2015-06-15'),
('Сидоров Петр Иванович', '2018-03-20');

INSERT INTO Shelter_Employee (Name, JobTitle) VALUES
('Козлова Анна Сергеевна', 'Менеджер по работе с посетителями'),
('Петров Иван Дмитриевич', 'Директор приюта');

INSERT INTO Guardian (Name, Number, Email, Address) VALUES
('Смирнов Алексей', '+7-911-111-11-11', 'smirnov@example.com', 'ул. Пушкина, 10'),
('Васильева Ольга', '+7-922-222-22-22', 'vasileva@example.com', 'пр. Ленина, 45');

INSERT INTO Animal (Name, Type, Breed, DateOfBirth, Id_Enclosure, CurrentStatus, Gender, Size, Temperament, Photo1) VALUES
('Рекс', 'Собака', 'Немецкая овчарка', '2020-05-10', 1, 'Новый', 'Самец', 'Крупный', 'Дружелюбный, активный, хорошо обучаем', NULL),
('Мурка', 'Кошка', 'Британская', '2021-03-15', 2, 'Новый', 'Самка', 'Средний', 'Спокойная, ласковая, любит детей', NULL),
('Шарик', 'Собака', 'Дворняга', '2019-08-20', 1, 'В рассмотрении', 'Самец', 'Средний', 'Игривый, добрый, преданный', NULL),
('Барсик', 'Кошка', 'Сиамская', '2022-01-05', 2, 'Новый', 'Самец', 'Средний', 'Независимый, умный, разговорчивый', NULL),
('Белка', 'Собака', 'Лабрадор', '2021-07-12', 3, 'Новый', 'Самка', 'Крупный', 'Энергичная, любит играть, дружелюбная', NULL);

INSERT INTO Medical_Record (Id_Animal, Id_Veterinarian, DateOfInspection, Diagnosis, Treatment) VALUES
(1, 1, '2024-01-15', 'Общий осмотр', 'Вакцинация'),
(2, 2, '2024-01-20', 'Профилактический осмотр', 'Обработка от паразитов'),
(3, 1, '2024-02-01', 'Небольшая травма лапы', 'Обработка раны, покой');

INSERT INTO Users (Username, PasswordHash, Email, FullName, Role, Id_Guardian) VALUES
('admin', '$2a$11$7oKJ8hP5N0Y4UQzXJh4Hf.LZWpLJCc2qYxH8sK6rP9mN4vT2wE8yO', 'admin@shelter.com', 'Администратор', 'Admin', NULL),
('user1', '$2a$11$7oKJ8hP5N0Y4UQzXJh4Hf.LZWpLJCc2qYxH8sK6rP9mN4vT2wE8yO', 'user1@example.com', 'Смирнов Алексей', 'User', 1);
