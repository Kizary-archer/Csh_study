/*==============================================================*/
/* DBMS name:      SAP SQL Anywhere 17                          */
/* Created on:     14.04.2019 20:51:17                          */
/*==============================================================*/


/*==============================================================*/
/* Table: list_of_products                                      */
/*==============================================================*/
create or replace table list_of_products 
(
   products_name        varchar                        not null,
   date_of_receipt      integer                        not null,
   count_of_unit        integer                        null,
   total_cost           integer                        null,
   constraint PK_LIST_OF_PRODUCTS primary key clustered (products_name)
);

/*==============================================================*/
/* Index: list_of_products_PK                                   */
/*==============================================================*/
create unique clustered index list_of_products_PK on list_of_products (
products_name ASC
);

/*==============================================================*/
/* Table: product                                               */
/*==============================================================*/
create or replace table product 
(
   products_name        varchar                        not null,
   cost_per_piece       integer                        not null,
   article              varchar                        not null,
   unit                 varchar                        not null,
   constraint PK_PRODUCT primary key clustered (products_name)
);

/*==============================================================*/
/* Index: product_PK                                            */
/*==============================================================*/
create unique clustered index product_PK on product (
products_name ASC
);

/*==============================================================*/
/* Table: write_off                                             */
/*==============================================================*/
create or replace table write_off 
(
   products_name        varchar                        not null,
   date_off_end         varchar                        not null,
   total_cost_off       integer                        not null,
   "unit's_count"       integer                        not null,
   constraint PK_WRITE_OFF primary key clustered (products_name)
);

/*==============================================================*/
/* Index: write_off_PK                                          */
/*==============================================================*/
create unique clustered index write_off_PK on write_off (
products_name ASC
);

alter table list_of_products
   add constraint FK_LIST_OF__REFERENCE_WRITE_OF foreign key (products_name)
      references write_off (products_name)
      on update restrict
      on delete restrict;

alter table product
   add constraint FK_PRODUCT_REFERENCE_LIST_OF_ foreign key (products_name)
      references list_of_products (products_name)
      on update restrict
      on delete restrict;

