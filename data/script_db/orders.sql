-- Table: public.orders

-- DROP TABLE IF EXISTS public.orders;

CREATE TABLE IF NOT EXISTS public.orders
(
    id uuid NOT NULL,
    cart_id uuid NOT NULL,
    total_amount numeric(18,2) NOT NULL,
    status text COLLATE pg_catalog."default" NOT NULL,
    price numeric(18,2) NOT NULL,
    created_at timestamp with time zone NOT NULL,
    created_by text COLLATE pg_catalog."default" NOT NULL,
    updated_at timestamp with time zone,
    updated_by text COLLATE pg_catalog."default",
    CONSTRAINT "PK_orders" PRIMARY KEY (id),
    CONSTRAINT "FK_orders_carts_cart_id" FOREIGN KEY (cart_id)
        REFERENCES public.carts (id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE CASCADE
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public.orders
    OWNER to test;
-- Index: IX_orders_cart_id

-- DROP INDEX IF EXISTS public."IX_orders_cart_id";

CREATE INDEX IF NOT EXISTS "IX_orders_cart_id"
    ON public.orders USING btree
    (cart_id ASC NULLS LAST)
    WITH (fillfactor=100, deduplicate_items=True)
    TABLESPACE pg_default;