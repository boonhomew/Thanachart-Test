-- Table: public.order_items

-- DROP TABLE IF EXISTS public.order_items;

CREATE TABLE IF NOT EXISTS public.order_items
(
    id uuid NOT NULL,
    order_id uuid NOT NULL,
    product_id uuid NOT NULL,
    quantity integer NOT NULL,
    price numeric(18,2) NOT NULL,
    created_at timestamp with time zone NOT NULL,
    created_by text COLLATE pg_catalog."default" NOT NULL,
    updated_at timestamp with time zone,
    updated_by text COLLATE pg_catalog."default",
    CONSTRAINT "PK_order_items" PRIMARY KEY (id),
    CONSTRAINT "FK_order_items_orders_order_id" FOREIGN KEY (order_id)
        REFERENCES public.orders (id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE CASCADE,
    CONSTRAINT "FK_order_items_products_product_id" FOREIGN KEY (product_id)
        REFERENCES public.products (id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE CASCADE
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public.order_items
    OWNER to test;
-- Index: IX_order_items_order_id

-- DROP INDEX IF EXISTS public."IX_order_items_order_id";

CREATE INDEX IF NOT EXISTS "IX_order_items_order_id"
    ON public.order_items USING btree
    (order_id ASC NULLS LAST)
    WITH (fillfactor=100, deduplicate_items=True)
    TABLESPACE pg_default;
-- Index: IX_order_items_product_id

-- DROP INDEX IF EXISTS public."IX_order_items_product_id";

CREATE INDEX IF NOT EXISTS "IX_order_items_product_id"
    ON public.order_items USING btree
    (product_id ASC NULLS LAST)
    WITH (fillfactor=100, deduplicate_items=True)
    TABLESPACE pg_default;