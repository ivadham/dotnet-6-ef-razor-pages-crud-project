# dotnet-6-ef-razor-pages-crud-project

Similar to [dotnet-6-razor-pages-crud-project](https://github.com/ivadham/dotnet-6-razor-pages-crud-project), updated with EF Core:

## Database query

```
INSERT INTO Status (StatusName)
VALUES
('active'),
('deactive');

INSERT INTO Roles (RoleName)
VALUES
('su'),
('admin'),
('customer'),
('guest');

INSERT INTO users(UserName, Email, Created_at)
VALUES
('Bill Gates', 'bill.gates@microsoft.com', '2019-08-20 10:22:34'),
('Elon Musk', 'elon.musk@spacex.com', '2019-09-21 10:22:34'),
('Steve Jobs', 'steve.jobs@apple.com', '2019-10-06 10:22:34'),
('Shuhei Yoshida', 'shuhei.yoshida@playstation.com', '2019-11-30 10:22:34'),
('John McAfee', 'john.mcafee@mcafee.com', '2020-02-19 10:22:34'),
('Phil Spencer', 'phil.spencer@xbox.com', '2021-03-01 10:22:34');


INSERT INTO UserStatus(UserId, StatusId)
VALUES
(1, 1),
(2, 1),
(3, 2),
(4, 1),
(5, 2),
(6, 1);

INSERT INTO UserRoles(UserId, RoleId)
VALUES
(1, 1),
(2, 2),
(3, 2),
(4, 3),
(5, 4),
(6, 3);
```

## Contributing

For major changes, please open an issue first
to discuss what you would like to change.

## COPYRIGHT/COPYLEFT

December 2023
