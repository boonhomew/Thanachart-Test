-- Table: public.stocks

-- DROP TABLE IF EXISTS public.stocks;

CREATE TABLE IF NOT EXISTS public.stocks
(
    id uuid NOT NULL,
    product_id uuid NOT NULL,
    quantity integer NOT NULL,
    created_at timestamp with time zone NOT NULL,
    created_by text COLLATE pg_catalog."default" NOT NULL,
    updated_at timestamp with time zone,
    updated_by text COLLATE pg_catalog."default",
    CONSTRAINT "PK_stocks" PRIMARY KEY (id),
    CONSTRAINT "FK_stocks_products_product_id" FOREIGN KEY (product_id)
        REFERENCES public.products (id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE CASCADE
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public.stocks
    OWNER to test;
-- Index: IX_stocks_product_id

-- DROP INDEX IF EXISTS public."IX_stocks_product_id";

CREATE UNIQUE INDEX IF NOT EXISTS "IX_stocks_product_id"
    ON public.stocks USING btree
    (product_id ASC NULLS LAST)
    WITH (fillfactor=100, deduplicate_items=True)
    TABLESPACE pg_default;