-- SQLite does not have GUID generation functions, so we have to generate them manually
CREATE FUNCTION GenerateUUID() RETURNS TEXT AS $$
BEGIN
RETURN lower(
    substr(hex(randomblob(4)), 1, 8) || '-' ||
    substr(hex(randomblob(2)), 1, 4) || '-' ||
    '4' || substr(hex(randomblob(2)), 2, 3) || '-' ||
    substr('89ab', abs(random() % 4) + 1, 1) ||
    substr(hex(randomblob(2)), 2, 3) || '-' ||
    substr(hex(randomblob(6)), 1, 12)
       );
END;
$$;
