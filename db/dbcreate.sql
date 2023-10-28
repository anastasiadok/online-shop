BEGIN;

CREATE TYPE public.status_type AS ENUM
    ('in review', 'in delivery', 'completed', 'cancelled');

CREATE TYPE public.user_type AS ENUM
    ('admin', 'user');

CREATE TABLE IF NOT EXISTS public.addresses
(
    address_id integer NOT NULL,
    user_id integer NOT NULL,
    address character varying(100) NOT NULL,
    CONSTRAINT adresses_pkey PRIMARY KEY (address_id)
);

CREATE TABLE IF NOT EXISTS public.users
(
    user_id integer NOT NULL,
    role user_type NOT NULL,
    email character varying(40) NOT NULL,
    password character varying(256) NOT NULL,
    phone character varying(20) NOT NULL,
    first_name character varying(30) NOT NULL,
    last_name character varying(50) NOT NULL,
    CONSTRAINT users_pkey PRIMARY KEY (user_id),
    CONSTRAINT users_emai_phone_key UNIQUE (email, phone)
);

CREATE TABLE IF NOT EXISTS public.cart_items
(
    product_var_id integer NOT NULL,
    user_id integer NOT NULL,
    quantity integer NOT NULL,
    CONSTRAINT pk_cart_item PRIMARY KEY (user_id, product_var_id)
);

CREATE TABLE IF NOT EXISTS public.product_variants
(
    prod_variant_id integer NOT NULL,
    color_id integer NOT NULL,
    size_id integer NOT NULL,
    product_id integer NOT NULL,
    quantity integer NOT NULL,
    sku character varying(256) NOT NULL,
    CONSTRAINT product_variants_pkey PRIMARY KEY (prod_variant_id),
    CONSTRAINT product_variants_sku_key UNIQUE (sku)
);

CREATE TABLE IF NOT EXISTS public.colors
(
    color_id integer NOT NULL,
    color_name character varying(20) NOT NULL,
    CONSTRAINT colors_pkey PRIMARY KEY (color_id),
    CONSTRAINT colors_color_name_key UNIQUE (color_name)
);

CREATE TABLE IF NOT EXISTS public.products
(
    product_id integer NOT NULL,
    brand_id integer NOT NULL,
    category_id integer NOT NULL,
    name character varying(50) NOT NULL,
    price money NOT NULL,
    average_rating real,
    CONSTRAINT products_pkey PRIMARY KEY (product_id),
    CONSTRAINT products_name_key UNIQUE (name)
);

CREATE INDEX IF NOT EXISTS prod_by_brand_category
    ON public.products USING btree
    (brand_id ASC NULLS LAST, category_id ASC NULLS LAST)

CREATE TABLE IF NOT EXISTS public.brands
(
    brand_id integer NOT NULL,
    name character varying(30) NOT NULL,
    CONSTRAINT brands_pkey PRIMARY KEY (brand_id),
    CONSTRAINT brands_name_key UNIQUE (name)
);

CREATE TABLE IF NOT EXISTS public.categories
(
    category_id integer NOT NULL,
    name character varying(20) NOT NULL,
    parent_category_id integer,
    CONSTRAINT categories_pkey PRIMARY KEY (category_id),
    CONSTRAINT categories_name_key UNIQUE (name)
);

CREATE TABLE IF NOT EXISTS public.section_categories
(
    sc_category_id integer NOT NULL,
    sc_section_id integer NOT NULL,
    CONSTRAINT pk_sc PRIMARY KEY (sc_category_id, sc_section_id)
);

CREATE TABLE IF NOT EXISTS public.sections
(
    section_id integer NOT NULL,
    name character varying(20) NOT NULL,
    CONSTRAINT sections_pkey PRIMARY KEY (section_id),
    CONSTRAINT sections_name_key UNIQUE (name)
);

