DO $$
    DECLARE
        currencies TEXT[] := ARRAY['USD', 'SGD', 'MYR'];
        category_ids UUID[];
        seller_ids UUID[];
        i INT;
        random_category UUID;
        random_seller UUID;
        random_name TEXT;
        random_desc TEXT;
        random_amount INT;
        random_currency TEXT;
        random_quantity INT;
    BEGIN
        -- Fetch all Category IDs into an array
        SELECT ARRAY_AGG("Id") INTO category_ids FROM "Categories";

        -- Fetch all Seller IDs into an array (assuming sellers exist in the Sellers table)
        SELECT ARRAY_AGG("Id") INTO seller_ids FROM "Sellers";

        -- Ensure there are categories and sellers
        IF category_ids IS NULL OR array_length(category_ids, 1) = 0 THEN
            RAISE EXCEPTION 'No categories found!';
        END IF;

        IF seller_ids IS NULL OR array_length(seller_ids, 1) = 0 THEN
            RAISE EXCEPTION 'No sellers found!';
        END IF;

        -- Loop to insert 100,000 products
        FOR i IN 1..100000 LOOP
                random_category := category_ids[ceil(random() * array_length(category_ids, 1))];
                random_seller := seller_ids[ceil(random() * array_length(seller_ids, 1))];
                random_name := 'Product ' || i || ' ' || substr(md5(random()::text), 1, 8);
                random_desc := 'Description ' || i || ' ' || substr(md5(random()::text), 1, 8);
                random_amount := (random() * 1000)::INT + 1;
                random_quantity := (random() * 1000)::INT + 1;
                random_currency := currencies[ceil(random() * array_length(currencies, 1))];

                INSERT INTO "Products" ("Id", "Name", "Description", "Amount", "Currency", "Quantity", "ImageUrl", "CategoryId", "SellerId", "CreatedAt", "UpdatedAt")
                VALUES (gen_random_uuid(), random_name, random_desc,random_amount, random_currency, random_quantity, null, random_category, random_seller, now(), now());
            END LOOP;

        RAISE NOTICE 'Seeded 100,000 products successfully.';
    END $$;
