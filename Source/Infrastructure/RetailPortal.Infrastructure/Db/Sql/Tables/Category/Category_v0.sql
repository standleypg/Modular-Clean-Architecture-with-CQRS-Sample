INSERT INTO "Categories" ("Id", "Name", "CreatedAt", "UpdatedAt")
VALUES (gen_random_uuid(), 'Electronics', now(), now()),
       (gen_random_uuid(), 'Books', now(), now()),
       (gen_random_uuid(), 'Clothing', now(), now()),
       (gen_random_uuid(), 'Home & Kitchen', now(), now()),
       (gen_random_uuid(), 'Toys & Games', now(), now());
