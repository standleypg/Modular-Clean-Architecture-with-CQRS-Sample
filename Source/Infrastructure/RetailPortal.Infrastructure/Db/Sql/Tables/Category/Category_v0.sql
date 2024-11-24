INSERT INTO Categories (Id, Name, CreatedAt, UpdatedAt)
VALUES ('00000000-0000-0000-0000-000000000000', 'Electronics', datetime('now'), datetime('now')),
       (GenerateUUID(), 'Books', datetime('now'), datetime('now')),
       (GenerateUUID(), 'Clothing', datetime('now'), datetime('now')),
       (GenerateUUID(), 'Home & Kitchen', datetime('now'), datetime('now')),
       (GenerateUUID(), 'Toys & Games', datetime('now'), datetime('now'));
