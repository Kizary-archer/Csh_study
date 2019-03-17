
create table clients 
(
   id_passport          integer                        null,
   id_client            integer                        not null,
   name                 varchar(30)                    null,
   surname              varchar(30)                    null,
   patronymic           varchar(30)                    null,
   phone                integer                        null,
   id_pasport           integer                        null,
   constraint PK_CLIENTS primary key clustered (id_client)
);

/*==============================================================*/
/* Index: Relationship_2_FK                                     */
/*==============================================================*/
create index Relationship_2_FK on clients (
id_passport ASC
);

/*==============================================================*/
/* Table: contracts                                             */
/*==============================================================*/
create table contracts 
(
   id_contracts         integer                        not null,
   id_client            integer                        null,
   id_tariffs           integer                        null,
   id_status            integer                        null,
   date_of_conclusion   date                           null,
   status               varchar(30)                    null,
   constraint PK_CONTRACTS primary key clustered (id_contracts)
);

/*==============================================================*/
/* Index: содержит_FK                                           */
/*==============================================================*/
create index содержит_FK on contracts (
id_status ASC
);

/*==============================================================*/
/* Table: passport                                              */
/*==============================================================*/
create table passport 
(
   id_passport          integer                        not null,
   id_client            integer                        null,
   Date_issues          date                           null,
   Date_of_birth        date                           null,
   issued_by            varchar(30)                    null,
   constraint PK_PASSPORT primary key clustered (id_passport)
);



/*==============================================================*/
/* Index: Relationship_1_FK                                     */
/*==============================================================*/
create index Relationship_1_FK on passport (
id_client ASC
);

/*==============================================================*/
/* Table: product                                               */
/*==============================================================*/
create table product 
(
   id_cell              integer                        not null,
   id_product           integer                        not null,
   id_client            integer                        null,
   name_product         varchar(30)                    null,
   number_product       integer                        null,
   id_contracts         integer                        null,
   id_cells             integer                        null,
   constraint PK_PRODUCT primary key clustered (id_cell, id_product)
);

/*==============================================================*/
/* Index: product_PK                                            */
/*==============================================================*/
create unique clustered index product_PK on product (
id_cell ASC,
id_product ASC
);

/*==============================================================*/
/* Index: принадлежит_FK                                        */
/*==============================================================*/
create index принадлежит_FK on product (
id_client ASC
);

/*==============================================================*/
/* Index: хранится_FK                                           */
/*==============================================================*/
create index хранится_FK on product (
id_cell ASC
);

/*==============================================================*/
/* Table: status_contracts                                      */
/*==============================================================*/
create table status_contracts 
(
   id_contracts         integer                        null,
   id_status            integer                        not null,
   status               varchar(30)                    null,
   constraint PK_STATUS_CONTRACTS primary key clustered (id_status)
);

/*==============================================================*/
/* Index: содержит2_FK                                          */
/*==============================================================*/
create index содержит2_FK on status_contracts (
id_contracts ASC
);

/*==============================================================*/
/* Table: storage_cells                                         */
/*==============================================================*/
create table storage_cells 
(
   id_cell              integer                        not null,
   name_cell            varchar(30)                    null,
   constraint PK_STORAGE_CELLS primary key clustered (id_cell)
);

/*==============================================================*/
/* Index: storage_cells_PK                                      */
/*==============================================================*/
create unique clustered index storage_cells_PK on storage_cells (
id_cell ASC
);

/*==============================================================*/
/* Table: tariffs                                               */
/*==============================================================*/
create table tariffs 
(
   id_tariffs           integer                        not null,
   name_tariffs         varchar(30)                    null,
   description          varchar(100)                   null,
   price                integer                        null,
   constraint PK_TARIFFS primary key clustered (id_tariffs)
);

alter table contracts
   add constraint FK_CONTRACT_ЕСТЬ_TARIFFS foreign key (id_tariffs)
      references tariffs (id_tariffs)


alter table contracts
   add constraint FK_CONTRACT_ЗАКЛЮЧАЕТ_CLIENTS foreign key (id_client)
      references clients (id_client)


alter table contracts
   add constraint FK_CONTRACT_СОДЕРЖИТ_STATUS_C foreign key (id_status)
      references status_contracts (id_status)


alter table passport
   add constraint FK_PASSPORT_RELATIONS_CLIENTS foreign key (id_client)
      references clients (id_client)


alter table product
   add constraint FK_PRODUCT_В_CONTRACT foreign key (id_contracts)
      references contracts (id_contracts)


alter table product
   add constraint FK_PRODUCT_ПРИНАДЛЕЖ_CLIENTS foreign key (id_client)
      references clients (id_client)


alter table product
   add constraint FK_PRODUCT_ХРАНИТСЯ_STORAGE_ foreign key (id_cell)
      references storage_cells (id_cell)

alter table status_contracts
   add constraint FK_STATUS_C_СОДЕРЖИТ2_CONTRACT foreign key (id_contracts)
      references contracts (id_contracts)


