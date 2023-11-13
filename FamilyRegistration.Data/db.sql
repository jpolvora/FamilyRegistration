CREATE DATABASE "FamilyRegistration"
    WITH
    OWNER = postgres
    ENCODING = 'UTF8'
    CONNECTION LIMIT = -1
    IS_TEMPLATE = False;


-- Table: public.Pessoas

-- DROP TABLE IF EXISTS public."Pessoas";

CREATE TABLE IF NOT EXISTS public."Pessoas"
(
    id uuid NOT NULL,
    full_name character varying(100) COLLATE pg_catalog."default",
    gender character varying(10) COLLATE pg_catalog."default",
    age numeric,
    income numeric(8,2),
    parent uuid,
    dependents numeric,
    CONSTRAINT "Pessoas_pkey" PRIMARY KEY (id)
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public."Pessoas"
    OWNER to postgres;

-- Column: public."Pessoas".score

-- ALTER TABLE IF EXISTS public."Pessoas" DROP COLUMN IF EXISTS score;

ALTER TABLE IF EXISTS public."Pessoas"
    ADD COLUMN score integer;