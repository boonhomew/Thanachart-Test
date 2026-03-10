-- Table: public.cart_items

-- DROP TABLE IF EXISTS public.cart_items;

CREATE TABLE IF NOT EXISTS public.cart_items
(
    id uuid NOT NULL,
    cart_id uuid NOT NULL,
    product_id uuid NOT NULL,
    quantity integer NOT NULL,
    price numeric(18,2) NOT NULL,
    created_at timestamp with time zone NOT NULL,
    created_by text COLLATE pg_catalog."default" NOT NULL,
    updated_at timestamp with time zone,
    updated_by text COLLATE pg_catalog."default",
    CONSTRAINT "PK_cart_items" PRIMARY KEY (id),
    CONSTRAINT "FK_cart_items_carts_cart_id" FOREIGN KEY (cart_id)
        REFERENCES public.carts (id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE CASCADE,
    CONSTRAINT "FK_cart_items_products_product_id" FOREIGN KEY (product_id)
        REFERENCES public.products (id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE CASCADE
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public.cart_items
    OWNER to test;
-- Index: IX_cart_items_cart_id

-- DROP INDEX IF EXISTS public."IX_cart_items_cart_id";

CREATE INDEX IF NOT EXISTS "IX_cart_items_cart_id"
    ON public.cart_items USING btree
    (cart_id ASC NULLS LAST)
    WITH (fillfactor=100, deduplicate_items=True)
    TABLESPACE pg_default;
-- Index: IX_cart_items_product_id

-- DROP INDEX IF EXISTS public."IX_cart_items_product_id";

CREATE INDEX IF NOT EXISTS "IX_cart_items_product_id"
    ON public.cart_items USING btree
    (product_id ASC NULLS LAST)
    WITH (fillfactor=100, deduplicate_items=True)
    TABLESPACE pg_default;