
create table list_of_products 
(
   products_name        varchar(30)                        not null,
   date_of_receipt      date                       not null,
   count_of_unit        integer                        null,
   total_cost           integer                        null,
   constraint PK_LIST_OF_PRODUCTS primary key clustered (products_name)
);

create table product 
(
   products_name        varchar(30)                        not null,
   cost_per_piece       integer                        not null,
   article              varchar(30)                        not null,
   unit                 varchar(30)                        not null,
   constraint PK_PRODUCT primary key clustered (products_name)
);

create  table write_off 
(
   products_name        varchar(30)                        not null,
   date_off_end         date                          not null,
   total_cost_off       integer                        not null,
   units_count			integer                        not null,
   constraint PK_WRITE_OFF primary key clustered (products_name)
);

alter table list_of_products
   add constraint FK_LIST_OF__REFERENCE_WRITE_OF foreign key (products_name)
      references write_off (products_name)


alter table product
   add constraint FK_PRODUCT_REFERENCE_LIST_OF_ foreign key (products_name)
      references list_of_products (products_name)