CREATE TABLE IF NOT EXISTS public.reviews
(
    review_id integer NOT NULL,
    product_id integer NOT NULL,
    user_id integer NOT NULL,
    rating integer NOT NULL,
    comment_text character varying(2000),
    title character varying(50),
    created_at timestamp(6) without time zone NOT NULL,
    CONSTRAINT reviews_pkey PRIMARY KEY (review_id)
);

CREATE INDEX IF NOT EXISTS by_product_rating
    ON public.reviews USING btree
    (product_id ASC NULLS LAST, rating ASC NULLS LAST);

CREATE TABLE IF NOT EXISTS public.media
(
    medium_id integer NOT NULL,
    product_id integer NOT NULL,
    bytes bytea NOT NULL,
    file_type character varying(10) NOT NULL,
    file_name character varying(50) NOT NULL,
    CONSTRAINT media_pkey PRIMARY KEY (medium_id),
    CONSTRAINT media_file_type_file_name_key UNIQUE (file_type, file_name)
);

CREATE TABLE IF NOT EXISTS public.sizes
(
    size_id integer NOT NULL,
    size_name character varying(10) NOT NULL,
    CONSTRAINT sizes_pkey PRIMARY KEY (size_id),
    CONSTRAINT sizes_size_name_key UNIQUE (size_name)
);

CREATE TABLE IF NOT EXISTS public.order_items
(
    product_var_id integer NOT NULL,
    order_id integer NOT NULL,
    quantity integer NOT NULL,
    CONSTRAINT pk_order_item PRIMARY KEY (product_var_id, order_id)
);

CREATE TABLE IF NOT EXISTS public.orders
(
    order_id integer NOT NULL,
    user_id integer NOT NULL,
    total_price money NOT NULL,
    address_id integer NOT NULL,
    created_at timestamp(6) without time zone NOT NULL,
    status status_type NOT NULL,
    CONSTRAINT orders_pkey PRIMARY KEY (order_id)
);

CREATE TABLE IF NOT EXISTS public.order_transactions
(
    order_id integer NOT NULL,
    status status_type NOT NULL,
    updated_at timestamp(6) without time zone NOT NULL,
    CONSTRAINT pk_order_tr PRIMARY KEY (order_id, status)
);

CREATE INDEX IF NOT EXISTS by_order_status
    ON public.order_transactions USING btree
    (order_id ASC NULLS LAST, status ASC NULLS LAST)
    TABLESPACE pg_default;
	
ALTER TABLE IF EXISTS public.addresses
    ADD CONSTRAINT adresses_user_id_fkey FOREIGN KEY (user_id)
    REFERENCES public.users (user_id) MATCH SIMPLE
    ON UPDATE NO ACTION
    ON DELETE NO ACTION;


ALTER TABLE IF EXISTS public.cart_items
    ADD CONSTRAINT cart_items_product_var_id_fkey FOREIGN KEY (product_var_id)
    REFERENCES public.product_variants (prod_variant_id) MATCH SIMPLE
    ON UPDATE NO ACTION
    ON DELETE NO ACTION;


ALTER TABLE IF EXISTS public.cart_items
    ADD CONSTRAINT cart_items_user_id_fkey FOREIGN KEY (user_id)
    REFERENCES public.users (user_id) MATCH SIMPLE
    ON UPDATE NO ACTION
    ON DELETE NO ACTION;

ALTER TABLE IF EXISTS public.product_variants
    ADD CONSTRAINT product_variants_color_id_fkey FOREIGN KEY (color_id)
    REFERENCES public.colors (color_id) MATCH SIMPLE
    ON UPDATE NO ACTION
    ON DELETE NO ACTION;


ALTER TABLE IF EXISTS public.product_variants
    ADD CONSTRAINT product_variants_product_id_fkey FOREIGN KEY (product_id)
    REFERENCES public.products (product_id) MATCH SIMPLE
    ON UPDATE NO ACTION
    ON DELETE NO ACTION;

ALTER TABLE IF EXISTS public.product_variants
    ADD CONSTRAINT product_variants_size_id_fkey FOREIGN KEY (size_id)
    REFERENCES public.sizes (size_id) MATCH SIMPLE
    ON UPDATE NO ACTION
    ON DELETE NO ACTION;

