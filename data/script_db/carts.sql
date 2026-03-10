-- Table: public.carts

-- DROP TABLE IF EXISTS public.carts;

CREATE TABLE IF NOT EXISTS public.carts
(
    id uuid NOT NULL,
    status text COLLATE pg_catalog."default" NOT NULL,
    created_at timestamp with time zone NOT NULL,
    created_by text COLLATE pg_catalog."default" NOT NULL,
    updated_at timestamp with time zone,
    updated_by text COLLATE pg_catalog."default",
    CONSTRAINT "PK_carts" PRIMARY KEY (id)
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public.carts
    OWNER to test;