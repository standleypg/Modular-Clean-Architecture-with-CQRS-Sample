-- Seed Users and Roles Function
CREATE OR REPLACE FUNCTION seed_users_and_roles() RETURNS void AS
$$
DECLARE
seller_role_id UUID;
    user_role_id UUID;
    admin_role_id UUID;
    seller_id UUID;
    user_id UUID;
    user_only_id UUID;
    admin_only_id UUID;
    business_name TEXT := 'SellerJohn Sdn. Bhd.';
BEGIN
    -- Fetch role IDs
SELECT "Id" INTO seller_role_id FROM "Roles" WHERE "Name" = 'Seller';
SELECT "Id" INTO user_role_id FROM "Roles" WHERE "Name" = 'User';
SELECT "Id" INTO admin_role_id FROM "Roles" WHERE "Name" = 'Admin';

-- Seed Users (need to seed one by one to get the IDs - otherwise, returning multiple IDs is not supported)
-- Insert Seller User
INSERT INTO "Users" ("Id", "FirstName", "LastName", "Email", "Password", "SellerId", "CreatedAt", "UpdatedAt")
VALUES (gen_random_uuid(), 'SellerJohn', 'Doe', 'seller@email.com', 'password', NULL, now(), now())
    RETURNING "Id" INTO user_id;
INSERT INTO "Sellers" ("Id", "BusinessName", "UserId", "CreatedAt", "UpdatedAt")
VALUES (gen_random_uuid(), business_name, NULL, now(), now())
    RETURNING "Id" INTO seller_id;


-- Insert User-only User
INSERT INTO "Users" ("Id", "FirstName", "LastName", "Email", "Password", "SellerId", "CreatedAt", "UpdatedAt")
VALUES (gen_random_uuid(), 'UserJane', 'Doe', 'user@email.com', 'password', NULL, now(), now())
    RETURNING "Id" INTO user_only_id;

-- Insert Admin User
INSERT INTO "Users" ("Id", "FirstName", "LastName", "Email", "Password", "SellerId", "CreatedAt", "UpdatedAt")
VALUES (gen_random_uuid(), 'AdminJake', 'Smith', 'admin@email.com', 'password', NULL, now(), now())
    RETURNING "Id" INTO admin_only_id;


-- Assign Roles
-- 7. Assign the role to the seler user
INSERT INTO "UserRoles" ("UserId", "RoleId")
VALUES (user_id, seller_role_id);
INSERT INTO "UserRoles" ("UserId", "RoleId")
VALUES (user_id, user_role_id);

-- 8. Assign the role to the user
INSERT INTO "UserRoles" ("UserId", "RoleId")
VALUES (user_only_id, user_role_id);

-- 9. Assign the role to the admin
INSERT INTO "UserRoles" ("UserId", "RoleId")
VALUES
    (admin_only_id, admin_role_id);
END;
$$ LANGUAGE plpgsql;

-- Core Seeding Script: Categories, Roles, Users
DO
$$
BEGIN
        -- Seed Categories
INSERT INTO "Categories" ("Id", "Name", "CreatedAt", "UpdatedAt")
VALUES (gen_random_uuid(), 'Electronics', now(), now()),
       (gen_random_uuid(), 'Books', now(), now()),
       (gen_random_uuid(), 'Clothing', now(), now()),
       (gen_random_uuid(), 'Home & Kitchen', now(), now()),
       (gen_random_uuid(), 'Toys & Games', now(), now())
    ON CONFLICT DO NOTHING;

-- Seed Roles
INSERT INTO "Roles" ("Id", "Name", "Description", "CreatedAt", "UpdatedAt")
VALUES (gen_random_uuid(), 'Admin', 'Administrator role with full access', now(), now()),
       (gen_random_uuid(), 'User', 'Regular user role with limited access', now(), now()),
       (gen_random_uuid(), 'Seller', 'Seller role with permissions to manage products', now(), now())
    ON CONFLICT DO NOTHING;

-- Seeding Users and Roles
PERFORM seed_users_and_roles();
        RAISE NOTICE 'Categories, Roles, and Users seeded successfully.';
END;
$$ LANGUAGE plpgsql;