ALTER TABLE IF EXISTS public.products
    ADD CONSTRAINT products_brand_id_fkey FOREIGN KEY (brand_id)
    REFERENCES public.brands (brand_id) MATCH SIMPLE
    ON UPDATE NO ACTION
    ON DELETE NO ACTION;

ALTER TABLE IF EXISTS public.products
    ADD CONSTRAINT products_category_id_fkey FOREIGN KEY (category_id)
    REFERENCES public.categories (category_id) MATCH SIMPLE
    ON UPDATE NO ACTION
    ON DELETE NO ACTION;

ALTER TABLE IF EXISTS public.categories
    ADD CONSTRAINT categories_parent_category_id_fkey FOREIGN KEY (parent_category_id)
    REFERENCES public.categories (category_id) MATCH SIMPLE
    ON UPDATE NO ACTION
    ON DELETE NO ACTION;

ALTER TABLE IF EXISTS public.section_categories
    ADD CONSTRAINT section_categories_sc_category_id_fkey FOREIGN KEY (sc_category_id)
    REFERENCES public.categories (category_id) MATCH SIMPLE
    ON UPDATE NO ACTION
    ON DELETE NO ACTION;

ALTER TABLE IF EXISTS public.section_categories
    ADD CONSTRAINT section_categories_sc_section_id_fkey FOREIGN KEY (sc_section_id)
    REFERENCES public.sections (section_id) MATCH SIMPLE
    ON UPDATE NO ACTION
    ON DELETE NO ACTION;

ALTER TABLE IF EXISTS public.reviews
    ADD CONSTRAINT reviews_product_id_fkey FOREIGN KEY (product_id)
    REFERENCES public.products (product_id) MATCH SIMPLE
    ON UPDATE NO ACTION
    ON DELETE NO ACTION;

ALTER TABLE IF EXISTS public.reviews
    ADD CONSTRAINT reviews_user_id_fkey FOREIGN KEY (user_id)
    REFERENCES public.users (user_id) MATCH SIMPLE
    ON UPDATE NO ACTION
    ON DELETE NO ACTION;

ALTER TABLE IF EXISTS public.media
    ADD CONSTRAINT media_product_id_fkey FOREIGN KEY (product_id)
    REFERENCES public.products (product_id) MATCH SIMPLE
    ON UPDATE NO ACTION
    ON DELETE NO ACTION;

ALTER TABLE IF EXISTS public.order_items
    ADD CONSTRAINT order_items_order_id_fkey FOREIGN KEY (order_id)
    REFERENCES public.orders (order_id) MATCH SIMPLE
    ON UPDATE NO ACTION
    ON DELETE NO ACTION;

ALTER TABLE IF EXISTS public.order_items
    ADD CONSTRAINT order_items_product_var_id_fkey FOREIGN KEY (product_var_id)
    REFERENCES public.product_variants (prod_variant_id) MATCH SIMPLE
    ON UPDATE NO ACTION
    ON DELETE NO ACTION;

ALTER TABLE IF EXISTS public.orders
    ADD CONSTRAINT orders_adress_id_fkey FOREIGN KEY (adress_id)
    REFERENCES public.addresses (address_id) MATCH SIMPLE
    ON UPDATE NO ACTION
    ON DELETE NO ACTION;

ALTER TABLE IF EXISTS public.orders
    ADD CONSTRAINT orders_user_id_fkey FOREIGN KEY (user_id)
    REFERENCES public.users (user_id) MATCH SIMPLE
    ON UPDATE NO ACTION
    ON DELETE NO ACTION;

ALTER TABLE IF EXISTS public.order_transactions
    ADD CONSTRAINT order_transactions_order_id_fkey FOREIGN KEY (order_id)
    REFERENCES public.orders (order_id) MATCH SIMPLE
    ON UPDATE NO ACTION
    ON DELETE NO ACTION;
	
END;