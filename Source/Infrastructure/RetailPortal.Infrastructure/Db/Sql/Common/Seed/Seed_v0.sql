DO $$
    DECLARE
        seller_id UUID;
        user_id UUID;
        seller_role_id UUID;
        user_role_id UUID;
        business_name TEXT := 'John Doe Sdn. Bhd.';
        first_name TEXT := 'John';
        last_name TEXT := 'Doe';
        seller_role_name TEXT := 'Seller';
        user_role_name TEXT := 'User';
        email TEXT := 'jd@email.com';
        password TEXT := 'password'; -- we're not hashing the password here for simplicity of an example
    BEGIN
        -- 1. Fetch the RoleId from the Role table based on the seller role name
        SELECT "Id" INTO seller_role_id
        FROM "Roles"
        WHERE "Name" = seller_role_name;

        IF seller_role_id IS NULL THEN
            RAISE EXCEPTION 'Role with name "%" not found.', seller_role_name;
        END IF;

        -- 2. Fetch the RoleId from the Role table based on the user role name
        SELECT "Id" INTO user_role_id
        FROM "Roles"
        WHERE "Name" = user_role_name;

        IF user_role_id IS NULL THEN
            RAISE EXCEPTION 'Role with name "%" not found.', user_role_name;
        END IF;

        -- 3. Insert a new seller and generate its UUID
        INSERT INTO "Sellers" ("Id", "BusinessName", "UserId", "CreatedAt", "UpdatedAt")
        VALUES (gen_random_uuid(), business_name, NULL, now(), now())
        RETURNING "Id" INTO seller_id;

        -- 4. Insert a new user and link it to the seller
        INSERT INTO "Users" ("Id", "FirstName", "LastName", "Email", "Password", "SellerId", "CreatedAt", "UpdatedAt")
        VALUES (gen_random_uuid(), first_name, last_name, email, password, seller_id, now(), now())
        RETURNING "Id" INTO user_id;

        -- 5. Update the seller's UserId field (optional if required)
        UPDATE "Sellers"
        SET "UserId" = user_id
        WHERE "Id" = seller_id;

        -- 6. Assign the role to the seller
        INSERT INTO "UserRoles" ("UserId", "RoleId")
        VALUES (user_id, seller_role_id);

        -- 7. Assign the role to the user
        INSERT INTO "UserRoles" ("UserId", "RoleId")
        VALUES (user_id, user_role_id);

        -- Output the inserted IDs for reference
        RAISE NOTICE 'SellerId: %, UserId: %, RoleId: %', seller_id, user_id, seller_role_id;
    END $$;
