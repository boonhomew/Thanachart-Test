-- Table: public.products

-- DROP TABLE IF EXISTS public.products;

CREATE TABLE IF NOT EXISTS public.products
(
    id uuid NOT NULL,
    product_sku text COLLATE pg_catalog."default" NOT NULL,
    product_name text COLLATE pg_catalog."default" NOT NULL,
    product_price numeric(18,2),
    created_at timestamp with time zone NOT NULL,
    created_by text COLLATE pg_catalog."default" NOT NULL,
    updated_at timestamp with time zone,
    updated_by text COLLATE pg_catalog."default",
    CONSTRAINT "PK_products" PRIMARY KEY (id)
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public.products
    OWNER to test;
-- Index: IX_products_product_sku

-- DROP INDEX IF EXISTS public."IX_products_product_sku";

CREATE UNIQUE INDEX IF NOT EXISTS "IX_products_product_sku"
    ON public.products USING btree
    (product_sku COLLATE pg_catalog."default" ASC NULLS LAST)
    WITH (fillfactor=100, deduplicate_items=True)
    TABLESPACE pg_default;