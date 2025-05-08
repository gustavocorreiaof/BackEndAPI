CREATE TABLE "User" (
    "Id" BIGSERIAL PRIMARY KEY,
    "Name" TEXT NOT NULL,
    "Password" TEXT NOT NULL,
    "TaxNumber" TEXT NOT NULL,
    "Email" TEXT NOT NULL,
    "Type" SMALLINT NOT NULL,
    "CreationDate" TIMESTAMPTZ NOT NULL,
    "UpdateDate" TIMESTAMPTZ
);

CREATE TABLE "Account" (
    "Id" BIGSERIAL PRIMARY KEY,
    "UserId" BIGINT NOT NULL,
    "Balance" NUMERIC(18, 2) NOT NULL,
    "CreationDate" TIMESTAMPTZ NOT NULL,
    "UpdateDate" TIMESTAMPTZ,
    CONSTRAINT "FK_Account_User_UserId" FOREIGN KEY ("UserId") REFERENCES "User" ("Id") ON DELETE CASCADE
);

CREATE TABLE "Transaction" (
    "Id" SERIAL PRIMARY KEY,
    "PayerId" BIGINT NOT NULL,
    "PayeeId" BIGINT NOT NULL,
    "TransferValue" DECIMAL(18, 2) NOT NULL,
    "TransferDate" TIMESTAMPTZ NOT NULL,
    "CreationDate" TIMESTAMPTZ NOT NULL,
    "UpdateDate" TIMESTAMPTZ,
    CONSTRAINT "FK_Transaction_Payee" FOREIGN KEY ("PayeeId") REFERENCES "User" ("Id"),
    CONSTRAINT "FK_Transaction_Payer" FOREIGN KEY ("PayerId") REFERENCES "User" ("Id")
);

CREATE OR REPLACE PROCEDURE "PerformTransaction"(
    IN paramPayerId BIGINT,
    IN paramPayeeId BIGINT,
    IN paramTransferValue NUMERIC(18, 2),
    IN paramTransferDate TIMESTAMPTZ
)
LANGUAGE plpgsql
AS $$
DECLARE
    payerBalance NUMERIC(18, 2);
BEGIN
    BEGIN
        SELECT "Balance" INTO payerBalance FROM "Account" WHERE "UserId" = paramPayerId;

        IF payerBalance IS NULL THEN
            RAISE EXCEPTION 'Payer not found.' USING ERRCODE = 'P0001';
        END IF;

        IF payerBalance < paramTransferValue THEN
            RAISE EXCEPTION 'Insufficient balance.' USING ERRCODE = 'P0002';
        END IF;

        IF NOT EXISTS (SELECT 1 FROM "Account" WHERE "UserId" = paramPayeeId) THEN
            RAISE EXCEPTION 'Payee not found.' USING ERRCODE = 'P0003';
        END IF;

        UPDATE "Account"
        SET "Balance" = "Balance" - paramTransferValue, "UpdateDate" = paramTransferDate
        WHERE "UserId" = paramPayerId;

        UPDATE "Account"
        SET "Balance" = "Balance" + paramTransferValue, "UpdateDate" = paramTransferDate
        WHERE "UserId" = paramPayeeId;

        INSERT INTO "Transaction" (
            "PayerId", "PayeeId", "TransferValue", "TransferDate", "CreationDate", "UpdateDate"
        )
        VALUES (
            paramPayerId, paramPayeeId, paramTransferValue, paramTransferDate, NOW(), NOW()
        );
    EXCEPTION
        WHEN OTHERS THEN
            RAISE NOTICE 'Transaction failed: %', SQLERRM;
            RAISE;
    END;
END;
$$;

INSERT INTO "User" ("Name", "Password", "TaxNumber", "Email", "Type", "CreationDate")
VALUES 
('Alice Johnson', 'password123', '12345678909', 'alice@example.com', 0, NOW()),
('Bob Smith', 'securepass', '98765432100', 'bob@example.com', 0, NOW()),
('Charlie Brown', 'charlie123', '11144477735', 'charlie@example.com', 0, NOW()),
('Diana Prince', 'wonderwoman', '22233344450', 'diana@example.com', 0, NOW()),
('Ethan Hunt', 'impossible', '77829763000150', 'ethan@example.com', 1, NOW()),
('Fiona Glenanne', 'burnnotice', '51496577000125', 'fiona@example.com', 1, NOW()),
('George Bailey', 'lifeiswonderful', '12312312387', 'george@example.com', 0, NOW()),
('Hannah Wells', 'designated', '00000000191', 'hannah@example.com', 0, NOW()),
('Ian Fleming', 'bond007', '48731072000110', 'ian@example.com', 1, NOW()),
('Jane Doe', 'janedoe2025', '92715204000100', 'jane@example.com', 1, NOW());

INSERT INTO "Account" ("UserId", "Balance", "CreationDate")
VALUES
(1,  1000.00, NOW()),
(2,  1500.50, NOW()),
(3,   750.75, NOW()),
(4,  2200.00, NOW()),
(5,  5000.00, NOW()),
(6,   305.40, NOW()),
(7,  1800.00, NOW()),
(8,   950.90, NOW()),
(9, 10000.00, NOW()),
(10,  125.25, NOW());
