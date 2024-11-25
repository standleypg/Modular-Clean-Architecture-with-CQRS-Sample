INSERT INTO "Roles" ("Id", "Name", "Description", "CreatedAt", "UpdatedAt")
VALUES
    (gen_random_uuid(), 'Admin', 'Administrator role with full access', now(), now()),
    (gen_random_uuid(), 'User', 'Regular user role with limited access', now(), now()),
    (gen_random_uuid(), 'Seller', 'Seller role with permissions to manage products', now(), now());
