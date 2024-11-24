INSERT INTO Roles (Id, Name, Description, CreatedAt, UpdatedAt)
VALUES
    ('00000000-0000-0000-0000-000000000000', 'Admin', 'Administrator role with full access', datetime('now'), datetime('now')),
    ( GenerateUUID(), 'User', 'Regular user role with limited access', datetime('now'), datetime('now')),
    ( GenerateUUID(), 'Seller', 'Seller role with permissions to manage products', datetime('now'), datetime('now'));
