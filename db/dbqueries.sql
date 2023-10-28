--Get all products of selected brand
SELECT
product_id AS "id",
products.name AS product,
price,
average_rating AS rating,
brands.name AS brand,
categories.name AS category
FROM products
JOIN brands ON products.brand_id=brands.brand_id
JOIN categories ON products.category_id=categories.category_id
WHERE brands.name='Zara';

--Get all product variations for the selected product
SELECT
prod_variant_id AS "id",
"name" AS product,
color_name AS color,
size_name AS "size",
quantity,
sku
FROM product_variants
JOIN products ON product_variants.product_id=products.product_id
JOIN colors ON product_variants.color_id=colors.color_id
JOIN sizes ON product_variants.size_id=sizes.size_id
ORDER BY product,color;

--Select all brands with the number of their products respectively. Order by the number of products.
SELECT 
brands.name AS brand,
SUM(CASE WHEN products.brand_id IS NULL THEN 0 ELSE 1 END) AS total
FROM brands
LEFT JOIN products ON products.brand_id=brands.brand_id
GROUP BY brands.name
ORDER BY total DESC;

--Get all products for a given category and section.
SELECT 
product_id AS "id",
sections.name AS "section",
categories.name AS category,
products.name AS product,
price,
average_rating AS rating,
brands.name AS brand
FROM products
JOIN brands ON products.brand_id=brands.brand_id
JOIN categories ON products.category_id=categories.category_id
JOIN section_categories ON categories.category_id=sc_category_id
JOIN sections ON section_id=sc_section_id
WHERE categories.name='shoes' AND sections.name='women';

--Get all completed orders with a given product. Order from newest to latest.
SELECT 
orders.order_id,
user_id,
products.name AS product,
status,
total_price AS order_price,
address_id,
created_at AS date
FROM orders
JOIN order_items ON orders.order_id=order_items.order_id
JOIN product_variants ON product_var_id=prod_variant_id
JOIN products ON product_variants.product_id=products.product_id
WHERE products.name='dress1' AND status='completed'
ORDER BY date ASC;

--Get all reviews for a given product. Implement this as a viewtable which contains rating, comment and info of a person who left a comment.
SELECT 
review_id AS "id",
products.name AS product,
rating,
comment_text AS "comment",
CONCAT(first_name, ' ', last_name) AS user_name
FROM reviews
JOIN users ON users.user_id=reviews.user_id
JOIN products ON products.product_id=reviews.product_id
WHERE products.name='sneakers1'
